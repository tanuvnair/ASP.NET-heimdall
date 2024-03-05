using System;
using System.Data.SqlClient;

namespace ASP.NET_heimdall
{
    public partial class SignUp : System.Web.UI.Page
    {
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\projects\heimdall\ASP.NET-heimdall\App_Data\Heimdall.mdf;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            connection.Open();
        }

        protected void SignUpButtonClick(object sender, EventArgs e)
        {
            Response.Write("hello");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Write("hello");
            string username = signUpUsername.Text;
            string email = signUpEmail.Text;
            string phoneNumber = signUpPhoneNumber.Text;
            string role = "member";
            DateTime createdAt = DateTime.Now;

            //Hash password tomorrow pls
            string password = signUpPassword.Text;

            string query = "INSERT INTO Users (Username, Email, PhoneNumber, Password, Role, CreatedAt) VALUES (@Username, @Email, @PhoneNumber, @Password, @Role, @CreatedAt)";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
            command.Parameters.AddWithValue("@Password", password);
            command.Parameters.AddWithValue("@Role", role);
            command.Parameters.AddWithValue("@CreatedAt", createdAt);

            command.ExecuteNonQuery();

            Response.Write("<h1>TESTING</h1>");
        }

        //protected void SignUpButtonClick(object sender, EventArgs e)
        //{
        //    //string username = signUpUsername.Text;
        //    //string email = signUpEmail.Text;
        //    //string phoneNumber = signUpPhoneNumber.Text;
        //    //string role = "member";
        //    //DateTime createdAt = DateTime.Now;

        //    //// Hash password tomorrow pls
        //    //string password = signUpPassword.Text;

        //    //string query = "INSERT INTO Users (Username, Email, PhoneNumber, Password, Role, CreatedAt) VALUES (@Username, @Email, @PhoneNumber, @Password, @Role, @CreatedAt)";

        //    //SqlCommand command = new SqlCommand(query, connection);


        //    //command.Parameters.AddWithValue("@Username", username);
        //    //command.Parameters.AddWithValue("@Email", email);
        //    //command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
        //    //command.Parameters.AddWithValue("@Password", password);
        //    //command.Parameters.AddWithValue("@Role", role);
        //    //command.Parameters.AddWithValue("@CreatedAt", createdAt);

        //    //command.ExecuteNonQuery();    

        //    Response.Write("<h1>TESTING</h1>");
        //}
    }
}