#if ASYNC
using System.Threading.Tasks;
#endif
using ZendeskApi_v2.Models.Shared;

namespace ZendeskApi_v2.Requests
{
	public interface IJobStatuses : ICore
	{
#if SYNC
		JobStatusResponse GetJobStatus(string id);
#endif

#if ASYNC
		Task<JobStatusResponse> GetJobStatusAsync(string id);
#endif
	}

	public class JobStatuses : Core, IJobStatuses
	{

        public JobStatuses(string yourZendeskUrl, string user, string password, string apiToken, string p_OAuthToken, string customHeaderName, string customHeaderValue)
            : base(yourZendeskUrl, user, password, apiToken, p_OAuthToken, customHeaderName, customHeaderValue)
        {
        }

#if SYNC
        public JobStatusResponse GetJobStatus(string id)
        {
            return GenericGet<JobStatusResponse>($"job_statuses/{id}.json");
        }
#endif

#if ASYNC
        public async Task<JobStatusResponse> GetJobStatusAsync(string id)
        {
            return await GenericGetAsync<JobStatusResponse>($"job_statuses/{id}.json");
        }
#endif
    }
}
