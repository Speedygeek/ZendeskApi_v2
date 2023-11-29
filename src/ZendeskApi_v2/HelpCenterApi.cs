using ZendeskApi_v2.Requests.HelpCenter;

namespace ZendeskApi_v2.HelpCenter
{
    public interface IHelpCenterApi
    {
        ICategories Categories { get; }
        ISections Sections { get; }
        IArticles Articles { get; }
        IArticleAttachments ArticleAttachments { get; }
        ITranslations Translations { get; }
        IPosts Posts { get; }
        IVotes Votes { get; }
        IComments Comments { get; }
        IUserSegments UserSegments { get; }
        ITopics Topics { get; }
        string Locale { get; }
    }

    public class HelpCenterApi : IHelpCenterApi
    {
        public HelpCenterApi(
            string yourZendeskUrl,
            string user,
            string password,
            string apiToken,
            string locale,
            string p_OAuthToken,
            string customHeaderName,
            string customHeaderValue)
        {
            Categories = new Categories(yourZendeskUrl, user, password, apiToken, locale, p_OAuthToken, customHeaderName, customHeaderValue);
            Sections = new Sections(yourZendeskUrl, user, password, apiToken, locale, p_OAuthToken, customHeaderName, customHeaderValue);
            Articles = new Articles(yourZendeskUrl, user, password, apiToken, locale, p_OAuthToken, customHeaderName, customHeaderValue);
            Translations = new Translations(yourZendeskUrl, user, password, apiToken, p_OAuthToken, customHeaderName, customHeaderValue);
            Votes = new Votes(yourZendeskUrl, user, password, apiToken, p_OAuthToken, customHeaderName, customHeaderValue);
            Comments = new Comments(yourZendeskUrl, user, password, apiToken, p_OAuthToken, customHeaderName, customHeaderValue);
            UserSegments = new UserSegments(yourZendeskUrl, user, password, apiToken, p_OAuthToken, customHeaderName, customHeaderValue);
            Topics = new Topics(yourZendeskUrl, user, password, apiToken, p_OAuthToken, customHeaderName, customHeaderValue);
            Posts = new Posts(yourZendeskUrl, user, password, apiToken, p_OAuthToken, customHeaderName, customHeaderValue);
            Locale = locale;
            ArticleAttachments = new ArticleAttachments(yourZendeskUrl, user, password, apiToken, locale, p_OAuthToken, customHeaderName, customHeaderValue);
        }

        public ICategories Categories { get; }
        public ISections Sections { get; }
        public IArticles Articles { get; }
        public ITranslations Translations { get; }
        public IVotes Votes { get; }
        public IComments Comments { get; }
        public IUserSegments UserSegments { get; }
        public ITopics Topics { get; }
        public IPosts Posts { get; }
        public string Locale { get; }
        public IArticleAttachments ArticleAttachments { get; }
    }
}
