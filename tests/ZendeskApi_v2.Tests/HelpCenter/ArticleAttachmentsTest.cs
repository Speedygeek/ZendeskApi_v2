using NUnit.Framework;
using System.IO;
using System.Threading.Tasks;
using ZendeskApi_v2.Models.Articles;
using ZendeskApi_v2.Models.Shared;
using ZendeskApi_v2.Tests.Base;

namespace ZendeskApi_v2.Tests.HelpCenter;

[TestFixture]
[Category("HelpCenter")]
public class ArticleAttachmentsTest : TestBase
{
    private readonly long _sectionId = 201010935;

    [Test]
    public void CanUploadAttachmentsForArticle()
    {
        var file = new ZenFile()
        {
            ContentType = "text/plain",
            FileName = "testupload.txt",
            FileData = File.ReadAllBytes(TestContext.CurrentContext.TestDirectory + "\\testupload.txt")
        };

        var articleResponse = Api.HelpCenter.Articles.CreateArticle(_sectionId, new Article
        {
            Title = "My Test article",
            Body = "The body of my article",
            Locale = "en-us"
        });

        var resp = Api.HelpCenter.ArticleAttachments.UploadAttachment(articleResponse.Article.Id, file);

        Assert.That(resp.Attachment, Is.Not.Null);

        var res = Api.HelpCenter.ArticleAttachments.GetAttachments(articleResponse.Article.Id);
        Assert.Multiple(() =>
        {
            Assert.That(res.Attachments, Is.Not.Null);

            Assert.That(Api.HelpCenter.ArticleAttachments.DeleteAttachment(resp.Attachment.Id), Is.True);
            Assert.That(Api.HelpCenter.Articles.DeleteArticle(articleResponse.Article.Id.Value), Is.True);
        });
    }

    [Test]
    public async Task CanUploadAttachmentsForArticleAsync()
    {
        var file = new ZenFile()
        {
            ContentType = "image/jpeg",
            FileName = "gracehoppertocat3.jpg",
            FileData = File.ReadAllBytes(TestContext.CurrentContext.TestDirectory + "\\gracehoppertocat3.jpg")
        };

        var articleResponse = await Api.HelpCenter.Articles.CreateArticleAsync(_sectionId, new Article
        {
            Title = "My Test article",
            Body = "The body of my article",
            Locale = "en-us"
        });

        var resp = await Api.HelpCenter.ArticleAttachments.UploadAttachmentAsync(articleResponse.Article.Id, file, true);

        Assert.That(resp.Attachment, Is.Not.Null);
        Assert.That(resp.Attachment.Inline, Is.True);

        var res = await Api.HelpCenter.ArticleAttachments.GetAttachmentsAsync(articleResponse.Article.Id);
        Assert.Multiple(async () =>
        {
            Assert.That(res.Attachments, Is.Not.Null);

            Assert.That(await Api.HelpCenter.ArticleAttachments.DeleteAttachmentAsync(resp.Attachment.Id), Is.True);
            Assert.That(await Api.HelpCenter.Articles.DeleteArticleAsync(articleResponse.Article.Id.Value), Is.True);
        });
    }
}
