using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using ZendeskApi_v2.Extensions;
using ZendeskApi_v2.Models.Constants;
using ZendeskApi_v2.Models.Macros;
using ZendeskApi_v2.Models.Tickets;
using ZendeskApi_v2.Tests.Base;

namespace ZendeskApi_v2.Tests;

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
    public void CanGetMacrosPaginated()
    {
        const int count = 5;
        var all = Api.Macros.GetAllMacros(count);

        Assert.That(all.Macros, Has.Count.EqualTo(count));  // 5
        Assert.That(all.Macros, Has.Count.Not.EqualTo(all.Count));   // 5 != total count of macros (assumption)

        const int page = 3;
        var thirdPage = Api.Macros.GetAllMacros(count, page);

        Assert.That(thirdPage.Macros, Has.Count.EqualTo(count));

        var nextPage = thirdPage.NextPage.GetQueryStringDict()
                .Where(x => x.Key == "page")
                    .Select(x => x.Value)
                    .FirstOrDefault();
        Assert.Multiple(() =>
        {
            Assert.That(nextPage, Is.Not.Null);

            Assert.That((page + 1).ToString(), Is.EqualTo(nextPage));
        });
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
        Assert.Multiple(() =>
        {
            Assert.That(ticket.Id, Is.EqualTo(applyToTicket.Result.Ticket.Id));
            Assert.That(Api.Tickets.Delete(ticket.Id.Value), Is.True);
            Assert.That(Api.Macros.DeleteMacro(create.Macro.Id.Value), Is.True);
        });
    }

    [Test]
    public void CanGetMacroByID()
    {
        var macro = Api.Macros.GetMacroById(45319945);

        Assert.That(macro, Is.Not.Null);
    }
}