using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Voice
{
    public class AccountOverview
    {
        /// <summary>
        /// Average time of call across all calls
        /// </summary>
        [JsonProperty("average_call_duration")]
        public long AverageCallDuration { get; set; }

        /// <summary>
        /// Average time caller spent in queue waiting to be routed to an agent
        /// </summary>
        [JsonProperty("average_queue_wait_time")]
        public long AverageQueueWaitTime { get; set; }

        /// <summary>
        /// Average wrap-up time across all calls
        /// </summary>
        [JsonProperty("average_wrap_up_time")]
        public long AverageWarpUpTime { get; set; }

        /// <summary>
        /// Maximum number of calls waiting for an agent in the queue, including caller on the line and callback requests
        /// </summary>
        [JsonProperty("max_calls_waiting")]
        public long MaxCallsWaiting { get; set; }

        /// <summary>
        /// Maximum time caller spent in queue waiting to be routed to an agent
        /// </summary>
        [JsonProperty("max_queue_wait_time")]
        public long MaxQueueWaitTime { get; set; }

        /// <summary>
        /// Total duration of all calls
        /// </summary>
        [JsonProperty("total_call_duration")]
        public long TotalCallDuration { get; set; }

        /// <summary>
        /// Total number of inbound and outbound calls
        /// </summary>
        [JsonProperty("total_calls")]
        public long TotalCalls { get; set; }

        /// <summary>
        /// Total number of calls that went to voicemail for any reason
        /// </summary>
        [JsonProperty("total_voicemails")]
        public long TotalVoicemails { get; set; }

        /// <summary>
        /// Total wrap-up time across all calls
        /// </summary>
        [JsonProperty("total_wrap_up_time")]
        public long TotalWrapUpTime { get; set; }

        /// <summary>
        /// <para>Only available in Talk Professional plan and higher</para>
        /// <para>Average callback time a customer has been waiting for an agent in the queue. Excludes Available agents greeting</para>
        /// </summary>
        [JsonProperty("average_callback_wait_time")]
        public long AverageCallbackWaitTime { get; set; }

        /// <summary>
        /// <para>Only available in Talk Professional plan and higher</para>
        /// <para>Average time caller spent on hold per call</para>
        /// </summary>
        [JsonProperty("average_hold_time")]
        public long AverageHoldTime { get; set; }

        /// <summary>
        /// <para>Only available in Talk Professional plan and higher</para>
        /// Average time between system answering a call and customer being connected with an agent. Includes greetings and other recordings played
        /// </summary>
        [JsonProperty("average_time_to_answer")]
        public long AverageTimeToAnswer { get; set; }

        /// <summary>
        /// <para>Only available in Talk Professional plan and higher</para>
        /// Total number of callback requests (successful or not)
        /// </summary>
        [JsonProperty("total_callback_calls")]
        public long TotalCallbackCalls { get; set; }

        /// <summary>
        /// <para>Only available in Talk Professional plan and higher</para>
        /// Total number of calls where customer hung up while waiting in the queue
        /// </summary>
        [JsonProperty("total_calls_abandoned_in_queue")]
        public long TotalCallsAbandonedInQueue { get; set; }

        /// <summary>
        /// <para>Only available in Talk Professional plan and higher</para>
        /// Total number of calls received outside business hours
        /// </summary>
        [JsonProperty("total_calls_outside_business_hours")]
        public long TotalCallsOutsideBusinessHours { get; set; }

        /// <summary>
        /// <para>Only available in Talk Professional plan and higher</para>
        /// Total number of calls sent to voicemail after exceeding the max wait time in the queue
        /// </summary>
        [JsonProperty("total_calls_with_exceeded_queue_wait_time")]
        public long TotalCallsWithExceededQueueWaitTime { get; set; }

        /// <summary>
        /// <para>Only available in Talk Professional plan and higher</para>
        /// Total number of calls where customer requested to be put through to voicemail by dialing 1
        /// </summary>
        [JsonProperty("total_calls_with_requested_voicemail")]
        public long TotalCallsWithRequestedVoicemail { get; set; }

        /// <summary>
        /// <para>Only available in Talk Professional plan and higher</para>
        /// Total hold time across all calls
        /// </summary>
        [JsonProperty("total_hold_time")]
        public long TotalHoldTime { get; set; }

        /// <summary>
        /// <para>Only available in Talk Professional plan and higher</para>
        /// Total number of inbound calls
        /// </summary>
        [JsonProperty("total_inbound_calls")]
        public long TotalInboundCalls { get; set; }

        /// <summary>
        /// <para>Only available in Talk Professional plan and higher</para>
        /// Total number of outbound calls
        /// </summary>
        [JsonProperty("total_outbound_calls")]
        public long TotalOutboundCalls { get; set; }

        /// <summary>
        /// <para>Only available in Talk Professional plan and higher</para>
        /// Total number of textback messages sent from IVR
        /// </summary>
        [JsonProperty("total_textback_requests")]
        public long TotalTextbackRequests { get; set; }

        /// <summary>
        /// <para>Only available in Talk Professional plan and higher</para>
        /// Total number of callback calls requested via Web Widget (successful or not)
        /// </summary>
        [JsonProperty("total_embeddable_callback_calls")]
        public long TotalEmbeddableCallbackCalls { get; set; }
    }
}
