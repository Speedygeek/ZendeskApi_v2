using NUnit.Framework;
using ZendeskApi_v2;
using ZendeskApi_v2.Models.Articles;
using ZendeskApi_v2.Requests.HelpCenter;

namespace Tests.HelpCenter
{
	[TestFixture]
	public class CommentTests
	{
		private ZendeskApi api = new ZendeskApi(Settings.Site, Settings.Email, Settings.Password);
        private long _articleIdWithComments = 204838115; //https://csharpapi.zendesk.com/hc/en-us/articles/204838115-Thing-4?page=1#comment_200486479

		[Test]
		public void CanGetArticleComments()
		{
            var comments = api.HelpCenter.Comments.GetCommentsForArticle(_articleIdWithComments);

            Assert.IsTrue(comments.Count > 0);
		}

        [Test]
		public void CanGetUserComments()
		{
            var comments = api.HelpCenter.Comments.GetCommentsForUser(Settings.UserId);

            Assert.IsTrue(comments.Count > 0);
		}
	}
}