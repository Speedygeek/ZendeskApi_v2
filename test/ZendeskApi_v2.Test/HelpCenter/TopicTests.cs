using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ZendeskApi_v2;
using ZendeskApi_v2.Models.HelpCenter.Topics;

namespace Tests.HelpCenter
{
    [TestFixture]
    [Category("HelpCenter")]
    public class TopicTests
    {
        private readonly ZendeskApi api = new ZendeskApi(Settings.Site, Settings.AdminEmail, Settings.AdminPassword);
        private readonly long topicId = 360032658531;

        [OneTimeSetUp]
        public async Task CleanUp()
        {
            var topics = await api.HelpCenter.Topics.GetTopicsAsync();

            foreach (var topic in topics.Topics)
            {
                if (topic.Id != topicId & topic.Id != Settings.Topic_ID & !topic.Name.Contains("Do Not Delete"))
                {
                   await api.HelpCenter.Topics.DeleteTopicAsync(topic.Id.Value);
                }
            }
        }

        [Test]
        public void CanCreateUpdateAndDeleteTopic()
        {
            var topic = new Topic { Name = "This is a Test" };

            var res = api.HelpCenter.Topics.CreateTopic(topic);
            Assert.That(res?.Topic, Is.Not.Null);

            res.Topic.Description = "More Testing";
            var update = api.HelpCenter.Topics.UpdateTopic(res.Topic).Topic;
            Assert.That(update.Description, Is.EqualTo("More Testing"));

            var res2 = api.HelpCenter.Topics.GetTopic(res.Topic.Id.Value);
            Assert.That(res2.Topic, Is.Not.Null);

            Assert.That(api.HelpCenter.Topics.DeleteTopic(res.Topic.Id.Value), Is.True);
        }
    }
}
