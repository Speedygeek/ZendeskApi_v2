using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var body = new { access_policy = section.AccessPolicy };
            return GenericPut<IndividualAccessPolicyResponse>(string.Format("/api/v2/help_center/sections/{0}/access_policy.json", section.Id), body);
        }
#endif
#if ASYNC
        public async Task<IndividualAccessPolicyResponse> UpdateSectionAccessPolicyAsync(Section section)
        {
            var body = new { access_policy = section.AccessPolicy };
            return await GenericPutAsync<IndividualAccessPolicyResponse>(string.Format("/api/v2/help_center/sections/{0}/access_policy.json", section.Id), body);
        }
#endif
    }
}
