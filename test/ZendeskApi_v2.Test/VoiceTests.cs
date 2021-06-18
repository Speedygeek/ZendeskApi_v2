using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ZendeskApi_v2;

namespace Tests
{
    [TestFixture]
    [Category("Voice")]
    public class VoiceTests
    {
        private readonly ZendeskApi api = new ZendeskApi(Settings.Site, Settings.AdminEmail, Settings.AdminPassword);
        private long agentid;
        private long ticketid;
        private long userid;

        [Test]
        public void OpenTicketForAgent()
        {
            agentid = Settings.UserId;
            ticketid = Settings.SampleTicketId;

            var result = api.Voice.OpenTicketInAgentBrowser(agentid, ticketid);
            Assert.That(result, Is.True);
        }

        [Test]
        public void OpenTicketTabForAgentAsync()
        {
            agentid = Settings.UserId;
            ticketid = Settings.SampleTicketId;

            var result = api.Voice.OpenTicketInAgentBrowserAsync(agentid, ticketid);
            Assert.That(result.Result, Is.True);
        }

        [Test]
        public void OpenUserProfileInAgentBrowser()
        {
            agentid = Settings.UserId;
            userid = Settings.EndUserId;

            var result = api.Voice.OpenUserProfileInAgentBrowser(agentid, userid);
            Assert.That(result, Is.True);
        }

        [Test]
        public void OpenUserProfileInAgentBrowserAsync()
        {
            agentid = Settings.UserId;
            userid = Settings.EndUserId;

            var result = api.Voice.OpenUserProfileInAgentBrowserAsync(agentid, userid);
            Assert.That(result.Result, Is.True);
        }

        [Test]
        public void GetAllAgentAvailability()
        {
            var res = api.Voice.GetVoiceAgentActivity();

            var agent = res.AgentActivity.FirstOrDefault();
            Assert.That(agent, Is.Not.Null);
            Assert.That(agent.AgentId, Is.EqualTo(2110053086)); 
        }

        [Test]
        public void GetAllAgentAvailabilityAsync()
        {
            var res = api.Voice.GetVoiceAgentActivityAsync();

            var agent = res.Result.AgentActivity.FirstOrDefault();
            Assert.That(agent, Is.Not.Null);
            Assert.That(agent.AgentId, Is.EqualTo(2110053086)); 
        }
      
        [Test]
        public void GetCurrentQueueActivity()
        {
            var res = api.Voice.GetCurrentQueueActivity();

            Assert.That(res, Is.Not.Null);
        }

        [Test]
        public void GetCurrentQueueActivityAsync()
        {
            var res = api.Voice.GetCurrentQueueActivityAsync();
            Assert.That(res, Is.Not.Null);
        }

        [Test]
        public void GetAccountOverview()
        {
            var res = api.Voice.GetAccountOverview();
            Assert.That(res, Is.Not.Null);
        }

        [Test]
        public void GetAccountOverviewAsync()
        {
            var res = api.Voice.GetAccountOverviewAsync();
            Assert.That(res, Is.Not.Null);
        }

    }
}
