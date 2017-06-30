using Newtonsoft.Json;
using ZendeskApi_v2.Models.Shared;

namespace ZendeskApi_v2.Models.HelpCenter.Attachments
{
    public class ArticleAttachment
    {
        [JsonProperty("article_attachment")]
        public Attachment Attachment { get; set; }
    }
}
