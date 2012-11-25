using System.Net;

namespace ZenDeskApi_v2
{
    public class RequestResult
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public string Content { get; set; }
    }
}