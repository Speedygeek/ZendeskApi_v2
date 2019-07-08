using System.Collections.Generic;

#if ASYNC

using System.Threading.Tasks;

#endif

using ZendeskApi_v2.Models.Automations;

namespace ZendeskApi_v2.Requests
{
    public interface IAutomations : ICore
    {
#if SYNC
        GroupAutomationResponse GetAutomations();
        IndividualAutomationResponse GetAutomationById(long id);
        GroupAutomationResponse GetActiveAutomations();
        IndividualAutomationResponse CreateAutomation(Automation Automation);
        IndividualAutomationResponse UpdateAutomation(Automation Automation);
        bool DeleteAutomation(long id);
        bool ReorderAutomations(IEnumerable<long> Automation_ids);
#endif

#if ASYNC
        Task<GroupAutomationResponse> GetAutomationsAsync();
        Task<IndividualAutomationResponse> GetAutomationByIdAsync(long id);
        Task<GroupAutomationResponse> GetActiveAutomationsAsync();
        Task<IndividualAutomationResponse> CreateAutomationAsync(Automation Automation);
        Task<IndividualAutomationResponse> UpdateAutomationAsync(Automation Automation);
        Task<bool> DeleteAutomationAsync(long id);
        Task<bool> ReorderAutomationsAsync(IEnumerable<long> Automation_ids);
#endif
    }

    public class Automations : Core, IAutomations
    {
        public Automations(string yourZendeskUrl, string user, string password, string apiToken, string p_OAuthToken)
            : base(yourZendeskUrl, user, password, apiToken, p_OAuthToken)
        {
        }

#if SYNC
        public GroupAutomationResponse GetAutomations()
        {
            return GenericGet<GroupAutomationResponse>(string.Format("Automations.json"));
        }

        public IndividualAutomationResponse GetAutomationById(long id)
        {
            return GenericGet<IndividualAutomationResponse>($"Automations/{id}.json");
        }

        public GroupAutomationResponse GetActiveAutomations()
        {
            return GenericGet<GroupAutomationResponse>(string.Format("Automations/active.json"));
        }

        public IndividualAutomationResponse CreateAutomation(Automation Automation)
        {
            var body = new { Automation };
            return GenericPost<IndividualAutomationResponse>("Automations.json", body);
        }

        public IndividualAutomationResponse UpdateAutomation(Automation Automation)
        {
            var body = new { Automation };
            return GenericPut<IndividualAutomationResponse>($"Automations/{Automation.Id}.json", body);
        }

        public bool DeleteAutomation(long id)
        {
            return GenericDelete($"Automations/{id}.json");
        }

        public bool ReorderAutomations(IEnumerable<long> Automation_ids)
        {
            var body = new { Automation_ids };
            return GenericBoolPut("Automations/reorder.json", body);
        }
#endif

#if ASYNC    
        public async Task<GroupAutomationResponse> GetAutomationsAsync()
        {
            return await GenericGetAsync<GroupAutomationResponse>(string.Format("Automations.json"));
        }

        public async Task<IndividualAutomationResponse> GetAutomationByIdAsync(long id)
        {
            return await GenericGetAsync<IndividualAutomationResponse>($"Automations/{id}.json");
        }

        public async Task<GroupAutomationResponse> GetActiveAutomationsAsync()
        {
            return await GenericGetAsync<GroupAutomationResponse>(string.Format("Automations/active.json"));
        }

        public async Task<IndividualAutomationResponse> CreateAutomationAsync(Automation Automation)
        {
            var body = new { Automation };
            return await GenericPostAsync<IndividualAutomationResponse>("Automations.json", body);
        }

        public async Task<IndividualAutomationResponse> UpdateAutomationAsync(Automation Automation)
        {
            var body = new { Automation };
            return await GenericPutAsync<IndividualAutomationResponse>($"Automations/{Automation.Id}.json", body);
        }

        public async Task<bool> DeleteAutomationAsync(long id)
        {
            return await GenericDeleteAsync($"Automations/{id}.json");
        }

        public async Task<bool> ReorderAutomationsAsync(IEnumerable<long> Automation_ids)
        {
            var body = new { Automation_ids };
            return await GenericBoolPutAsync("Automations/reorder.json", body);
        }
#endif
    }
}
