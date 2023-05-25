using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using ZendeskApi_v2.Models.Constants;
using ZendeskApi_v2.Models.Tickets;
using ZendeskApi_v2.Models.Users;
using ZendeskApi_v2.Tests.Base;

namespace ZendeskApi_v2.Tests;

[TestFixture]
[Category("Voice")]
public class VoiceTests : TestBase
{
    private long ticketId;
    private long userId;

    [OneTimeSetUp]
    public async Task SetUp()
    {
        await CleanUp();

        var ticket = new Ticket()
        {
            Subject = "my printer is on fire",
            Comment = new Comment() { Body = "HELP" },
            Priority = TicketPriorities.Urgent,
            ExternalId = TEST_EXTERNAL_ID // marks ticket for auto delete via TestBase.BaseCleanUp
        };

        var respT = await Api.Tickets.CreateTicketAsync(ticket);

        ticketId = respT.Ticket.Id.Value;

        var user = new User()
        {
            Name = "tester voice",
            Email = "testvoice@tester.com",
            Role = "end-user",
            Verified = true,
        };
        var respU = await Api.Users.CreateUserAsync(user);
        userId = respU.User.Id.Value;
    }

    [OneTimeTearDown]
    public async Task CleanUp()
    {
        var groupUserResponse = await Api.Users.SearchByEmailAsync("testvoice@tester.com");
        foreach (var u in groupUserResponse.Users)
        {
            await Api.Users.DeleteUserAsync(u.Id.Value);
        }
    }

    [Test]
    public void OpenTicketForAgent()
    {
        var result = Api.Voice.OpenTicketInAgentBrowser(Admin.ID, ticketId);
        Assert.That(result, Is.True);
    }

    [Test]
    public async Task OpenTicketTabForAgentAsync()
    {
        var result = await Api.Voice.OpenTicketInAgentBrowserAsync(Admin.ID, ticketId);
        Assert.That(result, Is.True);
    }

    [Test]
    public void OpenUserProfileInAgentBrowser()
    {
        var result = Api.Voice.OpenUserProfileInAgentBrowser(Admin.ID, userId);
        Assert.That(result, Is.True);
    }

    [Test]
    public async Task OpenUserProfileInAgentBrowserAsync()
    {
        var result = await Api.Voice.OpenUserProfileInAgentBrowserAsync(Admin.ID, userId);
        Assert.That(result, Is.True);
    }

    [Test]
    public void GetAllAgentAvailability()
    {
        var res = Api.Voice.GetVoiceAgentActivity();

        var agent = res.AgentActivity.FirstOrDefault();
        Assert.That(agent, Is.Not.Null);
        Assert.That(agent.AgentId, Is.EqualTo(2110053086));
    }

    [Test]
    public async Task GetAllAgentAvailabilityAsync()
    {
        var res = await Api.Voice.GetVoiceAgentActivityAsync();

        var agent = res.AgentActivity.FirstOrDefault();
        Assert.That(agent, Is.Not.Null);
        Assert.That(agent.AgentId, Is.Not.Zero);
    }

    [Test]
    public void GetCurrentQueueActivity()
    {
        var res = Api.Voice.GetCurrentQueueActivity();

        Assert.That(res, Is.Not.Null);
    }

    [Test]
    public async Task GetCurrentQueueActivityAsync()
    {
        var res = await Api.Voice.GetCurrentQueueActivityAsync();
        Assert.That(res, Is.Not.Null);
    }

    [Test]
    public void GetAccountOverview()
    {
        var res = Api.Voice.GetAccountOverview();
        Assert.That(res, Is.Not.Null);
    }

    [Test]
    public async Task GetAccountOverviewAsync()
    {
        var res = await Api.Voice.GetAccountOverviewAsync();
        Assert.That(res, Is.Not.Null);
    }
}
