using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZendeskApi_v2.Models.HelpCenter.Subscriptions;

namespace ZendeskApi_v2.Requests.HelpCenter
{
    public static class Subscription
    {
        public static string SubscriptionSideloadUri(this string resourceUrl, SubscriptionSideLoadOptions sideloadOptions)
        {
            if (sideloadOptions != SubscriptionSideLoadOptions.None)
            {
                resourceUrl = $"{resourceUrl}?include={sideloadOptions.ToString().ToLower().Replace(" ", "")}";
            }
            return resourceUrl;
        }
    }
}
