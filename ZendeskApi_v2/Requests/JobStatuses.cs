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

        public JobStatuses(string yourZendeskUrl, string user, string password, string apiToken)
            : base(yourZendeskUrl, user, password, apiToken)
        {
        }

#if SYNC
        public JobStatusResponse GetJobStatus(string id)
        {
            return GenericGet<JobStatusResponse>(string.Format("job_statuses/{0}.json", id));
        }
#endif

#if ASYNC
        public async Task<JobStatusResponse> GetJobStatusAsync(string id)
        {
            return await GenericGetAsync<JobStatusResponse>(string.Format("job_statuses/{0}.json", id));
        }
#endif
    }
}