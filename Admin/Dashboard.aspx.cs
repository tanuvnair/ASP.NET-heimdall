using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASP.NET_heimdall.Admin
{
    public partial class Dashboard : System.Web.UI.Page
    {
        SqlConnection connection = DatabaseHelper.GetConnection();

        protected void Page_Load(object sender, EventArgs e)
        {
            GenerateAccountEmailSuccessLabel.Text = "";
         
            int userCount = 0;
            int adminCount = 0;

            string query = "SELECT COUNT(*) FROM Users";
            string adminQuery = "SELECT COUNT(*) FROM Users WHERE Role = 'admin'";

            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlCommand adminCommand = new SqlCommand(adminQuery, connection))
            {
                try
                {
                    connection.Open();
                    userCount = (int)command.ExecuteScalar();
                    adminCount = (int)adminCommand.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            connection.Close();

            totalUsers.Text = $"{userCount}";
            totalAdmin.Text = $"{adminCount}";
        }

        protected void SendSignUpEmail(string email, string verificationToken)
        {
            string host = @"https://localhost:44390/";
            string fromEmail = "rajnimurali@yahoo.com";
            string subject = "Heimdall: Employee Sign Up";
            string body = $"Please click the following link to Sign Up: <a href='{host}SignUp.aspx?token={verificationToken}'>Sign Up</a>";

            MailMessage message = new MailMessage(fromEmail, email, subject, body);
            message.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Send(message);
        }

        protected void GenerateAccountButtonClick(object sender, EventArgs e)
        {
            string email = generateAccountEmailTextBox.Text;
            string verificationToken = VerificationTokenGenerator.GenerateToken(email);

            string query = @"INSERT INTO Users (Email, VerificationToken) VALUES (@Email, @VerificationToken)";

            connection.Open();

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@VerificationToken", verificationToken);

                command.ExecuteNonQuery();
                connection.Close();
            }

            SendSignUpEmail(email, verificationToken);

            GenerateAccountEmailSuccessLabel.Text = "A Sign Up link has been sent to the employee.";
        }

    }
}