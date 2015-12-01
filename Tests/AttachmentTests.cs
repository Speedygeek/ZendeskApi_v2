using System;
using System.IO;
using NUnit.Framework;
using Tests.Properties;
using ZendeskApi_v2;
using ZendeskApi_v2.Models.Shared;


namespace Tests
{
    [TestFixture]
    public class AttachmentTests
    {        
        ZendeskApi api = new ZendeskApi(Settings.Default.Site, Settings.Default.Email, Settings.Default.Password);

        [Test]
        public void CanUploadAttachments()
        {
            var res = api.Attachments.UploadAttachment(new ZenFile()
                                                               {
                                                                   ContentType = "text/plain",
                                                                   FileName = "testupload.txt",
                                                                   FileData =
                                                                       File.ReadAllBytes(Environment.CurrentDirectory +
                                                                                         "\\testupload.txt")
                                                               });
            Assert.True(!string.IsNullOrEmpty(res.Token));
        }


    }
}
