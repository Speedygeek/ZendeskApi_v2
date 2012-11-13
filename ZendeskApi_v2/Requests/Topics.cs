using System.Collections.Generic;
using ZenDeskApi_v2.Extensions;
using ZenDeskApi_v2.Models.Topics;

namespace ZenDeskApi_v2.Requests
{
    public class Topics : Core
    {
        public Topics(string yourZenDeskUrl, string user, string password)
            : base(yourZenDeskUrl, user, password)
        {
        }

        public GroupTopicResponse GetTopics()
        {
            return GenericGet<GroupTopicResponse>("topics.json");
        }

        public IndividualTopicResponse GetTopicById(long topicId)
        {
            return GenericGet<IndividualTopicResponse>(string.Format("topics/{0}.json", topicId));
        }

        public GroupTopicResponse GetMultipleTopicsById(List<long> topicIds)
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
    }
}