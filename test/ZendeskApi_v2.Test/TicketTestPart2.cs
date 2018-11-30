using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ZendeskApi_v2;
using ZendeskApi_v2.Extensions;
using ZendeskApi_v2.Models.Brands;
using ZendeskApi_v2.Models.Constants;
using ZendeskApi_v2.Models.Shared;
using ZendeskApi_v2.Models.Tickets;
using ZendeskApi_v2.Requests;

namespace Tests
{
    [TestFixture]
    [Category("Tickets")]
    public class TicketTestsPart2
    {
        private ZendeskApi api = new ZendeskApi(Settings.Site, Settings.AdminEmail, Settings.AdminPassword);
        private const string ExternalId = "this is a test";

        [OneTimeTearDown]
        public async Task TestCleanUp()
        {
            var response = await api.Tickets.GetTicketsByExternalIdAsync(ExternalId);
            foreach (var item in response.Tickets)
            {
                await api.Tickets.DeleteAsync(item.Id.Value);
            }
        }

        [Test]
        public async Task CanGetTicketsByExternalIdAsync()
        {
            var ticket = new Ticket()
            {
                Subject = "my printer is on fire",
                Comment = new Comment { Body = "HELP", Public = true },
                Priority = TicketPriorities.Urgent,
                ExternalId = ExternalId
            };

            var resp1 = await api.Tickets.CreateTicketAsync(ticket);
            var response = await api.Tickets.GetTicketsByExternalIdAsync(ticket.ExternalId);

            Assert.That(response.Tickets.Count, Is.GreaterThan(0));
            Assert.That((await api.Tickets.DeleteAsync(resp1.Ticket.Id.Value)), Is.True);
        }

        [Test]
        public void CanGetTicketsByExternalId()
        {
            var ticket = new Ticket()
            {
                Subject = "my printer is on fire",
                Comment = new Comment { Body = "HELP", Public = true },
                Priority = TicketPriorities.Urgent,
                ExternalId = ExternalId
            };

            var resp1 = api.Tickets.CreateTicket(ticket);
            var response = api.Tickets.GetTicketsByExternalId(ticket.ExternalId);

            Assert.That(response.Tickets.Count, Is.GreaterThan(0));
            Assert.That(api.Tickets.Delete(resp1.Ticket.Id.Value), Is.True);
        }

    }
}
