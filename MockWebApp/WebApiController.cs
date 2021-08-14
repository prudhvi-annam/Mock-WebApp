using MockWebApp.Managers;
using MockWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MockWebApp
{
    public class WebApiController : ApiController
    {
        [AcceptVerbs("Get")]
        public async Task<StoryModel[]> TopStories(int? page, string keywords)
        {
            return await HackerNewsApiManager.GetTopStories(page ?? 0, keywords);
        }

        [AcceptVerbs("Get")]
        public async Task<StoryModel> Story(int id)
        {
            return await Task.FromResult(HackerNewsApiManager.FetchStoryById(id));
        }
    }
}