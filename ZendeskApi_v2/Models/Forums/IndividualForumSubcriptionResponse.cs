using Newtonsoft.Json;

namespace ZenDeskApi_v2.Models.Forums
{
    public class IndividualForumSubcriptionResponse
    {
        [JsonProperty("forum_subscription")]
        public ForumSubscription ForumSubscription { get; set; }
    }
}