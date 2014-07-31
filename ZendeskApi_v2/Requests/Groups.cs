#if ASYNC
using System.Threading.Tasks;
#endif
using ZendeskApi_v2.Models.Groups;

namespace ZendeskApi_v2.Requests
{
	public interface IGroups : ICore
	{
#if SYNC
		MultipleGroupResponse GetGroups();
		MultipleGroupResponse GetAssignableGroups();
		IndividualGroupResponse GetGroupById(long id);
		IndividualGroupResponse CreateGroup(string groupName);
		IndividualGroupResponse UpdateGroup(Group group);
		bool DeleteGroup(long id);
		MultipleGroupMembershipResponse GetGroupMemberships();
		MultipleGroupMembershipResponse GetGroupMembershipsByUser(long userId);
		MultipleGroupMembershipResponse GetGroupMembershipsByGroup(long groupId);
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
		Task<MultipleGroupResponse> GetGroupsAsync();
		Task<MultipleGroupResponse> GetAssignableGroupsAsync();
		Task<IndividualGroupResponse> GetGroupByIdAsync(long id);
		Task<IndividualGroupResponse> CreateGroupAsync(string groupName);
		Task<IndividualGroupResponse> UpdateGroupAsync(Group group);
		Task<bool> DeleteGroupAsync(long id);
		Task<MultipleGroupMembershipResponse> GetGroupMembershipsAsync();
		Task<MultipleGroupMembershipResponse> GetGroupMembershipsByUserAsync(long userId);
		Task<MultipleGroupMembershipResponse> GetGroupMembershipsByGroupAsync(long groupId);
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
        public Groups(string yourZendeskUrl, string user, string password, string apiToken)
            : base(yourZendeskUrl, user, password, apiToken)
        {
        }

#if SYNC
        public MultipleGroupResponse GetGroups()
        {
            return GenericGet<MultipleGroupResponse>("groups.json");
        }

        public MultipleGroupResponse GetAssignableGroups()
        {
            return GenericGet<MultipleGroupResponse>("groups/assignable.json");
        }

        public IndividualGroupResponse GetGroupById(long id)
        {
            return GenericGet<IndividualGroupResponse>(string.Format("groups/{0}.json", id));
        }

        public IndividualGroupResponse CreateGroup(string groupName)
        {
            var body = new {group = new {name = groupName}};
            return GenericPost<IndividualGroupResponse>("groups.json", body);
        }

        public IndividualGroupResponse UpdateGroup(Group group)
        {
            var body = new { group };
            return GenericPut<IndividualGroupResponse>(string.Format("groups/{0}.json", group.Id), body);
        }
        
        public bool DeleteGroup(long id)
        {            
            return GenericDelete(string.Format("groups/{0}.json", id));
        }



        public MultipleGroupMembershipResponse GetGroupMemberships()
        {
            return GenericGet<MultipleGroupMembershipResponse>("group_memberships.json");
        }

        public MultipleGroupMembershipResponse GetGroupMembershipsByUser(long userId)
        {
            return GenericGet<MultipleGroupMembershipResponse>(string.Format("users/{0}/group_memberships.json", userId));
        }

        public MultipleGroupMembershipResponse GetGroupMembershipsByGroup(long groupId)
        {
            return GenericGet<MultipleGroupMembershipResponse>(string.Format("groups/{0}/memberships.json", groupId));
        }

        public MultipleGroupMembershipResponse GetAssignableGroupMemberships()
        {
            return GenericGet<MultipleGroupMembershipResponse>("group_memberships/assignable.json");
        }

        public MultipleGroupMembershipResponse GetAssignableGroupMembershipsByGroup(long groupId)
        {
            return GenericGet<MultipleGroupMembershipResponse>(string.Format("groups/{0}/memberships/assignable.json", groupId));
        }

        public IndividualGroupMembershipResponse GetGroupMembershipsByMembershipId(long groupMembershipId)
        {
            return GenericGet<IndividualGroupMembershipResponse>(string.Format("group_memberships/{0}.json", groupMembershipId));
        }

        public IndividualGroupMembershipResponse GetGroupMembershipsByUserAndMembershipId(long userId, long groupMembershipId)
        {
            return GenericGet<IndividualGroupMembershipResponse>(string.Format("users/{0}/group_memberships/{1}.json", userId, groupMembershipId));
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
            return GenericPut<MultipleGroupMembershipResponse>(string.Format("users/{0}/group_memberships/{1}/make_default.json", userId, groupMembershipId));
        }

        public bool DeleteGroupMembership(long groupMembershipId)
        {            
            return GenericDelete(string.Format("group_memberships/{0}.json", groupMembershipId));
        }

        public bool DeleteUserGroupMembership(long userId, long groupMembershipId)
        {
            return GenericDelete(string.Format("users/{0}/group_memberships/{1}.json", userId, groupMembershipId));
        }
#endif

#if ASYNC
        public async Task<MultipleGroupResponse> GetGroupsAsync()
        {
            return await GenericGetAsync<MultipleGroupResponse>("groups.json");
        }

        public async Task<MultipleGroupResponse> GetAssignableGroupsAsync()
        {
            return await GenericGetAsync<MultipleGroupResponse>("groups/assignable.json");
        }

        public async Task<IndividualGroupResponse> GetGroupByIdAsync(long id)
        {
            return await GenericGetAsync<IndividualGroupResponse>(string.Format("groups/{0}.json", id));
        }

        public async Task<IndividualGroupResponse> CreateGroupAsync(string groupName)
        {
            var body = new { group = new { name = groupName } };
            return await GenericPostAsync<IndividualGroupResponse>("groups.json", body);
        }

        public async Task<IndividualGroupResponse> UpdateGroupAsync(Group group)
        {
            var body = new { group };
            return await GenericPutAsync<IndividualGroupResponse>(string.Format("groups/{0}.json", group.Id), body);
        }

        public async Task<bool> DeleteGroupAsync(long id)
        {
            return await GenericDeleteAsync(string.Format("groups/{0}.json", id));
        }

        public async Task<MultipleGroupMembershipResponse> GetGroupMembershipsAsync()
        {
            return await GenericGetAsync<MultipleGroupMembershipResponse>("group_memberships.json");
        }

        public async Task<MultipleGroupMembershipResponse> GetGroupMembershipsByUserAsync(long userId)
        {
            return await GenericGetAsync<MultipleGroupMembershipResponse>(string.Format("users/{0}/group_memberships.json", userId));
        }

        public async Task<MultipleGroupMembershipResponse> GetGroupMembershipsByGroupAsync(long groupId)
        {
            return await GenericGetAsync<MultipleGroupMembershipResponse>(string.Format("groups/{0}/memberships.json", groupId));
        }

        public async Task<MultipleGroupMembershipResponse> GetAssignableGroupMembershipsAsync()
        {
            return await GenericGetAsync<MultipleGroupMembershipResponse>("group_memberships/assignable.json");
        }

        public async Task<MultipleGroupMembershipResponse> GetAssignableGroupMembershipsByGroupAsync(long groupId)
        {
            return await GenericGetAsync<MultipleGroupMembershipResponse>(string.Format("groups/{0}/memberships/assignable.json", groupId));
        }

        public async Task<IndividualGroupMembershipResponse> GetGroupMembershipsByMembershipIdAsync(long groupMembershipId)
        {
            return await GenericGetAsync<IndividualGroupMembershipResponse>(string.Format("group_memberships/{0}.json", groupMembershipId));
        }

        public async Task<IndividualGroupMembershipResponse> GetGroupMembershipsByUserAndMembershipIdAsync(long userId, long groupMembershipId)
        {
            return await GenericGetAsync<IndividualGroupMembershipResponse>(string.Format("users/{0}/group_memberships/{1}.json", userId, groupMembershipId));
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
            return await GenericPutAsync<MultipleGroupMembershipResponse>(string.Format("users/{0}/group_memberships/{1}/make_default.json", userId, groupMembershipId));
        }

        public async Task<bool> DeleteGroupMembershipAsync(long groupMembershipId)
        {
            return await GenericDeleteAsync(string.Format("group_memberships/{0}.json", groupMembershipId));
        }

        public async Task<bool> DeleteUserGroupMembershipAsync(long userId, long groupMembershipId)
        {
            return await GenericDeleteAsync(string.Format("users/{0}/group_memberships/{1}.json", userId, groupMembershipId));
        }
#endif
    }
}