using ZendeskApi_v2.Models.CustomRoles;

namespace ZendeskApi_v2.Requests
{
    public class CustomAgentRoles : Core
    {
        public CustomAgentRoles(string yourZendeskUrl, string user, string password)
            : base(yourZendeskUrl, user, password)
        {
        }

        public CustomRoles GetCustomRoles()
        {
            return GenericGet<CustomRoles>("custom_roles.json");
        }
    }
}