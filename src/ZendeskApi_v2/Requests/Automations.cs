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
        GroupAutomationResponse SearchAutomations(string query);
        IndividualAutomationResponse CreateAutomation(Automation Automation);
        IndividualAutomationResponse UpdateAutomation(Automation Automation);
        bool DeleteAutomation(long id);
#endif

#if ASYNC
        Task<GroupAutomationResponse> GetAutomationsAsync();
        Task<IndividualAutomationResponse> GetAutomationByIdAsync(long id);
        Task<GroupAutomationResponse> GetActiveAutomationsAsync();
        Task<GroupAutomationResponse> SearchAutomationsAsync(string query);
        Task<IndividualAutomationResponse> CreateAutomationAsync(Automation Automation);
        Task<IndividualAutomationResponse> UpdateAutomationAsync(Automation Automation);
        Task<bool> DeleteAutomationAsync(long id);
#endif
    }

    public class Automations : Core, IAutomations
    {
        public Automations(string yourZendeskUrl, string user, string password, string apiToken, string p_OAuthToken, Dictionary<string,string> customHeaders)
            : base(yourZendeskUrl, user, password, apiToken, p_OAuthToken, customHeaders)
        {
        }

#if SYNC
        public GroupAutomationResponse GetAutomations()
        {
            return GenericGet<GroupAutomationResponse>(string.Format("automations.json"));
        }

        public IndividualAutomationResponse GetAutomationById(long id)
        {
            return GenericGet<IndividualAutomationResponse>($"automations/{id}.json");
        }

        public GroupAutomationResponse GetActiveAutomations()
        {
            return GenericGet<GroupAutomationResponse>(string.Format("automations/active.json"));
        }

        public GroupAutomationResponse SearchAutomations(string query)
        {
            return GenericGet<GroupAutomationResponse>(string.Format($"automations/search.json?query={query}"));
        }

        public IndividualAutomationResponse CreateAutomation(Automation Automation)
        {
            var body = new { Automation };
            return GenericPost<IndividualAutomationResponse>("automations.json", body);
        }

        public IndividualAutomationResponse UpdateAutomation(Automation Automation)
        {
            var body = new { Automation };
            return GenericPut<IndividualAutomationResponse>($"automations/{Automation.Id}.json", body);
        }

        public bool DeleteAutomation(long id)
        {
            return GenericDelete($"automations/{id}.json");
        }
#endif

#if ASYNC    
        public async Task<GroupAutomationResponse> GetAutomationsAsync()
        {
            return await GenericGetAsync<GroupAutomationResponse>(string.Format("automations.json"));
        }

        public async Task<IndividualAutomationResponse> GetAutomationByIdAsync(long id)
        {
            return await GenericGetAsync<IndividualAutomationResponse>($"automations/{id}.json");
        }

        public async Task<GroupAutomationResponse> GetActiveAutomationsAsync()
        {
            return await GenericGetAsync<GroupAutomationResponse>(string.Format("automations/active.json"));
        }

        public async Task<GroupAutomationResponse> SearchAutomationsAsync(string query)
        {
            return await GenericGetAsync<GroupAutomationResponse>(string.Format($"automations/search.json?query={query}"));
        }

        public async Task<IndividualAutomationResponse> CreateAutomationAsync(Automation Automation)
        {
            var body = new { Automation };
            return await GenericPostAsync<IndividualAutomationResponse>("automations.json", body);
        }

        public async Task<IndividualAutomationResponse> UpdateAutomationAsync(Automation Automation)
        {
            var body = new { Automation };
            return await GenericPutAsync<IndividualAutomationResponse>($"automations/{Automation.Id}.json", body);
        }

        public async Task<bool> DeleteAutomationAsync(long id)
        {
            return await GenericDeleteAsync($"automations/{id}.json");
        }
#endif
    }
}
