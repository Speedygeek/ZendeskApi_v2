#if ASYNC
using System.Threading.Tasks;
#endif
using ZendeskApi_v2.Models.CustomRoles;

namespace ZendeskApi_v2.Requests
{
    public class CustomAgentRoles : Core
    {
        internal CustomAgentRoles(IZendeskConnectionSettings connectionSettings)
            : base(connectionSettings)
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