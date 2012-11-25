using ZendeskApi_v2.Models.Triggers;

namespace ZendeskApi_v2.Requests
{
    public class Triggers : Core
    {
        public Triggers(string yourZendeskUrl, string user, string password)
            : base(yourZendeskUrl, user, password)
        {
        }

        public GroupTriggerResponse GetTriggers()
        {
            return GenericGet<GroupTriggerResponse>(string.Format("triggers.json"));
        }

        public IndividualTriggerResponse GetTriggerById(long id)
        {
            return GenericGet<IndividualTriggerResponse>(string.Format("triggers/{0}.json", id));
        }

        public GroupTriggerResponse GetActiveTriggers()
        {
            return GenericGet<GroupTriggerResponse>(string.Format("triggers/active.json"));
        }
    }
}