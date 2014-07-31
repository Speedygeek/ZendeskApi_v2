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
        public Topics(string yourZendeskUrl, string user, string password, string apiToken)
            : base(yourZendeskUrl, user, password, apiToken)
        {
        }

#if SYNC
        public GroupTopicResponse GetTopics()
        {
            return GenericGet<GroupTopicResponse>("topics.json");
        }

        public IndividualTopicResponse GetTopicById(long topicId)
        {
            return GenericGet<IndividualTopicResponse>(string.Format("topics/{0}.json", topicId));
        }

        public GroupTopicResponse GetMultipleTopicsById(IEnumerable<long> topicIds)
        {
            return GenericPost<GroupTopicResponse>(string.Format("topics/show_many?ids={0}.json", topicIds.ToCsv()));
        }

        public GroupTopicResponse GetTopicsByForum(long forumId)
        {
            return GenericGet<GroupTopicResponse>(string.Format("forums/{0}/topics.json", forumId));
        }

        public GroupTopicResponse GetTopicsByUser(long userId)
        {
            return GenericGet<GroupTopicResponse>(string.Format("users/{0}/topics.json", userId));
        }

        public IndividualTopicResponse CreateTopic(Topic topic)
        {
            var body = new {topic};
            return GenericPost<IndividualTopicResponse>(string.Format("topics.json"), body);
        }

        public IndividualTopicResponse UpdateTopic(Topic topic)
        {
            var body = new { topic };
            return GenericPut<IndividualTopicResponse>(string.Format("topics/{0}.json", topic.Id), body);
        }

        public bool DeleteTopic(long topicId)
        {
            return GenericDelete(string.Format("topics/{0}.json", topicId));
        }

        public GroupTopicCommentResponse GetTopicCommentsByTopicId(long topicId)
        {
            return GenericGet<GroupTopicCommentResponse>(string.Format("topics/{0}/comments.json", topicId));
        }

        public GroupTopicCommentResponse GetTopicCommentsByUserId(long userId)
        {
            return GenericGet<GroupTopicCommentResponse>(string.Format("users/{0}/topic_comments.json", userId));
        }

        public IndividualTopicCommentResponse GetSpecificTopicCommentByTopic(long topicId, long commentId)
        {
            return GenericGet<IndividualTopicCommentResponse>(string.Format("topics/{0}/comments/{1}.json", topicId, commentId));
        }

        public IndividualTopicCommentResponse GetSpecificTopicCommentByUser(long userId, long commentId)
        {
            return GenericGet<IndividualTopicCommentResponse>(string.Format("users/{0}/topic_comments/{1}.json", userId, commentId));
        }

        public IndividualTopicCommentResponse CreateTopicComment(long topicId, TopicComment topicComment)
        {
            var body = new { topic_comment = topicComment };
            return GenericPost<IndividualTopicCommentResponse>(string.Format("topics/{0}/comments.json", topicId), body);
        }

        public IndividualTopicCommentResponse UpdateTopicComment(TopicComment topicComment)
        {
            var body = new { topic_comment = topicComment};
            return GenericPut<IndividualTopicCommentResponse>(string.Format("topics/{0}/comments/{1}.json", topicComment.TopicId, topicComment.Id), body);
        }

        public bool DeleteTopicComment(long topicId, long commentId)
        {
            return GenericDelete(string.Format("topics/{0}/comments/{1}.json", topicId, commentId));
        }

        public GroupTopicSubscriptionResponse GetTopicSubscriptionsByTopic(long topicId)
        {
            return GenericGet<GroupTopicSubscriptionResponse>(string.Format("topics/{0}/subscriptions.json", topicId));
        }

        public GroupTopicSubscriptionResponse GetAllTopicSubscriptions()
        {
            return GenericGet<GroupTopicSubscriptionResponse>(string.Format("topic_subscriptions.json"));
        }

        public IndividualTopicSubscriptionResponse GetTopicSubscriptionById(long topicSubscriptionId)
        {
            return GenericGet<IndividualTopicSubscriptionResponse>(string.Format("topic_subscriptions/{0}.json", topicSubscriptionId));
        }

        public IndividualTopicSubscriptionResponse CreateTopicSubscription(long userId, long topicId)
        {
            var body = new { user_id = userId, topic_id = topicId };
            return GenericPost<IndividualTopicSubscriptionResponse>(string.Format("topic_subscriptions.json"), body);
        }

        public bool DeleteTopicSubscription(long topicSubscriptionId)
        {
            return GenericDelete(string.Format("topic_subscriptions/{0}.json", topicSubscriptionId));
        }

        public GroupTopicVoteResponse GetTopicVotes(long topicId)
        {
            return GenericGet<GroupTopicVoteResponse>(string.Format("topics/{0}/votes.json", topicId));
        }

        public GroupTopicVoteResponse GetTopicVotesByUser(long userId)
        {
            return GenericGet<GroupTopicVoteResponse>(string.Format("users/{0}/topic_votes.json", userId));
        }

        /// <summary>
        /// Checks to see if the current user has cast a vote in this topic. Returns null if not
        /// </summary>
        /// <param name="topicId"></param>
        /// <returns></returns>
        public IndividualTopicVoteResponse CheckForVote(long topicId)
        {
            return GenericGet<IndividualTopicVoteResponse>(string.Format("topics/{0}/vote.json", topicId));
        }

        public IndividualTopicVoteResponse CreateVote(long topicId)
        {
            return GenericPost<IndividualTopicVoteResponse>(string.Format("topics/{0}/vote.json", topicId));
        }

        public bool DeleteVote(long topicId)
        {
            return GenericDelete(string.Format("topics/{0}/vote.json", topicId));
        }
#endif

#if ASYNC
        public async Task<GroupTopicResponse> GetTopicsAsync()
        {
            return await GenericGetAsync<GroupTopicResponse>("topics.json");
        }

        public async Task<IndividualTopicResponse> GetTopicByIdAsync(long topicId)
        {
            return await GenericGetAsync<IndividualTopicResponse>(string.Format("topics/{0}.json", topicId));
        }

        public async Task<GroupTopicResponse> GetMultipleTopicsByIdAsync(IEnumerable<long> topicIds)
        {
            return await GenericPostAsync<GroupTopicResponse>(string.Format("topics/show_many?ids={0}.json", topicIds.ToCsv()));
        }

        public async Task<GroupTopicResponse> GetTopicsByForumAsync(long forumId)
        {
            return await GenericGetAsync<GroupTopicResponse>(string.Format("forums/{0}/topics.json", forumId));
        }

        public async Task<GroupTopicResponse> GetTopicsByUserAsync(long userId)
        {
            return await GenericGetAsync<GroupTopicResponse>(string.Format("users/{0}/topics.json", userId));
        }

        public async Task<IndividualTopicResponse> CreateTopicAsync(Topic topic)
        {
            var body = new {topic};
            return await GenericPostAsync<IndividualTopicResponse>(string.Format("topics.json"), body);
        }

        public async Task<IndividualTopicResponse> UpdateTopicAsync(Topic topic)
        {
            var body = new { topic };
            return await GenericPutAsync<IndividualTopicResponse>(string.Format("topics/{0}.json", topic.Id), body);
        }

        public async Task<bool> DeleteTopicAsync(long topicId)
        {
            return await GenericDeleteAsync(string.Format("topics/{0}.json", topicId));
        }

        public async Task<GroupTopicCommentResponse> GetTopicCommentsByTopicIdAsync(long topicId)
        {
            return await GenericGetAsync<GroupTopicCommentResponse>(string.Format("topics/{0}/comments.json", topicId));
        }

        public async Task<GroupTopicCommentResponse> GetTopicCommentsByUserIdAsync(long userId)
        {
            return await GenericGetAsync<GroupTopicCommentResponse>(string.Format("users/{0}/topic_comments.json", userId));
        }

        public async Task<IndividualTopicCommentResponse> GetSpecificTopicCommentByTopicAsync(long topicId, long commentId)
        {
            return await GenericGetAsync<IndividualTopicCommentResponse>(string.Format("topics/{0}/comments/{1}.json", topicId, commentId));
        }

        public async Task<IndividualTopicCommentResponse> GetSpecificTopicCommentByUserAsync(long userId, long commentId)
        {
            return await GenericGetAsync<IndividualTopicCommentResponse>(string.Format("users/{0}/topic_comments/{1}.json", userId, commentId));
        }

        public async Task<IndividualTopicCommentResponse> CreateTopicCommentAsync(long topicId, TopicComment topicComment)
        {
            var body = new { topic_comment = topicComment };
            return await GenericPostAsync<IndividualTopicCommentResponse>(string.Format("topics/{0}/comments.json", topicId), body);
        }

        public async Task<IndividualTopicCommentResponse> UpdateTopicCommentAsync(TopicComment topicComment)
        {
            var body = new { topic_comment = topicComment};
            return await GenericPutAsync<IndividualTopicCommentResponse>(string.Format("topics/{0}/comments/{1}.json", topicComment.TopicId, topicComment.Id), body);
        }

        public async Task<bool> DeleteTopicCommentAsync(long topicId, long commentId)
        {
            return await GenericDeleteAsync(string.Format("topics/{0}/comments/{1}.json", topicId, commentId));
        }

        public async Task<GroupTopicSubscriptionResponse> GetTopicSubscriptionsByTopicAsync(long topicId)
        {
            return await GenericGetAsync<GroupTopicSubscriptionResponse>(string.Format("topics/{0}/subscriptions.json", topicId));
        }

        public async Task<GroupTopicSubscriptionResponse> GetAllTopicSubscriptionsAsync()
        {
            return await GenericGetAsync<GroupTopicSubscriptionResponse>(string.Format("topic_subscriptions.json"));
        }

        public async Task<IndividualTopicSubscriptionResponse> GetTopicSubscriptionByIdAsync(long topicSubscriptionId)
        {
            return await GenericGetAsync<IndividualTopicSubscriptionResponse>(string.Format("topic_subscriptions/{0}.json", topicSubscriptionId));
        }

        public async Task<IndividualTopicSubscriptionResponse> CreateTopicSubscriptionAsync(long userId, long topicId)
        {
            var body = new { user_id = userId, topic_id = topicId };
            return await GenericPostAsync<IndividualTopicSubscriptionResponse>(string.Format("topic_subscriptions.json"), body);
        }

        public async Task<bool> DeleteTopicSubscriptionAsync(long topicSubscriptionId)
        {
            return await GenericDeleteAsync(string.Format("topic_subscriptions/{0}.json", topicSubscriptionId));
        }

        public async Task<GroupTopicVoteResponse> GetTopicVotesAsync(long topicId)
        {
            return await GenericGetAsync<GroupTopicVoteResponse>(string.Format("topics/{0}/votes.json", topicId));
        }

        public async Task<GroupTopicVoteResponse> GetTopicVotesByUserAsync(long userId)
        {
            return await GenericGetAsync<GroupTopicVoteResponse>(string.Format("users/{0}/topic_votes.json", userId));
        }

        /// <summary>
        /// Checks to see if the current user has cast a vote in this topic. Returns null if not
        /// </summary>
        /// <param name="topicId"></param>
        /// <returns></returns>
        public async Task<IndividualTopicVoteResponse> CheckForVoteAsync(long topicId)
        {
            return await GenericGetAsync<IndividualTopicVoteResponse>(string.Format("topics/{0}/vote.json", topicId));
        }

        public async Task<IndividualTopicVoteResponse> CreateVoteAsync(long topicId)
        {
            return await GenericPostAsync<IndividualTopicVoteResponse>(string.Format("topics/{0}/vote.json", topicId));
        }

        public async Task<bool> DeleteVoteAsync(long topicId)
        {
            return await GenericDeleteAsync(string.Format("topics/{0}/vote.json", topicId));
        }
#endif
    }
}