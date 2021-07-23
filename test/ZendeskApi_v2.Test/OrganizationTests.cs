using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            Assert.That(res.Count, Is.GreaterThan(0));

            var org = api.Organizations.GetOrganization(res.Organizations[0].Id.Value);
            Assert.That(res.Organizations[0].Id, Is.EqualTo(org.Organization.Id));
        }

        [Test]
        public void CanSearchForOrganizations()
        {
            var res = api.Organizations.GetOrganizationsStartingWith(Settings.DefaultOrg.Substring(0, 3));
            Assert.That(res.Count, Is.GreaterThan(0));

            var search = api.Organizations.SearchForOrganizationsByExternalId(Settings.DefaultExternalId);
            Assert.That(search.Count, Is.GreaterThan(0));
        }

        [Test]
        public void CanGetMultipleOrganizations()
        {
            var org = api.Organizations.CreateOrganization(new Organization()
            {
                Name = "Test Org"
            });

            Assert.That(org.Organization.Id, Is.GreaterThan(0));

            var org2 = api.Organizations.CreateOrganization(new Organization()
            {
                Name = "Test Org2"
            });

            var orgs = api.Organizations.GetMultipleOrganizations(new[] {org.Organization.Id.Value, org2.Organization.Id.Value});
            Assert.That(orgs.Organizations.Count, Is.EqualTo(2));
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

            Assert.That(org.Organization.Id, Is.GreaterThan(0));
            Assert.That(org2.Organization.Id, Is.GreaterThan(0));

            var orgs = api.Organizations.GetMultipleOrganizationsByExternalIds(new[] {org.Organization.ExternalId.ToString(), org2.Organization.ExternalId.ToString()});

            Assert.That(orgs.Organizations.Count, Is.EqualTo(2));
        }

        [Test]
        public void CanCreateUpdateAndDeleteOrganizations()
        {
            var res = api.Organizations.CreateOrganization(new Organization()
            {
                Name = "Test Org"
            });

            Assert.That(res.Organization.Id, Is.GreaterThan(0));

            res.Organization.Notes = "Here is a sample note";
            var update = api.Organizations.UpdateOrganization(res.Organization);
            Assert.That(res.Organization.Notes, Is.EqualTo(update.Organization.Notes));

            Assert.That(api.Organizations.DeleteOrganization(res.Organization.Id.Value), Is.True);
        }

        [Test]
        public void CanCreateOrUpdateOrganizations()
        {
            var res = api.Organizations.CreateOrUpdateOrganization(new Organization()
            {
                Name = "Test Org (original)"
            });

            Assert.That(res.Organization.Id, Is.GreaterThan(0));

            res.Organization.Name = "Test Org (updated)";
            var update = api.Organizations.CreateOrUpdateOrganization(res.Organization);

            Assert.That(update.Organization.Id, Is.EqualTo(res.Organization.Id));
            Assert.That(update.Organization.Name, Is.EqualTo(res.Organization.Name));
            Assert.That(api.Organizations.DeleteOrganization(res.Organization.Id.Value), Is.True);
        }

        [Test]
        public void CanCreateMultipleOrganizations()
        {
            var createJobStatus = api.Organizations.CreateMultipleOrganizations(new[]
            {
                new Organization
                {
                    Name = "Create Multiple Test Org 1"
                },
                new Organization
                {
                    Name = "Create Multiple Test Org 2"
                }
            });

            Assert.That(createJobStatus.JobStatus.Status, Is.EqualTo("queued"));
            JobStatusResponse job;
            do
            {
                Thread.Sleep(1000);
                job = api.JobStatuses.GetJobStatus(createJobStatus.JobStatus.Id);
                Assert.That(job.JobStatus.Id, Is.EqualTo(createJobStatus.JobStatus.Id));

                if (job.JobStatus.Status == "completed") break;
            } while (true);

            Assert.That(job.JobStatus.Results.Count, Is.EqualTo(2));

            foreach (var result in job.JobStatus.Results)
                Assert.That(result.Id, Is.Not.EqualTo(0));
        }

        [Test]
        public async Task CanCreateMultipleOrganizationsAsync()
        {
            var createJobStatus = await api.Organizations.CreateMultipleOrganizationsAsync(new[]
            {
                new Organization
                {
                    Name = "Create Multiple Async Test Org 1"
                },
                new Organization
                {
                    Name = "Create Multiple Async Test Org 2"
                }
            });

            Assert.That(createJobStatus.JobStatus.Status, Is.EqualTo("queued"));
            JobStatusResponse job;
            do
            {
                Thread.Sleep(1000);
                job = await api.JobStatuses.GetJobStatusAsync(createJobStatus.JobStatus.Id);
                Assert.That(job.JobStatus.Id, Is.EqualTo(createJobStatus.JobStatus.Id));

                if (job.JobStatus.Status == "completed") break;
            } while (true);

            Assert.That(job.JobStatus.Results.Count, Is.EqualTo(2));

            foreach (var result in job.JobStatus.Results)
                Assert.That(result.Id, Is.Not.EqualTo(0));
        }

        [Test]
        public async Task CanCreateOrUpdateOrganizationsAsync()
        {
            var res = await api.Organizations.CreateOrUpdateOrganizationAsync(new Organization()
            {
                Name = "Test Org (original)"
            });

            Assert.That(res.Organization.Id, Is.GreaterThan(0));

            res.Organization.Name = "Test Org (updated)";
            var update = await api.Organizations.CreateOrUpdateOrganizationAsync(res.Organization);

            Assert.That(update.Organization.Id, Is.EqualTo(res.Organization.Id));
            Assert.That(update.Organization.Name, Is.EqualTo(res.Organization.Name));
            Assert.That(api.Organizations.DeleteOrganization(res.Organization.Id.Value), Is.True);
        }

        [Test]
        public void CanCreateUpdateAndDeleteMultipleOrganizations()
        {
            var res1 = api.Organizations.CreateOrganization(new Organization()
            {
                Name = "Test Org 1"
            });

            var res2 = api.Organizations.CreateOrganization(new Organization()
            {
                Name = "Test Org 2"
            });

            Assert.That(res1.Organization.Id, Is.GreaterThan(0));
            Assert.That(res2.Organization.Id, Is.GreaterThan(0));

            res1.Organization.Notes = "Here is a sample note 1";
            res2.Organization.Notes = "Here is a sample note 2";

            var organisations = new List<Organization> {res1.Organization, res2.Organization};
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

            var updatedOrganizationIds = new List<long> {res1.Organization.Id.Value, res2.Organization.Id.Value};
            var updatedOrganizations = api.Organizations.GetMultipleOrganizations(updatedOrganizationIds);

            Assert.That(updatedOrganizations.Organizations.FirstOrDefault(o => o.Id == res1.Organization.Id).Notes, Is.EqualTo(res1.Organization.Notes));
            Assert.That(updatedOrganizations.Organizations.FirstOrDefault(o => o.Id == res2.Organization.Id).Notes, Is.EqualTo(res2.Organization.Notes));

            api.Organizations.DeleteOrganization(res1.Organization.Id.Value);
            api.Organizations.DeleteOrganization(res2.Organization.Id.Value);
        }

        [Test]
        public void CanCreateAndDeleteMultipleOrganizationsUsingBulkApis()
        {
            var createJobStatus = api.Organizations.CreateMultipleOrganizations(new[]
            {
                new Organization
                {
                    Name = "Bulk Create Org 1"
                },
                new Organization
                {
                    Name = "Bulk Create Org 2"
                }
            });

            Assert.That(createJobStatus.JobStatus.Status, Is.EqualTo("queued"));
            JobStatusResponse job;
            do
            {
                Thread.Sleep(1000);
                job = api.JobStatuses.GetJobStatus(createJobStatus.JobStatus.Id);
                Assert.That(job.JobStatus.Id, Is.EqualTo(createJobStatus.JobStatus.Id));

                if (job.JobStatus.Status == "completed") break;
            } while (true);

            Assert.That(job.JobStatus.Results.Count, Is.EqualTo(2));

            foreach (var result in job.JobStatus.Results)
                Assert.That(result.Id, Is.Not.EqualTo(0));

            var createdOrgIds = job.JobStatus.Results.Select(r => r.Id).ToList();

            var deleteJobStatus = api.Organizations.DeleteMultipleOrganizations(createdOrgIds);
            Assert.That(deleteJobStatus.JobStatus.Status, Is.EqualTo("queued"));
            do
            {
                Thread.Sleep(1000);
                job = api.JobStatuses.GetJobStatus(createJobStatus.JobStatus.Id);
                Assert.That(job.JobStatus.Id, Is.EqualTo(createJobStatus.JobStatus.Id));

                if (job.JobStatus.Status == "completed") break;
            } while (true);

            //verify they have actually been deleted
            foreach (var orgId in createdOrgIds)
            {
                var exception = Assert.Throws<WebException>(() => api.Organizations.GetOrganization(orgId));
                Assert.That((exception?.Response as HttpWebResponse)?.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            }
        }

        [Test]
        public void CanDeleteMultipleOrganizationsByExternalIds()
        {
            var orgs = new[]
            {
                new Organization
                {
                    Name = "Bulk Create Org 1",
                    ExternalId = "Test Id 1"
                },
                new Organization
                {
                    Name = "Bulk Create Org 2",
                    ExternalId = "External Id 1"
                }
            };
            var createJobStatus = api.Organizations.CreateMultipleOrganizations(orgs);

            Assert.That(createJobStatus.JobStatus.Status, Is.EqualTo("queued"));
            JobStatusResponse job;
            do
            {
                Thread.Sleep(1000);
                job = api.JobStatuses.GetJobStatus(createJobStatus.JobStatus.Id);
                Assert.That(job.JobStatus.Id, Is.EqualTo(createJobStatus.JobStatus.Id));

                if (job.JobStatus.Status == "completed") break;
            } while (true);

            Assert.That(job.JobStatus.Results.Count, Is.EqualTo(2));

            foreach (var result in job.JobStatus.Results)
                Assert.That(result.Id, Is.Not.EqualTo(0));

            var createdOrgIds = job.JobStatus.Results.Select(r => r.Id).ToList();
            var externalIds = orgs.Select(o => o.ExternalId.ToString()).ToList();

            var deleteJobStatus = api.Organizations.DeleteMultipleOrganizationsByExternalIds(externalIds);
            Assert.That(deleteJobStatus.JobStatus.Status, Is.EqualTo("queued"));
            do
            {
                Thread.Sleep(1000);
                job = api.JobStatuses.GetJobStatus(createJobStatus.JobStatus.Id);
                Assert.That(job.JobStatus.Id, Is.EqualTo(createJobStatus.JobStatus.Id));

                if (job.JobStatus.Status == "completed") break;
            } while (true);

            //verify they have actually been deleted
            foreach (var orgId in createdOrgIds)
            {
                var exception = Assert.Throws<WebException>(() => api.Organizations.GetOrganization(orgId));
                Assert.That((exception?.Response as HttpWebResponse)?.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            }
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

            var org_membership = new OrganizationMembership() {UserId = res.User.Id, OrganizationId = org.Organization.Id};

            var res2 = api.Organizations.CreateOrganizationMembership(org_membership);

            Assert.That(res2.OrganizationMembership.Id, Is.GreaterThan(0));
            Assert.That(api.Organizations.DeleteOrganizationMembership(res2.OrganizationMembership.Id.Value), Is.True);
            Assert.That(api.Users.DeleteUser(res.User.Id.Value), Is.True);
            Assert.That(api.Organizations.DeleteOrganization(org.Organization.Id.Value), Is.True);
        }


        [Test]
        [Ignore("Support ticket opend will update when I(Elizabeth) have a fix ")]
        public void CanCreateManyAndDeleteOrganizationMemberships()
        {
            var org = api.Organizations.CreateOrganization(new Organization()
            {
                Name = "Test Org"
            });

            Assert.That(org.Organization.Id, Is.GreaterThan(0));

            var org2 = api.Organizations.CreateOrganization(new Organization()
            {
                Name = "Test Org2"
            });

            Assert.That(org2.Organization.Id, Is.GreaterThan(0));

            var res = api.Users.CreateUser(new User()
            {
                Name = "Test User Org Mem",
                Email = "test_org_mem@test.com",
                Role = "end-user"
            });

            Assert.That(res.User.Id, Is.GreaterThan(0));

            var memberships = new List<OrganizationMembership>
            {
                new OrganizationMembership() {UserId = res.User.Id, OrganizationId = org.Organization.Id},
                new OrganizationMembership() {UserId = res.User.Id, OrganizationId = org2.Organization.Id}
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

            Assert.That(job.Results.Count(), Is.GreaterThan(0));

            Assert.That(api.Organizations.DeleteOrganizationMembership(job.Results[0].Id), Is.True);
            Assert.That(api.Organizations.DeleteOrganizationMembership(job.Results[1].Id), Is.True);

            Assert.That(api.Users.DeleteUser(res.User.Id.Value), Is.True);
            Assert.That(api.Organizations.DeleteOrganization(org.Organization.Id.Value), Is.True);
            Assert.That(api.Organizations.DeleteOrganization(org2.Organization.Id.Value), Is.True);
        }


        [Test]
        public async Task CanSearchForOrganizationsAsync()
        {
            var search = await api.Organizations.SearchForOrganizationsAsync(Settings.DefaultExternalId);
            Assert.That(search.Count, Is.GreaterThan(0));
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
        public async Task CanCreateUpdateAndDeleteMultipleOrganizationsAsync()
        {
            var res1 = await api.Organizations.CreateOrganizationAsync(new Organization()
            {
                Name = "Test Org 1 Async"
            });

            var res2 = await api.Organizations.CreateOrganizationAsync(new Organization()
            {
                Name = "Test Org 2 Async"
            });

            Assert.That(res1.Organization.Id, Is.GreaterThan(0));
            Assert.That(res2.Organization.Id, Is.GreaterThan(0));

            res1.Organization.Notes = "Here is a sample note 1";
            res2.Organization.Notes = "Here is a sample note 2";

            var organisations = new List<Organization> {res1.Organization, res2.Organization};
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

            var updatedOrganizationIds = new List<long> {res1.Organization.Id.Value, res2.Organization.Id.Value};
            var updatedOrganizations = await api.Organizations.GetMultipleOrganizationsAsync(updatedOrganizationIds);

            Assert.That(updatedOrganizations.Organizations.FirstOrDefault(o => o.Id == res1.Organization.Id).Notes, Is.EqualTo(res1.Organization.Notes));
            Assert.That(updatedOrganizations.Organizations.FirstOrDefault(o => o.Id == res2.Organization.Id).Notes, Is.EqualTo(res2.Organization.Notes));

            await api.Organizations.DeleteOrganizationAsync(res1.Organization.Id.Value);
            await api.Organizations.DeleteOrganizationAsync(res2.Organization.Id.Value);
        }

        [Test]
        public async Task CanCreateAndDeleteMultipleOrganizationsUsingBulkApisAsync()
        {
            var createJobStatus = await api.Organizations.CreateMultipleOrganizationsAsync(new[]
            {
                new Organization
                {
                    Name = "Bulk Create Org 1"
                },
                new Organization
                {
                    Name = "Bulk Create Org 2"
                }
            });

            Assert.That(createJobStatus.JobStatus.Status, Is.EqualTo("queued"));
            JobStatusResponse job;
            do
            {
                Thread.Sleep(1000);
                job = await api.JobStatuses.GetJobStatusAsync(createJobStatus.JobStatus.Id);
                Assert.That(job.JobStatus.Id, Is.EqualTo(createJobStatus.JobStatus.Id));

                if (job.JobStatus.Status == "completed") break;
            } while (true);

            Assert.That(job.JobStatus.Results.Count, Is.EqualTo(2));

            foreach (var result in job.JobStatus.Results)
                Assert.That(result.Id, Is.Not.EqualTo(0));

            var createdOrgIds = job.JobStatus.Results.Select(r => r.Id).ToList();

            var deleteJobStatus = await api.Organizations.DeleteMultipleOrganizationsAsync(createdOrgIds);
            Assert.That(deleteJobStatus.JobStatus.Status, Is.EqualTo("queued"));
            do
            {
                Thread.Sleep(1000);
                job = await api.JobStatuses.GetJobStatusAsync(createJobStatus.JobStatus.Id);
                Assert.That(job.JobStatus.Id, Is.EqualTo(createJobStatus.JobStatus.Id));

                if (job.JobStatus.Status == "completed") break;
            } while (true);

            //verify they have actually been deleted
            foreach (var orgId in createdOrgIds)
            {
                var exception = Assert.ThrowsAsync<WebException>(async () => await api.Organizations.GetOrganizationAsync(orgId));
                Assert.That((exception?.Response as HttpWebResponse)?.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            }
        }

        [Test]
        public async Task CanDeleteMultipleOrganizationsByExternalIdsAsync()
        {
            var orgs = new[]
            {
                new Organization
                {
                    Name = "Bulk Create Org 1",
                    ExternalId = "Test Id 1"
                },
                new Organization
                {
                    Name = "Bulk Create Org 2",
                    ExternalId = "External Id 1"
                }
            };
            var createJobStatus = await api.Organizations.CreateMultipleOrganizationsAsync(orgs);

            Assert.That(createJobStatus.JobStatus.Status, Is.EqualTo("queued"));
            JobStatusResponse job;
            do
            {
                Thread.Sleep(1000);
                job = await api.JobStatuses.GetJobStatusAsync(createJobStatus.JobStatus.Id);
                Assert.That(job.JobStatus.Id, Is.EqualTo(createJobStatus.JobStatus.Id));

                if (job.JobStatus.Status == "completed") break;
            } while (true);

            Assert.That(job.JobStatus.Results.Count, Is.EqualTo(2));

            foreach (var result in job.JobStatus.Results)
                Assert.That(result.Id, Is.Not.EqualTo(0));

            var createdOrgIds = job.JobStatus.Results.Select(r => r.Id).ToList();
            var externalIds = orgs.Select(o => o.ExternalId.ToString()).ToList();

            var deleteJobStatus = await api.Organizations.DeleteMultipleOrganizationsByExternalIdsAsync(externalIds);
            Assert.That(deleteJobStatus.JobStatus.Status, Is.EqualTo("queued"));
            do
            {
                Thread.Sleep(1000);
                job = await api.JobStatuses.GetJobStatusAsync(createJobStatus.JobStatus.Id);
                Assert.That(job.JobStatus.Id, Is.EqualTo(createJobStatus.JobStatus.Id));

                if (job.JobStatus.Status == "completed") break;
            } while (true);

            //verify they have actually been deleted
            foreach (var orgId in createdOrgIds)
            {
                var exception = Assert.ThrowsAsync<WebException>(async () => await api.Organizations.GetOrganizationAsync(orgId));
                Assert.That((exception?.Response as HttpWebResponse)?.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            }
        }
    }
}