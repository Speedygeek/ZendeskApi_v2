using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using RestSharp;
using RestSharp.Contrib;
using ZenDeskApi_v2.Requests;

namespace ZenDeskApi_v2
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

        public ZendeskApi(string yourZenDeskUrl, string user, string password)
        {
            Tickets = new Tickets(yourZenDeskUrl, user, password);
            Attachments = new Attachments(yourZenDeskUrl, user, password);
            Views = new Views(yourZenDeskUrl, user, password);
            Users = new Users(yourZenDeskUrl, user, password);
            Requests = new Requests.Requests(yourZenDeskUrl, user, password);
            Groups = new Groups(yourZenDeskUrl, user, password);
            CustomAgentRoles = new CustomAgentRoles(yourZenDeskUrl, user, password);
            Organizations = new Organizations(yourZenDeskUrl, user, password);
            Search = new Search(yourZenDeskUrl, user, password);
            Tags = new Tags(yourZenDeskUrl, user, password);
            Forums = new Forums(yourZenDeskUrl, user, password);
            Categories = new Categories(yourZenDeskUrl, user, password);
            Topics = new Topics(yourZenDeskUrl, user, password);
            AccountsAndActivity = new AccountsAndActivity(yourZenDeskUrl, user, password);
            JobStatuses = new JobStatuses(yourZenDeskUrl, user, password);
            Locales = new Locales(yourZenDeskUrl, user, password);
        }
    }
}
