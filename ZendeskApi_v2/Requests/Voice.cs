using System;
using System.Net;
#if ASYNC
using System.Threading.Tasks;
#endif

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
            return GenericBoolPost(string.Format("{0}/{1}/{2}/{3}/{4}.json", channelsVoiceAgent, agentId, users, userId, display));
        }

        public bool OpenTicketInAgentBrowser(long agentId, long ticketId)
        {
            return GenericBoolPost(string.Format("{0}/{1}/{2}/{3}/{4}.json", channelsVoiceAgent, agentId, tickets, ticketId, display));
        }

#endif

#if ASYNC
        public async Task<bool> OpenUserProfileInAgentBrowserAsync(long agentId, long userId)
        {
            return await GenericBoolPostAsync(string.Format("{0}/{1}/{2}/{3}/{4}.json", channelsVoiceAgent, agentId, users, userId, display));
        }

        public async Task<bool> OpenTicketInAgentBrowserAsync(long agentId, long ticketId)
        {
            return await GenericBoolPostAsync(string.Format("{0}/{1}/{2}/{3}/{4}.json", channelsVoiceAgent, agentId, tickets, ticketId, display));
        }
#endif
    }
}
