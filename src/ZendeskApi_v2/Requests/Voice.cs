using System;
using System.Net;
#if ASYNC
using System.Threading.Tasks;
#endif
using ZendeskApi_v2.Models.Voice;

namespace ZendeskApi_v2.Requests
{
    public interface IVoice : ICore
    {
#if SYNC
        bool OpenUserProfileInAgentBrowser(long agentId, long userId);
        bool OpenTicketInAgentBrowser(long agentId, long ticketId);
        GroupAgentActivityResponse GetVoiceAgentActivity();
        HistoricalQueueActivityDetails GetHistoricalQueueActivity();

#endif

#if ASYNC
        Task<bool> OpenUserProfileInAgentBrowserAsync(long agentId, long userId);
        Task<bool> OpenTicketInAgentBrowserAsync(long agentId, long ticketId);
        Task<GroupAgentActivityResponse> GetVoiceAgentActivityAsync();
        Task<HistoricalQueueActivity> GetHistoricalQueueActivityAsync();
#endif
    }

    class Voice : Core, IVoice
    {
        private const string display = "display";
        private const string channelsVoiceAgent = "channels/voice/agents";
        private const string tickets = "tickets";
        private const string users = "users";
        private const string agentsActivity = "channels/voice/stats/agents_activity";
        private const string historicalQueueActivity = "channels/voice/stats/historical_queue_activity";


        public Voice(string yourZendeskUrl, string user, string password, string apiToken, string p_OAuthToken)
            : base(yourZendeskUrl, user, password, apiToken, p_OAuthToken)
        {
        }

#if SYNC
        public bool OpenUserProfileInAgentBrowser(long agentId, long userId)
        {
            return GenericBoolPost($"{channelsVoiceAgent}/{agentId}/{users}/{userId}/{display}.json");
        }

        public bool OpenTicketInAgentBrowser(long agentId, long ticketId)
        {
            return GenericBoolPost($"{channelsVoiceAgent}/{agentId}/{tickets}/{ticketId}/{display}.json");
        }

        public GroupAgentActivityResponse GetVoiceAgentActivity()
        {
            return GenericGet<GroupAgentActivityResponse>(agentsActivity + ".json");
        }

        public HistoricalQueueActivityDetails GetHistoricalQueueActivity()
        {
            return GenericGet<HistoricalQueueActivity>(historicalQueueActivity + ".json").Details;
        }

#endif

#if ASYNC
        public async Task<bool> OpenUserProfileInAgentBrowserAsync(long agentId, long userId)
        {
            return await GenericBoolPostAsync($"{channelsVoiceAgent}/{agentId}/{users}/{userId}/{display}.json");
        }

        public async Task<bool> OpenTicketInAgentBrowserAsync(long agentId, long ticketId)
        {
            return await GenericBoolPostAsync($"{channelsVoiceAgent}/{agentId}/{tickets}/{ticketId}/{display}.json");
        }

        public async Task<GroupAgentActivityResponse> GetVoiceAgentActivityAsync()
        {
            return await GenericGetAsync<GroupAgentActivityResponse>(agentsActivity + ".json");
        }

        public async Task<HistoricalQueueActivity> GetHistoricalQueueActivityAsync()
        {
            return await GenericGetAsync<HistoricalQueueActivity>(historicalQueueActivity + ".json");
        }
#endif
    }
}
