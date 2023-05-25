using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace ZendeskApi_v2.Tests.Base;

public class TestBase
{
    public const string TEST_EXTERNAL_ID = "clean up after unit test run";
    public UserSettings Admin { get; internal set; }
    public OrganizationSettings Organization { get; set; }
    public ZendeskApi Api { get; internal set; }

    [OneTimeSetUp]
    public void BaseSetUp()
    {
        var configuration = new ConfigurationBuilder().SetBasePath(TestContext.CurrentContext.TestDirectory)
                           .AddJsonFile("appsettings.json", optional: true)
                           .AddUserSecrets("411e2606-2274-427d-ad1e-89c0d4bc9f5a", true)
                           .AddEnvironmentVariables()
                           .Build();

        Admin = configuration.GetSection("admin").Get<UserSettings>();
        Organization = configuration.GetSection("organization").Get<OrganizationSettings>();
        Api = new ZendeskApi(Organization.SiteURL, Admin.Email, Admin.Password);
    }

    [OneTimeTearDown]
    public async Task BaseCleanUp()
    {
        var response = await Api.Tickets.GetTicketsByExternalIdAsync(TEST_EXTERNAL_ID);
        var ids = response.Tickets.Select(t => t.Id.Value).ToList();
        if (ids.Any())
        {
            await Api.Tickets.DeleteMultipleAsync(ids);
        }
    }
}
