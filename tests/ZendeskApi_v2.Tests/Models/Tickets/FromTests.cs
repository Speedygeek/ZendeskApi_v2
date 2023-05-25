using Newtonsoft.Json;
using NUnit.Framework;
using ZendeskApi_v2.Models.Tickets;

namespace ZendeskApi_v2.Tests.Models.Tickets;

[TestFixture]
public class FromTests
{
    private const string AllFieldsJson = "{\"address\":\"Test\",\"name\":\"Caller +49 89 555 666777\",\"formatted_phone\":\"+49 89 555 666777\",\"phone\":\"+4989555666777\"}";

    [Test]
    public void DeserializeAllFieldsTest()
    {
        var from = JsonConvert.DeserializeObject<From>(AllFieldsJson);

        Assert.That(from, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(from.FormattedPhone, Is.EqualTo("+49 89 555 666777"));
            Assert.That(from.Phone, Is.EqualTo("+4989555666777"));
            Assert.That(from.Name, Is.EqualTo("Caller +49 89 555 666777"));
            Assert.That(from.Address, Is.EqualTo("Test"));
        });
    }

    [Test]
    public void SerializeAllFieldsTest()
    {
        var from = new From
        {
            Address = "Test",
            Name = "Caller +49 89 555 666777",
            Phone = "+4989555666777",
            FormattedPhone = "+49 89 555 666777"
        };

        var jsonFrom = JsonConvert.SerializeObject(from);

        Assert.That(jsonFrom, Is.Not.Null);
        Assert.That(jsonFrom, Is.EqualTo(AllFieldsJson));
    }
}
