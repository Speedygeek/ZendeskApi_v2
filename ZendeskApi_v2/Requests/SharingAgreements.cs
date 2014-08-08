#if ASYNC
using System.Threading.Tasks;
#endif
using ZendeskApi_v2.Models.SharingAgreements;

namespace ZendeskApi_v2.Requests
{
	public interface ISharingAgreements : ICore
	{
#if SYNC
		GroupSharingAgreementResponse GetSharingAgreements();
#endif

#if ASYNC
		Task<GroupSharingAgreementResponse> GetSharingAgreementsAsync();
#endif
	}

	public class SharingAgreements : Core, ISharingAgreements
	{

        public SharingAgreements(string yourZendeskUrl, string user, string password, string apiToken)
            : base(yourZendeskUrl, user, password, apiToken)
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