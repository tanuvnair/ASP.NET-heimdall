using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace ASP.NET_heimdall
{
    public partial class Default : System.Web.UI.Page
    {
        SqlConnection connection = DatabaseHelper.GetConnection();

        private bool IsValidUser(string username, string password, string savedPasswordHash)
        {
            string query = @"SELECT Password FROM Users WHERE Username = @Username";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);

                connection.Open();
                savedPasswordHash = (string)command.ExecuteScalar();
                connection.Close();
            }

            if (savedPasswordHash == null)
                return false;

            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    return false;

            return true;
                    
        }

        private void CreateSession(string username)
        {
            string query = "SELECT UserID, Email, Role, LastLogin FROM Users WHERE Username = @Username";

            connection.Open();

            using(SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Session["UserID"] = reader["UserID"];
                        Session["Username"] = username;
                        Session["Email"] = reader["Email"];
                        Session["Role"] = reader["Role"];
                        Session["LastLogin"] = reader["LastLogin"];
                    }
                }
            }

            connection.Close();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void SignInButtonClick(object sender, EventArgs e)
        {

            string username = signInUsername.Text;
            string password = signInPassword.Text;
            string savedPasswordHash = null;

            signInUsername.Text = "";
            signInPassword.Text = "";

            if(IsValidUser(username, password, savedPasswordHash))
            {
                CreateSession(username);
                Response.Redirect("Dashboard.aspx");
            } else
            {
                invalidCredentials.Text = "*Invalid Credentials";
            }
        }
    }
}