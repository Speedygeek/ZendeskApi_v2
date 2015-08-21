using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ZendeskApi_v2.Requests
{
    public interface IVoice : ICore
    {
        #if SYNC
        bool OpenUserProfileInAgentBrowser(long agentId, long userId);
        bool OpenTicketInAgentBrowser(long agentId, long ticketId);
        #endif
        
        #if ASYNC
        Task<bool> OpenUserProfileInAgentBrowserAsync(long agentId, long userId);
        Task<bool> OpenTicketInAgentBrowserAsync(long agentId, long ticketId);
        #endif
    }

    class Voice : Core, IVoice
    {
        private const string display = "display";
        private const string channelsVoiceAgent = "channels/voice/agents";
        private const string tickets = "tickets";
        private const string users = "users";

        public Voice(string yourZendeskUrl, string user, string password, string apiToken, string p_OAuthToken)
            : base(yourZendeskUrl, user, password, apiToken, p_OAuthToken)
        {
        }

        #if SYNC
        public bool OpenUserProfileInAgentBrowser(long agentId, long userId)
        {
            var result = RunRequest(string.Format("{0}/{1}/{2}/{3}/{4}.json", channelsVoiceAgent, agentId, users, userId, display), "POST");
            if (result.HttpStatusCode == HttpStatusCode.OK)
                return true;
            else
                return false;

        }

        public bool OpenTicketInAgentBrowser(long agentId, long ticketId)
        {
            var result = RunRequest(string.Format("{0}/{1}/{2}/{3}/{4}.json", channelsVoiceAgent, agentId, tickets, ticketId, display), "POST");
            if (result.HttpStatusCode == HttpStatusCode.OK)
                return true;
            else
                return false;
        }

        #endif

        #if ASYNC
        public async Task<bool> OpenUserProfileInAgentBrowserAsync(long agentId, long userId)
        {
            var result = await RunRequestAsync(string.Format("{0}/{1}/{2}/{3}/{4}.json", channelsVoiceAgent, agentId, users, userId, display), "POST");
            if (result.HttpStatusCode == HttpStatusCode.OK)
                return true;
            else
                return false;
        }

        public async Task<bool> OpenTicketInAgentBrowserAsync(long agentId, long ticketId)
        {
            var result = await RunRequestAsync(string.Format("{0}/{1}/{2}/{3}/{4}.json", channelsVoiceAgent, agentId, tickets, ticketId, display), "POST");
            if (result.HttpStatusCode == HttpStatusCode.OK)
                return true;
            else
                return false;
        }
        #endif
    }
}
