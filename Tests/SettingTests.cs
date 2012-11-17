using NUnit.Framework;
using ZenDeskApi_v2;

namespace Tests
{
    [TestFixture]
    public class SettingTests
    {
        private ZendeskApi api = new ZendeskApi(Settings.Site, Settings.Email, Settings.Password);

        [Test]
        public void CanGetSettings()
        {
            var res = api.Settings.GetSettings();
            Assert.IsNotEmpty(res.Settings.Branding.HeaderColor);
        }
    }

    
}