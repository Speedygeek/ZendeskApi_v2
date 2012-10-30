using System;
using System.IO;
using NUnit.Framework;
using ZenDeskApi_v2;
using ZenDeskApi_v2.Models.Shared;


namespace Tests
{
    [TestFixture]
    public class AttachmentTests
    {        
        ZenDeskApi api = new ZenDeskApi(Settings.Site, Settings.Email, Settings.Password);

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
