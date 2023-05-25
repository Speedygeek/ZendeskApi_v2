using NUnit.Framework;
using System;
using System.Net;
using ZendeskApi_v2.Models.Tickets;
using ZendeskApi_v2.Tests.Base;

namespace ZendeskApi_v2.Tests;

[TestFixture]
public class CoreTests : TestBase
{
    [Test]
    public void CreatesUrisCorrectly()
    {
        var res = new ZendeskApi("https://csharpapi.zendesk.com/api/v2", Admin.Email, Admin.Password);
        Assert.That(res.ZendeskUrl, Is.EqualTo(Organization.SiteURL));

        var res1 = new ZendeskApi("csharpapi.zendesk.com/api/v2", Admin.Email, Admin.Password);
        Assert.That(res1.ZendeskUrl, Is.EqualTo(Organization.SiteURL));

        var res2 = new ZendeskApi("csharpapi.zendesk.com", Admin.Email, Admin.Password);
        Assert.That(res2.ZendeskUrl, Is.EqualTo(Organization.SiteURL));

        var api3 = new ZendeskApi("csharpapi", Admin.Email, Admin.Password);
        Assert.That(api3.ZendeskUrl, Is.EqualTo(Organization.SiteURL));

        var api4 = new ZendeskApi("http://csharpapi.zendesk.com/api/v2", Admin.Email, Admin.Password);
        Assert.That(api4.ZendeskUrl, Is.EqualTo(Organization.SiteURL));
    }

    [Test]
    public void CanUseTokenAccess()
    {
        var api = new ZendeskApi("https://csharpapi.zendesk.com/Api/v2", Admin.Email, "", Admin.ApiToken, "en-us", null);
        var id = Settings.SampleTicketId;
        var ticket = api.Tickets.GetTicket(id).Ticket;
        Assert.Multiple(() =>
        {
            Assert.That(ticket, Is.Not.Null);
            Assert.That(id, Is.EqualTo(ticket.Id));
        });
    }

    [Test]
    public void AsyncGivesCorrectException()
    {
        var api = new ZendeskApi(
            "http://csharpapi.zendesk.com/Api/v2",
            Admin.Email,
            "Incorrect password");

        Assert.ThrowsAsync<WebException>(async () =>
        {
            await api.Tickets.CreateTicketAsync(new Ticket
            {
                Subject = "subject"
            });
        });
    }

    [Test]
    public void GivesCorrectException()
    {
        var api = new ZendeskApi(
            Organization.SiteURL,
            Admin.Email,
            "Incorrect password");

        Assert.Throws<WebException>(() =>
        {
            api.Tickets.CreateTicket(new Ticket
            {
                Subject = "subject"
            });
        });

        api = new ZendeskApi(
            Organization.SiteURL,
            Admin.Email,
            Admin.Password);

        try
        {
            api.Users.CreateUser(new ZendeskApi_v2.Models.Users.User() { Name = "sdfsd sadfs", Email = "" });
        }
        catch (Exception e)
        {
            Assert.That(e.Message.Contains("Email: cannot be blank") && e.Data["jsonException"] != null && e.Data["jsonException"].ToString().Contains("Email: cannot be blank"), Is.True);
        }
    }
}
