using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace ASP.NET_heimdall
{
    public partial class Default : System.Web.UI.Page
    {
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\projects\heimdall\ASP.NET-heimdall\App_Data\Heimdall.mdf;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            connection.Open();
        }

        protected void SignInButtonClick(object sender, EventArgs e)
        {
            //string username = signInUsername.Text;
            //string password = signInPassword.Text;

            //string query = "INSERT INTO Testing (name) VALUES (@username)";
            //SqlCommand command = new SqlCommand(query, connection);

            //command.Parameters.AddWithValue("@username", username);
            //command.ExecuteNonQuery();


            Response.Write("<h1>REDIRECT</h1>");
        }
    }
}