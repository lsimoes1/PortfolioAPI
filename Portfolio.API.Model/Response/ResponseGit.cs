using Newtonsoft.Json;
using System;

namespace Portfolio.API.Model.Response
{
    public class ResponseGit
    {
        [JsonProperty("name")]
        public string NameProject { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("updated_at")]
        public DateTime LastUpdate { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("homepage")]
        public string HomePage { get; set; }

        [JsonProperty("html_url")]
        public string UrlRepository { get; set; }
    }
}
