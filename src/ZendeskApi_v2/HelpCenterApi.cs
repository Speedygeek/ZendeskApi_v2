using ZendeskApi_v2.Requests.HelpCenter;
using static ZendeskApi_v2.Utils.ApiUtils;

namespace ZendeskApi_v2.HelpCenter
{
    public interface IHelpCenterApi
    {
        ICategories Categories { get; }
        ISections Sections { get; }
        IArticles Articles { get; }
        ITranslations Translations { get; }
        IVotes Votes { get; }
        IComments Comments { get; }
        IAccessPolicies AccessPolicies { get; }
        ITopics Topics { get; }
        string Locale { get; }
        string ZendeskUrl { get; }
    }

    public class HelpCenterApi : IHelpCenterApi
    {
        public ICategories Categories { get; set; }
        public ISections Sections { get; set; }
        public IArticles Articles { get; set; }
        public ITranslations Translations { get; set; }
        public IVotes Votes { get; set; }
        public IComments Comments { get; set; }
        public IAccessPolicies AccessPolicies { get; set; }
        public ITopics Topics { get; set; }
        public string Locale { get; set; }
        public string ZendeskUrl { get; set; }

        public HelpCenterApi(string yourZendeskUrl, string user, string password, string apiToken, string locale, string p_OAuthToken)
        {
            var formattedUrl = GetFormattedZendeskUrl(yourZendeskUrl).AbsoluteUri;

            Categories = new Categories(formattedUrl, user, password, apiToken, locale, p_OAuthToken);
            Sections = new Sections(formattedUrl, user, password, apiToken, p_OAuthToken);
            Articles = new Articles(formattedUrl, user, password, apiToken, p_OAuthToken);
            Translations = new Translations(formattedUrl, user, password, apiToken, p_OAuthToken);
            Votes = new Votes(formattedUrl, user, password, apiToken, p_OAuthToken);
            Comments = new Comments(formattedUrl, user, password, apiToken, p_OAuthToken);
            AccessPolicies = new AccessPolicies(formattedUrl, user, password, apiToken, p_OAuthToken);
            Topics = new Topics(formattedUrl, user, password, apiToken, p_OAuthToken);
            Locale = locale;

            ZendeskUrl = formattedUrl;
        }


        /// <summary>
        /// Constructor that takes 3 params.
        /// </summary>
        /// <param name="yourZendeskUrl">Will be formated to "https://yoursite.zendesk.com/api/v2"</param>
        /// <param name="user">Email adress of the user.</param>
        /// <param name="password">Password of the user.</param>
        public HelpCenterApi(string yourZendeskUrl,
            string user,
            string password) : this(yourZendeskUrl, user, password, null, "en-us", null)
        {
        }
    }
}
