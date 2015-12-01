﻿using NUnit.Framework;
using Tests.Properties;
using ZendeskApi_v2;
using ZendeskApi_v2.Models.HelpCenter.Categories;

namespace Tests.HelpCenter
{
    [TestFixture]
    [Category("HelpCenter")]
    public class CategoryTests
    {
        private ZendeskApi api = new ZendeskApi(Settings.Default.Site, Settings.Default.Email, Settings.Default.Password);

        [Test]
        //[Timeout(3000)]
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
                Name = "My Test category"
            });
            Assert.Greater(res.Category.Id, 0);

            res.Category.Description = "updated description";
            var update = api.HelpCenter.Categories.UpdateCategory(res.Category);
            Assert.AreEqual(update.Category.Description, res.Category.Description);

            Assert.True(api.HelpCenter.Categories.DeleteCategory(res.Category.Id.Value));
        }
    }
}