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

        public ZendeskApi(string yourZendeskUrl, string user, string password)
        {
            Tickets = new Tickets(yourZendeskUrl, user, password);
            Attachments = new Attachments(yourZendeskUrl, user, password);
            Views = new Views(yourZendeskUrl, user, password);
            Users = new Users(yourZendeskUrl, user, password);
            Requests = new Requests.Requests(yourZendeskUrl, user, password);
            Groups = new Groups(yourZendeskUrl, user, password);
            CustomAgentRoles = new CustomAgentRoles(yourZendeskUrl, user, password);
            Organizations = new Organizations(yourZendeskUrl, user, password);
            Search = new Search(yourZendeskUrl, user, password);
            Tags = new Tags(yourZendeskUrl, user, password);
            Forums = new Forums(yourZendeskUrl, user, password);
            Categories = new Categories(yourZendeskUrl, user, password);
            Topics = new Topics(yourZendeskUrl, user, password);
            AccountsAndActivity = new AccountsAndActivity(yourZendeskUrl, user, password);
            JobStatuses = new JobStatuses(yourZendeskUrl, user, password);
            Locales = new Locales(yourZendeskUrl, user, password);
            Macros = new Macros(yourZendeskUrl, user, password);
            SatisfactionRatings = new SatisfactionRatings(yourZendeskUrl, user, password);
            SharingAgreements = new SharingAgreements(yourZendeskUrl, user, password);
            Triggers = new Triggers(yourZendeskUrl, user, password);

            ZendeskUrl = yourZendeskUrl;
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
