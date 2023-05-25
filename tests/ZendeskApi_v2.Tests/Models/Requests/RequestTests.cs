using Newtonsoft.Json;
using NUnit.Framework;
using ZendeskApi_v2.Models.Requests;

namespace ZendeskApi_v2.Tests.Models.Requests;

[TestFixture]
public class RequestTests
{
    private const string RequestJsonFormat = "{{\"url\":\"{0}\",\"id\":{1},\"status\":\"{2}\",\"priority\":null,\"type\":null,\"subject\":\"{3}\",\"description\":\"{4}\",\"organization_id\":{5},\"via\":{6},\"custom_fields\":{7},\"requester_id\":{8},\"collaborator_ids\":[],\"due_at\":null,\"can_be_solved_by_me\":{9},\"created_at\":\"2015-11-09T19:38:40Z\",\"updated_at\":\"2015-11-11T20:02:27Z\",\"assignee_id\":5,\"ticket_form_id\":6,\"fields\":[{{\"id\":2,\"value\":\"custom_1\"}}]}}";
    private const string ToJson = "{}";
    private const string FromJson = "{}";
    private const string SourceJsonFormat = "{{\"from\":{0},\"to\":{1},\"rel\":null}}";
    private const string ViaJsonFormat = "{{\"channel\":\"Api\",\"source\":{0}}}";
    private const string CustomFieldsJsonFormat = "[{{\"id\":{0},\"value\":\"{1}\"}}]";
    private const long Id = 1;
    private const string UrlFormat = "https://example.zendesk.com/Api/v2/requests/{0}.json";
    private const string OpenStatus = "open";
    private const string SolvedStatus = "solved";
    private const string Subject = "Example Subject";
    private const string Description = "Example Description";
    private const long OrganizationId = 2005;
    private const long FieldId = 1;
    private const string FieldValue = "custom_field_value";
    private const int RequesterId = 10000;
    private const bool OpenCanBeSolvedByMe = true;
    private const bool SolvedCanBeSolvedByMe = false;

    private static readonly string Url = string.Format(UrlFormat, Id);
    private static readonly string SourceJson = string.Format(SourceJsonFormat, FromJson, ToJson);
    private static readonly string ViaJson = string.Format(ViaJsonFormat, SourceJson);
    private static readonly string CustomFieldsJson = string.Format(CustomFieldsJsonFormat, FieldId, FieldValue);

    private static readonly string OpenRequestJson = string.Format(RequestJsonFormat, Url, Id, OpenStatus, Subject, Description, OrganizationId, ViaJson, CustomFieldsJson, RequesterId, OpenCanBeSolvedByMe.ToString().ToLower());
    private static readonly string SolvedRequestJson = string.Format(RequestJsonFormat, Url, Id, SolvedStatus, Subject, Description, OrganizationId, ViaJson, CustomFieldsJson, RequesterId, SolvedCanBeSolvedByMe.ToString().ToLower());

    [Test]
    public void TestDeserialize()
    {
        var openRequest = JsonConvert.DeserializeObject<Request>(OpenRequestJson);

        Assert.That(openRequest, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(openRequest.Url, Is.EqualTo(Url));
            Assert.That(openRequest.Id, Is.EqualTo(Id));
            Assert.That(openRequest.Status, Is.EqualTo(OpenStatus));
            Assert.That(openRequest.Subject, Is.EqualTo(Subject));
            Assert.That(openRequest.Description, Is.EqualTo(Description));
            Assert.That(openRequest.RequesterId, Is.EqualTo(RequesterId));
            Assert.That(openRequest.CanBeSolvedByMe, Is.EqualTo(OpenCanBeSolvedByMe));
        });
        var solvedRequest = JsonConvert.DeserializeObject<Request>(SolvedRequestJson);

        Assert.That(solvedRequest, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(solvedRequest.Status, Is.EqualTo(SolvedStatus));
            Assert.That(solvedRequest.CanBeSolvedByMe, Is.EqualTo(SolvedCanBeSolvedByMe));
        });
    }
}
