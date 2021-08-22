using NUnit.Framework;
using ZendeskApi_v2.Tests.Base;

namespace ZendeskApi_v2.Tests
{
    [TestFixture]
    public class TagTests : TestBase
    {
        [Test]
        public void CanGetTags()
        {
            var res = Api.Tags.GetTags();
            Assert.That(res.Tags.Count, Is.GreaterThan(0));
        }

        [Test]
        public void CanAutocompleteTags()
        {
            var res = Api.Tags.GetTags();
            var auto = Api.Tags.AutocompleteTags(res.Tags[0].Name.Substring(0, 3));

            Assert.That(auto.Tags.Count, Is.GreaterThan(0));
        }
    }
}