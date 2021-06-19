using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ZendeskApi_v2;
using ZendeskApi_v2.Extensions;
using ZendeskApi_v2.Models.Constants;
using ZendeskApi_v2.Models.Shared;
using ZendeskApi_v2.Models.Tickets;
using ZendeskApi_v2.Models.Views;
using ZendeskApi_v2.Models.Views.Executed;


namespace Tests
{
    [TestFixture]
    public class ViewTests
    {
        readonly ZendeskApi api = new ZendeskApi(Settings.Site, Settings.AdminEmail, Settings.AdminPassword);

        [Test]
        public void CanGetViews()
        {
            var views = api.Views.GetAllViews();
            Assert.That(views.Count, Is.GreaterThan(0));
        }

        [Test]
        public void CanGetActiveViews()
        {
            var views = api.Views.GetActiveViews();
            Assert.That(views.Count, Is.GreaterThan(0));
        }

        [Test]
        public void CanGetCompactViews()
        {
            var views = api.Views.GetCompactViews();
            Assert.That(views.Count, Is.GreaterThan(0));
        }

        [Test]
        public void CanGetViewById()
        {
            var views = api.Views.GetAllViews();
            var view = api.Views.GetView(views.Views.First().Id);
            Assert.That(view.View.Id, Is.GreaterThan(0));
        }

        [Test]
        public void CanExecuteViews()
        {
            api.Views.GetAllViews();
            //var res = api.Views.ExecuteView(views.Views.First().Id);

            //id for all unsolved tickets
            var res = api.Views.ExecuteView(31559032);

            Assert.That(res.Rows.Count, Is.GreaterThan(0));
            Assert.That(res.Columns.Count, Is.GreaterThan(0));
        }

        [Test]
        public void CanExecutePagedView()
        {
            var res = api.Views.ExecuteView(Settings.ViewId, "", true, 25, 2);

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
                    All = new List<All> {new All {Field = "status", Value = "open", Operator = "is"}},
                    Output = new PreviewViewOutput { Columns = new List<string> { "subject" } }
                }
            };

            var previewRes = api.Views.PreviewView(preview);
            Assert.That(previewRes.Rows.Count, Is.GreaterThan(0));
            Assert.That(previewRes.Columns.Count, Is.GreaterThan(0));
        }

        [Test]
        public void CanGetViewCounts()
        {
            var views = api.Views.GetAllViews();
            var res = api.Views.GetViewCounts(new List<long>() { views.Views[0].Id });
            Assert.That(res.ViewCounts.Count, Is.GreaterThan(0));

            Assert.That(views.Count, Is.GreaterThan(0));
        }

        [Test]
        public void CanGetViewCount()
        {
            var views = api.Views.GetAllViews();
            var id = views.Views[0].Id;
            var res = api.Views.GetViewCount(id);

            Assert.That(res.ViewCount.ViewId, Is.EqualTo(id));
        }
    }
}
