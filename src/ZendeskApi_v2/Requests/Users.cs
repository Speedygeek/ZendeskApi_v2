using System;
using System.Collections.Generic;
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
        IndividualUserResponse GetUser(long id);
        IndividualUserRelatedInformationResponse GetUserRelatedInformation(long id);
        IndividualUserResponse MergeUser(long fromId, long intoId);
        GroupUserResponse GetMultipleUsers(IEnumerable<long> ids, UserSideLoadOptions sideLoadOptions = UserSideLoadOptions.None);
        GroupUserResponse SearchByEmail(string email);
        GroupUserResponse SearchByPhone(string phone);
        GroupUserResponse SearchByCustomUserField(string fieldKey, string fieldValue);
        GroupUserResponse SearchByExternalId(string externalId);
        GroupUserResponse GetUsersInGroup(long id);
        GroupUserResponse GetUsersInOrganization(long id);
        IndividualUserResponse CreateUser(User user);
        JobStatusResponse BulkCreateUsers(IEnumerable<User> users);
        IndividualUserResponse SuspendUser(long id);
        IndividualUserResponse UpdateUser(User user);
        bool DeleteUser(long id);
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
#endif

#if ASYNC
        Task<IndividualUserResponse> GetCurrentUserAsync();
        Task<GroupUserResponse> GetAllUsersAsync(int? perPage = null, int? page = null, UserSideLoadOptions sideLoadOptions = UserSideLoadOptions.None);
        Task<IndividualUserResponse> GetUserAsync(long id);
        Task<IndividualUserRelatedInformationResponse> GetUserRelatedInformationAsync(long id);
        Task<IndividualUserResponse> MergeUserAsync(long fromId, long intoId);
        Task<GroupUserResponse> GetMultipleUsersAsync(IEnumerable<long> ids, UserSideLoadOptions sideLoadOptions = UserSideLoadOptions.None);
        Task<GroupUserResponse> SearchByEmailAsync(string email);
        Task<GroupUserResponse> SearchByPhoneAsync(string phone);
        Task<GroupUserResponse> SearchByCustomUserFieldAsync(string fieldKey, string fieldValue);
        Task<GroupUserResponse> SearchByExternalIdAsync(string externalId);
        Task<GroupUserResponse> GetUsersInGroupAsync(long id);
        Task<GroupUserResponse> GetUsersInOrganizationAsync(long id);
        Task<IndividualUserResponse> CreateUserAsync(User user);
        Task<JobStatusResponse> BulkCreateUsersAsync(IEnumerable<User> users);
        Task<IndividualUserResponse> SuspendUserAsync(long id);
        Task<IndividualUserResponse> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(long id);
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
#endif
    }

    public class Users : Core, IUsers
    {
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
            string resource = GetResourceStringWithSideLoadOptionsParam("users.json", sideLoadOptions);

            return GenericPagedGet<GroupUserResponse>(resource, perPage, page);
        }

        public IndividualUserResponse GetUser(long id)
        {
            return GenericGet<IndividualUserResponse>(string.Format("users/{0}.json", id));
        }

        public IndividualUserRelatedInformationResponse GetUserRelatedInformation(long id)
        {
            return GenericGet<IndividualUserRelatedInformationResponse>(string.Format("users/{0}/related.json", id));
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
            return GenericPut<IndividualUserResponse>(string.Format("users/{0}/merge.json", fromId), body);
        }

        public GroupUserResponse GetMultipleUsers(IEnumerable<long> ids, UserSideLoadOptions sideLoadOptions = UserSideLoadOptions.None)
        {
            string resource = GetResourceStringWithSideLoadOptionsParam(string.Format("users/show_many.json?ids={0}", ids.ToCsv()), sideLoadOptions);

            return GenericGet<GroupUserResponse>(resource);
        }

        public GroupUserResponse SearchByEmail(string email)
        {
            return GenericGet<GroupUserResponse>(string.Format("users/search.json?query={0}", email));
        }

        public GroupUserResponse SearchByPhone(string phone)
        {
            return GenericGet<GroupUserResponse>(string.Format("users/search.json?query=role:end-user phone:*{0}", Regex.Replace(phone, @"\D", "")));
        }

        public GroupUserResponse SearchByCustomUserField(string fieldKey, string fieldValue)
        {
            return GenericGet<GroupUserResponse>(string.Format("users/search.json?query={0}:{1}", fieldKey, fieldValue));
        }

        public GroupUserResponse SearchByExternalId(string externalId)
        {
            return GenericGet<GroupUserResponse>(string.Format("users/search.json?external_id={0}", externalId));
        }

        public GroupUserResponse GetUsersInGroup(long id)
        {
            return GenericGet<GroupUserResponse>(string.Format("groups/{0}/users.json", id));
        }

        public GroupUserResponse GetUsersInOrganization(long id)
        {
            return GenericGet<GroupUserResponse>(string.Format("organizations/{0}/users.json", id));
        }

        public IndividualUserResponse CreateUser(User user)
        {
            var body = new { user = user };
            return GenericPost<IndividualUserResponse>("users.json", body);
        }

        public JobStatusResponse BulkCreateUsers(IEnumerable<User> users)
        {
            var body = new { users = users };
            return GenericPost<JobStatusResponse>("users/create_many.json", body);
        }

        public IndividualUserResponse SuspendUser(long id)
        {
            var body = new { user = new { suspended = true } };
            return GenericPut<IndividualUserResponse>(string.Format("users/{0}.json", id), body);
        }

        public IndividualUserResponse UpdateUser(User user)
        {
            var body = new { user = user };
            return GenericPut<IndividualUserResponse>(string.Format("users/{0}.json", user.Id), body);
        }

        public bool DeleteUser(long id)
        {
            return GenericDelete(string.Format("users/{0}.json", id));
        }

        public bool SetUsersPassword(long userId, string newPassword)
        {
            var body = new { password = newPassword };
            return GenericBoolPost(string.Format("users/{0}/password.json", userId), body);
        }

        public bool ChangeUsersPassword(long userId, string oldPassword, string newPassword)
        {
            var body = new
            {
                previous_password = oldPassword,
                password = newPassword
            };
            return GenericBoolPost(string.Format("users/{0}/password.json", userId), body);
        }

        public GroupUserIdentityResponse GetUserIdentities(long userId)
        {
            return GenericGet<GroupUserIdentityResponse>(string.Format("users/{0}/identities.json", userId));
        }

        public IndividualUserIdentityResponse GetSpecificUserIdentity(long userId, long identityId)
        {
            return GenericGet<IndividualUserIdentityResponse>(string.Format("users/{0}/identities/{1}.json", userId, identityId));
        }

        public IndividualUserIdentityResponse AddUserIdentity(long userId, UserIdentity identity)
        {
            var body = new { identity };
            return GenericPost<IndividualUserIdentityResponse>(string.Format("users/{0}/identities.json", userId), body);
        }
        public IndividualUserIdentityResponse UpdateUserIdentity(long userId, UserIdentity identity)
        {
            var body = new { identity };
            return GenericPost<IndividualUserIdentityResponse>(string.Format("users/{0}/identities.json", userId), body);
        }

        public IndividualUserIdentityResponse SetUserIdentityAsVerified(long userId, long identityId)
        {
            return GenericPut<IndividualUserIdentityResponse>(string.Format("users/{0}/identities/{1}/verify.json", userId, identityId));
        }

        public GroupUserIdentityResponse SetUserIdentityAsPrimary(long userId, long identityId)
        {
            return GenericPut<GroupUserIdentityResponse>(string.Format("users/{0}/identities/{1}/make_primary.json", userId, identityId));
        }

        /// <summary>
        /// This sends a verification email to the user, asking him to click a link in order to verify ownership of the email address
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="identityId"></param>
        /// <returns></returns>
        public IndividualUserIdentityResponse SendUserVerificationRequest(long userId, long identityId)
        {
            return GenericPut<IndividualUserIdentityResponse>(string.Format("users/{0}/identities/{1}/request_verification.json", userId, identityId));
        }

        public bool DeleteUserIdentity(long userId, long identityId)
        {
            return GenericDelete(string.Format("users/{0}/identities/{1}.json", userId, identityId));
        }

        public IndividualUserResponse SetUserPhoto(long userId, ZenFile photo)
        {
            return GenericPut<IndividualUserResponse>(string.Format("users/{0}.json", userId), photo, "user[photo][uploaded_data]");
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

        public async Task<IndividualUserResponse> GetUserAsync(long id)
        {
            return await GenericGetAsync<IndividualUserResponse>(string.Format("users/{0}.json", id));
        }

        public async Task<IndividualUserRelatedInformationResponse> GetUserRelatedInformationAsync(long id)
        {
            return await GenericGetAsync<IndividualUserRelatedInformationResponse>(string.Format("users/{0}/related.json", id));
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
            return await GenericPutAsync<IndividualUserResponse>(string.Format("users/{0}/merge.json", fromId), body);
        }

        public async Task<GroupUserResponse> GetMultipleUsersAsync(IEnumerable<long> ids, UserSideLoadOptions sideLoadOptions = UserSideLoadOptions.None)
        {
            return await GenericGetAsync<GroupUserResponse>(string.Format("users/show_many.json?ids={0}", ids.ToCsv()));
        }

        public async Task<GroupUserResponse> SearchByEmailAsync(string email)
        {
            return await GenericGetAsync<GroupUserResponse>(string.Format("users/search.json?query={0}", email));
        }

        public async Task<GroupUserResponse> SearchByPhoneAsync(string phone)
        {
            return await GenericGetAsync<GroupUserResponse>(string.Format("users/search.json?query=role:end-user phone:*{0}", Regex.Replace(phone, @"\D", "")));
        }

        public async Task<GroupUserResponse> SearchByCustomUserFieldAsync(string fieldKey, string fieldValue)
        {
            return await GenericGetAsync<GroupUserResponse>(string.Format("users/search.json?query={0}:{1}", fieldKey, fieldValue));
        }

        public async Task<GroupUserResponse> SearchByExternalIdAsync(string externalId)
        {
            return await GenericGetAsync<GroupUserResponse>(string.Format("users/search.json?external_id={0}", externalId));
        }

        public async Task<GroupUserResponse> GetUsersInGroupAsync(long id)
        {
            return await GenericGetAsync<GroupUserResponse>(string.Format("groups/{0}/users.json", id));
        }

        public async Task<GroupUserResponse> GetUsersInOrganizationAsync(long id)
        {
            return await GenericGetAsync<GroupUserResponse>(string.Format("organizations/{0}/users.json", id));
        }

        public async Task<IndividualUserResponse> CreateUserAsync(User user)
        {
            var body = new { user = user };
            return await GenericPostAsync<IndividualUserResponse>("users.json", body);
        }

        public async Task<JobStatusResponse> BulkCreateUsersAsync(IEnumerable<User> users)
        {
            var body = new { users = users };
            return await GenericPostAsync<JobStatusResponse>("users/create_many.json", body);
        }

        public async Task<IndividualUserResponse> SuspendUserAsync(long id)
        {
            var body = new { user = new { suspended = true } };
            return await GenericPutAsync<IndividualUserResponse>(string.Format("users/{0}.json", id), body);
        }

        public async Task<IndividualUserResponse> UpdateUserAsync(User user)
        {
            var body = new { user = user };
            return await GenericPutAsync<IndividualUserResponse>(string.Format("users/{0}.json", user.Id), body);
        }

        public async Task<bool> DeleteUserAsync(long id)
        {
            return await GenericDeleteAsync(string.Format("users/{0}.json", id));
        }

        public async Task<bool> SetUsersPasswordAsync(long userId, string newPassword)
        {
            var body = new { password = newPassword };
            return await GenericBoolPostAsync(string.Format("users/{0}/password.json", userId), body);
        }

        public async Task<bool> ChangeUsersPasswordAsync(long userId, string oldPassword, string newPassword)
        {
            var body = new
            {
                previous_password = oldPassword,
                password = newPassword
            };
            return await GenericBoolPostAsync(string.Format("users/{0}/password.json", userId), body);
        }

        public async Task<GroupUserIdentityResponse> GetUserIdentitiesAsync(long userId)
        {
            return await GenericGetAsync<GroupUserIdentityResponse>(string.Format("users/{0}/identities.json", userId));
        }

        public async Task<IndividualUserIdentityResponse> GetSpecificUserIdentityAsync(long userId, long identityId)
        {
            return await GenericGetAsync<IndividualUserIdentityResponse>(string.Format("users/{0}/identities/{1}.json", userId, identityId));
        }

        public async Task<IndividualUserIdentityResponse> AddUserIdentityAsync(long userId, UserIdentity identity)
        {
            var body = new { identity };
            return await GenericPostAsync<IndividualUserIdentityResponse>(string.Format("users/{0}/identities.json", userId), body);
        }

        public async Task<IndividualUserIdentityResponse> UpdateUserIdentityAsync(long userId, UserIdentity identity)
        {
            var body = new { identity };
            return await GenericPostAsync<IndividualUserIdentityResponse>(string.Format("users/{0}/identities.json", userId), body);
        }

        public async Task<IndividualUserIdentityResponse> SetUserIdentityAsVerifiedAsync(long userId, long identityId)
        {
            return await GenericPutAsync<IndividualUserIdentityResponse>(string.Format("users/{0}/identities/{1}/verify.json", userId, identityId));
        }

        public async Task<GroupUserIdentityResponse> SetUserIdentityAsPrimaryAsync(long userId, long identityId)
        {
            return await GenericPutAsync<GroupUserIdentityResponse>(string.Format("users/{0}/identities/{1}/make_primary.json", userId, identityId));
        }

        /// <summary>
        /// This sends a verification email to the user, asking him to click a link in order to verify ownership of the email address
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="identityId"></param>
        /// <returns></returns>
        public async Task<IndividualUserIdentityResponse> SendUserVerificationRequestAsync(long userId, long identityId)
        {
            return await GenericPutAsync<IndividualUserIdentityResponse>(string.Format("users/{0}/identities/{1}/request_verification.json", userId, identityId));
        }

        public async Task<bool> DeleteUserIdentityAsync(long userId, long identityId)
        {
            return await GenericDeleteAsync(string.Format("users/{0}/identities/{1}.json", userId, identityId));
        }

        public Task<IndividualUserResponse> SetUserPhotoAsync(long userId, ZenFile photo)
        {
            return GenericPutAsync<IndividualUserResponse>(string.Format("users/{0}.json", userId), photo, "user[photo][uploaded_data]");
        }
#endif
        private string GetResourceStringWithSideLoadOptionsParam(string resource, UserSideLoadOptions sideLoadOptions)
        {
            if (sideLoadOptions != UserSideLoadOptions.None)
            {
                if (sideLoadOptions.HasFlag(UserSideLoadOptions.None))
                    sideLoadOptions &= ~UserSideLoadOptions.None;

                string sideLoads = sideLoadOptions.ToString().ToLower().Replace(" ", "");
                resource += (resource.Contains("?") ? "&" : "?") + "include=" + sideLoads;
                return resource;
            }

            return resource;
        }

    }
}
