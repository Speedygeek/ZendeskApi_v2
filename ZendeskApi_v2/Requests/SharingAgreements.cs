using ZendeskApi_v2.Models.SharingAgreements;

namespace ZenDeskApi_v2.Requests
{
    public class SharingAgreements : Core
    {

        public SharingAgreements(string yourZenDeskUrl, string user, string password)
            : base(yourZenDeskUrl, user, password)
        {
        }

        public GroupSharingAgreementResponse GetSharingAgreements()
        {
            return GenericGet<GroupSharingAgreementResponse>("sharing_agreements.json");
        }
    }
}