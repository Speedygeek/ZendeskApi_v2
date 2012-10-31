using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ZenDeskApi_v2;
using ZenDeskApi_v2.Models.Constants;
using ZenDeskApi_v2.Models.Shared;
using ZenDeskApi_v2.Models.Tickets;


namespace Tests
{
    [TestFixture]
    public class TicketTests
    {        
        ZenDeskApi api = new ZenDeskApi(Settings.Site, Settings.Email, Settings.Password);

        [Test]
        public void CanGetTickets()
        {
            var tickets = api.Tickets.GetAll();
            Assert.True(tickets.Count > 0);
        }

        [Test]
        public  void CanGetTicketById()
        {
            var id = 1;
            var ticket = api.Tickets.Get(id).Ticket;
            Assert.NotNull(ticket);
            Assert.AreEqual(ticket.Id, id);
        }
        
        [Test]
        public void CanGetMultipleTickets()
        {
            var ids = new List<int>() {1, 2};
            var tickets = api.Tickets.GetMultiple(ids);
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

            var res = api.Tickets.Create(ticket).Ticket;

            Assert.NotNull(res);
            Assert.Greater(res.Id, 0);

            res.Status = TicketStatus.Solved;
            res.AssigneeId = Settings.UserId;

            res.CollaboratorEmails = new List<string>(){ Settings.ColloboratorEmail};
            var updateResponse = api.Tickets.Update(res, new Comment() {Body = "got it thanks", Public = true});

            Assert.NotNull(updateResponse);
            Assert.Greater(updateResponse.Audit.Events.Count, 0);

            Assert.True(api.Tickets.Delete(res.Id));
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

            var res = api.Tickets.Create(ticket).Ticket;

            Assert.NotNull(res);
            Assert.AreEqual(res.RequesterId, Settings.CollaboratorId);

            Assert.True(api.Tickets.Delete(res.Id));
        }

        [Test]
        public void CanBulkUpdateTickets()
        { 
            var t1 = api.Tickets.Create(new Ticket()
            {
                Subject = "testing bulk update",
                Description = "HELP",
                Priority = TicketPriorities.Normal 
            }).Ticket;
            var t2 = api.Tickets.Create(new Ticket()
            {
                Subject = "more testing for bulk update",
                Description = "Bulk Update testing",
                Priority = TicketPriorities.Normal
            }).Ticket;

            var res = api.Tickets.BulkUpdate(new List<int>() {t1.Id, t2.Id}, new BulkUpdate()
                                                                       {
                                                                          Status = TicketStatus.Solved,
                                                                          Comment = new Comment(){Public = true, Body = "check your email"},
                                                                          CollaboratorEmails = new List<string>() { Settings.ColloboratorEmail },
                                                                          AssigneeId = Settings.UserId                                                                          
                                                                       });                        

            
            Assert.AreEqual(res.JobStatus.Status, "queued");

            Assert.True(api.Tickets.DeleteMultiple(new List<int>(){ t1.Id, t2.Id}));
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

            var t1 = api.Tickets.Create(ticket);
            Assert.AreEqual(t1.Audit.Events.First().Attachments.Count, 1);

            Assert.True(api.Tickets.Delete(t1.Ticket.Id));
        }

        [Test]
        public void CanGetCollaborators()
        {
            var res = api.Tickets.GetCollaborators(1);
            Assert.Greater(res.Users.Count, 0);
        }

        [Test]
        public void CanGetIncidents()
        {
            var t1 = api.Tickets.Create(new Ticket()
            {
                Subject = "test problem",
                Description = "testing incidents with problems",
                Priority = TicketPriorities.Normal,
                Type = TicketTypes.Problem
            }).Ticket;

            var t2 = api.Tickets.Create(new Ticket()
            {
                Subject = "incident",
                Description = "testing incidents",
                Priority = TicketPriorities.Normal,
                Type = TicketTypes.Incident,
                ProblemId = t1.Id
            }).Ticket;

            var res = api.Tickets.GetIncidents(t1.Id);
            Assert.Greater(res.Tickets.Count, 0);

            Assert.True(api.Tickets.DeleteMultiple(new List<int>(){ t1.Id, t2.Id}));
        }

        [Test]
        public void CanGetProblems()
        {
            var t1 = api.Tickets.Create(new Ticket()
            {
                Subject = "test problem",
                Description = "testing incidents with problems",
                Priority = TicketPriorities.Normal,
                Type = TicketTypes.Problem
            }).Ticket;           

            var res = api.Tickets.GetProblems();
            Assert.Greater(res.Tickets.Count, 0);

            Assert.True(api.Tickets.Delete(t1.Id));
        }

        /// <summary>
        /// Can't find out what Autocomplete does, so I'm not sure how to test it
        /// </summary>
        [Test]
        public void CanAutocompleteProblems()
        {
            var t1 = api.Tickets.Create(new Ticket()
            {
                Subject = "test problem",
                Description = "testing incidents with problems",
                Priority = TicketPriorities.Normal,
                Type = TicketTypes.Problem
            }).Ticket;

            var res = api.Tickets.AutoCompleteProblems("att");

            Assert.Inconclusive();
        }
    }
}
