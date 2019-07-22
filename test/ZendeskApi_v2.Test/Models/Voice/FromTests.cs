using Newtonsoft.Json;
using NUnit.Framework;
using ZendeskApi_v2.Models.Voice;

namespace Tests.Models.Voice
{
    [TestFixture]
    public class FromTests
    {
        private const string AllFieldsJson = "{\"current_queue_activity\":{\"agents_online\":3,\"average_wait_time\":142,\"calls_waiting\":13,\"longest_wait_time\":387,\"callbacks_waiting\":7,\"embeddable_callbacks_waiting\":0}}";

        [Test]
        public void DeserializeAllFieldsTest()
        {
            var from = JsonConvert.DeserializeObject<CurrentQueueActivityResponse>(AllFieldsJson);

            Assert.NotNull(from);
            Assert.AreEqual(3, from.CurrentQueueActivity.AgentsOnline);
            Assert.AreEqual(13, from.CurrentQueueActivity.CallsWaiting);
            Assert.AreEqual(7, from.CurrentQueueActivity.CallbacksWaiting);
            Assert.AreEqual(142, from.CurrentQueueActivity.AverageWaitTime);
            Assert.AreEqual(387, from.CurrentQueueActivity.LongestWaitTime);
        }
    }
}
