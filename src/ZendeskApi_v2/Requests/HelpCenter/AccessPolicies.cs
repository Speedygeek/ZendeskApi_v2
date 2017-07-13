using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if ASYNC
using System.Threading.Tasks;
#endif
using ZendeskApi_v2.Models.AccessPolicies;
using ZendeskApi_v2.Models.Sections;

namespace ZendeskApi_v2.Requests.HelpCenter
{
    public interface IAccessPolicies : ICore
    {
#if SYNC
        IndividualAccessPolicyResponse UpdateSectionAccessPolicy(Section section);
#endif

#if ASYNC
        Task<IndividualAccessPolicyResponse> UpdateSectionAccessPolicyAsync(Section section);
#endif
    }

    public class AccessPolicies : Core, IAccessPolicies
    {
        public AccessPolicies(string yourZendeskUrl, string user, string password, string apiToken, string p_OAuthToken)
            : base(yourZendeskUrl, user, password, apiToken, p_OAuthToken)
        {
        }

#if SYNC
        public IndividualAccessPolicyResponse UpdateSectionAccessPolicy(Section section)
        {
            return GenericPut<IndividualAccessPolicyResponse>($"help_center/sections/{section.Id}/access_policy.json", new { access_policy = section.AccessPolicy });
        }
#endif
#if ASYNC
        public async Task<IndividualAccessPolicyResponse> UpdateSectionAccessPolicyAsync(Section section)
        {
            return await GenericPutAsync<IndividualAccessPolicyResponse>($"help_center/sections/{section.Id}/access_policy.json", new { access_policy = section.AccessPolicy });
        }
#endif
    }
}
