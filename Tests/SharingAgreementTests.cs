using NUnit.Framework;
using Tests.Properties;
using ZendeskApi_v2;

namespace Tests
{
    [TestFixture]
    public class SharingAgreementTests
    {
        private ZendeskApi api = new ZendeskApi(Settings.Default.Site, Settings.Default.Email, Settings.Default.Password);

        [Test]
        public void CanGetSharingAgreements()
        {
            var res = api.SharingAgreements.GetSharingAgreements();

            Assert.NotNull(res);
        }
    }
}