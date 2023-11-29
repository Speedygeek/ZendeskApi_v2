using System;
using ZendeskApi_v2.Requests;
using System.Net;
using ZendeskApi_v2.HelpCenter;
using System.Collections.Generic;

#if Net35
using System.Web;
using System.Security.Cryptography;
#endif

namespace ZendeskApi_v2
{
    public interface IZendeskApi
    {
        ITickets Tickets { get; }
        IAttachments Attachments { get; }
        IBrands Brands { get; }
        IViews Views { get; }
        IUsers Users { get; }
        IRequests Requests { get; }
        IGroups Groups { get; }
        ICustomAgentRoles CustomAgentRoles { get; }
        IOrganizations Organizations { get; }
        ISearch Search { get; }
        ITags Tags { get; }
        IAccountsAndActivity AccountsAndActivity { get; }
        IJobStatuses JobStatuses { get; }
        ILocales Locales { get; }
        IMacros Macros { get; }
        ISatisfactionRatings SatisfactionRatings { get; }
        ISharingAgreements SharingAgreements { get; }
        ITriggers Triggers { get; }
        IHelpCenterApi HelpCenter { get; }
        IVoice Voice { get; }
        ISchedules Schedules { get; }
        ITargets Targets { get; }
        IAutomations Automations { get; }

        string ZendeskUrl { get; }
    }

    public class ZendeskApi : IZendeskApi
    {
        public ITickets Tickets { get; set; }
        public IAttachments Attachments { get; set; }
        public IBrands Brands { get; set; }
        public IViews Views { get; set; }
        public IUsers Users { get; set; }
        public IRequests Requests { get; set; }
        public IGroups Groups { get; set; }
        public ICustomAgentRoles CustomAgentRoles { get; set; }
        public IOrganizations Organizations { get; set; }
        public ISearch Search { get; set; }
        public ITags Tags { get; set; }
        public IAccountsAndActivity AccountsAndActivity { get; set; }
        public IJobStatuses JobStatuses { get; set; }
        public ILocales Locales { get; set; }
        public IMacros Macros { get; set; }
        public ISatisfactionRatings SatisfactionRatings { get; set; }
        public ISharingAgreements SharingAgreements { get; set; }
        public ITriggers Triggers { get; set; }
        public IHelpCenterApi HelpCenter { get; set; }
        public IVoice Voice { get; set; }
        public ISchedules Schedules { get; set; }
        public ITargets Targets { get; set; }
        public IAutomations Automations { get; set; }

        public string ZendeskUrl { get; set; }

        /// <summary>
        /// Constructor that takes 2 params.
        /// </summary>
        /// <param name="yourZendeskUrl">Will be formatted to "https://yoursite.zendesk.com/api/v2"</param>
        /// <param name="oauthToken">Access token</param>
        public ZendeskApi(string yourZendeskUrl,
            string p_OauthToken) : this(yourZendeskUrl, null, null, null, "en-us", p_OauthToken)
        {
        }

        /// <summary>
        /// Constructor that takes 3 params.
        /// </summary>
        /// <param name="yourZendeskUrl">Will be formatted to "https://yoursite.zendesk.com/api/v2"</param>
        /// <param name="user">Email address of the user.</param>
        /// <param name="password">Password of the user.</param>
        public ZendeskApi(string yourZendeskUrl,
            string user,
            string password) : this(yourZendeskUrl, user, password, null, "en-us", null)
        {
        }

        /// <summary>
        /// Constructor that takes 4 params.
        /// </summary>
        /// <param name="yourZendeskUrl">Will be formatted to "https://yoursite.zendesk.com/api/v2"</param>
        /// <param name="user">Email address of the user</param>
        /// <param name="password">LEAVE BLANK IF USING TOKEN</param>
        /// <param name="apiToken">Used if specified instead of the password</param>
        /// <param name="locale">Locale to use for Help Center requests. Defaults to "en-us" if no value is provided.</param>

        public ZendeskApi(string yourZendeskUrl,
            string user,
            string apiToken,
            string locale) : this(yourZendeskUrl, user, "", apiToken, locale, null)
        {
        }

        /// <summary>
        /// Constructor that takes 6 params.
        /// </summary>
        /// <param name="yourZendeskUrl">Will be formatted to "https://yoursite.zendesk.com/api/v2"</param>
        /// <param name="user">Email address of the user</param>
        /// <param name="password">LEAVE BLANK IF USING TOKEN</param>
        /// <param name="apiToken">Used if specified instead of the password</param>
        /// <param name="locale">Locale to use for Help Center requests. Defaults to "en-us" if no value is provided.</param>
        public ZendeskApi(string yourZendeskUrl,
                          string user,
                          string password,
                          string apiToken,
                          string locale,
                          string p_OAuthToken) : this(yourZendeskUrl,user,password, apiToken,locale, p_OAuthToken, null)
        {
        }

        /// <summary>
        ///  Constructor that takes 7 params.
        /// </summary>
        /// <param name="yourZendeskUrl">Will be formatted to "https://yoursite.zendesk.com/api/v2"</param>
        /// <param name="user">Email address of the user</param>
        /// <param name="password">LEAVE BLANK IF USING TOKEN</param>
        /// <param name="apiToken">Used if specified instead of the password</param>
        /// <param name="locale">Locale to use for Help Center requests. Defaults to "en-us" if no value is provided.</param>
        /// <param name="p_OAuthToken">Authentication token</param>
        /// <param name="customHeaderName">The name of the custom header</param>
        /// <param name="customHeaderValue">The value to store in the header</param>
        public ZendeskApi(string yourZendeskUrl,
                          string user,
                          string password,
                          string apiToken,
                          string locale,
                          string p_OAuthToken,
                          Dictionary<string, string> customHeaders)
        {
            var formattedUrl = GetFormattedZendeskUrl(yourZendeskUrl).AbsoluteUri;

            Tickets = new Tickets(formattedUrl, user, password, apiToken, p_OAuthToken, customHeaders);
            Attachments = new Attachments(formattedUrl, user, password, apiToken, p_OAuthToken, customHeaders);
            Brands = new Brands(formattedUrl, user, password, apiToken, p_OAuthToken, customHeaders);
            Views = new Views(formattedUrl, user, password, apiToken, p_OAuthToken, customHeaders);
            Users = new Users(formattedUrl, user, password, apiToken, p_OAuthToken, customHeaders);
            Requests = new Requests.Requests(formattedUrl, user, password, apiToken, p_OAuthToken, customHeaders);
            Groups = new Groups(formattedUrl, user, password, apiToken, p_OAuthToken, customHeaders);
            CustomAgentRoles = new CustomAgentRoles(formattedUrl, user, password, apiToken, p_OAuthToken, customHeaders);
            Organizations = new Organizations(formattedUrl, user, password, apiToken, p_OAuthToken, customHeaders);
            Search = new Search(formattedUrl, user, password, apiToken, p_OAuthToken, customHeaders);
            Tags = new Tags(formattedUrl, user, password, apiToken, p_OAuthToken, customHeaders);
            AccountsAndActivity = new AccountsAndActivity(formattedUrl, user, password, apiToken, p_OAuthToken, customHeaders);
            JobStatuses = new JobStatuses(formattedUrl, user, password, apiToken, p_OAuthToken, customHeaders);
            Locales = new Locales(formattedUrl, user, password, apiToken, p_OAuthToken, customHeaders);
            Macros = new Macros(formattedUrl, user, password, apiToken, p_OAuthToken, customHeaders);
            SatisfactionRatings = new SatisfactionRatings(formattedUrl, user, password, apiToken, p_OAuthToken, customHeaders);
            SharingAgreements = new SharingAgreements(formattedUrl, user, password, apiToken, p_OAuthToken, customHeaders);
            Triggers = new Triggers(formattedUrl, user, password, apiToken, p_OAuthToken, customHeaders);
            HelpCenter = new HelpCenterApi(formattedUrl, user, password, apiToken, locale, p_OAuthToken, customHeaders);
            Voice = new Voice(formattedUrl, user, password, apiToken, p_OAuthToken, customHeaders);
            Schedules = new Schedules(formattedUrl, user, password, apiToken, p_OAuthToken, customHeaders);
            Targets = new Targets(formattedUrl, user, password, apiToken, p_OAuthToken, customHeaders);
            Automations = new Automations(formattedUrl, user, password, apiToken, p_OAuthToken, customHeaders);

            ZendeskUrl = formattedUrl;
        }
#if SYNC

        /// <summary>
        /// Constructor that takes 3 params.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="yourZendeskUrl">Will be formatted to "https://yoursite.zendesk.com/api/v2"</param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        public ZendeskApi(IWebProxy proxy, string yourZendeskUrl, string user, string password)
            : this(yourZendeskUrl, user, password, null, "en-us", null)
        {
            if (proxy == null) { return; }

            ((Tickets)Tickets).Proxy = proxy;
            ((Attachments)Attachments).Proxy = proxy;
            ((Brands)Brands).Proxy = proxy;
            ((Views)Views).Proxy = proxy;
            ((Users)Users).Proxy = proxy;
            ((Requests.Requests)Requests).Proxy = proxy;
            ((Groups)Groups).Proxy = proxy;
            ((CustomAgentRoles)CustomAgentRoles).Proxy = proxy;
            ((Organizations)Organizations).Proxy = proxy;
            ((Search)Search).Proxy = proxy;
            ((Tags)Tags).Proxy = proxy;
            ((AccountsAndActivity)AccountsAndActivity).Proxy = proxy;
            ((JobStatuses)JobStatuses).Proxy = proxy;
            ((Locales)Locales).Proxy = proxy;
            ((Macros)Macros).Proxy = proxy;
            ((SatisfactionRatings)SatisfactionRatings).Proxy = proxy;
            ((SharingAgreements)SharingAgreements).Proxy = proxy;
            ((Triggers)Triggers).Proxy = proxy;
            ((Voice)Voice).Proxy = proxy;
            ((Schedules)Schedules).Proxy = proxy;
            ((Targets)Targets).Proxy = proxy;
            ((Automations)Automations).Proxy = proxy;
        }
#endif
        private Uri GetFormattedZendeskUrl(string yourZendeskUrl)
        {
            yourZendeskUrl = yourZendeskUrl.ToLower();

            //Make sure the Authority is https://
            if (yourZendeskUrl.StartsWith("http://"))
            {
                yourZendeskUrl = yourZendeskUrl.Replace("http://", "https://");
            }

            if (!yourZendeskUrl.StartsWith("https://"))
            {
                yourZendeskUrl = "https://" + yourZendeskUrl;
            }

            if (!yourZendeskUrl.EndsWith("/api/v2"))
            {
                //ensure that url ends with ".zendesk.com/api/v2"
                yourZendeskUrl = yourZendeskUrl.Split(new[] { ".zendesk.com" }, StringSplitOptions.RemoveEmptyEntries)[0] + ".zendesk.com/api/v2";
            }

            if (!yourZendeskUrl.EndsWith("/", StringComparison.CurrentCultureIgnoreCase))
            {
                yourZendeskUrl += "/";
            }
            return new Uri(yourZendeskUrl);
        }
    }
}
