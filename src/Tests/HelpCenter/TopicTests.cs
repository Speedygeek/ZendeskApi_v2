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
        private ZendeskApi api = new ZendeskApi(Settings.Site, Settings.AdminEmail, Settings.AdminPassword);

        [Test]
        public void CanGetTopic()
        {
            var res = api.HelpCenter.Topics.GetTopic(200298245);
            Assert.That(res?.Topic, Is.Not.Null);
        }

        [Test]
        public void CanGetTopics()
        {
            var res = api.HelpCenter.Topics.GetTopics();
            Assert.That(res.Topics.Count, Is.GreaterThan(0));
        }

        [Test]
        public void CanCreateUpdateAndDeleteTopic()
        {
            Topic topic = new Topic { Name = "This is a Test" };

            var res = api.HelpCenter.Topics.CreateTopic(topic);
            Assert.That(res?.Topic, Is.Not.Null);

            res.Topic.Description = "More Testing";
            var update = api.HelpCenter.Topics.UpdateTopic(res.Topic).Topic;
            Assert.That(update.Description, Is.EqualTo("More Testing"));

            Assert.That(api.HelpCenter.Topics.DeleteTopic(res.Topic.Id.Value), Is.True);
        }
    }
}
