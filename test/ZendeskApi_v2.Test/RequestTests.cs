using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;
using ZendeskApi_v2;
using ZendeskApi_v2.Models.Requests;
using ZendeskApi_v2.Models.Tickets;
using ZendeskApi_v2.Models.Users;

namespace Tests
{
    [TestFixture]
    public class RequestTests
    {
        private readonly ZendeskApi _api = new ZendeskApi(Settings.Site, Settings.AdminEmail, Settings.AdminPassword);

        [Test]
        public void CanGetAllRequests()
        {
            var res = _api.Requests.GetAllRequests();
            Assert.That(res.Count, Is.GreaterThan(0));
        }

        [TestCase(1, 1)]
        [TestCase(1, 2)]
        public void CanGetAllRequestsPaged(int perPage, int page)
        {
            Assert.DoesNotThrow(() =>
            {
                var res = _api.Requests.GetAllRequests(perPage: perPage, page: page);

                Assert.That(res, Is.Not.Null);
                Assert.That(res.Requests, Is.Not.Null);
                Assert.That(res.PageSize, Is.EqualTo(perPage));
                Assert.That(res.Page, Is.EqualTo(page));
            });
        }

        [Test]
        public void CanGetAllRequestsSorted()
        {
            Assert.DoesNotThrow(() =>
            {
                var unsorted = _api.Requests.GetAllRequests();

                Assert.That(unsorted, Is.Not.Null);
                Assert.That(unsorted.Requests, Is.Not.Null);
#pragma warning disable NUnit2009 // The same value has been provided as both the actual and the expected argument
                Assert.That(unsorted.Requests.AsQueryable(), Is.EqualTo(unsorted.Requests.AsQueryable()));
#pragma warning restore NUnit2009 // The same value has been provided as both the actual and the expected argument
                Assert.That(unsorted.Requests.AsQueryable(), Is.Not.EqualTo(unsorted.Requests.OrderBy(request => request.UpdatedAt).AsQueryable()));

                var sorted = _api.Requests.GetAllRequests(sortCol: "updated_at", sortAscending: true);

                Assert.That(sorted, Is.Not.Null);
                Assert.That(sorted.Requests, Is.Not.Null);
#pragma warning disable NUnit2009 // The same value has been provided as both the actual and the expected argument
                Assert.That(sorted.Requests.AsQueryable(), Is.EqualTo(sorted.Requests.AsQueryable()));
#pragma warning restore NUnit2009 // The same value has been provided as both the actual and the expected argument
                Assert.That(sorted.Requests.AsQueryable(), Is.EqualTo(sorted.Requests.OrderBy(request => request.UpdatedAt).AsQueryable()));
            });
        }

        [Test]
        public void CanGetOpenRequests()
        {
            var res = _api.Requests.GetAllOpenRequests();
            Assert.That(res.Count, Is.GreaterThan(0));
        }

        [TestCase(1, 1)]
        [TestCase(1, 2)]
        public void CanGetAllOpenRequestsPaged(int perPage, int page)
        {
            Assert.DoesNotThrow(() =>
            {
                var res = _api.Requests.GetAllOpenRequests(perPage: perPage, page: page);

                Assert.That(res, Is.Not.Null);
                Assert.That(res.Requests, Is.Not.Null);
                Assert.That(res.PageSize, Is.EqualTo(perPage));
                Assert.That(res.Page, Is.EqualTo(page));
            });
        }

        [Test]
        public void CanGetAllOpenRequestsSorted()
        {
            Assert.DoesNotThrow(() =>
            {
                var unsorted = _api.Requests.GetAllOpenRequests();

                Assert.That(unsorted, Is.Not.Null);
                Assert.That(unsorted.Requests, Is.Not.Null);
#pragma warning disable NUnit2009 // The same value has been provided as both the actual and the expected argument
                Assert.That(unsorted.Requests.AsQueryable(), Is.EqualTo(unsorted.Requests.AsQueryable()));
#pragma warning restore NUnit2009 // The same value has been provided as both the actual and the expected argument
                Assert.That(unsorted.Requests.AsQueryable(), Is.Not.EqualTo(unsorted.Requests.OrderBy(request => request.UpdatedAt).AsQueryable()));

                var sorted = _api.Requests.GetAllOpenRequests(sortCol: "updated_at", sortAscending: true);

                Assert.That(sorted, Is.Not.Null);
                Assert.That(sorted.Requests, Is.Not.Null);
#pragma warning disable NUnit2009 // The same value has been provided as both the actual and the expected argument
                Assert.That(sorted.Requests.AsQueryable(), Is.EqualTo(sorted.Requests.AsQueryable()));
#pragma warning restore NUnit2009 // The same value has been provided as both the actual and the expected argument
                Assert.That(sorted.Requests.AsQueryable(), Is.EqualTo(sorted.Requests.OrderBy(request => request.UpdatedAt).AsQueryable()));
            });
        }

        [Test]
        public void CanGetAllSolvedRequests()
        {
            var res = _api.Requests.GetAllSolvedRequests();
            Assert.That(res.Count, Is.GreaterThan(0));
        }

        [TestCase(1, 1)]
        [TestCase(1, 2)]
        public void CanGetAllSolvedRequestsPaged(int perPage, int page)
        {
            Assert.DoesNotThrow(() =>
            {
                var res = _api.Requests.GetAllSolvedRequests(perPage: perPage, page: page);

                Assert.That(res, Is.Not.Null);
                Assert.That(res.Requests, Is.Not.Null);
                Assert.That(res.PageSize, Is.EqualTo(perPage));
                Assert.That(res.Page, Is.EqualTo(page));
            });
        }

        [Test]
        public void CanGetAllSolvedRequestsSorted()
        {
            Assert.DoesNotThrow(() =>
            {
                var unsorted = _api.Requests.GetAllSolvedRequests();

                Assert.That(unsorted, Is.Not.Null);
                Assert.That(unsorted.Requests, Is.Not.Null);
#pragma warning disable NUnit2009 // The same value has been provided as both the actual and the expected argument
                Assert.That(unsorted.Requests.AsQueryable(), Is.EqualTo(unsorted.Requests.AsQueryable()));
#pragma warning restore NUnit2009 // The same value has been provided as both the actual and the expected argument
                Assert.That(unsorted.Requests.AsQueryable(), Is.Not.EqualTo(unsorted.Requests.OrderBy(request => request.UpdatedAt).AsQueryable()));

                var sorted = _api.Requests.GetAllSolvedRequests(sortCol: "updated_at", sortAscending: true);

                Assert.That(sorted, Is.Not.Null);
                Assert.That(sorted.Requests, Is.Not.Null);
#pragma warning disable NUnit2009 // The same value has been provided as both the actual and the expected argument
                Assert.That(sorted.Requests.AsQueryable(), Is.EqualTo(sorted.Requests.AsQueryable()));
#pragma warning restore NUnit2009 // The same value has been provided as both the actual and the expected argument
                Assert.That(sorted.Requests.AsQueryable(), Is.EqualTo(sorted.Requests.OrderBy(request => request.UpdatedAt).AsQueryable()));
            });
        }

        [Test]
        // [Ignore("")]
        public void CanCreateAndUpdateRequests()
        {
            var req = new Request
            {
                Subject = "end user request test",
                Type = RequestType.incident,
                Comment = new Comment
                    {Body = "end user test", HtmlBody = "end user test with </br> new line", Public = true},
                Requester = new Requester
                {
                    Name = "Test Name"
                },
                Tags = new List<string> {"tag1", "tag2"}
            };

            var res = _api.Requests.CreateRequest(req);

            try
            {
                Assert.That(res, Is.Not.Null);
                Assert.That(res.Request, Is.Not.Null);
                Assert.That(res.Request.Id.HasValue, Is.True);
                Assert.That(res.Request.Type, Is.EqualTo(RequestType.incident));
                Assert.That(res.Request.Id.Value, Is.GreaterThan(0));


                IndividualUserResponse user = _api.Users.GetUser(res.Request.RequesterId.Value);
                Assert.That(user.User.Name, Is.EqualTo("Test Name"));

                IndividualTicketResponse ticket = _api.Tickets.GetTicket(res.Request.Id.Value);
                CollectionAssert.AreEquivalent(new[] {"tag1", "tag2"}, ticket.Ticket.Tags);

                var res1 = _api.Requests.GetRequestById(res.Request.Id.Value);
                Assert.That(res.Request.Id, Is.EqualTo(res1.Request.Id));

                res1.Request.Subject = "new subject";
                res1.Request.Comment = new Comment
                {
                    Body = "something more to say",
                    Public = true
                };
                _ = _api.Requests.UpdateRequest(res1.Request);
                var res3 = _api.Requests.GetRequestCommentsById(res.Request.Id.Value);

                Assert.That(res3.Comments.Last().Body.Replace("\n", ""), Is.EqualTo("something more to say"));

                var res4 = _api.Requests.GetSpecificRequestComment(res.Request.Id.Value, res3.Comments.Last().Id.Value);

                res1.Request.RequesterId = 56766413L;
                var res5 = _api.Requests.UpdateRequest(res1.Request);
                var res6 = _api.Requests.GetRequestById(res.Request.Id.Value);

                Assert.That(res6.Request.RequesterId, Is.EqualTo(res5.Request.RequesterId));
                Assert.That(res3.Comments.Last().Id, Is.EqualTo(res4.Comment.Id));
            }
            finally
            {
                Assert.That(_api.Tickets.Delete(res.Request.Id.Value), Is.True);
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
                Type = RequestType.incident,
                Comment = new Comment
                { Body = "end user test", HtmlBody = "end user test with </br> new line", Public = true },
                Requester = new Requester
                {
                    Name = "Test Name"
                },
                Tags = new List<string> { "tag1", "tag2" },
                EmailCCs = emailCCs
            };

            var res = _api.Requests.CreateRequest(req);

            try
            {
                Assert.That(res, Is.Not.Null);
                Assert.That(res.Request, Is.Not.Null);
                Assert.That(res.Request.Id.HasValue, Is.True);
                Assert.That(res.Request.Id.Value, Is.GreaterThan(0));

                IndividualUserResponse user = _api.Users.GetUser(res.Request.RequesterId.Value);
                Assert.That(user.User.Name, Is.EqualTo("Test Name"));

                IndividualTicketResponse ticket = _api.Tickets.GetTicket(res.Request.Id.Value);
                CollectionAssert.AreEquivalent(new[] { "tag1", "tag2" }, ticket.Ticket.Tags);

                IList<long> collaboratorsIds = ticket.Ticket.CollaboratorIds;
                GroupUserResponse collaborators = _api.Users.GetMultipleUsers(collaboratorsIds.AsEnumerable());
                CollectionAssert.AreEquivalent(emailCCs.Select(e => e.UserEmail), collaborators.Users.Select(u => u.Email));
            }
            finally
            {
                Assert.That(_api.Tickets.Delete(res.Request.Id.Value), Is.True);
            }
        }
    }
}
