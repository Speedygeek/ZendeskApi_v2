using Newtonsoft.Json;
using NUnit.Framework;
using ZendeskApi_v2.Models.Voice;

namespace ZendeskApi_v2.Tests.Models.Voice;

[TestFixture]
public class FromTests
{
    private const string AllFieldsJson = "{\"current_queue_activity\":{\"agents_online\":3,\"average_wait_time\":142,\"calls_waiting\":13,\"longest_wait_time\":387,\"callbacks_waiting\":7,\"embeddable_callbacks_waiting\":0}}";

    [Test]
    public void DeserializeAllFieldsTest()
    {
        var from = JsonConvert.DeserializeObject<CurrentQueueActivityResponse>(AllFieldsJson);

        Assert.That(from, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(from.CurrentQueueActivity.AgentsOnline, Is.EqualTo(3));
            Assert.That(from.CurrentQueueActivity.CallsWaiting, Is.EqualTo(13));
            Assert.That(from.CurrentQueueActivity.CallbacksWaiting, Is.EqualTo(7));
            Assert.That(from.CurrentQueueActivity.AverageWaitTime, Is.EqualTo(142));
            Assert.That(from.CurrentQueueActivity.LongestWaitTime, Is.EqualTo(387));
        });
    }
}
