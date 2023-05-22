using NUnit.Framework;
using ZendeskApi_v2.Tests.Base;

namespace ZendeskApi_v2.Tests;

[TestFixture]
public class SatisfactionRatingTests : TestBase
{
    [Test]
    public void CanGetBadSatisfactionRatings()
    {
        var receivedSatisfactionRating = Api.SatisfactionRatings.GetSatisfactionRatingById(360342335066); //From Ticket 15157
        Assert.Multiple(() =>
        {
            Assert.That(receivedSatisfactionRating.SatisfactionRating.Score, Is.EqualTo("bad"));
            Assert.That(receivedSatisfactionRating.SatisfactionRating.Comment, Is.EqualTo("poor job!"));
            Assert.That(receivedSatisfactionRating.SatisfactionRating.Reason, Is.EqualTo("The issue was not resolved"));
        });
    }

    [Test]
    public void CanGetGoodSatisfactionRatings()
    {
        var receivedSatisfactionRating = Api.SatisfactionRatings.GetSatisfactionRatingById(360342335186); //From Ticket 15156
        Assert.Multiple(() =>
        {
            Assert.That(receivedSatisfactionRating.SatisfactionRating.Score, Is.EqualTo("good"));
            Assert.That(receivedSatisfactionRating.SatisfactionRating.Comment, Is.EqualTo("nice job!"));
            Assert.That(receivedSatisfactionRating.SatisfactionRating.Reason, Is.EqualTo("No reason provided"));
        });
    }
}