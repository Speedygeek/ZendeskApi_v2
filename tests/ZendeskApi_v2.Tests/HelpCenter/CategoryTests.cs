using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using ZendeskApi_v2.Extensions;
using ZendeskApi_v2.Models.HelpCenter.Categories;
using ZendeskApi_v2.Tests.Base;

namespace ZendeskApi_v2.Tests.HelpCenter;

[TestFixture]
[Category("HelpCenter")]
public class CategoryTests : TestBase
{
    [OneTimeSetUp]
    public async Task Setup()
    {
        var categoriesResponse = await Api.HelpCenter.Categories.GetCategoriesAsync();

        do
        {
            foreach (var category in categoriesResponse.Categories)
            {
                if (category.Name == "My Test category")
                {
                    await Api.HelpCenter.Categories.DeleteCategoryAsync(category.Id.Value);
                }
            }

            if (!string.IsNullOrWhiteSpace(categoriesResponse.NextPage))
            {
                categoriesResponse = await Api.HelpCenter.Articles.GetByPageUrlAsync<GroupCategoryResponse>(categoriesResponse.NextPage, 100);
            }
        } while (!string.IsNullOrWhiteSpace(categoriesResponse.NextPage));
    }

    [Test]
    public void CanGetCategories()
    {
        var res = Api.HelpCenter.Categories.GetCategories();
        Assert.That(res.Count, Is.GreaterThan(0));

        var res1 = Api.HelpCenter.Categories.GetCategoryById(res.Categories[0].Id.Value);
        Assert.That(res.Categories[0].Id.Value, Is.EqualTo(res1.Category.Id));
    }

    [Test]
    public void CanGetCategoriesPaged()
    {
        var category1 = Api.HelpCenter.Categories.CreateCategory(new Category
        {
            Name = "My Test category 1",
            Position = 0,
            Description = "First category"
        });

        var category2 = Api.HelpCenter.Categories.CreateCategory(new Category
        {
            Name = "My Test category 2",
            Position = 0,
            Description = "Second category"
        });

        const int count = 2;
        var categories = Api.HelpCenter.Categories.GetCategories(count, 1);
        Assert.Multiple(() =>
        {
            Assert.That(categories.Categories, Has.Count.EqualTo(count));  // 2
            Assert.That(categories.Count, Is.Not.EqualTo(categories.Categories.Count));   // 2 != total count of categories (assumption)
        });
        const int page = 2;
        var secondPage = Api.HelpCenter.Categories.GetCategories(count, page);

        Assert.That(secondPage.Categories, Has.Count.EqualTo(count));

        var nextPage = secondPage.NextPage.GetQueryStringDict()
            .Where(x => x.Key == "page")
            .Select(x => x.Value)
            .FirstOrDefault();

        Assert.That(nextPage, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(nextPage, Is.EqualTo((page + 1).ToString()));
            Assert.That(Api.HelpCenter.Categories.DeleteCategory(category1.Category.Id.Value), Is.True);
            Assert.That(Api.HelpCenter.Categories.DeleteCategory(category2.Category.Id.Value), Is.True);
        });
    }

    [Test]
    public void CanGetCategoriesPagedAsync()
    {
        var category1 = Api.HelpCenter.Categories.CreateCategory(new Category
        {
            Name = "My Test category 1",
            Position = 0,
            Description = "First category"
        });

        var category2 = Api.HelpCenter.Categories.CreateCategory(new Category
        {
            Name = "My Test category 2",
            Position = 0,
            Description = "Second category"
        });

        const int count = 2;
        var categoriesAsync = Api.HelpCenter.Categories.GetCategoriesAsync(count, 1).Result;

        Assert.That(categoriesAsync.Count, Is.GreaterThan(0));

        var categoryById1 = Api.HelpCenter.Categories.GetCategoryById(categoriesAsync.Categories[0].Id.Value);

        Assert.That(categoryById1.Category.Id, Is.EqualTo(categoriesAsync.Categories[0].Id.Value));

        const int page = 2;
        var secondPage = Api.HelpCenter.Categories.GetCategoriesAsync(count, page).Result;
        var categoryById2 = Api.HelpCenter.Categories.GetCategoryById(secondPage.Categories[0].Id.Value);
        Assert.Multiple(() =>
        {
            Assert.That(secondPage.Categories, Has.Count.EqualTo(count));
            Assert.That(categoryById2.Category.Id, Is.EqualTo(secondPage.Categories[0].Id.Value));
        });
        var nextPage = secondPage.NextPage.GetQueryStringDict()
            .Where(x => x.Key == "page")
            .Select(x => x.Value)
            .FirstOrDefault();

        Assert.That(nextPage, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(nextPage, Is.EqualTo((page + 1).ToString()));
            Assert.That(Api.HelpCenter.Categories.DeleteCategory(category1.Category.Id.Value), Is.True);
            Assert.That(Api.HelpCenter.Categories.DeleteCategory(category2.Category.Id.Value), Is.True);
        });
    }

    [Test]
    public void CanCreateUpdateAndDeleteCategories()
    {
        var res = Api.HelpCenter.Categories.CreateCategory(new Category()
        {
            Name = "My Test category",
            Description = "stuff and things",
            Position = 1
        });

        Assert.That(res.Category.Id, Is.GreaterThan(0));

        res.Category.Position = 2;
        var update = Api.HelpCenter.Categories.UpdateCategory(res.Category);
        Assert.Multiple(() =>
        {
            Assert.That(res.Category.Position, Is.EqualTo(update.Category.Position));

            Assert.That(Api.HelpCenter.Categories.DeleteCategory(res.Category.Id.Value), Is.True);
        });
    }
}
