using NUnit.Framework;
using Tests.Properties;
using ZendeskApi_v2;

namespace Tests.HelpCenter {
	[TestFixture]
    [Category("HelpCenter")]
    public class VoteTests
    {
        private ZendeskApi api = new ZendeskApi(Settings.Default.Site, Settings.Default.Email, Settings.Default.Password);
        private long _articleIdWithVotes = 204838115; //https://csharpapi.zendesk.com/hc/en-us/articles/204838115-Thing-4?page=1#comment_200486479

        [Test]
        public void CanGetArticleVotes()
        {
            var votes = api.HelpCenter.Votes.GetVotesForArticle(_articleIdWithVotes);

            Assert.IsTrue(votes.Count > 0);
        }
    }
}