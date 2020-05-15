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

            Assert.AreEqual(count, categories.Categories.Count);  // 2
            Assert.AreNotEqual(categories.Count, categories.Categories.Count);   // 2 != total count of categories (assumption)

            const int page = 2;
            var secondPage = api.HelpCenter.Categories.GetCategories(count, page);

            Assert.AreEqual(count, secondPage.Categories.Count);

            var nextPage = secondPage.NextPage.GetQueryStringDict()
                .Where(x => x.Key == "page")
                .Select(x => x.Value)
                .FirstOrDefault();

            Assert.NotNull(nextPage);
            Assert.AreEqual(nextPage, (page + 1).ToString());
            Assert.True(api.HelpCenter.Categories.DeleteCategory(category1.Category.Id.Value));
            Assert.True(api.HelpCenter.Categories.DeleteCategory(category2.Category.Id.Value));
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