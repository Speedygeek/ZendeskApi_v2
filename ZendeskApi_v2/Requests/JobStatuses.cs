using ZenDeskApi_v2.Models.Shared;

namespace ZenDeskApi_v2.Requests
{
    public class JobStatuses : Core
    {

        public JobStatuses(string yourZenDeskUrl, string user, string password)
            : base(yourZenDeskUrl, user, password)
        {
        }

        public JobStatusResponse GetJobStatus(string id)
        {
            return GenericGet<JobStatusResponse>(string.Format("job_statuses/{0}.json", id));
        }

    }
}