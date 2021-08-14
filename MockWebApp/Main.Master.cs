using System;
using System.Web;
using System.Web.UI;

namespace MockWebApp
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Link_Login.Visible = !Page.User.Identity.IsAuthenticated;
            Button_Logout.Visible = !Link_Login.Visible;           

        }

        protected void Button_Logout_Click(object sender, EventArgs e)
        {
            var authManager = HttpContext.Current.GetOwinContext().Authentication;
            authManager.SignOut();            
            Response.Redirect("~/Default.aspx");
        }
    }
}