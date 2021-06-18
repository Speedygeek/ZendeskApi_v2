﻿using System;
using System.IO;
using NUnit.Framework;
using ZendeskApi_v2;
using ZendeskApi_v2.Models.Shared;
using System.Threading.Tasks;
using ZendeskApi_v2.Models.Tickets;
using ZendeskApi_v2.Models.Constants;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Net;

namespace Tests
{
    [TestFixture]
    public class AttachmentTests
    {
        readonly ZendeskApi api = new ZendeskApi(Settings.Site, Settings.AdminEmail, Settings.AdminPassword);

        [Test]
        public void CanUploadAttachments()
        {
            var res = api.Attachments.UploadAttachment(new ZenFile()
            {
                ContentType = "text/plain",
                FileName = "testupload.txt",
                FileData = File.ReadAllBytes(Path.Combine(TestContext.CurrentContext.TestDirectory, "testupload.txt"))
            });
            Assert.That(!string.IsNullOrEmpty(res.Token), Is.True);
        }


        [Test]
        public async Task CanDowloadAttachment()
        {
            var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "testupload.txt");

            var res = await api.Attachments.UploadAttachmentAsync(new ZenFile()
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

            var t1 = await api.Tickets.CreateTicketAsync(ticket);
            Assert.That(t1.Audit.Events.First().Attachments.Count, Is.EqualTo(1));

            var test = t1.Audit.Events.First().Attachments.First();
            var file = await api.Attachments.DownloadAttachmentAsync(test);

            Assert.That(file.FileData, Is.Not.Null);

            Assert.That(api.Tickets.Delete(t1.Ticket.Id.Value), Is.True);
            Assert.That(api.Attachments.DeleteUpload(res));
        }

        [Test]
        public async Task CanRedactAttachment()
        {
            //This could probably be brought into a helper for above two tests perhaps
            var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "testupload.txt");

            var res = await api.Attachments.UploadAttachmentAsync(new ZenFile()
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

            var t1 = await api.Tickets.CreateTicketAsync(ticket);

            var comments = api.Tickets.GetTicketComments(t1.Ticket.Id.Value);

            var attach = comments.Comments[0].Attachments[0];

            var delRes = api.Attachments.RedactCommentAttachment(attach.Id, t1.Ticket.Id.Value, comments.Comments[0].Id.Value);
            //Returned correct attachment
            Assert.That(delRes.Attachment.Id, Is.EqualTo(attach.Id));

            //Check the file has been replaced by redacted.txt
            Assert.That(api.Tickets.GetTicketComments(t1.Ticket.Id.Value).Comments[0].Attachments[0].FileName, Is.EqualTo("redacted.txt"));
        }
    }
}
