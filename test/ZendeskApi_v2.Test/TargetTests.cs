using NUnit.Framework;
using System.Linq;
using System.Net;
using ZendeskApi_v2;
using ZendeskApi_v2.Models.Targets;

namespace Tests
{
    [TestFixture]
    public class TargetTests
    {
        private readonly ZendeskApi api = new ZendeskApi(Settings.Site, Settings.AdminEmail, Settings.AdminPassword);

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
        public void CanCreateUpdateAndDeleteHttpTargets()
        {
            var target = new HTTPTarget()
            {
                Title = "Test Email Target",
                Active = false, 
                TargetUrl= "https://test.com",
                ContentType = "application/json",
                Method = "post",
                Username = "TestUser",
                Password = "TestPass"
            };

            var targetResult = (HTTPTarget)api.Targets.CreateTarget(target).Target;
            Assert.IsNotNull(targetResult);
            Assert.IsInstanceOf<HTTPTarget>(targetResult);
            Assert.IsFalse(targetResult.Active);
            Assert.AreEqual("https://test.com", targetResult.TargetUrl);
            Assert.AreEqual("http_target", targetResult.Type);
            Assert.AreEqual("application/json", targetResult.ContentType);
            Assert.AreEqual("post", targetResult.Method);
            Assert.AreEqual("TestUser", targetResult.Username);
            Assert.IsNull(targetResult.Password);

            targetResult.Active = true;

            var update = (HTTPTarget)api.Targets.UpdateTarget(targetResult).Target;
            Assert.AreEqual(targetResult.Active, update.Active);

            Assert.True(api.Targets.DeleteTarget(update.Id.Value));
        }

        [Test]
        public void CanCreateUpdateAndDeleteTargets()
        {
            var target = new EmailTarget()
            {
                Title   = "Test Email Target",
                Active  = false,
                Email   = "test@test.com",
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
                Title   = "Test Email Target",
                Active  = false,
                Email   = "test@test.com",
                Subject = "Test"
            };

            var emailResult = (EmailTarget)api.Targets.CreateTarget(emailTarget).Target;
            Assert.IsNotNull(emailResult);
            Assert.IsInstanceOf<EmailTarget>(emailResult);

            var jiraTarget = new JiraTarget()
            {
                Title     = "Test Jira Target",
                Active    = false,
                TargetUrl = "http://test.com",
                Username  = "testuser",
                Password  = "testpassword"
            };

            var jiraResult = (JiraTarget)api.Targets.CreateTarget(jiraTarget).Target;
            Assert.IsNotNull(jiraResult);
            Assert.IsInstanceOf<JiraTarget>(jiraResult);

            var targets = api.Targets.GetAllTargets();
            foreach (var target in targets.Targets)
            {
                if(target.Id == emailResult.Id)
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