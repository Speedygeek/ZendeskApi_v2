using NUnit.Framework;
using System.Threading.Tasks;
using ZendeskApi_v2.Models.HelpCenter.Topics;
using ZendeskApi_v2.Tests.Base;

namespace ZendeskApi_v2.Tests.HelpCenter;

[TestFixture]
[Category("HelpCenter")]
public class TopicTests : TestBase
{
    private readonly long topicId = 360032658531;

    [OneTimeSetUp]
    public async Task CleanUp()
    {
        var topics = await Api.HelpCenter.Topics.GetTopicsAsync();

        foreach (var topic in topics.Topics)
        {
            if (topic.Id != topicId & topic.Id != Settings.Topic_ID & !topic.Name.Contains("Do Not Delete"))
            {
                await Api.HelpCenter.Topics.DeleteTopicAsync(topic.Id.Value);
            }
        }
    }

    [Test]
    public void CanCreateUpdateAndDeleteTopic()
    {
        var topic = new Topic { Name = "This is a Test" };

        var res = Api.HelpCenter.Topics.CreateTopic(topic);
        Assert.That(res?.Topic, Is.Not.Null);

        res.Topic.Description = "More Testing";
        var update = Api.HelpCenter.Topics.UpdateTopic(res.Topic).Topic;
        Assert.That(update.Description, Is.EqualTo("More Testing"));

        var res2 = Api.HelpCenter.Topics.GetTopic(res.Topic.Id.Value);
        Assert.Multiple(() =>
        {
            Assert.That(res2.Topic, Is.Not.Null);

            Assert.That(Api.HelpCenter.Topics.DeleteTopic(res.Topic.Id.Value), Is.True);
        });
    }
}
