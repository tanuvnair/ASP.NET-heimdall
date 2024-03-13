using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace ASP.NET_heimdall
{
    public partial class AttendanceHistory : System.Web.UI.Page
    {
        SqlConnection connection = DatabaseHelper.GetConnection();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void AttendanceHistoryGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Assuming the date column is at index 0
                string attendanceDate = e.Row.Cells[0].Text;
                DateTime parsedDate;
                if (DateTime.TryParse(attendanceDate, out parsedDate))
                {
                    e.Row.Cells[0].Text = parsedDate.ToString("dd-MM-yyyy");
                }

                // Assuming the time column is at index 1
                string punchedInTime = e.Row.Cells[1].Text;
                TimeSpan parsedInTime;
                if (TimeSpan.TryParse(punchedInTime, out parsedInTime))
                {
                    e.Row.Cells[1].Text = parsedInTime.ToString(@"hh\:mm\:ss");
                }

                // Assuming the time column is at index 2
                string punchedOutTime = e.Row.Cells[2].Text;
                TimeSpan parsedOutTime;
                if (TimeSpan.TryParse(punchedOutTime, out parsedOutTime))
                {
                    e.Row.Cells[2].Text = parsedOutTime.ToString(@"hh\:mm\:ss");
                }
            }
        }
    }
}
