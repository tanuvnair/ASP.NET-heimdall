using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASP.NET_heimdall.Admin
{
    public partial class UserDetails : System.Web.UI.Page
    {
        SqlConnection connection = DatabaseHelper.GetConnection();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] != null)
            {
                // Variables to store attendance statistics with default values set to 0
                int daysPresent = 0;
                int daysLate = 0;
                int daysMissed = 0;
                decimal percentagePresent = 0;

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

                connection.Open();

                // Execute query to calculate attendance statistics
                SqlCommand commandAttendanceStats = new SqlCommand(queryAttendanceStats, connection);
                commandAttendanceStats.Parameters.AddWithValue("@UserID", Request.QueryString["UserID"]);
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