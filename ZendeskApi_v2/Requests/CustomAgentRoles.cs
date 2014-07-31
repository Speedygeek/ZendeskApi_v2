#if ASYNC
using System.Threading.Tasks;
#endif
using ZendeskApi_v2.Models.CustomRoles;

namespace ZendeskApi_v2.Requests
{
	public interface ICustomAgentRoles : ICore
	{
#if SYNC
		CustomRoles GetCustomRoles();
#endif

#if ASYNC
		Task<CustomRoles> GetCustomRolesAsync();
#endif
	}

	public class CustomAgentRoles : Core, ICustomAgentRoles
	{
        public CustomAgentRoles(string yourZendeskUrl, string user, string password, string apiToken)
            : base(yourZendeskUrl, user, password, apiToken)
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