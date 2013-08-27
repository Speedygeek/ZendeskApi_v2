#if ASYNC
using System.Threading.Tasks;
#endif
using ZendeskApi_v2.Models.Shared;

namespace ZendeskApi_v2.Requests
{
    public class JobStatuses : Core
    {

        internal JobStatuses(IZendeskConnectionSettings connectionSettings)
            : base(connectionSettings)
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