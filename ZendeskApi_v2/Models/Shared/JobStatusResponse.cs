using Newtonsoft.Json;

namespace ZenDeskApi_v2.Models.Shared
{
    public  class JobStatusResponse
    {
        [JsonProperty("job_status")]
        public JobStatus JobStatus { get; set; }
    }
}