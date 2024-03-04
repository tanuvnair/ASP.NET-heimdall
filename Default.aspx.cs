using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace ASP.NET_heimdall
{
    public partial class Default : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void SignIn(object sender, EventArgs e)
        {
        }

        protected void SignUp(object sender, EventArgs e)
        {
        }
    }
}