using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using ZendeskApi_v2.Extensions;
using ZendeskApi_v2.Models.Sections;
using ZendeskApi_v2.Tests.Base;

namespace ZendeskApi_v2.Tests.HelpCenter;

[TestFixture]
[Category("HelpCenter")]
public class SectionTests : TestBase
{
    private readonly long[] safeSections = new long[] { 360002891952, 360000205286, 201010935 };

    [OneTimeSetUp]
    public async Task Setup()
    {
        var sectionsResp = await Api.HelpCenter.Sections.GetSectionsAsync();
        do
        {
            foreach (var section in sectionsResp.Sections)
            {
                if (!safeSections.Contains(section.Id.Value))
                {
                    await Api.HelpCenter.Sections.DeleteSectionAsync(section.Id.Value);
                }
            }

            if (!string.IsNullOrWhiteSpace(sectionsResp.NextPage))
            {
                sectionsResp = await Api.HelpCenter.Sections.GetSectionsAsync();
            }
        } while (!string.IsNullOrWhiteSpace(sectionsResp.NextPage));
    }

    [Test]
    public void CanGetSections()
    {
        var res = Api.HelpCenter.Sections.GetSections();
        Assert.That(res.Count, Is.GreaterThan(0));

        var res1 = Api.HelpCenter.Sections.GetSectionById(res.Sections[0].Id.Value);
        Assert.That(res.Sections[0].Id.Value, Is.EqualTo(res1.Section.Id));
    }

    [Test]
    public void CanGetSectionsPaged()
    {
        //https://csharpapi.zendesk.com/hc/en-us/categories/200382245-Category-1
        long category_id = 200382245;

        var section1 = Api.HelpCenter.Sections.CreateSection(new Section
        {
            Name = "My Test section 1",
            Position = 0,
            CategoryId = category_id
        });

        var section2 = Api.HelpCenter.Sections.CreateSection(new Section
        {
            Name = "My Test section 2",
            Position = 0,
            CategoryId = category_id
        });

        const int count = 2;
        var sections = Api.HelpCenter.Sections.GetSections(count, 1);
        Assert.Multiple(() =>
        {
            Assert.That(sections.Sections, Has.Count.EqualTo(count));  // 2
            Assert.That(sections.Count, Is.Not.EqualTo(sections.Sections.Count));   // 2 != total count of sections (assumption)
        });
        const int page = 2;
        var secondPage = Api.HelpCenter.Sections.GetSections(count, page);

        Assert.That(secondPage.Sections, Has.Count.EqualTo(count));

        var nextPage = secondPage.NextPage.GetQueryStringDict()
            .Where(x => x.Key == "page")
            .Select(x => x.Value)
            .FirstOrDefault();

        Assert.That(nextPage, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(nextPage, Is.EqualTo((page + 1).ToString()));
            Assert.That(Api.HelpCenter.Sections.DeleteSection(section1.Section.Id.Value), Is.True);
            Assert.That(Api.HelpCenter.Sections.DeleteSection(section2.Section.Id.Value), Is.True);
        });
    }

    [Test]
    public void CanGetSectionsPagedAsync()
    {
        //https://csharpapi.zendesk.com/hc/en-us/categories/200382245-Category-1
        long category_id = 200382245;

        var section1 = Api.HelpCenter.Sections.CreateSection(new Section
        {
            Name = "My Test section 1",
            Position = 0,
            CategoryId = category_id
        });

        var section2 = Api.HelpCenter.Sections.CreateSection(new Section
        {
            Name = "My Test section 2",
            Position = 0,
            CategoryId = category_id
        });

        const int count = 2;
        var sectionsAsync = Api.HelpCenter.Sections.GetSectionsAsync(count, 1).Result;

        Assert.That(sectionsAsync.Count, Is.GreaterThan(0));

        var sectionById1 = Api.HelpCenter.Sections.GetSectionById(sectionsAsync.Sections[0].Id.Value);

        Assert.That(sectionById1.Section.Id, Is.EqualTo(sectionsAsync.Sections[0].Id.Value));

        const int page = 2;
        var secondPage = Api.HelpCenter.Sections.GetSectionsAsync(count, page).Result;
        var sectionById2 = Api.HelpCenter.Sections.GetSectionById(secondPage.Sections[0].Id.Value);
        Assert.Multiple(() =>
        {
            Assert.That(secondPage.Sections, Has.Count.EqualTo(count));
            Assert.That(sectionById2.Section.Id, Is.EqualTo(secondPage.Sections[0].Id.Value));
        });
        var nextPage = secondPage.NextPage.GetQueryStringDict()
            .Where(x => x.Key == "page")
            .Select(x => x.Value)
            .FirstOrDefault();

        Assert.That(nextPage, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(nextPage, Is.EqualTo((page + 1).ToString()));
            Assert.That(Api.HelpCenter.Sections.DeleteSection(section1.Section.Id.Value), Is.True);
            Assert.That(Api.HelpCenter.Sections.DeleteSection(section2.Section.Id.Value), Is.True);
        });
    }

    [Test]
    public void CanCreateUpdateAndDeleteSections()
    {
        //https://csharpapi.zendesk.com/hc/en-us/categories/200382245-Category-1
        long category_id = 200382245;

        var res = Api.HelpCenter.Sections.CreateSection(new Section
        {
            Name = "My Test section",
            Position = 12,
            CategoryId = category_id
        });
        Assert.That(res.Section.Id, Is.GreaterThan(0));

        res.Section.Position = 42;
        var update = Api.HelpCenter.Sections.UpdateSection(res.Section);
        Assert.Multiple(() =>
        {
            Assert.That(update.Section.Position, Is.EqualTo(res.Section.Position));
            Assert.That(Api.HelpCenter.Sections.DeleteSection(res.Section.Id.Value), Is.True);
        });
    }

    [Test]
    public void CanGetSectionsAsync()
    {
        var res = Api.HelpCenter.Sections.GetSectionsAsync().Result;
        Assert.That(res.Count, Is.GreaterThan(0));

        var res1 = Api.HelpCenter.Sections.GetSectionById(res.Sections[0].Id.Value);
        Assert.That(res.Sections[0].Id.Value, Is.EqualTo(res1.Section.Id));
    }

    [Test]
    public async Task CanCreateUpdateAndDeleteSectionsAsync()
    {
        //https://csharpapi.zendesk.com/hc/en-us/categories/200382245-Category-1
        long category_id = 200382245;

        var res = await Api.HelpCenter.Sections.CreateSectionAsync(new Section
        {
            Name = "My Test section",
            Position = 12,
            CategoryId = category_id
        });

        Assert.That(res.Section.Id, Is.GreaterThan(0));

        res.Section.Position = 42;
        var update = await Api.HelpCenter.Sections.UpdateSectionAsync(res.Section);
        Assert.Multiple(async () =>
        {
            Assert.That(update.Section.Position, Is.EqualTo(res.Section.Position));
            Assert.That(await Api.HelpCenter.Sections.DeleteSectionAsync(res.Section.Id.Value), Is.True);
        });
    }
}
