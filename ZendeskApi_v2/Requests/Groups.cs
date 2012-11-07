using ZendeskApi_v2.Models.Groups;

namespace ZenDeskApi_v2.Requests
{
    public class Groups : Core
    {
        public Groups(string yourZenDeskUrl, string user, string password)
            : base(yourZenDeskUrl, user, password)
        {
        }

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
    }
}