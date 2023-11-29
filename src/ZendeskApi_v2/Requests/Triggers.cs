#if ASYNC
using System.Threading.Tasks;
#endif
using System.Collections.Generic;
using ZendeskApi_v2.Models.Triggers;

namespace ZendeskApi_v2.Requests
{
    public interface ITriggers : ICore
    {
#if SYNC
        GroupTriggerResponse GetTriggers();
        IndividualTriggerResponse GetTriggerById(long id);
        GroupTriggerResponse GetActiveTriggers();
        IndividualTriggerResponse CreateTrigger(Trigger trigger);
        IndividualTriggerResponse UpdateTrigger(Trigger trigger);
        bool DeleteTrigger(long id);
        bool ReorderTriggers(IEnumerable<long> trigger_ids);
#endif

#if ASYNC
        Task<GroupTriggerResponse> GetTriggersAsync();
        Task<IndividualTriggerResponse> GetTriggerByIdAsync(long id);
        Task<GroupTriggerResponse> GetActiveTriggersAsync();
        Task<IndividualTriggerResponse> CreateTriggerAsync(Trigger trigger);
        Task<IndividualTriggerResponse> UpdateTriggerAsync(Trigger trigger);
        Task<bool> DeleteTriggerAsync(long id);
        Task<bool> ReorderTriggersAsync(IEnumerable<long> trigger_ids);
#endif
    }

    public class Triggers : Core, ITriggers
    {
        public Triggers(string yourZendeskUrl, string user, string password, string apiToken, string p_OAuthToken, Dictionary<string,string> customHeaders)
            : base(yourZendeskUrl, user, password, apiToken, p_OAuthToken, customHeaders)
        {
        }

#if SYNC
        public GroupTriggerResponse GetTriggers()
        {
            return GenericGet<GroupTriggerResponse>(string.Format("triggers.json"));
        }

        public IndividualTriggerResponse GetTriggerById(long id)
        {
            return GenericGet<IndividualTriggerResponse>($"triggers/{id}.json");
        }

        public GroupTriggerResponse GetActiveTriggers()
        {
            return GenericGet<GroupTriggerResponse>(string.Format("triggers/active.json"));
        }

        public IndividualTriggerResponse CreateTrigger(Trigger trigger)
        {
            var body = new { trigger };
            return GenericPost<IndividualTriggerResponse>("triggers.json", body);
        }

        public IndividualTriggerResponse UpdateTrigger(Trigger trigger)
        {
            var body = new { trigger };
            return GenericPut<IndividualTriggerResponse>($"triggers/{trigger.Id}.json", body);
        }

        public bool DeleteTrigger(long id)
        {
            return GenericDelete($"triggers/{id}.json");
        }

        public bool ReorderTriggers(IEnumerable<long> trigger_ids)
        {
            var body = new { trigger_ids };
            return GenericBoolPut("triggers/reorder.json", body);
        }
#endif

#if ASYNC    
        public async Task<GroupTriggerResponse> GetTriggersAsync()
        {
            return await GenericGetAsync<GroupTriggerResponse>(string.Format("triggers.json"));
        }

        public async Task<IndividualTriggerResponse> GetTriggerByIdAsync(long id)
        {
            return await GenericGetAsync<IndividualTriggerResponse>($"triggers/{id}.json");
        }

        public async Task<GroupTriggerResponse> GetActiveTriggersAsync()
        {
            return await GenericGetAsync<GroupTriggerResponse>(string.Format("triggers/active.json"));
        }

        public async Task<IndividualTriggerResponse> CreateTriggerAsync(Trigger trigger)
        {
            var body = new { trigger };
            return await GenericPostAsync<IndividualTriggerResponse>("triggers.json", body);
        }

        public async Task<IndividualTriggerResponse> UpdateTriggerAsync(Trigger trigger)
        {
            var body = new { trigger };
            return await GenericPutAsync<IndividualTriggerResponse>($"triggers/{trigger.Id}.json", body);
        }

        public async Task<bool> DeleteTriggerAsync(long id)
        {
            return await GenericDeleteAsync($"triggers/{id}.json");
        }

        public async Task<bool> ReorderTriggersAsync(IEnumerable<long> trigger_ids)
        {
            var body = new { trigger_ids };
            return await GenericBoolPutAsync("triggers/reorder.json", body);
        }
#endif
    }
}
