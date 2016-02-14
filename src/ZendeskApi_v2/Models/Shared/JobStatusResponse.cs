using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Shared
{
    public  class JobStatusResponse
    {
        [JsonProperty("job_status")]
        public JobStatus JobStatus { get; set; }
    }
}