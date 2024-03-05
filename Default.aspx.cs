using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace ASP.NET_heimdall
{
    public partial class Default : System.Web.UI.Page
    {
        // Laptop
        // SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\projects\heimdall\ASP.NET-heimdall\App_Data\Heimdall.mdf;Integrated Security=True");

        // PC
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\projects\algorisys-internship\heimdall\ASP.NET-heimdall\App_Data\Heimdall.mdf;Integrated Security=True");
        
        protected void Page_Load(object sender, EventArgs e)
        {
            connection.Open();
        }

        protected void SignInButtonClick(object sender, EventArgs e)
        {
            string username = signInUsername.Text;
            string password = signInPassword.Text;
            string savedPasswordHash = null;

            signInUsername.Text = "";
            signInPassword.Text = "";

            string query = @"SELECT Password FROM Users WHERE Username = @Username";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                savedPasswordHash = (string)command.ExecuteScalar();
                connection.Close();
            }

            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    throw new UnauthorizedAccessException();
                else
                    Response.Redirect("Dashboard.aspx");
        }
    }
}