using System;
using ZendeskApi_v2.Requests; 
using System.Net;
using ZendeskApi_v2.HelpCenter;

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
		IViews Views { get; }
		IUsers Users { get; }
		IRequests Requests { get; }
		IGroups Groups { get; }
		ICustomAgentRoles CustomAgentRoles { get; }
		IOrganizations Organizations { get; }
		ISearch Search { get; }
		ITags Tags { get; }
		IForums Forums { get; }
		ICategories Categories { get; }
		ITopics Topics { get; }
		IAccountsAndActivity AccountsAndActivity { get; }
		IJobStatuses JobStatuses { get; }
		ILocales Locales { get; }
		IMacros Macros { get; }
		ISatisfactionRatings SatisfactionRatings { get; }
		ISharingAgreements SharingAgreements { get; }
		ITriggers Triggers { get; }
        IHelpCenterApi HelpCenter { get; }

		string ZendeskUrl { get; }
	}

	public class ZendeskApi : IZendeskApi
    {
		public ITickets Tickets { get; set; }
		public IAttachments Attachments { get; set; }
        public IViews Views { get; set; }
		public IUsers Users { get; set; }
        public IRequests Requests { get; set; }
        public IGroups Groups { get; set; }
		public ICustomAgentRoles CustomAgentRoles { get; set; }
		public IOrganizations Organizations { get; set; }
		public ISearch Search { get; set; }
		public ITags Tags { get; set; }
		public IForums Forums { get; set; }
		public ICategories Categories { get; set; }
		public ITopics Topics { get; set; }
		public IAccountsAndActivity AccountsAndActivity { get; set; }
		public IJobStatuses JobStatuses { get; set; }
		public ILocales Locales { get; set; }
		public IMacros Macros { get; set; }
		public ISatisfactionRatings SatisfactionRatings { get; set; }
		public ISharingAgreements SharingAgreements { get; set; }
		public ITriggers Triggers { get; set; }
        public IHelpCenterApi HelpCenter { get; set; }

        public string ZendeskUrl { get; set; }

        /// <summary>
        /// Constructor that takes 3 params.
        /// </summary>
        /// <param name="yourZendeskUrl">Will be formated to "https://yoursite.zendesk.com/api/v2"</param>
        /// <param name="user"></param>
        /// <param name="password">LEAVE BLANK IF USING TOKEN</param>
        /// <param name="apiToken">Optional Param which is used if specified instead of the password</param>
        /// <param name="locale">Optional param which designates the locale to use for Help Center requests. Defaults to "en-us" if no value is provided.</param>
        public ZendeskApi(string yourZendeskUrl, string user="", string password="", string apiToken="", string locale="en-us")
        {
            var formattedUrl = GetFormattedZendeskUrl(yourZendeskUrl).AbsoluteUri;
            
            Tickets = new Tickets(formattedUrl, user, password, apiToken);
            Attachments = new Attachments(formattedUrl, user, password, apiToken);
            Views = new Views(formattedUrl, user, password, apiToken);
            Users = new Users(formattedUrl, user, password, apiToken);
            Requests = new Requests.Requests(formattedUrl, user, password, apiToken);
            Groups = new Groups(formattedUrl, user, password, apiToken);
            CustomAgentRoles = new CustomAgentRoles(formattedUrl, user, password, apiToken);
            Organizations = new Organizations(formattedUrl, user, password, apiToken);
            Search = new Search(formattedUrl, user, password, apiToken);
            Tags = new Tags(formattedUrl, user, password, apiToken);
            Forums = new Forums(formattedUrl, user, password, apiToken);
            Categories = new Categories(formattedUrl, user, password, apiToken);
            Topics = new Topics(formattedUrl, user, password, apiToken);
            AccountsAndActivity = new AccountsAndActivity(formattedUrl, user, password, apiToken);
            JobStatuses = new JobStatuses(formattedUrl, user, password, apiToken);
            Locales = new Locales(formattedUrl, user, password, apiToken);
            Macros = new Macros(formattedUrl, user, password, apiToken);
            SatisfactionRatings = new SatisfactionRatings(formattedUrl, user, password, apiToken);
            SharingAgreements = new SharingAgreements(formattedUrl, user, password, apiToken);
            Triggers = new Triggers(formattedUrl, user, password, apiToken);
            HelpCenter = new HelpCenterApi(formattedUrl, user, password, apiToken, locale);

            ZendeskUrl = formattedUrl;
        }

#if SYNC
        /// <summary>
        /// Constructor that takes 3 params.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="yourZendeskUrl">Will be formated to "https://yoursite.zendesk.com/api/v2"</param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        public ZendeskApi(IWebProxy proxy, string yourZendeskUrl, string user, string password)
            : this(yourZendeskUrl, user, password)
        {
            if (proxy == null) return;

            ((Tickets)Tickets).Proxy = proxy;
			((Attachments)Attachments).Proxy = proxy;
			((Views)Views).Proxy = proxy;
			((Users)Users).Proxy = proxy;
			((Requests.Requests)Requests).Proxy = proxy;
			((Groups)Groups).Proxy = proxy;
			((CustomAgentRoles)CustomAgentRoles).Proxy = proxy;
			((Organizations)Organizations).Proxy = proxy;
			((Search)Search).Proxy = proxy;
			((Tags)Tags).Proxy = proxy;
			((Forums)Forums).Proxy = proxy;
			((ZendeskApi_v2.Requests.Categories)Categories).Proxy = proxy;
			((Topics)Topics).Proxy = proxy;
			((AccountsAndActivity)AccountsAndActivity).Proxy = proxy;
			((JobStatuses)JobStatuses).Proxy = proxy;
			((Locales)Locales).Proxy = proxy;
			((Macros)Macros).Proxy = proxy;
			((SatisfactionRatings)SatisfactionRatings).Proxy = proxy;
			((SharingAgreements)SharingAgreements).Proxy = proxy;
			((Triggers)Triggers).Proxy = proxy;
        }

#endif
        Uri GetFormattedZendeskUrl(string yourZendeskUrl)
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
                        
            return new Uri(yourZendeskUrl);
        }

#if Net35 
        public string GetLoginUrl(string name, string email, string authenticationToken, string returnToUrl = "")
        {
            string url = string.Format("{0}/access/remoteauth/", ZendeskUrl);

            string timestamp = GetUnixEpoch(DateTime.Now).ToString();


            string message = string.Format("{0}|{1}|||||{2}|{3}", name, email, authenticationToken, timestamp);
            //string message = name + email + token + timestamp;
            string hash = Md5(message);

            string result = url + "?name=" + HttpUtility.UrlEncode(name) +
                            "&email=" + HttpUtility.UrlEncode(email) +
                            "&timestamp=" + timestamp +
                            "&hash=" + hash;

            if (returnToUrl.Length > 0)
                result += "&return_to=" + returnToUrl;

            return result;
        }

        private double GetUnixEpoch(DateTime dateTime)
        {
            var unixTime = dateTime.ToUniversalTime() -
                           new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return unixTime.TotalSeconds;
        }


        public string Md5(string strChange)
        {
            //Change the syllable into UTF8 code
            byte[] pass = Encoding.UTF8.GetBytes(strChange);

            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(pass);
            string strPassword = ByteArrayToHexString(md5.Hash);
            return strPassword;
        }

        public string ByteArrayToHexString(byte[] Bytes)
        {
            // important bit, you have to change the byte array to hex string or zenddesk will reject
            StringBuilder Result;
            string HexAlphabet = "0123456789abcdef";

            Result = new StringBuilder();

            foreach (byte B in Bytes)
            {
                Result.Append(HexAlphabet[(int)(B >> 4)]);
                Result.Append(HexAlphabet[(int)(B & 0xF)]);
            }

            return Result.ToString();
        }
#endif

    }
}
