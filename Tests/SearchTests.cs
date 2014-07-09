using System;
using System.Collections.Generic;
using NUnit.Framework;
using ZendeskApi_v2;
using ZendeskApi_v2.Models.Constants;
using ZendeskApi_v2.Models.Tickets;

namespace Tests
{
    [TestFixture]
    public class SearchTests
    {
        private ZendeskApi api = new ZendeskApi(Settings.Site, Settings.Email, Settings.Password);
         /*     
        [TestFixtureSetUp]
        public void Init()
        {
            const int range = 3; 
            var r = new Random();
            var number = 0;
            var typesOfFeedback = new[] { "Effective", "Descriptive", "Evaluative ", "Motivational" };
            for (var i = 0; i < 100; i++)
            {
                var ticket = new Ticket
                {
                    Subject = string.Format("my printer is on fire - {0}", i),
                    Comment = new Comment()
                    {
                        Body = "HELP"
                    },
                    Priority = TicketPriorities.Urgent,
                    CustomFields = new List<CustomField>()
                    {
                        new CustomField()
                        {
                            Id = Settings.CustomFieldTypeOfFeedbackId,
                            Value = typesOfFeedback[number]
                        },
                    },
                };

                var res = api.Tickets.CreateTicket(ticket).Ticket;

            }

            for (int i = 0; i < 150; i++)
            {
                number = r.Next(range);

                var ticket = new Ticket
                {
                    Subject = string.Format("my printer is on fire - {0}", i),
                    Comment = new Comment()
                    {
                        Body = "HELP"
                    },
                    Priority = TicketPriorities.Urgent,
                    CustomFields = new List<CustomField>()
                    {
                        new CustomField()
                        {
                            Id = Settings.CustomFieldTypeOfFeedbackId,
                            Value = typesOfFeedback[number]
                        },
                    },
                };

                var res = api.Tickets.CreateTicket(ticket).Ticket;

            }
        }
         */

        [Test]
        public void CanSearch()
        {
            var res = api.Search.SearchFor(Settings.Email);
            Assert.AreEqual(res.Results[0].ResultType, "user");
        }

        [Test]
        public void BackwardCompatibilitAfterAddingPagination()
        {
            var res = api.Search.SearchFor("Effective", "created_at","asc");
            Assert.IsTrue(res.Count > 0);
        }
        [Test]
        public void TotalNumberOftickesShouldbeSameWhenReterivingNextPage()
        {
            var res = api.Search.SearchFor("Effective"); //search for a custom field - the results are more than one page
            var total = res.Count;

            Assert.IsTrue(res.Count >0); 
            Assert.IsTrue(res.Count > res.Results.Count); //result has more than one page
            Assert.IsTrue(!string.IsNullOrEmpty(res.NextPage)); //It has next page

            res = api.Search.SearchFor("Effective","","",2); //fetch next page
            Assert.IsTrue(res.Count > 0);
            Assert.IsTrue(res.Count==total); //number of results should be same as page 1
            
        }
        [Test]
        public void TicketHasSubject()
        {
            var res = api.Search.SearchFor("my printer is on fire");

            Assert.IsTrue(res!=null);
            Assert.IsTrue(res.Results.Count>0);
            Assert.IsTrue(!string.IsNullOrEmpty(res.Results[0].Subject));
        }
    }
}