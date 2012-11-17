using ZenDeskApi_v2.Models.Settings;

namespace ZenDeskApi_v2.Requests
{
    public class Settings : Core
    {

        public Settings(string yourZenDeskUrl, string user, string password)
            : base(yourZenDeskUrl, user, password)
        {
        }

        public SettingsResponse GetSettings()
        {
            return GenericGet<SettingsResponse>("account/settings.json");
        }
    }
}