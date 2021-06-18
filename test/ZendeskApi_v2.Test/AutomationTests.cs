﻿using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ZendeskApi_v2;
using ZendeskApi_v2.Models.Automations;

namespace Tests
{
    [TestFixture]
    public class AutomationTests
    {
        private readonly ZendeskApi api = new ZendeskApi(Settings.Site, Settings.AdminEmail, Settings.AdminPassword);

        [OneTimeSetUp]
        public void Init()
        {
            var automations = api.Automations.GetAutomations();
            if (automations != null)
            {
                foreach (var automation in automations.Automations.Where(o => o.Title.Contains("Test Automation") || o.Title.Contains("Test Automation Updated") || o.Title.Contains("Test Automation1") || o.Title.Contains("Test Automation2")))
                {
                    api.Automations.DeleteAutomation(automation.Id.Value);
                }
            }
        }

        [Test]
        public void CanGetAutomations()
        {
            var res = api.Automations.GetAutomations();
            Assert.That(res.Count, Is.GreaterThan(0));

            var ind = api.Automations.GetAutomationById(res.Automations[0].Id.Value);
            Assert.That(res.Automations[0].Id, Is.EqualTo(ind.Automation.Id));
        }

        [Test]
        public void CanCreateUpdateAndDeleteAutomations()
        {
            var automation = new Automation()
            {
                Title = "Test Automation",
                Active = true,
                Conditions = new Conditions() { All = new List<All>(), Any = new List<All>() },
                Actions = new List<Action>(),
                Position = 9999
            };

            automation.Conditions.All.Add(new All() { Field = "status", Operator = "is", Value = "open" });
            automation.Conditions.All.Add(new All() { Field = "description_includes_word", Operator = "is", Value = "match_no_tickets" });
            automation.Conditions.All.Add(new All() { Field = "NEW", Operator = "is", Value = "24" });
            automation.Actions.Add(new Action() { Field = "group_id", Value = "20402842" });

            var res = api.Automations.CreateAutomation(automation);

            Assert.That(res.Automation.Id, Is.GreaterThan(0));

            res.Automation.Title = "Test Automation Updated";
            var update = api.Automations.UpdateAutomation(res.Automation);
            Assert.That(res.Automation.Title, Is.EqualTo(update.Automation.Title));

            Assert.That(api.Automations.DeleteAutomation(res.Automation.Id.Value), Is.True);
        }

        [Test]
        public void CanSearchAutomations()
        {
            var res = api.Automations.SearchAutomations("Close").Automations;
            Assert.That(res.Count(), Is.EqualTo(1));
            Assert.That(res[0].Title, Is.EqualTo("Close ticket 4 days after status is set to solved"));
        }
    }
}
