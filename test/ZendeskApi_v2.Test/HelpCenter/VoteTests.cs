using NUnit.Framework;
using ZendeskApi_v2;
using ZendeskApi_v2.Models.Articles;
using ZendeskApi_v2.Requests.HelpCenter;

namespace Tests.HelpCenter
{
    [TestFixture]
    [Category("HelpCenter")]
    public class VoteTests
    {
        private ZendeskApi api = new ZendeskApi(Settings.Site, Settings.AdminEmail, Settings.AdminPassword);
        private readonly long _articleIdWithVotes = 360019617932;

        [Test]
        public void CanGetArticleVotes()
        {
            var votes = api.HelpCenter.Votes.GetVotesForArticle(_articleIdWithVotes);

            Assert.That(votes.Count, Is.GreaterThan(0));
        }
    }
}