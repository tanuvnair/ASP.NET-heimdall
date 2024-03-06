using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;

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

                } else
                {
                    ShowAttendanceDetails.CssClass += " d-none";
                    AttendanceRecordButtonWrapper.CssClass.Replace("d-none", "").Trim();
                }
                
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

            if (recordCount != 0)
            {
                return true;
            }

            return false;
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