using NUnit.Framework;
using System.Threading.Tasks;
using ZendeskApi_v2.Models.Tickets;
using ZendeskApi_v2.Models.Users;
using ZendeskApi_v2.Tests.Base;

namespace ZendeskApi_v2.Tests;

[TestFixture]
[Category("Search")]
public class SearchTests : TestBase
{
    [Test]
    public void CanSearch()
    {
        var res = Api.Search.SearchFor(Admin.Email);
        Assert.Multiple(() =>
        {
            Assert.That(res.Results[0].ResultType, Is.EqualTo("user"));
            Assert.That(res.Results[0].Id, Is.GreaterThan(0));
        });
    }

    [Test]
    public void BackwardCompatibilitAfterAddingPagination()
    {
        var res = Api.Search.SearchFor("Effective", "updated_at", "asc");
        Assert.That(res.Count, Is.GreaterThan(0));
    }

    [Test]
    public void TotalNumberOftickesShouldbeSameWhenReterivingNextPage()
    {
        var res = Api.Search.SearchFor("Effective"); //search for a custom field - the results are more than one page
        var total = res.Count;

        Assert.That(res.Count, Is.GreaterThan(0));
        Assert.Multiple(() =>
        {
            Assert.That(res.Count, Is.GreaterThan(res.Results.Count)); //result has more than one page
            Assert.That(!string.IsNullOrEmpty(res.NextPage), Is.True); //It has next page
        });
        res = Api.Search.SearchFor("Effective", page: 2); //fetch next page
        Assert.That(res.Count, Is.GreaterThan(0));
        Assert.That(res.Count, Is.EqualTo(total)); //number of results should be same as page 1
    }

    [Test]
    public void TicketHasSubject()
    {
        var res = Api.Search.SearchFor("my printer is on fire");

        Assert.That(res, Is.Not.EqualTo(null));
        Assert.Multiple(() =>
        {
            Assert.That(res.Results, Is.Not.Empty);
            Assert.That(!string.IsNullOrEmpty(res.Results[0].Subject), Is.True);
        });
    }

    [Test]
    public void TicketSearchByTicketAnonymousType()
    {
        var res = Api.Search.SearchFor<Ticket>("my printer is on fire");

        Assert.That(res, Is.Not.EqualTo(null));
        Assert.Multiple(() =>
        {
            Assert.That(res.Results, Has.Count.GreaterThan(10));
            Assert.That(!string.IsNullOrEmpty(res.Results[0].Subject), Is.True);
        });
        var noRes = Api.Search.SearchFor<User>("my printer is on fire");

        Assert.That(noRes, Is.Not.EqualTo(null));
        Assert.That(noRes.Results, Is.Empty);

        res = Api.Search.SearchFor<Ticket>("my printer is on fire", perPage: 10);
        Assert.That(res, Is.Not.EqualTo(null));
        Assert.Multiple(() =>
        {
            Assert.That(res.Results, Has.Count.EqualTo(10));
            Assert.That(res.Page, Is.EqualTo(1));
        });
        Assert.That(res.Results[0] is Ticket, Is.True);
    }

    [Test]
    public async Task TicketSearchByTicketAnonymousTypeAsync()
    {
        var res = await Api.Search.SearchForAsync<Ticket>("my printer is on fire");

        Assert.That(res, Is.Not.EqualTo(null));
        Assert.Multiple(() =>
        {
            Assert.That(res.Results, Has.Count.GreaterThan(10));
            Assert.That(!string.IsNullOrEmpty(res.Results[0].Subject), Is.True);
        });
        var noRes = await Api.Search.SearchForAsync<User>("my printer is on fire");

        Assert.That(noRes, Is.Not.EqualTo(null));
        Assert.That(noRes.Results, Is.Empty);

        res = await Api.Search.SearchForAsync<Ticket>("my printer is on fire", perPage: 10);
        Assert.That(res, Is.Not.EqualTo(null));
        Assert.Multiple(() =>
        {
            Assert.That(res.Results, Has.Count.EqualTo(10));
            Assert.That(res.Page, Is.EqualTo(1));
        });
        Assert.That(res.Results[0] is Ticket, Is.True);
    }

    [Test]
    public void UserSearchByUserAnonymousType()
    {
        var res = Api.Search.SearchFor<User>(Admin.Email);

        Assert.That(res, Is.Not.EqualTo(null));
        Assert.That(res.Results, Has.Count.EqualTo(1));
        Assert.Multiple(() =>
        {
            Assert.That(res.Results[0].Id, Is.EqualTo(Admin.ID));
            Assert.That(res.Results[0] is User, Is.True);
        });
    }

    [Test]
    public async Task UserSearchByUserAnonymousTypeAsync()
    {
        var res = await Api.Search.SearchForAsync<User>(Admin.Email);

        Assert.That(res, Is.Not.EqualTo(null));
        Assert.That(res.Results, Has.Count.EqualTo(1));
        Assert.Multiple(() =>
        {
            Assert.That(res.Results[0].Id, Is.EqualTo(Admin.ID));
            Assert.That(res.Results[0] is User, Is.True);
        });
    }

    [Test]
    public void SearchSortIsWorking()
    {
        //desc asc 
        var res = Api.Search.SearchFor<Ticket>("Effective", "created_at", "asc");
        Assert.That(res.Count, Is.GreaterThan(2));
        var first = res.Results[0];
        var second = res.Results[1];
        Assert.That(second.CreatedAt, Is.GreaterThan(first.CreatedAt));
    }
}