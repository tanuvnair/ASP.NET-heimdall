using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASP.NET_heimdall
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        SqlConnection connection = DatabaseHelper.GetConnection();

        protected void Page_Load(object sender, EventArgs e)
        {
            ForgotPasswordEmailSuccessLabel.Text = "";
        }

        protected void SendPasswordResetEmail(string email, string verificationToken)
        {
            string host = @"https://localhost:44390/";
            string fromEmail = "rajnimurali@yahoo.com";
            string subject = "Heimdall: Password Reset";
            string body = $"Please click the following link to reset your password: <a href='{host}ResetPassword.aspx?token={verificationToken}'>Reset Password</a>";

            MailMessage message = new MailMessage(fromEmail, email, subject, body);
            message.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Send(message);
        }

        protected void resetPasswordButtonClick(object sender, EventArgs e)
        {
            string email = forgotPasswordEmail.Text;
            string verificationToken = VerificationTokenGenerator.GenerateToken(email);

            string query = @"UPDATE Users SET VerificationToken = @VerificationToken WHERE Email = @Email";

            connection.Open();

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@VerificationToken", verificationToken);
                command.Parameters.AddWithValue("@Email", email);

                command.ExecuteNonQuery();
                connection.Close();
            }

            SendPasswordResetEmail(email, verificationToken);

            ForgotPasswordEmailSuccessLabel.Text = "A mail has been sent to your email to reset your password.";
        }
    }
}