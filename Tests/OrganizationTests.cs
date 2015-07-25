using NUnit.Framework;
using System.Linq;
using ZendeskApi_v2;
using ZendeskApi_v2.Models.Organizations;

namespace Tests
{
    [TestFixture]
    public class OrganizationTests
    {
        ZendeskApi api = new ZendeskApi(Settings.Site, Settings.Email, Settings.Password);
        [TestFixtureSetUp]
        public void Init()
        {

            var orgs = api.Organizations.GetOrganizations();
            if (orgs != null)
            {
                foreach (var org in orgs.Organizations.Where(o => o.Name.Contains("Test Org")))
                {
                    api.Organizations.DeleteOrganization(org.Id.Value);
                }

            }
        }

        [Test]
        public void CanGetOrganizations()
        {
            var res = api.Organizations.GetOrganizations();
            Assert.Greater(res.Count, 0);

            var org = api.Organizations.GetOrganization(res.Organizations[0].Id.Value);
            Assert.AreEqual(org.Organization.Id, res.Organizations[0].Id);
        }

        [Test]
        public void CanSearchForOrganizations()
        {
            var res = api.Organizations.GetOrganizationsStartingWith(Settings.DefaultOrg.Substring(0, 3));
            Assert.Greater(res.Count, 0);

            var search = api.Organizations.SearchForOrganizations(Settings.DefaultOrg);
            Assert.Greater(res.Count, 0);
        }

        [Test]
        public void CanCreateUpdateAndDeleteOrganizations()
        {
            var res = api.Organizations.CreateOrganization(new Organization()
            {
                Name = "Test Org"
            });

            Assert.Greater(res.Organization.Id, 0);

            res.Organization.Notes = "Here is a sample note";
            var update = api.Organizations.UpdateOrganization(res.Organization);
            Assert.AreEqual(update.Organization.Notes, res.Organization.Notes);

            Assert.True(api.Organizations.DeleteOrganization(res.Organization.Id.Value));
        }
    }
}