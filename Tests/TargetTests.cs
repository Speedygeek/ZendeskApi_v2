using NUnit.Framework;
using ZendeskApi_v2;
using ZendeskApi_v2.Models.Targets;

namespace Tests
{
    [TestFixture]
    public class TargetTests
    {
        private ZendeskApi api = new ZendeskApi(Settings.Site, Settings.Email, Settings.Password);

        [Test]
        public void CanCreateTargets()
        {
            var target = new EmailTarget()
            {
                Title   = "Test Target",
                Type    = "email_target",
                Active  = false,
                Email   = "test@test.com",
                Subject = "Test"
            };

            var res = api.Targets.CreateTarget(target);
            api.Targets.DeleteTarget(res.Target.Id.Value);
        }
    }
}