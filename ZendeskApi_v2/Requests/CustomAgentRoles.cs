#if ASYNC
using System.Threading.Tasks;
#endif
using ZendeskApi_v2.Models.CustomRoles;

namespace ZendeskApi_v2.Requests
{
    public class CustomAgentRoles : Core
    {
        public CustomAgentRoles(string yourZendeskUrl, string user, string password)
            : base(yourZendeskUrl, user, password)
        {
        }

#if SYNC
        public CustomRoles GetCustomRoles()
        {
            return GenericGet<CustomRoles>("custom_roles.json");
        }
#endif

#if ASYNC
        public async Task<CustomRoles> GetCustomRolesAsync()
        {
            return await GenericGetAsync<CustomRoles>("custom_roles.json");
        }
#endif
    }
}