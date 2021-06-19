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
            Assert.That(res.Results[0].ResultType, Is.EqualTo("user"));
            Assert.That(res.Results[0].Id, Is.GreaterThan(0));
        }

        [Test]
        public void BackwardCompatibilitAfterAddingPagination()
        {
            var res = api.Search.SearchFor("Effective", "updated_at", "asc");
            Assert.That(res.Count, Is.GreaterThan(0));
        }

        [Test]
        public void TotalNumberOftickesShouldbeSameWhenReterivingNextPage()
        {
            var res = api.Search.SearchFor("Effective"); //search for a custom field - the results are more than one page
            var total = res.Count;

            Assert.That(res.Count, Is.GreaterThan(0));
            Assert.That(res.Count, Is.GreaterThan(res.Results.Count)); //result has more than one page
            Assert.That(!string.IsNullOrEmpty(res.NextPage), Is.True); //It has next page

            res = api.Search.SearchFor("Effective", page: 2); //fetch next page
            Assert.That(res.Count, Is.GreaterThan(0));
            Assert.That(res.Count, Is.EqualTo(total)); //number of results should be same as page 1

        }

        [Test]
        public void TicketHasSubject()
        {
            var res = api.Search.SearchFor("my printer is on fire");

            Assert.That(res, Is.Not.EqualTo(null));
            Assert.That(res.Results.Count, Is.GreaterThan(0));
            Assert.That(!string.IsNullOrEmpty(res.Results[0].Subject), Is.True);
        }

        [Test]
        public void TicketSearchByTicketAnonymousType()
        {
            var res = api.Search.SearchFor<Ticket>("my printer is on fire");

            Assert.That(res, Is.Not.EqualTo(null));
            Assert.That(res.Results.Count, Is.GreaterThan(10));
            Assert.That(!string.IsNullOrEmpty(res.Results[0].Subject), Is.True);

            var noRes = api.Search.SearchFor<User>("my printer is on fire");

            Assert.That(noRes, Is.Not.EqualTo(null));
            Assert.That(noRes.Results.Count, Is.EqualTo(0));

            res = api.Search.SearchFor<Ticket>("my printer is on fire", perPage: 10);
            Assert.That(res, Is.Not.EqualTo(null));
            Assert.That(res.Results.Count, Is.EqualTo(10));
            Assert.That(res.Page, Is.EqualTo(1));
            Assert.That(res.Results[0] is Ticket, Is.True);

        }

        [Test]
        public async Task TicketSearchByTicketAnonymousTypeAsync()
        {
            var res = await api.Search.SearchForAsync<Ticket>("my printer is on fire");

            Assert.That(res, Is.Not.EqualTo(null));
            Assert.That(res.Results.Count, Is.GreaterThan(10));
            Assert.That(!string.IsNullOrEmpty(res.Results[0].Subject), Is.True);

            var noRes = await api.Search.SearchForAsync<User>("my printer is on fire");

            Assert.That(noRes, Is.Not.EqualTo(null));
            Assert.That(noRes.Results.Count, Is.EqualTo(0));

            res = await api.Search.SearchForAsync<Ticket>("my printer is on fire", perPage: 10);
            Assert.That(res, Is.Not.EqualTo(null));
            Assert.That(res.Results.Count, Is.EqualTo(10));
            Assert.That(res.Page, Is.EqualTo(1));
            Assert.That(res.Results[0] is Ticket, Is.True);

        }

        [Test]
        public void UserSearchByUserAnonymousType()
        {
            var res = api.Search.SearchFor<User>(Settings.AdminEmail);

            Assert.That(res, Is.Not.EqualTo(null));
            Assert.That(res.Results.Count, Is.EqualTo(1));
            Assert.That(res.Results[0].Id, Is.EqualTo(Settings.UserId));
            Assert.That(res.Results[0] is User, Is.True);
        }

        [Test]
        public async Task UserSearchByUserAnonymousTypeAsync()
        {
            var res = await api.Search.SearchForAsync<User>(Settings.AdminEmail);

            Assert.That(res, Is.Not.EqualTo(null));
            Assert.That(res.Results.Count, Is.EqualTo(1));
            Assert.That(res.Results[0].Id, Is.EqualTo(Settings.UserId));
            Assert.That(res.Results[0] is User, Is.True);
        }

        [Test]
        public void SearchSortIsWorking()
        {
            //desc asc 
            var res = api.Search.SearchFor<Ticket>("Effective", "created_at", "asc");
            Assert.That(res.Count, Is.GreaterThan(2));
            var first = res.Results[0];
            var second = res.Results[1];
            Assert.That(second.CreatedAt, Is.GreaterThan(first.CreatedAt));

        }
    }
}