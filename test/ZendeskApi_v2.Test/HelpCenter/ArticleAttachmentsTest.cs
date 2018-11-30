using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;
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
        public void CanUploadAndGetAttachmentsForArticle()
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

            var resp = api.HelpCenter.ArticleAttachments.UploadAttachment(articleResponse.Article.Id, file);

            Assert.That(resp.Attachment, Is.Not.Null);

            var getResponse = api.HelpCenter.ArticleAttachments.GetAttachments(articleResponse.Article.Id.Value);
            Assert.That(getResponse.Attachments, Is.Not.Null);
            Assert.That(api.HelpCenter.ArticleAttachments.DeleteAttachment(resp.Attachment.Id), Is.True);
            Assert.That(api.HelpCenter.Articles.DeleteArticle(articleResponse.Article.Id.Value), Is.True);
        }

        [Test]
        public async Task CanUploadandGetAttachmentsForArticleAsync()
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

            var resp = await api.HelpCenter.ArticleAttachments.UploadAttachmentAsync(articleResponse.Article.Id, file, true);

            Assert.That(resp.Attachment, Is.Not.Null);
            Assert.That(resp.Attachment.Inline, Is.True);

            var res = await api.HelpCenter.ArticleAttachments.GetAttachmentsAsync(articleResponse.Article.Id.Value);
            Assert.That(res.Attachments, Is.Not.Null);

            Assert.That(await api.HelpCenter.ArticleAttachments.DeleteAttachmentAsync(resp.Attachment.Id), Is.True);
            Assert.That(await api.HelpCenter.Articles.DeleteArticleAsync(articleResponse.Article.Id.Value), Is.True);
        }
    }
}
