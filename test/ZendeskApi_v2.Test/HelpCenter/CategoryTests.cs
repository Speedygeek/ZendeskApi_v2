using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using ZendeskApi_v2;
using ZendeskApi_v2.Extensions;
using ZendeskApi_v2.Models.HelpCenter.Categories;

namespace Tests.HelpCenter
{
    [TestFixture]
    [Category("HelpCenter")]
    public class CategoryTests
    {
        private readonly ZendeskApi api = new ZendeskApi(Settings.Site, Settings.AdminEmail, Settings.AdminPassword);


        [OneTimeSetUp]
        public async Task Setup()
        {
            var categoriesResponse = await api.HelpCenter.Categories.GetCategoriesAsync();

            do
            {
                foreach (var category in categoriesResponse.Categories)
                {
                    if (category.Name == "My Test category")
                    {
                        await api.HelpCenter.Categories.DeleteCategoryAsync(category.Id.Value);
                    }
                }

                if (!string.IsNullOrWhiteSpace(categoriesResponse.NextPage))
                {
                    categoriesResponse = await api.HelpCenter.Articles.GetByPageUrlAsync<GroupCategoryResponse>(categoriesResponse.NextPage, 100);
                }

            } while (!string.IsNullOrWhiteSpace(categoriesResponse.NextPage));
        }


        [Test]
        public void CanGetCategories()
        {
            var res = api.HelpCenter.Categories.GetCategories();
            Assert.Greater(res.Count, 0);

            var res1 = api.HelpCenter.Categories.GetCategoryById(res.Categories[0].Id.Value);
            Assert.AreEqual(res1.Category.Id, res.Categories[0].Id.Value);
        }

        [Test]
        public void CanGetCategoriesPaged()
        {
           var category1 = api.HelpCenter.Categories.CreateCategory(new Category {
                Name = "My Test category 1",
                Position = 0,
                Description = "First category"
            });

            var category2 = api.HelpCenter.Categories.CreateCategory(new Category {
                Name = "My Test category 2",
                Position = 0,
                Description = "Second category"
            });

            const int count = 2;
            var categories = api.HelpCenter.Categories.GetCategories(count, 1);

            Assert.That(count, Is.EqualTo(categories.Categories.Count));  // 2
            Assert.That(categories.Count, !Is.EqualTo(categories.Categories.Count));   // 2 != total count of categories (assumption)

            const int page = 2;
            var secondPage = api.HelpCenter.Categories.GetCategories(count, page);

            Assert.That(count, Is.EqualTo(secondPage.Categories.Count));

            var nextPage = secondPage.NextPage.GetQueryStringDict()
                .Where(x => x.Key == "page")
                .Select(x => x.Value)
                .FirstOrDefault();

            Assert.That(nextPage != null);
            Assert.That(nextPage, Is.EqualTo((page + 1).ToString()));
            Assert.That(api.HelpCenter.Categories.DeleteCategory(category1.Category.Id.Value), Is.True);
            Assert.That(api.HelpCenter.Categories.DeleteCategory(category2.Category.Id.Value), Is.True);
        }

        [Test]
        public void CanGetCategoriesPagedAsync()
        {
            var category1 = api.HelpCenter.Categories.CreateCategory(new Category {
                Name = "My Test category 1",
                Position = 0,
                Description = "First category"
            });

            var category2 = api.HelpCenter.Categories.CreateCategory(new Category {
                Name = "My Test category 2",
                Position = 0,
                Description = "Second category"
            });

            const int count = 2;
            var categoriesAsync = api.HelpCenter.Categories.GetCategoriesAsync(count, 1).Result;

            Assert.That(categoriesAsync.Count > 0);

            var categoryById1 = api.HelpCenter.Categories.GetCategoryById(categoriesAsync.Categories[0].Id.Value);

            Assert.That(categoryById1.Category.Id, Is.EqualTo(categoriesAsync.Categories[0].Id.Value));

            const int page = 2;
            var secondPage = api.HelpCenter.Categories.GetCategoriesAsync(count, page).Result;
            var categoryById2 = api.HelpCenter.Categories.GetCategoryById(secondPage.Categories[0].Id.Value);

            Assert.That(count, Is.EqualTo(secondPage.Categories.Count));
            Assert.That(categoryById2.Category.Id, Is.EqualTo(secondPage.Categories[0].Id.Value));

            var nextPage = secondPage.NextPage.GetQueryStringDict()
                .Where(x => x.Key == "page")
                .Select(x => x.Value)
                .FirstOrDefault();

            Assert.That(nextPage != null);
            Assert.That(nextPage, Is.EqualTo((page + 1).ToString()));
            Assert.That(api.HelpCenter.Categories.DeleteCategory(category1.Category.Id.Value), Is.True);
            Assert.That(api.HelpCenter.Categories.DeleteCategory(category2.Category.Id.Value), Is.True);
        }

        [Test]
        public void CanCreateUpdateAndDeleteCategories()
        {
            var res = api.HelpCenter.Categories.CreateCategory(new Category()
            {
                Name = "My Test category",
                Description = "stuff and things",
                Position = 1
            });

            Assert.Greater(res.Category.Id, 0);

            res.Category.Position = 2;
            var update = api.HelpCenter.Categories.UpdateCategory(res.Category);
            Assert.AreEqual(update.Category.Position, res.Category.Position);

            Assert.True(api.HelpCenter.Categories.DeleteCategory(res.Category.Id.Value));
        }
    }
}