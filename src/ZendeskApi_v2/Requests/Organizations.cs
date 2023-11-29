#if ASYNC
using System.Threading.Tasks;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using ZendeskApi_v2.Extensions;
using ZendeskApi_v2.Models.Organizations;
using ZendeskApi_v2.Models.Shared;

namespace ZendeskApi_v2.Requests
{
    public interface IOrganizations : ICore
    {
#if SYNC
        GroupOrganizationResponse GetOrganizations(int? perPage = null, int? page = null);

        /// <summary>
        /// Returns an array of organizations whose name starts with the value specified in the name parameter. The name must be at least 2 characters in length.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        GroupOrganizationResponse GetOrganizationsStartingWith(string name);

        GroupOrganizationResponse SearchForOrganizationsByExternalId(string externalId);
        IndividualOrganizationResponse GetOrganization(long id);
        GroupOrganizationResponse GetMultipleOrganizations(IEnumerable<long> ids);

        /// <summary>
        /// Get organizations by external ids
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        GroupOrganizationResponse GetMultipleOrganizationsByExternalIds(IEnumerable<string> externalIds);
        IndividualOrganizationResponse CreateOrganization(Organization organization);
        JobStatusResponse CreateMultipleOrganizations(IEnumerable<Organization> organizations);
        IndividualOrganizationResponse CreateOrUpdateOrganization(Organization organization);

        IndividualOrganizationResponse UpdateOrganization(Organization organization);
        JobStatusResponse UpdateMultipleOrganizations(IEnumerable<Organization> organizations);
        bool DeleteOrganization(long id);
        JobStatusResponse DeleteMultipleOrganizations(IEnumerable<long> ids);
        JobStatusResponse DeleteMultipleOrganizationsByExternalIds(IEnumerable<string> externalIds);

        GroupOrganizationMembershipResponse GetOrganizationMemberships(int? perPage = null, int? page = null);
        GroupOrganizationMembershipResponse GetOrganizationMembershipsByUserId(long userId, int? perPage = null, int? page = null);
        GroupOrganizationMembershipResponse GetOrganizationMembershipsByOrganizationId(long organizationId, int? perPage = null, int? page = null);
        IndividualOrganizationMembershipResponse GetOrganizationMembership(long id);
        IndividualOrganizationMembershipResponse GetOrganizationMembershipByIdAndUserId(long id, long userid);
        IndividualOrganizationMembershipResponse CreateOrganizationMembership(OrganizationMembership organizationMembership);
        IndividualOrganizationMembershipResponse CreateOrganizationMembership(long userId, OrganizationMembership organizationMembership);
        JobStatusResponse CreateManyOrganizationMemberships(IEnumerable<OrganizationMembership> organizationMemberships);
        bool DeleteOrganizationMembership(long id);
        bool DeleteOrganizationMembership(long id, long userId);
        JobStatusResponse DestroyManyOrganizationMemberships(IEnumerable<long> ids);
        GroupOrganizationMembershipResponse SetOrganizationMembershipAsDefault(long id, long userId);

        GroupOrganizationExportResponse GetIncrementalOrganizationExport(DateTimeOffset startTime);
        GroupOrganizationExportResponse GetIncrementalOrganizationExportNextPage(string nextPage);
#endif

#if ASYNC
        Task<GroupOrganizationResponse> GetOrganizationsAsync(int? perPage = null, int? page = null);

        /// <summary>
        /// Returns an array of organizations whose name starts with the value specified in the name parameter. The name must be at least 2 characters in length.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<GroupOrganizationResponse> GetOrganizationsStartingWithAsync(string name);

        // TODO: Rename SearchForOrganizationsByExternalIdAsync(string externalId);
        Task<GroupOrganizationResponse> SearchForOrganizationsAsync(string searchTerm);
        Task<IndividualOrganizationResponse> GetOrganizationAsync(long id);
        Task<GroupOrganizationResponse> GetMultipleOrganizationsAsync(IEnumerable<long> ids);
        /// <summary>
        /// Get organizations by external ids async
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<GroupOrganizationResponse> GetMultipleOrganizationsByExternalIdsAsync(IEnumerable<string> externalIds);
        Task<IndividualOrganizationResponse> CreateOrganizationAsync(Organization organization);
        Task<JobStatusResponse> CreateMultipleOrganizationsAsync(IEnumerable<Organization> organizations);
        Task<IndividualOrganizationResponse> CreateOrUpdateOrganizationAsync(Organization organization);
        Task<IndividualOrganizationResponse> UpdateOrganizationAsync(Organization organization);
        Task<JobStatusResponse> UpdateMultipleOrganizationsAsync(IEnumerable<Organization> organizations);
        Task<bool> DeleteOrganizationAsync(long id);
        Task<JobStatusResponse> DeleteMultipleOrganizationsAsync(IEnumerable<long> ids);
        Task<JobStatusResponse> DeleteMultipleOrganizationsByExternalIdsAsync(IEnumerable<string> externalIds);

        Task<GroupOrganizationMembershipResponse> GetOrganizationMembershipsAsync(int? perPage = null, int? page = null);
        Task<GroupOrganizationMembershipResponse> GetOrganizationMembershipsByUserIdAsync(long userId, int? perPage = null, int? page = null);
        Task<GroupOrganizationMembershipResponse> GetOrganizationMembershipsByOrganizationIdAsync(long organizationId, int? perPage = null, int? page = null);
        Task<IndividualOrganizationMembershipResponse> GetOrganizationMembershipAsync(long id);
        Task<IndividualOrganizationMembershipResponse> GetOrganizationMembershipByIdAndUserIdAsync(long id, long userid);
        Task<IndividualOrganizationMembershipResponse> CreateOrganizationMembershipAsync(OrganizationMembership organizationMembership);
        Task<IndividualOrganizationMembershipResponse> CreateOrganizationMembershipAsync(long userId, OrganizationMembership organizationMembership);
        Task<JobStatusResponse> CreateManyOrganizationMembershipsAsync(IEnumerable<OrganizationMembership> organizationMemberships);
        Task<bool> DeleteOrganizationMembershipAsync(long id);
        Task<bool> DeleteOrganizationMembershipAsync(long id, long userId);
        Task<JobStatusResponse> DestroyManyOrganizationMembershipsAsync(IEnumerable<long> ids);
        Task<GroupOrganizationMembershipResponse> SetOrganizationMembershipAsDefaultAsync(long id, long userId);

        Task<GroupOrganizationExportResponse> GetIncrementalOrganizationExportAsync(DateTimeOffset startTime);
        Task<GroupOrganizationExportResponse> GetIncrementalOrganizationExportNextPageAsync(string nextPage);
#endif
    }

    public class Organizations : Core, IOrganizations
    {
        private const string _incremental_export = "incremental/organizations.json?start_time=";

        public Organizations(string yourZendeskUrl, string user, string password, string apiToken, string p_OAuthToken, Dictionary<string,string> customHeaders)
            : base(yourZendeskUrl, user, password, apiToken, p_OAuthToken, customHeaders)
        {
        }

#if SYNC
        public GroupOrganizationResponse GetOrganizations(int? perPage = null, int? page = null)
        {
            return GenericPagedGet<GroupOrganizationResponse>("organizations.json", perPage, page);
        }

        /// <summary>
        /// Returns an array of organizations whose name starts with the value specified in the name parameter. The name must be at least 2 characters in length.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public GroupOrganizationResponse GetOrganizationsStartingWith(string name)
        {
            return GenericPost<GroupOrganizationResponse>($"organizations/autocomplete.json?name={name}");
        }

        public GroupOrganizationResponse SearchForOrganizationsByExternalId(string externalId)
        {
            return GenericGet<GroupOrganizationResponse>($"organizations/search.json?external_id={externalId}");
        }

        public IndividualOrganizationResponse GetOrganization(long id)
        {
            return GenericGet<IndividualOrganizationResponse>($"organizations/{id}.json");
        }

        public GroupOrganizationResponse GetMultipleOrganizations(IEnumerable<long> ids)
        {
            return GenericGet<GroupOrganizationResponse>($"organizations/show_many.json?ids={ids.ToCsv()}");
        }

        public GroupOrganizationResponse GetMultipleOrganizationsByExternalIds(IEnumerable<string> externalIds)
        {
            return GenericGet<GroupOrganizationResponse>($"organizations/show_many.json?external_ids={externalIds.ToCsv()}");
        }

        public IndividualOrganizationResponse CreateOrganization(Organization organization)
        {
            var body = new { organization };
            return GenericPost<IndividualOrganizationResponse>("organizations.json", body);
        }

        public JobStatusResponse CreateMultipleOrganizations(IEnumerable<Organization> organizations)
        {
            return GenericPost<JobStatusResponse>("organizations/create_many.json", new { organizations });
        }

        public IndividualOrganizationResponse CreateOrUpdateOrganization(Organization organization)
        {
            var body = new { organization };
            return GenericPost<IndividualOrganizationResponse>("organizations/create_or_update.json", body);
        }

        public IndividualOrganizationResponse UpdateOrganization(Organization organization)
        {
            var body = new { organization };
            return GenericPut<IndividualOrganizationResponse>($"organizations/{organization.Id}.json", body);
        }

        public JobStatusResponse UpdateMultipleOrganizations(IEnumerable<Organization> organizations)
        {
            return GenericPut<JobStatusResponse>($"organizations/update_many.json", new { organizations });
        }

        public bool DeleteOrganization(long id)
        {
            return GenericDelete($"organizations/{id}.json");
        }

        public JobStatusResponse DeleteMultipleOrganizations(IEnumerable<long> ids)
        {
            return GenericDelete<JobStatusResponse>($"organizations/destroy_many.json?ids={string.Join(",", ids)}");
        }

        public JobStatusResponse DeleteMultipleOrganizationsByExternalIds(IEnumerable<string> externalIds)
        {
            var encodedIds =  WebUtility.UrlEncode(string.Join(",", externalIds));
            return GenericDelete<JobStatusResponse>($"organizations/destroy_many.json?external_ids={encodedIds}");
        }

        public GroupOrganizationMembershipResponse GetOrganizationMemberships(int? perPage = null, int? page = null)
        {
            return GenericPagedGet<GroupOrganizationMembershipResponse>("organization_memberships.json", perPage, page);
        }

        public GroupOrganizationMembershipResponse GetOrganizationMembershipsByUserId(long userId, int? perPage = null, int? page = null)
        {
            return GenericPagedGet<GroupOrganizationMembershipResponse>($"users/{userId}/organization_memberships.json", perPage, page);
        }

        public GroupOrganizationMembershipResponse GetOrganizationMembershipsByOrganizationId(long organizationId, int? perPage = null, int? page = null)
        {
            return GenericPagedGet<GroupOrganizationMembershipResponse>($"organizations/{organizationId}/organization_memberships.json", perPage, page);
        }

        public IndividualOrganizationMembershipResponse GetOrganizationMembership(long id)
        {
            return GenericGet<IndividualOrganizationMembershipResponse>($"organization_memberships/{id}.json");
        }

        public IndividualOrganizationMembershipResponse GetOrganizationMembershipByIdAndUserId(long id, long userid)
        {
            return GenericGet<IndividualOrganizationMembershipResponse>($"users/{userid}/organizations/organization_memberships/{id}.json");
        }

        public IndividualOrganizationMembershipResponse CreateOrganizationMembership(OrganizationMembership organization_membership)
        {
            var body = new { organization_membership };
            return GenericPost<IndividualOrganizationMembershipResponse>("organization_memberships.json", body);
        }
        public IndividualOrganizationMembershipResponse CreateOrganizationMembership(long userId, OrganizationMembership organization_membership)
        {
            var body = new { organization_membership };
            return GenericPost<IndividualOrganizationMembershipResponse>($"users/{userId}/organization_memberships.json", body);
        }

        public JobStatusResponse CreateManyOrganizationMemberships(IEnumerable<OrganizationMembership> organization_memberships)
        {
            var body = new { organization_memberships };
            return GenericPost<JobStatusResponse>("organization_memberships/create_many.json", body);
        }

        public bool DeleteOrganizationMembership(long id)
        {
            return GenericDelete($"organization_memberships/{id}.json");
        }

        public bool DeleteOrganizationMembership(long id, long userId)
        {
            return GenericDelete($"users/{userId}/organization_memberships/{id}.json");
        }

        public JobStatusResponse DestroyManyOrganizationMemberships(IEnumerable<long> ids)
        {
            var idList = string.Join(",", ids.Select(i => i.ToString()).ToArray());
            return GenericDelete<JobStatusResponse>($"organization_memberships/destroy_many.json?ids={idList}");
        }

        public GroupOrganizationMembershipResponse SetOrganizationMembershipAsDefault(long id, long userId)
        {
            return GenericPut<GroupOrganizationMembershipResponse>($"users/{userId}/organization_memberships/{id}/make_default.json");
        }

        public GroupOrganizationExportResponse GetIncrementalOrganizationExport(DateTimeOffset startTime)
        {
            var resource = _incremental_export + startTime.UtcDateTime.GetEpoch();
            return GenericGet<GroupOrganizationExportResponse>(resource);
        }

        public GroupOrganizationExportResponse GetIncrementalOrganizationExportNextPage(string nextPage)
        {
            var resource = nextPage.Replace(ZendeskUrl, string.Empty);
            return GenericGet<GroupOrganizationExportResponse>(resource);
        }
#endif

#if ASYNC

        public async Task<GroupOrganizationResponse> GetOrganizationsAsync(int? perPage = null, int? page = null)
        {
            return await GenericPagedGetAsync<GroupOrganizationResponse>("organizations.json", perPage, page);
        }

        /// <summary>
        /// Returns an array of organizations whose name starts with the value specified in the name parameter. The name must be at least 2 characters in length.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<GroupOrganizationResponse> GetOrganizationsStartingWithAsync(string name)
        {
            return await GenericPostAsync<GroupOrganizationResponse>($"organizations/autocomplete.json?name={name}");
        }

        public async Task<GroupOrganizationResponse> SearchForOrganizationsAsync(string searchTerm)
        {
            return await GenericGetAsync<GroupOrganizationResponse>($"organizations/search.json?external_id={searchTerm}");
        }

        public async Task<IndividualOrganizationResponse> GetOrganizationAsync(long id)
        {
            return await GenericGetAsync<IndividualOrganizationResponse>($"organizations/{id}.json");
        }

        public async Task<JobStatusResponse> CreateMultipleOrganizationsAsync(IEnumerable<Organization> organizations)
        {
            return await GenericPostAsync<JobStatusResponse>("organizations/create_many.json", new { organizations });
        }

        public async Task<GroupOrganizationResponse> GetMultipleOrganizationsAsync(IEnumerable<long> ids)
        {
            return await GenericGetAsync<GroupOrganizationResponse>($"organizations/show_many.json?ids={ids.ToCsv()}");
        }

        public async Task<GroupOrganizationResponse> GetMultipleOrganizationsByExternalIdsAsync(IEnumerable<string> externalIds)
        {
            return await GenericGetAsync<GroupOrganizationResponse>($"organizations/show_many.json?external_ids={externalIds.ToCsv()}");
        }

        public async Task<IndividualOrganizationResponse> CreateOrganizationAsync(Organization organization)
        {
            return await GenericPostAsync<IndividualOrganizationResponse>("organizations.json", new { organization });
        }

        public async Task<IndividualOrganizationResponse> CreateOrUpdateOrganizationAsync(Organization organization)
        {
            return await GenericPostAsync<IndividualOrganizationResponse>("organizations/create_or_update.json", new { organization });
        }

        public async Task<IndividualOrganizationResponse> UpdateOrganizationAsync(Organization organization)
        {
            return await GenericPutAsync<IndividualOrganizationResponse>($"organizations/{organization.Id}.json", new { organization });
        }

        public async Task<JobStatusResponse> UpdateMultipleOrganizationsAsync(IEnumerable<Organization> organizations)
        {
            return await GenericPutAsync<JobStatusResponse>($"organizations/update_many.json", new { organizations });
        }

        public async Task<bool> DeleteOrganizationAsync(long id)
        {
            return await GenericDeleteAsync($"organizations/{id}.json");
        }

        public async Task<JobStatusResponse> DeleteMultipleOrganizationsAsync(IEnumerable<long> ids)
        {
            return await GenericDeleteAsync<JobStatusResponse>($"organizations/destroy_many.json?ids={string.Join(",", ids)}");
        }

        public async Task<JobStatusResponse> DeleteMultipleOrganizationsByExternalIdsAsync(IEnumerable<string> externalIds)
        {
            var encodedIds =  WebUtility.UrlEncode(string.Join(",", externalIds));
            return await GenericDeleteAsync<JobStatusResponse>($"organizations/destroy_many.json?external_ids={encodedIds}");
        }

        public async Task<GroupOrganizationMembershipResponse> GetOrganizationMembershipsAsync(int? perPage = null, int? page = null)
        {
            return await GenericPagedGetAsync<GroupOrganizationMembershipResponse>("organization_memberships.json", perPage, page);
        }

        public async Task<GroupOrganizationMembershipResponse> GetOrganizationMembershipsByUserIdAsync(long userId, int? perPage = null, int? page = null)
        {
            return await GenericPagedGetAsync<GroupOrganizationMembershipResponse>($"users/{userId}/organization_memberships.json", perPage, page);
        }

        public async Task<GroupOrganizationMembershipResponse> GetOrganizationMembershipsByOrganizationIdAsync(long organizationId, int? perPage = null, int? page = null)
        {
            return await GenericPagedGetAsync<GroupOrganizationMembershipResponse>($"organizations/{organizationId}/organization_memberships.json", perPage, page);
        }

        public async Task<IndividualOrganizationMembershipResponse> GetOrganizationMembershipAsync(long id)
        {
            return await GenericGetAsync<IndividualOrganizationMembershipResponse>($"organization_memberships/{id}.json");
        }

        public async Task<IndividualOrganizationMembershipResponse> GetOrganizationMembershipByIdAndUserIdAsync(long id, long userid)
        {
            return await GenericGetAsync<IndividualOrganizationMembershipResponse>($"users/{userid}/organizations/organization_memberships/{id}.json");
        }

        public async Task<IndividualOrganizationMembershipResponse> CreateOrganizationMembershipAsync(OrganizationMembership organization_membership)
        {
            return await GenericPostAsync<IndividualOrganizationMembershipResponse>("organization_memberships.json", new { organization_membership });
        }

        public async Task<IndividualOrganizationMembershipResponse> CreateOrganizationMembershipAsync(long userId, OrganizationMembership organization_membership)
        {
            var body = new { organization_membership };
            return await GenericPostAsync<IndividualOrganizationMembershipResponse>($"users/{userId}/organization_memberships.json", body);
        }

        public async Task<JobStatusResponse> CreateManyOrganizationMembershipsAsync(IEnumerable<OrganizationMembership> organization_memberships)
        {
            var body = new { organization_memberships };
            return await GenericPostAsync<JobStatusResponse>("organization_memberships/create_many.json", body);
        }

        public async Task<bool> DeleteOrganizationMembershipAsync(long id)
        {
            return await GenericDeleteAsync($"organization_memberships/{id}.json");
        }

        public async Task<bool> DeleteOrganizationMembershipAsync(long id, long userId)
        {
            return await GenericDeleteAsync($"users/{userId}/organization_memberships/{id}.json");
        }

        public async Task<JobStatusResponse> DestroyManyOrganizationMembershipsAsync(IEnumerable<long> ids)
        {
            var idList = string.Join(",", ids.Select(i => i.ToString()).ToArray());
            return await GenericDeleteAsync<JobStatusResponse>($"organization_memberships/destroy_many.json?ids={idList}");
        }

        public async Task<GroupOrganizationMembershipResponse> SetOrganizationMembershipAsDefaultAsync(long id, long userId)
        {
            return await GenericPutAsync<GroupOrganizationMembershipResponse>($"users/{userId}/organization_memberships/{id}/make_default.json");
        }

        public async Task<GroupOrganizationExportResponse> GetIncrementalOrganizationExportAsync(DateTimeOffset startTime)
        {
            var resource = _incremental_export + startTime.UtcDateTime.GetEpoch();
            return await GenericGetAsync<GroupOrganizationExportResponse>(resource);
        }

        public async Task<GroupOrganizationExportResponse> GetIncrementalOrganizationExportNextPageAsync(string nextPage)
        {
            var resource = nextPage.Replace(ZendeskUrl, string.Empty);
            return await GenericGetAsync<GroupOrganizationExportResponse>(resource);
        }
#endif
    }
}
