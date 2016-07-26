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
        ZendeskApi api = new ZendeskApi(Settings.Site, Settings.Email, Settings.Password);
        TicketSideLoadOptionsEnum ticketSideLoadOptions = TicketSideLoadOptionsEnum.Users | TicketSideLoadOptionsEnum.Organizations | TicketSideLoadOptionsEnum.Groups;

        [Test]
        public void CanGetTicketsAsync()
        {
            var tickets = api.Tickets.GetAllTicketsAsync();
            Assert.True(tickets.Result.Count > 0);
        }

        [Test]
        public void CanGetTicketsAsyncWithSideLoad()
        {
            var tickets = api.Tickets.GetAllTicketsAsync(sideLoadOptions: ticketSideLoadOptions);
            Assert.True(tickets.Result.Count > 0);
            Assert.IsTrue(tickets.Result.Users.Any());
            Assert.IsTrue(tickets.Result.Organizations.Any());
        }

        [Test]
        public void CanCanGetTicketsByOrganizationIDAsync()
        {
            var id = Settings.OrganizationId;
            var tickets = api.Tickets.GetTicketsByOrganizationIDAsync(id);
            Assert.True(tickets.Result.Count > 0);
        }

        [Test]
        public void CanGetTickets()
        {
            var tickets = api.Tickets.GetAllTickets();
            Assert.True(tickets.Count > 0);

            var count = 50;
            var nextPage = api.Tickets.GetByPageUrl<GroupTicketResponse>(tickets.NextPage, count);
            Assert.AreEqual(nextPage.Tickets.Count, count);

            var ticketsByUser = api.Tickets.GetTicketsByUserID(tickets.Tickets[0].RequesterId.Value);
            Assert.True(ticketsByUser.Count > 0);
        }

        [Test]
        public void CanGetTicketsWithSideLoad()
        {
            var tickets = api.Tickets.GetAllTickets(sideLoadOptions: ticketSideLoadOptions);
            Assert.True(tickets.Count > 0);
            Assert.IsTrue(tickets.Users.Any());
            Assert.IsTrue(tickets.Organizations.Any());
        }

        [Test]
        public void CanGetTicketsPaged()
        {
            const int count = 50;
            var tickets = api.Tickets.GetAllTickets(count);

            Assert.AreEqual(count, tickets.Tickets.Count);  // 50
            Assert.AreNotEqual(tickets.Count, tickets.Tickets.Count);   // 50 != total count of tickets (assumption)

            const int page = 3;
            var thirdPage = api.Tickets.GetAllTickets(count, page);

            Assert.AreEqual(count, thirdPage.Tickets.Count);

            var nextPage = thirdPage.NextPage.GetQueryStringDict()
                    .Where(x => x.Key == "page")
                        .Select(x => x.Value)
                        .FirstOrDefault();

            Assert.NotNull(nextPage);

            Assert.AreEqual(nextPage, (page + 1).ToString());
        }


        [Test]
        public void CanGetTicketById()
        {
            var id = Settings.SampleTicketId;
            var ticket = api.Tickets.GetTicket(id).Ticket;
            Assert.NotNull(ticket);
            Assert.AreEqual(ticket.Id, id);
        }

        [Test]
        public void CanGetTicketByIdWithSideLoad()
        {
            var id = Settings.SampleTicketId;
            var ticket = api.Tickets.GetTicket(id, sideLoadOptions: ticketSideLoadOptions);
            Assert.NotNull(ticket);
            Assert.NotNull(ticket.Ticket);
            Assert.AreEqual(ticket.Ticket.Id, id);
            Assert.IsTrue(ticket.Users.Any());
            Assert.IsTrue(ticket.Organizations.Any());
        }

        [Test]
        public void CanGetTicketsByOrganizationId()
        {
            var id = Settings.OrganizationId;
            var tickets = api.Tickets.GetTicketsByOrganizationID(id);
            Assert.True(tickets.Count > 0);
        }


        [Test]
        public void CanGetTicketsByOrganizationIdPaged()
        {
            var id = Settings.OrganizationId;
            var ticketsRes = api.Tickets.GetTicketsByOrganizationID(id, 2, 3);

            Assert.AreEqual(3, ticketsRes.PageSize);
            Assert.AreEqual(3, ticketsRes.Tickets.Count);
            Assert.Greater(ticketsRes.Count, 0);

            var nextPage = ticketsRes.NextPage.GetQueryStringDict()
                    .Where(x => x.Key == "page")
                        .Select(x => x.Value)
                        .FirstOrDefault();

            Assert.NotNull(nextPage);

            Assert.AreEqual("3", nextPage);
        }

        [Test]
        public void CanGetTicketsByViewIdPaged()
        {
            var ticketsRes = api.Tickets.GetTicketsByViewID(Settings.ViewId, 10, 2);

            Assert.AreEqual(10, ticketsRes.PageSize);
            Assert.AreEqual(10, ticketsRes.Tickets.Count);
            Assert.Greater(ticketsRes.Count, 0);

            var nextPage = ticketsRes.NextPage.GetQueryStringDict()
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
            var ticketsRes = api.Tickets.GetTicketsByViewID(Settings.ViewId, 10, 2, sideLoadOptions: ticketSideLoadOptions);

            Assert.IsTrue(ticketsRes.Users.Any());
            Assert.IsTrue(ticketsRes.Users.Any());
        }

        [Test]
        public void CanTicketsByUserIdPaged()
        {
            var ticketsRes = api.Tickets.GetTicketsByUserID(Settings.UserId, 5, 2);

            Assert.AreEqual(5, ticketsRes.PageSize);
            Assert.AreEqual(5, ticketsRes.Tickets.Count);
            Assert.Greater(ticketsRes.Count, 0);

            var nextPage = ticketsRes.NextPage.GetQueryStringDict()
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
            var ticketsRes = api.Tickets.GetTicketsByUserID(Settings.UserId, 5, 2, sideLoadOptions: ticketSideLoadOptions);
            Assert.IsTrue(ticketsRes.Users.Any());
            Assert.IsTrue(ticketsRes.Organizations.Any());
        }

        [Test]
        public void CanTicketsByUserIdPagedAsyncWithSideLoad()
        {
            var ticketsRes = api.Tickets.GetTicketsByUserIDAsync(Settings.UserId, 5, 2, sideLoadOptions: ticketSideLoadOptions);
            Assert.IsTrue(ticketsRes.Result.Users.Any());
            Assert.IsTrue(ticketsRes.Result.Organizations.Any());
        }

        [Test]
        public void CanAssignedTicketsByUserIdPaged()
        {
            var ticketsRes = api.Tickets.GetAssignedTicketsByUserID(Settings.UserId, 5, 2);

            Assert.AreEqual(5, ticketsRes.PageSize);
            Assert.AreEqual(5, ticketsRes.Tickets.Count);
            Assert.Greater(ticketsRes.Count, 0);

            var nextPage = ticketsRes.NextPage.GetQueryStringDict()
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
            var ticketsRes = api.Tickets.GetAssignedTicketsByUserID(Settings.UserId, 5, 2, sideLoadOptions: ticketSideLoadOptions);
            Assert.IsTrue(ticketsRes.Users.Any());
            Assert.IsTrue(ticketsRes.Organizations.Any());
        }

        [Test]
        public void CanAssignedTicketsByUserIdPagedAsyncWithSideLoad()
        {
            var ticketsRes = api.Tickets.GetAssignedTicketsByUserIDAsync(Settings.UserId, 5, 2, sideLoadOptions: ticketSideLoadOptions);
            Assert.IsTrue(ticketsRes.Result.Users.Any());
            Assert.IsTrue(ticketsRes.Result.Organizations.Any());
        }

        [Test]
        public void CanGetMultipleTickets()
        {
            var ids = new List<long>() { Settings.SampleTicketId, Settings.SampleTicketId2 };
            var tickets = api.Tickets.GetMultipleTickets(ids);
            Assert.NotNull(tickets);
            Assert.AreEqual(tickets.Count, ids.Count);
        }

        [Test]
        public async Task CanGetMultipleTicketsAsync()
        {
            var ids = new List<long>() { Settings.SampleTicketId, Settings.SampleTicketId2 };
            var tickets = await api.Tickets.GetMultipleTicketsAsync(ids);
            Assert.NotNull(tickets);
            Assert.AreEqual(tickets.Count, ids.Count);
        }

        [Test]
        public void CanGetMultipleTicketsWithSideLoad()
        {
            var ids = new List<long>() { Settings.SampleTicketId, Settings.SampleTicketId2 };
            var tickets = api.Tickets.GetMultipleTickets(ids, sideLoadOptions: ticketSideLoadOptions);
            Assert.NotNull(tickets);
            Assert.AreEqual(tickets.Count, ids.Count);
            Assert.IsTrue(tickets.Users.Any());
            Assert.IsTrue(tickets.Organizations.Any());
        }

        [Test]
        public async Task CanGetMultipleTicketsAsyncWithSideLoad()
        {
            var ids = new List<long>() { Settings.SampleTicketId, Settings.SampleTicketId2 };
            var tickets = await api.Tickets.GetMultipleTicketsAsync(ids, sideLoadOptions: ticketSideLoadOptions);
            Assert.NotNull(tickets);
            Assert.AreEqual(tickets.Count, ids.Count);
            Assert.IsTrue(tickets.Users.Any());
            Assert.IsTrue(tickets.Organizations.Any());
        }


        [Test]
        public void CanGetMultipleTicketsSingleTicket()
        {
            var ids = new List<long>() { Settings.SampleTicketId };
            var tickets = api.Tickets.GetMultipleTickets(ids);
            Assert.NotNull(tickets);
            Assert.AreEqual(tickets.Count, ids.Count);
        }

        [Test]
        public async Task CanGetMultipleTicketsAsyncSingleTicket()
        {
            var ids = new List<long>() { Settings.SampleTicketId };
            var tickets = await api.Tickets.GetMultipleTicketsAsync(ids);
            Assert.NotNull(tickets);
            Assert.AreEqual(tickets.Count, ids.Count);
        }

        [Test]
        public void CorrectErrorMessagesAreThrown()
        {
            //var t = api.Tickets.GetTicket(921);
            //var res = api.Tickets.UpdateTicket(t.Ticket,
            //                                   new Comment()
            //                                       {
            //                                           Body = "trying to cause an error by updating a closed ticket. Let's see how it responds :)"                                                           
            //                                       });

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
            Assert.AreEqual(ticket.CustomFields[1].Value, res.CustomFields[1].Value);

            //var updateResponse = api.Tickets.UpdateTicket(res, new Comment() { Body = "Just trying to update it!", Public = true});
            //res.UpdatedAt = null;
            //res.CreatedAt = null;

            var updateResponse = api.Tickets.UpdateTicket(res, new Comment() { Body = "Just trying to update it!", Public = true });

            Assert.AreEqual(ticket.CustomFields[1].Value, updateResponse.Ticket.CustomFields[1].Value);


            Assert.True(api.Tickets.Delete(res.Id.Value));

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

            Assert.NotNull(res);
            Assert.Greater(res.Id, 0);

            Assert.AreEqual(res.CreatedAt, res.UpdatedAt);
            Assert.LessOrEqual(res.CreatedAt - DateTimeOffset.UtcNow, TimeSpan.FromMinutes(1.0));

            res.Status = TicketStatus.Solved;
            res.AssigneeId = Settings.UserId;

            res.CollaboratorIds.Add(Settings.CollaboratorId);
            var body = "got it thanks";

            res.CustomFields[0].Value = "updated";

            var updateResponse = api.Tickets.UpdateTicket(res, new Comment() { Body = body, Public = true, Uploads = new List<string>() });

            Assert.NotNull(updateResponse);
            //Assert.AreEqual(updateResponse.Audit.Events.First().Body, body);
            Assert.Greater(updateResponse.Ticket.CollaboratorIds.Count, 0);
            Assert.GreaterOrEqual(updateResponse.Ticket.UpdatedAt, updateResponse.Ticket.CreatedAt);

            Assert.True(api.Tickets.Delete(res.Id.Value));
        }

        [Test]
        public void CanGetTicketComments()
        {
            var comments = api.Tickets.GetTicketComments(2);
            Assert.IsNotEmpty(comments.Comments[1].Body);
        }

        [Test]
        public void CanGetTicketCommentsWithSideLoading()
        {
            var comments = api.Tickets.GetTicketComments(2, sideLoadOptions: ticketSideLoadOptions);
            Assert.IsNotEmpty(comments.Users);
            Assert.IsNull(comments.Organizations);
        }


        [Test]
        public void CanGetTicketCommentsPaged()
        {
            const int perPage = 5;
            const int page = 2;
            var commentsRes = api.Tickets.GetTicketComments(2, perPage, page);

            Assert.AreEqual(perPage, commentsRes.Comments.Count);
            Assert.AreEqual(perPage, commentsRes.PageSize);
            Assert.AreEqual(page, commentsRes.Page);

            Assert.IsNotEmpty(commentsRes.Comments[1].Body);

            var nextPageValue = commentsRes.NextPage.GetQueryStringDict()
                    .Where(x => x.Key == "page")
                        .Select(x => x.Value)
                        .FirstOrDefault();

            Assert.NotNull(nextPageValue);

            Assert.AreEqual((page + 1).ToString(), nextPageValue);
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

            Assert.NotNull(res);
            Assert.AreEqual(res.RequesterId, Settings.CollaboratorId);

            Assert.True(api.Tickets.Delete(res.Id.Value));
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

            Assert.NotNull(res);
            Assert.NotNull(res.Ticket);
            Assert.AreEqual(res.Ticket.RequesterId, Settings.CollaboratorId);

            Assert.True(api.Tickets.Delete(res.Ticket.Id.Value));
        }


        [Test]
        public void CanCreateTicketWithDueDate()
        {
            var dueAt = DateTimeOffset.Parse("12/31/2020 07:00:00 -05:00");

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

            Assert.True(api.Tickets.Delete(res.Id.Value));
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

            Assert.NotNull(res);
            Assert.AreEqual(Settings.TicketFormId, res.TicketFormId);

            Assert.True(api.Tickets.Delete(res.Id.Value));
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


            Assert.AreEqual(res.JobStatus.Status, "queued");

            //also test JobStatuses while we have a job here
            var job = api.JobStatuses.GetJobStatus(res.JobStatus.Id);
            Assert.AreEqual(job.JobStatus.Id, res.JobStatus.Id);

            Assert.True(api.Tickets.DeleteMultiple(new List<long>() { t1.Id.Value, t2.Id.Value }));
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

            Assert.AreEqual(t1.Audit.Events.First().Attachments.Count, 1);

            Assert.True(await api.Tickets.DeleteAsync(t1.Ticket.Id.Value));
        }

        [Test]
        public void CanAddAttachmentToTicket()
        {
            var res = api.Attachments.UploadAttachment(new ZenFile()
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

            var t1 = api.Tickets.CreateTicket(ticket);
            Assert.AreEqual(t1.Audit.Events.First().Attachments.Count, 1);

            Assert.True(api.Tickets.Delete(t1.Ticket.Id.Value));
        }

        [Test]
        public void CanGetCollaborators()
        {
            var res = api.Tickets.GetCollaborators(Settings.SampleTicketId);
            Assert.Greater(res.Users.Count, 0);
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
            Assert.Greater(res.Tickets.Count, 0);

            Assert.True(api.Tickets.DeleteMultiple(new List<long>() { t1.Id.Value, t2.Id.Value }));
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
            Assert.Greater(res.Tickets.Count, 0);

            Assert.True(api.Tickets.Delete(t1.Id.Value));
        }

        [Test]
        public void CanGetInrementalTicketExportTestOnly()
        {
            var res = api.Tickets.__TestOnly__GetIncrementalTicketExport(DateTime.Now.AddDays(-1));
            Assert.True(res.Count > 0);
        }

        [Test]
        public void CanGetIncrementalTicketExportPaged()
        {
            const int maxTicketsPerPage = 1000;

            var res = api.Tickets.GetIncrementalTicketExport(DateTime.Now.AddDays(-365));

            Assert.AreEqual(maxTicketsPerPage, res.Tickets.Count);
            Assert.That(res.NextPage, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void CanGetIncrementalTicketExportWithUsersSideLoadPaged()
        {
            const int maxTicketsPerPage = 1000;

            var res = api.Tickets.GetIncrementalTicketExport(DateTime.Now.AddDays(-365), TicketSideLoadOptionsEnum.Users);

            Assert.AreEqual(maxTicketsPerPage, res.Tickets.Count);
            Assert.IsTrue(res.Users.Count > 0);
            Assert.That(res.NextPage, Is.Not.Null.Or.Empty);

            res = this.api.Tickets.GetIncrementalTicketExportNextPage(res.NextPage);

            Assert.IsTrue(res.Tickets.Count > 0);
            Assert.IsTrue(res.Users.Count > 0);
        }

        [Test]
        public void CanGetIncrementalTicketExportWithGroupsSideLoadPaged()
        {
            const int maxTicketsPerPage = 1000;

            var res = api.Tickets.GetIncrementalTicketExport(DateTime.Now.AddDays(-700), TicketSideLoadOptionsEnum.Groups);

            Assert.AreEqual(maxTicketsPerPage, res.Tickets.Count);
            Assert.IsTrue(res.Groups.Count > 0);
            Assert.That(res.NextPage, Is.Not.Null.Or.Empty);

            res = this.api.Tickets.GetIncrementalTicketExportNextPage(res.NextPage);

            Assert.IsTrue(res.Tickets.Count > 0);
            Assert.IsTrue(res.Groups.Count > 0);
        }

        [Test]
        public void CanGetTicketFields()
        {
            var res = api.Tickets.GetTicketFields();
            Assert.True(res.TicketFields.Count > 0);
        }

        [Test]
        public void CanGetTicketFieldById()
        {
            var id = Settings.CustomFieldId;
            var ticketField = api.Tickets.GetTicketFieldById(id).TicketField;
            Assert.NotNull(ticketField);
            Assert.AreEqual(ticketField.Id, id);
        }

        [Test]
        public void CanGetTicketFieldByIdAsync()
        {
            var id = Settings.CustomFieldId;
            var ticketField = api.Tickets.GetTicketFieldByIdAsync(id).Result.TicketField;
            Assert.NotNull(ticketField);
            Assert.AreEqual(ticketField.Id, id);
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
            Assert.NotNull(res.TicketField);

            var updatedTF = res.TicketField;
            updatedTF.Title = "My Custom Field";

            var updatedRes = api.Tickets.UpdateTicketField(updatedTF);
            Assert.AreEqual(updatedRes.TicketField.Title, updatedTF.Title);

            Assert.True(api.Tickets.DeleteTicketField(updatedTF.Id.Value));
        }

        [Test]
        public void CanCreateAndDeleteTaggerTicketField()
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
                Name = "test entry",
                Value = "test value"
            });

            var res = api.Tickets.CreateTicketField(tField);
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

            var res = api.Tickets.CreateTicketField(tField);
            Assert.That(res.TicketField, Is.Not.Null);
            Assert.That(res.TicketField.Id, Is.Not.Null);
            Assert.That(res.TicketField.CustomFieldOptions.Count, Is.EqualTo(2));
            Assert.That(res.TicketField.CustomFieldOptions[0].Value, Is.EqualTo(option1));
            Assert.That(res.TicketField.CustomFieldOptions[1].Value, Is.EqualTo(option2));

            long id = res.TicketField.Id.Value;

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

            var resU = api.Tickets.UpdateTicketField(tFieldU);

            Assert.That(resU.TicketField.CustomFieldOptions.Count, Is.EqualTo(2));
            Assert.That(resU.TicketField.CustomFieldOptions[0].Value, Is.EqualTo(option1_Update));
            Assert.That(resU.TicketField.CustomFieldOptions[1].Value, Is.Not.EqualTo(option2));

            Assert.True(api.Tickets.DeleteTicketField(id));
        }

        [Test]
        public void CanGetSuspendedTickets()
        {
            var all = api.Tickets.GetSuspendedTickets();
            Assert.Greater(all.Count, 0);

            var ind = api.Tickets.GetSuspendedTicketById(all.SuspendedTickets[0].Id);
            Assert.AreEqual(ind.SuspendedTicket.Id, all.SuspendedTickets[0].Id);

            //There is no way to suspend a ticket so I can run a tests for recovering and deleting them
        }

        [Test]
        public void CanGetTicketForms()
        {
            var res = api.Tickets.GetTicketForms();
            Assert.Greater(res.Count, 0);
        }

        [Test]
        public void CanCreateUpdateAndDeleteTicketForms()
        {
            //api.Tickets.DeleteTicketForm(52523);
            //return;

            var res = api.Tickets.CreateTicketForm(new TicketForm()
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

            var get = api.Tickets.GetTicketFormById(res.TicketForm.Id.Value);
            Assert.AreEqual(get.TicketForm.Id, res.TicketForm.Id);

            res.TicketForm.Name = "Snowboard Fixed";
            res.TicketForm.DisplayName = "Snowboard has been fixed";
            res.TicketForm.Active = false;

            var update = api.Tickets.UpdateTicketForm(res.TicketForm);
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
            var metrics = api.Tickets.GetAllTicketMetrics();
            Assert.True(metrics.Count > 0);
            var count = 50;
            var nextPage = api.Tickets.GetByPageUrl<GroupTicketMetricResponse>(metrics.NextPage, count);
            Assert.AreEqual(nextPage.TicketMetrics.Count, count);

        }

        [Test]
        public void CanGetTicketMetricsAsync()
        {
            var tickets = api.Tickets.GetAllTicketMetricsAsync();
            Assert.True(tickets.Result.Count > 0);
        }

        [Test]
        public void CanGetTicketMetricByTicketId()
        {
            var id = Settings.SampleTicketId;
            var metric = api.Tickets.GetTicketMetricsForTicket(id).TicketMetric;
            Assert.NotNull(metric);
            Assert.AreEqual(metric.TicketId, id);
        }

        [Test]
        public void CanGetTicketMetricByTicketIdAsync()
        {
            var id = Settings.SampleTicketId;
            var metric = api.Tickets.GetTicketMetricsForTicketAsync(id).Result.TicketMetric;
            Assert.NotNull(metric);
            Assert.AreEqual(metric.TicketId, id);
        }

        [Test]
        public void CanGetAllTicketsWithSideLoad()
        {
            var tickets =
                api.Tickets.GetAllTickets(sideLoadOptions: ticketSideLoadOptions);

            Assert.IsTrue(tickets.Users.Any());
            Assert.IsTrue(tickets.Organizations.Any());
        }

        [Test]
        public void CanGetAllTicketsAsyncWithSideLoad()
        {
            var tickets =
                api.Tickets.GetAllTicketsAsync(sideLoadOptions: ticketSideLoadOptions);

            Assert.IsTrue(tickets.Result.Users.Any());
            Assert.IsTrue(tickets.Result.Organizations.Any());
        }

        [Test]
        public void CanGetTicketsByOrganizationIDAsyncWithSideLoad()
        {
            var id = Settings.OrganizationId;
            var tickets = api.Tickets.GetTicketsByOrganizationIDAsync(id, sideLoadOptions: ticketSideLoadOptions);
            Assert.True(tickets.Result.Count > 0);
            Assert.IsTrue(tickets.Result.Users.Any());
            Assert.IsTrue(tickets.Result.Organizations.Any());
        }

        [Test]
        public void CanCanGetTicketsByOrganizationIDWithSideLoad()
        {
            var id = Settings.OrganizationId;
            var tickets = api.Tickets.GetTicketsByOrganizationID(id, sideLoadOptions: ticketSideLoadOptions);
            Assert.True(tickets.Count > 0);
            Assert.IsTrue(tickets.Users.Any());
            Assert.IsTrue(tickets.Organizations.Any());
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

            Assert.NotNull(res);
            Assert.True(res.Id.HasValue);
            Assert.Greater(res.Id.Value, 0);
            Assert.Less(res.CreatedAt.Value.LocalDateTime, DateTime.Now.AddDays(-4));
            Assert.Greater(res.UpdatedAt.Value.LocalDateTime, res.CreatedAt.Value.LocalDateTime);
            Assert.AreEqual(res.Status, TicketStatus.Solved);
            Assert.AreEqual(res.Description, "test description");

            var resComments = api.Tickets.GetTicketComments(res.Id.Value);
            Assert.NotNull(resComments);
            Assert.AreEqual(resComments.Count, 3);

            api.Tickets.DeleteAsync(res.Id.Value);
            //Assert.Greater(res.SolvedAt.Value.LocalDateTime, res.UpdatedAt.Value.LocalDateTime);
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

            Assert.NotNull(res.Result);
            Assert.Greater(res.Result.Ticket.Id.Value, 0);
            Assert.Less(res.Result.Ticket.CreatedAt.Value.LocalDateTime, DateTime.Now.AddDays(-4));
            Assert.Greater(res.Result.Ticket.UpdatedAt.Value.LocalDateTime, res.Result.Ticket.CreatedAt.Value.LocalDateTime);
            Assert.AreEqual(res.Result.Ticket.Status, TicketStatus.Solved);
            Assert.AreEqual(res.Result.Ticket.Description, "test description");

            var resComments = api.Tickets.GetTicketComments(res.Result.Ticket.Id.Value);
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

            Assert.AreEqual(res.JobStatus.Status, "queued");

            var job = api.JobStatuses.GetJobStatus(res.JobStatus.Id);
            Assert.AreEqual(job.JobStatus.Id, res.JobStatus.Id);


            int count = 0;
            while (job.JobStatus.Status.ToLower() != "completed" && count < 10)
            {
                Thread.Sleep(1000);
                job = api.JobStatuses.GetJobStatus(res.JobStatus.Id);
                count++;
            }

            Assert.AreEqual(job.JobStatus.Status.ToLower(), "completed");

            foreach (var r in job.JobStatus.Results)
            {
                var ticket = api.Tickets.GetTicket(r.Id).Ticket;
                Assert.AreEqual(ticket.Description, "test description");
                var resComments = api.Tickets.GetTicketComments(r.Id);
                Assert.NotNull(resComments);
                Assert.AreEqual(resComments.Count, 3);
                foreach (var c in resComments.Comments)
                {
                    Assert.True(c.CreatedAt.HasValue);
                    Assert.Less(c.CreatedAt.Value.LocalDateTime, DateTime.Now.AddDays(-1));
                }

                api.Tickets.DeleteAsync(r.Id);
            }
        }
    }
}
