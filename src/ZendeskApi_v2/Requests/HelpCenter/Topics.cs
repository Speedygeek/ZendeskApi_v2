#if ASYNC
using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi_v2.Models.HelpCenter.Subscriptions;
#endif
using ZendeskApi_v2.Models.HelpCenter.Topics;

namespace ZendeskApi_v2.Requests.HelpCenter
{
    public interface ITopics : ICore
    {
#if SYNC
        GroupTopicResponse GetTopics(int? perPage = null, int? page = null);
        IndividualTopicResponse GetTopic(long topicId);
        IndividualTopicResponse CreateTopic(Topic topic);
        IndividualTopicResponse UpdateTopic(Topic topic);
        bool DeleteTopic(long topicId);
        IndividualSubscriptionResponse CreateSubscription(long topicId, Subscription subscription);
        IndividualSubscriptionResponse UpdateSubscription(long topicId, Subscription subscription);
        IndividualSubscriptionResponse GetSubscription(long topicId, long SubscriptionId, SubscriptionSideLoadOptions sideLoadOptions = SubscriptionSideLoadOptions.None);
        IndividualSubscriptionResponse GetSubscriptions(long topicId, SubscriptionSideLoadOptions sideLoadOptions = SubscriptionSideLoadOptions.None);
        bool DeleteSubscription(long topicId, long SubscriptionId);
#endif
#if ASYNC
        Task<GroupTopicResponse> GetTopicsAsync(int? perPage = null, int? page = null);
        Task<IndividualTopicResponse> GetTopicAsync(long topicId);
        Task<IndividualTopicResponse> CreateTopicAsync(Topic topic);
        Task<IndividualTopicResponse> UpdateTopicAsync(Topic topic);
        Task<bool> DeleteTopicAsync(long topicId);
        Task<IndividualSubscriptionResponse> CreateSubscriptionAsync(long topicId, Subscription subscription);
        Task<IndividualSubscriptionResponse> UpdateSubscriptionAsync(long topicId, Subscription subscription);
        Task<IndividualSubscriptionResponse> GetSubscriptionAsync(long topicId, long SubscriptionId, SubscriptionSideLoadOptions sideLoadOptions = SubscriptionSideLoadOptions.None);
        Task<IndividualSubscriptionResponse> GetSubscriptionsAsync(long topicId, SubscriptionSideLoadOptions sideLoadOptions = SubscriptionSideLoadOptions.None);
        Task<bool> DeleteSubscriptionAsync(long topicId, long SubscriptionId);
#endif
    }

    public class Topics : Core, ITopics
    {
        public Topics(string yourZendeskUrl, string user, string password, string apiToken, string p_OAuthToken, Dictionary<string,string> customHeaders)
            : base(yourZendeskUrl, user, password, apiToken, p_OAuthToken, customHeaders)
        {
        }
#if SYNC
        public GroupTopicResponse GetTopics(int? perPage = null, int? page = null)
        {
            return GenericPagedGet<GroupTopicResponse>("community/topics.json", perPage, page);
        }

        public IndividualTopicResponse GetTopic(long topicId)
        {
            return GenericGet<IndividualTopicResponse>($"community/topics/{topicId}.json");
        }

        public IndividualTopicResponse CreateTopic(Topic topic)
        {
            return GenericPost<IndividualTopicResponse>("community/topics.json", new { topic });
        }

        public IndividualTopicResponse UpdateTopic(Topic topic)
        {
            return GenericPut<IndividualTopicResponse>($"community/topics/{topic.Id.Value}.json", new { topic });
        }

        public bool DeleteTopic(long topicId)
        {
            return GenericDelete($"community/topics/{topicId}.json");
        }

        public IndividualSubscriptionResponse CreateSubscription(long topicId, Subscription subscription)
        {
            return GenericPost<IndividualSubscriptionResponse>($"community/topics/{topicId}/subscriptions.json", new { subscription });
        }

        public IndividualSubscriptionResponse UpdateSubscription(long topicId, Subscription subscription)
        {
            return GenericPut<IndividualSubscriptionResponse>($"community/topics/{topicId}/subscriptions/{subscription.Id.Value}.json", new { subscription });
        }

        public IndividualSubscriptionResponse GetSubscription(long topicId, long SubscriptionId, SubscriptionSideLoadOptions sideLoadOptions = SubscriptionSideLoadOptions.None)
        {
            return GenericGet<IndividualSubscriptionResponse>($"community/topics/{topicId}/subscriptions/{SubscriptionId}.json".SubscriptionSideloadUri(sideLoadOptions));
        }

        public IndividualSubscriptionResponse GetSubscriptions(long topicId, SubscriptionSideLoadOptions sideLoadOptions = SubscriptionSideLoadOptions.None)
        {
            return GenericGet<IndividualSubscriptionResponse>($"community/topics/{topicId}/subscriptions.json".SubscriptionSideloadUri(sideLoadOptions));
        }

        public bool DeleteSubscription(long topicId, long SubscriptionId)
        {
            return GenericDelete($"community/topics/{topicId}/subscriptions/{SubscriptionId}.json");
        }
#endif
#if ASYNC
        public async Task<GroupTopicResponse> GetTopicsAsync(int? perPage = default, int? page = default)
        {
            return await GenericPagedGetAsync<GroupTopicResponse>("community/topics.json", perPage, page);
        }

        public async Task<IndividualTopicResponse> GetTopicAsync(long topicId)
        {
            return await GenericGetAsync<IndividualTopicResponse>($"community/topics/{topicId}.json");
        }

        public async Task<IndividualTopicResponse> CreateTopicAsync(Topic topic)
        {
            return await GenericPostAsync<IndividualTopicResponse>("community/topics.json", new { topic });
        }

        public async Task<IndividualTopicResponse> UpdateTopicAsync(Topic topic)
        {
            return await GenericPutAsync<IndividualTopicResponse>($"community/topics/{topic.Id.Value}.json", new { topic });
        }

        public async Task<bool> DeleteTopicAsync(long topicId)
        {
            return await GenericDeleteAsync($"community/topics/{topicId}.json)");
        }

        public Task<IndividualSubscriptionResponse> CreateSubscriptionAsync(long topicId, Subscription subscription)
        {
            return GenericPostAsync<IndividualSubscriptionResponse>($"community/topics/{topicId}/subscriptions.json", new { subscription });
        }

        public Task<IndividualSubscriptionResponse> UpdateSubscriptionAsync(long topicId, Subscription subscription)
        {
            return GenericPutAsync<IndividualSubscriptionResponse>($"community/topics/{topicId}/subscriptions/{subscription.Id.Value}.json", new { subscription });
        }

        public Task<IndividualSubscriptionResponse> GetSubscriptionAsync(long topicId, long SubscriptionId, SubscriptionSideLoadOptions sideLoadOptions = SubscriptionSideLoadOptions.None)
        {
            return GenericGetAsync<IndividualSubscriptionResponse>($"community/topics/{topicId}/subscriptions/{SubscriptionId}.json".SubscriptionSideloadUri(sideLoadOptions));
        }

        public Task<IndividualSubscriptionResponse> GetSubscriptionsAsync(long topicId, SubscriptionSideLoadOptions sideLoadOptions = SubscriptionSideLoadOptions.None)
        {
            return GenericGetAsync<IndividualSubscriptionResponse>($"community/topics/{topicId}/subscriptions.json".SubscriptionSideloadUri(sideLoadOptions));
        }

        public Task<bool> DeleteSubscriptionAsync(long topicId, long SubscriptionId)
        {
            return GenericDeleteAsync($"community/topics/{topicId}/subscriptions/{SubscriptionId}.json");
        }
#endif
    }
}
