using NUnit.Framework;
using ZendeskApi_v2.Tests.Base;

namespace ZendeskApi_v2.Tests;

[TestFixture]
public class LocaleTests : TestBase
{
    [Test, Ignore("working on issue")]

    public void CanGetLocales()
    {
        var all = Api.Locales.GetAllLocales();
        Assert.That(all.Count, Is.GreaterThan(0));

        var agent = Api.Locales.GetLocalesForAgents();
        Assert.That(agent.Count, Is.GreaterThan(0));

        var specific = Api.Locales.GetLocaleById(all.Locales[0].Id);
        Assert.Multiple(() =>
        {
            Assert.That(all.Locales[0].Id, Is.EqualTo(specific.Locale.Id));
            Assert.That(specific.Locale.Translations, Is.Null);
        });
        var specificWithTranslation = Api.Locales.GetLocaleById(all.Locales[0].Id, true);
        Assert.Multiple(() =>
        {
            Assert.That(all.Locales[0].Id, Is.EqualTo(specificWithTranslation.Locale.Id));
            Assert.That(specificWithTranslation.Locale.Translations, Is.Not.Null);
        });
        var current = Api.Locales.GetCurrentLocale();
        Assert.Multiple(() =>
        {
            Assert.That(current.Locale.Id, Is.GreaterThan(0));
            Assert.That(current.Locale.Translations, Is.Null);
        });
        var currentWithTranslation = Api.Locales.GetCurrentLocale(true);
        Assert.Multiple(() =>
        {
            Assert.That(currentWithTranslation.Locale.Id, Is.GreaterThan(0));
            Assert.That(currentWithTranslation.Locale.Translations, Is.Not.Null);
        });
    }
}