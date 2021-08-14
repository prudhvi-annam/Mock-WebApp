using MockWebApp.Managers;
using Newtonsoft.Json;
using System;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Routing;


namespace MockWebApp
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.MapHttpRoute("webApi", "api/{controller}/{action}");
            GlobalConfiguration.Configuration.Formatters.Add(new BrowserJsonFormatter());
          
        }
    }


    public class BrowserJsonFormatter : JsonMediaTypeFormatter
    {
        public BrowserJsonFormatter()
        {
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            this.SerializerSettings.Formatting = Formatting.Indented;
        }

        public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
        {
            base.SetDefaultContentHeaders(type, headers, mediaType);
            headers.ContentType = new MediaTypeHeaderValue("application/json");
        }
    }
}


#region Commented Code
//GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;

//GlobalConfiguration.Configuration.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
//GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

//protected void Session_Start(object sender, EventArgs e)
//{
//    HackerNewsApiManager.GetTopStories(0);
//}
#endregion