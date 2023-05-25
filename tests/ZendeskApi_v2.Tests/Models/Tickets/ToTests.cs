using Newtonsoft.Json;
using NUnit.Framework;
using ZendeskApi_v2.Models.Tickets;

namespace ZendeskApi_v2.Tests.Models.Tickets;

[TestFixture]
public class ToTests
{
    private const string AllFieldsJson = "{\"address\":\"Test\",\"name\":\"Caller +49 89 555 666777\",\"formatted_phone\":\"+49 89 555 666777\",\"phone\":\"+4989555666777\"}";

    [Test]
    public void DeserializeAllFieldsTest()
    {
        var to = JsonConvert.DeserializeObject<To>(AllFieldsJson);

        Assert.That(to, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(to.FormattedPhone, Is.EqualTo("+49 89 555 666777"));
            Assert.That(to.Phone, Is.EqualTo("+4989555666777"));
            Assert.That(to.Name, Is.EqualTo("Caller +49 89 555 666777"));
            Assert.That(to.Address, Is.EqualTo("Test"));
        });
    }

    [Test]
    public void SerializeAllFieldsTest()
    {
        var to = new To
        {
            Address = "Test",
            Name = "Caller +49 89 555 666777",
            Phone = "+4989555666777",
            FormattedPhone = "+49 89 555 666777"
        };

        var toJson = JsonConvert.SerializeObject(to);

        Assert.That(toJson, Is.Not.Null);
        Assert.That(toJson, Is.EqualTo(AllFieldsJson));
    }
}
