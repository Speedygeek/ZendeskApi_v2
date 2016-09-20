using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Articles
{
    public class IndividualArticleResponse
    {

        [JsonProperty("article")]
        public Article Article { get; set; }
    }
}