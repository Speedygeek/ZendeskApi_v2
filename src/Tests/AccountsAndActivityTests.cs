using NUnit.Framework;
using ZendeskApi_v2;

namespace Tests
{
    [TestFixture]
    public class AccountsAndActivityTests
    {
        private ZendeskApi api = new ZendeskApi(Settings.Site, Settings.Email, Settings.Password);

        [Test]
        public void CanGetSettings()
        {
            var res = api.AccountsAndActivity.GetSettings();
            Assert.IsNotEmpty(res.Settings.Branding.HeaderColor);
        }

        [Test]
        public void CanGetActivities()
        {
            //the api returns empty objects and I'm not sure how to get it to populate
            var res = api.AccountsAndActivity.GetActivities();

            //var res1 = api.AccountsAndActivity.GetActivityById()
            
        }
    }

    
}