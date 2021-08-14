using MockWebApp.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MockWebApp.Secured
{
    public partial class FeedInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var id = Request.QueryString["id"];
            if (!string.IsNullOrWhiteSpace(id))
            {
                var model = HackerNewsApiManager.FetchStoryById(Convert.ToInt32(id));
                TextBox_ID.Text = model.Id.ToString();
                TextBox_Title.Text = model.Title;
                TextBox_By.Text = model.By;
                TextBox_Score.Text = model.Score?.ToString();
                Link_URL.NavigateUrl = model.Url;
                Link_URL.Text = model.Url;
            }
        }
    }
}