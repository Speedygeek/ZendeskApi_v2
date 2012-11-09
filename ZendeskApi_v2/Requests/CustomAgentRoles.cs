using ZenDeskApi_v2.Models.CustomRoles;

namespace ZenDeskApi_v2.Requests
{
    public class CustomAgentRoles : Core
    {
        public CustomAgentRoles(string yourZenDeskUrl, string user, string password)
            : base(yourZenDeskUrl, user, password)
        {
        }

        public CustomRoles GetCustomRoles()
        {
            return GenericGet<CustomRoles>("custom_roles.json");
        }
    }
}