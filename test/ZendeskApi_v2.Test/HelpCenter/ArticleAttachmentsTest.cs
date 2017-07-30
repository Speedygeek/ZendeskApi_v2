using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZendeskApi_v2;
using ZendeskApi_v2.Models.Articles;
using ZendeskApi_v2.Models.Shared;

namespace Tests.HelpCenter
{
    [TestFixture]
    [Category("HelpCenter")]
    public class ArticleAttachmentsTest
    {
        private ZendeskApi api = new ZendeskApi(Settings.Site, Settings.AdminEmail, Settings.AdminPassword);

        [Test]
        public void CanGetAttachmentsForArticle()
        {
            var res = api.HelpCenter.ArticleAttachments.GetAttachments(204838115);
            Assert.That(res.Attachments, Is.Not.Null);
        }

        [Test]
        public void CanUploadAttachmentsForArticle()
        {
            var file = new ZenFile()
            {
                ContentType = "text/plain",
                FileName = "testupload.txt",
                FileData = File.ReadAllBytes(TestContext.CurrentContext.TestDirectory + "\\testupload.txt")
            };

            var respSections = api.HelpCenter.Sections.GetSections();
            var articleResponse = api.HelpCenter.Articles.CreateArticle(respSections.Sections[0].Id.Value, new Article
            {
                Title = "My Test article",
                Body = "The body of my article",
                Locale = "en-us"
            });

            var resp = api.HelpCenter.ArticleAttachments.UploadAttchment(articleResponse.Article.Id, file);

            Assert.That(resp.Attachment, Is.Not.Null);
            Assert.That(api.HelpCenter.ArticleAttachments.DeleteAttchment(resp.Attachment.Id), Is.True);
            Assert.That(api.HelpCenter.Articles.DeleteArticle(articleResponse.Article.Id.Value), Is.True);
        }


        [Test]
        public async Task CanGetAttachmentsForArticleAsync()
        {
            var res = await api.HelpCenter.ArticleAttachments.GetAttachmentsAsync(204838115);
            Assert.That(res.Attachments, Is.Not.Null);
        }

        [Test]
        public async Task CanUploadAttachmentsForArticleAsync()
        {
            var file = new ZenFile()
            {
                ContentType = "text/plain",
                FileName = "testupload.txt",
                FileData = File.ReadAllBytes(TestContext.CurrentContext.TestDirectory + "\\testupload.txt")
            };

            var respSections = await api.HelpCenter.Sections.GetSectionsAsync();
            var articleResponse = await api.HelpCenter.Articles.CreateArticleAsync(respSections.Sections[0].Id.Value, new Article
            {
                Title = "My Test article",
                Body = "The body of my article",
                Locale = "en-us"
            });

            var resp = await api.HelpCenter.ArticleAttachments.UploadAttchmentAsync(articleResponse.Article.Id, file, true);

            Assert.That(resp.Attachment, Is.Not.Null);
            Assert.That(resp.Attachment.Inline, Is.True);

            Assert.That(await api.HelpCenter.ArticleAttachments.DeleteAttchmentAsync(resp.Attachment.Id), Is.True);
            Assert.That(await api.HelpCenter.Articles.DeleteArticleAsync(articleResponse.Article.Id.Value), Is.True);
        }
    }
}
