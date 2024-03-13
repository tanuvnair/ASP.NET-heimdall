using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASP.NET_heimdall
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        SqlConnection connection = DatabaseHelper.GetConnection();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string verificationToken = Request.QueryString["token"];
                if (!string.IsNullOrEmpty(verificationToken))
                {
                    if (!IsValidToken(verificationToken))
                    {
                        // Token is invalid, redirect to an error page or another appropriate page
                        Response.Redirect("InvalidToken.aspx");
                    }
                }
                else
                {
                    // Token parameter is missing, redirect to an error page or another appropriate page
                    Response.Redirect("MissingToken.aspx");
                }
            }
        }

        private bool IsValidToken(string token)
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

        protected void resetPasswordButtonClick(object sender, EventArgs e)
        {
            string password = resetPasswordTextBox.Text;
            string verificationToken = Request.QueryString["token"];

            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string savedPasswordHash = Convert.ToBase64String(hashBytes);

            string updatePasswordQuery = @"UPDATE Users SET Password = @Password, VerificationToken = NULL WHERE VerificationToken = @VerificationToken";

            connection.Open();

            using (SqlCommand command = new SqlCommand(updatePasswordQuery, connection))
            {
                command.Parameters.AddWithValue("@Password", savedPasswordHash);
                command.Parameters.AddWithValue("@VerificationToken", verificationToken);

                command.ExecuteNonQuery();
                connection.Close();
            }

            Response.Redirect("/Default.aspx");
        }
    }
}