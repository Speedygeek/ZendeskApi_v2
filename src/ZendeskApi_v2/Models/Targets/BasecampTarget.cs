using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Targets
{
    public class BasecampTarget : BaseTarget
    {
        [JsonProperty("type")]
        public override string Type
        {
            get
            {
                return "basecamp_target";
            }
        }

        [JsonProperty("target_url")]
        public string TargetUrl { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("project_id")]
        public string ProjectId { get; set; }

        [JsonProperty("resource")]
        public string Resource { get; set; }

        [JsonProperty("message_id")]
        public string MessageId { get; set; }

        [JsonProperty("todo_list_id")]
        public string TodoListId { get; set; }
    }
}
