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

    internal class Voice : Core, IVoice
    {
        private const string Display = "display";
        private const string ChannelsVoiceAgent = "channels/voice/agents";
        private const string Tickets = "tickets";
        private const string Users = "users";
        private const string AgentsActivity = "channels/voice/stats/agents_activity";
        private const string HistoricalQueueActivity = "channels/voice/stats/historical_queue_activity";

        public Voice(string yourZendeskUrl, string user, string password, string apiToken, string p_OAuthToken)
            : base(yourZendeskUrl, user, password, apiToken, p_OAuthToken)
        {
        }

#if SYNC

        public bool OpenUserProfileInAgentBrowser(long agentId, long userId)
        {
            return GenericBoolPost(string.Format("{0}/{1}/{2}/{3}/{4}.json", ChannelsVoiceAgent, agentId, Users, userId, Display));
        }

        public bool OpenTicketInAgentBrowser(long agentId, long ticketId)
        {
            return GenericBoolPost(string.Format("{0}/{1}/{2}/{3}/{4}.json", ChannelsVoiceAgent, agentId, Tickets, ticketId, Display));
        }

        public GroupAgentActivityResponse GetVoiceAgentActivity()
        {
            return GenericGet<GroupAgentActivityResponse>(AgentsActivity + ".json");
        }

        public HistoricalQueueActivityDetails GetHistoricalQueueActivity()
        {
            return GenericGet<HistoricalQueueActivity>(HistoricalQueueActivity + ".json").Details;
        }

#endif

#if ASYNC

        public async Task<bool> OpenUserProfileInAgentBrowserAsync(long agentId, long userId)
        {
            return await GenericBoolPostAsync(string.Format("{0}/{1}/{2}/{3}/{4}.json", ChannelsVoiceAgent, agentId, Users, userId, Display));
        }

        public async Task<bool> OpenTicketInAgentBrowserAsync(long agentId, long ticketId)
        {
            return await GenericBoolPostAsync(string.Format("{0}/{1}/{2}/{3}/{4}.json", ChannelsVoiceAgent, agentId, Tickets, ticketId, Display));
        }

        public async Task<GroupAgentActivityResponse> GetVoiceAgentActivityAsync()
        {
            return await GenericGetAsync<GroupAgentActivityResponse>(AgentsActivity + ".json");
        }

        public async Task<HistoricalQueueActivity> GetHistoricalQueueActivityAsync()
        {
            return await GenericGetAsync<HistoricalQueueActivity>(HistoricalQueueActivity + ".json");
        }

#endif
    }
}