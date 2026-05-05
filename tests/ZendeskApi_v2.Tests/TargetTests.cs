using NUnit.Framework;
using System.Linq;
using ZendeskApi_v2.Models.Targets;
using ZendeskApi_v2.Tests.Base;

namespace ZendeskApi_v2.Tests;

[TestFixture]
public class TargetTests : TestBase
{
    [OneTimeSetUp]
    public void Setup()
    {
        var targets = Api.Targets.GetAllTargets();

        if (targets != null)
        {
            foreach (var target in targets.Targets.Where(o => o.Title.Contains("Test Email Target") || o.Title.Contains("Test Jira Target")))
            {
                Api.Targets.DeleteTarget(target.Id.Value);
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

        var emailResult = (EmailTarget)Api.Targets.CreateTarget(target).Target;
        Assert.That(emailResult, Is.Not.Null);
        Assert.That(emailResult, Is.InstanceOf<EmailTarget>());
        using (Assert.EnterMultipleScope())
        {
            Assert.That(emailResult.Type, Is.EqualTo("email_target"));
            Assert.That(emailResult.Email, Is.EqualTo("test@test.com"));
            Assert.That(emailResult.Subject, Is.EqualTo("Test"));
        }
        emailResult.Subject = "Test Update";

        var update = (EmailTarget)Api.Targets.UpdateTarget(emailResult).Target;
        using (Assert.EnterMultipleScope())
        {
            Assert.That(update.Subject, Is.EqualTo(emailResult.Subject));

            Assert.That(Api.Targets.DeleteTarget(emailResult.Id.Value), Is.True);
        }
    }

    [Test]
    //[Ignore("Opend issue with zendesk")]
    public void CanRetrieveMultipleTargetTypes()
    {
        var emailTarget = new EmailTarget()
        {
            Title = "Test Email Target",
            Active = false,
            Email = "test@test.com",
            Subject = "Test"
        };

        var emailResult = (EmailTarget)Api.Targets.CreateTarget(emailTarget).Target;
        Assert.That(emailResult, Is.Not.Null);
        Assert.That(emailResult, Is.InstanceOf<EmailTarget>());

        var emailTarget2 = new EmailTarget()
        {
            Title = "Test Email Target",
            Active = false,
            Email = "test@test.com",
            Subject = "Test"
        };

        var emailResult2 = (EmailTarget)Api.Targets.CreateTarget(emailTarget2).Target;
        Assert.That(emailResult2, Is.Not.Null);
        Assert.That(emailResult2, Is.InstanceOf<EmailTarget>());
        _ = Api.Targets.GetAllTargets();
        using (Assert.EnterMultipleScope())
        {
            Assert.That(Api.Targets.DeleteTarget(emailResult.Id.Value), Is.True);
            Assert.That(Api.Targets.DeleteTarget(emailResult2.Id.Value), Is.True);
        }
    }
}