﻿using System;
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
            var res = new ZendeskApi_v2.ZendeskApi("https://csharpapi.zendesk.com/api/v2", Settings.Email, Settings.Password);
            Assert.AreEqual(Settings.Site, res.ZendeskUrl);

            var res1 = new ZendeskApi_v2.ZendeskApi("csharpapi.zendesk.com/api/v2", Settings.Email, Settings.Password);
            Assert.AreEqual(Settings.Site, res1.ZendeskUrl);

            var res2 = new ZendeskApi_v2.ZendeskApi("csharpapi.zendesk.com", Settings.Email, Settings.Password);
            Assert.AreEqual(Settings.Site, res2.ZendeskUrl);

            var api3 = new ZendeskApi_v2.ZendeskApi("csharpapi", Settings.Email, Settings.Password);
            Assert.AreEqual(Settings.Site, api3.ZendeskUrl);

            var api4 = new ZendeskApi_v2.ZendeskApi("http://csharpapi.zendesk.com/api/v2", Settings.Email, Settings.Password);
            Assert.AreEqual(Settings.Site, api4.ZendeskUrl);
        }

        [Test]
        public void CanUseTokenAccess()
        {
            var api = new ZendeskApi_v2.ZendeskApi("https://csharpapi.zendesk.com/api/v2", Settings.Email, "", Settings.ApiToken, "en-us", null);
            var id = Settings.SampleTicketId;
            var ticket = api.Tickets.GetTicket(id).Ticket;

            Assert.NotNull(ticket);
            Assert.AreEqual(ticket.Id, id);
        }

        [Test]
        public void AsyncGivesCorrectException()
        {
            var api = new ZendeskApi_v2.ZendeskApi(
                "http://csharpapi.zendesk.com/api/v2",
                Settings.Email,
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
                Settings.Email,
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
                Settings.Email,
                Settings.Password);

            try
            {
                api.Users.CreateUser(new ZendeskApi_v2.Models.Users.User() { Name = "sdfsd sadfs", Email = "" });
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Contains("Email: cannot be blank") && e.Data["jsonException"] != null && e.Data["jsonException"].ToString().Contains("Email: cannot be blank"));
            }
        }
    }
}