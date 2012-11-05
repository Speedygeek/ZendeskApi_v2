using System.Linq;
using NUnit.Framework;
using ZenDeskApi_v2;
using ZenDeskApi_v2.Models.Requests;
using ZenDeskApi_v2.Models.Tickets;

namespace Tests
{
    [TestFixture]
    public class RequestTests
    {
        private ZenDeskApi api = new ZenDeskApi(Settings.Site, Settings.Email, Settings.Password);

        [Test]
        public void CanGetRequests()
        {
            var res = api.Requests.GetAllRequests();
            Assert.True(res.Count > 0);
        }        

        [Test]
        public void CanCreateAndUpdateRequests()
        {
            var req = new Request()
                          {
                              Subject = "end user request test",
                              Comment = new Comment() {Body = "end user test"}
                          };


            var res = api.Requests.CreateRequest(req);
            Assert.True(res.Request.Id > 0);

            var res1 = api.Requests.GetRequestById(res.Request.Id.Value);
            Assert.AreEqual(res1.Request.Id, res.Request.Id);

            var res2 = api.Requests.UpdateRequest(res.Request.Id.Value, new Comment() {Body = "something more to say"});
            var res3 = api.Requests.GetRequestCommentsById(res.Request.Id.Value);

            Assert.AreEqual(res3.Comments.Last().Body.Replace("\n", ""), "something more to say");

            var res4 = api.Requests.GetSpecificRequestComment(res.Request.Id.Value, res3.Comments.Last().Id.Value);
            Assert.AreEqual(res4.Comment.Id, res3.Comments.Last().Id);

            Assert.True(api.Tickets.Delete(res1.Request.Id.Value));
        }
    }
}