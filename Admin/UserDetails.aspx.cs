using System;
using System.Data.SqlClient;

namespace ASP.NET_heimdall.Admin
{
    public partial class UserDetails : System.Web.UI.Page
    {
        SqlConnection connection = DatabaseHelper.GetConnection();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] != null)
            {
                if (Request.QueryString["UserID"] != null)
                {
                    int userID = Convert.ToInt32(Request.QueryString["UserID"]);

                    // Variables to store attendance statistics with default values set to 0
                    int daysPresent = 0;
                    int daysLate = 0;
                    int daysMissed = 0;
                    decimal percentagePresent = 0;

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

                    connection.Open();

                    // Execute query to calculate attendance statistics
                    SqlCommand commandAttendanceStats = new SqlCommand(queryAttendanceStats, connection);
                    commandAttendanceStats.Parameters.AddWithValue("@UserID", userID);
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

                    connection.Close();

                    DaysPresent.Text = $"{daysPresent}";
                    DaysLate.Text = $"{daysLate}";
                    DaysMissed.Text = $"{daysMissed}";
                    AttendancePercentage.Text = $"{percentagePresent}%";
                }
            }
        }
    }
}
