using System;
using System.Text;
using ZendeskApi_v2.Requests;

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
    }
}
