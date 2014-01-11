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
        private ZendeskApi api = new ZendeskApi(Settings.Site, Settings.Email, Settings.Password);

        [Test]
        public void CanGetMacros()
        {
            var all = api.Macros.GetAllMacros();
            Assert.Greater(all.Count, 0);

            var ind = api.Macros.GetMacroById(all.Macros[0].Id.Value);
            Assert.Greater(all.Count, 0);

            var active = api.Macros.GetActiveMacros();
            Assert.Greater(active.Count, 0);
        }

        [Test]
        public void CanCreateUpdateAndDeleteMacros()
        {
            var create = api.Macros.CreateMacro(new Macro()
                                                    {
                                                      Title  = "Roger Wilco",
                                                      Actions = new List<Action>()
                                                                    {
                                                                        new Action()
                                                                            {
                                                                                Field = "status",
                                                                                Value = "open"
                                                                            }
                                                                    }
                                                    });

            Assert.Greater(create.Macro.Id, 0);

            create.Macro.Title = "Roger wilco 2";
            var update = api.Macros.UpdateMacro(create.Macro);
            Assert.AreEqual(update.Macro.Id, create.Macro.Id);

            //Test apply macro
            var ticket = api.Tickets.CreateTicket(new Ticket()
                                                   {
                                                       Subject = "macro test ticket",
                                                       Comment = new Comment() { Body = "Testing macros" },
                                                       Priority = TicketPriorities.Normal
                                                   }).Ticket;            
            
            var applyToTicket = api.Macros.ApplyMacroToTicket(ticket.Id.Value, create.Macro.Id.Value);
            Assert.AreEqual(applyToTicket.Result.Ticket.Id, ticket.Id);

            Assert.True(api.Tickets.Delete(ticket.Id.Value));

            Assert.True(api.Macros.DeleteMacro(create.Macro.Id.Value));
        }
    }
}