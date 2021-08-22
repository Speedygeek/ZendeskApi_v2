using NUnit.Framework;
using System.Collections.Generic;
using ZendeskApi_v2.Models.Constants;
using ZendeskApi_v2.Models.Macros;
using ZendeskApi_v2.Models.Tickets;
using ZendeskApi_v2.Tests.Base;

namespace ZendeskApi_v2.Tests
{
    [TestFixture]
    public class MacroTests : TestBase
    {
        [Test]
        public void CanGetMacros()
        {
            var all = Api.Macros.GetAllMacros();
            Assert.That(all.Count, Is.GreaterThan(0));

            Api.Macros.GetMacroById(all.Macros[0].Id.Value);
            Assert.That(all.Count, Is.GreaterThan(0));

            var active = Api.Macros.GetActiveMacros();
            Assert.That(active.Count, Is.GreaterThan(0));
        }

        [Test]
        public void CanCreateUpdateAndDeleteMacros()
        {
            var create = Api.Macros.CreateMacro(new Macro
            {
                Title = "Roger Wilco",
                Actions = new List<Action> { new Action { Field = "status", Value = new List<string> { "open" } } }
            });

            Assert.That(create.Macro.Id, Is.GreaterThan(0));

            create.Macro.Title = "Roger wilco 2";
            var update = Api.Macros.UpdateMacro(create.Macro);
            Assert.That(create.Macro.Id, Is.EqualTo(update.Macro.Id));

            //Test apply macro
            var ticket = Api.Tickets.CreateTicket(new Ticket
            {
                Subject = "macro test ticket",
                Comment = new Comment { Body = "Testing macros" },
                Priority = TicketPriorities.Normal
            }).Ticket;

            var applyToTicket = Api.Macros.ApplyMacroToTicket(ticket.Id.Value, create.Macro.Id.Value);
            Assert.That(ticket.Id, Is.EqualTo(applyToTicket.Result.Ticket.Id));
            Assert.That(Api.Tickets.Delete(ticket.Id.Value), Is.True);
            Assert.That(Api.Macros.DeleteMacro(create.Macro.Id.Value), Is.True);
        }

        [Test]
        public void CanGetMacroByID()
        {
            var macro = Api.Macros.GetMacroById(45319945);

            Assert.That(macro, Is.Not.Null);
        }
    }
}