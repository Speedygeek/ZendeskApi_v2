using NUnit.Framework;
using ZendeskApi_v2;

namespace Tests
{
    [TestFixture]
    public class SearchTests
    {
        private ZendeskApi api = new ZendeskApi(Settings.Site, Settings.Email, Settings.Password);

        [Test]
        public void CanSearch()
        {
            var res = api.Search.SearchFor(Settings.Email);

            Assert.AreEqual(res.Results[0].ResultType, "user");            
        }
    }
}