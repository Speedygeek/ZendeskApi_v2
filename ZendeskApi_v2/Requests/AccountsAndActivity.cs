using ZenDeskApi_v2.Models.AccountsAndActivities;


namespace ZenDeskApi_v2.Requests
{
    public class AccountsAndActivity : Core
    {

        public AccountsAndActivity(string yourZenDeskUrl, string user, string password)
            : base(yourZenDeskUrl, user, password)
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
    }
}