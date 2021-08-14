using MockWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MockWebApp.Managers
{
    public static class HackerNewsApiManager
    {
        static object LockObject = new object();
        static Tuple<DateTime, StoryModel[]> StoriesCache = null;


        public static async Task<StoryModel[]> GetTopStories(int page, string keywords = null)
        {
            if (StoriesCache == null || StoriesCache.Item1 < DateTime.Now.AddMinutes(-10))
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage httpMessage = httpClient.GetAsync("https://hacker-news.firebaseio.com/v0/topstories.json").Result;
                    if (!httpMessage.IsSuccessStatusCode)
                        throw new Exception($"{(int)httpMessage.StatusCode} ({httpMessage.ReasonPhrase})");
                    lock (LockObject)
                        StoriesCache = Tuple.Create(DateTime.Now, FetchStories(httpMessage.Content.ReadAsAsync<int[]>().Result));
                }
            }           

            return await Task.FromResult(StoriesCache.Item2.Where(x=> x != null && (string.IsNullOrWhiteSpace(keywords) 
                || keywords.ToLower().Split(new[] { " " },StringSplitOptions.RemoveEmptyEntries).Any(k => (x.Title + " " + x.By + " " + x.Score).ToLower().Contains(k))))
                .OrderByDescending(x=> x.Time).Skip(page * 10).Take(10).ToArray());
        }

        public static StoryModel FetchStoryById(int id)
        {
            return StoriesCache.Item2.FirstOrDefault(x => x.Id == id);
        }


        static StoryModel[] FetchStories(params int[] keys)
        {
            var returnList = new List<StoryModel>();
            foreach(var x in keys)
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage httpMessage = httpClient.GetAsync($"https://hacker-news.firebaseio.com/v0/item/{x}.json").Result;
                    if (!httpMessage.IsSuccessStatusCode)
                        throw new Exception($"{(int)httpMessage.StatusCode} ({httpMessage.ReasonPhrase})");

                    returnList.Add(httpMessage.Content.ReadAsAsync<StoryModel>().Result);
                }                
            };
            return returnList.ToArray();
        }

    }
}



#region Commented Code

////{BaseAddress = new Uri("")}
//public static async Task<int[]> GetTopStories()
//{
//    using (var httpClient = new HttpClient())
//    {
//        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//        HttpResponseMessage httpMessage = httpClient.GetAsync("https://hacker-news.firebaseio.com/v0/topstories.json").Result;
//        if (!httpMessage.IsSuccessStatusCode)
//            throw new Exception($"{(int)httpMessage.StatusCode} ({httpMessage.ReasonPhrase})");

//        return await httpMessage.Content.ReadAsAsync<int[]>();
//    }
//}


//public static async Task<StoryModel> GetStory(int id)
//{
//    using (var httpClient = new HttpClient())
//    {
//        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//        HttpResponseMessage httpMessage = httpClient.GetAsync($"https://hacker-news.firebaseio.com/v0/item/{id}.json").Result;
//        if (!httpMessage.IsSuccessStatusCode)
//            throw new Exception($"{(int)httpMessage.StatusCode} ({httpMessage.ReasonPhrase})");

//        return await httpMessage.Content.ReadAsAsync<StoryModel>();
//    }
//}

#endregion