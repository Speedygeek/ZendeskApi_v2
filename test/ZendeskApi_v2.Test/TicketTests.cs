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
        private readonly ZendeskApi api = new ZendeskApi(Settings.Site, Settings.AdminEmail, Settings.AdminPassword);
        private readonly TicketSideLoadOptionsEnum ticketSideLoadOptions = TicketSideLoadOptionsEnum.Users | TicketSideLoadOptionsEnum.Organizations | TicketSideLoadOptionsEnum.Groups | TicketSideLoadOptionsEnum.Comment_Count;

        [OneTimeTearDown]
        public async Task TestCleanUp()
        {
            var response = await api.Tickets.GetTicketFieldsAsync();
            foreach (var item in response.TicketFields)
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
            var tickets = api.Tickets.GetAllTicketsAsync();
            Assert.That(tickets.Result.Count, Is.GreaterThan(0));
        }

        [Test]
        public void CanGetTicketsAsyncWithSideLoad()
        {
            var tickets = api.Tickets.GetAllTicketsAsync(sideLoadOptions: ticketSideLoadOptions);
            Assert.That(tickets.Result.Count, Is.GreaterThan(0));
            Assert.That(tickets.Result.Users.Any(), Is.True);
            Assert.That(tickets.Result.Organizations.Any(), Is.True);
        }

        [Test]
        public void CanCanGetTicketsByOrganizationIDAsync()
        {
            var id = Settings.OrganizationId;
            var tickets = api.Tickets.GetTicketsByOrganizationIDAsync(id);
            Assert.That(tickets.Result.Count, Is.GreaterThan(0));
        }

        [Test]
        public void CanGetTickets()
        {
            var tickets = api.Tickets.GetAllTickets();
            Assert.That(tickets.Count, Is.GreaterThan(0));

            var count = 50;
            var nextPage = api.Tickets.GetByPageUrl<GroupTicketResponse>(tickets.NextPage, count);
            Assert.That(count, Is.EqualTo(nextPage.Tickets.Count));

            var ticketsByUser = api.Tickets.GetTicketsByUserID(tickets.Tickets[0].RequesterId.Value);
            Assert.That(ticketsByUser.Count, Is.GreaterThan(0));
        }

        [Test]
        public void CanGetTicketsWithSideLoad()
        {
            var tickets = api.Tickets.GetAllTickets(sideLoadOptions: ticketSideLoadOptions);
            Assert.That(tickets.Count, Is.GreaterThan(0));
            Assert.That(tickets.Users.Any(), Is.True);
            Assert.That(tickets.Organizations.Any(), Is.True);
        }

        [Test]
        public void CanGetTicketsPaged()
        {
            const int count = 50;
            var tickets = api.Tickets.GetAllTickets(count);

            Assert.That(tickets.Tickets.Count, Is.EqualTo(count));  // 50
            Assert.That(tickets.Tickets.Count, Is.Not.EqualTo(tickets.Count));   // 50 != total count of tickets (assumption)

            const int page = 3;
            var thirdPage = api.Tickets.GetAllTickets(count, page);

            Assert.That(thirdPage.Tickets.Count, Is.EqualTo(count));

            var nextPage = thirdPage.NextPage.GetQueryStringDict()
                    .Where(x => x.Key == "page")
                        .Select(x => x.Value)
                        .FirstOrDefault();

            Assert.That(nextPage, Is.Not.Null);

            Assert.That((page + 1).ToString(), Is.EqualTo(nextPage));
        }

        [Test]
        public void CanGetTicketById()
        {
            var id = Settings.SampleTicketId;
            var ticket = api.Tickets.GetTicket(id).Ticket;
            Assert.That(ticket, Is.Not.Null);
            Assert.That(id, Is.EqualTo(ticket.Id));
        }

        [Test]
        public void CanGetTicketByIdWithSideLoad()
        {
            var id = Settings.SampleTicketId;
            var ticket = api.Tickets.GetTicket(id, sideLoadOptions: ticketSideLoadOptions);
            Assert.That(ticket, Is.Not.Null);
            Assert.That(ticket.Ticket, Is.Not.Null);
            Assert.That(id, Is.EqualTo(ticket.Ticket.Id));
            Assert.That(ticket.Users.Any(), Is.True);
            Assert.That(ticket.Organizations.Any(), Is.True);
        }

        [Test]
        public void CanGetTicketsByOrganizationId()
        {
            var id = Settings.OrganizationId;
            var tickets = api.Tickets.GetTicketsByOrganizationID(id);
            Assert.That(tickets.Count, Is.GreaterThan(0));
        }

        [Test]
        public void CanGetTicketsByOrganizationIdPaged()
        {
            var id = Settings.OrganizationId;
            var ticketsRes = api.Tickets.GetTicketsByOrganizationID(id, 2, 3);

            Assert.That(ticketsRes.PageSize, Is.EqualTo(3));
            Assert.That(ticketsRes.Tickets.Count, Is.EqualTo(3));
            Assert.That(ticketsRes.Count, Is.GreaterThan(0));

            var nextPage = ticketsRes.NextPage.GetQueryStringDict()
                    .Where(x => x.Key == "page")
                        .Select(x => x.Value)
                        .FirstOrDefault();

            Assert.That(nextPage, Is.Not.Null);

            Assert.That(nextPage, Is.EqualTo("3"));
        }

        [Test]
        public void CanGetTicketsByViewIdPaged()
        {
            var ticketsRes = api.Tickets.GetTicketsByViewID(Settings.ViewId, 10, 2);

            Assert.That(ticketsRes.PageSize, Is.EqualTo(10));
            Assert.That(ticketsRes.Tickets.Count, Is.EqualTo(10));
            Assert.That(ticketsRes.Count, Is.GreaterThan(0));

            var nextPage = ticketsRes.NextPage.GetQueryStringDict()
                    .Where(x => x.Key == "page")
                        .Select(x => x.Value)
                        .FirstOrDefault();

            Assert.That(nextPage, Is.Not.Null);

            Assert.That(nextPage, Is.EqualTo("3"));
        }

        [Test]
        public void CanGetTicketsByViewIdPagedWithSideLoad()
        {
            CanGetTicketsByViewIdPaged();
            var ticketsRes = api.Tickets.GetTicketsByViewID(Settings.ViewId, 10, 2, sideLoadOptions: ticketSideLoadOptions);

            Assert.That(ticketsRes.Users.Any(), Is.True);
            Assert.That(ticketsRes.Users.Any(), Is.True);
        }

        [Test]
        [Ignore("Issue Opened with Zendesk")]
        public void CanTicketsByUserIdPaged()
        {
            var ticketsRes = api.Tickets.GetTicketsByUserID(Settings.UserId, 5, 2);

            Assert.That(ticketsRes.PageSize, Is.EqualTo(5));
            Assert.That(ticketsRes.Tickets.Count, Is.EqualTo(5));
            Assert.That(ticketsRes.Count, Is.GreaterThan(0));

            var nextPage = ticketsRes.NextPage.GetQueryStringDict()
                    .Where(x => x.Key == "page")
                        .Select(x => x.Value)
                        .FirstOrDefault();

            Assert.That(nextPage, Is.Not.Null);

            Assert.That(nextPage, Is.EqualTo("3"));
        }

        [Test]
        [Ignore("Issue Opened with Zendesk")]
        public void CanTicketsByUserIdPagedWithSideLoad()
        {
            CanTicketsByUserIdPaged();
            var ticketsRes = api.Tickets.GetTicketsByUserID(Settings.UserId, 5, 2, sideLoadOptions: ticketSideLoadOptions);
            Assert.That(ticketsRes.Users.Any(), Is.True);
            Assert.That(ticketsRes.Organizations.Any(), Is.True);
        }

        [Test]
        public async Task CanTicketsByUserIdPagedAsyncWithSideLoad()
        {
            var ticketsRes = await api.Tickets.GetTicketsByUserIDAsync(Settings.UserId, 50, 2, sideLoadOptions: ticketSideLoadOptions);

            Assert.That(ticketsRes.Users.Any(), Is.True);
            Assert.That(ticketsRes.Organizations.Any(), Is.True);
        }

        [Test]
        public void CanAssignedTicketsByUserIdPaged()
        {
            var ticketsRes = api.Tickets.GetAssignedTicketsByUserID(Settings.UserId, 5, 2);

            Assert.That(ticketsRes.PageSize, Is.EqualTo(5));
            Assert.That(ticketsRes.Tickets.Count, Is.EqualTo(5));
            Assert.That(ticketsRes.Count, Is.GreaterThan(0));

            var nextPage = ticketsRes.NextPage.GetQueryStringDict()
                    .Where(x => x.Key == "page")
                        .Select(x => x.Value)
                        .FirstOrDefault();

            Assert.That(nextPage, Is.Not.Null);

            Assert.That(nextPage, Is.EqualTo("3"));
        }

        [Test]
        [Ignore("Issue Opened with Zendesk")]
        public void CanAssignedTicketsByUserIdPagedWithSideLoad()
        {
            CanTicketsByUserIdPaged();
            var ticketsRes = api.Tickets.GetAssignedTicketsByUserID(Settings.UserId, 5, 2, sideLoadOptions: ticketSideLoadOptions);
            Assert.That(ticketsRes.Users.Any(), Is.True);
            Assert.That(ticketsRes.Organizations.Any(), Is.True);
        }

        [Test]
        public void CanAssignedTicketsByUserIdPagedAsyncWithSideLoad()
        {
            var ticketsRes = api.Tickets.GetAssignedTicketsByUserIDAsync(Settings.UserId, 5, 2, sideLoadOptions: ticketSideLoadOptions);
            Assert.That(ticketsRes.Result.Users.Any(), Is.True);
            Assert.That(ticketsRes.Result.Organizations.Any(), Is.True);
        }

        [Test]
        public void CanGetMultipleTickets()
        {
            var ids = new List<long>() { Settings.SampleTicketId, Settings.SampleTicketId2 };
            var tickets = api.Tickets.GetMultipleTickets(ids);
            Assert.That(tickets, Is.Not.Null);
            Assert.That(ids.Count, Is.EqualTo(tickets.Count));
        }

        [Test]
        public async Task CanGetMultipleTicketsAsync()
        {
            var ids = new List<long>() { Settings.SampleTicketId, Settings.SampleTicketId2 };
            var tickets = await api.Tickets.GetMultipleTicketsAsync(ids);
            Assert.That(tickets, Is.Not.Null);
            Assert.That(ids.Count, Is.EqualTo(tickets.Count));
        }

        [Test]
        public void CanGetMultipleTicketsWithSideLoad()
        {
            var ids = new List<long>() { Settings.SampleTicketId, Settings.SampleTicketId2 };
            var tickets = api.Tickets.GetMultipleTickets(ids, sideLoadOptions: ticketSideLoadOptions);
            Assert.That(tickets, Is.Not.Null);
            Assert.That(ids.Count, Is.EqualTo(tickets.Count));
            Assert.That(tickets.Users.Any(), Is.True);
            Assert.That(tickets.Organizations.Any(), Is.True);
        }

        [Test]
        public async Task CanGetMultipleTicketsAsyncWithSideLoad()
        {
            var ids = new List<long>() { Settings.SampleTicketId, Settings.SampleTicketId2 };
            var tickets = await api.Tickets.GetMultipleTicketsAsync(ids, sideLoadOptions: ticketSideLoadOptions);
            Assert.That(tickets, Is.Not.Null);
            Assert.That(ids.Count, Is.EqualTo(tickets.Count));
            Assert.That(tickets.Users.Any(), Is.True);
            Assert.That(tickets.Organizations.Any(), Is.True);
        }

        [Test]
        public void CanGetMultipleTicketsSingleTicket()
        {
            var ids = new List<long>() { Settings.SampleTicketId };
            var tickets = api.Tickets.GetMultipleTickets(ids);
            Assert.That(tickets, Is.Not.Null);
            Assert.That(ids.Count, Is.EqualTo(tickets.Count));
        }

        [Test]
        public async Task CanGetMultipleTicketsAsyncSingleTicket()
        {
            var ids = new List<long>() { Settings.SampleTicketId };
            var tickets = await api.Tickets.GetMultipleTicketsAsync(ids);
            Assert.That(tickets, Is.Not.Null);
            Assert.That(ids.Count, Is.EqualTo(tickets.Count));
        }

        [Test]
        public void BooleanCustomFieldValuesArePreservedOnUpdate()
        {
            var ticket = new Ticket()
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

            var res = api.Tickets.CreateTicket(ticket).Ticket;
            Assert.That(res.CustomFields.Where(f => f.Id == Settings.CustomBoolFieldId).FirstOrDefault().Value, Is.EqualTo(ticket.CustomFields[1].Value));

            var updateResponse = api.Tickets.UpdateTicket(res, new Comment() { Body = "Just trying to update it!", Public = true });

            Assert.That(updateResponse.Ticket.CustomFields[1].Value, Is.EqualTo(ticket.CustomFields[1].Value));

            Assert.That(api.Tickets.Delete(res.Id.Value), Is.True);
        }

        [Test]
        public void CanCreateUpdateAndDeleteTicket()
        {
            var ticket = new Ticket()
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

            var res = api.Tickets.CreateTicket(ticket).Ticket;

            Assert.That(res, Is.Not.Null);
            Assert.That(res.Id, Is.GreaterThan(0));

            Assert.That(res.UpdatedAt, Is.EqualTo(res.CreatedAt));
            Assert.That(res.CreatedAt - DateTimeOffset.UtcNow, Is.LessThanOrEqualTo(TimeSpan.FromMinutes(1.0)));

            res.Status = TicketStatus.Solved;
            res.AssigneeId = Settings.UserId;

            res.CollaboratorIds.Add(Settings.CollaboratorId);
            var body = "got it thanks";

            res.CustomFields[0].Value = "updated";

            var updateResponse = api.Tickets.UpdateTicket(res, new Comment() { Body = body, Public = true, Uploads = new List<string>() });

            Assert.That(updateResponse, Is.Not.Null);
            //Assert.AreEqual(updateResponse.Audit.Events.First().Body, body);
            Assert.That(updateResponse.Ticket.CollaboratorIds.Count, Is.GreaterThan(0));
            Assert.That(updateResponse.Ticket.UpdatedAt, Is.GreaterThanOrEqualTo(updateResponse.Ticket.CreatedAt));

            Assert.That(api.Tickets.Delete(res.Id.Value), Is.True);
        }

        [Test]
        public void CanCreateUpdateAndDeleteHTMLTicket()
        {
            var ticket = new Ticket()
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

            var res = api.Tickets.CreateTicket(ticket).Ticket;

            Assert.That(res, Is.Not.Null);
            Assert.That(res.Id, Is.GreaterThan(0));

            Assert.That(res.UpdatedAt, Is.EqualTo(res.CreatedAt));
            Assert.That(res.CreatedAt - DateTimeOffset.UtcNow, Is.LessThanOrEqualTo(TimeSpan.FromMinutes(1.0)));

            res.Status = TicketStatus.Solved;
            res.AssigneeId = Settings.UserId;

            res.CollaboratorIds.Add(Settings.CollaboratorId);
            var htmlBody = "HELP</br>HELP On a New line.";

            res.CustomFields[0].Value = "updated";

            var updateResponse = api.Tickets.UpdateTicket(res, new Comment() { HtmlBody = htmlBody, Public = true, Uploads = new List<string>() });

            Assert.That(updateResponse, Is.Not.Null);
            //Assert.AreEqual(updateResponse.Audit.Events.First().Body, body);
            Assert.That(updateResponse.Ticket.CollaboratorIds.Count, Is.GreaterThan(0));
            Assert.That(updateResponse.Ticket.UpdatedAt, Is.GreaterThanOrEqualTo(updateResponse.Ticket.CreatedAt));

            Assert.That(api.Tickets.Delete(res.Id.Value), Is.True);
        }

        [Test]
        public void CanGetTicketComments()
        {
            var comments = api.Tickets.GetTicketComments(2);
            Assert.That(comments.Comments[1].Body, Is.Not.Empty);
        }

        [Test]
        public void CanGetTicketHTMLComments()
        {
            var comments = api.Tickets.GetTicketComments(2);
            Assert.That(comments.Comments[1].HtmlBody, Is.Not.Empty);
        }

        [Test]
        public void CanGetTicketCommentsWithSideLoading()
        {
            var comments = api.Tickets.GetTicketComments(2, sideLoadOptions: ticketSideLoadOptions);
            Assert.That(comments.Users, Is.Not.Empty);
            Assert.That(comments.Organizations, Is.Null);
        }

        [Test]
        public void CanGetTicketCommentsPaged()
        {
            const int perPage = 5;
            const int page = 2;
            var commentsRes = api.Tickets.GetTicketComments(2, perPage, page);

            Assert.That(commentsRes.Comments.Count, Is.EqualTo(perPage));
            Assert.That(commentsRes.PageSize, Is.EqualTo(perPage));
            Assert.That(commentsRes.Page, Is.EqualTo(page));

            Assert.That(commentsRes.Comments[1].Body, Is.Not.Empty);

            var nextPageValue = commentsRes.NextPage.GetQueryStringDict()
                    .Where(x => x.Key == "page")
                        .Select(x => x.Value)
                        .FirstOrDefault();

            Assert.That(nextPageValue, Is.Not.Null);

            Assert.That(nextPageValue, Is.EqualTo((page + 1).ToString()));
        }

        [Test]
        public void CanGetTicketCommentsPagedAndSorted()
        {
            const int perPage = 1;
            const int page = 1;
            var commentsRes = api.Tickets.GetTicketComments(2, perPage, page);
            var commentsRes2 = api.Tickets.GetTicketComments(2, false, perPage, page);

            Assert.That(commentsRes.Comments[0].CreatedAt, Is.EqualTo(new DateTimeOffset(2012, 10, 30, 13, 35, 11, TimeSpan.Zero)));
            Assert.That(commentsRes2.Comments[0].CreatedAt, Is.EqualTo(new DateTimeOffset(2014, 01, 24, 03, 29, 30, TimeSpan.Zero)));
        }

        [Test]
        public void CanCreateTicketWithRequester()
        {
            var ticket = new Ticket()
            {
                Subject = "ticket with requester",
                Comment = new Comment() { Body = "testing requester" },
                Priority = TicketPriorities.Normal,
                Requester = new Requester() { Email = Settings.ColloboratorEmail }
            };

            var res = api.Tickets.CreateTicket(ticket).Ticket;

            Assert.That(res, Is.Not.Null);
            Assert.That(res.RequesterId, Is.EqualTo(Settings.CollaboratorId));

            Assert.That(api.Tickets.Delete(res.Id.Value), Is.True);
        }

        [Test]
        public async Task CanCreateTicketWithRequesterAsync()
        {
            var ticket = new Ticket()
            {
                Subject = "ticket with requester",
                Comment = new Comment() { Body = "testing requester" },
                Priority = TicketPriorities.Normal,
                Requester = new Requester() { Email = Settings.ColloboratorEmail }
            };

            var res = await api.Tickets.CreateTicketAsync(ticket);

            Assert.That(res, Is.Not.Null);
            Assert.That(res.Ticket, Is.Not.Null);
            Assert.That(res.Ticket.RequesterId, Is.EqualTo(Settings.CollaboratorId));

            Assert.That(api.Tickets.Delete(res.Ticket.Id.Value), Is.True);
        }

        [Test]
        public void CanCreateTicketWithDueDate()
        {
            //31 December 2020 2AM
            var dueAt = DateTimeOffset.Parse("12/31/2020 07:00:00 -05:00", CultureInfo.InvariantCulture);

            var ticket = new Ticket()
            {
                Subject = "ticket with due date",
                Comment = new Comment() { Body = "test comment" },
                Type = "task",
                Priority = TicketPriorities.Normal,
                DueAt = dueAt
            };

            var res = api.Tickets.CreateTicket(ticket).Ticket;

            Assert.That(res, Is.Not.Null);
            Assert.That(res.DueAt, Is.EqualTo(dueAt));

            Assert.That(api.Tickets.Delete(res.Id.Value), Is.True);
        }

        [Test]
        public void CanCreateTicketWithTicketFormId()
        {
            var ticket = new Ticket()
            {
                Subject = "ticket with ticket form id",
                Comment = new Comment() { Body = "testing requester" },
                Priority = TicketPriorities.Normal,
                TicketFormId = Settings.TicketFormId
            };

            var res = api.Tickets.CreateTicket(ticket).Ticket;

            Assert.That(res, Is.Not.Null);
            Assert.That(res.TicketFormId, Is.EqualTo(Settings.TicketFormId));

            Assert.That(api.Tickets.Delete(res.Id.Value), Is.True);
        }

        [Test]
        public void CanBulkUpdateTickets()
        {
            var t1 = api.Tickets.CreateTicket(new Ticket()
            {
                Subject = "testing bulk update",
                Comment = new Comment() { Body = "HELP" },
                Priority = TicketPriorities.Normal
            }).Ticket;
            var t2 = api.Tickets.CreateTicket(new Ticket()
            {
                Subject = "more testing for bulk update",
                Comment = new Comment() { Body = "Bulk UpdateTicket testing" },
                Priority = TicketPriorities.Normal
            }).Ticket;

            var res = api.Tickets.BulkUpdate(new List<long>() { t1.Id.Value, t2.Id.Value }, new BulkUpdate()
            {
                Status = TicketStatus.Solved,
                Comment = new Comment() { Public = true, Body = "check your email" },
                CollaboratorEmails = new List<string>() { Settings.ColloboratorEmail },
                AssigneeId = Settings.UserId
            });

            Assert.That(res.JobStatus.Status, Is.EqualTo("queued"));

            //also test JobStatuses while we have a job here
            var job = api.JobStatuses.GetJobStatus(res.JobStatus.Id);
            Assert.That(res.JobStatus.Id, Is.EqualTo(job.JobStatus.Id));

            Assert.That(api.Tickets.DeleteMultiple(new List<long>() { t1.Id.Value, t2.Id.Value }), Is.True);
        }

        [Test]
        public async Task CanAddAttachmentToTicketAsync()
        {
            var res = await api.Attachments.UploadAttachmentAsync(new ZenFile()
            {
                ContentType = "text/plain",
                FileName = "testupload.txt",
                FileData = File.ReadAllBytes(TestContext.CurrentContext.TestDirectory + "\\testupload.txt")
            });

            var ticket = new Ticket()
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

            var t1 = await api.Tickets.CreateTicketAsync(ticket);

            Assert.That(t1.Audit.Events.First().Attachments.Count, Is.EqualTo(1));

            Assert.That(await api.Tickets.DeleteAsync(t1.Ticket.Id.Value), Is.True);
        }

        [Test]
        public void CanAddAttachmentToTicket()
        {
            var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "testupload.txt");

            var res = api.Attachments.UploadAttachment(new ZenFile()
            {
                ContentType = "text/plain",
                FileName = "testupload.txt",
                FileData = File.ReadAllBytes(path)
            });

            var ticket = new Ticket()
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

            var t1 = api.Tickets.CreateTicket(ticket);
            Assert.That(t1.Audit.Events.First().Attachments.Count, Is.EqualTo(1));

            Assert.That(api.Tickets.Delete(t1.Ticket.Id.Value), Is.True);
            Assert.That(api.Attachments.DeleteUpload(res));
        }

        [Test]
        public void CanGetCollaborators()
        {
            var res = api.Tickets.GetCollaborators(Settings.SampleTicketId3);
            Assert.That(res.Users.Count, Is.GreaterThan(0));
        }

        [Test]
        public void CanGetIncidents()
        {
            var t1 = api.Tickets.CreateTicket(new Ticket()
            {
                Subject = "test problem",
                Comment = new Comment() { Body = "testing incidents with problems" },
                Priority = TicketPriorities.Normal,
                Type = TicketTypes.Problem
            }).Ticket;

            var t2 = api.Tickets.CreateTicket(new Ticket()
            {
                Subject = "incident",
                Comment = new Comment() { Body = "testing incidents" },
                Priority = TicketPriorities.Normal,
                Type = TicketTypes.Incident,
                ProblemId = t1.Id
            }).Ticket;

            var res = api.Tickets.GetIncidents(t1.Id.Value);
            Assert.That(res.Tickets.Count, Is.GreaterThan(0));

            Assert.That(api.Tickets.DeleteMultiple(new List<long>() { t1.Id.Value, t2.Id.Value }), Is.True);
        }

        [Test]
        public void CanGetProblems()
        {
            var t1 = api.Tickets.CreateTicket(new Ticket()
            {
                Subject = "test problem",
                Comment = new Comment() { Body = "testing incidents with problems" },
                Priority = TicketPriorities.Normal,
                Type = TicketTypes.Problem
            }).Ticket;

            var res = api.Tickets.GetProblems();
            Assert.That(res.Tickets.Count, Is.GreaterThan(0));

            Assert.That(api.Tickets.Delete(t1.Id.Value), Is.True);
        }

        //[Test]
        //public void CanGetIncrementalTicketExportPaged()
        //{
        //    Thread.Sleep(60000);
        //    const int maxTicketsPerPage = 1000;

        //    var res = api.Tickets.GetIncrementalTicketExport(DateTime.Now.AddDays(-365));

        //    Assert.AreEqual(maxTicketsPerPage, res.Tickets.Count);
        //    Assert.That(res.NextPage, Is.Not.Null.Or.Empty);
        //}

        //[Test]
        //public void CanGetIncrementalTicketExportWithUsersSideLoadPaged()
        //{
        //    Thread.Sleep(60000);
        //    const int maxTicketsPerPage = 1000;

        //    GroupTicketExportResponse res = api.Tickets.GetIncrementalTicketExport(DateTime.Now.AddDays(-365), TicketSideLoadOptionsEnum.Users);

        //    Assert.AreEqual(maxTicketsPerPage, res.Tickets.Count);
        //    Assert.IsTrue(res.Users.Count > 0);
        //    Assert.That(res.NextPage, Is.Not.Null.Or.Empty);

        //    res = api.Tickets.GetIncrementalTicketExportNextPage(res.NextPage);

        //    Assert.IsTrue(res.Tickets.Count > 0);
        //    Assert.IsTrue(res.Users.Count > 0);
        //}

        //[Test]
        //public void CanGetIncrementalTicketExportWithGroupsSideLoadPaged()
        //{
        //    Thread.Sleep(60000);

        //    const int maxTicketsPerPage = 1000;

        //    var res = api.Tickets.GetIncrementalTicketExport(DateTime.Now.AddDays(-700), TicketSideLoadOptionsEnum.Groups);

        //    Assert.AreEqual(maxTicketsPerPage, res.Tickets.Count);
        //    Assert.IsTrue(res.Groups.Count > 0);
        //    Assert.That(res.NextPage, Is.Not.Null.Or.Empty);

        //    res = api.Tickets.GetIncrementalTicketExportNextPage(res.NextPage);

        //    Assert.IsTrue(res.Tickets.Count > 0);
        //    Assert.IsTrue(res.Groups.Count > 0);
        //}

        //[Test]
        //public async Task CanGetIncrementalTicketExportAsyncWithSideLoadOptions()
        //{
        //    await Task.Delay(60000);
        //    var res = await api.Tickets.GetIncrementalTicketExportAsync(DateTime.Now.AddDays(-31), TicketSideLoadOptionsEnum.Users);

        //    Assert.That(res.Count, Is.GreaterThan(0));
        //    Assert.That(res.Users, Is.Not.Null);
        //}

        [Test]
        public void CanGetTicketFields()
        {
            var res = api.Tickets.GetTicketFields();
            Assert.That(res.TicketFields.Count, Is.GreaterThan(0));
        }

        [Test]
        public void CanGetTicketFieldById()
        {
            var id = Settings.CustomFieldId;
            var ticketField = api.Tickets.GetTicketFieldById(id).TicketField;
            Assert.That(ticketField, Is.Not.Null);
            Assert.That(id, Is.EqualTo(ticketField.Id));
        }

        [Test]
        public void CanGetTicketFieldByIdAsync()
        {
            var id = Settings.CustomFieldId;
            var ticketField = api.Tickets.GetTicketFieldByIdAsync(id).Result.TicketField;
            Assert.That(ticketField, Is.Not.Null);
            Assert.That(id, Is.EqualTo(ticketField.Id));
        }

        [Test]
        public void CanCreateUpdateAndDeleteTicketFields()
        {
            var tField = new TicketField()
            {
                Type = TicketFieldTypes.Text,
                Title = "MyField",
            };

            var res = api.Tickets.CreateTicketField(tField);
            Assert.That(res.TicketField, Is.Not.Null);

            var updatedTF = res.TicketField;
            updatedTF.Title = "My Custom Field";

            var updatedRes = api.Tickets.UpdateTicketField(updatedTF);
            Assert.That(updatedTF.Title, Is.EqualTo(updatedRes.TicketField.Title));

            Assert.That(api.Tickets.DeleteTicketField(updatedTF.Id.Value), Is.True);
        }

        [TestCase(true, "test entry", "test_entry")]
        [TestCase(false, "test entry", "test entry")]
        public void CanCreateAndDeleteTaggerTicketField(bool replaceNameSpaceWithUnderscore, string name, string expectedName)
        {
            var tField = new TicketField()
            {
                Type = TicketFieldTypes.Tagger,
                Title = "My Tagger",
                Description = "test description",
                TitleInPortal = "Test Tagger",
                CustomFieldOptions = new List<CustomFieldOptions>()
            };

            tField.CustomFieldOptions.Add(new CustomFieldOptions()
            {
                Name = name,
                Value = "test value"
            });

            var res = api.Tickets.CreateTicketField(tField, replaceNameSpaceWithUnderscore);
            Assert.That(res.TicketField, Is.Not.Null);
            Assert.That(expectedName, Is.EqualTo(res.TicketField.CustomFieldOptions[0].Name));

            Assert.That(api.Tickets.DeleteTicketField(res.TicketField.Id.Value), Is.True);
        }

        [TestCase(true, "test entryA", "test entryA newTitle", "test entryB", "test entryC", "test_entryA", "test_entryA_newTitle", "test_entryB", "test_entryC")]
        [TestCase(false, "test entryA", "test entryA newTitle", "test entryB", "test entryC", "test entryA", "test entryA newTitle", "test entryB", "test entryC")]
        public void CanCreateUpdateOptionsAndDeleteTaggerTicketField(bool replaceNameSpaceWithUnderscore, string name1, string name1_Update, string name2, string name3,
            string expectedName1, string expectedName1_Update, string expectedName2, string expectedName3)
        {
            // https://support.zendesk.com/hc/en-us/articles/204579973--BREAKING-Update-ticket-field-dropdown-fields-by-value-instead-of-id-

            var option1 = "test_value_a";
            var option1_Update = "test value_a_newtag";
            var option2 = "test_value_b";
            var option3 = "test_value_c";

            var tField = new TicketField()
            {
                Type = TicketFieldTypes.Tagger,
                Title = "My Tagger 2",
                Description = "test description",
                TitleInPortal = "Test Tagger",
                CustomFieldOptions = new List<CustomFieldOptions>()
            };

            tField.CustomFieldOptions.Add(new CustomFieldOptions()
            {
                Name = name1,
                Value = option1
            });

            tField.CustomFieldOptions.Add(new CustomFieldOptions()
            {
                Name = name2,
                Value = option2
            });

            var res = api.Tickets.CreateTicketField(tField, replaceNameSpaceWithUnderscore);
            Assert.That(res.TicketField, Is.Not.Null);
            Assert.That(res.TicketField.Id, Is.Not.Null);
            Assert.That(res.TicketField.CustomFieldOptions.Count, Is.EqualTo(2));
            Assert.That(res.TicketField.CustomFieldOptions[0].Value, Is.EqualTo(option1));
            Assert.That(res.TicketField.CustomFieldOptions[1].Value, Is.EqualTo(option2));
            Assert.That(res.TicketField.CustomFieldOptions[0].Name, Is.EqualTo(expectedName1));
            Assert.That(res.TicketField.CustomFieldOptions[1].Name, Is.EqualTo(expectedName2));

            var id = res.TicketField.Id.Value;

            var tFieldU = new TicketField()
            {
                Id = id,
                CustomFieldOptions = new List<CustomFieldOptions>()
            };

            //update CustomFieldOption A
            tFieldU.CustomFieldOptions.Add(new CustomFieldOptions()
            {
                Name = name1_Update,
                Value = option1_Update
            });
            //delete CustomFieldOption B
            //add CustomFieldOption C
            tFieldU.CustomFieldOptions.Add(new CustomFieldOptions()
            {
                Name = name3,
                Value = option3
            });

            var resU = api.Tickets.UpdateTicketField(tFieldU, replaceNameSpaceWithUnderscore);

            Assert.That(resU.TicketField.CustomFieldOptions.Count, Is.EqualTo(2));
            Assert.That(resU.TicketField.CustomFieldOptions[0].Value, Is.EqualTo(option1_Update.Replace(" ", "_")));
            Assert.That(resU.TicketField.CustomFieldOptions[1].Value, Is.EqualTo(option3));
            Assert.That(resU.TicketField.CustomFieldOptions[0].Name, Is.EqualTo(expectedName1_Update));
            Assert.That(resU.TicketField.CustomFieldOptions[1].Name, Is.EqualTo(expectedName3));

            Assert.That(api.Tickets.DeleteTicketField(id), Is.True);
        }

        [Test]
        [Ignore("This test requires an email be sent. A this time that is not automated.")]
        public void CanGetSuspendedTickets()
        {
            var all = api.Tickets.GetSuspendedTickets();
            Assert.That(all.Count, Is.GreaterThan(0));

            var ind = api.Tickets.GetSuspendedTicketById(all.SuspendedTickets[0].Id);
            Assert.That(all.SuspendedTickets[0].Id, Is.EqualTo(ind.SuspendedTicket.Id));
        }

        [Test]
        public void CanGetTicketForms()
        {
            var res = api.Tickets.GetTicketForms();
            Assert.That(res.Count, Is.GreaterThan(0));
        }

        [Test]
        public void CanCreateUpdateAndDeleteTicketForms()
        {
            var res = api.Tickets.CreateTicketForm(new TicketForm()
            {
                Name = "Snowboard Problem",
                EndUserVisible = true,
                DisplayName = "Snowboard Damage",
                Position = 2,
                Active = true,
                Default = false
            });

            Assert.That(res, Is.Not.Null);
            Assert.That(res.TicketForm.Id, Is.GreaterThan(0));

            var get = api.Tickets.GetTicketFormById(res.TicketForm.Id.Value);
            Assert.That(res.TicketForm.Id, Is.EqualTo(get.TicketForm.Id));

            res.TicketForm.Name = "Snowboard Fixed";
            res.TicketForm.DisplayName = "Snowboard has been fixed";
            res.TicketForm.Active = false;

            var update = api.Tickets.UpdateTicketForm(res.TicketForm);
            Assert.That(res.TicketForm.Name, Is.EqualTo(update.TicketForm.Name));

            Assert.That(api.Tickets.DeleteTicketForm(res.TicketForm.Id.Value), Is.True);
        }

        [Test]
        public void CanGetAllTicketMetrics()
        {
            var metrics = api.Tickets.GetAllTicketMetrics();
            Assert.That(metrics.Count, Is.GreaterThan(0));
            var count = 50;
            var nextPage = api.Tickets.GetByPageUrl<GroupTicketMetricResponse>(metrics.NextPage, count);
            Assert.That(count, Is.EqualTo(nextPage.TicketMetrics.Count));
        }

        [Test]
        public void CanGetTicketMetricsAsync()
        {
            var tickets = api.Tickets.GetAllTicketMetricsAsync();
            Assert.That(tickets.Result.Count, Is.GreaterThan(0));
        }

        [Test]
        public void CanGetTicketMetricByTicketId()
        {
            var id = Settings.SampleTicketId;
            var metric = api.Tickets.GetTicketMetricsForTicket(id).TicketMetric;
            Assert.That(metric, Is.Not.Null);
            Assert.That(id, Is.EqualTo(metric.TicketId));
        }

        [Test]
        public void CanGetTicketMetricByTicketIdAsync()
        {
            var id = Settings.SampleTicketId;
            var metric = api.Tickets.GetTicketMetricsForTicketAsync(id).Result.TicketMetric;
            Assert.That(metric, Is.Not.Null);
            Assert.That(id, Is.EqualTo(metric.TicketId));
        }

        [Test]
        public void CanGetAllTicketsWithSideLoad()
        {
            var tickets =
                api.Tickets.GetAllTickets(sideLoadOptions: ticketSideLoadOptions);

            Assert.That(tickets.Users.Any(), Is.True);
            Assert.That(tickets.Organizations.Any(), Is.True);
        }

        [Test]
        public void CanGetAllTicketsAsyncWithSideLoad()
        {
            var tickets =
                api.Tickets.GetAllTicketsAsync(sideLoadOptions: ticketSideLoadOptions);

            Assert.That(tickets.Result.Users.Any(), Is.True);
            Assert.That(tickets.Result.Organizations.Any(), Is.True);
            Assert.That(tickets.Result.Tickets.Count, Is.EqualTo(tickets.Result.Tickets.Where(t => t.CommentCount.HasValue).Count()));
        }

        [Test]
        public void CanGetTicketsByOrganizationIDAsyncWithSideLoad()
        {
            var id = Settings.OrganizationId;
            var tickets = api.Tickets.GetTicketsByOrganizationIDAsync(id, sideLoadOptions: ticketSideLoadOptions);
            Assert.That(tickets.Result.Count, Is.GreaterThan(0));
            Assert.That(tickets.Result.Users.Any(), Is.True);
            Assert.That(tickets.Result.Organizations.Any(), Is.True);
        }

        [Test]
        public void CanCanGetTicketsByOrganizationIDWithSideLoad()
        {
            var id = Settings.OrganizationId;
            var tickets = api.Tickets.GetTicketsByOrganizationID(id, sideLoadOptions: ticketSideLoadOptions);
            Assert.That(tickets.Count, Is.GreaterThan(0));
            Assert.That(tickets.Users.Any(), Is.True);
            Assert.That(tickets.Organizations.Any(), Is.True);
        }

        [Test]
        public void CanImportTicket()
        {
            var ticket = new TicketImport()
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

            var res = api.Tickets.ImportTicket(ticket).Ticket;

            Assert.That(res, Is.Not.Null);
            Assert.That(res.Id.HasValue, Is.True);
            Assert.That(res.Id.Value, Is.GreaterThan(0));
            Assert.That(res.CreatedAt.Value.LocalDateTime, Is.LessThan(DateTime.Now.AddDays(-4)));
            Assert.That(res.UpdatedAt.Value.LocalDateTime, Is.GreaterThan(res.CreatedAt.Value.LocalDateTime));
            Assert.That(res.Status, Is.EqualTo(TicketStatus.Solved));
            Assert.That(res.Description, Is.EqualTo("test description"));

            var resComments = api.Tickets.GetTicketComments(res.Id.Value);
            Assert.That(resComments, Is.Not.Null);
            Assert.That(resComments.Count, Is.EqualTo(3));

            api.Tickets.DeleteAsync(res.Id.Value);
        }

        [Test]
        public void CanImportTicketAsync()
        {
            var ticket = new TicketImport()
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

            var res = api.Tickets.ImportTicketAsync(ticket);

            Assert.That(res.Result, Is.Not.Null);
            Assert.That(res.Result.Ticket.Id.Value, Is.GreaterThan(0));
            Assert.That(res.Result.Ticket.CreatedAt.Value.LocalDateTime, Is.LessThan(DateTime.Now.AddDays(-4)));
            Assert.That(res.Result.Ticket.UpdatedAt.Value.LocalDateTime, Is.GreaterThan(res.Result.Ticket.CreatedAt.Value.LocalDateTime));
            Assert.That(res.Result.Ticket.Status, Is.EqualTo(TicketStatus.Solved));
            Assert.That(res.Result.Ticket.Description, Is.EqualTo("test description"));

            var resComments = api.Tickets.GetTicketComments(res.Result.Ticket.Id.Value);
            Assert.That(resComments, Is.Not.Null);
            Assert.That(resComments.Count, Is.EqualTo(3));

            api.Tickets.DeleteAsync(res.Id);
        }

        [Test]
        public void CanMergeTickets()
        {
            var sourceDescription = new List<string> { "This is a source ticket 1", "This is a source ticket 2" };
            var targetDescription = "This is a the target ticket";

            var sourceTicket1 = new Ticket
            {
                Subject = "Source Ticket 1",
                Comment = new Comment { Body = sourceDescription[0], Public = true, }
            };
            var sourceTicket2 = new Ticket
            {
                Subject = "Source Ticket 2",
                Comment = new Comment { Body = sourceDescription[1], Public=true, }
            };
            var targetTicket = new Ticket
            {
                Subject = "Target Ticket",
                Comment = new Comment { Body = targetDescription, Public = true, }
            };

            var mergeIds = new List<long>();

            var tick = api.Tickets.CreateTicket(sourceTicket1);
            mergeIds.Add(tick.Ticket.Id.Value);
            tick = api.Tickets.CreateTicket(sourceTicket2);
            mergeIds.Add(tick.Ticket.Id.Value);
            tick = api.Tickets.CreateTicket(targetTicket);
            var targetTicketId = tick.Ticket.Id.Value;

            var targetMergeComment =
                $"Merged with ticket(s) {string.Join(", ", mergeIds.Select(m => $"#{m}").ToArray())}";
            var sourceMergeComment = $"Closing in favor of #{targetTicketId}";

            var res = api.Tickets.MergeTickets(
                targetTicketId, 
                mergeIds, 
                targetMergeComment, 
                sourceMergeComment,
                true,
                true);

            Assert.That(res.JobStatus.Status, Is.EqualTo("queued"));

            do
            {
                Thread.Sleep(5000);
                var job = api.JobStatuses.GetJobStatus(res.JobStatus.Id);
                Assert.That(job.JobStatus.Id, Is.EqualTo(res.JobStatus.Id));

                if (job.JobStatus.Status == "completed") break;
            } while (true);

            var counter = 0;
            foreach (var id in mergeIds)
            {
                var oldTicket = api.Tickets.GetTicket(id);
                Assert.That(oldTicket.Ticket.Id.Value, Is.EqualTo(id));
                Assert.That(oldTicket.Ticket.Status, Is.EqualTo("closed"));

                var oldComments = api.Tickets.GetTicketComments(id);
                Assert.That(oldComments.Comments.Count, Is.EqualTo(2));
                Assert.That(oldComments.Comments[0].Body, Is.EqualTo(sourceDescription[counter]));
                Assert.That(oldComments.Comments[1].Body, Is.EqualTo(sourceMergeComment));

                api.Tickets.DeleteAsync(id);
                counter++;
            }

            var ticket = api.Tickets.GetTicket(targetTicketId);
            Assert.That(ticket.Ticket.Id.Value, Is.EqualTo(targetTicketId));

            var comments = api.Tickets.GetTicketComments(targetTicketId);
            Assert.That(comments.Comments.Count, Is.EqualTo(2));
            Assert.That(comments.Comments[0].Body, Is.EqualTo(targetDescription));
            Assert.That(comments.Comments[1].Body, Is.EqualTo(targetMergeComment));

            api.Tickets.DeleteAsync(targetTicketId);
        }

        [Test]
        public async Task CanMergeTicketsAsync()
        {
            var sourceDescription = new List<string> { "This is a source ticket 1", "This is a source ticket 2" };
            var targetDescription = "This is a the target ticket";

            var sourceTicket1 = new Ticket
            {
                Subject = "Source Ticket 1",
                Comment = new Comment { Body = sourceDescription[0], Public = true, }
            };
            var sourceTicket2 = new Ticket
            {
                Subject = "Source Ticket 2",
                Comment = new Comment { Body = sourceDescription[1], Public = true, }
            };
            var targetTicket = new Ticket
            {
                Subject = "Target Ticket",
                Comment = new Comment { Body = targetDescription, Public = true, }
            };

            var mergeIds = new List<long>();

            var tick = await api.Tickets.CreateTicketAsync(sourceTicket1);
            mergeIds.Add(tick.Ticket.Id.Value);
            tick = await api.Tickets.CreateTicketAsync(sourceTicket2);
            mergeIds.Add(tick.Ticket.Id.Value);
            tick = await api.Tickets.CreateTicketAsync(targetTicket);
            var targetTicketId = tick.Ticket.Id.Value;

            var targetMergeComment =
                $"Merged with ticket(s) {string.Join(", ", mergeIds.Select(m => $"#{m}").ToArray())}";
            var sourceMergeComment = $"Closing in favor of #{targetTicketId}";

            var res = await api.Tickets.MergeTicketsAsync(
                targetTicketId,
                mergeIds,
                targetMergeComment,
                sourceMergeComment);

            Assert.That(res.JobStatus.Status, Is.EqualTo("queued"));

            do
            {
                await Task.Delay(5000);
                var job = await api.JobStatuses.GetJobStatusAsync(res.JobStatus.Id);
                Assert.That(job.JobStatus.Id, Is.EqualTo(res.JobStatus.Id));

                if (job.JobStatus.Status == "completed") break;
            } while (true);

            var counter = 0;
            foreach (var id in mergeIds)
            {
                var oldTicket = await api.Tickets.GetTicketAsync(id);
                Assert.That(oldTicket.Ticket.Id.Value, Is.EqualTo(id));
                Assert.That(oldTicket.Ticket.Status, Is.EqualTo("closed"));

                var oldComments = await api.Tickets.GetTicketCommentsAsync(id);
                Assert.That(oldComments.Comments.Count, Is.EqualTo(2));
                Assert.That(oldComments.Comments[0].Body, Is.EqualTo(sourceDescription[counter]));
                Assert.That(oldComments.Comments[1].Body, Is.EqualTo(sourceMergeComment));

                await api.Tickets.DeleteAsync(id);
                counter++;
            }

            var ticket = await api.Tickets.GetTicketAsync(targetTicketId);
            Assert.That(ticket.Ticket.Id.Value, Is.EqualTo(targetTicketId));

            var comments = await api.Tickets.GetTicketCommentsAsync(targetTicketId);
            Assert.That(comments.Comments.Count, Is.EqualTo(2));
            Assert.That(comments.Comments[0].Body, Is.EqualTo(targetDescription));
            Assert.That(comments.Comments[1].Body, Is.EqualTo(targetMergeComment));

            await api.Tickets.DeleteAsync(targetTicketId);
        }

        [Test]
        public void CanBulkImportTicket()
        {
            var test = new List<TicketImport>();

            for (var x = 0; x < 2; x++)
            {
                var ticket = new TicketImport()
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

            var res = api.Tickets.BulkImportTickets(test);

            Assert.That(res.JobStatus.Status, Is.EqualTo("queued"));

            var job = api.JobStatuses.GetJobStatus(res.JobStatus.Id);
            Assert.That(res.JobStatus.Id, Is.EqualTo(job.JobStatus.Id));

            var count = 0;
            while (job.JobStatus.Status.ToLower() != "completed" && count < 10)
            {
                Thread.Sleep(1000);
                job = api.JobStatuses.GetJobStatus(res.JobStatus.Id);
                count++;
            }

            Assert.That(job.JobStatus.Status.ToLower(), Is.EqualTo("completed"));

            foreach (var r in job.JobStatus.Results)
            {
                var ticket = api.Tickets.GetTicket(r.Id).Ticket;
                Assert.That(ticket.Description, Is.EqualTo("test description"));
                var resComments = api.Tickets.GetTicketComments(r.Id);
                Assert.That(resComments, Is.Not.Null);
                Assert.That(resComments.Count, Is.EqualTo(3));
                foreach (var c in resComments.Comments)
                {
                    Assert.That(c.CreatedAt.HasValue, Is.True);
                    Assert.That(c.CreatedAt.Value.LocalDateTime, Is.LessThan(DateTime.Now.AddDays(-1)));
                }

                api.Tickets.DeleteAsync(r.Id);
            }
        }

        [Test]
        public void CanCreateTicketWithPrivateComment()
        {
            var ticket = new Ticket { Comment = new Comment { Body = "This is a Test", Public = false } };

            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DateParseHandling = DateParseHandling.DateTimeOffset,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
                ContractResolver = ZendeskApi_v2.Serialization.ZendeskContractResolver.Instance
            };

            var json = JsonConvert.SerializeObject(ticket, Formatting.None, jsonSettings);
            Assert.That(json, Contains.Substring("false"));
        }

        [Test]
        public async Task ViaChannel_Set_To_API_Issue_254()
        {
            // see https://github.com/mozts2005/ZendeskApi_v2/issues/254

            var initCommentBody = "HELP";
            var secondCommentBody = "New comment";

            var ticket = new Ticket()
            {
                Subject = "my printer is on fire",
                Comment = new Comment() { Body = initCommentBody },
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

            var resp = await api.Tickets.CreateTicketAsync(ticket);
            var newTicket = resp.Ticket;

            Assert.That(newTicket.Via.Channel, Is.EqualTo("api"));

            var comment = new Comment { Body = secondCommentBody, Public = true };

            var resp2 = await api.Tickets.UpdateTicketAsync(newTicket, comment);
            var resp3 = await api.Tickets.GetTicketCommentsAsync(newTicket.Id.Value);
            var resp4 = await api.Tickets.GetTicketCommentsAsync(newTicket.Id.Value, false);

            Assert.That(resp3.Comments.Any(c => c.Via?.Channel != "api"), Is.False);
            Assert.That(resp3.Comments[0].Body, Is.EqualTo(initCommentBody));
            Assert.That(resp4.Comments[0].Body, Is.EqualTo(secondCommentBody));

            // clean up
            await api.Tickets.DeleteAsync(newTicket.Id.Value);
        }

        [Test]
        public async Task TicketField()
        {
            var tField = new TicketField
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

            var res = await api.Tickets.CreateTicketFieldAsync(tField);
            Assert.That(res.TicketField, Is.Not.Null);
            Assert.That(res.TicketField.Id, Is.Not.Null);
            Assert.That(res.TicketField.CustomFieldOptions.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task CanCreateUpdateOptionsAndDeleteTaggerTicketFieldAsync()
        {
            var option1 = "test_value_a";
            var option1_Update = "test_value_a_newtag";
            var option2 = "test_value_b";
            var option3 = "test_value_c";

            var tField = new TicketField()
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

            var res = await api.Tickets.CreateTicketFieldAsync(tField);
            Assert.That(res.TicketField, Is.Not.Null);
            Assert.That(res.TicketField.Id, Is.Not.Null);
            Assert.That(res.TicketField.CustomFieldOptions.Count, Is.EqualTo(2));
            Assert.That(res.TicketField.CustomFieldOptions[0].Value, Is.EqualTo(option1));
            Assert.That(res.TicketField.CustomFieldOptions[1].Value, Is.EqualTo(option2));

            var id = res.TicketField.Id.Value;

            var tFieldU = new TicketField()
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

            var resU = await api.Tickets.UpdateTicketFieldAsync(tFieldU);

            Assert.That(resU.TicketField.CustomFieldOptions.Count, Is.EqualTo(2));
            Assert.That(resU.TicketField.CustomFieldOptions[0].Value, Is.EqualTo(option1_Update));
            Assert.That(resU.TicketField.CustomFieldOptions[1].Value, Is.Not.EqualTo(option2));

            Assert.That(await api.Tickets.DeleteTicketFieldAsync(id), Is.True);
        }

        [Test]
        public async Task CanGetBrandId()
        {
            var respBrand = api.Brands.GetBrands();
            var brand = respBrand.Brands[0];
            var ticket = new Ticket { Comment = new Comment { Body = "This is a Brand id Test", Public = false }, BrandId = brand.Id };
            var respTicket = await api.Tickets.CreateTicketAsync(ticket);

            Assert.That(respTicket.Ticket.BrandId, Is.EqualTo(brand.Id));

            // clean up
            Assert.That(await api.Tickets.DeleteAsync(respTicket.Ticket.Id.Value), Is.True);
        }

        [Test]
        public async Task CanGetIsPublicAsync()
        {
            var ticket = new Ticket()
            {
                Subject = "my printer is on fire",
                Comment = new Comment { Body = "HELP", Public = true },
                Priority = TicketPriorities.Urgent
            };

            var resp1 = await api.Tickets.CreateTicketAsync(ticket);
            Assert.That(resp1.Ticket.IsPublic, Is.True);

            ticket.Comment.Public = false;
            var resp2 = await api.Tickets.CreateTicketAsync(ticket);

            Assert.That(resp2.Ticket.IsPublic, Is.False);

            Assert.That(await api.Tickets.DeleteAsync(resp1.Ticket.Id.Value), Is.True);
            Assert.That(await api.Tickets.DeleteAsync(resp2.Ticket.Id.Value), Is.True);
        }

        [Test]
        public async Task CanGetSystemFieldOptions()
        {
            var resp = await api.Tickets.GetTicketFieldByIdAsync(21830872);

            Assert.That(resp.TicketField.SystemFieldOptions, Is.Not.Null);
        }

        [Test]
        public async Task CanSetFollowupID()
        {
            var ticket = new Ticket { Comment = new Comment { Body = "This is a Test", Public = false } };

            var resp1 = await api.Tickets.CreateTicketAsync(ticket);

            var closedTicket = resp1.Ticket;

            closedTicket.Status = TicketStatus.Closed;

            await api.Tickets.UpdateTicketAsync(closedTicket, new Comment { Body = "Closing Ticket" });

            var ticket_Followup = new Ticket()
            {
                Subject = "This is a Test Follow up",
                Comment = new Comment { Body = "HELP", Public = true },
                Priority = TicketPriorities.Urgent,
                ViaFollowupSourceId = closedTicket.Id.Value
            };

            var resp3 = await api.Tickets.CreateTicketAsync(ticket_Followup);

            Assert.That(resp3.Ticket.Via.Source.Rel, Is.EqualTo("follow_up"));

            Assert.That(await api.Tickets.DeleteAsync(resp3.Ticket.Id.Value), Is.True);
            Assert.That(await api.Tickets.DeleteAsync(closedTicket.Id.Value), Is.True);
        }

        [Test]
        public void CanCreateManyTickets()
        {
            var comment = "testing create Many";

            var tickets = new List<Ticket> {
                new Ticket{ Subject = "ticket Test number 1", Comment = new Comment() { Body = comment  }, Priority = TicketPriorities.Normal},
                new Ticket{ Subject ="ticket Test number 2", Comment = new Comment{ Body = comment  }, Priority = TicketPriorities.Normal }
                };

            var res = api.Tickets.CreateManyTickets(tickets);
            Assert.That(res.JobStatus.Status, Is.EqualTo("queued"));

            var job = api.JobStatuses.GetJobStatus(res.JobStatus.Id);
            Assert.That(job.JobStatus.Id, Is.EqualTo(res.JobStatus.Id));

            var count = 0;
            while (job.JobStatus.Status.ToLower() != "completed" && count < 10)
            {
                Thread.Sleep(1000);
                job = api.JobStatuses.GetJobStatus(res.JobStatus.Id);
                count++;
            }

            Assert.That(job.JobStatus.Status.ToLower(), Is.EqualTo("completed"));

            foreach (var r in job.JobStatus.Results)
            {
                var ticket = api.Tickets.GetTicket(r.Id).Ticket;
                Assert.That(ticket.Description, Is.EqualTo(comment));
                api.Tickets.Delete(r.Id);
            }
        }

        [Test]
        public async Task CanCreateManyTicketsAsync()
        {
            var comment = "testing create Many";

            var tickets = new List<Ticket> {
                new Ticket{ Subject = "ticket Test number 1", Comment = new Comment() { Body = comment  }, Priority = TicketPriorities.Normal},
                new Ticket{ Subject ="ticket Test number 2", Comment = new Comment{ Body = comment  }, Priority = TicketPriorities.Normal }
                };

            var res = await api.Tickets.CreateManyTicketsAsync(tickets);
            Assert.That(res.JobStatus.Status, Is.EqualTo("queued"));

            var job = await api.JobStatuses.GetJobStatusAsync(res.JobStatus.Id);
            Assert.That(job.JobStatus.Id, Is.EqualTo(res.JobStatus.Id));

            var count = 0;
            while (job.JobStatus.Status.ToLower() != "completed" && count < 10)
            {
                await Task.Delay(1000);
                job = await api.JobStatuses.GetJobStatusAsync(res.JobStatus.Id);
                count++;
            }

            Assert.That(job.JobStatus.Status.ToLower(), Is.EqualTo("completed"));

            foreach (var r in job.JobStatus.Results)
            {
                var ticket = (await api.Tickets.GetTicketAsync(r.Id)).Ticket;
                Assert.That(ticket.Description, Is.EqualTo(comment));
                await api.Tickets.DeleteAsync(r.Id);
            }
        }

    }
}
