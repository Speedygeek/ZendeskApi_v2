using ZendeskApi_v2.Models.AccountsAndActivities;


namespace ZendeskApi_v2.Requests
{
    public class AccountsAndActivity : Core
    {

        public AccountsAndActivity(string yourZendeskUrl, string user, string password)
            : base(yourZendeskUrl, user, password)
        {
        }

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

#if NotNet35
        public aysnc Task<SettingsResponse> GetSettingsAsync()
        {
            return await GenericGetAync<SettingsResponse>("account/settings.json");
        }        
        public aysnc Task<GroupActivityResponse> GetActivitiesAync()
        {
            return await GenericGetAync<GroupActivityResponse>("activities.json");
        }

        public aysnc Task<IndividualActivityResponse> GetActivityByIdAync(long activityId)
        {
            return await GenericGetAync<IndividualActivityResponse>(string.Format("activities/{0}.json", activityId));
        }
#endif
    }
}