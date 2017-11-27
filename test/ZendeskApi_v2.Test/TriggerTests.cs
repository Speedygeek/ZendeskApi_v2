using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ZendeskApi_v2;
using ZendeskApi_v2.Models.Triggers;

namespace Tests
{
    [TestFixture]
    public class TriggerTests
    {
        private ZendeskApi api = new ZendeskApi(Settings.Site, Settings.AdminEmail, Settings.AdminPassword);

        [OneTimeSetUp]
        public void Init()
        {
            var triggers = api.Triggers.GetTriggers();
            if (triggers != null)
            {
                foreach (var trigger in triggers.Triggers.Where(o => o.Title.Contains("Test Trigger") || o.Title.Contains("Test Trigger Updated") || o.Title.Contains("Test Trigger1") || o.Title.Contains("Test Trigger2")))
                {
                    api.Triggers.DeleteTrigger(trigger.Id.Value);
                }
            }
        }

        [Test]
        public void CanGetTriggers()
        {
            var res = api.Triggers.GetTriggers();
            Assert.Greater(res.Count, 0);

            var ind = api.Triggers.GetTriggerById(res.Triggers[0].Id.Value);
            Assert.AreEqual(ind.Trigger.Id, res.Triggers[0].Id);
        }

        [Test]
        public void CanCreateUpdateAndDeleteTriggers()
        {
            var trigger = new Trigger()
            {
                Title = "Test Trigger",
                Active = true,
                Conditions = new Conditions() { All = new List<All>(), Any = new List<All>() },
                Actions = new List<Action>(),
                Position = 9999
            };

            trigger.Conditions.All.Add(new All() { Field = "status", Operator = "is", Value = "open" });
            trigger.Actions.Add(new Action() { Field = "group_id", Value = "20402842" });

            var res = api.Triggers.CreateTrigger(trigger);

            Assert.Greater(res.Trigger.Id, 0);

            res.Trigger.Title = "Test Trigger Updated";
            var update = api.Triggers.UpdateTrigger(res.Trigger);
            Assert.AreEqual(update.Trigger.Title, res.Trigger.Title);

            Assert.True(api.Triggers.DeleteTrigger(res.Trigger.Id.Value));
        }

        [Test]
        public void CanReorderTriggers()
        {
            var res = api.Triggers.GetActiveTriggers().Triggers;
            Assert.AreEqual(res.Count(), 0);

            var trigger = new Trigger()
            {
                Title = "Test Trigger1",
                Active = true,
                Conditions = new Conditions() { All = new List<All>() { new All() { Field = "status", Operator = "is", Value = "open" } }, Any = new List<All>() },
                Actions = new List<Action>() { new Action() { Field = "group_id", Value = "20402842" } },
                Position = 5000
            };

            var trigger2 = new Trigger()
            {
                Title = "Test Trigger2",
                Active = true,
                Conditions = new Conditions() { All = new List<All>() { new All() { Field = "status", Operator = "is", Value = "open" } }, Any = new List<All>() },
                Actions = new List<Action>() { new Action() { Field = "group_id", Value = "20402842" } },
                Position = 6000
            };

            var res2 = api.Triggers.CreateTrigger(trigger);
            var res3 = api.Triggers.CreateTrigger(trigger2);

            var ids = new List<long>() { res3.Trigger.Id.Value, res2.Trigger.Id.Value };

            Assert.True(api.Triggers.ReorderTriggers(ids));

            res = api.Triggers.GetActiveTriggers().Triggers;

            Assert.AreEqual(res[0].Id.Value, res3.Trigger.Id.Value);

            Assert.True(api.Triggers.DeleteTrigger(res2.Trigger.Id.Value));
            Assert.True(api.Triggers.DeleteTrigger(res3.Trigger.Id.Value));
        }
    }
}
