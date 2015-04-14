using NUnit.Framework;
using ZendeskApi_v2;
using ZendeskApi_v2.Models.Articles;
using ZendeskApi_v2.Requests.HelpCenter;

namespace Tests.HelpCenter
{
	[TestFixture]
	public class VoteTests
	{
		private ZendeskApi api = new ZendeskApi(Settings.Site, Settings.Email, Settings.Password);
        private long _articleIdWithVotes = 204838115; //https://csharpapi.zendesk.com/hc/en-us/articles/204838115-Thing-4?page=1#comment_200486479

		[Test]
		public void CanGetArticleVotes()
		{
            var votes = api.HelpCenter.Votes.GetVotesForArticle(_articleIdWithVotes);

            Assert.IsTrue(votes.Count > 0);
		}
	}
}