#if ASYNC
using System.Threading.Tasks;
#endif
using ZendeskApi_v2.Models.SharingAgreements;

namespace ZendeskApi_v2.Requests
{
    public class SharingAgreements : Core
    {

        internal SharingAgreements(IZendeskConnectionSettings connectionSettings)
            : base(connectionSettings)
        {
        }

#if SYNC
        public GroupSharingAgreementResponse GetSharingAgreements()
        {
            return GenericGet<GroupSharingAgreementResponse>("sharing_agreements.json");
        }
#endif

#if ASYNC
        public async Task<GroupSharingAgreementResponse> GetSharingAgreementsAsync()
        {
            return await GenericGetAsync<GroupSharingAgreementResponse>("sharing_agreements.json");
        }
#endif
    }
}