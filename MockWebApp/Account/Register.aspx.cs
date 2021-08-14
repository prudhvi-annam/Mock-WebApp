using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

using System.Linq;
using System.Web;

namespace MockWebApp.Account
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ErrorMessage.Text = string.Empty;
            var returnUrl = Request.QueryString["ReturnUrl"];
            if (!string.IsNullOrWhiteSpace(returnUrl))
                Link_Login.NavigateUrl += "?ReturnUrl=" + returnUrl;
        }

        protected void Button_Register_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBox_Email.Text))
                ErrorMessage.Text = "Email must be entered.";
            if (string.IsNullOrWhiteSpace(ErrorMessage.Text) && string.IsNullOrWhiteSpace(TextBox_Password.Text))
                ErrorMessage.Text = "Password must be entered.";
            if (string.IsNullOrWhiteSpace(ErrorMessage.Text) && string.Compare(TextBox_Password.Text, TextBox_ConfirmPassword.Text, true) != 0)
                ErrorMessage.Text = "Password and Confirmation Password do not match.";

            if(string.IsNullOrWhiteSpace(ErrorMessage.Text))
            {
                using (var manager = new UserManager<IdentityUser>(new UserStore<IdentityUser>()))
                {
                    var identity = new IdentityUser { UserName = TextBox_Email.Text, Email = TextBox_Email.Text };
                    IdentityResult iResult = manager.Create(identity, TextBox_Password.Text);
                    if(iResult.Succeeded)
                    {
                        var authManager = HttpContext.Current.GetOwinContext().Authentication;
                        var uIdenttiy = manager.CreateIdentity(identity, DefaultAuthenticationTypes.ApplicationCookie);
                        authManager.SignIn(new AuthenticationProperties { }, uIdenttiy);
                        var redirectUrl = Request.QueryString["ReturnUrl"];
                        if (string.IsNullOrWhiteSpace(redirectUrl))
                            redirectUrl = "~/Default.aspx";
                        Response.Redirect(redirectUrl);                        
                    }
                    else
                    {
                        ErrorMessage.Text = iResult.Errors.FirstOrDefault();
                    }
                }
            }

        }
    }
}