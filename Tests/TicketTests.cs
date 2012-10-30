using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ZenDeskApi_v2;


namespace Tests
{
    [TestFixture]
    public class TicketTests
    {        
        ZenDeskApi api = new ZenDeskApi(Settings.Site, Settings.Email, Settings.Password);

        [Test]
        public void CanGetTickets()
        {
            var tickets = api.Tickets.GetAll();
            Assert.True(tickets.Count > 0);
        }

        [Test]
        public  void CanGetTicketById()
        {
            var id = 1;
            var ticket = api.Tickets.Get(id);
            Assert.NotNull(ticket);
            Assert.AreEqual(ticket.Id, id);
        }

    }
}
