using NUnit.Framework;
using ZendeskApi_v2.Tests.Base;

namespace ZendeskApi_v2.Tests;

[TestFixture]
public class AccountsAndActivityTests : TestBase
{
    [Test]
    public void CanGetSettings()
    {
        var res = Api.AccountsAndActivity.GetSettings();
        Assert.That(res.Settings.Branding.HeaderColor, Is.Not.Empty);
    }
}
