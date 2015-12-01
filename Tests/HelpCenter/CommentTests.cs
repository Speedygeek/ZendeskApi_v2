using NUnit.Framework;
using Tests.Properties;
using ZendeskApi_v2;

namespace Tests.HelpCenter {
	[TestFixture]
    [Category("HelpCenter")]
    public class CommentTests
    {
        private ZendeskApi api = new ZendeskApi(Settings.Default.Site, Settings.Default.Email, Settings.Default.Password);
        private long _articleIdWithComments = 204838115; //https://csharpapi.zendesk.com/hc/en-us/articles/204838115-Thing-4?page=1#comment_200486479

        [Test]
        [Ignore]
        [Timeout(1000)]
        public void CanGetArticleComments()
        {
            var comments = api.HelpCenter.Comments.GetCommentsForArticle(_articleIdWithComments);

            Assert.IsTrue(comments.Count > 0);
        }

        [Test]
        [Ignore]
        [Timeout(1000)]
        public void CanGetUserComments()
        {
            var comments = api.HelpCenter.Comments.GetCommentsForUser(Settings.Default.UserId);

            Assert.IsTrue(comments.Count > 0);
        }
    }
}