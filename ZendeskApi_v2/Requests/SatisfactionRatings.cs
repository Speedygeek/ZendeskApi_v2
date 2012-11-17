using ZenDeskApi_v2.Models.Satisfaction;

namespace ZenDeskApi_v2.Requests
{
    public class SatisfactionRatings : Core
    {
        public SatisfactionRatings(string yourZenDeskUrl, string user, string password)
            : base(yourZenDeskUrl, user, password)
        {
        }

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
    }
}