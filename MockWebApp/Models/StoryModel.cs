using MockWebApp.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MockWebApp.Models
{
    public class StoryModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("by")]
        public string By { get; set; }
        
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("descendants")]
        public int? Descendants { get; set; }

        [JsonProperty("score")]
        public int? Score { get; set; }

        [JsonProperty("time"), JsonConverter(typeof(UnixDateConverter))]
        public DateTime? Time { get; set; }
      
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("kids")]
        public int[] Kids { get; set; }

        [JsonProperty("duration")]
        public string Duration
        {
            get
            {

                if (!Time.HasValue)
                    return string.Empty;
                var timeSpan = DateTime.Now - Time.Value;
                if (timeSpan.Days > 0)
                    return timeSpan.Days + (timeSpan.Days == 1 ? " day" : " days");
                if (timeSpan.Hours > 0)
                    return timeSpan.Hours + (timeSpan.Hours == 1 ? " hour" : " hours");
                if (timeSpan.Minutes > 0)
                    return timeSpan.Minutes + (timeSpan.Minutes == 1 ? " minute" : " minutes");
                return timeSpan.Seconds + (timeSpan.Seconds == 1 ? " second" : " seconds");
            }
        }

        [JsonProperty("domain")]
        public string Domain => string.IsNullOrWhiteSpace(Url) ? string.Empty : new Uri(Url).Host;

    }
}