using NUnit.Framework;
using Tests.Properties;
using ZendeskApi_v2;

namespace Tests {
	[TestFixture]
    [Category("Voice")]
    public class VoiceTests
    {
        private ZendeskApi api = new ZendeskApi(Settings.Default.Site, Settings.Default.Email, Settings.Default.Password);
        private long agentid;
        private long ticketid;
        private long userid;

        [Test]
        public void OpenTicketForAgent()
        {
            agentid = Settings.Default.UserId;
            ticketid = Settings.Default.SampleTicketId;

            var result = api.Voice.OpenTicketInAgentBrowser(agentid, ticketid);
            Assert.IsTrue(result);
        }

        [Test]
        public void OpenTicketTabForAgentAsync()
        {
            agentid = Settings.Default.UserId;
            ticketid = Settings.Default.SampleTicketId;

            var result = api.Voice.OpenTicketInAgentBrowserAsync(agentid, ticketid);
            Assert.IsTrue(result.Result);
        }

        [Test]
        public void OpenUserProfileInAgentBrowser()
        {
            agentid = Settings.Default.UserId;
            userid = Settings.Default.EndUserId;

            var result = api.Voice.OpenUserProfileInAgentBrowser(agentid, userid);
            Assert.IsTrue(result);
        }

        [Test]
        public void OpenUserProfileInAgentBrowserAsync()
        {
            agentid = Settings.Default.UserId;
            userid = Settings.Default.EndUserId;

            var result = api.Voice.OpenUserProfileInAgentBrowserAsync(agentid, userid);
            Assert.IsTrue(result.Result);
        }

    }
}
