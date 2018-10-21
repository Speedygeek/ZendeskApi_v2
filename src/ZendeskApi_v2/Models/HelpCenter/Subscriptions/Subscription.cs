using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace ZendeskApi_v2.Models.HelpCenter.Subscriptions
{
    public class Subscription  : HelpCenterBase
    {
        /// <summary>
        /// The id of the user who has this subscription
        /// </summary>
        [JsonProperty("user_id")]
        public long UserId { get; set; }

        /// <summary>
        /// The id of the subscribed item
        /// </summary>
        [JsonProperty("content_id")]
        public long ContentId { get; set; }

        /// <summary>
        /// The type of the subscribed item
        /// </summary>
        [JsonProperty("content_type")] 
        public string ContentType { get; set; } // TODO: Make Enum

        /// <summary>
        /// The locale of the subscribed item
        /// </summary>
        [JsonProperty("locale")]
        public string Locale { get; set; }     

        /// <summary>
        /// Subscribe also to article comments. 
        /// Only for section subscriptions
        /// </summary>
        [JsonProperty("include_comments")]
        public bool include_comments { get; set; }


    }
}