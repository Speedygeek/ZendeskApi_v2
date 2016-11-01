using System.Threading.Tasks;
using NUnit.Framework;
using ZendeskApi_v2;
using ZendeskApi_v2.Models.Sections;
using ZendeskApi_v2.Requests.HelpCenter;

namespace Tests.HelpCenter
{
    [TestFixture]
    [Category("HelpCenter")]
    internal class SectionTests
    {
        private ZendeskApi api = new ZendeskApi(Settings.Site, Settings.Email, Settings.Password);

        [Test]
        public void CanGetSections()
        {
            var res = api.HelpCenter.Sections.GetSections();
            Assert.Greater(res.Count, 0);

            var res1 = api.HelpCenter.Sections.GetSectionById(res.Sections[0].Id.Value);
            Assert.AreEqual(res1.Section.Id, res.Sections[0].Id.Value);
        }

        [Test]
        public void CanCreateUpdateAndDeleteSections()
        {
            //https://csharpapi.zendesk.com/hc/en-us/categories/200382245-Category-1
            long category_id = 200382245;

            var res = api.HelpCenter.Sections.CreateSection(new Section
            {
                Name = "My Test section",
                Position = 12,
                CategoryId = category_id
            });
            Assert.Greater(res.Section.Id, 0);

            res.Section.Position = 42;
            var update = api.HelpCenter.Sections.UpdateSection(res.Section);
            Assert.That(update.Section.Position, Is.EqualTo(res.Section.Position));
            Assert.That(api.HelpCenter.Sections.DeleteSection(res.Section.Id.Value), Is.True);
        }

        [Test]
        public void CanGetSectionsAsync()
        {
            var res = api.HelpCenter.Sections.GetSectionsAsync().Result;
            Assert.Greater(res.Count, 0);

            var res1 = api.HelpCenter.Sections.GetSectionById(res.Sections[0].Id.Value);
            Assert.AreEqual(res1.Section.Id, res.Sections[0].Id.Value);
        }

        [Test]
        public async Task CanCreateUpdateAndDeleteSectionsAsync()
        {
            //https://csharpapi.zendesk.com/hc/en-us/categories/200382245-Category-1
            long category_id = 200382245;

            var res = await api.HelpCenter.Sections.CreateSectionAsync(new Section
            {
                Name = "My Test section",
                Position = 12,
                CategoryId = category_id
            });

            Assert.That(res.Section.Id, Is.GreaterThan(0));

            res.Section.Position = 42;
            var update = await api.HelpCenter.Sections.UpdateSectionAsync(res.Section);
            Assert.That(update.Section.Position, Is.EqualTo(res.Section.Position));
            Assert.That(await api.HelpCenter.Sections.DeleteSectionAsync(res.Section.Id.Value), Is.True);
        }
    }
}