using System;
using System.Collections.Generic;
using System.Linq;
using ZendeskApi_v2.Extensions;

#if ASYNC

using System.Threading.Tasks;

#endif

using ZendeskApi_v2.Models.Shared;
using ZendeskApi_v2.Models.Users;
using User = ZendeskApi_v2.Models.Users.User;
using System.Text.RegularExpressions;

namespace ZendeskApi_v2.Requests
{
    [Flags]
    public enum UserSideLoadOptions
    {
        None = 0,
        Organizations = 1,
        Abilities = 2,
        Roles = 4,
        Identities = 8,
        Groups = 16
    }

    public interface IUsers : ICore
    {
#if SYNC

        IndividualUserResponse GetCurrentUser();

        GroupUserResponse GetAllUsers(int? perPage = null, int? page = null, UserSideLoadOptions sideLoadOptions = UserSideLoadOptions.None);

        GroupUserResponse GetAllAgents(int? perPage = null, int? page = null, UserSideLoadOptions sideLoadOptions = UserSideLoadOptions.None);

        GroupUserResponse GetAllAdmins(int? perPage = null, int? page = null, UserSideLoadOptions sideLoadOptions = UserSideLoadOptions.None);

        GroupUserResponse GetAllEndUsers(int? perPage = null, int? page = null, UserSideLoadOptions sideLoadOptions = UserSideLoadOptions.None);

        GroupUserResponse GetAllUsersInRoles(int? perPage = null, int? page = null, UserSideLoadOptions sideLoadOptions = UserSideLoadOptions.None, bool agents = false, bool endUsers = false, bool admins = false);

        GroupUserResponse GetAllUsersInEnterpriseRole(long enterpriseRoleId, int? perPage = null, int? page = null, UserSideLoadOptions sideLoadOptions = UserSideLoadOptions.None);

        IndividualUserResponse GetUser(long id);

        IndividualUserRelatedInformationResponse GetUserRelatedInformation(long id);

        IndividualUserResponse MergeUser(long fromId, long intoId);

        GroupUserResponse GetMultipleUsers(IEnumerable<long> ids, UserSideLoadOptions sideLoadOptions = UserSideLoadOptions.None);

        GroupUserResponse SearchByEmail(string email);

        GroupUserResponse SearchByPhone(string phone);

        GroupUserResponse SearchByCustomUserField(string fieldKey, string fieldValue);

        GroupUserResponse SearchByExternalId(string externalId);

        GroupUserResponse GetUsersInGroup(long id);

        GroupUserResponse GetUsersInOrganization(long id, int? perPage = null, int? page = null, UserSideLoadOptions sideLoadOptions = UserSideLoadOptions.None);

        IndividualUserResponse CreateUser(User user);

        JobStatusResponse BulkCreateUsers(IEnumerable<User> users);

        IndividualUserResponse SuspendUser(long id);

        IndividualUserResponse UpdateUser(User user);

        bool DeleteUser(long id);

        JobStatusResponse BulkDeleteUsers(IEnumerable<User> users);

        bool SetUsersPassword(long userId, string newPassword);

        bool ChangeUsersPassword(long userId, string oldPassword, string newPassword);

        GroupUserIdentityResponse GetUserIdentities(long userId);

        IndividualUserIdentityResponse GetSpecificUserIdentity(long userId, long identityId);

        IndividualUserIdentityResponse AddUserIdentity(long userId, UserIdentity identity);

        IndividualUserIdentityResponse UpdateUserIdentity(long userId, UserIdentity identity);

        IndividualUserIdentityResponse SetUserIdentityAsVerified(long userId, long identityId);

        GroupUserIdentityResponse SetUserIdentityAsPrimary(long userId, long identityId);

        /// <summary>
        /// This sends a verification email to the user, asking him to click a link in order to verify ownership of the email address
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="identityId"></param>
        /// <returns></returns>
        IndividualUserIdentityResponse SendUserVerificationRequest(long userId, long identityId);

        bool DeleteUserIdentity(long userId, long identityId);

        IndividualUserResponse SetUserPhoto(long userId, ZenFile photo);

        GroupUserExportResponse GetIncrementalUserExport(DateTimeOffset startTime, UserSideLoadOptions sideLoadOptions = UserSideLoadOptions.None);

        GroupUserExportResponse GetIncrementalUserExportNextPage(string nextPage);
#endif

#if ASYNC

        Task<IndividualUserResponse> GetCurrentUserAsync();

        Task<GroupUserResponse> GetAllUsersAsync(int? perPage = null, int? page = null, UserSideLoadOptions sideLoadOptions = UserSideLoadOptions.None);

        Task<GroupUserResponse> GetAllAgentsAsync(int? perPage = null, int? page = null, UserSideLoadOptions sideLoadOptions = UserSideLoadOptions.None);

        Task<GroupUserResponse> GetAllAdminsAsync(int? perPage = null, int? page = null, UserSideLoadOptions sideLoadOptions = UserSideLoadOptions.None);

        Task<GroupUserResponse> GetAllEndUsersAsync(int? perPage = null, int? page = null, UserSideLoadOptions sideLoadOptions = UserSideLoadOptions.None);

        Task<GroupUserResponse> GetAllUsersInRolesAsync(int? perPage = null, int? page = null, UserSideLoadOptions sideLoadOptions = UserSideLoadOptions.None, bool agents = false, bool endUsers = false, bool admins = false);

        Task<GroupUserResponse> GetAllUsersInEnterpriseRoleAsync(long enterpriseRoleId, int? perPage = null, int? page = null, UserSideLoadOptions sideLoadOptions = UserSideLoadOptions.None);

        Task<IndividualUserResponse> GetUserAsync(long id);

        Task<IndividualUserRelatedInformationResponse> GetUserRelatedInformationAsync(long id);

        Task<IndividualUserResponse> MergeUserAsync(long fromId, long intoId);

        Task<GroupUserResponse> GetMultipleUsersAsync(IEnumerable<long> ids, UserSideLoadOptions sideLoadOptions = UserSideLoadOptions.None);

        Task<GroupUserResponse> SearchByEmailAsync(string email);

        Task<GroupUserResponse> SearchByPhoneAsync(string phone);

        Task<GroupUserResponse> SearchByCustomUserFieldAsync(string fieldKey, string fieldValue);

        Task<GroupUserResponse> SearchByExternalIdAsync(string externalId);

        Task<GroupUserResponse> GetUsersInGroupAsync(long id);

        Task<GroupUserResponse> GetUsersInOrganizationAsync(long id, int? perPage = null, int? page = null, UserSideLoadOptions sideLoadOptions = UserSideLoadOptions.None);

        Task<IndividualUserResponse> CreateUserAsync(User user);

        Task<JobStatusResponse> BulkCreateUsersAsync(IEnumerable<User> users);

        Task<IndividualUserResponse> SuspendUserAsync(long id);

        Task<IndividualUserResponse> UpdateUserAsync(User user);

        Task<bool> DeleteUserAsync(long id);

        Task<JobStatusResponse> BulkDeleteUsersAsync(IEnumerable<User> users);

        Task<bool> SetUsersPasswordAsync(long userId, string newPassword);

        Task<bool> ChangeUsersPasswordAsync(long userId, string oldPassword, string newPassword);

        Task<GroupUserIdentityResponse> GetUserIdentitiesAsync(long userId);

        Task<IndividualUserIdentityResponse> GetSpecificUserIdentityAsync(long userId, long identityId);

        Task<IndividualUserIdentityResponse> AddUserIdentityAsync(long userId, UserIdentity identity);

        Task<IndividualUserIdentityResponse> UpdateUserIdentityAsync(long userId, UserIdentity identity);

        Task<IndividualUserIdentityResponse> SetUserIdentityAsVerifiedAsync(long userId, long identityId);

        Task<GroupUserIdentityResponse> SetUserIdentityAsPrimaryAsync(long userId, long identityId);

        /// <summary>
        /// This sends a verification email to the user, asking him to click a link in order to verify ownership of the email address
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="identityId"></param>
        /// <returns></returns>
        Task<IndividualUserIdentityResponse> SendUserVerificationRequestAsync(long userId, long identityId);

        Task<bool> DeleteUserIdentityAsync(long userId, long identityId);

        Task<IndividualUserResponse> SetUserPhotoAsync(long userId, ZenFile photo);

        Task<GroupUserExportResponse> GetIncrementalUserExportAsync(DateTimeOffset startTime, UserSideLoadOptions sideLoadOptions = UserSideLoadOptions.None);

        Task<GroupUserExportResponse> GetIncrementalUserExportNextPageAsync(string nextPage);

#endif
    }

    public class Users : Core, IUsers
    {
        private const string _incremental_export = "incremental/users.json?start_time=";

        public Users(string yourZendeskUrl, string user, string password, string apiToken, string p_OAuthToken)
            : base(yourZendeskUrl, user, password, apiToken, p_OAuthToken)
        {
        }

#if SYNC

        public IndividualUserResponse GetCurrentUser()
        {
            return GenericGet<IndividualUserResponse>("users/me.json");
        }

        public GroupUserResponse GetAllUsers(int? perPage = null, int? page = null, UserSideLoadOptions sideLoadOptions = UserSideLoadOptions.None)
        {
            var resource = GetResourceStringWithSideLoadOptionsParam("users.json", sideLoadOptions);

            return GenericPagedGet<GroupUserResponse>(resource, perPage, page);
        }

        public GroupUserResponse GetAllAgents(int? perPage = null, int? page = null, UserSideLoadOptions sideLoadOptions = UserSideLoadOptions.None)
        {
            var resource = GetResourceStringWithSideLoadOptionsParam("users.json?role=agent", sideLoadOptions);

            return GenericPagedGet<GroupUserResponse>(resource, perPage, page);
        }

        public GroupUserResponse GetAllAdmins(int? perPage = null, int? page = null, UserSideLoadOptions sideLoadOptions = UserSideLoadOptions.None)
        {
            var resource = GetResourceStringWithSideLoadOptionsParam("users.json?role=admin", sideLoadOptions);

            return GenericPagedGet<GroupUserResponse>(resource, perPage, page);
        }

        public GroupUserResponse GetAllEndUsers(int? perPage = null, int? page = null, UserSideLoadOptions sideLoadOptions = UserSideLoadOptions.None)
        {
            var resource = GetResourceStringWithSideLoadOptionsParam("users.json?role=end-user", sideLoadOptions);

            return GenericPagedGet<GroupUserResponse>(resource, perPage, page);
        }

        public GroupUserResponse GetAllUsersInRoles(int? perPage = null, int? page = null, UserSideLoadOptions sideLoadOptions = UserSideLoadOptions.None, bool agents = false, bool endUsers = false, bool admins = false)
        {
            var resourceString = "users.json?";

            if (agents)
            {
                resourceString += "role[]=agent&";
            }

            if (endUsers)
            {
                resourceString += "role[]=end-user&";
            }

            if (admins)
            {
                resourceString += "role[]=admin&";
            }

            resourceString = resourceString.TrimEnd('&');

            var resource = GetResourceStringWithSideLoadOptionsParam(resourceString, sideLoadOptions);

            return GenericPagedGet<GroupUserResponse>(resource, perPage, page);
        }

        public GroupUserResponse GetAllUsersInEnterpriseRole(long enterpriseRoleId, int? perPage = null, int? page = null, UserSideLoadOptions sideLoadOptions = UserSideLoadOptions.None)
        {
            var resource = GetResourceStringWithSideLoadOptionsParam($"users.json?permission_set={enterpriseRoleId}", sideLoadOptions);

            return GenericPagedGet<GroupUserResponse>(resource, perPage, page);
        }

        public IndividualUserResponse GetUser(long id)
        {
            return GenericGet<IndividualUserResponse>($"users/{id}.json");
        }

        public IndividualUserRelatedInformationResponse GetUserRelatedInformation(long id)
        {
            return GenericGet<IndividualUserRelatedInformationResponse>($"users/{id}/related.json");
        }

        /// <summary>
        /// The user whose id is provided in the URL will be merged into the existing user provided in the params. Any two arbitrary users can be merged.
        /// </summary>
        /// <param name="fromId"></param>
        /// <param name="intoId"></param>
        /// <returns></returns>
        public IndividualUserResponse MergeUser(long fromId, long intoId)
        {
            var body = new { user = new { id = intoId } };
            return GenericPut<IndividualUserResponse>($"users/{fromId}/merge.json", body);
        }

        public GroupUserResponse GetMultipleUsers(IEnumerable<long> ids, UserSideLoadOptions sideLoadOptions = UserSideLoadOptions.None)
        {
            var resource = GetResourceStringWithSideLoadOptionsParam($"users/show_many.json?ids={ids.ToCsv()}", sideLoadOptions);

            return GenericGet<GroupUserResponse>(resource);
        }

        public GroupUserResponse SearchByEmail(string email)
        {
            return GenericGet<GroupUserResponse>($"users/search.json?query={email}");
        }

        public GroupUserResponse SearchByPhone(string phone)
        {
            return GenericGet<GroupUserResponse>($"users/search.json?query=role:end-user phone:*{Regex.Replace(phone, @"\D", "")}");
        }

        public GroupUserResponse SearchByCustomUserField(string fieldKey, string fieldValue)
        {
            return GenericGet<GroupUserResponse>($"users/search.json?query={fieldKey}:{fieldValue}");
        }

        public GroupUserResponse SearchByExternalId(string externalId)
        {
            return GenericGet<GroupUserResponse>($"users/search.json?external_id={externalId}");
        }

        public GroupUserResponse GetUsersInGroup(long id)
        {
            return GenericGet<GroupUserResponse>($"groups/{id}/users.json");
        }

        public GroupUserResponse GetUsersInOrganization(long id, int? perPage = null, int? page = null, UserSideLoadOptions sideLoadOptions = UserSideLoadOptions.None)
        {
            var resource = GetResourceStringWithSideLoadOptionsParam($"organizations/{id}/users.json", sideLoadOptions);
            return GenericPagedGet<GroupUserResponse>(resource, perPage, page);
        }

        public IndividualUserResponse CreateUser(User user)
        {
            var body = new { user };
            return GenericPost<IndividualUserResponse>("users.json", body);
        }

        public JobStatusResponse BulkCreateUsers(IEnumerable<User> users)
        {
            var body = new { users };
            return GenericPost<JobStatusResponse>("users/create_many.json", body);
        }

        public IndividualUserResponse SuspendUser(long id)
        {
            var body = new { user = new { suspended = true } };
            return GenericPut<IndividualUserResponse>($"users/{id}.json", body);
        }

        public IndividualUserResponse UpdateUser(User user)
        {
            var body = new { user };
            return GenericPut<IndividualUserResponse>($"users/{user.Id}.json", body);
        }

        public bool DeleteUser(long id)
        {
            return GenericDelete($"users/{id}.json");
        }

        public JobStatusResponse BulkDeleteUsers(IEnumerable<User> users)
        {
            var userIdsCommaSeperatedList = CreateCommaSeperatedUserIdString(users);

            return GenericDelete<JobStatusResponse>($"users/destroy_many.json?ids={userIdsCommaSeperatedList}");
        }

        public bool SetUsersPassword(long userId, string newPassword)
        {
            var body = new { password = newPassword };
            return GenericBoolPost($"users/{userId}/password.json", body);
        }

        public bool ChangeUsersPassword(long userId, string oldPassword, string newPassword)
        {
            var body = new
            {
                previous_password = oldPassword,
                password = newPassword
            };
            return GenericBoolPost($"users/{userId}/password.json", body);
        }

        public GroupUserIdentityResponse GetUserIdentities(long userId)
        {
            return GenericGet<GroupUserIdentityResponse>($"users/{userId}/identities.json");
        }

        public IndividualUserIdentityResponse GetSpecificUserIdentity(long userId, long identityId)
        {
            return GenericGet<IndividualUserIdentityResponse>($"users/{userId}/identities/{identityId}.json");
        }

        public IndividualUserIdentityResponse AddUserIdentity(long userId, UserIdentity identity)
        {
            var body = new { identity };
            return GenericPost<IndividualUserIdentityResponse>($"users/{userId}/identities.json", body);
        }

        public IndividualUserIdentityResponse UpdateUserIdentity(long userId, UserIdentity identity)
        {
            var body = new { identity };
            return GenericPut<IndividualUserIdentityResponse>($"users/{userId}/identities/{identity.Id}.json", body);
        }

        public IndividualUserIdentityResponse SetUserIdentityAsVerified(long userId, long identityId)
        {
            return GenericPut<IndividualUserIdentityResponse>($"users/{userId}/identities/{identityId}/verify.json");
        }

        public GroupUserIdentityResponse SetUserIdentityAsPrimary(long userId, long identityId)
        {
            return GenericPut<GroupUserIdentityResponse>($"users/{userId}/identities/{identityId}/make_primary.json");
        }

        /// <summary>
        /// This sends a verification email to the user, asking him to click a link in order to verify ownership of the email address
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="identityId"></param>
        /// <returns></returns>
        public IndividualUserIdentityResponse SendUserVerificationRequest(long userId, long identityId)
        {
            return GenericPut<IndividualUserIdentityResponse>($"users/{userId}/identities/{identityId}/request_verification.json");
        }

        public bool DeleteUserIdentity(long userId, long identityId)
        {
            return GenericDelete($"users/{userId}/identities/{identityId}.json");
        }

        public IndividualUserResponse SetUserPhoto(long userId, ZenFile photo)
        {
            return GenericPut<IndividualUserResponse>($"users/{userId}.json", null, new Dictionary<string, object> { { "user[photo][uploaded_data]", photo } });
        }

        public GroupUserExportResponse GetIncrementalUserExport(DateTimeOffset startTime, UserSideLoadOptions sideLoadOptions = UserSideLoadOptions.None)
        {
            var resource =
                GetResourceStringWithSideLoadOptionsParam(
                    _incremental_export + startTime.UtcDateTime.GetEpoch(),
                    sideLoadOptions);

            return GenericGet<GroupUserExportResponse>(resource);
        }

        public GroupUserExportResponse GetIncrementalUserExportNextPage(string nextPage)
        {
            var resource = nextPage.Replace(ZendeskUrl, string.Empty);

            return GenericGet<GroupUserExportResponse>(resource);
        }
#endif

#if ASYNC

        public async Task<IndividualUserResponse> GetCurrentUserAsync()
        {
            return await GenericGetAsync<IndividualUserResponse>("users/me.json");
        }

        public async Task<GroupUserResponse> GetAllUsersAsync(int? perPage = null, int? page = null, UserSideLoadOptions sideLoadOptions = UserSideLoadOptions.None)
        {
            return await GenericPagedGetAsync<GroupUserResponse>("users.json", perPage, page);
        }

        public async Task<GroupUserResponse> GetAllAgentsAsync(int? perPage = null, int? page = null, UserSideLoadOptions sideLoadOptions = UserSideLoadOptions.None)
        {
            return await GenericPagedGetAsync<GroupUserResponse>("users.json?role=agent", perPage, page);
        }

        public async Task<GroupUserResponse> GetAllAdminsAsync(int? perPage = null, int? page = null, UserSideLoadOptions sideLoadOptions = UserSideLoadOptions.None)
        {
            return await GenericPagedGetAsync<GroupUserResponse>("users.json?role=admin", perPage, page);
        }

        public async Task<GroupUserResponse> GetAllEndUsersAsync(int? perPage = null, int? page = null, UserSideLoadOptions sideLoadOptions = UserSideLoadOptions.None)
        {
            return await GenericPagedGetAsync<GroupUserResponse>("users.json?role=end-user", perPage, page);
        }

        public async Task<GroupUserResponse> GetAllUsersInRolesAsync(int? perPage = null, int? page = null, UserSideLoadOptions sideLoadOptions = UserSideLoadOptions.None, bool agents = false, bool endUsers = false, bool admins = false)
        {
            var resourceString = "users.json?";

            if (agents)
            {
                resourceString += "role[]=agent&";
            }

            if (endUsers)
            {
                resourceString += "role[]=end-user&";
            }

            if (admins)
            {
                resourceString += "role[]=admin&";
            }

            resourceString = resourceString.TrimEnd('&');

            return await GenericPagedGetAsync<GroupUserResponse>(resourceString, perPage, page);
        }

        public async Task<GroupUserResponse> GetAllUsersInEnterpriseRoleAsync(long enterpriseRoleId, int? perPage = null, int? page = null, UserSideLoadOptions sideLoadOptions = UserSideLoadOptions.None)
        {
            return await GenericPagedGetAsync<GroupUserResponse>($"users.json?permission_set={enterpriseRoleId}", perPage, page);
        }

        public async Task<IndividualUserResponse> GetUserAsync(long id)
        {
            return await GenericGetAsync<IndividualUserResponse>($"users/{id}.json");
        }

        public async Task<IndividualUserRelatedInformationResponse> GetUserRelatedInformationAsync(long id)
        {
            return await GenericGetAsync<IndividualUserRelatedInformationResponse>($"users/{id}/related.json");
        }

        /// <summary>
        /// The user whose id is provided in the URL will be merged into the existing user provided in the params. Any two arbitrary users can be merged.
        /// </summary>
        /// <param name="fromId"></param>
        /// <param name="intoId"></param>
        /// <returns></returns>
        public async Task<IndividualUserResponse> MergeUserAsync(long fromId, long intoId)
        {
            var body = new { user = new { id = intoId } };
            return await GenericPutAsync<IndividualUserResponse>($"users/{fromId}/merge.json", body);
        }

        public async Task<GroupUserResponse> GetMultipleUsersAsync(IEnumerable<long> ids, UserSideLoadOptions sideLoadOptions = UserSideLoadOptions.None)
        {
            return await GenericGetAsync<GroupUserResponse>($"users/show_many.json?ids={ids.ToCsv()}");
        }

        public async Task<GroupUserResponse> SearchByEmailAsync(string email)
        {
            return await GenericGetAsync<GroupUserResponse>($"users/search.json?query={email}");
        }

        public async Task<GroupUserResponse> SearchByPhoneAsync(string phone)
        {
            return await GenericGetAsync<GroupUserResponse>($"users/search.json?query=role:end-user phone:*{Regex.Replace(phone, @"\D", "")}");
        }

        public async Task<GroupUserResponse> SearchByCustomUserFieldAsync(string fieldKey, string fieldValue)
        {
            return await GenericGetAsync<GroupUserResponse>($"users/search.json?query={fieldKey}:{fieldValue}");
        }

        public async Task<GroupUserResponse> SearchByExternalIdAsync(string externalId)
        {
            return await GenericGetAsync<GroupUserResponse>($"users/search.json?external_id={externalId}");
        }

        public async Task<GroupUserResponse> GetUsersInGroupAsync(long id)
        {
            return await GenericGetAsync<GroupUserResponse>($"groups/{id}/users.json");
        }

        public async Task<GroupUserResponse> GetUsersInOrganizationAsync(long id, int? perPage = null, int? page = null, UserSideLoadOptions sideLoadOptions = UserSideLoadOptions.None)
        {
            var resource = GetResourceStringWithSideLoadOptionsParam($"organizations/{id}/users.json", sideLoadOptions);
            return await GenericPagedGetAsync<GroupUserResponse>(resource, perPage, page);
        }

        public async Task<IndividualUserResponse> CreateUserAsync(User user)
        {
            var body = new { user };
            return await GenericPostAsync<IndividualUserResponse>("users.json", body);
        }

        public async Task<JobStatusResponse> BulkCreateUsersAsync(IEnumerable<User> users)
        {
            var body = new { users };
            return await GenericPostAsync<JobStatusResponse>("users/create_many.json", body);
        }

        public async Task<IndividualUserResponse> SuspendUserAsync(long id)
        {
            var body = new { user = new { suspended = true } };
            return await GenericPutAsync<IndividualUserResponse>($"users/{id}.json", body);
        }

        public async Task<IndividualUserResponse> UpdateUserAsync(User user)
        {
            var body = new { user };
            return await GenericPutAsync<IndividualUserResponse>($"users/{user.Id}.json", body);
        }

        public async Task<bool> DeleteUserAsync(long id)
        {
            return await GenericDeleteAsync($"users/{id}.json");
        }

        public Task<JobStatusResponse> BulkDeleteUsersAsync(IEnumerable<User> users)
        {
            var userIdsCommaSeperatedList = CreateCommaSeperatedUserIdString(users);

            return GenericDeleteAsync<JobStatusResponse>($"users/destroy_many.json?ids={userIdsCommaSeperatedList}");
        }

        public async Task<bool> SetUsersPasswordAsync(long userId, string newPassword)
        {
            var body = new { password = newPassword };
            return await GenericBoolPostAsync($"users/{userId}/password.json", body);
        }

        public async Task<bool> ChangeUsersPasswordAsync(long userId, string oldPassword, string newPassword)
        {
            var body = new
            {
                previous_password = oldPassword,
                password = newPassword
            };
            return await GenericBoolPostAsync($"users/{userId}/password.json", body);
        }

        public async Task<GroupUserIdentityResponse> GetUserIdentitiesAsync(long userId)
        {
            return await GenericGetAsync<GroupUserIdentityResponse>($"users/{userId}/identities.json");
        }

        public async Task<IndividualUserIdentityResponse> GetSpecificUserIdentityAsync(long userId, long identityId)
        {
            return await GenericGetAsync<IndividualUserIdentityResponse>($"users/{userId}/identities/{identityId}.json");
        }

        public async Task<IndividualUserIdentityResponse> AddUserIdentityAsync(long userId, UserIdentity identity)
        {
            var body = new { identity };
            return await GenericPostAsync<IndividualUserIdentityResponse>($"users/{userId}/identities.json", body);
        }

        public async Task<IndividualUserIdentityResponse> UpdateUserIdentityAsync(long userId, UserIdentity identity)
        {
            var body = new { identity };
            return await GenericPutAsync<IndividualUserIdentityResponse>($"users/{userId}/identities/{identity.Id}.json", body);
        }

        public async Task<IndividualUserIdentityResponse> SetUserIdentityAsVerifiedAsync(long userId, long identityId)
        {
            return await GenericPutAsync<IndividualUserIdentityResponse>($"users/{userId}/identities/{identityId}/verify.json");
        }

        public async Task<GroupUserIdentityResponse> SetUserIdentityAsPrimaryAsync(long userId, long identityId)
        {
            return await GenericPutAsync<GroupUserIdentityResponse>($"users/{userId}/identities/{identityId}/make_primary.json");
        }

        /// <summary>
        /// This sends a verification email to the user, asking him to click a link in order to verify ownership of the email address
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="identityId"></param>
        /// <returns></returns>
        public async Task<IndividualUserIdentityResponse> SendUserVerificationRequestAsync(long userId, long identityId)
        {
            return await GenericPutAsync<IndividualUserIdentityResponse>($"users/{userId}/identities/{identityId}/request_verification.json");
        }

        public async Task<bool> DeleteUserIdentityAsync(long userId, long identityId)
        {
            return await GenericDeleteAsync($"users/{userId}/identities/{identityId}.json");
        }

        public Task<IndividualUserResponse> SetUserPhotoAsync(long userId, ZenFile photo)
        {
            return GenericPutAsync<IndividualUserResponse>($"users/{userId}.json", null, new Dictionary<string, object> { { "user[photo][uploaded_data]", photo } });
        }

        public async Task<GroupUserExportResponse> GetIncrementalUserExportAsync(DateTimeOffset startTime, UserSideLoadOptions sideLoadOptions = UserSideLoadOptions.None)
        {
            var resource =
                GetResourceStringWithSideLoadOptionsParam(
                    _incremental_export + startTime.UtcDateTime.GetEpoch(),
                    sideLoadOptions);

            return await GenericGetAsync<GroupUserExportResponse>(resource);
        }

        public async Task<GroupUserExportResponse> GetIncrementalUserExportNextPageAsync(string nextPage)
        {
            var resource = nextPage.Replace(ZendeskUrl, string.Empty);

            return await GenericGetAsync<GroupUserExportResponse>(resource);
        }
#endif

        private string GetResourceStringWithSideLoadOptionsParam(string resource, UserSideLoadOptions sideLoadOptions)
        {
            if (sideLoadOptions != UserSideLoadOptions.None)
            {
                if (sideLoadOptions.HasFlag(UserSideLoadOptions.None))
                {
                    sideLoadOptions &= ~UserSideLoadOptions.None;
                }

                var sideLoads = sideLoadOptions.ToString().ToLower().Replace(" ", "");
                resource += (resource.Contains("?") ? "&" : "?") + "include=" + sideLoads;
                return resource;
            }

            return resource;
        }

        private static string CreateCommaSeperatedUserIdString(IEnumerable<User> users)
        {
            var userIds = users.Select(user => user.Id);
            var userIdsCommaSeperatedList = string.Join(",", userIds);

            return userIdsCommaSeperatedList;
        }
    }
}
