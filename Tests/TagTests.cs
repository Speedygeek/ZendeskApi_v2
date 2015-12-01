using NUnit.Framework;
using Tests.Properties;
using ZendeskApi_v2;

namespace Tests
{
    [TestFixture]
    public class TagTests
    {
        private ZendeskApi api = new ZendeskApi(Settings.Default.Site, Settings.Default.Email, Settings.Default.Password);

        [Test]
        public void CanGetTags()
        {
            var res = api.Tags.GetTags();

            Assert.Greater(res.Tags.Count, 0);
        }

        [Test]
        public void CanAutocompleteTags()
        {
            var res = api.Tags.GetTags();
            var auto = api.Tags.AutocompleteTags(res.Tags[0].Name.Substring(0, 3));

            Assert.Greater(auto.Tags.Count, 0);
        }
    }
}