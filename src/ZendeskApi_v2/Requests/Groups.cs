#if ASYNC
using System.Threading.Tasks;
#endif
using ZendeskApi_v2.Models.Groups;

namespace ZendeskApi_v2.Requests
{
    public interface IGroups : ICore
    {
#if SYNC
        MultipleGroupResponse GetGroups(int? perPage = null, int? page = null);
        MultipleGroupResponse GetAssignableGroups();
        IndividualGroupResponse GetGroupById(long id);
        IndividualGroupResponse CreateGroup(string groupName);
        IndividualGroupResponse UpdateGroup(Group group);
        bool DeleteGroup(long id);
        MultipleGroupMembershipResponse GetGroupMemberships();
        MultipleGroupMembershipResponse GetGroupMembershipsByUser(long userId);
        MultipleGroupMembershipResponse GetGroupMembershipsByUser(long userId, int perPage, int page);
        MultipleGroupMembershipResponse GetGroupMembershipsByGroup(long groupId);
        MultipleGroupMembershipResponse GetGroupMembershipsByGroup(long groupId, int perPage, int page);
        MultipleGroupMembershipResponse GetAssignableGroupMemberships();
        MultipleGroupMembershipResponse GetAssignableGroupMembershipsByGroup(long groupId);
        IndividualGroupMembershipResponse GetGroupMembershipsByMembershipId(long groupMembershipId);
        IndividualGroupMembershipResponse GetGroupMembershipsByUserAndMembershipId(long userId, long groupMembershipId);

        /// <summary>
        /// Creating a membership means assigning an agent to a given group
        /// </summary>
        /// <param name="groupMembership"></param>
        /// <returns></returns>
        IndividualGroupMembershipResponse CreateGroupMembership(GroupMembership groupMembership);

        MultipleGroupMembershipResponse SetGroupMembershipAsDefault(long userId, long groupMembershipId);
        bool DeleteGroupMembership(long groupMembershipId);
        bool DeleteUserGroupMembership(long userId, long groupMembershipId);
#endif

#if ASYNC
        Task<MultipleGroupResponse> GetGroupsAsync(int? perPage = null, int? page = null);
        Task<MultipleGroupResponse> GetAssignableGroupsAsync();
        Task<IndividualGroupResponse> GetGroupByIdAsync(long id);
        Task<IndividualGroupResponse> CreateGroupAsync(string groupName);
        Task<IndividualGroupResponse> UpdateGroupAsync(Group group);
        Task<bool> DeleteGroupAsync(long id);
        Task<MultipleGroupMembershipResponse> GetGroupMembershipsAsync();
        Task<MultipleGroupMembershipResponse> GetGroupMembershipsByUserAsync(long userId);
        Task<MultipleGroupMembershipResponse> GetGroupMembershipsByUserAsync(long userId, int perPage, int page);
        Task<MultipleGroupMembershipResponse> GetGroupMembershipsByGroupAsync(long groupId);
        Task<MultipleGroupMembershipResponse> GetGroupMembershipsByGroupAsync(long groupId, int perPage, int page);
        Task<MultipleGroupMembershipResponse> GetAssignableGroupMembershipsAsync();
        Task<MultipleGroupMembershipResponse> GetAssignableGroupMembershipsByGroupAsync(long groupId);
        Task<IndividualGroupMembershipResponse> GetGroupMembershipsByMembershipIdAsync(long groupMembershipId);
        Task<IndividualGroupMembershipResponse> GetGroupMembershipsByUserAndMembershipIdAsync(long userId, long groupMembershipId);

        /// <summary>
        /// Creating a membership means assigning an agent to a given group
        /// </summary>
        /// <param name="groupMembership"></param>
        /// <returns></returns>
        Task<IndividualGroupMembershipResponse> CreateGroupMembershipAsync(GroupMembership groupMembership);

        Task<MultipleGroupMembershipResponse> SetGroupMembershipAsDefaultAsync(long userId, long groupMembershipId);
        Task<bool> DeleteGroupMembershipAsync(long groupMembershipId);
        Task<bool> DeleteUserGroupMembershipAsync(long userId, long groupMembershipId);
#endif
    }

    public class Groups : Core, IGroups
    {
        public Groups(string yourZendeskUrl, string user, string password, string apiToken, string p_OAuthToken)
            : base(yourZendeskUrl, user, password, apiToken, p_OAuthToken)
        {
        }

#if SYNC
        public MultipleGroupResponse GetGroups(int? perPage = null, int? page = null)
        {
            return GenericPagedGet<MultipleGroupResponse>("groups.json", perPage, page);
        }

        public MultipleGroupResponse GetAssignableGroups()
        {
            return GenericGet<MultipleGroupResponse>("groups/assignable.json");
        }

        public IndividualGroupResponse GetGroupById(long id)
        {
            return GenericGet<IndividualGroupResponse>($"groups/{id}.json");
        }

        public IndividualGroupResponse CreateGroup(string groupName)
        {
            var body = new {group = new {name = groupName}};
            return GenericPost<IndividualGroupResponse>("groups.json", body);
        }

        public IndividualGroupResponse UpdateGroup(Group group)
        {
            var body = new { group };
            return GenericPut<IndividualGroupResponse>($"groups/{group.Id}.json", body);
        }

        public bool DeleteGroup(long id)
        {
            return GenericDelete($"groups/{id}.json");
        }

        public MultipleGroupMembershipResponse GetGroupMemberships()
        {
            return GenericGet<MultipleGroupMembershipResponse>("group_memberships.json");
        }

        public MultipleGroupMembershipResponse GetGroupMembershipsByUser(long userId)
        {
            return GenericGet<MultipleGroupMembershipResponse>($"users/{userId}/group_memberships.json");
        }

        public MultipleGroupMembershipResponse GetGroupMembershipsByUser(long userId, int perPage, int page)
        {
            return GenericPagedGet<MultipleGroupMembershipResponse>($"users/{userId}/group_memberships.json", perPage, page);
        }

        public MultipleGroupMembershipResponse GetGroupMembershipsByGroup(long groupId)
        {
            return GenericGet<MultipleGroupMembershipResponse>($"groups/{groupId}/memberships.json");
        }

        public MultipleGroupMembershipResponse GetGroupMembershipsByGroup(long groupId, int perPage, int page)
        {
            return GenericPagedGet<MultipleGroupMembershipResponse>($"groups/{groupId}/memberships.json", perPage,
                page);
        }

        public MultipleGroupMembershipResponse GetAssignableGroupMemberships()
        {
            return GenericGet<MultipleGroupMembershipResponse>("group_memberships/assignable.json");
        }

        public MultipleGroupMembershipResponse GetAssignableGroupMembershipsByGroup(long groupId)
        {
            return GenericGet<MultipleGroupMembershipResponse>($"groups/{groupId}/memberships/assignable.json");
        }

        public IndividualGroupMembershipResponse GetGroupMembershipsByMembershipId(long groupMembershipId)
        {
            return GenericGet<IndividualGroupMembershipResponse>($"group_memberships/{groupMembershipId}.json");
        }

        public IndividualGroupMembershipResponse GetGroupMembershipsByUserAndMembershipId(long userId, long groupMembershipId)
        {
            return GenericGet<IndividualGroupMembershipResponse>($"users/{userId}/group_memberships/{groupMembershipId}.json");
        }

        /// <summary>
        /// Creating a membership means assigning an agent to a given group
        /// </summary>
        /// <param name="groupMembership"></param>
        /// <returns></returns>
        public IndividualGroupMembershipResponse CreateGroupMembership(GroupMembership groupMembership)
        {
            var body = new {group_membership = groupMembership};
            return GenericPost<IndividualGroupMembershipResponse>(string.Format("group_memberships.json"), body);
        }

        public MultipleGroupMembershipResponse SetGroupMembershipAsDefault(long userId, long groupMembershipId)
        {
            return GenericPut<MultipleGroupMembershipResponse>($"users/{userId}/group_memberships/{groupMembershipId}/make_default.json");
        }

        public bool DeleteGroupMembership(long groupMembershipId)
        {
            return GenericDelete($"group_memberships/{groupMembershipId}.json");
        }

        public bool DeleteUserGroupMembership(long userId, long groupMembershipId)
        {
            return GenericDelete($"users/{userId}/group_memberships/{groupMembershipId}.json");
        }
#endif

#if ASYNC
        public async Task<MultipleGroupResponse> GetGroupsAsync(int? perPage = null, int? page = null)
        {
            return await GenericPagedGetAsync<MultipleGroupResponse>("groups.json", perPage, page);
        }

        public async Task<MultipleGroupResponse> GetAssignableGroupsAsync()
        {
            return await GenericGetAsync<MultipleGroupResponse>("groups/assignable.json");
        }

        public async Task<IndividualGroupResponse> GetGroupByIdAsync(long id)
        {
            return await GenericGetAsync<IndividualGroupResponse>($"groups/{id}.json");
        }

        public async Task<IndividualGroupResponse> CreateGroupAsync(string groupName)
        {
            var body = new { group = new { name = groupName } };
            return await GenericPostAsync<IndividualGroupResponse>("groups.json", body);
        }

        public async Task<IndividualGroupResponse> UpdateGroupAsync(Group group)
        {
            var body = new { group };
            return await GenericPutAsync<IndividualGroupResponse>($"groups/{group.Id}.json", body);
        }

        public async Task<bool> DeleteGroupAsync(long id)
        {
            return await GenericDeleteAsync($"groups/{id}.json");
        }

        public async Task<MultipleGroupMembershipResponse> GetGroupMembershipsAsync()
        {
            return await GenericGetAsync<MultipleGroupMembershipResponse>("group_memberships.json");
        }

        public async Task<MultipleGroupMembershipResponse> GetGroupMembershipsByUserAsync(long userId)
        {
            return await GenericGetAsync<MultipleGroupMembershipResponse>($"users/{userId}/group_memberships.json");
        }

        public async Task<MultipleGroupMembershipResponse> GetGroupMembershipsByUserAsync(long userId, int perPage, int page)
        {
            return await GenericPagedGetAsync<MultipleGroupMembershipResponse>($"users/{userId}/group_memberships.json", perPage,
                page);
        }

        public async Task<MultipleGroupMembershipResponse> GetGroupMembershipsByGroupAsync(long groupId)
        {
            return await GenericGetAsync<MultipleGroupMembershipResponse>($"groups/{groupId}/memberships.json");
        }

        public async Task<MultipleGroupMembershipResponse> GetGroupMembershipsByGroupAsync(long groupId, int perPage, int page)
        {
            return await GenericPagedGetAsync<MultipleGroupMembershipResponse>($"groups/{groupId}/memberships.json", perPage,
                page);
        }

        public async Task<MultipleGroupMembershipResponse> GetAssignableGroupMembershipsAsync()
        {
            return await GenericGetAsync<MultipleGroupMembershipResponse>("group_memberships/assignable.json");
        }

        public async Task<MultipleGroupMembershipResponse> GetAssignableGroupMembershipsByGroupAsync(long groupId)
        {
            return await GenericGetAsync<MultipleGroupMembershipResponse>($"groups/{groupId}/memberships/assignable.json");
        }

        public async Task<IndividualGroupMembershipResponse> GetGroupMembershipsByMembershipIdAsync(long groupMembershipId)
        {
            return await GenericGetAsync<IndividualGroupMembershipResponse>($"group_memberships/{groupMembershipId}.json");
        }

        public async Task<IndividualGroupMembershipResponse> GetGroupMembershipsByUserAndMembershipIdAsync(long userId, long groupMembershipId)
        {
            return await GenericGetAsync<IndividualGroupMembershipResponse>($"users/{userId}/group_memberships/{groupMembershipId}.json");
        }

        /// <summary>
        /// Creating a membership means assigning an agent to a given group
        /// </summary>
        /// <param name="groupMembership"></param>
        /// <returns></returns>
        public async Task<IndividualGroupMembershipResponse> CreateGroupMembershipAsync(GroupMembership groupMembership)
        {
            var body = new { group_membership = groupMembership };
            return await GenericPostAsync<IndividualGroupMembershipResponse>(string.Format("group_memberships.json"), body);
        }

        public async Task<MultipleGroupMembershipResponse> SetGroupMembershipAsDefaultAsync(long userId, long groupMembershipId)
        {
            return await GenericPutAsync<MultipleGroupMembershipResponse>($"users/{userId}/group_memberships/{groupMembershipId}/make_default.json");
        }

        public async Task<bool> DeleteGroupMembershipAsync(long groupMembershipId)
        {
            return await GenericDeleteAsync($"group_memberships/{groupMembershipId}.json");
        }

        public async Task<bool> DeleteUserGroupMembershipAsync(long userId, long groupMembershipId)
        {
            return await GenericDeleteAsync($"users/{userId}/group_memberships/{groupMembershipId}.json");
        }
#endif
    }
}
