#if ASYNC

using System.Threading.Tasks;

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

#endif
#if ASYNC

        Task<GroupTopicResponse> GetTopicsAsync(int? perPage = null, int? page = null);

        Task<IndividualTopicResponse> GetTopicAsync(long topicId);

        Task<IndividualTopicResponse> CreateTopicAsync(Topic topic);

        Task<IndividualTopicResponse> UpdateTopicAsync(Topic topic);

        Task<bool> DeleteTopicAsync(long topicId);

#endif
    }

    public class Topics : Core, ITopics
    {
        public Topics(string yourZendeskUrl, string user, string password, string apiToken, string p_OAuthToken)
            : base(yourZendeskUrl, user, password, apiToken, p_OAuthToken)
        {
        }

#if SYNC

        public GroupTopicResponse GetTopics(int? perPage = null, int? page = null)
        {
            return GenericPagedGet<GroupTopicResponse>("community/topics.json", perPage, page);
        }

        public IndividualTopicResponse GetTopic(long topicId)
        {
            return GenericGet<IndividualTopicResponse>(string.Format("community/topics/{0}.json", topicId));
        }

        public IndividualTopicResponse CreateTopic(Topic topic)
        {
            var body = new { topic };
            return GenericPost<IndividualTopicResponse>("community/topics.json", body);
        }

        public IndividualTopicResponse UpdateTopic(Topic topic)
        {
            var body = new { topic };
            return GenericPut<IndividualTopicResponse>(string.Format("community/topics/{0}.json", topic.Id.Value), body);
        }

        public bool DeleteTopic(long topicId)
        {
            return GenericDelete(string.Format("community/topics/{0}.json", topicId));
        }

#endif
#if ASYNC

        public async Task<GroupTopicResponse> GetTopicsAsync(int? perPage = default(int?), int? page = default(int?))
        {
            return await GenericPagedGetAsync<GroupTopicResponse>("community/topics.json", perPage, page);
        }

        public async Task<IndividualTopicResponse> GetTopicAsync(long topicId)
        {
            return await GenericGetAsync<IndividualTopicResponse>(string.Format("community/topics/{0}.json", topicId));
        }

        public async Task<IndividualTopicResponse> CreateTopicAsync(Topic topic)
        {
            var body = new { topic };
            return await GenericPostAsync<IndividualTopicResponse>("community/topics.json", body);
        }

        public async Task<IndividualTopicResponse> UpdateTopicAsync(Topic topic)
        {
            var body = new { topic };
            return await GenericPutAsync<IndividualTopicResponse>(string.Format("community/topics/{0}.json", topic.Id.Value), body);
        }

        public async Task<bool> DeleteTopicAsync(long topicId)
        {
            return await GenericDeleteAsync(string.Format("community/topics/{0}.json)", topicId));
        }

#endif
    }
}