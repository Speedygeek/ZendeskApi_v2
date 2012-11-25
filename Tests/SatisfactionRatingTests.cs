using NUnit.Framework;
using ZendeskApi_v2;

namespace Tests
{
    [TestFixture]
    public class SatisfactionRatingTests
    {
        private ZendeskApi api = new ZendeskApi(Settings.Site, Settings.Email, Settings.Password);

        [Test]
        public void CanGetSatisfactionRatings()
        {
            //there is no way to create satisfaction ratings through the api so they can't be tested
        }
    }
}