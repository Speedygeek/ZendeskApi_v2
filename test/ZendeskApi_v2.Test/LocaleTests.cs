using NUnit.Framework;
using ZendeskApi_v2;

namespace Tests
{
    [TestFixture]
    public class LocaleTests
    {
        private readonly ZendeskApi api = new ZendeskApi(Settings.Site, Settings.AdminEmail, Settings.AdminPassword);

        [Test]
        public void CanGetLocales()
        {
            var all = api.Locales.GetAllLocales();
            Assert.That(all.Count, Is.GreaterThan(0));

            var agent = api.Locales.GetLocalesForAgents();
            Assert.That(agent.Count, Is.GreaterThan(0));

            var specific = api.Locales.GetLocaleById(all.Locales[0].Id);
            Assert.That(all.Locales[0].Id, Is.EqualTo(specific.Locale.Id));
            Assert.IsNull(specific.Locale.Translations);

            var specificWithTranslation = api.Locales.GetLocaleById(all.Locales[0].Id, true);
            Assert.That(all.Locales[0].Id, Is.EqualTo(specificWithTranslation.Locale.Id));
            Assert.That(specificWithTranslation.Locale.Translations, Is.Not.Null);

            var current = api.Locales.GetCurrentLocale();
            Assert.That(current.Locale.Id, Is.GreaterThan(0));
            Assert.IsNull(current.Locale.Translations);

            var currentWithTranslation = api.Locales.GetCurrentLocale(true);
            Assert.That(currentWithTranslation.Locale.Id, Is.GreaterThan(0));
            Assert.That(currentWithTranslation.Locale.Translations, Is.Not.Null);
        }
    }
}