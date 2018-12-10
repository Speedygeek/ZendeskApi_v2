using System.Threading.Tasks;
using NUnit.Framework;
using ZendeskApi_v2;
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
            var categotriesResponse = await api.HelpCenter.Categories.GetCategoriesAsync();

            do
            {
                foreach (var category in categotriesResponse.Categories)
                {
                    if (category.Name == "My Test category")
                    {
                        await api.HelpCenter.Categories.DeleteCategoryAsync(category.Id.Value);
                    }
                }

                if (!string.IsNullOrWhiteSpace(categotriesResponse.NextPage))
                {
                    categotriesResponse = await api.HelpCenter.Articles.GetByPageUrlAsync<GroupCategoryResponse>(categotriesResponse.NextPage, 100);
                }

            } while (!string.IsNullOrWhiteSpace(categotriesResponse.NextPage));
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