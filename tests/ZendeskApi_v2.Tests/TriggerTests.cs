using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using ZendeskApi_v2.Models.Triggers;
using ZendeskApi_v2.Tests.Base;

namespace ZendeskApi_v2.Tests;

[TestFixture]
public class TriggerTests : TestBase
{
    [OneTimeSetUp]
    public void Setup()
    {
        var triggers = Api.Triggers.GetTriggers();
        if (triggers != null)
        {
            foreach (var trigger in triggers.Triggers.Where(o => o.Title.Contains("Test Trigger") || o.Title.Contains("Test Trigger Updated") || o.Title.Contains("Test Trigger1") || o.Title.Contains("Test Trigger2")))
            {
                Api.Triggers.DeleteTrigger(trigger.Id.Value);
            }
        }
    }

    [Test]
    public void CanGetTriggers()
    {
        var res = Api.Triggers.GetTriggers();
        Assert.That(res.Count, Is.GreaterThan(0));

        var ind = Api.Triggers.GetTriggerById(res.Triggers[0].Id.Value);
        Assert.That(res.Triggers[0].Id, Is.EqualTo(ind.Trigger.Id));
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

        var res = Api.Triggers.CreateTrigger(trigger);

        Assert.That(res.Trigger.Id, Is.GreaterThan(0));

        res.Trigger.Title = "Test Trigger Updated";
        var update = Api.Triggers.UpdateTrigger(res.Trigger);
        Assert.Multiple(() =>
        {
            Assert.That(res.Trigger.Title, Is.EqualTo(update.Trigger.Title));

            Assert.That(Api.Triggers.DeleteTrigger(res.Trigger.Id.Value), Is.True);
        });
    }

    [Test]
    public void CanReorderTriggers()
    {
        var res = Api.Triggers.GetActiveTriggers().Triggers;
        Assert.That(res, Is.Empty);

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

        var res2 = Api.Triggers.CreateTrigger(trigger);
        var res3 = Api.Triggers.CreateTrigger(trigger2);

        var ids = new List<long>() { res3.Trigger.Id.Value, res2.Trigger.Id.Value };

        Assert.That(Api.Triggers.ReorderTriggers(ids), Is.True);

        res = Api.Triggers.GetActiveTriggers().Triggers;
        Assert.Multiple(() =>
        {
            Assert.That(res3.Trigger.Id.Value, Is.EqualTo(res[0].Id.Value));

            Assert.That(Api.Triggers.DeleteTrigger(res2.Trigger.Id.Value), Is.True);
            Assert.That(Api.Triggers.DeleteTrigger(res3.Trigger.Id.Value), Is.True);
        });
    }
}
