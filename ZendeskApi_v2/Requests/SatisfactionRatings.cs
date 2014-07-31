#if ASYNC
using System.Threading.Tasks;
#endif
using ZendeskApi_v2.Models.Satisfaction;

namespace ZendeskApi_v2.Requests
{
	public interface ISatisfactionRatings : ICore
	{
#if SYNC
		/// <summary>
		/// Lists all received satisfaction rating requests ever issued for your account. To only list the satisfaction ratings submitted by your customers, use the "received" end point below instead.
		/// </summary>
		/// <returns></returns>
		GroupSatisfactionResponse GetSatisfactionRatings();

		/// <summary>
		/// Lists ratings provided by customers.
		/// </summary>
		/// <returns></returns>
		GroupSatisfactionResponse GetReceivedSatisfactionRatings();

		IndividualSatisfactionResponse GetSatisfactionRatingById(long id);
#endif

#if ASYNC
		/// <summary>
		/// Lists all received satisfaction rating requests ever issued for your account. To only list the satisfaction ratings submitted by your customers, use the "received" end point below instead.
		/// </summary>
		/// <returns></returns>
		Task<GroupSatisfactionResponse> GetSatisfactionRatingsAsync();

		/// <summary>
		/// Lists ratings provided by customers.
		/// </summary>
		/// <returns></returns>
		Task<GroupSatisfactionResponse> GetReceivedSatisfactionRatingsAsync();

		Task<IndividualSatisfactionResponse> GetSatisfactionRatingByIdAsync(long id);
#endif
	}

	public class SatisfactionRatings : Core, ISatisfactionRatings
	{
        public SatisfactionRatings(string yourZendeskUrl, string user, string password, string apiToken)
            : base(yourZendeskUrl, user, password, apiToken)
        {
        }

#if SYNC
        /// <summary>
        /// Lists all received satisfaction rating requests ever issued for your account. To only list the satisfaction ratings submitted by your customers, use the "received" end point below instead.
        /// </summary>
        /// <returns></returns>
        public GroupSatisfactionResponse GetSatisfactionRatings()
        {
            return GenericGet<GroupSatisfactionResponse>(string.Format("satisfaction_ratings.json"));
        }

        /// <summary>
        /// Lists ratings provided by customers.
        /// </summary>
        /// <returns></returns>
        public GroupSatisfactionResponse GetReceivedSatisfactionRatings()
        {
            return GenericGet<GroupSatisfactionResponse>(string.Format("satisfaction_ratings/received.json"));
        }

        public IndividualSatisfactionResponse GetSatisfactionRatingById(long id)
        {
            return GenericGet<IndividualSatisfactionResponse>(string.Format("satisfaction_ratings/{0}.json", id));
        }
#endif

#if ASYNC
        /// <summary>
        /// Lists all received satisfaction rating requests ever issued for your account. To only list the satisfaction ratings submitted by your customers, use the "received" end point below instead.
        /// </summary>
        /// <returns></returns>
        public async Task<GroupSatisfactionResponse> GetSatisfactionRatingsAsync()
        {
            return await GenericGetAsync<GroupSatisfactionResponse>(string.Format("satisfaction_ratings.json"));
        }

        /// <summary>
        /// Lists ratings provided by customers.
        /// </summary>
        /// <returns></returns>
        public async Task<GroupSatisfactionResponse> GetReceivedSatisfactionRatingsAsync()
        {
            return await GenericGetAsync<GroupSatisfactionResponse>(string.Format("satisfaction_ratings/received.json"));
        }

        public async Task<IndividualSatisfactionResponse> GetSatisfactionRatingByIdAsync(long id)
        {
            return await GenericGetAsync<IndividualSatisfactionResponse>(string.Format("satisfaction_ratings/{0}.json", id));
        }
#endif
    }
}