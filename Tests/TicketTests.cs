using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ZendeskApi_v2;
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
                Description = "HELP",
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

            Assert.True(api.Tickets.DeleteAsync(res.Id.Value).Result);
        }

        [Test]
        public void CanGetTickets()
        {
            var tickets = api.Tickets.GetAllTickets();            
            Assert.True(tickets.Count > 0);

            var ticketsByUser = api.Tickets.GetTicketsByUserID(tickets.Tickets[0].RequesterId.Value);
            Assert.True(ticketsByUser.Count > 0);
        }

        [Test]
        public  void CanGetTicketById()
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
        public void CanGetMultipleTickets()
        {
            var ids = new List<long>() { Settings.SampleTicketId, Settings.SampleTicketId2 };
            var tickets = api.Tickets.GetMultipleTickets(ids);
            Assert.NotNull(tickets);
            Assert.AreEqual(tickets.Count, ids.Count);            
        }

        [Test]
        public void CanCreateUpdateAndDeleteTicket()
        {
            var ticket = new Ticket()
                             {
                                 Subject = "my printer is on fire",
                                 Description = "HELP",
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

            res.Status = TicketStatus.Solved;
            res.AssigneeId = Settings.UserId;

            res.CollaboratorEmails = new List<string>(){ Settings.ColloboratorEmail};
            var body = "got it thanks";

            res.CustomFields[0].Value = "updated";

            var updateResponse = api.Tickets.UpdateTicket(res, new Comment() {Body = body, Public = true, Uploads = new List<string>()});            

            Assert.NotNull(updateResponse);
            Assert.AreEqual(updateResponse.Audit.Events.First().Body, body);
            
            Assert.True(api.Tickets.Delete(res.Id.Value));
        }

        [Test]
        public void CanGetTicketComments()
        {
            var comments = api.Tickets.GetTicketComments(2);
            Assert.IsNotEmpty(comments.Comments[1].Body);            
        }

        [Test]
        public void CanCreateTicketWithRequester()
        {
            var ticket = new Ticket()
            {
                Subject = "ticket with requester",
                Description = "testing requester",
                Priority = TicketPriorities.Normal,
                Requester = new Requester(){Email = Settings.ColloboratorEmail}
            };

            var res = api.Tickets.CreateTicket(ticket).Ticket;

            Assert.NotNull(res);
            Assert.AreEqual(res.RequesterId, Settings.CollaboratorId);

            Assert.True(api.Tickets.Delete(res.Id.Value));
        }

        [Test]
        public void CanBulkUpdateTickets()
        { 
            var t1 = api.Tickets.CreateTicket(new Ticket()
            {
                Subject = "testing bulk update",
                Description = "HELP",
                Priority = TicketPriorities.Normal 
            }).Ticket;
            var t2 = api.Tickets.CreateTicket(new Ticket()
            {
                Subject = "more testing for bulk update",
                Description = "Bulk UpdateTicket testing",
                Priority = TicketPriorities.Normal
            }).Ticket;

            var res = api.Tickets.BulkUpdate(new List<long>() { t1.Id.Value, t2.Id.Value }, new BulkUpdate()
                                                                       {
                                                                          Status = TicketStatus.Solved,
                                                                          Comment = new Comment(){Public = true, Body = "check your email"},
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
                Description = "test attachment",
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
                Description = "test attachment",
                Priority = TicketPriorities.Normal,
                Comment = new Comment() { 
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
                Description = "testing incidents with problems",
                Priority = TicketPriorities.Normal,
                Type = TicketTypes.Problem
            }).Ticket;

            var t2 = api.Tickets.CreateTicket(new Ticket()
            {
                Subject = "incident",
                Description = "testing incidents",
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
                Description = "testing incidents with problems",
                Priority = TicketPriorities.Normal,
                Type = TicketTypes.Problem
            }).Ticket;           

            var res = api.Tickets.GetProblems();
            Assert.Greater(res.Tickets.Count, 0);

            Assert.True(api.Tickets.Delete(t1.Id.Value));
        }

        /// <summary>
        /// Can't find out what Autocomplete does, so I'm not sure how to test it
        /// </summary>
        [Test]
        public void CanAutocompleteProblems()
        {
            var t1 = api.Tickets.CreateTicket(new Ticket()
            {
                Subject = "test problem",
                Description = "testing incidents with problems",
                Priority = TicketPriorities.Normal,
                Type = TicketTypes.Problem
            }).Ticket;

            var res = api.Tickets.AutoCompleteProblems("att");

            api.Tickets.Delete(t1.Id.Value);

            Assert.Inconclusive();
        }

        [Test]
        public  void CanGetAuditsAndMarkAsTrusted()
        {
            var audits = api.Tickets.GetAudits(Settings.SampleTicketId);
            Assert.Greater(audits.Audits.Count, 0);

            var aud = api.Tickets.GetAuditById(Settings.SampleTicketId, audits.Audits.First().Id);
            Assert.NotNull(aud.Audit);

            Assert.True(api.Tickets.MarkAuditAsTrusted(Settings.SampleTicketId, audits.Audits.First().Id));
        }        

        [Test]
        public  void CanGetInrementalTicketExport()
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
        public void CanCreateUpdateAndDeleteTicketFields()
        {
            // Something seems wrong with the api itself.
            Assert.Inconclusive();

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
        public void CanGetSuspendedTickets()
        {
            var all = api.Tickets.GetSuspendedTickets();
            Assert.Greater(all.Count, 0);

            var ind = api.Tickets.GetSuspendedTicketById(all.SuspendedTickets[0].Id);
            Assert.AreEqual(ind.SuspendedTicket.Id, all.SuspendedTickets[0].Id);

            //There is no way to suspend a ticket so I can run a tests for recovering and deleteing them
        }
    }
}
