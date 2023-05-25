using NUnit.Framework;
using ZendeskApi_v2.Tests.Base;

namespace ZendeskApi_v2.Tests.HelpCenter;

[TestFixture]
[Category("HelpCenter")]
public class VoteTests : TestBase
{
    private readonly long _articleIdWithVotes = 360019896991; //https://csharpapi.zendesk.com/hc/en-us/articles/204838115-Thing-4?page=1#comment_200486479

    [Test]
    public void CanGetArticleVotes()
    {
        var votes = Api.HelpCenter.Votes.GetVotesForArticle(_articleIdWithVotes);

        Assert.That(votes.Count, Is.GreaterThan(0));
    }
}