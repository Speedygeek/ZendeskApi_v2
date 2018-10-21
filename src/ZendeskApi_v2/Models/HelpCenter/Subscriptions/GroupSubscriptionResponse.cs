using Newtonsoft.Json;
using System.Collections.Generic;
using ZendeskApi_v2.Models.Users;
using ZendeskApi_v2.Models.Sections;
using ZendeskApi_v2.Models.Articles;
using ZendeskApi_v2.Models.HelpCenter.Translations;

namespace ZendeskApi_v2.Models.HelpCenter.Subscriptions
{
    public class GroupSubscriptionsResponse : GroupResponseBase
    {
        [JsonProperty("subscriptions")]
        public IList<Subscription> Subscriptions { get; set; }

        /// <summary>
        /// TODO: List When Loaded 
        /// </summary>
        [JsonProperty("users")]
        public IList<User> Users { get; set; }

        /// <summary>
        /// TODO: List when loaded
        /// </summary>
        /// <value></value>
        [JsonProperty("sections")]
        public IList<Section> Sections { get; set; }

        /// <summary>
        /// TODO: list when loaded
        /// </summary>
        /// <value></value>
        [JsonProperty("articles")]
        public IList<Article> Articles { get; set; }

        /// <summary>
        /// TODO: List when loaded. 
        /// </summary>
        [JsonProperty("translations")]
        public IList<Translation> Translations { get; set; }
    }
}
