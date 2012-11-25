using NUnit.Framework;
using ZendeskApi_v2;

namespace Tests
{
    [TestFixture]
    public class TriggerTests
    {
        private ZendeskApi api = new ZendeskApi(Settings.Site, Settings.Email, Settings.Password);

        [Test]
        public void CanGetTriggers()
        {
            var res = api.Triggers.GetTriggers();
            Assert.Greater(res.Count, 0);

            var ind = api.Triggers.GetTriggerById(res.Triggers[0].Id);
            Assert.AreEqual(ind.Trigger.Id, res.Triggers[0].Id);            
        }
    }
}