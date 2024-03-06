using System;
using System.Configuration;
using System.Data.SqlClient;

namespace ASP.NET_heimdall
{
    public partial class Dashboard : System.Web.UI.Page
    {
        SqlConnection connection = DatabaseHelper.GetConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void RecordAttendanceButtonClick(object sender, EventArgs e)
        {

        }
    }
}