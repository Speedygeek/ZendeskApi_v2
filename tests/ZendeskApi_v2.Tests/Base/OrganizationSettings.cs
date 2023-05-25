namespace ZendeskApi_v2.Tests.Base;

public class OrganizationSettings
{
    public long ID { get; set; }
    public string Name { get; set; }
    public string SiteURL { get; set; } = "https://csharpapi.zendesk.com/api/v2/";
    public string ExternalID { get; set; }
}
