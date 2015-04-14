using ZendeskApi_v2.Requests.HelpCenter;
namespace ZendeskApi_v2.HelpCenter
{
    public interface IHelpCenterApi
    {
        ICategories Categories { get; }
        ISections Sections { get; }
        IArticles Articles { get; }
        IVotes Votes { get; }
        IComments Comments { get; }

        string Locale { get; }
    }

    public class HelpCenterApi : IHelpCenterApi
    {
        public ICategories Categories { get; set; }
        public ISections Sections { get; set; }
        public IArticles Articles { get; set; }
        public IVotes Votes { get; set; }
        public IComments Comments { get; set; }

        public string Locale { get; set; }

        public HelpCenterApi(string yourZendeskUrl, string user, string password, string apiToken, string locale)
        {
            Categories = new Categories(yourZendeskUrl, user, password, apiToken, locale);
            Sections = new Sections(yourZendeskUrl, user, password, apiToken);
            Articles = new Articles(yourZendeskUrl, user, password, apiToken);
            Votes = new Votes(yourZendeskUrl, user, password, apiToken);
            Comments = new Comments(yourZendeskUrl, user, password, apiToken);

            Locale = locale;
        }
    }
}
