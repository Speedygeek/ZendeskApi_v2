using NUnit.Framework;
using System;
using System.Linq;
using ZendeskApi_v2.Models.Brands;
using ZendeskApi_v2.Tests.Base;

namespace ZendeskApi_v2.Tests;

[TestFixture]
public class BrandTests : TestBase
{
    [OneTimeSetUp]
    public void Setup()
    {
        var brands = Api.Brands.GetBrands();
        if (brands != null)
        {
            foreach (var brand in brands.Brands.Where(o => o.Name.Contains("Test Brand")))
            {
                Api.Brands.DeleteBrand(brand.Id.Value);
            }
        }
    }

    [Test]
    public void CanGetBrands()
    {
        var res = Api.Brands.GetBrands();
        Assert.That(res.Count, Is.GreaterThan(0));

        var ind = Api.Brands.GetBrand(res.Brands[0].Id.Value);
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

        var res = Api.Brands.CreateBrand(brand);

        Assert.That(res.Brand.Id, Is.GreaterThan(0));

        res.Brand.Name = "Test Brand Updated";
        var update = Api.Brands.UpdateBrand(res.Brand);
        Assert.Multiple(() =>
        {
            Assert.That(res.Brand.Name, Is.EqualTo(update.Brand.Name));

            Assert.That(Api.Brands.DeleteBrand(res.Brand.Id.Value), Is.True);
        });
    }
}
