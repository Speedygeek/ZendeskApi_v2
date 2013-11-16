using System;
using System.Text;
using ZendeskApi_v2.Requests; 

#if Net35 
using System.Web;
using System.Security.Cryptography;
#endif

namespace ZendeskApi_v2
{
    public class ZendeskApi
    {
        public Tickets Tickets { get; set; }
        public Attachments Attachments { get; set; }
        public Views Views { get; set; }
        public Users Users { get; set; }
        public Requests.Requests Requests { get; set; }
        public Groups Groups { get; set; }
        public CustomAgentRoles CustomAgentRoles { get; set; }
        public Organizations Organizations { get; set; }
        public Search Search { get; set; }
        public Tags Tags { get; set; }
        public Forums Forums { get; set; }
        public Categories Categories { get; set; }
        public Topics Topics { get; set; }
        public AccountsAndActivity AccountsAndActivity { get; set; }
        public JobStatuses JobStatuses { get; set; }
        public Locales Locales { get; set; }
        public Macros Macros { get; set; }
        public SatisfactionRatings SatisfactionRatings { get; set; }
        public SharingAgreements SharingAgreements { get; set; }
        public Triggers Triggers { get; set; }

        public string ZendeskUrl { get; set; }        

        /// <summary>
        /// Constructor that takes 3 params.
        /// </summary>
        /// <param name="yourZendeskUrl">Will be formated to "https://yoursite.zendesk.com/api/v2"</param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        public ZendeskApi(string yourZendeskUrl, string user, string password)
        {
            var formattedUrl = GetFormattedZendeskUrl(yourZendeskUrl).AbsoluteUri;

            Tickets = new Tickets(formattedUrl, user, password);
            Attachments = new Attachments(formattedUrl, user, password);
            Views = new Views(formattedUrl, user, password);
            Users = new Users(formattedUrl, user, password);
            Requests = new Requests.Requests(formattedUrl, user, password);
            Groups = new Groups(formattedUrl, user, password);
            CustomAgentRoles = new CustomAgentRoles(formattedUrl, user, password);
            Organizations = new Organizations(formattedUrl, user, password);
            Search = new Search(formattedUrl, user, password);
            Tags = new Tags(formattedUrl, user, password);
            Forums = new Forums(formattedUrl, user, password);
            Categories = new Categories(formattedUrl, user, password);
            Topics = new Topics(formattedUrl, user, password);
            AccountsAndActivity = new AccountsAndActivity(formattedUrl, user, password);
            JobStatuses = new JobStatuses(formattedUrl, user, password);
            Locales = new Locales(formattedUrl, user, password);
            Macros = new Macros(formattedUrl, user, password);
            SatisfactionRatings = new SatisfactionRatings(formattedUrl, user, password);
            SharingAgreements = new SharingAgreements(formattedUrl, user, password);
            Triggers = new Triggers(formattedUrl, user, password);

            ZendeskUrl = formattedUrl;
        }

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
