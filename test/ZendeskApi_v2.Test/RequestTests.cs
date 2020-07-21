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
            Assert.True(res.Count > 0);
        }

        [TestCase(1, 1)]
        [TestCase(1, 2)]
        public void CanGetAllRequestsPaged(int perPage, int page)
        {
            Assert.DoesNotThrow(() =>
            {
                var res = _api.Requests.GetAllRequests(perPage: perPage, page: page);

                Assert.IsNotNull(res);
                Assert.IsNotNull(res.Requests);
                Assert.AreEqual(perPage, res.PageSize);
                Assert.AreEqual(page, res.Page);
            });
        }

        [Test]
        public void CanGetAllRequestsSorted()
        {
            Assert.DoesNotThrow(() =>
            {
                var unsorted = _api.Requests.GetAllRequests();

                Assert.IsNotNull(unsorted);
                Assert.IsNotNull(unsorted.Requests);
                Assert.AreEqual(unsorted.Requests.AsQueryable(), unsorted.Requests.AsQueryable());
                Assert.AreNotEqual(unsorted.Requests.OrderBy(request => request.UpdatedAt).AsQueryable(),
                    unsorted.Requests.AsQueryable());

                var sorted = _api.Requests.GetAllRequests(sortCol: "updated_at", sortAscending: true);

                Assert.IsNotNull(sorted);
                Assert.IsNotNull(sorted.Requests);
                Assert.AreEqual(sorted.Requests.AsQueryable(), sorted.Requests.AsQueryable());
                Assert.AreEqual(sorted.Requests.OrderBy(request => request.UpdatedAt).AsQueryable(),
                    sorted.Requests.AsQueryable());
            });
        }

        [Test]
        public void CanGetOpenRequests()
        {
            var res = _api.Requests.GetAllOpenRequests();
            Assert.True(res.Count > 0);
        }

        [TestCase(1, 1)]
        [TestCase(1, 2)]
        public void CanGetAllOpenRequestsPaged(int perPage, int page)
        {
            Assert.DoesNotThrow(() =>
            {
                var res = _api.Requests.GetAllOpenRequests(perPage: perPage, page: page);

                Assert.IsNotNull(res);
                Assert.IsNotNull(res.Requests);
                Assert.AreEqual(perPage, res.PageSize);
                Assert.AreEqual(page, res.Page);
            });
        }

        [Test]
        public void CanGetAllOpenRequestsSorted()
        {
            Assert.DoesNotThrow(() =>
            {
                var unsorted = _api.Requests.GetAllOpenRequests();

                Assert.IsNotNull(unsorted);
                Assert.IsNotNull(unsorted.Requests);
                Assert.AreEqual(unsorted.Requests.AsQueryable(), unsorted.Requests.AsQueryable());
                Assert.AreNotEqual(unsorted.Requests.OrderBy(request => request.UpdatedAt).AsQueryable(),
                    unsorted.Requests.AsQueryable());

                var sorted = _api.Requests.GetAllOpenRequests(sortCol: "updated_at", sortAscending: true);

                Assert.IsNotNull(sorted);
                Assert.IsNotNull(sorted.Requests);
                Assert.AreEqual(sorted.Requests.AsQueryable(), sorted.Requests.AsQueryable());
                Assert.AreEqual(sorted.Requests.OrderBy(request => request.UpdatedAt).AsQueryable(),
                    sorted.Requests.AsQueryable());
            });
        }

        [Test]
        public void CanGetAllSolvedRequests()
        {
            var res = _api.Requests.GetAllSolvedRequests();
            Assert.True(res.Count > 0);
        }

        [TestCase(1, 1)]
        [TestCase(1, 2)]
        public void CanGetAllSolvedRequestsPaged(int perPage, int page)
        {
            Assert.DoesNotThrow(() =>
            {
                var res = _api.Requests.GetAllSolvedRequests(perPage: perPage, page: page);

                Assert.IsNotNull(res);
                Assert.IsNotNull(res.Requests);
                Assert.AreEqual(perPage, res.PageSize);
                Assert.AreEqual(page, res.Page);
            });
        }

        [Test]
        public void CanGetAllSolvedRequestsSorted()
        {
            Assert.DoesNotThrow(() =>
            {
                var unsorted = _api.Requests.GetAllSolvedRequests();

                Assert.IsNotNull(unsorted);
                Assert.IsNotNull(unsorted.Requests);
                Assert.AreEqual(unsorted.Requests.AsQueryable(), unsorted.Requests.AsQueryable());
                Assert.AreNotEqual(unsorted.Requests.OrderBy(request => request.UpdatedAt).AsQueryable(),
                    unsorted.Requests.AsQueryable());

                var sorted = _api.Requests.GetAllSolvedRequests(sortCol: "updated_at", sortAscending: true);

                Assert.IsNotNull(sorted);
                Assert.IsNotNull(sorted.Requests);
                Assert.AreEqual(sorted.Requests.AsQueryable(), sorted.Requests.AsQueryable());
                Assert.AreEqual(sorted.Requests.OrderBy(request => request.UpdatedAt).AsQueryable(),
                    sorted.Requests.AsQueryable());
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
                Assert.IsNotNull(res);
                Assert.IsNotNull(res.Request);
                Assert.IsTrue(res.Request.Id.HasValue);
                Assert.That(res.Request.Type == RequestType.incident);
                Assert.True(res.Request.Id.Value > 0);


                IndividualUserResponse user = _api.Users.GetUser(res.Request.RequesterId.Value);
                Assert.AreEqual("Test Name", user.User.Name);

                IndividualTicketResponse ticket = _api.Tickets.GetTicket(res.Request.Id.Value);
                CollectionAssert.AreEquivalent(new[] {"tag1", "tag2"}, ticket.Ticket.Tags);

                var res1 = _api.Requests.GetRequestById(res.Request.Id.Value);
                Assert.AreEqual(res1.Request.Id, res.Request.Id);

                res1.Request.Subject = "new subject";
                res1.Request.Comment = new Comment
                {
                    Body = "something more to say",
                    Public = true
                };

                var res2 = _api.Requests.UpdateRequest(res1.Request);
                var res3 = _api.Requests.GetRequestCommentsById(res.Request.Id.Value);

                Assert.AreEqual(res3.Comments.Last().Body.Replace("\n", ""), "something more to say");

                var res4 = _api.Requests.GetSpecificRequestComment(res.Request.Id.Value, res3.Comments.Last().Id.Value);

                res1.Request.RequesterId = 56766413L;
                var res5 = _api.Requests.UpdateRequest(res1.Request);
                var res6 = _api.Requests.GetRequestById(res.Request.Id.Value);

                Assert.AreEqual(res5.Request.RequesterId, res6.Request.RequesterId);
                Assert.AreEqual(res4.Comment.Id, res3.Comments.Last().Id);
            }
            finally
            {
                Assert.True(_api.Tickets.Delete(res.Request.Id.Value));
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
                Assert.IsNotNull(res);
                Assert.IsNotNull(res.Request);
                Assert.IsTrue(res.Request.Id.HasValue);
                Assert.True(res.Request.Id.Value > 0);

                IndividualUserResponse user = _api.Users.GetUser(res.Request.RequesterId.Value);
                Assert.AreEqual("Test Name", user.User.Name);

                IndividualTicketResponse ticket = _api.Tickets.GetTicket(res.Request.Id.Value);
                CollectionAssert.AreEquivalent(new[] { "tag1", "tag2" }, ticket.Ticket.Tags);

                IList<long> collaboratorsIds = ticket.Ticket.CollaboratorIds;
                GroupUserResponse collaborators = _api.Users.GetMultipleUsers(collaboratorsIds.AsEnumerable());
                CollectionAssert.AreEquivalent(emailCCs.Select(e => e.UserEmail), collaborators.Users.Select(u => u.Email));
            }
            finally
            {
                Assert.True(_api.Tickets.Delete(res.Request.Id.Value));
            }
        }
    }
}
