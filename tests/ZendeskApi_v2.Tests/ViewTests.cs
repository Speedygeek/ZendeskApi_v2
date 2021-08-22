using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using ZendeskApi_v2.Extensions;
using ZendeskApi_v2.Models.Views;
using ZendeskApi_v2.Models.Views.Executed;
using ZendeskApi_v2.Tests.Base;

namespace ZendeskApi_v2.Tests
{
    [TestFixture]
    [Category("View")]
    public class ViewTests : TestBase
    {
        [Test]
        public void CanGetViews()
        {
            var views = Api.Views.GetAllViews();
            Assert.That(views.Count, Is.GreaterThan(0));
        }

        [Test]
        public void CanGetActiveViews()
        {
            var views = Api.Views.GetActiveViews();
            Assert.That(views.Count, Is.GreaterThan(0));
        }

        [Test]
        public void CanGetCompactViews()
        {
            var views = Api.Views.GetCompactViews();
            Assert.That(views.Count, Is.GreaterThan(0));
        }

        [Test]
        public void CanGetViewById()
        {
            var views = Api.Views.GetAllViews();
            var view = Api.Views.GetView(views.Views.First().Id);
            Assert.That(view.View.Id, Is.GreaterThan(0));
        }

        [Test]
        public void CanExecuteViews()
        {
            Api.Views.GetAllViews();
            var res = Api.Views.ExecuteView(31559032);

            Assert.That(res.Rows.Count, Is.GreaterThan(0));
            Assert.That(res.Columns.Count, Is.GreaterThan(0));
        }

        [Test]
        public void CanExecutePagedView()
        {
            var res = Api.Views.ExecuteView(Settings.ViewId, "", true, 25, 2);

            Assert.That(res.Rows.Count, Is.EqualTo(25));

            var nextPage = res.NextPage.GetQueryStringDict()
                    .Where(x => x.Key == "page")
                        .Select(x => x.Value)
                        .FirstOrDefault();

            Assert.That(nextPage, Is.Not.Null);

            Assert.That(nextPage, Is.EqualTo("3"));
        }

        [Test]
        public void CanPreviewViews()
        {
            var preview = new PreviewViewRequest()
            {
                View = new PreviewView()
                {
                    All = new List<All> { new All { Field = "status", Value = "open", Operator = "is" } },
                    Output = new PreviewViewOutput { Columns = new List<string> { "subject" } }
                }
            };

            var previewRes = Api.Views.PreviewView(preview);
            Assert.That(previewRes.Rows.Count, Is.GreaterThan(0));
            Assert.That(previewRes.Columns.Count, Is.GreaterThan(0));
        }

        [Test]
        public void CanGetViewCounts()
        {
            var views = Api.Views.GetAllViews();
            var res = Api.Views.GetViewCounts(new List<long>() { views.Views[0].Id });
            Assert.That(res.ViewCounts.Count, Is.GreaterThan(0));

            Assert.That(views.Count, Is.GreaterThan(0));
        }

        [Test]
        public void CanGetViewCount()
        {
            var views = Api.Views.GetAllViews();
            var id = views.Views[0].Id;
            var res = Api.Views.GetViewCount(id);

            Assert.That(res.ViewCount.ViewId, Is.EqualTo(id));
        }
    }
}
