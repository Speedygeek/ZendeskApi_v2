using NUnit.Framework;
using ZendeskApi_v2;

namespace Tests
{
    [TestFixture]
    public class SharingAgreementTests
    {
        private ZendeskApi api = new ZendeskApi(Settings.Site, Settings.AdminEmail, Settings.AdminPassword);

        [Test]
        public void CanGetSharingAgreements()
        {
            var res = api.SharingAgreements.GetSharingAgreements();

            Assert.NotNull(res);
        }
    }
}