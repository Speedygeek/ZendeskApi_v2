#if ASYNC
using System.Threading.Tasks;
#endif
using ZendeskApi_v2.Models.Forums;
using ZendeskApi_v2.Models.Tags;

namespace ZendeskApi_v2.Requests
{
	public interface IForums : ICore
	{
#if SYNC
		GroupForumResponse GetForums();
		IndividualForumResponse GetForumById(long forumId);
		GroupForumResponse GetForumsByCategory(long categoryId);
		IndividualForumResponse CreateForum(Forum forum);
		IndividualForumResponse UpdateForum(Forum forum);
		bool DeleteForum(long id);
		GroupForumSubcriptionResponse GetForumSubscriptions();
		GroupForumSubcriptionResponse GetForumSubscriptionsByForumId(long forumId);
		IndividualForumSubcriptionResponse GetForumSubscriptionsById(long subscriptionId);
		IndividualForumSubcriptionResponse CreateForumSubscription(ForumSubscription forumSubscription);
		bool DeleteForumSubscription(long subscriptionId);
#endif

#if ASYNC
		Task<GroupForumResponse> GetForumsAsync();
		Task<IndividualForumResponse> GetForumByIdAsync(long forumId);
		Task<GroupForumResponse> GetForumsByCategoryAsync(long categoryId);
		Task<IndividualForumResponse> CreateForumAsync(Forum forum);
		Task<IndividualForumResponse> UpdateForumAsync(Forum forum);
		Task<bool> DeleteForumAsync(long id);
		Task<GroupForumSubcriptionResponse> GetForumSubscriptionsAsync();
		Task<GroupForumSubcriptionResponse> GetForumSubscriptionsByForumIdAsync(long forumId);
		Task<IndividualForumSubcriptionResponse> GetForumSubscriptionsByIdAsync(long subscriptionId);
		Task<IndividualForumSubcriptionResponse> CreateForumSubscriptionAsync(ForumSubscription forumSubscription);
		Task<bool> DeleteForumSubscriptionAsync(long subscriptionId);
#endif
	}

	public class Forums : Core, IForums
	{
        public Forums(string yourZendeskUrl, string user, string password, string apiToken, string p_OAuthToken)
            : base(yourZendeskUrl, user, password, apiToken, p_OAuthToken)
        {
        }
#if SYNC
        public GroupForumResponse GetForums()
        {
            return GenericGet<GroupForumResponse>("forums.json");
        }

        public IndividualForumResponse GetForumById(long forumId)
        {
            return GenericGet<IndividualForumResponse>($"forums/{forumId}.json");
        }

        public GroupForumResponse GetForumsByCategory(long categoryId)
        {
            return GenericGet<GroupForumResponse>($"categories/{categoryId}/forums.json");
        }

        public IndividualForumResponse CreateForum(Forum forum)
        {
            var body = new { forum };
            return GenericPost<IndividualForumResponse>("forums.json", body);
        }

        public IndividualForumResponse UpdateForum(Forum forum)
        {
            var body = new { forum };
            return GenericPut<IndividualForumResponse>($"forums/{forum.Id}.json", body);
        }

        public bool DeleteForum(long id)
        {
            return GenericDelete($"forums/{id}.json");
        }

        public GroupForumSubcriptionResponse GetForumSubscriptions()
        {
            return GenericGet<GroupForumSubcriptionResponse>("forum_subscriptions.json");
        }

        public GroupForumSubcriptionResponse GetForumSubscriptionsByForumId(long forumId)
        {
            return GenericGet<GroupForumSubcriptionResponse>($"forums/{forumId}/subscriptions.json");
        }

        public IndividualForumSubcriptionResponse GetForumSubscriptionsById(long subscriptionId)
        {
            return GenericGet<IndividualForumSubcriptionResponse>($"forum_subscriptions/{subscriptionId}.json");
        }

        public IndividualForumSubcriptionResponse CreateForumSubscription(ForumSubscription forumSubscription)
        {
            return GenericPost<IndividualForumSubcriptionResponse>(string.Format("forum_subscriptions.json"), forumSubscription);
        }

        public bool DeleteForumSubscription(long subscriptionId)
        {
            return GenericDelete($"forum_subscriptions/{subscriptionId}.json");
        }

#endif
#if ASYNC
        public async Task<GroupForumResponse> GetForumsAsync()
        {
            return await GenericGetAsync<GroupForumResponse>("forums.json");
        }

        public async Task<IndividualForumResponse> GetForumByIdAsync(long forumId)
        {
            return await GenericGetAsync<IndividualForumResponse>($"forums/{forumId}.json");
        }

        public async Task<GroupForumResponse> GetForumsByCategoryAsync(long categoryId)
        {
            return await GenericGetAsync<GroupForumResponse>($"categories/{categoryId}/forums.json");
        }

        public async Task<IndividualForumResponse> CreateForumAsync(Forum forum)
        {
            var body = new { forum };
            return await GenericPostAsync<IndividualForumResponse>("forums.json", body);
        }

        public async Task<IndividualForumResponse> UpdateForumAsync(Forum forum)
        {
            var body = new { forum };
            return await GenericPutAsync<IndividualForumResponse>($"forums/{forum.Id}.json", body);
        }

        public async Task<bool> DeleteForumAsync(long id)
        {
            return await GenericDeleteAsync($"forums/{id}.json");
        }

        public async Task<GroupForumSubcriptionResponse> GetForumSubscriptionsAsync()
        {
            return await GenericGetAsync<GroupForumSubcriptionResponse>("forum_subscriptions.json");
        }

        public async Task<GroupForumSubcriptionResponse> GetForumSubscriptionsByForumIdAsync(long forumId)
        {
            return await GenericGetAsync<GroupForumSubcriptionResponse>($"forums/{forumId}/subscriptions.json");
        }

        public async Task<IndividualForumSubcriptionResponse> GetForumSubscriptionsByIdAsync(long subscriptionId)
        {
            return await GenericGetAsync<IndividualForumSubcriptionResponse>($"forum_subscriptions/{subscriptionId}.json");
        }

        public async Task<IndividualForumSubcriptionResponse> CreateForumSubscriptionAsync(ForumSubscription forumSubscription)
        {
            return await GenericPostAsync<IndividualForumSubcriptionResponse>(string.Format("forum_subscriptions.json"), forumSubscription);
        }

        public async Task<bool> DeleteForumSubscriptionAsync(long subscriptionId)
        {
            return await GenericDeleteAsync($"forum_subscriptions/{subscriptionId}.json");
        }
#endif
    }
}