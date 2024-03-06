using System;
using System.Data;
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
                    AttendanceRecordButtonWrapper.CssClass += " d-none";
                    ShowAttendanceDetails.CssClass.Replace("d-none", "").Trim();
                }
                else
                {
                    ShowAttendanceDetails.CssClass += " d-none";
                    AttendanceRecordButtonWrapper.CssClass.Replace("d-none", "").Trim();
                }

                // Variables to store attendance statistics with default values set to 0
                int daysPresent = 0;
                int daysLate = 0;
                int daysMissed = 0;
                decimal percentagePresent = 0;

                // Variable to store today's attendance time
                TimeSpan? todayAttendanceTime = null;

                // Query to calculate attendance statistics
                string queryAttendanceStats = @"
            DECLARE @TotalDays INT;
            DECLARE @DaysPresent INT;
            DECLARE @DaysLate INT;
            DECLARE @DaysMissed INT;
            DECLARE @PercentagePresent DECIMAL(5, 2);

            -- Calculate total number of days
            SELECT @TotalDays = DATEDIFF(DAY, MIN(AttendanceDate), MAX(AttendanceDate)) + 1 FROM AttendanceRecords WHERE UserID = @UserID;

            -- Calculate days present
            SELECT @DaysPresent = COUNT(DISTINCT CONVERT(DATE, AttendanceDate)) FROM AttendanceRecords WHERE UserID = @UserID AND Status = 'Present';

            -- Calculate days late
            SELECT @DaysLate = COUNT(DISTINCT CONVERT(DATE, AttendanceDate)) FROM AttendanceRecords WHERE UserID = @UserID AND Status = 'Present' AND AttendanceTime > '09:30:00';

            -- Calculate days missed
            SET @DaysMissed = @TotalDays - @DaysPresent - @DaysLate;

            -- Calculate percentage of days present
            SET @PercentagePresent = ((@DaysPresent * 1.0) / @TotalDays) * 100;

            -- Select attendance statistics
            SELECT 
                @DaysPresent AS DaysPresent,
                @DaysLate AS DaysLate,
                @DaysMissed AS DaysMissed,
                @PercentagePresent AS PercentagePresent;";

                // Query to get today's attendance time for the specified user
                string queryTodayAttendanceTime = @"
            SELECT AttendanceTime
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

                DaysPresent.Text = $"{daysPresent}";
                DaysLate.Text = $"{daysLate}";
                DaysMissed.Text = $"{daysMissed}";
                AttendancePercentage.Text = $"{percentagePresent}%";
                PunchedInTime.Text = todayAttendanceTime?.ToString() ?? "No attendance time recorded for today.";
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

        protected void RecordAttendanceButtonClick(object sender, EventArgs e)
        {
            connection.Open();

            string query = @"INSERT INTO [dbo].[AttendanceRecords] ([UserID], [AttendanceDate], [AttendanceTime], [Status]) VALUES (@UserID, @AttendanceDate, @AttendanceTime, @Status)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", Session["UserID"]);
                command.Parameters.AddWithValue("@AttendanceDate", DateTime.Today);
                command.Parameters.AddWithValue("@AttendanceTime", DateTime.Now.TimeOfDay);
                command.Parameters.AddWithValue("@Status", "Present");

                command.ExecuteNonQuery();
            }

            connection.Close();

            Response.Redirect(Request.RawUrl);
        }
    }
}
