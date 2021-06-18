using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using ZendeskApi_v2;
using ZendeskApi_v2.Models.HelpCenter.Topics;
using ZendeskApi_v2.Models.UserSegments;

namespace Tests.HelpCenter
{
    [TestFixture]
    [Category("HelpCenter")]
    internal class UserSegmentTests
    {
        private readonly ZendeskApi api = new ZendeskApi(Settings.Site, Settings.AdminEmail, Settings.AdminPassword);

        [Test]
        public void CanGetUserSegments()
        {
            var res = api.HelpCenter.UserSegments.GetUserSegments();
            Assert.That(res.Count, Is.GreaterThan(0));

            var res1 = api.HelpCenter.UserSegments.GetUserSegment(res.UserSegments[0].Id.Value);
            Assert.That(res.UserSegments[0].Id.Value, Is.EqualTo(res1.UserSegment.Id));
        }

        [Test]
        public void CanGetUserSegmentsApplicable()
        {
            var res = api.HelpCenter.UserSegments.GetUserSegmentsApplicable();
            Assert.That(res.Count, Is.GreaterThan(0));

            var res1 = api.HelpCenter.UserSegments.GetUserSegment(res.UserSegments[0].Id.Value);
            Assert.That(res.UserSegments[0].Id.Value, Is.EqualTo(res1.UserSegment.Id));
        }

        [Test]
        public void CanCreateUpdateAndDeleteUserSegments()
        {
            var userSegment = new UserSegment() {
                Name = "My Test User Segment",
                UserType = UserType.signed_in_users
            };
            var res = api.HelpCenter.UserSegments.CreateUserSegment(userSegment);
            Assert.That(res.UserSegment.Id, Is.GreaterThan(0));

            res.UserSegment.UserType = UserType.staff;
            var update = api.HelpCenter.UserSegments.UpdateUserSegment(res.UserSegment);
            Assert.That(update.UserSegment.UserType, Is.EqualTo(res.UserSegment.UserType));
            Assert.That(api.HelpCenter.UserSegments.DeleteUserSegment(res.UserSegment.Id.Value), Is.True);
        }

        [Test]
        public void CanGetSecondPageUisngGetByPageUrl()
        {
            var pageSize = 3;

            var res = api.HelpCenter.UserSegments.GetUserSegments(perPage: pageSize);
            Assert.That(res.PageSize, Is.EqualTo(pageSize));

            var resp = api.HelpCenter.UserSegments.GetByPageUrl<GroupUserSegmentResponse>(res.NextPage, pageSize);
            Assert.That(resp.Page, Is.EqualTo(2));
        }

        //[Test]
        //public void CanGetSectionsByUserSegment()
        //{
        //    var res = api.HelpCenter.UserSegments.GetUserSegments();
        //    long category_id = 200382245;

        //    var sectionRes = api.HelpCenter.Sections.CreateSection(new Section {
        //        Name = "My Test section",
        //        CategoryId = category_id,
        //        UserSegmentId = res.UserSegments[0].Id
        //    });

        //    var res1 = api.HelpCenter.UserSegments.GetSectionsByUserSegmentId(res.UserSegments[0].Id.Value);

        //    Assert.That(res1.Sections.Count, Is.GreaterThan(0));
        //}

        [Test]
        public void CanGetTopicsByUserSegment()
        {
            var res = api.HelpCenter.UserSegments.GetUserSegments();

            var topicRes = api.HelpCenter.Topics.CreateTopic(new Topic {
                Name = "My Test Topic",
                UserSegmentId = res.UserSegments[0].Id
            });

            var res1 = api.HelpCenter.UserSegments.GetTopicsByUserSegmentId(res.UserSegments[0].Id.Value);

            Assert.That(res1.Topics.Count, Is.GreaterThan(0));
            Assert.That(api.HelpCenter.Topics.DeleteTopic(topicRes.Topic.Id.Value), Is.True);
        }

        [Test]
        public void CanGetUserSegmentsByUserId()
        {

            var res = api.HelpCenter.UserSegments.GetUserSegmentsByUserId(Settings.UserId);
            Assert.That(res.UserSegments.Count, Is.GreaterThan(0));
        }

        [Test]
        public void CanRetrieveUserSegmentOrTags()
        {
            var res = api.HelpCenter.UserSegments.GetUserSegments().UserSegments;
            var segment = res.First(seg => seg.Name == "Agents and managers (or_tags: tag1, tag2)");

            Assert.That(segment.OrTags.Count, Is.EqualTo(2));
            Assert.That(segment.OrTags.Contains("tag1"));
            Assert.That(segment.OrTags.Contains("tag2"));
        }

        [Test]
        public async Task CanGetUserSegmentsAsync()
        {
            var res = await api.HelpCenter.UserSegments.GetUserSegmentsAsync();
            Assert.That(res.Count, Is.GreaterThan(0));

            var res1 = await api.HelpCenter.UserSegments.GetUserSegmentAsync(res.UserSegments[0].Id.Value);
            Assert.That(res.UserSegments[0].Id.Value, Is.EqualTo(res1.UserSegment.Id));
        }

        [Test]
        public async Task CanGetUserSegmentsApplicableAsync()
        {
            var res = await api.HelpCenter.UserSegments.GetUserSegmentsApplicableAsync();
            Assert.That(res.Count, Is.GreaterThan(0));

            var res1 = await api.HelpCenter.UserSegments.GetUserSegmentAsync(res.UserSegments[0].Id.Value);
            Assert.That(res.UserSegments[0].Id.Value, Is.EqualTo(res1.UserSegment.Id));
        }

        [Test]
        public async Task CanCreateUpdateAndDeleteUserSegmentsAsync()
        {
            var userSegment = new UserSegment() {
                Name = "My Test User Segment Async",
                UserType = UserType.signed_in_users
            };
            var res = await api.HelpCenter.UserSegments.CreateUserSegmentAsync(userSegment);
            Assert.That(res.UserSegment.Id, Is.GreaterThan(0));

            res.UserSegment.UserType = UserType.staff;
            var update = await api.HelpCenter.UserSegments.UpdateUserSegmentAsync(res.UserSegment);
            Assert.That(update.UserSegment.UserType, Is.EqualTo(res.UserSegment.UserType));
            Assert.That(await api.HelpCenter.UserSegments.DeleteUserSegmentAsync(res.UserSegment.Id.Value), Is.True);
        }

        [Test]
        public async Task CanGetSecondPageUisngGetByPageUrlAsync()
        {
            var pageSize = 3;

            var res = await api.HelpCenter.UserSegments.GetUserSegmentsAsync(perPage: pageSize);
            Assert.That(res.PageSize, Is.EqualTo(pageSize));

            var resp = await api.HelpCenter.UserSegments.GetByPageUrlAsync<GroupUserSegmentResponse>(res.NextPage, pageSize);
            Assert.That(resp.Page, Is.EqualTo(2));
        }

        //[Test]
        //public async Task CanGetSectionsByUserSegmentAsync()
        //{
        //    var res = await api.HelpCenter.UserSegments.GetUserSegmentsAsync();
        //    long category_id = 200382245;

        //    var sectionRes = await api.HelpCenter.Sections.CreateSectionAsync(new Section {
        //        Name = "My Test section",
        //        CategoryId = category_id,
        //        UserSegmentId = res.UserSegments[0].Id
        //    });

        //    var res1 = await api.HelpCenter.UserSegments.GetSectionsByUserSegmentIdAsync(res.UserSegments[0].Id.Value);

        //    Assert.That(res1.Sections.Count, Is.GreaterThan(0));
        //}

        [Test]
        public async Task CanGetTopicsByUserSegmentAsync()
        {
            var res = await api.HelpCenter.UserSegments.GetUserSegmentsAsync();

            var topicRes = await api.HelpCenter.Topics.CreateTopicAsync(new Topic {
                Name = "My Test Topic",
                UserSegmentId = res.UserSegments[0].Id
            });

            var res1 = await api.HelpCenter.UserSegments.GetTopicsByUserSegmentIdAsync(res.UserSegments[0].Id.Value);

            Assert.That(res1.Topics.Count, Is.GreaterThan(0));
            Assert.That(await api.HelpCenter.Topics.DeleteTopicAsync(topicRes.Topic.Id.Value), Is.True);
        }

        [Test]
        public async Task CanGetUserSegmentsByUserIdAsync()
        {

            var res = await api.HelpCenter.UserSegments.GetUserSegmentsByUserIdAsync(Settings.UserId);
            Assert.That(res.UserSegments.Count, Is.GreaterThan(0));
        }
    }
}