using System;
using System.Configuration;
using System.Data.SqlClient;

namespace ASP.NET_heimdall
{
    public partial class VerifyEmail : System.Web.UI.Page
    {
        SqlConnection connection = DatabaseHelper.GetConnection();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string verificationToken = Request.QueryString["token"];
                if (!string.IsNullOrEmpty(verificationToken))
                {
                    if (VerifyToken(verificationToken))
                    {
                        UpdateVerificationStatus(verificationToken);
                        VerifedOrNotLabel.Text = "You have been verified! You may now close this window.";
                    }
                    else
                    {
                        VerifedOrNotLabel.Text = "Invalid verification token!";
                    }
                }
                else
                {
                    VerifedOrNotLabel.Text = "No verification token found!";
                }
            }
        }

        private bool VerifyToken(string token)
        {

            string query = "SELECT COUNT(*) FROM Users WHERE VerificationToken = @Token";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Token", token);

                connection.Open();
                int tokenCount = (int)command.ExecuteScalar();
                connection.Close();

                return tokenCount > 0;
            }
        }

        private void UpdateVerificationStatus(string token)
        {
            string query = "UPDATE Users SET IsEmailVerified = 1 WHERE VerificationToken = @Token";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Token", token);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}