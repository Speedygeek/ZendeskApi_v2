using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ZendeskApi_v2;
using ZendeskApi_v2.Models.Organizations;
using ZendeskApi_v2.Models.Tags;
using ZendeskApi_v2.Models.Users;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class OrganizationTests
    {
        ZendeskApi api = new ZendeskApi(Settings.Site, Settings.Email, Settings.Password);
        [OneTimeSetUp]
        public void Init()
        {
            var orgs = api.Organizations.GetOrganizations();
            if (orgs != null)
            {
                foreach (var org in orgs.Organizations.Where(o => o.Name.Contains("Test Org") || o.Name.Contains("Test Org2")))
                {
                    api.Organizations.DeleteOrganization(org.Id.Value);
                }
            }

            var users = api.Users.SearchByEmail("test_org_mem@test.com");
            if (users != null)
            {
                foreach (var user in users.Users.Where(o => o.Name.Contains("Test User Org Mem")))
                {
                    api.Users.DeleteUser(user.Id.Value);
                }
            }
        }

        [Test]
        public void CanAddAndRemoveTagsFromOrganization()
        {
            var tag = new Tag();
            var organization = api.Organizations.GetOrganizations().Organizations.First();
            tag.Name = "MM";
            organization.Tags.Add(tag.Name);

            var org = api.Organizations.UpdateOrganization(organization);
            org.Organization.Tags.Add("New");

            var org2 = api.Organizations.UpdateOrganization(org.Organization);
            org2.Organization.Tags.Remove("MM");
            org2.Organization.Tags.Remove("New");
            api.Organizations.UpdateOrganization(org2.Organization);
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

            var search = api.Organizations.SearchForOrganizationsByExternalId(Settings.DefaultExternalId);
            Assert.Greater(search.Count, 0);
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

        [Test]
        public void CanCreateAndDeleteOrganizationMemberships()
        {
            var org = api.Organizations.CreateOrganization(new Organization()
            {
                Name = "Test Org"
            });

            var user = new User()
            {
                Name = "Test User Org Mem",
                Email = "test_org_mem@test.com",
                Role = "end-user"
            };


            var res = api.Users.CreateUser(user);

            var org_membership = new OrganizationMembership() { UserId = res.User.Id, OrganizationId = org.Organization.Id };

            var res2 = api.Organizations.CreateOrganizationMembership(org_membership);

            Assert.Greater(res2.OrganizationMembership.Id, 0);
            Assert.True(api.Organizations.DeleteOrganizationMembership(res2.OrganizationMembership.Id.Value));
            Assert.True(api.Users.DeleteUser(res.User.Id.Value));
            Assert.True(api.Organizations.DeleteOrganization(org.Organization.Id.Value));
        }

        [Test]
        public void CanCreateManyAndDeleteOrganizationMemberships()
        {
            var org = api.Organizations.CreateOrganization(new Organization()
            {
                Name = "Test Org"
            });

            Assert.Greater(org.Organization.Id, 0);

            var org2 = api.Organizations.CreateOrganization(new Organization()
            {
                Name = "Test Org2"
            });

            Assert.Greater(org2.Organization.Id, 0);

            var res = api.Users.CreateUser(new User()
            {
                Name = "Test User Org Mem",
                Email = "test_org_mem@test.com",
                Role = "end-user"
            });

            Assert.Greater(res.User.Id, 0);

            var memberships = new List<OrganizationMembership>();
            memberships.Add(new OrganizationMembership() { UserId = res.User.Id, OrganizationId = org.Organization.Id });
            memberships.Add(new OrganizationMembership() { UserId = res.User.Id, OrganizationId = org2.Organization.Id });

            var job = api.Organizations.CreateManyOrganizationMemberships(memberships).JobStatus;

            int sleep = 2000;
            int retries = 0;
            while (!job.Status.Equals("completed") && retries < 7)
            {
                System.Threading.Thread.Sleep(sleep);
                job = api.JobStatuses.GetJobStatus(job.Id).JobStatus;
                sleep = (sleep < 64000 ? sleep *= 2 : 64000);
                retries++;
            }

            Assert.Greater(job.Results.Count(), 0);

            Assert.True(api.Organizations.DeleteOrganizationMembership(job.Results[0].Id));
            Assert.True(api.Organizations.DeleteOrganizationMembership(job.Results[1].Id));

            Assert.True(api.Users.DeleteUser(res.User.Id.Value));
            Assert.True(api.Organizations.DeleteOrganization(org.Organization.Id.Value));
            Assert.True(api.Organizations.DeleteOrganization(org2.Organization.Id.Value));
        }


        [Test]
        public async Task CanSearchForOrganizationsAsync()
        {
            var search = await api.Organizations.SearchForOrganizationsAsync(Settings.DefaultExternalId);
            Assert.Greater(search.Count, 0);
        }
    }
}