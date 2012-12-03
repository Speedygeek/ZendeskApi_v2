#if ASYNC
using System.Threading.Tasks;
#endif
using ZendeskApi_v2.Models.AccountsAndActivities;


namespace ZendeskApi_v2.Requests
{
    public class AccountsAndActivity : Core
    {

        public AccountsAndActivity(string yourZendeskUrl, string user, string password)
            : base(yourZendeskUrl, user, password)
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
            return GenericGet<IndividualActivityResponse>(string.Format("activities/{0}.json", activityId));
        }
#endif

#if ASYNC
        public async Task<SettingsResponse> GetSettingsAsync()
        {
            return await GenericGetAsync<SettingsResponse>("account/settings.json");
        }        
        public async Task<GroupActivityResponse> GetActivitiesAync()
        {
            return await GenericGetAsync<GroupActivityResponse>("activities.json");
        }

        public async Task<IndividualActivityResponse> GetActivityByIdAync(long activityId)
        {
            return await GenericGetAsync<IndividualActivityResponse>(string.Format("activities/{0}.json", activityId));
        }
#endif
    }
}