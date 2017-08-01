using Newtonsoft.Json;
using NUnit.Framework;
using ZendeskApi_v2.Models.Tickets;

namespace Tests.Models.Tickets
{
    [TestFixture]
    public class FromTests
    {
        private const string AllFieldsJson = "{\"address\":\"Test\",\"name\":\"Caller +49 89 555 666777\",\"formatted_phone\":\"+49 89 555 666777\",\"phone\":\"+4989555666777\"}";

        [Test]
        public void DeserializeAllFieldsTest()
        {
            var from = JsonConvert.DeserializeObject<From>(AllFieldsJson);

            Assert.NotNull(from);
            Assert.AreEqual("+49 89 555 666777", from.FormattedPhone);
            Assert.AreEqual("+4989555666777", from.Phone);
            Assert.AreEqual("Caller +49 89 555 666777", from.Name);
            Assert.AreEqual("Test", from.Address);
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

            Assert.NotNull(jsonFrom);
            Assert.AreEqual(AllFieldsJson, jsonFrom);
        }
    }
}
