using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ZendeskApi_v2;
using ZendeskApi_v2.Extensions;
using ZendeskApi_v2.Models.Brands;
using ZendeskApi_v2.Models.Constants;
using ZendeskApi_v2.Models.Shared;
using ZendeskApi_v2.Models.Tickets;
using ZendeskApi_v2.Requests;

namespace Tests
{
    [TestFixture]
    [Category("Tickets")]
    public class TicketTests
    {
        private ZendeskApi api = new ZendeskApi(Settings.Site, Settings.AdminEmail, Settings.AdminPassword);
        private readonly TicketSideLoadOptionsEnum ticketSideLoadOptions = TicketSideLoadOptionsEnum.Users | TicketSideLoadOptionsEnum.Organizations | TicketSideLoadOptionsEnum.Groups;

        [OneTimeTearDown]
        public async Task TestCleanUp()
        {
            GroupTicketFieldResponse response = await api.Tickets.GetTicketFieldsAsync();
            foreach (TicketField item in response.TicketFields)
            {
                if (item.Title == "My Tagger 2")
                {
                    await api.Tickets.DeleteTicketFieldAsync(item.Id.Value);
                }
            }


        }

        [Test]
        public void CanGetTicketsAsync()
        {
            Task<GroupTicketResponse> tickets = api.Tickets.GetAllTicketsAsync();
            Assert.True(tickets.Result.Count > 0);
        }

        [Test]
        public void CanGetTicketsAsyncWithSideLoad()
        {
            Task<GroupTicketResponse> tickets = api.Tickets.GetAllTicketsAsync(sideLoadOptions: ticketSideLoadOptions);
            Assert.True(tickets.Result.Count > 0);
            Assert.IsTrue(tickets.Result.Users.Any());
            Assert.IsTrue(tickets.Result.Organizations.Any());
        }

        [Test]
        public void CanCanGetTicketsByOrganizationIDAsync()
        {
            long id = Settings.OrganizationId;
            Task<GroupTicketResponse> tickets = api.Tickets.GetTicketsByOrganizationIDAsync(id);
            Assert.True(tickets.Result.Count > 0);
        }

        [Test]
        public void CanGetTickets()
        {
            GroupTicketResponse tickets = api.Tickets.GetAllTickets();
            Assert.True(tickets.Count > 0);

            int count = 50;
            GroupTicketResponse nextPage = api.Tickets.GetByPageUrl<GroupTicketResponse>(tickets.NextPage, count);
            Assert.AreEqual(nextPage.Tickets.Count, count);

            GroupTicketResponse ticketsByUser = api.Tickets.GetTicketsByUserID(tickets.Tickets[0].RequesterId.Value);
            Assert.True(ticketsByUser.Count > 0);
        }

        [Test]
        public void CanGetTicketsWithSideLoad()
        {
            GroupTicketResponse tickets = api.Tickets.GetAllTickets(sideLoadOptions: ticketSideLoadOptions);
            Assert.True(tickets.Count > 0);
            Assert.IsTrue(tickets.Users.Any());
            Assert.IsTrue(tickets.Organizations.Any());
        }

        [Test]
        public void CanGetTicketsPaged()
        {
            const int count = 50;
            GroupTicketResponse tickets = api.Tickets.GetAllTickets(count);

            Assert.AreEqual(count, tickets.Tickets.Count);  // 50
            Assert.AreNotEqual(tickets.Count, tickets.Tickets.Count);   // 50 != total count of tickets (assumption)

            const int page = 3;
            GroupTicketResponse thirdPage = api.Tickets.GetAllTickets(count, page);

            Assert.AreEqual(count, thirdPage.Tickets.Count);

            string nextPage = thirdPage.NextPage.GetQueryStringDict()
                    .Where(x => x.Key == "page")
                        .Select(x => x.Value)
                        .FirstOrDefault();

            Assert.NotNull(nextPage);

            Assert.AreEqual(nextPage, (page + 1).ToString());
        }

        [Test]
        public void CanGetTicketById()
        {
            long id = Settings.SampleTicketId;
            Ticket ticket = api.Tickets.GetTicket(id).Ticket;
            Assert.NotNull(ticket);
            Assert.AreEqual(ticket.Id, id);
        }

        [Test]
        public void CanGetTicketByIdWithSideLoad()
        {
            long id = Settings.SampleTicketId;
            IndividualTicketResponse ticket = api.Tickets.GetTicket(id, sideLoadOptions: ticketSideLoadOptions);
            Assert.NotNull(ticket);
            Assert.NotNull(ticket.Ticket);
            Assert.AreEqual(ticket.Ticket.Id, id);
            Assert.IsTrue(ticket.Users.Any());
            Assert.IsTrue(ticket.Organizations.Any());
        }

        [Test]
        public void CanGetTicketsByOrganizationId()
        {
            long id = Settings.OrganizationId;
            GroupTicketResponse tickets = api.Tickets.GetTicketsByOrganizationID(id);
            Assert.True(tickets.Count > 0);
        }

        [Test]
        public void CanGetTicketsByOrganizationIdPaged()
        {
            long id = Settings.OrganizationId;
            GroupTicketResponse ticketsRes = api.Tickets.GetTicketsByOrganizationID(id, 2, 3);

            Assert.AreEqual(3, ticketsRes.PageSize);
            Assert.AreEqual(3, ticketsRes.Tickets.Count);
            Assert.Greater(ticketsRes.Count, 0);

            string nextPage = ticketsRes.NextPage.GetQueryStringDict()
                    .Where(x => x.Key == "page")
                        .Select(x => x.Value)
                        .FirstOrDefault();

            Assert.NotNull(nextPage);

            Assert.AreEqual("3", nextPage);
        }

        [Test]
        public void CanGetTicketsByViewIdPaged()
        {
            GroupTicketResponse ticketsRes = api.Tickets.GetTicketsByViewID(Settings.ViewId, 10, 2);

            Assert.AreEqual(10, ticketsRes.PageSize);
            Assert.AreEqual(10, ticketsRes.Tickets.Count);
            Assert.Greater(ticketsRes.Count, 0);

            string nextPage = ticketsRes.NextPage.GetQueryStringDict()
                    .Where(x => x.Key == "page")
                        .Select(x => x.Value)
                        .FirstOrDefault();

            Assert.NotNull(nextPage);

            Assert.AreEqual("3", nextPage);
        }

        [Test]
        public void CanGetTicketsByViewIdPagedWithSideLoad()
        {
            CanGetTicketsByViewIdPaged();
            GroupTicketResponse ticketsRes = api.Tickets.GetTicketsByViewID(Settings.ViewId, 10, 2, sideLoadOptions: ticketSideLoadOptions);

            Assert.IsTrue(ticketsRes.Users.Any());
            Assert.IsTrue(ticketsRes.Users.Any());
        }

        [Test]
        public void CanTicketsByUserIdPaged()
        {
            GroupTicketResponse ticketsRes = api.Tickets.GetTicketsByUserID(Settings.UserId, 5, 2);

            Assert.AreEqual(5, ticketsRes.PageSize);
            Assert.AreEqual(5, ticketsRes.Tickets.Count);
            Assert.Greater(ticketsRes.Count, 0);

            string nextPage = ticketsRes.NextPage.GetQueryStringDict()
                    .Where(x => x.Key == "page")
                        .Select(x => x.Value)
                        .FirstOrDefault();

            Assert.NotNull(nextPage);

            Assert.AreEqual("3", nextPage);
        }

        [Test]
        public void CanTicketsByUserIdPagedWithSideLoad()
        {
            CanTicketsByUserIdPaged();
            GroupTicketResponse ticketsRes = api.Tickets.GetTicketsByUserID(Settings.UserId, 5, 2, sideLoadOptions: ticketSideLoadOptions);
            Assert.IsTrue(ticketsRes.Users.Any());
            Assert.IsTrue(ticketsRes.Organizations.Any());
        }

        [Test]
        public void CanTicketsByUserIdPagedAsyncWithSideLoad()
        {
            Task<GroupTicketResponse> ticketsRes = api.Tickets.GetTicketsByUserIDAsync(Settings.UserId, 5, 2, sideLoadOptions: ticketSideLoadOptions);
            Assert.IsTrue(ticketsRes.Result.Users.Any());
            Assert.IsTrue(ticketsRes.Result.Organizations.Any());
        }

        [Test]
        public void CanAssignedTicketsByUserIdPaged()
        {
            GroupTicketResponse ticketsRes = api.Tickets.GetAssignedTicketsByUserID(Settings.UserId, 5, 2);

            Assert.AreEqual(5, ticketsRes.PageSize);
            Assert.AreEqual(5, ticketsRes.Tickets.Count);
            Assert.Greater(ticketsRes.Count, 0);

            string nextPage = ticketsRes.NextPage.GetQueryStringDict()
                    .Where(x => x.Key == "page")
                        .Select(x => x.Value)
                        .FirstOrDefault();

            Assert.NotNull(nextPage);

            Assert.AreEqual("3", nextPage);
        }

        [Test]
        public void CanAssignedTicketsByUserIdPagedWithSideLoad()
        {
            CanTicketsByUserIdPaged();
            GroupTicketResponse ticketsRes = api.Tickets.GetAssignedTicketsByUserID(Settings.UserId, 5, 2, sideLoadOptions: ticketSideLoadOptions);
            Assert.IsTrue(ticketsRes.Users.Any());
            Assert.IsTrue(ticketsRes.Organizations.Any());
        }

        [Test]
        public void CanAssignedTicketsByUserIdPagedAsyncWithSideLoad()
        {
            Task<GroupTicketResponse> ticketsRes = api.Tickets.GetAssignedTicketsByUserIDAsync(Settings.UserId, 5, 2, sideLoadOptions: ticketSideLoadOptions);
            Assert.IsTrue(ticketsRes.Result.Users.Any());
            Assert.IsTrue(ticketsRes.Result.Organizations.Any());
        }

        [Test]
        public void CanGetMultipleTickets()
        {
            List<long> ids = new List<long>() { Settings.SampleTicketId, Settings.SampleTicketId2 };
            GroupTicketResponse tickets = api.Tickets.GetMultipleTickets(ids);
            Assert.NotNull(tickets);
            Assert.AreEqual(tickets.Count, ids.Count);
        }

        [Test]
        public async Task CanGetMultipleTicketsAsync()
        {
            List<long> ids = new List<long>() { Settings.SampleTicketId, Settings.SampleTicketId2 };
            GroupTicketResponse tickets = await api.Tickets.GetMultipleTicketsAsync(ids);
            Assert.NotNull(tickets);
            Assert.AreEqual(tickets.Count, ids.Count);
        }

        [Test]
        public void CanGetMultipleTicketsWithSideLoad()
        {
            List<long> ids = new List<long>() { Settings.SampleTicketId, Settings.SampleTicketId2 };
            GroupTicketResponse tickets = api.Tickets.GetMultipleTickets(ids, sideLoadOptions: ticketSideLoadOptions);
            Assert.NotNull(tickets);
            Assert.AreEqual(tickets.Count, ids.Count);
            Assert.IsTrue(tickets.Users.Any());
            Assert.IsTrue(tickets.Organizations.Any());
        }

        [Test]
        public async Task CanGetMultipleTicketsAsyncWithSideLoad()
        {
            List<long> ids = new List<long>() { Settings.SampleTicketId, Settings.SampleTicketId2 };
            GroupTicketResponse tickets = await api.Tickets.GetMultipleTicketsAsync(ids, sideLoadOptions: ticketSideLoadOptions);
            Assert.NotNull(tickets);
            Assert.AreEqual(tickets.Count, ids.Count);
            Assert.IsTrue(tickets.Users.Any());
            Assert.IsTrue(tickets.Organizations.Any());
        }

        [Test]
        public void CanGetMultipleTicketsSingleTicket()
        {
            List<long> ids = new List<long>() { Settings.SampleTicketId };
            GroupTicketResponse tickets = api.Tickets.GetMultipleTickets(ids);
            Assert.NotNull(tickets);
            Assert.AreEqual(tickets.Count, ids.Count);
        }

        [Test]
        public async Task CanGetMultipleTicketsAsyncSingleTicket()
        {
            List<long> ids = new List<long>() { Settings.SampleTicketId };
            GroupTicketResponse tickets = await api.Tickets.GetMultipleTicketsAsync(ids);
            Assert.NotNull(tickets);
            Assert.AreEqual(tickets.Count, ids.Count);
        }

        [Test]
        public void BooleanCustomFieldValuesArePreservedOnUpdate()
        {
            Ticket ticket = new Ticket()
            {
                Subject = "my printer is on fire",
                Comment = new Comment() { Body = "HELP" },
                Priority = TicketPriorities.Urgent,
            };

            ticket.CustomFields = new List<CustomField>()
                {
                    new CustomField()
                        {
                            Id = Settings.CustomFieldId,
                            Value = "testing"
                        },
                    new CustomField()
                        {
                            Id = Settings.CustomBoolFieldId,
                            Value = true
                        }
                };

            Ticket res = api.Tickets.CreateTicket(ticket).Ticket;
            Assert.AreEqual(ticket.CustomFields[1].Value, res.CustomFields.Where(f => f.Id == Settings.CustomBoolFieldId).FirstOrDefault().Value);

            //var updateResponse = api.Tickets.UpdateTicket(res, new Comment() { Body = "Just trying to update it!", Public = true});
            //res.UpdatedAt = null;
            //res.CreatedAt = null;

            IndividualTicketResponse updateResponse = api.Tickets.UpdateTicket(res, new Comment() { Body = "Just trying to update it!", Public = true });

            Assert.AreEqual(ticket.CustomFields[1].Value, updateResponse.Ticket.CustomFields[1].Value);

            Assert.True(api.Tickets.Delete(res.Id.Value));
        }

        [Test]
        public void CanCreateUpdateAndDeleteTicket()
        {
            Ticket ticket = new Ticket()
            {
                Subject = "my printer is on fire",
                Comment = new Comment() { Body = "HELP" },
                Priority = TicketPriorities.Urgent
            };

            ticket.CustomFields = new List<CustomField>()
                {
                    new CustomField()
                        {
                            Id = Settings.CustomFieldId,
                            Value = "testing"
                        }
                };

            Ticket res = api.Tickets.CreateTicket(ticket).Ticket;

            Assert.NotNull(res);
            Assert.Greater(res.Id, 0);

            Assert.AreEqual(res.CreatedAt, res.UpdatedAt);
            Assert.LessOrEqual(res.CreatedAt - DateTimeOffset.UtcNow, TimeSpan.FromMinutes(1.0));

            res.Status = TicketStatus.Solved;
            res.AssigneeId = Settings.UserId;

            res.CollaboratorIds.Add(Settings.CollaboratorId);
            string body = "got it thanks";

            res.CustomFields[0].Value = "updated";

            IndividualTicketResponse updateResponse = api.Tickets.UpdateTicket(res, new Comment() { Body = body, Public = true, Uploads = new List<string>() });

            Assert.NotNull(updateResponse);
            //Assert.AreEqual(updateResponse.Audit.Events.First().Body, body);
            Assert.Greater(updateResponse.Ticket.CollaboratorIds.Count, 0);
            Assert.GreaterOrEqual(updateResponse.Ticket.UpdatedAt, updateResponse.Ticket.CreatedAt);

            Assert.True(api.Tickets.Delete(res.Id.Value));
        }

        [Test]
        public void CanCreateUpdateAndDeleteHTMLTicket()
        {
            Ticket ticket = new Ticket()
            {
                Subject = "my printer is on fire",
                Comment = new Comment() { HtmlBody = "HELP</br>HELP On a New line." },
                Priority = TicketPriorities.Urgent
            };

            ticket.CustomFields = new List<CustomField>()
                {
                    new CustomField()
                        {
                            Id = Settings.CustomFieldId,
                            Value = "testing"
                        }
                };

            Ticket res = api.Tickets.CreateTicket(ticket).Ticket;

            Assert.NotNull(res);
            Assert.Greater(res.Id, 0);

            Assert.AreEqual(res.CreatedAt, res.UpdatedAt);
            Assert.LessOrEqual(res.CreatedAt - DateTimeOffset.UtcNow, TimeSpan.FromMinutes(1.0));

            res.Status = TicketStatus.Solved;
            res.AssigneeId = Settings.UserId;

            res.CollaboratorIds.Add(Settings.CollaboratorId);
            string htmlBody = "HELP</br>HELP On a New line.";

            res.CustomFields[0].Value = "updated";

            IndividualTicketResponse updateResponse = api.Tickets.UpdateTicket(res, new Comment() { HtmlBody = htmlBody, Public = true, Uploads = new List<string>() });

            Assert.NotNull(updateResponse);
            //Assert.AreEqual(updateResponse.Audit.Events.First().Body, body);
            Assert.Greater(updateResponse.Ticket.CollaboratorIds.Count, 0);
            Assert.GreaterOrEqual(updateResponse.Ticket.UpdatedAt, updateResponse.Ticket.CreatedAt);

            Assert.True(api.Tickets.Delete(res.Id.Value));
        }

        [Test]
        public void CanGetTicketComments()
        {
            ZendeskApi_v2.Models.Requests.GroupCommentResponse comments = api.Tickets.GetTicketComments(2);
            Assert.IsNotEmpty(comments.Comments[1].Body);
        }

        [Test]
        public void CanGetTicketHTMLComments()
        {
            ZendeskApi_v2.Models.Requests.GroupCommentResponse comments = api.Tickets.GetTicketComments(2);
            Assert.IsNotEmpty(comments.Comments[1].HtmlBody);
        }

        [Test]
        public void CanGetTicketCommentsWithSideLoading()
        {
            ZendeskApi_v2.Models.Requests.GroupCommentResponse comments = api.Tickets.GetTicketComments(2, sideLoadOptions: ticketSideLoadOptions);
            Assert.IsNotEmpty(comments.Users);
            Assert.IsNull(comments.Organizations);
        }

        [Test]
        public void CanGetTicketCommentsPaged()
        {
            const int perPage = 5;
            const int page = 2;
            ZendeskApi_v2.Models.Requests.GroupCommentResponse commentsRes = api.Tickets.GetTicketComments(2, perPage, page);

            Assert.AreEqual(perPage, commentsRes.Comments.Count);
            Assert.AreEqual(perPage, commentsRes.PageSize);
            Assert.AreEqual(page, commentsRes.Page);

            Assert.IsNotEmpty(commentsRes.Comments[1].Body);

            string nextPageValue = commentsRes.NextPage.GetQueryStringDict()
                    .Where(x => x.Key == "page")
                        .Select(x => x.Value)
                        .FirstOrDefault();

            Assert.NotNull(nextPageValue);

            Assert.AreEqual((page + 1).ToString(), nextPageValue);
        }

        [Test]
        public void CanCreateTicketWithRequester()
        {
            Ticket ticket = new Ticket()
            {
                Subject = "ticket with requester",
                Comment = new Comment() { Body = "testing requester" },
                Priority = TicketPriorities.Normal,
                Requester = new Requester() { Email = Settings.ColloboratorEmail }
            };

            Ticket res = api.Tickets.CreateTicket(ticket).Ticket;

            Assert.NotNull(res);
            Assert.AreEqual(res.RequesterId, Settings.CollaboratorId);

            Assert.True(api.Tickets.Delete(res.Id.Value));
        }

        [Test]
        public async Task CanCreateTicketWithRequesterAsync()
        {
            Ticket ticket = new Ticket()
            {
                Subject = "ticket with requester",
                Comment = new Comment() { Body = "testing requester" },
                Priority = TicketPriorities.Normal,
                Requester = new Requester() { Email = Settings.ColloboratorEmail }
            };

            IndividualTicketResponse res = await api.Tickets.CreateTicketAsync(ticket);

            Assert.NotNull(res);
            Assert.NotNull(res.Ticket);
            Assert.AreEqual(res.Ticket.RequesterId, Settings.CollaboratorId);

            Assert.True(api.Tickets.Delete(res.Ticket.Id.Value));
        }

        [Test]
        public void CanCreateTicketWithDueDate()
        {
            //31 December 2020 2AM
            DateTimeOffset dueAt = DateTimeOffset.Parse("12/31/2020 07:00:00 -05:00", CultureInfo.InvariantCulture);

            Ticket ticket = new Ticket()
            {
                Subject = "ticket with due date",
                Comment = new Comment() { Body = "test comment" },
                Type = "task",
                Priority = TicketPriorities.Normal,
                DueAt = dueAt
            };

            Ticket res = api.Tickets.CreateTicket(ticket).Ticket;

            Assert.That(res, Is.Not.Null);
            Assert.That(res.DueAt, Is.EqualTo(dueAt));

            Assert.True(api.Tickets.Delete(res.Id.Value));
        }

        [Test]
        public void CanCreateTicketWithTicketFormId()
        {
            Ticket ticket = new Ticket()
            {
                Subject = "ticket with ticket form id",
                Comment = new Comment() { Body = "testing requester" },
                Priority = TicketPriorities.Normal,
                TicketFormId = Settings.TicketFormId
            };

            Ticket res = api.Tickets.CreateTicket(ticket).Ticket;

            Assert.NotNull(res);
            Assert.AreEqual(Settings.TicketFormId, res.TicketFormId);

            Assert.True(api.Tickets.Delete(res.Id.Value));
        }

        [Test]
        public void CanBulkUpdateTickets()
        {
            Ticket t1 = api.Tickets.CreateTicket(new Ticket()
            {
                Subject = "testing bulk update",
                Comment = new Comment() { Body = "HELP" },
                Priority = TicketPriorities.Normal
            }).Ticket;
            Ticket t2 = api.Tickets.CreateTicket(new Ticket()
            {
                Subject = "more testing for bulk update",
                Comment = new Comment() { Body = "Bulk UpdateTicket testing" },
                Priority = TicketPriorities.Normal
            }).Ticket;

            JobStatusResponse res = api.Tickets.BulkUpdate(new List<long>() { t1.Id.Value, t2.Id.Value }, new BulkUpdate()
            {
                Status = TicketStatus.Solved,
                Comment = new Comment() { Public = true, Body = "check your email" },
                CollaboratorEmails = new List<string>() { Settings.ColloboratorEmail },
                AssigneeId = Settings.UserId
            });

            Assert.AreEqual(res.JobStatus.Status, "queued");

            //also test JobStatuses while we have a job here
            JobStatusResponse job = api.JobStatuses.GetJobStatus(res.JobStatus.Id);
            Assert.AreEqual(job.JobStatus.Id, res.JobStatus.Id);

            Assert.True(api.Tickets.DeleteMultiple(new List<long>() { t1.Id.Value, t2.Id.Value }));
        }

        [Test]
        public async Task CanAddAttachmentToTicketAsync()
        {
            Upload res = await api.Attachments.UploadAttachmentAsync(new ZenFile()
            {
                ContentType = "text/plain",
                FileName = "testupload.txt",
                FileData = File.ReadAllBytes(TestContext.CurrentContext.TestDirectory + "\\testupload.txt")
            });

            Ticket ticket = new Ticket()
            {
                Subject = "testing attachments",
                Priority = TicketPriorities.Normal,
                Comment = new Comment()
                {
                    Body = "comments are required for attachments",
                    Public = true,
                    Uploads = new List<string>() { res.Token }
                },
            };

            IndividualTicketResponse t1 = await api.Tickets.CreateTicketAsync(ticket);

            Assert.AreEqual(t1.Audit.Events.First().Attachments.Count, 1);

            Assert.True(await api.Tickets.DeleteAsync(t1.Ticket.Id.Value));
        }

        [Test]
        public void CanAddAttachmentToTicket()
        {
            string path = Path.Combine(TestContext.CurrentContext.TestDirectory, "testupload.txt");

            Upload res = api.Attachments.UploadAttachment(new ZenFile()
            {
                ContentType = "text/plain",
                FileName = "testupload.txt",
                FileData = File.ReadAllBytes(path)
            });

            Ticket ticket = new Ticket()
            {
                Subject = "testing attachments",
                Priority = TicketPriorities.Normal,
                Comment = new Comment()
                {
                    Body = "comments are required for attachments",
                    Public = true,
                    Uploads = new List<string>() { res.Token }
                },
            };

            IndividualTicketResponse t1 = api.Tickets.CreateTicket(ticket);
            Assert.That(t1.Audit.Events.First().Attachments.Count, Is.EqualTo(1));

            Assert.That(api.Tickets.Delete(t1.Ticket.Id.Value), Is.True);
            Assert.That(api.Attachments.DeleteUpload(res));
        }

        [Test]
        public void CanGetCollaborators()
        {
            ZendeskApi_v2.Models.Users.GroupUserResponse res = api.Tickets.GetCollaborators(Settings.SampleTicketId);
            Assert.Greater(res.Users.Count, 0);
        }

        [Test]
        public void CanGetIncidents()
        {
            Ticket t1 = api.Tickets.CreateTicket(new Ticket()
            {
                Subject = "test problem",
                Comment = new Comment() { Body = "testing incidents with problems" },
                Priority = TicketPriorities.Normal,
                Type = TicketTypes.Problem
            }).Ticket;

            Ticket t2 = api.Tickets.CreateTicket(new Ticket()
            {
                Subject = "incident",
                Comment = new Comment() { Body = "testing incidents" },
                Priority = TicketPriorities.Normal,
                Type = TicketTypes.Incident,
                ProblemId = t1.Id
            }).Ticket;

            GroupTicketResponse res = api.Tickets.GetIncidents(t1.Id.Value);
            Assert.Greater(res.Tickets.Count, 0);

            Assert.True(api.Tickets.DeleteMultiple(new List<long>() { t1.Id.Value, t2.Id.Value }));
        }

        [Test]
        public void CanGetProblems()
        {
            Ticket t1 = api.Tickets.CreateTicket(new Ticket()
            {
                Subject = "test problem",
                Comment = new Comment() { Body = "testing incidents with problems" },
                Priority = TicketPriorities.Normal,
                Type = TicketTypes.Problem
            }).Ticket;

            GroupTicketResponse res = api.Tickets.GetProblems();
            Assert.Greater(res.Tickets.Count, 0);

            Assert.True(api.Tickets.Delete(t1.Id.Value));
        }

        [Test]
        public void CanGetIncrementalTicketExportPaged()
        {
            const int maxTicketsPerPage = 1000;

            GroupTicketExportResponse res = api.Tickets.GetIncrementalTicketExport(DateTime.Now.AddDays(-365));

            Assert.AreEqual(maxTicketsPerPage, res.Tickets.Count);
            Assert.That(res.NextPage, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void CanGetIncrementalTicketExportWithUsersSideLoadPaged()
        {
            Thread.Sleep(60000);
            const int maxTicketsPerPage = 1000;

            GroupTicketExportResponse res = api.Tickets.GetIncrementalTicketExport(DateTime.Now.AddDays(-365), TicketSideLoadOptionsEnum.Users);

            Assert.AreEqual(maxTicketsPerPage, res.Tickets.Count);
            Assert.IsTrue(res.Users.Count > 0);
            Assert.That(res.NextPage, Is.Not.Null.Or.Empty);

            res = api.Tickets.GetIncrementalTicketExportNextPage(res.NextPage);

            Assert.IsTrue(res.Tickets.Count > 0);
            Assert.IsTrue(res.Users.Count > 0);
        }

        [Test]
        public void CanGetIncrementalTicketExportWithGroupsSideLoadPaged()
        {
            Thread.Sleep(60000);

            const int maxTicketsPerPage = 1000;

            GroupTicketExportResponse res = api.Tickets.GetIncrementalTicketExport(DateTime.Now.AddDays(-700), TicketSideLoadOptionsEnum.Groups);

            Assert.AreEqual(maxTicketsPerPage, res.Tickets.Count);
            Assert.IsTrue(res.Groups.Count > 0);
            Assert.That(res.NextPage, Is.Not.Null.Or.Empty);

            res = api.Tickets.GetIncrementalTicketExportNextPage(res.NextPage);

            Assert.IsTrue(res.Tickets.Count > 0);
            Assert.IsTrue(res.Groups.Count > 0);
        }

        [Test]
        public async Task CanGetIncrementalTicketExportAsyncWithSideLoadOptions()
        {
            GroupTicketExportResponse res = await api.Tickets.GetIncrementalTicketExportAsync(DateTime.Now.AddDays(-31), TicketSideLoadOptionsEnum.Users);

            Assert.That(res.Count, Is.GreaterThan(0));
            Assert.That(res.Users, Is.Not.Null);
        }

        [Test]
        public void CanGetTicketFields()
        {
            GroupTicketFieldResponse res = api.Tickets.GetTicketFields();
            Assert.True(res.TicketFields.Count > 0);
        }

        [Test]
        public void CanGetTicketFieldById()
        {
            long id = Settings.CustomFieldId;
            TicketField ticketField = api.Tickets.GetTicketFieldById(id).TicketField;
            Assert.NotNull(ticketField);
            Assert.AreEqual(ticketField.Id, id);
        }

        [Test]
        public void CanGetTicketFieldByIdAsync()
        {
            long id = Settings.CustomFieldId;
            TicketField ticketField = api.Tickets.GetTicketFieldByIdAsync(id).Result.TicketField;
            Assert.NotNull(ticketField);
            Assert.AreEqual(ticketField.Id, id);
        }

        [Test]
        public void CanCreateUpdateAndDeleteTicketFields()
        {
            TicketField tField = new TicketField()
            {
                Type = TicketFieldTypes.Text,
                Title = "MyField",
            };

            IndividualTicketFieldResponse res = api.Tickets.CreateTicketField(tField);
            Assert.NotNull(res.TicketField);

            TicketField updatedTF = res.TicketField;
            updatedTF.Title = "My Custom Field";

            IndividualTicketFieldResponse updatedRes = api.Tickets.UpdateTicketField(updatedTF);
            Assert.AreEqual(updatedRes.TicketField.Title, updatedTF.Title);

            Assert.True(api.Tickets.DeleteTicketField(updatedTF.Id.Value));
        }

        [Test]
        public void CanCreateAndDeleteTaggerTicketField()
        {
            TicketField tField = new TicketField()
            {
                Type = TicketFieldTypes.Tagger,
                Title = "My Tagger",
                Description = "test description",
                TitleInPortal = "Test Tagger",
                CustomFieldOptions = new List<CustomFieldOptions>()
            };

            tField.CustomFieldOptions.Add(new CustomFieldOptions()
            {
                Name = "test entry",
                Value = "test value"
            });

            IndividualTicketFieldResponse res = api.Tickets.CreateTicketField(tField);
            Assert.NotNull(res.TicketField);

            Assert.True(api.Tickets.DeleteTicketField(res.TicketField.Id.Value));
        }

        [Test]
        public void CanCreateUpdateOptionsAndDeleteTaggerTicketField()
        {
            // https://support.zendesk.com/hc/en-us/articles/204579973--BREAKING-Update-ticket-field-dropdown-fields-by-value-instead-of-id-

            string option1 = "test_value_a";
            string option1_Update = "test_value_a_newtag";
            string option2 = "test_value_b";
            string option3 = "test_value_c";

            TicketField tField = new TicketField()
            {
                Type = TicketFieldTypes.Tagger,
                Title = "My Tagger 2",
                Description = "test description",
                TitleInPortal = "Test Tagger",
                CustomFieldOptions = new List<CustomFieldOptions>()
            };

            tField.CustomFieldOptions.Add(new CustomFieldOptions()
            {
                Name = "test entryA",
                Value = option1
            });

            tField.CustomFieldOptions.Add(new CustomFieldOptions()
            {
                Name = "test entryB",
                Value = option2
            });

            IndividualTicketFieldResponse res = api.Tickets.CreateTicketField(tField);
            Assert.That(res.TicketField, Is.Not.Null);
            Assert.That(res.TicketField.Id, Is.Not.Null);
            Assert.That(res.TicketField.CustomFieldOptions.Count, Is.EqualTo(2));
            Assert.That(res.TicketField.CustomFieldOptions[0].Value, Is.EqualTo(option1));
            Assert.That(res.TicketField.CustomFieldOptions[1].Value, Is.EqualTo(option2));

            long id = res.TicketField.Id.Value;

            TicketField tFieldU = new TicketField()
            {
                Id = id,
                CustomFieldOptions = new List<CustomFieldOptions>()
            };

            //update CustomFieldOption A
            tFieldU.CustomFieldOptions.Add(new CustomFieldOptions()
            {
                Name = "test entryA newTitle",
                Value = option1_Update
            });
            //delete CustomFieldOption B
            //add CustomFieldOption C
            tFieldU.CustomFieldOptions.Add(new CustomFieldOptions()
            {
                Name = "test entryC",
                Value = option3
            });

            IndividualTicketFieldResponse resU = api.Tickets.UpdateTicketField(tFieldU);

            Assert.That(resU.TicketField.CustomFieldOptions.Count, Is.EqualTo(2));
            Assert.That(resU.TicketField.CustomFieldOptions[0].Value, Is.EqualTo(option1_Update));
            Assert.That(resU.TicketField.CustomFieldOptions[1].Value, Is.Not.EqualTo(option2));

            Assert.True(api.Tickets.DeleteTicketField(id));
        }

        [Test]
        [Ignore("Need to Create Suspended Ticket Working with Zendesk support Team")]
        public void CanGetSuspendedTickets()
        {
            ZendeskApi_v2.Models.Tickets.Suspended.GroupSuspendedTicketResponse all = api.Tickets.GetSuspendedTickets();
            Assert.Greater(all.Count, 0);

            ZendeskApi_v2.Models.Tickets.Suspended.IndividualSuspendedTicketResponse ind = api.Tickets.GetSuspendedTicketById(all.SuspendedTickets[0].Id);
            Assert.AreEqual(ind.SuspendedTicket.Id, all.SuspendedTickets[0].Id);

            //There is no way to suspend a ticket so I can run a tests for recovering and deleting them
        }

        [Test]
        public void CanGetTicketForms()
        {
            GroupTicketFormResponse res = api.Tickets.GetTicketForms();
            Assert.Greater(res.Count, 0);
        }

        [Test]
        public void CanCreateUpdateAndDeleteTicketForms()
        {
            //api.Tickets.DeleteTicketForm(52523);
            //return;

            IndividualTicketFormResponse res = api.Tickets.CreateTicketForm(new TicketForm()
            {
                Name = "Snowboard Problem",
                EndUserVisible = true,
                DisplayName = "Snowboard Damage",
                Position = 2,
                Active = true,
                Default = false
            });

            Assert.NotNull(res);
            Assert.Greater(res.TicketForm.Id, 0);

            IndividualTicketFormResponse get = api.Tickets.GetTicketFormById(res.TicketForm.Id.Value);
            Assert.AreEqual(get.TicketForm.Id, res.TicketForm.Id);

            res.TicketForm.Name = "Snowboard Fixed";
            res.TicketForm.DisplayName = "Snowboard has been fixed";
            res.TicketForm.Active = false;

            IndividualTicketFormResponse update = api.Tickets.UpdateTicketForm(res.TicketForm);
            Assert.AreEqual(update.TicketForm.Name, res.TicketForm.Name);

            Assert.True(api.Tickets.DeleteTicketForm(res.TicketForm.Id.Value));
        }

        [Test]
        public void CanReorderTicketForms()
        {
        }

        [Test]
        public void CanCloneTicketForms()
        {
        }

        [Test]
        public void CanGetAllTicketMetrics()
        {
            GroupTicketMetricResponse metrics = api.Tickets.GetAllTicketMetrics();
            Assert.True(metrics.Count > 0);
            int count = 50;
            GroupTicketMetricResponse nextPage = api.Tickets.GetByPageUrl<GroupTicketMetricResponse>(metrics.NextPage, count);
            Assert.AreEqual(nextPage.TicketMetrics.Count, count);
        }

        [Test]
        public void CanGetTicketMetricsAsync()
        {
            Task<GroupTicketMetricResponse> tickets = api.Tickets.GetAllTicketMetricsAsync();
            Assert.True(tickets.Result.Count > 0);
        }

        [Test]
        public void CanGetTicketMetricByTicketId()
        {
            long id = Settings.SampleTicketId;
            TicketMetric metric = api.Tickets.GetTicketMetricsForTicket(id).TicketMetric;
            Assert.NotNull(metric);
            Assert.AreEqual(metric.TicketId, id);
        }

        [Test]
        public void CanGetTicketMetricByTicketIdAsync()
        {
            long id = Settings.SampleTicketId;
            TicketMetric metric = api.Tickets.GetTicketMetricsForTicketAsync(id).Result.TicketMetric;
            Assert.NotNull(metric);
            Assert.AreEqual(metric.TicketId, id);
        }

        [Test]
        public void CanGetAllTicketsWithSideLoad()
        {
            GroupTicketResponse tickets =
                api.Tickets.GetAllTickets(sideLoadOptions: ticketSideLoadOptions);

            Assert.IsTrue(tickets.Users.Any());
            Assert.IsTrue(tickets.Organizations.Any());
        }

        [Test]
        public void CanGetAllTicketsAsyncWithSideLoad()
        {
            Task<GroupTicketResponse> tickets =
                api.Tickets.GetAllTicketsAsync(sideLoadOptions: ticketSideLoadOptions);

            Assert.IsTrue(tickets.Result.Users.Any());
            Assert.IsTrue(tickets.Result.Organizations.Any());
        }

        [Test]
        public void CanGetTicketsByOrganizationIDAsyncWithSideLoad()
        {
            long id = Settings.OrganizationId;
            Task<GroupTicketResponse> tickets = api.Tickets.GetTicketsByOrganizationIDAsync(id, sideLoadOptions: ticketSideLoadOptions);
            Assert.True(tickets.Result.Count > 0);
            Assert.IsTrue(tickets.Result.Users.Any());
            Assert.IsTrue(tickets.Result.Organizations.Any());
        }

        [Test]
        public void CanCanGetTicketsByOrganizationIDWithSideLoad()
        {
            long id = Settings.OrganizationId;
            GroupTicketResponse tickets = api.Tickets.GetTicketsByOrganizationID(id, sideLoadOptions: ticketSideLoadOptions);
            Assert.True(tickets.Count > 0);
            Assert.IsTrue(tickets.Users.Any());
            Assert.IsTrue(tickets.Organizations.Any());
        }

        [Test]
        public void CanImportTicket()
        {
            TicketImport ticket = new TicketImport()
            {
                Subject = "my printer is on fire",
                Comments = new List<TicketImportComment> { new TicketImportComment { AuthorId = Settings.UserId, Value = "HELP comment created in Import 1", Public = false, CreatedAt = DateTime.UtcNow.AddDays(-2) }, new TicketImportComment { AuthorId = Settings.UserId, Value = "HELP comment created in Import 2", Public = false, CreatedAt = DateTime.UtcNow.AddDays(-3) } },
                Priority = TicketPriorities.Urgent,
                CreatedAt = DateTime.Now.AddDays(-5),
                UpdatedAt = DateTime.Now.AddDays(-4),
                SolvedAt = DateTime.Now.AddDays(-3),
                Status = TicketStatus.Solved,
                AssigneeId = Settings.UserId,
                Description = "test description"
            };

            Ticket res = api.Tickets.ImportTicket(ticket).Ticket;

            Assert.NotNull(res);
            Assert.True(res.Id.HasValue);
            Assert.Greater(res.Id.Value, 0);
            Assert.Less(res.CreatedAt.Value.LocalDateTime, DateTime.Now.AddDays(-4));
            Assert.Greater(res.UpdatedAt.Value.LocalDateTime, res.CreatedAt.Value.LocalDateTime);
            Assert.AreEqual(res.Status, TicketStatus.Solved);
            Assert.AreEqual(res.Description, "test description");

            ZendeskApi_v2.Models.Requests.GroupCommentResponse resComments = api.Tickets.GetTicketComments(res.Id.Value);
            Assert.NotNull(resComments);
            Assert.AreEqual(resComments.Count, 3);

            api.Tickets.DeleteAsync(res.Id.Value);
            //Assert.Greater(res.SolvedAt.Value.LocalDateTime, res.UpdatedAt.Value.LocalDateTime);
        }

        [Test]
        public void CanImportTicketAsync()
        {
            TicketImport ticket = new TicketImport()
            {
                Subject = "my printer is on fire",
                Comments = new List<TicketImportComment> { new TicketImportComment { AuthorId = Settings.UserId, Value = "HELP comment created in Import 1", Public = false, CreatedAt = DateTime.UtcNow.AddDays(-2) }, new TicketImportComment { AuthorId = Settings.UserId, Value = "HELP comment created in Import 2", Public = false, CreatedAt = DateTime.UtcNow.AddDays(-3) } },
                Priority = TicketPriorities.Urgent,
                CreatedAt = DateTime.Now.AddDays(-5),
                UpdatedAt = DateTime.Now.AddDays(-4),
                SolvedAt = DateTime.Now.AddDays(-3),
                Status = TicketStatus.Solved,
                AssigneeId = Settings.UserId,
                Description = "test description"
            };

            Task<IndividualTicketResponse> res = api.Tickets.ImportTicketAsync(ticket);

            Assert.NotNull(res.Result);
            Assert.Greater(res.Result.Ticket.Id.Value, 0);
            Assert.Less(res.Result.Ticket.CreatedAt.Value.LocalDateTime, DateTime.Now.AddDays(-4));
            Assert.Greater(res.Result.Ticket.UpdatedAt.Value.LocalDateTime, res.Result.Ticket.CreatedAt.Value.LocalDateTime);
            Assert.AreEqual(res.Result.Ticket.Status, TicketStatus.Solved);
            Assert.AreEqual(res.Result.Ticket.Description, "test description");

            ZendeskApi_v2.Models.Requests.GroupCommentResponse resComments = api.Tickets.GetTicketComments(res.Result.Ticket.Id.Value);
            Assert.NotNull(resComments);
            Assert.AreEqual(resComments.Count, 3);

            api.Tickets.DeleteAsync(res.Id);
        }

        [Test]
        public void CanBulkImportTicket()
        {
            List<TicketImport> test = new List<TicketImport>();

            for (int x = 0; x < 2; x++)
            {
                TicketImport ticket = new TicketImport()
                {
                    Subject = "my printer is on fire",
                    Comments = new List<TicketImportComment> { new TicketImportComment { AuthorId = Settings.UserId, Value = "HELP comment created in Import 1", CreatedAt = DateTime.UtcNow.AddDays(-2), Public = false }, new TicketImportComment { AuthorId = Settings.UserId, Value = "HELP comment created in Import 2", CreatedAt = DateTime.UtcNow.AddDays(-3), Public = false } },
                    Priority = TicketPriorities.Urgent,
                    CreatedAt = DateTime.Now.AddDays(-5),
                    UpdatedAt = DateTime.Now.AddDays(-4),
                    SolvedAt = DateTime.Now.AddDays(-3),
                    Status = TicketStatus.Solved,
                    AssigneeId = Settings.UserId,
                    Description = "test description"
                };
                test.Add(ticket);
            }

            JobStatusResponse res = api.Tickets.BulkImportTickets(test);

            Assert.AreEqual(res.JobStatus.Status, "queued");

            JobStatusResponse job = api.JobStatuses.GetJobStatus(res.JobStatus.Id);
            Assert.AreEqual(job.JobStatus.Id, res.JobStatus.Id);

            int count = 0;
            while (job.JobStatus.Status.ToLower() != "completed" && count < 10)
            {
                Thread.Sleep(1000);
                job = api.JobStatuses.GetJobStatus(res.JobStatus.Id);
                count++;
            }

            Assert.AreEqual(job.JobStatus.Status.ToLower(), "completed");

            foreach (Result r in job.JobStatus.Results)
            {
                Ticket ticket = api.Tickets.GetTicket(r.Id).Ticket;
                Assert.AreEqual(ticket.Description, "test description");
                ZendeskApi_v2.Models.Requests.GroupCommentResponse resComments = api.Tickets.GetTicketComments(r.Id);
                Assert.NotNull(resComments);
                Assert.AreEqual(resComments.Count, 3);
                foreach (Comment c in resComments.Comments)
                {
                    Assert.True(c.CreatedAt.HasValue);
                    Assert.Less(c.CreatedAt.Value.LocalDateTime, DateTime.Now.AddDays(-1));
                }

                api.Tickets.DeleteAsync(r.Id);
            }
        }

        [Test]
        public void CanCreateTicketWithPrivateComment()
        {
            Ticket ticket = new Ticket { Comment = new Comment { Body = "This is a Test", Public = false } };

            JsonSerializerSettings jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DateParseHandling = DateParseHandling.DateTimeOffset,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
                ContractResolver = ZendeskApi_v2.Serialization.ZendeskContractResolver.Instance
            };

            string json = JsonConvert.SerializeObject(ticket, Formatting.None, jsonSettings);
            Assert.That(json, Contains.Substring("false"));
        }

        [Test]
        public async Task ViaChannel_Set_To_API_Isseue_254()
        {
            // see https://github.com/mozts2005/ZendeskApi_v2/issues/254

            Ticket ticket = new Ticket()
            {
                Subject = "my printer is on fire",
                Comment = new Comment() { Body = "HELP" },
                Priority = TicketPriorities.Urgent
            };

            ticket.CustomFields = new List<CustomField>()
                {
                    new CustomField()
                        {
                            Id = Settings.CustomFieldId,
                            Value = "testing"
                        }
                };

            IndividualTicketResponse resp = await api.Tickets.CreateTicketAsync(ticket);
            Ticket newTicket = resp.Ticket;

            Assert.That(newTicket.Via.Channel, Is.EqualTo("api"));

            Comment comment = new Comment { Body = "New comment", Public = true };

            IndividualTicketResponse resp2 = await api.Tickets.UpdateTicketAsync(newTicket, comment);
            ZendeskApi_v2.Models.Requests.GroupCommentResponse resp3 = await api.Tickets.GetTicketCommentsAsync(newTicket.Id.Value);

            Assert.That(resp3.Comments.Any(c => c.Via?.Channel != "api"), Is.False);

            // clean up
            await api.Tickets.DeleteAsync(newTicket.Id.Value);
        }

        [Test]
        public async Task TicketField()
        {
            TicketField tField = new TicketField
            {
                Type = TicketFieldTypes.Tagger,
                Title = "My Tagger 2",
                Description = "test description",
                TitleInPortal = "Test Tagger",
                CustomFieldOptions = new List<CustomFieldOptions>
                {
                    new CustomFieldOptions
                    {
                        Name = "test entryA",
                        Value = "Test2"
                    },
                    new CustomFieldOptions
                    {
                        Name = "test entryB",
                        Value = "test3"
                    }
                }
            };

            IndividualTicketFieldResponse res = await api.Tickets.CreateTicketFieldAsync(tField);
            Assert.That(res.TicketField, Is.Not.Null);
            Assert.That(res.TicketField.Id, Is.Not.Null);
            Assert.That(res.TicketField.CustomFieldOptions.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task CanCreateUpdateOptionsAndDeleteTaggerTicketFieldAsync()
        {
            string option1 = "test_value_a";
            string option1_Update = "test_value_a_newtag";
            string option2 = "test_value_b";
            string option3 = "test_value_c";

            TicketField tField = new TicketField()
            {
                Type = TicketFieldTypes.Tagger,
                Title = "My Tagger 2",
                Description = "test description",
                TitleInPortal = "Test Tagger",
                CustomFieldOptions = new List<CustomFieldOptions>()
            };

            tField.CustomFieldOptions.Add(new CustomFieldOptions()
            {
                Name = "test entryA",
                Value = option1
            });

            tField.CustomFieldOptions.Add(new CustomFieldOptions()
            {
                Name = "test entryB",
                Value = option2
            });

            IndividualTicketFieldResponse res = await api.Tickets.CreateTicketFieldAsync(tField);
            Assert.That(res.TicketField, Is.Not.Null);
            Assert.That(res.TicketField.Id, Is.Not.Null);
            Assert.That(res.TicketField.CustomFieldOptions.Count, Is.EqualTo(2));
            Assert.That(res.TicketField.CustomFieldOptions[0].Value, Is.EqualTo(option1));
            Assert.That(res.TicketField.CustomFieldOptions[1].Value, Is.EqualTo(option2));

            long id = res.TicketField.Id.Value;

            TicketField tFieldU = new TicketField()
            {
                Id = id,
                CustomFieldOptions = new List<CustomFieldOptions>()
            };

            //update CustomFieldOption A
            tFieldU.CustomFieldOptions.Add(new CustomFieldOptions()
            {
                Name = "test entryA newTitle",
                Value = option1_Update
            });
            //delete CustomFieldOption B
            //add CustomFieldOption C
            tFieldU.CustomFieldOptions.Add(new CustomFieldOptions()
            {
                Name = "test entryC",
                Value = option3
            });

            IndividualTicketFieldResponse resU = await api.Tickets.UpdateTicketFieldAsync(tFieldU);

            Assert.That(resU.TicketField.CustomFieldOptions.Count, Is.EqualTo(2));
            Assert.That(resU.TicketField.CustomFieldOptions[0].Value, Is.EqualTo(option1_Update));
            Assert.That(resU.TicketField.CustomFieldOptions[1].Value, Is.Not.EqualTo(option2));

            Assert.True(await api.Tickets.DeleteTicketFieldAsync(id));
        }

        [Test]
        public async Task CanGetBrandId()
        {
            Brand brand = new Brand()
            {
                Name = "Test Brand",
                Active = true,
                Subdomain = $"test-{Guid.NewGuid()}"
            };

            IndividualBrandResponse respBrand = api.Brands.CreateBrand(brand);

            brand = respBrand.Brand;

            Ticket ticket = new Ticket { Comment = new Comment { Body = "This is a Brand id Test", Public = false }, BrandId = brand.Id };
            IndividualTicketResponse respTicket = await api.Tickets.CreateTicketAsync(ticket);
            GroupTicketResponse respTikets = await api.Tickets.GetMultipleTicketsAsync(new List<long> { respTicket.Ticket.Id.Value });

            Assert.That(respTikets.Tickets[0].BrandId, Is.EqualTo(brand.Id));

            // clean up
            Assert.True(api.Brands.DeleteBrand(brand.Id.Value));
            Assert.True(await api.Tickets.DeleteAsync(respTicket.Ticket.Id.Value));
        }

        [Test]
        public async Task CanGetIsPublicAsync()
        {
            Ticket ticket = new Ticket()
            {
                Subject = "my printer is on fire",
                Comment = new Comment { Body = "HELP", Public = true },
                Priority = TicketPriorities.Urgent
            };

            IndividualTicketResponse resp1 = await api.Tickets.CreateTicketAsync(ticket);
            Assert.That(resp1.Ticket.IsPublic, Is.True);

            ticket.Comment.Public = false;
            IndividualTicketResponse resp2 = await api.Tickets.CreateTicketAsync(ticket);

            Assert.That(resp2.Ticket.IsPublic, Is.False);

            Assert.That(await api.Tickets.DeleteAsync(resp1.Ticket.Id.Value), Is.True);
            Assert.That(await api.Tickets.DeleteAsync(resp2.Ticket.Id.Value), Is.True);
        }

        [Test]
        public async Task CanGetSystemFieldOptions()
        {
            IndividualTicketFieldResponse resp = await api.Tickets.GetTicketFieldByIdAsync(21830872);

            Assert.That(resp.TicketField.SystemFieldOptions, Is.Not.Null);
        }

        [Test]
        public async Task CanSetFollowupID()
        {
            Ticket ticket = new Ticket { Comment = new Comment { Body = "This is a Test", Public = false } };

            IndividualTicketResponse resp1 = await api.Tickets.CreateTicketAsync(ticket);

            Ticket closedTicket = resp1.Ticket;

            closedTicket.Status = TicketStatus.Closed;

            IndividualTicketResponse resp2 = await api.Tickets.UpdateTicketAsync(closedTicket, new Comment { Body = "Closing Ticket" });

            Ticket ticket_Followup = new Ticket()
            {
                Subject = "This is a Test Follow up",
                Comment = new Comment { Body = "HELP", Public = true },
                Priority = TicketPriorities.Urgent,
                ViaFollowupSourceId = closedTicket.Id.Value
            };

            IndividualTicketResponse resp3 = await api.Tickets.CreateTicketAsync(ticket_Followup);

            Assert.That(resp3.Ticket.Via.Source.Rel, Is.EqualTo("follow_up"));

            Assert.That(await api.Tickets.DeleteAsync(resp3.Ticket.Id.Value), Is.True);
            Assert.That(await api.Tickets.DeleteAsync(closedTicket.Id.Value), Is.True);
        }

        [Test]
        public void CanCreateManyTickets()
        {
            string comment = "testing create Many";

            List<Ticket> tickets = new List<Ticket> {
                new Ticket{ Subject = "ticket Test number 1", Comment = new Comment() { Body = comment  }, Priority = TicketPriorities.Normal},
                new Ticket{ Subject ="ticket Test number 2", Comment = new Comment{ Body = comment  }, Priority = TicketPriorities.Normal }
                };

            JobStatusResponse res = api.Tickets.CreateManyTickets(tickets);
            Assert.That(res.JobStatus.Status, Is.EqualTo("queued"));

            JobStatusResponse job = api.JobStatuses.GetJobStatus(res.JobStatus.Id);
            Assert.That(job.JobStatus.Id, Is.EqualTo(res.JobStatus.Id));

            int count = 0;
            while (job.JobStatus.Status.ToLower() != "completed" && count < 10)
            {
                Thread.Sleep(1000);
                job = api.JobStatuses.GetJobStatus(res.JobStatus.Id);
                count++;
            }

            Assert.That(job.JobStatus.Status.ToLower(), Is.EqualTo("completed"));

            foreach (Result r in job.JobStatus.Results)
            {
                Ticket ticket = api.Tickets.GetTicket(r.Id).Ticket;
                Assert.That(ticket.Description, Is.EqualTo(comment));
                api.Tickets.Delete(r.Id);
            }
        }

        [Test]
        public async Task CanCreateManyTicketsAsync()
        {
            string comment = "testing create Many";

            List<Ticket> tickets = new List<Ticket> {
                new Ticket{ Subject = "ticket Test number 1", Comment = new Comment() { Body = comment  }, Priority = TicketPriorities.Normal},
                new Ticket{ Subject ="ticket Test number 2", Comment = new Comment{ Body = comment  }, Priority = TicketPriorities.Normal }
                };

            JobStatusResponse res = await api.Tickets.CreateManyTicketsAsync(tickets);
            Assert.That(res.JobStatus.Status, Is.EqualTo("queued"));

            JobStatusResponse job = await api.JobStatuses.GetJobStatusAsync(res.JobStatus.Id);
            Assert.That(job.JobStatus.Id, Is.EqualTo(res.JobStatus.Id));

            int count = 0;
            while (job.JobStatus.Status.ToLower() != "completed" && count < 10)
            {
                await Task.Delay(1000);
                job = await api.JobStatuses.GetJobStatusAsync(res.JobStatus.Id);
                count++;
            }

            Assert.That(job.JobStatus.Status.ToLower(), Is.EqualTo("completed"));

            foreach (Result r in job.JobStatus.Results)
            {
                Ticket ticket = (await api.Tickets.GetTicketAsync(r.Id)).Ticket;
                Assert.That(ticket.Description, Is.EqualTo(comment));
                await api.Tickets.DeleteAsync(r.Id);
            }
        }

    }
}
