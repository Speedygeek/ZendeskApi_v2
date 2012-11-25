using ZendeskApi_v2.Models.Forums;
using ZendeskApi_v2.Models.Tags;

namespace ZendeskApi_v2.Requests
{
    public class Forums : Core
    {
        public Forums(string yourZendeskUrl, string user, string password)
            : base(yourZendeskUrl, user, password)
        {
        }

        public GroupForumResponse GetForums()
        {
            return GenericGet<GroupForumResponse>("forums.json");
        }

        public IndividualForumResponse GetForumById(long forumId)
        {
            return GenericGet<IndividualForumResponse>(string.Format("forums/{0}.json", forumId));
        }

        public GroupForumResponse GetForumsByCategory(long categoryId)
        {
            return GenericGet<GroupForumResponse>(string.Format("categories/{0}/forums.json", categoryId));
        }

        public IndividualForumResponse CreateForum(Forum forum)
        {
            var body = new { forum };
            return GenericPost<IndividualForumResponse>("forums.json", body);
        }

        public IndividualForumResponse UpdateForum(Forum forum)
        {
            var body = new { forum };
            return GenericPut<IndividualForumResponse>(string.Format("forums/{0}.json", forum.Id), body);
        }

        public bool DeleteForum(long id)
        {
            return GenericDelete(string.Format("forums/{0}.json", id));
        }

        public GroupForumSubcriptionResponse GetForumSubscriptions()
        {
            return GenericGet<GroupForumSubcriptionResponse>("forum_subscriptions.json");
        }

        public GroupForumSubcriptionResponse GetForumSubscriptionsByForumId(long forumId)
        {
            return GenericGet<GroupForumSubcriptionResponse>(string.Format("forums/{0}/subscriptions.json", forumId));
        }

        public IndividualForumSubcriptionResponse GetForumSubscriptionsById(long subscriptionId)
        {
            return GenericGet<IndividualForumSubcriptionResponse>(string.Format("forum_subscriptions/{0}.json", subscriptionId));
        }

        public IndividualForumSubcriptionResponse CreateForumSubscription(ForumSubscription forumSubscription)
        {
            return GenericPost<IndividualForumSubcriptionResponse>(string.Format("forum_subscriptions.json"), forumSubscription);
        }

        public bool DeleteForumSubscription(long subscriptionId)
        {
            return GenericDelete(string.Format("forum_subscriptions/{0}.json", subscriptionId));
        }
    }
}