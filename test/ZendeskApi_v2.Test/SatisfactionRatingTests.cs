using NUnit.Framework;
using ZendeskApi_v2;

namespace Tests
{
    [TestFixture]
    public class SatisfactionRatingTests
    {
        private readonly ZendeskApi api = new ZendeskApi(Settings.Site, Settings.AdminEmail, Settings.AdminPassword);

        [Test]
        public void CanGetBadSatisfactionRatings()
        {
            var receivedSatisfactionRating = api.SatisfactionRatings.GetSatisfactionRatingById(360342335066); //From Ticket 15157
            Assert.That(receivedSatisfactionRating.SatisfactionRating.Score, Is.EqualTo("bad"));
            Assert.That(receivedSatisfactionRating.SatisfactionRating.Comment, Is.EqualTo("poor job!"));
            Assert.That(receivedSatisfactionRating.SatisfactionRating.Reason, Is.EqualTo("The issue was not resolved"));
        }
        
        [Test]
        public void CanGetGoodSatisfactionRatings()
        {
            var receivedSatisfactionRating = api.SatisfactionRatings.GetSatisfactionRatingById(360342335186); //From Ticket 15156
            Assert.That(receivedSatisfactionRating.SatisfactionRating.Score, Is.EqualTo("good"));
            Assert.That(receivedSatisfactionRating.SatisfactionRating.Comment, Is.EqualTo("nice job!"));
            Assert.That(receivedSatisfactionRating.SatisfactionRating.Reason, Is.EqualTo("No reason provided"));
        }
    }
}