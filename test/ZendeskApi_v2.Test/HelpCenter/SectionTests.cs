using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using ZendeskApi_v2;
using ZendeskApi_v2.Extensions;
using ZendeskApi_v2.Models.Sections;

namespace Tests.HelpCenter
{
    [TestFixture]
    [Category("HelpCenter")]
    public class SectionTests
    {
        private readonly ZendeskApi api = new ZendeskApi(Settings.Site, Settings.AdminEmail, Settings.AdminPassword);

        private readonly long[] safeSections = new long[] { 360002891952, 360000205286, 201010935 };

        [OneTimeSetUp]
        public async Task Setup()
        {
            var sectionsResp = await api.HelpCenter.Sections.GetSectionsAsync();
            do
            {
                foreach (var section in sectionsResp.Sections)
                {
                    if (!safeSections.Contains(section.Id.Value))
                    {
                        await api.HelpCenter.Sections.DeleteSectionAsync(section.Id.Value);
                    }
                }

                if (!string.IsNullOrWhiteSpace(sectionsResp.NextPage))
                {
                    sectionsResp = await api.HelpCenter.Sections.GetSectionsAsync();
                }
            } while (!string.IsNullOrWhiteSpace(sectionsResp.NextPage));
        }

        [Test]
        public void CanGetSections()
        {
            var res = api.HelpCenter.Sections.GetSections();
            Assert.Greater(res.Count, 0);

            var res1 = api.HelpCenter.Sections.GetSectionById(res.Sections[0].Id.Value);
            Assert.AreEqual(res1.Section.Id, res.Sections[0].Id.Value);
        }

        [Test]
        public void CanGetSectionsPaged()
        {
            //https://csharpapi.zendesk.com/hc/en-us/categories/200382245-Category-1
            long category_id = 200382245;

            var section1 = api.HelpCenter.Sections.CreateSection(new Section {
                Name = "My Test section 1",
                Position = 0,
                CategoryId = category_id
            });

            var section2 = api.HelpCenter.Sections.CreateSection(new Section {
                Name = "My Test section 2",
                Position = 0,
                CategoryId = category_id
            });

            const int count = 2;
            var sections = api.HelpCenter.Sections.GetSections(count, 1);

            Assert.AreEqual(count, sections.Sections.Count);  // 2
            Assert.AreNotEqual(sections.Count, sections.Sections.Count);   // 2 != total count of sections (assumption)

            const int page = 2;
            var secondPage = api.HelpCenter.Sections.GetSections(count, page);

            Assert.AreEqual(count, secondPage.Sections.Count);

            var nextPage = secondPage.NextPage.GetQueryStringDict()
                .Where(x => x.Key == "page")
                .Select(x => x.Value)
                .FirstOrDefault();

            Assert.NotNull(nextPage);
            Assert.AreEqual(nextPage, (page + 1).ToString());
            Assert.That(api.HelpCenter.Sections.DeleteSection(section1.Section.Id.Value), Is.True);
            Assert.That(api.HelpCenter.Sections.DeleteSection(section2.Section.Id.Value), Is.True);
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
