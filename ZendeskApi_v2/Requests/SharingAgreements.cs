using ZendeskApi_v2.Models.SharingAgreements;

namespace ZendeskApi_v2.Requests
{
    public class SharingAgreements : Core
    {

        public SharingAgreements(string yourZendeskUrl, string user, string password)
            : base(yourZendeskUrl, user, password)
        {
        }

        public GroupSharingAgreementResponse GetSharingAgreements()
        {
            return GenericGet<GroupSharingAgreementResponse>("sharing_agreements.json");
        }
    }
}