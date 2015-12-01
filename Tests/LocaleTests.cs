using NUnit.Framework;
using Tests.Properties;
using ZendeskApi_v2;

namespace Tests
{
    [TestFixture]
    public class LocaleTests
    {
        private ZendeskApi api = new ZendeskApi(Settings.Default.Site, Settings.Default.Email, Settings.Default.Password);

        [Test]
        public void CanGetLocales()
        {
            var all = api.Locales.GetAllLocales();
            Assert.Greater(all.Count, 0);

            var agent = api.Locales.GetLocalesForAgents();
            Assert.Greater(agent.Count, 0);

            var specific = api.Locales.GetLocaleById(all.Locales[0].Id);
            Assert.AreEqual(specific.Locale.Id, all.Locales[0].Id);

            var current = api.Locales.GetCurrentLocale();
            Assert.Greater(current.Locale.Id, 0);
        }
    }
}