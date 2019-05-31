using System;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.HelpCenter.Subscriptions
{
    /// <summary>
    /// only used when create a new article subscription
    /// </summary>
    public class ArticleSubscription : Subscription
    {
        [JsonConstructor]
        private ArticleSubscription() { }

        public ArticleSubscription(string locale)
        {
            SourceLocale = locale ?? throw new ArgumentNullException(nameof(locale));
        }

        /// <summary>
        /// only used when create a new article subscription
        /// </summary>
        [JsonProperty("source_locale")]
        public string SourceLocale { get; }
    }
}
