using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Tests.HelpCenter
{
    [TestFixture]
    class HelpCenterTests
    {
        [Test]
        public void CreatesUrisCorrectly()
        {
            var res = new ZendeskApi_v2.HelpCenter.HelpCenterApi("csharpapi.zendesk.com", Settings.Email,
                Settings.Password);
            Assert.AreEqual(Settings.Site, res.ZendeskUrl);

          
        }
    }
}
