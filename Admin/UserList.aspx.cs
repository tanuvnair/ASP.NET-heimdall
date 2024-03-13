using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASP.NET_heimdall.Admin
{
    public partial class UserList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AllUserList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Get the UserID from the current row
                int userId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "UserID"));

                // Set the navigation URL for the row
                e.Row.Attributes["onclick"] = "window.location.href = 'UserDetails.aspx?UserID=" + userId.ToString() + "'";
                e.Row.Style["cursor"] = "pointer"; // Change cursor to pointer when hovering over the row
            }
        }
    }
}