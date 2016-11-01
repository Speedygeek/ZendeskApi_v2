using System.Linq;
using NUnit.Framework;
using ZendeskApi_v2;
using ZendeskApi_v2.Models.Targets;

namespace Tests
{
    [TestFixture]
    public class TargetTests
    {
        private ZendeskApi api = new ZendeskApi(Settings.Site, Settings.Email, Settings.Password);

        [OneTimeSetUp]
        public void Init()
        {
            var targets = api.Targets.GetAllTargets();

            if (targets != null)
            {
                foreach (var target in targets.Targets.Where(o => o.Title.Contains("Test Email Target") || o.Title.Contains("Test Jira Target")))
                {
                    api.Targets.DeleteTarget(target.Id.Value);
                }
            }
        }

        [Test]
        public void CanCreateUpdateAndDeleteTargets()
        {
            var target = new EmailTarget()
            {
                Title = "Test Email Target",
                Active = false,
                Email = "test@test.com",
                Subject = "Test"
            };

            var emailResult = (EmailTarget)api.Targets.CreateTarget(target).Target;
            Assert.IsNotNull(emailResult);
            Assert.IsInstanceOf<EmailTarget>(emailResult);
            Assert.AreEqual("email_target", emailResult.Type);
            Assert.AreEqual("test@test.com", emailResult.Email);
            Assert.AreEqual("Test", emailResult.Subject);

            emailResult.Subject = "Test Update";

            var update = (EmailTarget)api.Targets.UpdateTarget(emailResult).Target;
            Assert.AreEqual(emailResult.Subject, update.Subject);

            Assert.True(api.Targets.DeleteTarget(emailResult.Id.Value));
        }

        [Test]
        public void CanRetrieveMultipleTargetTypes()
        {
            var emailTarget = new EmailTarget()
            {
                Title = "Test Email Target",
                Active = false,
                Email = "test@test.com",
                Subject = "Test"
            };

            var emailResult = (EmailTarget)api.Targets.CreateTarget(emailTarget).Target;
            Assert.IsNotNull(emailResult);
            Assert.IsInstanceOf<EmailTarget>(emailResult);

            var jiraTarget = new JiraTarget()
            {
                Title = "Test Jira Target",
                Active = false,
                TargetUrl = "http://test.com",
                Username = "testuser",
                Password = "testpassword"
            };

            var jiraResult = (JiraTarget)api.Targets.CreateTarget(jiraTarget).Target;
            Assert.IsNotNull(jiraResult);
            Assert.IsInstanceOf<JiraTarget>(jiraResult);

            var targets = api.Targets.GetAllTargets();
            foreach (var target in targets.Targets)
            {
                if (target.Id == emailResult.Id)
                {
                    Assert.IsInstanceOf<EmailTarget>(emailResult);
                }
                else if (target.Id == jiraResult.Id)
                {
                    Assert.IsInstanceOf<JiraTarget>(jiraResult);
                }
            }

            Assert.True(api.Targets.DeleteTarget(emailResult.Id.Value));
            Assert.True(api.Targets.DeleteTarget(jiraResult.Id.Value));
        }
    }
}