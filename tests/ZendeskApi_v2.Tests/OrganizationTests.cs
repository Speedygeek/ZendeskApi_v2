using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ZendeskApi_v2.Models.Organizations;
using ZendeskApi_v2.Models.Shared;
using ZendeskApi_v2.Models.Tags;
using ZendeskApi_v2.Models.Users;
using ZendeskApi_v2.Tests.Base;

namespace ZendeskApi_v2.Tests
{
    [TestFixture]
    public class OrganizationTests : TestBase
    {
        [OneTimeSetUp]
        [OneTimeTearDown]
        public void Init()
        {
            var orgs = Api.Organizations.GetOrganizations();
            if (orgs != null)
            {
                foreach (var org in orgs.Organizations.Where(o => o.Name != "CsharpAPI"))
                {
                    Api.Organizations.DeleteOrganization(org.Id.Value);
                }
            }

            var users = Api.Users.SearchByEmail("test_org_mem@test.com");
            if (users != null)
            {
                foreach (var user in users.Users.Where(o => o.Name.Contains("Test User Org Mem")))
                {
                    Api.Users.DeleteUser(user.Id.Value);
                }
            }
        }

        [Test]
        public void CanAddAndRemoveTagsFromOrganization()
        {
            var tag = new Tag();
            var organization = Api.Organizations.GetOrganizations().Organizations.First();
            tag.Name = "MM";
            organization.Tags.Add(tag.Name);

            var org = Api.Organizations.UpdateOrganization(organization);
            org.Organization.Tags.Add("New");

            var org2 = Api.Organizations.UpdateOrganization(org.Organization);
            org2.Organization.Tags.Remove("MM");
            org2.Organization.Tags.Remove("New");
            Api.Organizations.UpdateOrganization(org2.Organization);
        }

        [Test]
        public void CanGetOrganizations()
        {
            var res = Api.Organizations.GetOrganizations();
            Assert.That(res.Count, Is.GreaterThan(0));

            var org = Api.Organizations.GetOrganization(res.Organizations[0].Id.Value);
            Assert.That(res.Organizations[0].Id, Is.EqualTo(org.Organization.Id));
        }

        [Test]
        public void CanSearchForOrganizations()
        {
            var res = Api.Organizations.GetOrganizationsStartingWith(Organization.Name.Substring(0, 3));
            Assert.That(res.Count, Is.GreaterThan(0));

            var search = Api.Organizations.SearchForOrganizationsByExternalId(Organization.ExternalID);
            Assert.That(search.Count, Is.GreaterThan(0));
        }

        [Test]
        public void CanGetMultipleOrganizations()
        {
            var org = Api.Organizations.CreateOrganization(new Organization()
            {
                Name = "Test Org"
            });

            Assert.That(org.Organization.Id, Is.GreaterThan(0));

            var org2 = Api.Organizations.CreateOrganization(new Organization()
            {
                Name = "Test Org2"
            });

            var orgs = Api.Organizations.GetMultipleOrganizations(new[] { org.Organization.Id.Value, org2.Organization.Id.Value });
            Assert.That(orgs.Organizations.Count, Is.EqualTo(2));
        }

        [Test]
        public void CanGetMultipleOrganizationsByExternalId()
        {
            var org = Api.Organizations.CreateOrganization(new Organization()
            {
                Name = "Test Org with externalId",
                ExternalId = "TestExternalId1"
            });

            var org2 = Api.Organizations.CreateOrganization(new Organization()
            {
                Name = "Test Org2 with externalId",
                ExternalId = "TestExternalId2"
            });

            Assert.That(org.Organization.Id, Is.GreaterThan(0));
            Assert.That(org2.Organization.Id, Is.GreaterThan(0));

            var orgs = Api.Organizations.GetMultipleOrganizationsByExternalIds(new[] { org.Organization.ExternalId, org2.Organization.ExternalId });

            Assert.That(orgs.Organizations.Count, Is.EqualTo(2));
        }

        [Test]
        public void CanCreateUpdateAndDeleteOrganizations()
        {
            var res = Api.Organizations.CreateOrganization(new Organization()
            {
                Name = "Test Org"
            });

            Assert.That(res.Organization.Id, Is.GreaterThan(0));

            res.Organization.Notes = "Here is a sample note";
            var update = Api.Organizations.UpdateOrganization(res.Organization);
            Assert.That(res.Organization.Notes, Is.EqualTo(update.Organization.Notes));

            Assert.That(Api.Organizations.DeleteOrganization(res.Organization.Id.Value), Is.True);
        }

        [Test]
        public void CanCreateOrUpdateOrganizations()
        {
            var res = Api.Organizations.CreateOrUpdateOrganization(new Organization()
            {
                Name = "Test Org (original)"
            });

            Assert.That(res.Organization.Id, Is.GreaterThan(0));

            res.Organization.Name = "Test Org (updated)";
            var update = Api.Organizations.CreateOrUpdateOrganization(res.Organization);

            Assert.That(update.Organization.Id, Is.EqualTo(res.Organization.Id));
            Assert.That(update.Organization.Name, Is.EqualTo(res.Organization.Name));
            Assert.That(Api.Organizations.DeleteOrganization(res.Organization.Id.Value), Is.True);
        }

        [Test]
        public async Task CanCreateOrUpdateOrganizationsAsync()
        {
            var res = await Api.Organizations.CreateOrUpdateOrganizationAsync(new Organization()
            {
                Name = "Test Org (original)"
            });

            Assert.That(res.Organization.Id, Is.GreaterThan(0));

            res.Organization.Name = "Test Org (updated)";
            var update = await Api.Organizations.CreateOrUpdateOrganizationAsync(res.Organization);

            Assert.That(update.Organization.Id, Is.EqualTo(res.Organization.Id));
            Assert.That(update.Organization.Name, Is.EqualTo(res.Organization.Name));
            Assert.That(Api.Organizations.DeleteOrganization(res.Organization.Id.Value), Is.True);
        }

        [Test]
        public void CanCreateUpdateAndDeleteMultipeOrganizations()
        {
            var res1 = Api.Organizations.CreateOrganization(new Organization()
            {
                Name = "Test Org 1"
            });

            var res2 = Api.Organizations.CreateOrganization(new Organization()
            {
                Name = "Test Org 2"
            });

            Assert.That(res1.Organization.Id, Is.GreaterThan(0));
            Assert.That(res2.Organization.Id, Is.GreaterThan(0));

            res1.Organization.Notes = "Here is a sample note 1";
            res2.Organization.Notes = "Here is a sample note 2";

            var organisations = new List<Organization> { res1.Organization, res2.Organization };
            var updateJobStatus = Api.Organizations.UpdateMultipleOrganizations(organisations);

            Assert.That(updateJobStatus.JobStatus.Status, Is.EqualTo("queued"));
            JobStatusResponse job = null;

            do
            {
                Thread.Sleep(5000);
                job = Api.JobStatuses.GetJobStatus(updateJobStatus.JobStatus.Id);
                Assert.That(job.JobStatus.Id, Is.EqualTo(updateJobStatus.JobStatus.Id));

                if (job.JobStatus.Status == "completed") break;
            } while (true);

            var updatedOrganizationIds = new List<long> { res1.Organization.Id.Value, res2.Organization.Id.Value };
            var updatedOrganizations = Api.Organizations.GetMultipleOrganizations(updatedOrganizationIds);

            Assert.That(updatedOrganizations.Organizations.FirstOrDefault(o => o.Id == res1.Organization.Id).Notes, Is.EqualTo(res1.Organization.Notes));
            Assert.That(updatedOrganizations.Organizations.FirstOrDefault(o => o.Id == res2.Organization.Id).Notes, Is.EqualTo(res2.Organization.Notes));

            Api.Organizations.DeleteOrganization(res1.Organization.Id.Value);
            Api.Organizations.DeleteOrganization(res2.Organization.Id.Value);
        }

        [Test]
        public void CanCreateAndDeleteOrganizationMemberships()
        {
            var org = Api.Organizations.CreateOrganization(new Organization()
            {
                Name = "Test Org"
            });

            var user = new User()
            {
                Name = "Test User Org Mem",
                Email = "test_org_mem@test.com",
                Role = "end-user"
            };

            var res = Api.Users.CreateUser(user);

            var org_membership = new OrganizationMembership() { UserId = res.User.Id, OrganizationId = org.Organization.Id };

            var res2 = Api.Organizations.CreateOrganizationMembership(org_membership);

            Assert.That(res2.OrganizationMembership.Id, Is.GreaterThan(0));
            Assert.That(Api.Organizations.DeleteOrganizationMembership(res2.OrganizationMembership.Id.Value), Is.True);
            Assert.That(Api.Users.DeleteUser(res.User.Id.Value), Is.True);
            Assert.That(Api.Organizations.DeleteOrganization(org.Organization.Id.Value), Is.True);
        }

        [Test]
        public async Task CanSearchForOrganizationsAsync()
        {
            var search = await Api.Organizations.SearchForOrganizationsAsync(Organization.ExternalID);
            Assert.That(search.Count, Is.GreaterThan(0));
        }

        [Test]
        public void CanGetIncrementalOrganizationExport()
        {
            var incrementalOrganizationExport = Api.Organizations.GetIncrementalOrganizationExport(Settings.Epoch);
            Assert.That(incrementalOrganizationExport.Organizations.Count, Is.GreaterThan(0));

            var incrementalOrganizationExportNextPage = Api.Organizations.GetIncrementalOrganizationExportNextPage(incrementalOrganizationExport.NextPage);
            Assert.That(incrementalOrganizationExportNextPage.Organizations.Count, Is.GreaterThan(0));
        }

        [Test]
        public async Task CanGetIncrementalOrganizationExportAsync()
        {
            var incrementalOrganizationExport = await Api.Organizations.GetIncrementalOrganizationExportAsync(Settings.Epoch);
            Assert.That(incrementalOrganizationExport.Organizations.Count, Is.GreaterThan(0));

            var incrementalOrganizationExportNextPage = await Api.Organizations.GetIncrementalOrganizationExportNextPageAsync(incrementalOrganizationExport.NextPage);
            Assert.That(incrementalOrganizationExportNextPage.Organizations.Count, Is.GreaterThan(0));
        }

        [Test]
        public async Task CanCreateUpdateAndDeleteMultipeOrganizationsAsync()
        {
            var res1 = await Api.Organizations.CreateOrganizationAsync(new Organization()
            {
                Name = "Test Org 1"
            });

            var res2 = await Api.Organizations.CreateOrganizationAsync(new Organization()
            {
                Name = "Test Org 2"
            });

            Assert.That(res1.Organization.Id, Is.GreaterThan(0));
            Assert.That(res2.Organization.Id, Is.GreaterThan(0));

            res1.Organization.Notes = "Here is a sample note 1";
            res2.Organization.Notes = "Here is a sample note 2";

            var organisations = new List<Organization> { res1.Organization, res2.Organization };
            var updateJobStatus = await Api.Organizations.UpdateMultipleOrganizationsAsync(organisations);

            Assert.That(updateJobStatus.JobStatus.Status, Is.EqualTo("queued"));
            JobStatusResponse job = null;

            do
            {
                Thread.Sleep(5000);
                job = await Api.JobStatuses.GetJobStatusAsync(updateJobStatus.JobStatus.Id);
                Assert.That(job.JobStatus.Id, Is.EqualTo(updateJobStatus.JobStatus.Id));

                if (job.JobStatus.Status == "completed") break;
            } while (true);

            var updatedOrganizationIds = new List<long> { res1.Organization.Id.Value, res2.Organization.Id.Value };
            var updatedOrganizations = await Api.Organizations.GetMultipleOrganizationsAsync(updatedOrganizationIds);

            Assert.That(updatedOrganizations.Organizations.FirstOrDefault(o => o.Id == res1.Organization.Id).Notes, Is.EqualTo(res1.Organization.Notes));
            Assert.That(updatedOrganizations.Organizations.FirstOrDefault(o => o.Id == res2.Organization.Id).Notes, Is.EqualTo(res2.Organization.Notes));

            await Api.Organizations.DeleteOrganizationAsync(res1.Organization.Id.Value);
            await Api.Organizations.DeleteOrganizationAsync(res2.Organization.Id.Value);
        }
    }
}