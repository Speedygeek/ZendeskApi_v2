using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using ZendeskApi_v2.Models.Requests;
using ZendeskApi_v2.Models.Tickets;
using ZendeskApi_v2.Tests.Base;

namespace ZendeskApi_v2.Tests;

[TestFixture]
public class RequestTests : TestBase
{
    [Test]
    public void CanGetAllRequests()
    {
        var res = Api.Requests.GetAllRequests();
        Assert.That(res.Count, Is.GreaterThan(0));
    }

    [TestCase(1, 1)]
    [TestCase(1, 2)]
    public void CanGetAllRequestsPaged(int perPage, int page)
    {
        Assert.DoesNotThrow(() =>
        {
            var res = Api.Requests.GetAllRequests(perPage: perPage, page: page);

            Assert.That(res, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(res.Requests, Is.Not.Null);
                Assert.That(res.PageSize, Is.EqualTo(perPage));
                Assert.That(res.Page, Is.EqualTo(page));
            });
        });
    }

    [Test]
    public void CanGetAllRequestsSorted()
    {
        Assert.DoesNotThrow(() =>
        {
            var unsorted = Api.Requests.GetAllRequests();

            Assert.That(unsorted, Is.Not.Null);
            Assert.That(unsorted.Requests, Is.Not.Null);
            Assert.That(unsorted.Requests.AsQueryable(), Is.Not.EqualTo(unsorted.Requests.OrderBy(request => request.UpdatedAt).AsQueryable()));

            var sorted = Api.Requests.GetAllRequests(sortCol: "updated_at", sortAscending: true);

            Assert.That(sorted, Is.Not.Null);
            Assert.That(sorted.Requests, Is.Not.Null);
            Assert.That(sorted.Requests.AsQueryable(), Is.EqualTo(sorted.Requests.OrderBy(request => request.UpdatedAt).AsQueryable()));
        });
    }

    [Test]
    public void CanGetOpenRequests()
    {
        var res = Api.Requests.GetAllOpenRequests();
        Assert.That(res.Count, Is.GreaterThan(0));
    }

    [TestCase(1, 1)]
    [TestCase(1, 2)]
    public void CanGetAllOpenRequestsPaged(int perPage, int page)
    {
        Assert.DoesNotThrow(() =>
        {
            var res = Api.Requests.GetAllOpenRequests(perPage: perPage, page: page);

            Assert.That(res, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(res.Requests, Is.Not.Null);
                Assert.That(res.PageSize, Is.EqualTo(perPage));
                Assert.That(res.Page, Is.EqualTo(page));
            });
        });
    }

    [Test]
    public void CanGetAllOpenRequestsSorted()
    {
        Assert.DoesNotThrow(() =>
        {
            var unsorted = Api.Requests.GetAllOpenRequests();

            Assert.That(unsorted, Is.Not.Null);
            Assert.That(unsorted.Requests, Is.Not.Null);
            Assert.That(unsorted.Requests.AsQueryable(), Is.Not.EqualTo(unsorted.Requests.OrderBy(request => request.UpdatedAt).AsQueryable()));

            var sorted = Api.Requests.GetAllOpenRequests(sortCol: "updated_at", sortAscending: true);

            Assert.That(sorted, Is.Not.Null);
            Assert.That(sorted.Requests, Is.Not.Null);
            Assert.That(sorted.Requests.AsQueryable(), Is.EqualTo(sorted.Requests.OrderBy(request => request.UpdatedAt).AsQueryable()));
        });
    }

    [Test]
    public void CanGetAllSolvedRequests()
    {
        var res = Api.Requests.GetAllSolvedRequests();
        Assert.That(res.Count, Is.GreaterThan(0));
    }

    [TestCase(1, 1)]
    [TestCase(1, 2)]
    public void CanGetAllSolvedRequestsPaged(int perPage, int page)
    {
        Assert.DoesNotThrow(() =>
        {
            var res = Api.Requests.GetAllSolvedRequests(perPage: perPage, page: page);

            Assert.That(res, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(res.Requests, Is.Not.Null);
                Assert.That(res.PageSize, Is.EqualTo(perPage));
                Assert.That(res.Page, Is.EqualTo(page));
            });
        });
    }

    [Test]
    public void CanGetAllSolvedRequestsSorted()
    {
        Assert.DoesNotThrow(() =>
        {
            var unsorted = Api.Requests.GetAllSolvedRequests();

            Assert.That(unsorted, Is.Not.Null);
            Assert.That(unsorted.Requests, Is.Not.Null);
            Assert.That(unsorted.Requests.AsQueryable(), Is.Not.EqualTo(unsorted.Requests.OrderBy(request => request.UpdatedAt).AsQueryable()));

            var sorted = Api.Requests.GetAllSolvedRequests(sortCol: "updated_at", sortAscending: true);

            Assert.That(sorted, Is.Not.Null);
            Assert.That(sorted.Requests, Is.Not.Null);
            Assert.That(sorted.Requests.AsQueryable(), Is.EqualTo(sorted.Requests.OrderBy(request => request.UpdatedAt).AsQueryable()));
        });
    }

    [Test]
    public void CanCreateAndUpdateRequests()
    {
        var req = new Request
        {
            Subject = "end user request test",
            Type = RequestType.Incident,
            Comment = new Comment
            { Body = "end user test", HtmlBody = "end user test with </br> new line", Public = true },
            Requester = new Requester
            {
                Name = "Test Name"
            },
            Tags = new List<string> { "tag1", "tag2" }
        };

        var res = Api.Requests.CreateRequest(req);

        try
        {
            Assert.That(res, Is.Not.Null);
            Assert.That(res.Request, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(res.Request.Id.HasValue, Is.True);
                Assert.That(res.Request.Type, Is.EqualTo(RequestType.Incident));
                Assert.That(res.Request.Id.Value, Is.GreaterThan(0));
            });
            var user = Api.Users.GetUser(res.Request.RequesterId.Value);
            Assert.That(user.User.Name, Is.EqualTo("Test Name"));

            var ticket = Api.Tickets.GetTicket(res.Request.Id.Value);
            CollectionAssert.AreEquivalent(new[] { "tag1", "tag2" }, ticket.Ticket.Tags);

            var res1 = Api.Requests.GetRequestById(res.Request.Id.Value);
            Assert.That(res.Request.Id, Is.EqualTo(res1.Request.Id));

            res1.Request.Subject = "new subject";
            res1.Request.Comment = new Comment
            {
                Body = "something more to say",
                Public = true
            };
            _ = Api.Requests.UpdateRequest(res1.Request);
            var res3 = Api.Requests.GetRequestCommentsById(res.Request.Id.Value);

            var comment = res3.Comments.OrderBy(c => c.CreatedAt).Last();

            Assert.That(comment.Body.Replace("\n", ""), Is.EqualTo("something more to say"));
            var res4 = Api.Requests.GetSpecificRequestComment(res.Request.Id.Value, comment.Id.Value);

            res1.Request.RequesterId = 56766413L;
            var res5 = Api.Requests.UpdateRequest(res1.Request);
            var res6 = Api.Requests.GetRequestById(res.Request.Id.Value);
            Assert.Multiple(() =>
            {
                Assert.That(res6.Request.RequesterId, Is.EqualTo(res5.Request.RequesterId));
                Assert.That(res3.Comments.Last().Id, Is.EqualTo(res4.Comment.Id));
            });
        }
        finally
        {
            Assert.That(Api.Tickets.Delete(res.Request.Id.Value), Is.True);
        }
    }

    [Test]
    public void CanCreateRequestWithEmailCCs()
    {
        var emailCCs = new List<EmailCC>
        {
            new EmailCC{ UserEmail = "test1@test.com" },
            new EmailCC{ UserEmail = "test2@test.com" }
        };

        var req = new Request
        {
            Subject = "end user request test",
            Type = RequestType.Incident,
            Comment = new Comment
            { Body = "end user test", HtmlBody = "end user test with </br> new line", Public = true },
            Requester = new Requester
            {
                Name = "Test Name"
            },
            Tags = new List<string> { "tag1", "tag2" },
            EmailCCs = emailCCs
        };

        var res = Api.Requests.CreateRequest(req);

        try
        {
            Assert.That(res, Is.Not.Null);
            Assert.That(res.Request, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(res.Request.Id.HasValue, Is.True);
                Assert.That(res.Request.Id.Value, Is.GreaterThan(0));
                Assert.That(res.Request.Type, Is.EqualTo(RequestType.Incident));
            });
            var user = Api.Users.GetUser(res.Request.RequesterId.Value);
            Assert.That(user.User.Name, Is.EqualTo("Test Name"));

            var ticket = Api.Tickets.GetTicket(res.Request.Id.Value);
            CollectionAssert.AreEquivalent(new[] { "tag1", "tag2" }, ticket.Ticket.Tags);

            var collaboratorsIds = ticket.Ticket.CollaboratorIds;
            var collaborators = Api.Users.GetMultipleUsers(collaboratorsIds.AsEnumerable());
            CollectionAssert.AreEquivalent(emailCCs.Select(e => e.UserEmail), collaborators.Users.Select(u => u.Email));
        }
        finally
        {
            Assert.That(Api.Tickets.Delete(res.Request.Id.Value), Is.True);
        }
    }
}
