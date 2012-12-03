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

#if NotNet35
        public async Task<GroupSharingAgreementResponse> GetSharingAgreementsAsync()
        {
            return await GenericGetAsync<GroupSharingAgreementResponse>("sharing_agreements.json");
        }
#endif
    }
}