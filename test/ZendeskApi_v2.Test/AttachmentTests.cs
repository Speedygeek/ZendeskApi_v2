using System;
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
            Assert.True(!string.IsNullOrEmpty(res.Token));
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
    }
}
