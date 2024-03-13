using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASP.NET_heimdall
{
    public partial class Site : System.Web.UI.MasterPage
    {
        SqlConnection connection = DatabaseHelper.GetConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                Response.Redirect("/Default.aspx");
            }

            if ((string)Session["Role"] == "admin")
            {
                UserSidebar.CssClass += " d-none";
                AdminSidebar.CssClass.Replace("d-none", "").Trim();
            }
            else
            {
                AdminSidebar.CssClass += " d-none";
                UserSidebar.CssClass.Replace("d-none", "").Trim();
            }

            string username = (string)Session["Username"];

            usernameLabel.Text = "Welcome, " + username;
        }

        protected void SignOutButtonClick(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("/Default.aspx");
        }

        protected void ChangePasswordButtonClick(object sender, EventArgs e)
        {
            string verificationToken = VerificationTokenGenerator.GenerateToken((string)Session["Email"]);

            string query = @"UPDATE Users SET VerificationToken = @VerificationToken WHERE Email = @Email";

            connection.Open();

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@VerificationToken", verificationToken);
                command.Parameters.AddWithValue("@Email", Session["Email"]);

                command.ExecuteNonQuery();
                connection.Close();
            }

            Session.Clear();
            Session.Abandon();

            Response.Redirect($"/ResetPassword.aspx?token={verificationToken}");
        }
    }
}