using System;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace ASP.NET_heimdall
{
    public partial class SignUp : System.Web.UI.Page
    {
        // Laptop
        // SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\projects\heimdall\ASP.NET-heimdall\App_Data\Heimdall.mdf;Integrated Security=True");

        // PC
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\projects\algorisys-internship\heimdall\ASP.NET-heimdall\App_Data\Heimdall.mdf;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            connection.Open();
        }

        protected void SignUpButtonClick(object sender, EventArgs e)
        {
            string username = signUpUsername.Text;
            string email = signUpEmail.Text;
            string phoneNumber = signUpPhoneNumber.Text;
            string password = signUpPassword.Text;
            string role = "member";
            DateTime createdAt = DateTime.Now;

            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];    
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string savedPasswordHash = Convert.ToBase64String(hashBytes);

            string query = @"INSERT INTO Users (Username, Email, PhoneNumber, Password, Role, CreatedAt) VALUES (@Username, @Email, @PhoneNumber, @Password, @Role, @CreatedAt)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                command.Parameters.AddWithValue("@Password", savedPasswordHash);
                command.Parameters.AddWithValue("@Role", role);
                command.Parameters.AddWithValue("@CreatedAt", createdAt);

                command.ExecuteNonQuery();
                connection.Close();

                signUpUsername.Text = "";
                signUpEmail.Text = "";
                signUpPhoneNumber.Text = "";
                signUpPassword.Text = "";
                signUpConfirmPassword.Text = "";
            }
        }
    }
}