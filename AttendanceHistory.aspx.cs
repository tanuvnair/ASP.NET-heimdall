using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
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
                string attendanceDate = e.Row.Cells[0].Text;
                DateTime parsedDate;
                if (DateTime.TryParse(attendanceDate, out parsedDate))
                {
                    e.Row.Cells[0].Text = parsedDate.ToString("dd-MM-yyyy");
                }

                string attendanceTime = e.Row.Cells[1].Text;
                TimeSpan parsedTime;
                if (TimeSpan.TryParse(attendanceTime, out parsedTime))
                {
                    e.Row.Cells[1].Text = parsedTime.ToString(@"hh\:mm\:ss");
                }
            }
        }

    }
}