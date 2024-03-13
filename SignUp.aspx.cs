using System;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace ASP.NET_heimdall
{
    public partial class SignUp : System.Web.UI.Page
    {
        SqlConnection connection = DatabaseHelper.GetConnection();

        protected void Page_Load(object sender, EventArgs e)
        {
            SignUpSuccessLabel.Text = "";

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

        protected void SignUpButtonClick(object sender, EventArgs e)
        {
            string username = signUpUsername.Text;
            string phoneNumber = signUpPhoneNumber.Text;
            string password = signUpPassword.Text;
            string role = "member";
            DateTime createdAt = DateTime.Now;

            signUpUsername.Text = "";
            signUpPhoneNumber.Text = "";
            signUpPassword.Text = "";

            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string savedPasswordHash = Convert.ToBase64String(hashBytes);

            string verificationToken = Request.QueryString["token"]; // Retrieve verification token from query string

            // Retrieve email associated with the verification token from the database
            string email = "";
            using (SqlCommand command = new SqlCommand("SELECT Email FROM Users WHERE VerificationToken = @VerificationToken", connection))
            {
                command.Parameters.AddWithValue("@VerificationToken", verificationToken);
                connection.Open();
                email = command.ExecuteScalar()?.ToString();
                connection.Close();
            }

            if (string.IsNullOrEmpty(email))
            {
                // Handle case where verification token is not found
                Response.Redirect("MissingToken.aspx");
                return;
            }

            string query = @"UPDATE Users 
                     SET Username = @Username, 
                         PhoneNumber = @PhoneNumber, 
                         Password = @Password, 
                         Role = @Role, 
                         CreatedAt = @CreatedAt, 
                         VerificationToken = NULL 
                     WHERE Email = @Email AND VerificationToken = @VerificationToken";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                command.Parameters.AddWithValue("@Password", savedPasswordHash);
                command.Parameters.AddWithValue("@Role", role);
                command.Parameters.AddWithValue("@CreatedAt", createdAt);
                command.Parameters.AddWithValue("@VerificationToken", verificationToken);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            SignUpSuccessLabel.Text = "Your account has been created.";
        }


    }
}