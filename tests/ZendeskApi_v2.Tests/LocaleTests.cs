using NUnit.Framework;
using ZendeskApi_v2.Tests.Base;

namespace ZendeskApi_v2.Tests;

[TestFixture]
public class LocaleTests : TestBase
{
    //[Test, Ignore("working on issue")]
    [Test]
    public void CanGetLocales()
    {
        var all = Api.Locales.GetAllLocales();
        Assert.That(all.Count, Is.GreaterThan(0));

        var agent = Api.Locales.GetLocalesForAgents();
        Assert.That(agent.Count, Is.GreaterThan(0));

        var specific = Api.Locales.GetLocaleById(all.Locales[0].Id);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(all.Locales[0].Id, Is.EqualTo(specific.Locale.Id));
        }
        var current = Api.Locales.GetCurrentLocale();
        using (Assert.EnterMultipleScope())
        {
            Assert.That(current.Locale.Id, Is.GreaterThan(0));
        }
    }
}