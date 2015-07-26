using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ZendeskApi_v2;
using ZendeskApi_v2.Extensions;
using ZendeskApi_v2.Models.Constants;
using ZendeskApi_v2.Models.Shared;
using ZendeskApi_v2.Models.Tickets;


namespace Tests
{
    [TestFixture]
    public class TicketTests
    {
        ZendeskApi api = new ZendeskApi(Settings.Site, Settings.Email, Settings.Password);

        [Test]
        public void CanGetTicketsAsync()
        {
            var tickets = api.Tickets.GetAllTicketsAsync();
            Assert.True(tickets.Result.Count > 0);
        }

        [Test]
        public void CanCanGetTicketsByOrganizationIDAsync()
        {
            var id = Settings.OrganizationId;
            var tickets = api.Tickets.GetTicketsByOrganizationIDAsync(id);
            Assert.True(tickets.Result.Count > 0);
        }

        [Test]
        public void CanCreateUpdateAndDeleteTicketAsync()
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
                            Value = "Doing fine!"
                        }
                };

            var res = api.Tickets.CreateTicketAsync(ticket).Result.Ticket;

            Assert.NotNull(res);
            Assert.Greater(res.Id.Value, 0);

            res.Status = TicketStatus.Solved;
            res.AssigneeId = Settings.UserId;

            res.CollaboratorEmails = new List<string>() { Settings.ColloboratorEmail };
            var body = "got it thanks";
            var updateResponse = api.Tickets.UpdateTicketAsync(res, new Comment() { Body = body, Public = true });

            Assert.NotNull(updateResponse.Result);
            Assert.AreEqual(updateResponse.Result.Audit.Events.First().Body, body);

            //Assert.True(api.Tickets.DeleteAsync(res.Id.Value).Result);
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
        [Ignore] // fragile needs to be changed. 
        public void CanGetRecentTicketsPaged()
        {
            var ticketsRes = api.Tickets.GetRecentTickets(5, 2);

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
        public void CanCreateTicketWithDueDate()
        {
            var dueAt = DateTimeOffset.UtcNow;

            var ticket = new Ticket()
            {
                Subject = "ticket with due date",
                Comment = new Comment() { Body = "test comment" },
                Type = "task",
                Priority = TicketPriorities.Normal,
                DueAt = DateTimeOffset.UtcNow
            };

            var res = api.Tickets.CreateTicket(ticket).Ticket;

            Assert.NotNull(res);
            Assert.AreEqual(dueAt.ToString(), res.DueAt.ToString());

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
        public void CanAddAttachmentToTicketAsync()
        {
            var res = api.Attachments.UploadAttachmentAsync(new ZenFile()
            {
                ContentType = "text/plain",
                FileName = "testupload.txt",
                FileData = File.ReadAllBytes(Environment.CurrentDirectory + "\\testupload.txt")
            }).Result;

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

            var t1 = api.Tickets.CreateTicketAsync(ticket).Result;
            Assert.AreEqual(t1.Audit.Events.First().Attachments.Count, 1);

            Assert.True(api.Tickets.DeleteAsync(t1.Ticket.Id.Value).Result);
        }

        [Test]
        public void CanAddAttachmentToTicket()
        {
            var res = api.Attachments.UploadAttachment(new ZenFile()
            {
                ContentType = "text/plain",
                FileName = "testupload.txt",
                FileData = File.ReadAllBytes(Environment.CurrentDirectory + "\\testupload.txt")
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
        [Ignore] // currently getting a 404 need to talk with zendesk about why. 
        public void CanGetAuditsAndMarkAsTrusted()
        {
            var audits = api.Tickets.GetAudits(Settings.SampleTicketId);
            Assert.Greater(audits.Audits.Count, 0);

            var aud = api.Tickets.GetAuditById(Settings.SampleTicketId, audits.Audits.First().Id);
            Assert.NotNull(aud.Audit);

            Assert.True(api.Tickets.MarkAuditAsTrusted(Settings.SampleTicketId, audits.Audits.First().Id));
        }

        [Test]
        public void CanGetInrementalTicketExport()
        {
            var res = api.Tickets.__TestOnly__GetInrementalTicketExport(DateTime.Now.AddDays(-1));
            Assert.True(res.Results.Count > 0);
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
                Value = "test_valueA"
            });

            tField.CustomFieldOptions.Add(new CustomFieldOptions()
            {
                Name = "test entryB",
                Value = "test_valueB"
            });

            var res = api.Tickets.CreateTicketField(tField);
            Assert.NotNull(res.TicketField);
            Assert.NotNull(res.TicketField.Id);
            Assert.AreEqual(res.TicketField.CustomFieldOptions.Count, 2);
            Assert.NotNull(res.TicketField.CustomFieldOptions[0].Id);
            Assert.NotNull(res.TicketField.CustomFieldOptions[1].Id);

            long id = res.TicketField.Id.Value;
            long idCfoA = res.TicketField.CustomFieldOptions[0].Id.Value;
            long idCfoB = res.TicketField.CustomFieldOptions[1].Id.Value;

            /* https://developer.zendesk.com/rest_api/docs/core/ticket_fields#updating-a-dropdown-tagger-field
             * All options must be passed on update. Options that are not passed will be removed; as a result, these values will be removed from any tickets or macros.
             * To create a new option, pass a null id along with name and value.
             * To update an existing option, pass its id along with name and value.
             * To remove an option, omit it from the list of options upon update.
             */
            var tFieldU = new TicketField()
            {
                Id = id,
                CustomFieldOptions = new List<CustomFieldOptions>()
            };

            //update CustomFieldOption A
            tFieldU.CustomFieldOptions.Add(new CustomFieldOptions()
            {
                Id = idCfoA,
                Name = "test entryA newTitle",
                Value = "test_valueA_newTag"
            });
            //delete CustomFieldOption B
            //add CustomFieldOption C
            tFieldU.CustomFieldOptions.Add(new CustomFieldOptions()
            {
                Name = "test entryC",
                Value = "test_valueC"
            });

            var resU = api.Tickets.UpdateTicketField(tFieldU);

            Assert.AreEqual(resU.TicketField.CustomFieldOptions.Count, 2);
            Assert.AreEqual(resU.TicketField.CustomFieldOptions[0].Id, idCfoA);
            Assert.AreNotEqual(resU.TicketField.CustomFieldOptions[1].Id, idCfoB);

            Assert.True(api.Tickets.DeleteTicketField(id));
        }

        [Test]
        public void CanGetSuspendedTickets()
        {
            var all = api.Tickets.GetSuspendedTickets();
            Assert.Greater(all.Count, 0);

            var ind = api.Tickets.GetSuspendedTicketById(all.SuspendedTickets[0].Id);
            Assert.AreEqual(ind.SuspendedTicket.Id, all.SuspendedTickets[0].Id);

            //There is no way to suspend a ticket so I can run a tests for recovering and deleteing them
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

    }
}
