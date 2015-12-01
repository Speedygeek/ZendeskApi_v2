using NUnit.Framework;
using Tests.Properties;
using ZendeskApi_v2;
using ZendeskApi_v2.Models.Tickets;
using ZendeskApi_v2.Models.Users;

namespace Tests {
	[TestFixture]
    [Category("Search")]
    public class SearchTests
    {
        private ZendeskApi api = new ZendeskApi(Settings.Default.Site, Settings.Default.Email, Settings.Default.Password);
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
                            Id = Settings.Default.CustomFieldTypeOfFeedbackId,
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
                            Id = Settings.Default.CustomFieldTypeOfFeedbackId,
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
            var res = api.Search.SearchFor(Settings.Default.Email);
            Assert.AreEqual(res.Results[0].ResultType, "user");
            Assert.Greater(res.Results[0].Id, 0);
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

        [Test]
        public void TicketSearchByTicketAnonymousType()
        {
            var res = api.Search.SearchFor<Ticket>("my printer is on fire");

            Assert.IsTrue(res != null);
            Assert.Greater(res.Results.Count, 10);
            Assert.IsTrue(!string.IsNullOrEmpty(res.Results[0].Subject));

            var noRes = api.Search.SearchFor<User>("my printer is on fire");

            Assert.IsTrue(noRes != null);
            Assert.IsTrue(noRes.Results.Count == 0);

            res = api.Search.SearchFor<Ticket>("my printer is on fire", perPage: 10);
            Assert.IsTrue(res != null);
            Assert.AreEqual(res.Results.Count, 10);
            Assert.AreEqual(res.Page, 1);
            Assert.IsTrue(res.Results[0] is Ticket);

        }

        [Test]
        public async void TicketSearchByTicketAnonymousTypeAsync()
        {
            var res = await api.Search.SearchForAsync<Ticket>("my printer is on fire");

            Assert.IsTrue(res != null);
            Assert.Greater(res.Results.Count, 10);
            Assert.IsTrue(!string.IsNullOrEmpty(res.Results[0].Subject));

            var noRes = await api.Search.SearchForAsync<User>("my printer is on fire");

            Assert.IsTrue(noRes != null);
            Assert.IsTrue(noRes.Results.Count == 0);

            res = await api.Search.SearchForAsync<Ticket>("my printer is on fire", perPage: 10);
            Assert.IsTrue(res != null);
            Assert.AreEqual(res.Results.Count, 10);
            Assert.AreEqual(res.Page, 1);
            Assert.IsTrue(res.Results[0] is Ticket);

        }

        [Test]
        public void UserSearchByUserAnonymousType()
        {
            var res = api.Search.SearchFor<User>(Settings.Default.Email);

            Assert.IsTrue(res != null);
            Assert.AreEqual(res.Results.Count, 1);
            Assert.AreEqual(res.Results[0].Id, Settings.Default.UserId);
            Assert.IsTrue(res.Results[0] is User);
        }

        [Test]
        public async void UserSearchByUserAnonymousTypeAsync()
        {
            var res = await api.Search.SearchForAsync<User>(Settings.Default.Email);

            Assert.IsTrue(res != null);
            Assert.AreEqual(res.Results.Count, 1);
            Assert.AreEqual(res.Results[0].Id, Settings.Default.UserId);
            Assert.IsTrue(res.Results[0] is User);
        }
    }
}