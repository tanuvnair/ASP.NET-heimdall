using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASP.NET_heimdall
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                Response.Redirect("/Default.aspx");
            }

            if ((string)Session["Role"] == "admin") 
            {
                UserSidebar.CssClass += " d-none";
                AdminSidebar.CssClass.Replace("d-none", "").Trim();
            }
            else
            {
                AdminSidebar.CssClass += " d-none";
                UserSidebar.CssClass.Replace("d-none", "").Trim();
            }

            usernameLabel.Text = "Welcome, " + Session["username"];
        }

        protected void SignOutButtonClick(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("/Default.aspx");
        }
    }
}