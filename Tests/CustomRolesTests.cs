using NUnit.Framework;
using Tests.Properties;
using ZendeskApi_v2;

namespace Tests
{
    [TestFixture]
    public class CustomRolesTests
    {
        private ZendeskApi api = new ZendeskApi(Settings.Default.Site, Settings.Default.Email, Settings.Default.Password);

        //[Test]
        //[Ignore]
        public void CanGetCustomRoles()
        {
            //var res = api.CustomAgentRoles.GetCustomRoles();

            //Apparently you have to pay more to get this feature so the test will fail
            //Assert.Greater(res.CustomRoleCollection.Count, 0);

            //So I will just throw this up instead :P
            //  Assert.Inconclusive();
        }
    }
}