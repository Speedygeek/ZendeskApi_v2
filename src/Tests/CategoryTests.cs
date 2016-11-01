using NUnit.Framework;
using ZendeskApi_v2;
using ZendeskApi_v2.Models.Categories;

namespace Tests
{
    [TestFixture]
    public class CategoryTests
    {
        private ZendeskApi api = new ZendeskApi(Settings.Site, Settings.Email, Settings.Password);

        [TestFixtureSetUp]
        public void Setup()
        {
            var res = api.Categories.GetCategoriesAsync().Result;
            foreach (var category in res.Categories)
            {
                if (category.Name == "My Test category two")
                {
                    var test = api.Categories.DeleteCategoryAsync(category.Id.Value).Result;
                }
            }
        }

        [Test]
        public void CanGetCategories()
        {
            var res = api.Categories.GetCategories();
            Assert.Greater(res.Count, 0);

            var res1 = api.Categories.GetCategoryById(res.Categories[0].Id.Value);
            Assert.AreEqual(res1.Category.Id, res.Categories[0].Id);
        }

        [Test]
        public void CanCreateUpdateAndDeleteCategories()
        {
            var res = api.Categories.CreateCategory(new Category()
            {
                Name = "My Test category two"
            });
            Assert.Greater(res.Category.Id, 0);

            res.Category.Description = "updated description";
            var update = api.Categories.UpdateCategory(res.Category);
            //update = api.Categories.GetCategoryById(res.Category.Id.Value);
            Assert.AreEqual(update.Category.Description, res.Category.Description);

            Assert.True(api.Categories.DeleteCategory(res.Category.Id.Value));
        }
    }
}