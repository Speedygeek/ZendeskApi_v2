#if ASYNC
using System.Threading.Tasks;
#endif
using ZendeskApi_v2.Models.Organizations;

namespace ZendeskApi_v2.Requests
{
    public class Organizations : Core
    {
        public Organizations(string yourZendeskUrl, string user, string password)
            : base(yourZendeskUrl, user, password)
        {
        }

#if SYNC
        public GroupOrganizationResponse GetOrganizations()
        {
            return GenericGet<GroupOrganizationResponse>("organizations.json");
        }

        /// <summary>
        /// Returns an array of organizations whose name starts with the value specified in the name parameter. The name must be at least 2 characters in length.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public GroupOrganizationResponse GetOrganizationsStartingWith(string name)
        {
            return GenericPost<GroupOrganizationResponse>(string.Format("organizations/autocomplete.json?name={0}", name));
        }

        public GroupOrganizationResponse SearchForOrganizations(string searchTerm)
        {
            return GenericGet<GroupOrganizationResponse>(string.Format("organizations/search.json?external_id={0}", searchTerm));
        }

        public IndividualOrganizationResponse GetOrganization(long id)
        {
            return GenericGet<IndividualOrganizationResponse>(string.Format("organizations/{0}.json", id));
        }

        public IndividualOrganizationResponse CreateOrganization(Organization organization)
        {
            var body = new {organization};
            return GenericPost<IndividualOrganizationResponse>("organizations.json", body);
        }

        public IndividualOrganizationResponse UpdateOrganization(Organization organization)
        {
            var body = new { organization };
            return GenericPut<IndividualOrganizationResponse>(string.Format("organizations/{0}.json", organization.Id), body);
        }

        public bool DeleteOrganization(long id)
        {            
            return GenericDelete(string.Format("organizations/{0}.json", id));
        }

#endif

#if ASYNC

        public async Task<GroupOrganizationResponse> GetOrganizationsAsync()
        {
            return await GenericGetAsync<GroupOrganizationResponse>("organizations.json");
        }

        /// <summary>
        /// Returns an array of organizations whose name starts with the value specified in the name parameter. The name must be at least 2 characters in length.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<GroupOrganizationResponse> GetOrganizationsStartingWithAsync(string name)
        {
            return await GenericPostAsync<GroupOrganizationResponse>(string.Format("organizations/autocomplete.json?name={0}", name));
        }

        public async Task<GroupOrganizationResponse> SearchForOrganizationsAsync(string searchTerm)
        {
            return await GenericPostAsync<GroupOrganizationResponse>(string.Format("organizations/autocomplete.json?external_id={0}", searchTerm));
        }

        public async Task<IndividualOrganizationResponse> GetOrganizationAsync(long id)
        {
            return await GenericGetAsync<IndividualOrganizationResponse>(string.Format("organizations/{0}.json", id));
        }

        public async Task<IndividualOrganizationResponse> CreateOrganizationAsync(Organization organization)
        {
            var body = new {organization};
            return await GenericPostAsync<IndividualOrganizationResponse>("organizations.json", body);
        }

        public async Task<IndividualOrganizationResponse> UpdateOrganizationAsync(Organization organization)
        {
            var body = new { organization };
            return await GenericPutAsync<IndividualOrganizationResponse>(string.Format("organizations/{0}.json", organization.Id), body);
        }

        public async Task<bool> DeleteOrganizationAsync(long id)
        {            
            return await GenericDeleteAsync(string.Format("organizations/{0}.json", id));
        }
#endif
    }
}