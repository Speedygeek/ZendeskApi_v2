using System;

namespace ZendeskApi_v2.Utils
{
    public static class ApiUtils
    {
        public static Uri GetFormattedZendeskUrl(string yourZendeskUrl)
        {
            yourZendeskUrl = yourZendeskUrl.ToLower();

            //Make sure the Authority is https://
            if (yourZendeskUrl.StartsWith("http://"))
                yourZendeskUrl = yourZendeskUrl.Replace("http://", "https://");

            if (!yourZendeskUrl.StartsWith("https://"))
                yourZendeskUrl = "https://" + yourZendeskUrl;

            if (!yourZendeskUrl.EndsWith("/api/v2"))
            {
                //ensure that url ends with ".zendesk.com/api/v2"
                yourZendeskUrl = yourZendeskUrl.Split(new[] { ".zendesk.com" }, StringSplitOptions.RemoveEmptyEntries)[0] + ".zendesk.com/api/v2";
            }

            if (!yourZendeskUrl.EndsWith("/", StringComparison.CurrentCultureIgnoreCase))
                yourZendeskUrl += "/";
            return new Uri(yourZendeskUrl);
        }
    }
}
