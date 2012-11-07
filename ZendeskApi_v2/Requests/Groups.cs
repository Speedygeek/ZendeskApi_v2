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
    }
}