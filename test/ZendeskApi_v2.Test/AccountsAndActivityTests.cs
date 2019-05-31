using NUnit.Framework;
using ZendeskApi_v2;

namespace Tests
{
    [TestFixture]
    public class AccountsAndActivityTests
    {
        private readonly ZendeskApi api = new ZendeskApi(Settings.Site, Settings.AdminEmail, Settings.AdminPassword);

        [Test]
        public void CanGetSettings()
        {
            var res = api.AccountsAndActivity.GetSettings();
            Assert.IsNotEmpty(res.Settings.Branding.HeaderColor);
        }
    }
}