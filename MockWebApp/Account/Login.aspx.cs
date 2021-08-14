using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Web;

namespace MockWebApp.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ErrorMessage.Text = string.Empty;
            var returnUrl = Request.QueryString["ReturnUrl"];
            if (!string.IsNullOrWhiteSpace(returnUrl))
                Link_Register.NavigateUrl += "?ReturnUrl=" + returnUrl;
        }

        protected void Button_Login_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBox_Email.Text))
                ErrorMessage.Text = "Email must be entered.";
            if (string.IsNullOrWhiteSpace(ErrorMessage.Text) && string.IsNullOrWhiteSpace(TextBox_Password.Text))
                ErrorMessage.Text = "Password must be entered.";

            if(string.IsNullOrWhiteSpace(ErrorMessage.Text))
            {
                using (var manager = new UserManager<IdentityUser>(new UserStore<IdentityUser>()))
                {
                    var identity = manager.Find(TextBox_Email.Text, TextBox_Password.Text);

                    if (identity == null)
                        ErrorMessage.Text = "Invalid login or password.";
                    else
                    {
                        var authManager = HttpContext.Current.GetOwinContext().Authentication;
                        var uIdenttiy = manager.CreateIdentity(identity, DefaultAuthenticationTypes.ApplicationCookie);
                        authManager.SignIn(new AuthenticationProperties { IsPersistent = false }, uIdenttiy);

                        var redirectUrl = Request.QueryString["ReturnUrl"];
                        if (string.IsNullOrWhiteSpace(redirectUrl))
                            redirectUrl = "~/Default.aspx";
                        Response.Redirect(redirectUrl);
                    }                 
                }
            }

        }
    }
}