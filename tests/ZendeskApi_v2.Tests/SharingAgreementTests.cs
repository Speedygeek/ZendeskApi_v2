using NUnit.Framework;
using ZendeskApi_v2.Tests.Base;

namespace ZendeskApi_v2.Tests;

[TestFixture]
public class SharingAgreementTests : TestBase
{
    [Test]
    public void CanGetSharingAgreements()
    {
        var res = Api.SharingAgreements.GetSharingAgreements();

        Assert.That(res, Is.Not.Null);
    }
}