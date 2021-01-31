using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using NUnit.Framework;
using ZendeskApi_v2;
using ZendeskApi_v2.Models.Organizations;
using ZendeskApi_v2.Models.Shared;
using ZendeskApi_v2.Models.Tags;
using ZendeskApi_v2.Models.Users;

namespace Tests
{
    [TestFixture]
    public class OrganizationTests
    {
        readonly ZendeskApi api = new ZendeskApi(Settings.Site, Settings.AdminEmail, Settings.AdminPassword);

        [OneTimeSetUp]
        [OneTimeTearDown]
        public void Init()
        {
            var orgs = api.Organizations.GetOrganizations();
            if (orgs != null)
            {
                foreach (var org in orgs.Organizations.Where(o => o.Name != "CsharpAPI"))
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
        public void CanGetMultipleOrganizations()
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

            var orgs = api.Organizations.GetMultipleOrganizations(new[] { org.Organization.Id.Value, org2.Organization.Id.Value });
            Assert.AreEqual(orgs.Organizations.Count, 2);
        }

        [Test]
        public void CanGetMultipleOrganizationsByExternalId()
        {
            var org = api.Organizations.CreateOrganization(new Organization()
            {
                Name = "Test Org with externalId",
                ExternalId = "TestExternalId1"
            });

            var org2 = api.Organizations.CreateOrganization(new Organization()
            {
                Name = "Test Org2 with externalId",
                ExternalId = "TestExternalId2"
            });

            Assert.Greater(org.Organization.Id, 0);
            Assert.Greater(org2.Organization.Id, 0);

            var orgs = api.Organizations.GetMultipleOrganizationsByExternalIds(new[] { org.Organization.ExternalId.ToString(), org2.Organization.ExternalId.ToString() });

            Assert.AreEqual(orgs.Organizations.Count, 2);
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
        public void CanCreateUpdateAndDeleteMultipeOrganizations()
        {
            var res1 = api.Organizations.CreateOrganization(new Organization()
            {
                Name = "Test Org 1"
            });

            var res2 = api.Organizations.CreateOrganization(new Organization()
            {
                Name = "Test Org 2"
            });

            Assert.Greater(res1.Organization.Id, 0);
            Assert.Greater(res2.Organization.Id, 0);

            res1.Organization.Notes = "Here is a sample note 1";
            res2.Organization.Notes = "Here is a sample note 2";

            var organisations = new List<Organization> { res1.Organization, res2.Organization };
            var updateJobStatus = api.Organizations.UpdateMultipleOrganizations(organisations);

            Assert.That(updateJobStatus.JobStatus.Status, Is.EqualTo("queued"));
            JobStatusResponse job = null;

            do
            {
                Thread.Sleep(5000);
                job = api.JobStatuses.GetJobStatus(updateJobStatus.JobStatus.Id);
                Assert.That(job.JobStatus.Id, Is.EqualTo(updateJobStatus.JobStatus.Id));

                if (job.JobStatus.Status == "completed") break;
            } while (true);

            var updatedOrganizationIds = new List<long> { res1.Organization.Id.Value, res2.Organization.Id.Value };
            var updatedOrganizations = api.Organizations.GetMultipleOrganizations(updatedOrganizationIds);

            Assert.That(updatedOrganizations.Organizations.FirstOrDefault(o => o.Id == res1.Organization.Id).Notes, Is.EqualTo(res1.Organization.Notes));
            Assert.That(updatedOrganizations.Organizations.FirstOrDefault(o => o.Id == res2.Organization.Id).Notes, Is.EqualTo(res2.Organization.Notes));

            api.Organizations.DeleteOrganization(res1.Organization.Id.Value);
            api.Organizations.DeleteOrganization(res2.Organization.Id.Value);
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
        [Ignore("Support ticket opend will update when I(Elizabeth) have a fix ")]
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

            var memberships = new List<OrganizationMembership>
            {
                new OrganizationMembership() { UserId = res.User.Id, OrganizationId = org.Organization.Id },
                new OrganizationMembership() { UserId = res.User.Id, OrganizationId = org2.Organization.Id }
            };

            var job = api.Organizations.CreateManyOrganizationMemberships(memberships).JobStatus;

            var sleep = 2000;
            var retries = 0;
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

        [Test]
        public void CanGetIncrementalOrganizationExport()
        {
            var incrementalOrganizationExport = api.Organizations.GetIncrementalOrganizationExport(Settings.Epoch);
            Assert.That(incrementalOrganizationExport.Organizations.Count, Is.GreaterThan(0));

            var incrementalOrganizationExportNextPage = api.Organizations.GetIncrementalOrganizationExportNextPage(incrementalOrganizationExport.NextPage);
            Assert.That(incrementalOrganizationExportNextPage.Organizations.Count, Is.GreaterThan(0));
        }

        [Test]
        public async Task CanGetIncrementalOrganizationExportAsync()
        {
            var incrementalOrganizationExport = await api.Organizations.GetIncrementalOrganizationExportAsync(Settings.Epoch);
            Assert.That(incrementalOrganizationExport.Organizations.Count, Is.GreaterThan(0));

            var incrementalOrganizationExportNextPage = await api.Organizations.GetIncrementalOrganizationExportNextPageAsync(incrementalOrganizationExport.NextPage);
            Assert.That(incrementalOrganizationExportNextPage.Organizations.Count, Is.GreaterThan(0));
        }

        [Test]
        public async Task CanCreateUpdateAndDeleteMultipeOrganizationsAsync()
        {
            var res1 = await api.Organizations.CreateOrganizationAsync(new Organization()
            {
                Name = "Test Org 1"
            });

            var res2 = await api.Organizations.CreateOrganizationAsync(new Organization()
            {
                Name = "Test Org 2"
            });

            Assert.Greater(res1.Organization.Id, 0);
            Assert.Greater(res2.Organization.Id, 0);

            res1.Organization.Notes = "Here is a sample note 1";
            res2.Organization.Notes = "Here is a sample note 2";

            var organisations = new List<Organization> { res1.Organization, res2.Organization };
            var updateJobStatus = await api.Organizations.UpdateMultipleOrganizationsAsync(organisations);

            Assert.That(updateJobStatus.JobStatus.Status, Is.EqualTo("queued"));
            JobStatusResponse job = null;

            do
            {
                Thread.Sleep(5000);
                job = await api.JobStatuses.GetJobStatusAsync(updateJobStatus.JobStatus.Id);
                Assert.That(job.JobStatus.Id, Is.EqualTo(updateJobStatus.JobStatus.Id));

                if (job.JobStatus.Status == "completed") break;
            } while (true);

            var updatedOrganizationIds = new List<long> { res1.Organization.Id.Value, res2.Organization.Id.Value };
            var updatedOrganizations = await api.Organizations.GetMultipleOrganizationsAsync(updatedOrganizationIds);

            Assert.That(updatedOrganizations.Organizations.FirstOrDefault(o => o.Id == res1.Organization.Id).Notes, Is.EqualTo(res1.Organization.Notes));
            Assert.That(updatedOrganizations.Organizations.FirstOrDefault(o => o.Id == res2.Organization.Id).Notes, Is.EqualTo(res2.Organization.Notes));

            await api.Organizations.DeleteOrganizationAsync(res1.Organization.Id.Value);
            await api.Organizations.DeleteOrganizationAsync(res2.Organization.Id.Value);
        }
    }
}