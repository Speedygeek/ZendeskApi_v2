using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using ZendeskApi_v2;
using ZendeskApi_v2.Models.Constants;
using ZendeskApi_v2.Models.Tickets;
using ZendeskApi_v2.Models.Users;

namespace Tests
{
    [TestFixture]
    [Category("Search")]
    public class SearchTests
    {
        private readonly ZendeskApi api = new ZendeskApi(Settings.Site, Settings.AdminEmail, Settings.AdminPassword);

        [Test]
        public void CanSearch()
        {
            var res = api.Search.SearchFor(Settings.AdminEmail);
            Assert.AreEqual(res.Results[0].ResultType, "user");
            Assert.Greater(res.Results[0].Id, 0);
        }

        [Test]
        public void BackwardCompatibilitAfterAddingPagination()
        {
            var res = api.Search.SearchFor("Effective", "updated_at", "asc");
            Assert.IsTrue(res.Count > 0);
        }

        [Test]
        public void TotalNumberOftickesShouldbeSameWhenReterivingNextPage()
        {
            var res = api.Search.SearchFor("Effective"); //search for a custom field - the results are more than one page
            var total = res.Count;

            Assert.IsTrue(res.Count > 0);
            Assert.IsTrue(res.Count > res.Results.Count); //result has more than one page
            Assert.IsTrue(!string.IsNullOrEmpty(res.NextPage)); //It has next page

            res = api.Search.SearchFor("Effective", page: 2); //fetch next page
            Assert.IsTrue(res.Count > 0);
            Assert.IsTrue(res.Count == total); //number of results should be same as page 1

        }

        [Test]
        public void TicketHasSubject()
        {
            var res = api.Search.SearchFor("my printer is on fire");

            Assert.IsTrue(res != null);
            Assert.IsTrue(res.Results.Count > 0);
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
        public async Task TicketSearchByTicketAnonymousTypeAsync()
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
            var res = api.Search.SearchFor<User>(Settings.AdminEmail);

            Assert.IsTrue(res != null);
            Assert.AreEqual(res.Results.Count, 1);
            Assert.AreEqual(res.Results[0].Id, Settings.UserId);
            Assert.IsTrue(res.Results[0] is User);
        }

        [Test]
        public async Task UserSearchByUserAnonymousTypeAsync()
        {
            var res = await api.Search.SearchForAsync<User>(Settings.AdminEmail);

            Assert.IsTrue(res != null);
            Assert.AreEqual(res.Results.Count, 1);
            Assert.AreEqual(res.Results[0].Id, Settings.UserId);
            Assert.IsTrue(res.Results[0] is User);
        }

        [Test]
        public void SearchSortIsWorking()
        {
            //desc asc 
            var res = api.Search.SearchFor<Ticket>("Effective", "created_at", "asc");
            Assert.IsTrue(res.Count > 2);
            var first = res.Results[0];
            var second = res.Results[1];
            Assert.That(second.CreatedAt, Is.GreaterThan(first.CreatedAt));

        }
    }
}