using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Configuration;

namespace ASP.NET_heimdall
{
    public partial class SignUp : System.Web.UI.Page
    {
        SqlConnection connection = DatabaseHelper.GetConnection();

        protected void Page_Load(object sender, EventArgs e)
        {
            connection.Open();
        }

        protected void SendVerificationEmail(string email, string verificationToken)
        {
            string host = @"https://localhost:44390/";
            string fromEmail = "rajnimurali@yahoo.com";
            string subject = "Heimdall: Email Verification";
            string body = $"Please click the following link to verify your email: <a href='{host}VerifyEmail.aspx?token={verificationToken}'>Verify Email</a>";

            MailMessage message = new MailMessage(fromEmail, email, subject, body);
            message.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Send(message);
        }

        protected void SignUpButtonClick(object sender, EventArgs e)
        {
            string username = signUpUsername.Text;
            string email = signUpEmail.Text;
            string phoneNumber = signUpPhoneNumber.Text;
            string password = signUpPassword.Text;
            string role = "member";
            DateTime createdAt = DateTime.Now;

            signUpUsername.Text = "";
            signUpEmail.Text = "";
            signUpPhoneNumber.Text = "";
            signUpPassword.Text = "";
            signUpConfirmPassword.Text = "";

            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string savedPasswordHash = Convert.ToBase64String(hashBytes);
            string verificationToken = VerificationTokenGenerator.GenerateToken(email);

            string query = @"INSERT INTO Users (Username, Email, VerificationToken, PhoneNumber, Password, Role, CreatedAt) VALUES (@Username, @Email, @VerificationToken, @PhoneNumber, @Password, @Role, @CreatedAt)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@VerificationToken", verificationToken);
                command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                command.Parameters.AddWithValue("@Password", savedPasswordHash);
                command.Parameters.AddWithValue("@Role", role);
                command.Parameters.AddWithValue("@CreatedAt", createdAt);

                command.ExecuteNonQuery();
                connection.Close();
            }

            SendVerificationEmail(email, verificationToken);
        }
    }
}