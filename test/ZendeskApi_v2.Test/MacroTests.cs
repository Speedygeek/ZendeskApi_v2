using System.Collections.Generic;
using NUnit.Framework;
using ZendeskApi_v2;
using ZendeskApi_v2.Models.Constants;
using ZendeskApi_v2.Models.Macros;
using ZendeskApi_v2.Models.Tickets;

namespace Tests
{
    [TestFixture]
    public class MacroTests
    {
        private readonly ZendeskApi api = new ZendeskApi(Settings.Site, Settings.AdminEmail, Settings.AdminPassword);

        [Test]
        public void CanGetMacros()
        {
            var all = api.Macros.GetAllMacros();
            Assert.That(all.Count, Is.GreaterThan(0));

            api.Macros.GetMacroById(all.Macros[0].Id.Value);
            Assert.That(all.Count, Is.GreaterThan(0));

            var active = api.Macros.GetActiveMacros();
            Assert.That(active.Count, Is.GreaterThan(0));
        }

        [Test]
        public void CanCreateUpdateAndDeleteMacros()
        {
            var create = api.Macros.CreateMacro(new Macro
            {
                Title = "Roger Wilco",
                Actions = new List<Action> { new Action { Field = "status", Value = new List<string> { "open" } } }
            });

            Assert.That(create.Macro.Id, Is.GreaterThan(0));

            create.Macro.Title = "Roger wilco 2";
            var update = api.Macros.UpdateMacro(create.Macro);
            Assert.That(create.Macro.Id, Is.EqualTo(update.Macro.Id));

            //Test apply macro
            var ticket = api.Tickets.CreateTicket(new Ticket
            {
                Subject = "macro test ticket",
                Comment = new Comment { Body = "Testing macros" },
                Priority = TicketPriorities.Normal
            }).Ticket;

            var applyToTicket = api.Macros.ApplyMacroToTicket(ticket.Id.Value, create.Macro.Id.Value);
            Assert.That(ticket.Id, Is.EqualTo(applyToTicket.Result.Ticket.Id));
            Assert.That(api.Tickets.Delete(ticket.Id.Value), Is.True);
            Assert.That(api.Macros.DeleteMacro(create.Macro.Id.Value), Is.True);
        }


        [Test]
        public void CanGetMacroByID()
        {
            var macro = api.Macros.GetMacroById(45319945);

            Assert.That(macro, Is.Not.Null);
        }
    }
}