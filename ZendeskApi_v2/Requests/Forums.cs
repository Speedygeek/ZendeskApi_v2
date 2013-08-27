#if ASYNC
using System.Threading.Tasks;
#endif
using ZendeskApi_v2.Models.Forums;
using ZendeskApi_v2.Models.Tags;

namespace ZendeskApi_v2.Requests
{
    public class Forums : Core
    {
        internal Forums(IZendeskConnectionSettings connectionSettings)
            : base(connectionSettings)
        {
        }
#if SYNC
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

#endif
#if ASYNC
        public async Task<GroupForumResponse> GetForumsAsync()
        {
            return await GenericGetAsync<GroupForumResponse>("forums.json");
        }

        public async Task<IndividualForumResponse> GetForumByIdAsync(long forumId)
        {
            return await GenericGetAsync<IndividualForumResponse>(string.Format("forums/{0}.json", forumId));
        }

        public async Task<GroupForumResponse> GetForumsByCategoryAsync(long categoryId)
        {
            return await GenericGetAsync<GroupForumResponse>(string.Format("categories/{0}/forums.json", categoryId));
        }

        public async Task<IndividualForumResponse> CreateForumAsync(Forum forum)
        {
            var body = new { forum };
            return await GenericPostAsync<IndividualForumResponse>("forums.json", body);
        }

        public async Task<IndividualForumResponse> UpdateForumAsync(Forum forum)
        {
            var body = new { forum };
            return await GenericPutAsync<IndividualForumResponse>(string.Format("forums/{0}.json", forum.Id), body);
        }

        public async Task<bool> DeleteForumAsync(long id)
        {
            return await GenericDeleteAsync(string.Format("forums/{0}.json", id));
        }

        public async Task<GroupForumSubcriptionResponse> GetForumSubscriptionsAsync()
        {
            return await GenericGetAsync<GroupForumSubcriptionResponse>("forum_subscriptions.json");
        }

        public async Task<GroupForumSubcriptionResponse> GetForumSubscriptionsByForumIdAsync(long forumId)
        {
            return await GenericGetAsync<GroupForumSubcriptionResponse>(string.Format("forums/{0}/subscriptions.json", forumId));
        }

        public async Task<IndividualForumSubcriptionResponse> GetForumSubscriptionsByIdAsync(long subscriptionId)
        {
            return await GenericGetAsync<IndividualForumSubcriptionResponse>(string.Format("forum_subscriptions/{0}.json", subscriptionId));
        }

        public async Task<IndividualForumSubcriptionResponse> CreateForumSubscriptionAsync(ForumSubscription forumSubscription)
        {
            return await GenericPostAsync<IndividualForumSubcriptionResponse>(string.Format("forum_subscriptions.json"), forumSubscription);
        }

        public async Task<bool> DeleteForumSubscriptionAsync(long subscriptionId)
        {
            return await GenericDeleteAsync(string.Format("forum_subscriptions/{0}.json", subscriptionId));
        }
#endif
    }
}