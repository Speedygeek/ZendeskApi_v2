using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

using ZendeskApi_v2.Models.Tickets;

namespace Tests
{
    [TestFixture]
    public class CoreTests
    {
        [Test]
        public void CreatesUrisCorrectly()
        {
            var res = new ZendeskApi_v2.ZendeskApi("https://csharpapi.zendesk.com/api/v2", Settings.AdminEmail, Settings.AdminPassword);
            Assert.That(res.ZendeskUrl, Is.EqualTo(Settings.Site));

            var res1 = new ZendeskApi_v2.ZendeskApi("csharpapi.zendesk.com/api/v2", Settings.AdminEmail, Settings.AdminPassword);
            Assert.That(res1.ZendeskUrl, Is.EqualTo(Settings.Site));

            var res2 = new ZendeskApi_v2.ZendeskApi("csharpapi.zendesk.com", Settings.AdminEmail, Settings.AdminPassword);
            Assert.That(res2.ZendeskUrl, Is.EqualTo(Settings.Site));

            var api3 = new ZendeskApi_v2.ZendeskApi("csharpapi", Settings.AdminEmail, Settings.AdminPassword);
            Assert.That(api3.ZendeskUrl, Is.EqualTo(Settings.Site));

            var api4 = new ZendeskApi_v2.ZendeskApi("http://csharpapi.zendesk.com/api/v2", Settings.AdminEmail, Settings.AdminPassword);
            Assert.That(api4.ZendeskUrl, Is.EqualTo(Settings.Site));
        }

        [Test]
        public void CanUseTokenAccess()
        {
            var api = new ZendeskApi_v2.ZendeskApi("https://csharpapi.zendesk.com/api/v2", Settings.AdminEmail, "", Settings.ApiToken, "en-us", null);
            var id = Settings.SampleTicketId;
            var ticket = api.Tickets.GetTicket(id).Ticket;

            Assert.That(ticket, Is.Not.Null);
            Assert.That(id, Is.EqualTo(ticket.Id));
        }

        [Test]
        public void AsyncGivesCorrectException()
        {
            var api = new ZendeskApi_v2.ZendeskApi(
                "http://csharpapi.zendesk.com/api/v2", 
                Settings.AdminEmail, 
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
            var api = new ZendeskApi_v2.ZendeskApi(
                Settings.Site,
                Settings.AdminEmail,
                "Incorrect password");

            Assert.Throws<WebException>(() =>
            {
                api.Tickets.CreateTicket(new Ticket
                {
                    Subject = "subject"
                });
            });

            api = new ZendeskApi_v2.ZendeskApi(
                Settings.Site,
                Settings.AdminEmail,
                Settings.AdminPassword);

            try
            {
                api.Users.CreateUser(new ZendeskApi_v2.Models.Users.User() {Name = "sdfsd sadfs", Email = ""});
            }
            catch (Exception e)
            {
                Assert.That(e.Message.Contains("Email: cannot be blank") && e.Data["jsonException"] != null && e.Data["jsonException"].ToString().Contains("Email: cannot be blank"), Is.True);
            }

        }
    }
}
