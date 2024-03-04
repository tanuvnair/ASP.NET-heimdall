using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace ASP.NET_heimdall
{
    public partial class Default : System.Web.UI.Page
    {
        //SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            //connection.Open();
        }

        protected void SignInButtonClick(object sender, EventArgs e)
        {
            string username = signInUsername.Text;
            string password = signInPassword.Text;

            Response.Redirect("Dashboard.aspx");
        }
    }
}