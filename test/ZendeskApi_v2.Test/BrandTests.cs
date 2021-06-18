using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ZendeskApi_v2;
using ZendeskApi_v2.Models.Brands;
using System;

namespace Tests
{
    [TestFixture]
    public class BrandTests
    {
        private readonly ZendeskApi api = new ZendeskApi(Settings.Site, Settings.AdminEmail, Settings.AdminPassword);

        [OneTimeSetUp]
        public void Init()
        {
            var brands = api.Brands.GetBrands();
            if (brands != null)
            {
                foreach (var brand in brands.Brands.Where(o => o.Name.Contains("Test Brand")))
                {
                    api.Brands.DeleteBrand(brand.Id.Value);
                }
            }
        }

        [Test]
        public void CanGetBrands()
        {
            var res = api.Brands.GetBrands();
            Assert.That(res.Count, Is.GreaterThan(0));

            var ind = api.Brands.GetBrand(res.Brands[0].Id.Value);
            Assert.That(res.Brands[0].Id, Is.EqualTo(ind.Brand.Id));
        }

        [Test]
        public void CanCreateUpdateAndDeleteBrand()
        {
            var brand = new Brand()
            {
                Name = "Test Brand",
                Active = true,
                Subdomain = string.Format("test-{0}", Guid.NewGuid())
            };

            var res = api.Brands.CreateBrand(brand);

            Assert.That(res.Brand.Id, Is.GreaterThan(0));

            res.Brand.Name = "Test Brand Updated";
            var update = api.Brands.UpdateBrand(res.Brand);
            Assert.That(res.Brand.Name, Is.EqualTo(update.Brand.Name));

            Assert.That(api.Brands.DeleteBrand(res.Brand.Id.Value), Is.True);
        }

    }
}