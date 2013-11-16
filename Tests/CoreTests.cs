using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class CoreTests
    {
        [Test]
        public void CreatesUrisCorrectly()
        {
            var res = new ZendeskApi_v2.ZendeskApi("https://csharpapi.zendesk.com/api/v2",  Settings.Email, Settings.Password);
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
    }
}
