using System;
using System.Data.SqlClient;

namespace ASP.NET_heimdall
{
    public partial class Dashboard : System.Web.UI.Page
    {
        SqlConnection connection = DatabaseHelper.GetConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] != null)
            {
                if (IsPunchedIn((int)Session["UserID"]))
                {
                    PunchInWrapper.CssClass += " d-none";
                    PunchOutWrapper.CssClass.Replace("d-none", "").Trim();

                    if (IsPunchedOut((int)Session["UserID"]))
                    {
                        PunchOutWrapper.CssClass += " d-none";
                        AlreadyPunchedOutWrapper.CssClass.Replace("d-none", "").Trim();
                    }
                    else
                    {
                        AlreadyPunchedOutWrapper.CssClass += " d-none";
                        PunchOutWrapper.CssClass.Replace("d-none", "").Trim();
                    }
                }
                else
                {
                    PunchOutWrapper.CssClass += " d-none";
                    AlreadyPunchedOutWrapper.CssClass += " d-none";
                    PunchInWrapper.CssClass.Replace("d-none", "").Trim();
                }

                // Variables to store attendance statistics with default values set to 0
                int daysPresent = 0;
                int daysLate = 0;
                int daysMissed = 0;
                decimal percentagePresent = 0;

                // Variable to store today's attendance time
                TimeSpan? todayAttendanceTime = null;
                TimeSpan? todayPunchedOutTime = null;

                // Query to calculate attendance statistics
                string queryAttendanceStats = @"
DECLARE @TotalDaysInMonth INT;
DECLARE @DaysPresent INT;
DECLARE @DaysLate INT;
DECLARE @DaysMissed INT;
DECLARE @PercentagePresent DECIMAL(5, 2);

-- Calculate the first day of the current month
DECLARE @FirstDayOfMonth DATE = DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0);

-- Calculate the last day of the current month
DECLARE @LastDayOfMonth DATE = DATEADD(DAY, -1, DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0));

-- Calculate total number of days in the current month
SELECT @TotalDaysInMonth = DATEDIFF(DAY, @FirstDayOfMonth, @LastDayOfMonth) + 1;

-- Calculate days present in the current month
SELECT @DaysPresent = COUNT(DISTINCT CASE WHEN Status = 'Present' THEN CONVERT(DATE, AttendanceDate) END) 
FROM AttendanceRecords 
WHERE UserID = @UserID 
AND AttendanceDate >= @FirstDayOfMonth 
AND AttendanceDate <= @LastDayOfMonth;

-- Calculate days late in the current month
SELECT @DaysLate = COUNT(DISTINCT CASE WHEN Status = 'Present' AND CONVERT(TIME, PunchInTime) > '09:30:00' THEN CONVERT(DATE, AttendanceDate) END) 
FROM AttendanceRecords 
WHERE UserID = @UserID 
AND AttendanceDate >= @FirstDayOfMonth 
AND AttendanceDate <= @LastDayOfMonth;

-- Calculate days missed in the current month
SET @DaysMissed = @TotalDaysInMonth - @DaysPresent - @DaysLate;

-- Calculate percentage of days present in the current month
SET @PercentagePresent = CASE 
                            WHEN @TotalDaysInMonth = 0 THEN 0 -- Avoid division by zero
                            ELSE ((@DaysPresent * 1.0) / @TotalDaysInMonth) * 100 
                         END;

-- Select attendance statistics
SELECT 
    @DaysPresent AS DaysPresent,
    @DaysLate AS DaysLate,
    @DaysMissed AS DaysMissed,
    @PercentagePresent AS PercentagePresent;";


                // Query to get today's attendance time for the specified user
                string queryTodayAttendanceTime = @"
            SELECT PunchInTime, PunchOutTime
            FROM AttendanceRecords
            WHERE UserID = @UserID AND CAST(AttendanceDate AS DATE) = CAST(GETDATE() AS DATE);";

                connection.Open();

                // Execute query to calculate attendance statistics
                SqlCommand commandAttendanceStats = new SqlCommand(queryAttendanceStats, connection);
                commandAttendanceStats.Parameters.AddWithValue("@UserID", Session["UserID"]);
                SqlDataReader readerAttendanceStats = commandAttendanceStats.ExecuteReader();

                if (readerAttendanceStats.HasRows)
                {
                    readerAttendanceStats.Read();
                    daysPresent = readerAttendanceStats.IsDBNull(0) ? 0 : readerAttendanceStats.GetInt32(0);
                    daysLate = readerAttendanceStats.IsDBNull(1) ? 0 : readerAttendanceStats.GetInt32(1);
                    daysMissed = readerAttendanceStats.IsDBNull(2) ? 0 : readerAttendanceStats.GetInt32(2);
                    percentagePresent = readerAttendanceStats.IsDBNull(3) ? 0 : readerAttendanceStats.GetDecimal(3);
                }
                else
                {
                    Console.WriteLine("No attendance statistics found.");
                }
                readerAttendanceStats.Close();

                // Execute query to get today's attendance time
                SqlCommand commandTodayAttendanceTime = new SqlCommand(queryTodayAttendanceTime, connection);
                commandTodayAttendanceTime.Parameters.AddWithValue("@UserID", Session["UserID"]);
                SqlDataReader readerTodayAttendanceTime = commandTodayAttendanceTime.ExecuteReader();

                if (readerTodayAttendanceTime.HasRows)
                {
                    readerTodayAttendanceTime.Read();
                    todayAttendanceTime = readerTodayAttendanceTime.IsDBNull(0) ? (TimeSpan?)null : readerTodayAttendanceTime.GetTimeSpan(0);
                    todayPunchedOutTime = readerTodayAttendanceTime.IsDBNull(1) ? (TimeSpan?)null : readerTodayAttendanceTime.GetTimeSpan(1);
                }
                else
                {
                    Console.WriteLine("No attendance time recorded for today.");
                }
                readerTodayAttendanceTime.Close();

                if (todayAttendanceTime.HasValue)
                {
                    TimeSpan adjustedTime = new TimeSpan(todayAttendanceTime.Value.Hours, todayAttendanceTime.Value.Minutes, todayAttendanceTime.Value.Seconds);
                    todayAttendanceTime = adjustedTime;
                }

                if (todayPunchedOutTime.HasValue)
                {
                    TimeSpan adjustedOutTime = new TimeSpan(todayPunchedOutTime.Value.Hours, todayPunchedOutTime.Value.Minutes, todayPunchedOutTime.Value.Seconds);
                    todayPunchedOutTime = adjustedOutTime;
                }

                connection.Close();

                DaysPresent.Text = $"{daysPresent}";
                DaysLate.Text = $"{daysLate}";
                DaysMissed.Text = $"{daysMissed}";
                AttendancePercentage.Text = $"{percentagePresent}%";
                PunchedInTime.Text = todayAttendanceTime?.ToString() ?? "No attendance time recorded for today.";
                PunchedOutTime.Text = todayPunchedOutTime?.ToString() ?? "No punch-out time recorded for today.";
            }
        }



        protected bool IsPunchedIn(int userID)
        {
            string query = @"SELECT COUNT(*) FROM [dbo].[AttendanceRecords] WHERE [UserID] = @UserID AND [AttendanceDate] = @AttendanceDate";
            int recordCount = 0;

            connection.Open();

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userID);
                command.Parameters.AddWithValue("@AttendanceDate", DateTime.Today);

                recordCount = (int)command.ExecuteScalar();
            }

            connection.Close();

            return recordCount != 0;
        }

        protected bool IsPunchedOut(int userID)
        {
            string query = @"SELECT COUNT(*) FROM [dbo].[AttendanceRecords] WHERE [UserID] = @UserID AND [AttendanceDate] = @AttendanceDate AND [PunchOutTime] IS NOT NULL";
            int recordCount = 0;

            connection.Open();

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userID);
                command.Parameters.AddWithValue("@AttendanceDate", DateTime.Today);

                recordCount = (int)command.ExecuteScalar();
            }

            connection.Close();

            return recordCount > 0;
        }

        protected void PunchInButtonClick(object sender, EventArgs e)
        {
            connection.Open();

            string query = @"INSERT INTO [dbo].[AttendanceRecords] ([UserID], [AttendanceDate], [PunchInTime], [PunchOutTime], [Status]) VALUES (@UserID, @AttendanceDate, @PunchInTime, NULL, @Status)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", Session["UserID"]);
                command.Parameters.AddWithValue("@AttendanceDate", DateTime.Today);
                command.Parameters.AddWithValue("@PunchInTime", DateTime.Now.TimeOfDay);
                command.Parameters.AddWithValue("@Status", "Present");

                command.ExecuteNonQuery();
            }

            connection.Close();

            Response.Redirect(Request.RawUrl);
        }

        protected void PunchOutButtonClick(object sender, EventArgs e)
        {
            connection.Open();

            string query = @"UPDATE [dbo].[AttendanceRecords] SET [PunchOutTime] = @PunchOutTime WHERE [UserID] = @UserID AND [AttendanceDate] = @AttendanceDate";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", Session["UserID"]);
                command.Parameters.AddWithValue("@AttendanceDate", DateTime.Today);
                command.Parameters.AddWithValue("@PunchOutTime", DateTime.Now.TimeOfDay);

                command.ExecuteNonQuery();
            }

            connection.Close();

            Response.Redirect(Request.RawUrl);
        }
    }
}
