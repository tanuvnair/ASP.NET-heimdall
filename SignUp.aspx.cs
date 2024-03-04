using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace ASP.NET_heimdall
{
    public partial class SignUp : System.Web.UI.Page
    {
        //SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            //connection.Open();
        }

        protected void SignUpButtonClick(object sender, EventArgs e)
        {
            string username = signUpUsername.Text;
            string email = signUpEmail.Text;
            string phoneNumber = signUpPhoneNumber.Text;
            string role = "default";
            DateTime createdAt = DateTime.Now;

            // Hash password tomorrow pls
            string password = signUpPassword.Text;

            string query = @"
            INSERT INTO Users (Username, Email, PhoneNumber, Password, Role, CreatedAt)
            VALUES (@Username, @Email, @PhoneNumber, @Password, @Role, @CreatedAt);";

            //using (SqlCommand command = new SqlCommand(query, connection))
            //{
            //    command.Parameters.AddWithValue("@Username", username);
            //    command.Parameters.AddWithValue("@Email", email);
            //    command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
            //    command.Parameters.AddWithValue("@Password", password);
            //    command.Parameters.AddWithValue("@Role", role);
            //    command.Parameters.AddWithValue("@CreatedAt", createdAt);

            //    try
            //    {
            //        command.ExecuteNonQuery();
            //        Console.WriteLine("User data inserted successfully.");
            //    }
            //    catch (SqlException ex)
            //    {
            //        Console.WriteLine($"Error inserting user data: {ex.Message}");
            //    }
            //}

            Response.Redirect("Dashboard.aspx");
        }
    }
}