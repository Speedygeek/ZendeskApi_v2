using System.Net;

namespace ZendeskApi_v2
{
    public class RequestResult
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public string Content { get; set; }
    }
}