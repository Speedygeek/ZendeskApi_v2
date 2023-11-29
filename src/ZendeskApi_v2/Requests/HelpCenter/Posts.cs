#if ASYNC
using System.Collections.Generic;
using System.Threading.Tasks;
#endif
using ZendeskApi_v2.Models.HelpCenter.Post;
using ZendeskApi_v2.Models.HelpCenter.Subscriptions;

namespace ZendeskApi_v2.Requests.HelpCenter
{
    public interface IPosts : ICore
    {
#if SYNC
        GroupPostResponse GetPosts(int? perPage = null, int? page = null);
        GroupPostResponse GetPostsByTopicId(long topicId, int? perPage = null, int? page = null);
        GroupPostResponse GetPostsByUserId(long userId, int? perPage = null, int? page = null);
        IndividualPostResponse GetPost(long postId);
        IndividualPostResponse CreatePost(Post post);
        IndividualPostResponse UpdatePost(Post post);
        bool DeletePost(long PostId);
        IndividualSubscriptionResponse CreateSubscription(long postId, Subscription subscription);
        IndividualSubscriptionResponse GetSubscription(long postId, long subscriptionId, SubscriptionSideLoadOptions subscriptionSideLoadOptions = SubscriptionSideLoadOptions.None);
        GroupSubscriptionsResponse GetSubscriptions(long postId, SubscriptionSideLoadOptions subscriptionSideLoadOptions = SubscriptionSideLoadOptions.None, int? perPage = null, int? page = null);
        bool DeleteSubscription(long postId, long subscriptionId);
#endif
#if ASYNC
        Task<GroupPostResponse> GetPostsAsync(int? perPage = null, int? page = null);
        Task<GroupPostResponse> GetPostsByTopicIdAsync(long topicId, int? perPage = null, int? page = null);
        Task<GroupPostResponse> GetPostsByUserIdAsync(long userId, int? perPage = null, int? page = null);
        Task<IndividualPostResponse> GetPostAsync(long postId);
        Task<IndividualPostResponse> CreatePostAsync(Post post);
        Task<IndividualPostResponse> UpdatePostAsync(Post post);
        Task<bool> DeletePostAsync(long PostId);
        Task<IndividualSubscriptionResponse> CreateSubscriptionAsync(long postId, Subscription subscription);
        Task<IndividualSubscriptionResponse> GetSubscriptionAsync(long postId, long subscriptionId, SubscriptionSideLoadOptions subscriptionSideLoadOptions = SubscriptionSideLoadOptions.None);
        Task<GroupSubscriptionsResponse> GetSubscriptionsAsync(long postId, SubscriptionSideLoadOptions subscriptionSideLoadOptions = SubscriptionSideLoadOptions.None, int? perPage = null, int? page = null);
        Task<bool> DeleteSubscriptionAsync(long postId, long subscriptionId);
#endif
    }

    public class Posts : Core, IPosts
    {
        public Posts(string yourZendeskUrl, string user, string password, string apiToken, string p_OAuthToken, Dictionary<string,string> customHeaders)
            : base(yourZendeskUrl, user, password, apiToken, p_OAuthToken, customHeaders)
        {
        }
#if SYNC
        public GroupPostResponse GetPosts(int? perPage = null, int? page = null)
        {
            return GenericPagedGet<GroupPostResponse>("community/posts.json", perPage, page);
        }

        public GroupPostResponse GetPostsByTopicId(long topicId, int? perPage = null, int? page = null)
        {
            return GenericPagedGet<GroupPostResponse>($"community/topics/{topicId}/posts.json", perPage, page);
        }

        public GroupPostResponse GetPostsByUserId(long userId, int? perPage = null, int? page = null)
        {
            return GenericPagedGet<GroupPostResponse>($"community/users/{userId}/posts.json", perPage, page);
        }

        public IndividualPostResponse GetPost(long postId)
        {
            return GenericGet<IndividualPostResponse>($"community/posts/{postId}.json");
        }

        public IndividualPostResponse CreatePost(Post post)
        {
            return GenericPost<IndividualPostResponse>("community/posts.json", new { post });
        }

        public IndividualPostResponse UpdatePost(Post post)
        {
            return GenericPut<IndividualPostResponse>($"community/posts/{post.Id.Value}.json", new { post });
        }

        public bool DeletePost(long postId)
        {
            return GenericDelete($"community/posts/{postId}.json");
        }

        public IndividualSubscriptionResponse CreateSubscription(long postId, Subscription subscription)
        {
           return GenericPost<IndividualSubscriptionResponse>($"community/posts/{postId}/subscriptions.json", new { subscription });
        }

        public IndividualSubscriptionResponse GetSubscription(long postId, long subscriptionId, SubscriptionSideLoadOptions subscriptionSideLoadOptions = SubscriptionSideLoadOptions.None)
        {
            return GenericGet<IndividualSubscriptionResponse>($"community/posts/{postId}/subscriptions/{subscriptionId}.json".SubscriptionSideloadUri(subscriptionSideLoadOptions));
        }

        public GroupSubscriptionsResponse GetSubscriptions(long postId, SubscriptionSideLoadOptions subscriptionSideLoadOptions = SubscriptionSideLoadOptions.None, int? perPage = null, int? page = null)
        {
            return GenericPagedGet<GroupSubscriptionsResponse>($"community/posts/{postId}/subscriptions.json".SubscriptionSideloadUri(subscriptionSideLoadOptions), perPage, page);
        }

        public bool DeleteSubscription(long postId, long subscriptionId)
        {
            return GenericDelete($"community/posts/{postId}/subscriptions/{subscriptionId}.json");
        }
#endif
#if ASYNC
        public async Task<IndividualPostResponse> CreatePostAsync(Post post)
        {
            return await GenericPostAsync<IndividualPostResponse>("community/posts.json", new { post });
        }

        public async Task<IndividualPostResponse> GetPostAsync(long postId)
        {
            return await GenericGetAsync<IndividualPostResponse>($"community/posts/{postId}.json");
        }

        public Task<GroupPostResponse> GetPostsByTopicIdAsync(long topicId, int? perPage = null, int? page = null)
        {
            return GenericPagedGetAsync<GroupPostResponse>($"community/topics/{topicId}/posts.json", perPage, page);
        }

        public async Task<GroupPostResponse> GetPostsByUserIdAsync(long userId, int? perPage = null, int? page = null)
        {
            return await GenericPagedGetAsync<GroupPostResponse>($"community/users/{userId}/posts.json", perPage, page);
        }

        public async Task<GroupPostResponse> GetPostsAsync(int? perPage = null, int? page = null)
        {
            return await GenericPagedGetAsync<GroupPostResponse>("community/posts.json", perPage, page);
        }

        public async Task<IndividualPostResponse> UpdatePostAsync(Post post)
        {
            return await GenericPutAsync<IndividualPostResponse>($"community/posts/{post.Id.Value}.json", new { post });
        }

        public async Task<bool> DeletePostAsync(long postId)
        {
            return await GenericDeleteAsync($"community/posts/{postId}.json");
        }

        public Task<IndividualSubscriptionResponse> CreateSubscriptionAsync(long postId, Subscription subscription)
        {
            return GenericPostAsync<IndividualSubscriptionResponse>($"community/posts/{postId}/subscriptions.json", new { subscription });
        }

        public Task<IndividualSubscriptionResponse> GetSubscriptionAsync(long postId, long subscriptionId, SubscriptionSideLoadOptions subscriptionSideLoadOptions = SubscriptionSideLoadOptions.None)
        {
            return GenericGetAsync<IndividualSubscriptionResponse>($"community/posts/{postId}/subscriptions/{subscriptionId}.json".SubscriptionSideloadUri(subscriptionSideLoadOptions));
        }

        public Task<GroupSubscriptionsResponse> GetSubscriptionsAsync(long postId, SubscriptionSideLoadOptions subscriptionSideLoadOptions = SubscriptionSideLoadOptions.None, int? perPage = null, int? page = null)
        {
            return GenericPagedGetAsync<GroupSubscriptionsResponse>($"community/posts/{postId}/subscriptions.json".SubscriptionSideloadUri(subscriptionSideLoadOptions), perPage, page);
        }

        public Task<bool> DeleteSubscriptionAsync(long postId, long subscriptionId)
        {
            return GenericDeleteAsync($"community/posts/{postId}/subscriptions/{subscriptionId}.json");
        }
#endif
    }
}
