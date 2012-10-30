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

        /// <summary>
        /// This test fails because the problem is with ZenDesk.
        /// https://{subdomain}.zendesk.com/api/v2/tickets/show_many?ids={id,id,id}.json 
        /// This only returns the 2nd for some reason.        
        /// </summary>
        [Test]
        public void CanGetMultipleTickets()
        {
            var ids = new List<int>() {1, 2};
            var tickets = api.Tickets.GetMultiple(ids);
            Assert.NotNull(tickets);
            Assert.AreEqual(tickets.Count, ids.Count);            
        }

        [Test]
        public void CanCreateAndUpdateTicket()
        {
            var ticket = new Ticket()
                             {
                                 Subject = "my printer is on fire",
                                 Description = "HELP",
                                 Priority = TicketPriorities.Urgent
                             };

            var res = api.Tickets.CreateTicket(ticket).Ticket;

            Assert.NotNull(res);
            Assert.Greater(res.Id, 0);

            res.Status = TicketStatus.Solved;
            res.AssigneeId = Settings.UserId;

            res.CollaboratorEmails = new List<string>(){ Settings.ColloboratorEmail};
            var updateResponse = api.Tickets.UpdateTicket(res, new Comment() {Body = "got it thanks", Public = true});

            Assert.NotNull(updateResponse);
            Assert.Greater(updateResponse.Audit.Events.Count, 0);
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
        }

        [Test]
        public void CanAddAttachmentToTicket()
        {
            var res = api.Attachments.UploadAttachment(new ZenFile()
            {
                ContentType = "text/plain",
                FileName = "testupload.txt",
                FileData =
                    File.ReadAllBytes(Environment.CurrentDirectory + "\\testupload.txt")
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
        }
    }
}
