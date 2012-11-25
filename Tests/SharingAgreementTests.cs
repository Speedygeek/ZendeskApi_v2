using NUnit.Framework;
using ZendeskApi_v2;

namespace Tests
{
    [TestFixture]
    public class SharingAgreementTests
    {
        private ZendeskApi api = new ZendeskApi(Settings.Site, Settings.Email, Settings.Password);

        [Test]
        public void CanGetSharingAgreements()
        {
            var res = api.SharingAgreements.GetSharingAgreements();

            Assert.NotNull(res);
        }
    }
}