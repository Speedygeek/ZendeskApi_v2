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
        ///  For more information on when users can be side-loaded please see: <see cref="https://developer.zendesk.com/rest_api/docs/help_center/subscriptions"/> 
        /// </summary>
        [JsonProperty("users")]
        public IList<User> Users { get; set; }

        /// <summary>
        /// For more information on when sections can be side-loaded please see: <see cref="https://developer.zendesk.com/rest_api/docs/help_center/subscriptions"/> 
        /// </summary>
        /// <value></value>
        [JsonProperty("sections")]
        public IList<Section> Sections { get; set; }

        /// <summary>
        /// For more information on when articles can be side-loaded please see: <see cref="https://developer.zendesk.com/rest_api/docs/help_center/subscriptions"/> 
        /// </summary>
        /// <value></value>
        [JsonProperty("articles")]
        public IList<Article> Articles { get; set; }

        /// <summary>
        /// For more information on when translations can be side-loaded please see: <see cref="https://developer.zendesk.com/rest_api/docs/help_center/subscriptions"/> 
        /// </summary>
        [JsonProperty("translations")]
        public IList<Translation> Translations { get; set; }
    }
}
