using System.Collections.Generic;
#if ASYNC
using System.Threading.Tasks;
#endif
using ZendeskApi_v2.Extensions;
using ZendeskApi_v2.Models.Topics;

namespace ZendeskApi_v2.Requests
{
	public interface ITopics : ICore
	{
#if SYNC
		GroupTopicResponse GetTopics();
		IndividualTopicResponse GetTopicById(long topicId);
		GroupTopicResponse GetMultipleTopicsById(IEnumerable<long> topicIds);
		GroupTopicResponse GetTopicsByForum(long forumId);
		GroupTopicResponse GetTopicsByUser(long userId);
		IndividualTopicResponse CreateTopic(Topic topic);
		IndividualTopicResponse UpdateTopic(Topic topic);
		bool DeleteTopic(long topicId);
		GroupTopicCommentResponse GetTopicCommentsByTopicId(long topicId);
		GroupTopicCommentResponse GetTopicCommentsByUserId(long userId);
		IndividualTopicCommentResponse GetSpecificTopicCommentByTopic(long topicId, long commentId);
		IndividualTopicCommentResponse GetSpecificTopicCommentByUser(long userId, long commentId);
		IndividualTopicCommentResponse CreateTopicComment(long topicId, TopicComment topicComment);
		IndividualTopicCommentResponse UpdateTopicComment(TopicComment topicComment);
		bool DeleteTopicComment(long topicId, long commentId);
		GroupTopicSubscriptionResponse GetTopicSubscriptionsByTopic(long topicId);
		GroupTopicSubscriptionResponse GetAllTopicSubscriptions();
		IndividualTopicSubscriptionResponse GetTopicSubscriptionById(long topicSubscriptionId);
		IndividualTopicSubscriptionResponse CreateTopicSubscription(long userId, long topicId);
		bool DeleteTopicSubscription(long topicSubscriptionId);
		GroupTopicVoteResponse GetTopicVotes(long topicId);
		GroupTopicVoteResponse GetTopicVotesByUser(long userId);

		/// <summary>
		/// Checks to see if the current user has cast a vote in this topic. Returns null if not
		/// </summary>
		/// <param name="topicId"></param>
		/// <returns></returns>
		IndividualTopicVoteResponse CheckForVote(long topicId);

		IndividualTopicVoteResponse CreateVote(long topicId);
		bool DeleteVote(long topicId);
#endif
		
#if ASYNC
		Task<GroupTopicResponse> GetTopicsAsync();
		Task<IndividualTopicResponse> GetTopicByIdAsync(long topicId);
		Task<GroupTopicResponse> GetMultipleTopicsByIdAsync(IEnumerable<long> topicIds);
		Task<GroupTopicResponse> GetTopicsByForumAsync(long forumId);
		Task<GroupTopicResponse> GetTopicsByUserAsync(long userId);
		Task<IndividualTopicResponse> CreateTopicAsync(Topic topic);
		Task<IndividualTopicResponse> UpdateTopicAsync(Topic topic);
		Task<bool> DeleteTopicAsync(long topicId);
		Task<GroupTopicCommentResponse> GetTopicCommentsByTopicIdAsync(long topicId);
		Task<GroupTopicCommentResponse> GetTopicCommentsByUserIdAsync(long userId);
		Task<IndividualTopicCommentResponse> GetSpecificTopicCommentByTopicAsync(long topicId, long commentId);
		Task<IndividualTopicCommentResponse> GetSpecificTopicCommentByUserAsync(long userId, long commentId);
		Task<IndividualTopicCommentResponse> CreateTopicCommentAsync(long topicId, TopicComment topicComment);
		Task<IndividualTopicCommentResponse> UpdateTopicCommentAsync(TopicComment topicComment);
		Task<bool> DeleteTopicCommentAsync(long topicId, long commentId);
		Task<GroupTopicSubscriptionResponse> GetTopicSubscriptionsByTopicAsync(long topicId);
		Task<GroupTopicSubscriptionResponse> GetAllTopicSubscriptionsAsync();
		Task<IndividualTopicSubscriptionResponse> GetTopicSubscriptionByIdAsync(long topicSubscriptionId);
		Task<IndividualTopicSubscriptionResponse> CreateTopicSubscriptionAsync(long userId, long topicId);
		Task<bool> DeleteTopicSubscriptionAsync(long topicSubscriptionId);
		Task<GroupTopicVoteResponse> GetTopicVotesAsync(long topicId);
		Task<GroupTopicVoteResponse> GetTopicVotesByUserAsync(long userId);

		/// <summary>
		/// Checks to see if the current user has cast a vote in this topic. Returns null if not
		/// </summary>
		/// <param name="topicId"></param>
		/// <returns></returns>
		Task<IndividualTopicVoteResponse> CheckForVoteAsync(long topicId);

		Task<IndividualTopicVoteResponse> CreateVoteAsync(long topicId);
		Task<bool> DeleteVoteAsync(long topicId);
#endif
	}

	public class Topics : Core, ITopics
	{
        public Topics(string yourZendeskUrl, string user, string password, string apiToken, string p_OAuthToken)
            : base(yourZendeskUrl, user, password, apiToken, p_OAuthToken)
        {
        }

#if SYNC
        public GroupTopicResponse GetTopics()
        {
            return GenericGet<GroupTopicResponse>("topics.json");
        }

        public IndividualTopicResponse GetTopicById(long topicId)
        {
            return GenericGet<IndividualTopicResponse>($"topics/{topicId}.json");
        }

        public GroupTopicResponse GetMultipleTopicsById(IEnumerable<long> topicIds)
        {
            return GenericPost<GroupTopicResponse>($"topics/show_many?ids={topicIds.ToCsv()}.json");
        }

        public GroupTopicResponse GetTopicsByForum(long forumId)
        {
            return GenericGet<GroupTopicResponse>($"forums/{forumId}/topics.json");
        }

        public GroupTopicResponse GetTopicsByUser(long userId)
        {
            return GenericGet<GroupTopicResponse>($"users/{userId}/topics.json");
        }

        public IndividualTopicResponse CreateTopic(Topic topic)
        {
            var body = new {topic};
            return GenericPost<IndividualTopicResponse>(string.Format("topics.json"), body);
        }

        public IndividualTopicResponse UpdateTopic(Topic topic)
        {
            var body = new { topic };
            return GenericPut<IndividualTopicResponse>($"topics/{topic.Id}.json", body);
        }

        public bool DeleteTopic(long topicId)
        {
            return GenericDelete($"topics/{topicId}.json");
        }

        public GroupTopicCommentResponse GetTopicCommentsByTopicId(long topicId)
        {
            return GenericGet<GroupTopicCommentResponse>($"topics/{topicId}/comments.json");
        }

        public GroupTopicCommentResponse GetTopicCommentsByUserId(long userId)
        {
            return GenericGet<GroupTopicCommentResponse>($"users/{userId}/topic_comments.json");
        }

        public IndividualTopicCommentResponse GetSpecificTopicCommentByTopic(long topicId, long commentId)
        {
            return GenericGet<IndividualTopicCommentResponse>($"topics/{topicId}/comments/{commentId}.json");
        }

        public IndividualTopicCommentResponse GetSpecificTopicCommentByUser(long userId, long commentId)
        {
            return GenericGet<IndividualTopicCommentResponse>($"users/{userId}/topic_comments/{commentId}.json");
        }

        public IndividualTopicCommentResponse CreateTopicComment(long topicId, TopicComment topicComment)
        {
            var body = new { topic_comment = topicComment };
            return GenericPost<IndividualTopicCommentResponse>($"topics/{topicId}/comments.json", body);
        }

        public IndividualTopicCommentResponse UpdateTopicComment(TopicComment topicComment)
        {
            var body = new { topic_comment = topicComment};
            return GenericPut<IndividualTopicCommentResponse>($"topics/{topicComment.TopicId}/comments/{topicComment.Id}.json", body);
        }

        public bool DeleteTopicComment(long topicId, long commentId)
        {
            return GenericDelete($"topics/{topicId}/comments/{commentId}.json");
        }

        public GroupTopicSubscriptionResponse GetTopicSubscriptionsByTopic(long topicId)
        {
            return GenericGet<GroupTopicSubscriptionResponse>($"topics/{topicId}/subscriptions.json");
        }

        public GroupTopicSubscriptionResponse GetAllTopicSubscriptions()
        {
            return GenericGet<GroupTopicSubscriptionResponse>(string.Format("topic_subscriptions.json"));
        }

        public IndividualTopicSubscriptionResponse GetTopicSubscriptionById(long topicSubscriptionId)
        {
            return GenericGet<IndividualTopicSubscriptionResponse>($"topic_subscriptions/{topicSubscriptionId}.json");
        }

        public IndividualTopicSubscriptionResponse CreateTopicSubscription(long userId, long topicId)
        {
            var body = new { user_id = userId, topic_id = topicId };
            return GenericPost<IndividualTopicSubscriptionResponse>(string.Format("topic_subscriptions.json"), body);
        }

        public bool DeleteTopicSubscription(long topicSubscriptionId)
        {
            return GenericDelete($"topic_subscriptions/{topicSubscriptionId}.json");
        }

        public GroupTopicVoteResponse GetTopicVotes(long topicId)
        {
            return GenericGet<GroupTopicVoteResponse>($"topics/{topicId}/votes.json");
        }

        public GroupTopicVoteResponse GetTopicVotesByUser(long userId)
        {
            return GenericGet<GroupTopicVoteResponse>($"users/{userId}/topic_votes.json");
        }

        /// <summary>
        /// Checks to see if the current user has cast a vote in this topic. Returns null if not
        /// </summary>
        /// <param name="topicId"></param>
        /// <returns></returns>
        public IndividualTopicVoteResponse CheckForVote(long topicId)
        {
            return GenericGet<IndividualTopicVoteResponse>($"topics/{topicId}/vote.json");
        }

        public IndividualTopicVoteResponse CreateVote(long topicId)
        {
            return GenericPost<IndividualTopicVoteResponse>($"topics/{topicId}/vote.json");
        }

        public bool DeleteVote(long topicId)
        {
            return GenericDelete($"topics/{topicId}/vote.json");
        }
#endif

#if ASYNC
        public async Task<GroupTopicResponse> GetTopicsAsync()
        {
            return await GenericGetAsync<GroupTopicResponse>("topics.json");
        }

        public async Task<IndividualTopicResponse> GetTopicByIdAsync(long topicId)
        {
            return await GenericGetAsync<IndividualTopicResponse>($"topics/{topicId}.json");
        }

        public async Task<GroupTopicResponse> GetMultipleTopicsByIdAsync(IEnumerable<long> topicIds)
        {
            return await GenericPostAsync<GroupTopicResponse>($"topics/show_many?ids={topicIds.ToCsv()}.json");
        }

        public async Task<GroupTopicResponse> GetTopicsByForumAsync(long forumId)
        {
            return await GenericGetAsync<GroupTopicResponse>($"forums/{forumId}/topics.json");
        }

        public async Task<GroupTopicResponse> GetTopicsByUserAsync(long userId)
        {
            return await GenericGetAsync<GroupTopicResponse>($"users/{userId}/topics.json");
        }

        public async Task<IndividualTopicResponse> CreateTopicAsync(Topic topic)
        {
            var body = new {topic};
            return await GenericPostAsync<IndividualTopicResponse>(string.Format("topics.json"), body);
        }

        public async Task<IndividualTopicResponse> UpdateTopicAsync(Topic topic)
        {
            var body = new { topic };
            return await GenericPutAsync<IndividualTopicResponse>($"topics/{topic.Id}.json", body);
        }

        public async Task<bool> DeleteTopicAsync(long topicId)
        {
            return await GenericDeleteAsync($"topics/{topicId}.json");
        }

        public async Task<GroupTopicCommentResponse> GetTopicCommentsByTopicIdAsync(long topicId)
        {
            return await GenericGetAsync<GroupTopicCommentResponse>($"topics/{topicId}/comments.json");
        }

        public async Task<GroupTopicCommentResponse> GetTopicCommentsByUserIdAsync(long userId)
        {
            return await GenericGetAsync<GroupTopicCommentResponse>($"users/{userId}/topic_comments.json");
        }

        public async Task<IndividualTopicCommentResponse> GetSpecificTopicCommentByTopicAsync(long topicId, long commentId)
        {
            return await GenericGetAsync<IndividualTopicCommentResponse>($"topics/{topicId}/comments/{commentId}.json");
        }

        public async Task<IndividualTopicCommentResponse> GetSpecificTopicCommentByUserAsync(long userId, long commentId)
        {
            return await GenericGetAsync<IndividualTopicCommentResponse>($"users/{userId}/topic_comments/{commentId}.json");
        }

        public async Task<IndividualTopicCommentResponse> CreateTopicCommentAsync(long topicId, TopicComment topicComment)
        {
            var body = new { topic_comment = topicComment };
            return await GenericPostAsync<IndividualTopicCommentResponse>($"topics/{topicId}/comments.json", body);
        }

        public async Task<IndividualTopicCommentResponse> UpdateTopicCommentAsync(TopicComment topicComment)
        {
            var body = new { topic_comment = topicComment};
            return await GenericPutAsync<IndividualTopicCommentResponse>($"topics/{topicComment.TopicId}/comments/{topicComment.Id}.json", body);
        }

        public async Task<bool> DeleteTopicCommentAsync(long topicId, long commentId)
        {
            return await GenericDeleteAsync($"topics/{topicId}/comments/{commentId}.json");
        }

        public async Task<GroupTopicSubscriptionResponse> GetTopicSubscriptionsByTopicAsync(long topicId)
        {
            return await GenericGetAsync<GroupTopicSubscriptionResponse>($"topics/{topicId}/subscriptions.json");
        }

        public async Task<GroupTopicSubscriptionResponse> GetAllTopicSubscriptionsAsync()
        {
            return await GenericGetAsync<GroupTopicSubscriptionResponse>(string.Format("topic_subscriptions.json"));
        }

        public async Task<IndividualTopicSubscriptionResponse> GetTopicSubscriptionByIdAsync(long topicSubscriptionId)
        {
            return await GenericGetAsync<IndividualTopicSubscriptionResponse>($"topic_subscriptions/{topicSubscriptionId}.json");
        }

        public async Task<IndividualTopicSubscriptionResponse> CreateTopicSubscriptionAsync(long userId, long topicId)
        {
            var body = new { user_id = userId, topic_id = topicId };
            return await GenericPostAsync<IndividualTopicSubscriptionResponse>(string.Format("topic_subscriptions.json"), body);
        }

        public async Task<bool> DeleteTopicSubscriptionAsync(long topicSubscriptionId)
        {
            return await GenericDeleteAsync($"topic_subscriptions/{topicSubscriptionId}.json");
        }

        public async Task<GroupTopicVoteResponse> GetTopicVotesAsync(long topicId)
        {
            return await GenericGetAsync<GroupTopicVoteResponse>($"topics/{topicId}/votes.json");
        }

        public async Task<GroupTopicVoteResponse> GetTopicVotesByUserAsync(long userId)
        {
            return await GenericGetAsync<GroupTopicVoteResponse>($"users/{userId}/topic_votes.json");
        }

        /// <summary>
        /// Checks to see if the current user has cast a vote in this topic. Returns null if not
        /// </summary>
        /// <param name="topicId"></param>
        /// <returns></returns>
        public async Task<IndividualTopicVoteResponse> CheckForVoteAsync(long topicId)
        {
            return await GenericGetAsync<IndividualTopicVoteResponse>($"topics/{topicId}/vote.json");
        }

        public async Task<IndividualTopicVoteResponse> CreateVoteAsync(long topicId)
        {
            return await GenericPostAsync<IndividualTopicVoteResponse>($"topics/{topicId}/vote.json");
        }

        public async Task<bool> DeleteVoteAsync(long topicId)
        {
            return await GenericDeleteAsync($"topics/{topicId}/vote.json");
        }
#endif
    }
}