using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using ZendeskApi_v2.Models.HelpCenter.Topics;
using ZendeskApi_v2.Models.UserSegments;
using ZendeskApi_v2.Tests.Base;

namespace ZendeskApi_v2.Tests.HelpCenter;

[TestFixture]
[Category("HelpCenter")]
internal class UserSegmentTests : TestBase
{
    [Test]
    public void CanGetUserSegments()
    {
        var res = Api.HelpCenter.UserSegments.GetUserSegments();
        Assert.That(res.Count, Is.GreaterThan(0));

        var res1 = Api.HelpCenter.UserSegments.GetUserSegment(res.UserSegments[0].Id.Value);
        Assert.That(res.UserSegments[0].Id.Value, Is.EqualTo(res1.UserSegment.Id));
    }

    [Test]
    public void CanGetUserSegmentsApplicable()
    {
        var res = Api.HelpCenter.UserSegments.GetUserSegmentsApplicable();
        Assert.That(res.Count, Is.GreaterThan(0));

        var res1 = Api.HelpCenter.UserSegments.GetUserSegment(res.UserSegments[0].Id.Value);
        Assert.That(res.UserSegments[0].Id.Value, Is.EqualTo(res1.UserSegment.Id));
    }

    [Test]
    public void CanCreateUpdateAndDeleteUserSegments()
    {
        var userSegment = new UserSegment()
        {
            Name = "My Test User Segment",
            UserType = UserType.signed_in_users
        };
        var res = Api.HelpCenter.UserSegments.CreateUserSegment(userSegment);
        Assert.That(res.UserSegment.Id, Is.GreaterThan(0));

        res.UserSegment.UserType = UserType.staff;
        var update = Api.HelpCenter.UserSegments.UpdateUserSegment(res.UserSegment);
        Assert.Multiple(() =>
        {
            Assert.That(update.UserSegment.UserType, Is.EqualTo(res.UserSegment.UserType));
            Assert.That(Api.HelpCenter.UserSegments.DeleteUserSegment(res.UserSegment.Id.Value), Is.True);
        });
    }

    [Test]
    public void CanGetSecondPageUsingGetByPageUrl()
    {
        var pageSize = 3;

        var res = Api.HelpCenter.UserSegments.GetUserSegments(perPage: pageSize);
        Assert.That(res.PageSize, Is.EqualTo(pageSize));

        var resp = Api.HelpCenter.UserSegments.GetByPageUrl<GroupUserSegmentResponse>(res.NextPage, pageSize);
        Assert.That(resp.Page, Is.EqualTo(2));
    }

    [Test]
    public void CanGetTopicsByUserSegment()
    {
        var res = Api.HelpCenter.UserSegments.GetUserSegments();

        var topicRes = Api.HelpCenter.Topics.CreateTopic(new Topic
        {
            Name = "My Test Topic",
            UserSegmentId = res.UserSegments[0].Id
        });

        var res1 = Api.HelpCenter.UserSegments.GetTopicsByUserSegmentId(res.UserSegments[0].Id.Value);
        Assert.Multiple(() =>
        {
            Assert.That(res1.Topics, Is.Not.Empty);
            Assert.That(Api.HelpCenter.Topics.DeleteTopic(topicRes.Topic.Id.Value), Is.True);
        });
    }

    [Test]
    public void CanGetUserSegmentsByUserId()
    {
        var res = Api.HelpCenter.UserSegments.GetUserSegmentsByUserId(Admin.ID);
        Assert.That(res.UserSegments, Is.Not.Empty);
    }

    [Test]
    public void CanRetrieveUserSegmentOrTags()
    {
        var res = Api.HelpCenter.UserSegments.GetUserSegments().UserSegments;
        var segment = res.First(seg => seg.Name == "Agents and managers (or_tags: tag1, tag2)");

        Assert.That(segment.OrTags, Has.Count.EqualTo(2));
        Assert.Multiple(() =>
        {
            Assert.That(segment.OrTags.Contains("tag1"));
            Assert.That(segment.OrTags.Contains("tag2"));
        });
    }

    [Test]
    public async Task CanGetUserSegmentsAsync()
    {
        var res = await Api.HelpCenter.UserSegments.GetUserSegmentsAsync();
        Assert.That(res.Count, Is.GreaterThan(0));

        var res1 = await Api.HelpCenter.UserSegments.GetUserSegmentAsync(res.UserSegments[0].Id.Value);
        Assert.That(res.UserSegments[0].Id.Value, Is.EqualTo(res1.UserSegment.Id));
    }

    [Test]
    public async Task CanGetUserSegmentsApplicableAsync()
    {
        var res = await Api.HelpCenter.UserSegments.GetUserSegmentsApplicableAsync();
        Assert.That(res.Count, Is.GreaterThan(0));

        var res1 = await Api.HelpCenter.UserSegments.GetUserSegmentAsync(res.UserSegments[0].Id.Value);
        Assert.That(res.UserSegments[0].Id.Value, Is.EqualTo(res1.UserSegment.Id));
    }

    [Test]
    public async Task CanCreateUpdateAndDeleteUserSegmentsAsync()
    {
        var userSegment = new UserSegment()
        {
            Name = "My Test User Segment Async",
            UserType = UserType.signed_in_users
        };
        var res = await Api.HelpCenter.UserSegments.CreateUserSegmentAsync(userSegment);
        Assert.That(res.UserSegment.Id, Is.GreaterThan(0));

        res.UserSegment.UserType = UserType.staff;
        var update = await Api.HelpCenter.UserSegments.UpdateUserSegmentAsync(res.UserSegment);
        Assert.Multiple(async () =>
        {
            Assert.That(update.UserSegment.UserType, Is.EqualTo(res.UserSegment.UserType));
            Assert.That(await Api.HelpCenter.UserSegments.DeleteUserSegmentAsync(res.UserSegment.Id.Value), Is.True);
        });
    }

    [Test]
    public async Task CanGetSecondPageUsingGetByPageUrlAsync()
    {
        var pageSize = 3;

        var res = await Api.HelpCenter.UserSegments.GetUserSegmentsAsync(perPage: pageSize);
        Assert.That(res.PageSize, Is.EqualTo(pageSize));

        var resp = await Api.HelpCenter.UserSegments.GetByPageUrlAsync<GroupUserSegmentResponse>(res.NextPage, pageSize);
        Assert.That(resp.Page, Is.EqualTo(2));
    }

    [Test]
    public async Task CanGetTopicsByUserSegmentAsync()
    {
        var res = await Api.HelpCenter.UserSegments.GetUserSegmentsAsync();

        var topicRes = await Api.HelpCenter.Topics.CreateTopicAsync(new Topic
        {
            Name = "My Test Topic",
            UserSegmentId = res.UserSegments[0].Id
        });

        var res1 = await Api.HelpCenter.UserSegments.GetTopicsByUserSegmentIdAsync(res.UserSegments[0].Id.Value);
        Assert.Multiple(async () =>
        {
            Assert.That(res1.Topics, Is.Not.Empty);
            Assert.That(await Api.HelpCenter.Topics.DeleteTopicAsync(topicRes.Topic.Id.Value), Is.True);
        });
    }

    [Test]
    public async Task CanGetUserSegmentsByUserIdAsync()
    {
        var res = await Api.HelpCenter.UserSegments.GetUserSegmentsByUserIdAsync(Admin.ID);
        Assert.That(res.UserSegments, Is.Not.Empty);
    }
}
