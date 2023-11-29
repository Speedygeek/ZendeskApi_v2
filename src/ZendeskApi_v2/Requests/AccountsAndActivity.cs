#if ASYNC
using System.Collections.Generic;
using System.Threading.Tasks;
#endif
using ZendeskApi_v2.Models.AccountsAndActivities;

namespace ZendeskApi_v2.Requests
{
	public interface IAccountsAndActivity : ICore
	{
#if SYNC
		SettingsResponse GetSettings();
		GroupActivityResponse GetActivities();
		IndividualActivityResponse GetActivityById(long activityId);
#endif

#if ASYNC
		Task<SettingsResponse> GetSettingsAsync();
		Task<GroupActivityResponse> GetActivitiesAsync();
		Task<IndividualActivityResponse> GetActivityByIdAsync(long activityId);
#endif
	}

	public class AccountsAndActivity : Core, IAccountsAndActivity
	{

        public AccountsAndActivity(string yourZendeskUrl, string user, string password, string apiToken, string p_OAuthToken, Dictionary<string,string> customHeaders)
            : base(yourZendeskUrl, user, password, apiToken, p_OAuthToken, customHeaders)
        {
        }
#if SYNC
        public SettingsResponse GetSettings()
        {
            return GenericGet<SettingsResponse>("account/settings.json");
        }

        public GroupActivityResponse GetActivities()
        {
            return GenericGet<GroupActivityResponse>("activities.json");
        }

        public IndividualActivityResponse GetActivityById(long activityId)
        {
            return GenericGet<IndividualActivityResponse>($"activities/{activityId}.json");
        }
#endif

#if ASYNC
        public async Task<SettingsResponse> GetSettingsAsync()
        {
            return await GenericGetAsync<SettingsResponse>("account/settings.json");
        }        
        public async Task<GroupActivityResponse> GetActivitiesAsync()
        {
            return await GenericGetAsync<GroupActivityResponse>("activities.json");
        }

        public async Task<IndividualActivityResponse> GetActivityByIdAsync(long activityId)
        {
            return await GenericGetAsync<IndividualActivityResponse>($"activities/{activityId}.json");
        }
#endif
    }
}
