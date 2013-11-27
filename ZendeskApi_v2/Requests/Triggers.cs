#if ASYNC
using System.Threading.Tasks;
#endif
using ZendeskApi_v2.Models.Triggers;

namespace ZendeskApi_v2.Requests
{
    public class Triggers : Core
    {
        public Triggers(string yourZendeskUrl, string user, string password, string apiToken)
            : base(yourZendeskUrl, user, password, apiToken)
        {
        }

#if SYNC
        public GroupTriggerResponse GetTriggers()
        {
            return GenericGet<GroupTriggerResponse>(string.Format("triggers.json"));
        }

        public IndividualTriggerResponse GetTriggerById(long id)
        {
            return GenericGet<IndividualTriggerResponse>(string.Format("triggers/{0}.json", id));
        }

        public GroupTriggerResponse GetActiveTriggers()
        {
            return GenericGet<GroupTriggerResponse>(string.Format("triggers/active.json"));
        }
#endif

#if ASYNC    
        public async Task<GroupTriggerResponse> GetTriggersAsync()
        {
            return await GenericGetAsync<GroupTriggerResponse>(string.Format("triggers.json"));
        }

        public async Task<IndividualTriggerResponse> GetTriggerByIdAsync(long id)
        {
            return await GenericGetAsync<IndividualTriggerResponse>(string.Format("triggers/{0}.json", id));
        }

        public async Task<GroupTriggerResponse> GetActiveTriggersAsync()
        {
            return await GenericGetAsync<GroupTriggerResponse>(string.Format("triggers/active.json"));
        }
#endif
    }
}