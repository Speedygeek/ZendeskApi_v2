using ZendeskApi_v2.Requests.HelpCenter;

namespace ZendeskApi_v2.HelpCenter
{
    public interface IHelpCenterApi
    {
        ICategories Categories { get; }
        ISections Sections { get; }
        IArticles Articles { get; }
        ITranslations Translations { get; }
        IPosts Posts { get; }
        IVotes Votes { get; }
        IComments Comments { get; }
        IAccessPolicies AccessPolicies { get; }
        ITopics Topics { get; }
        string Locale { get; }
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
        public IPosts Posts { get; set; }
        public string Locale { get; set; }

        public HelpCenterApi(string yourZendeskUrl, string user, string password, string apiToken, string locale, string p_OAuthToken)
        {
            Categories = new Categories(yourZendeskUrl, user, password, apiToken, locale, p_OAuthToken);
            Sections = new Sections(yourZendeskUrl, user, password, apiToken, p_OAuthToken);
            Articles = new Articles(yourZendeskUrl, user, password, apiToken, p_OAuthToken);
            Translations = new Translations(yourZendeskUrl, user, password, apiToken, p_OAuthToken);
            Votes = new Votes(yourZendeskUrl, user, password, apiToken, p_OAuthToken);
            Comments = new Comments(yourZendeskUrl, user, password, apiToken, p_OAuthToken);
            AccessPolicies = new AccessPolicies(yourZendeskUrl, user, password, apiToken, p_OAuthToken);
            Topics = new Topics(yourZendeskUrl, user, password, apiToken, p_OAuthToken);
            Posts = new Posts(yourZendeskUrl, user, password, apiToken, p_OAuthToken);
            Locale = locale;
        }
    }
}