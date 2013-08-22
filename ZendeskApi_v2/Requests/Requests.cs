#if ASYNC
using System.Threading.Tasks;
#endif
using ZendeskApi_v2.Models.Requests;
using ZendeskApi_v2.Models.Tickets;

namespace ZendeskApi_v2.Requests
{
    public class Requests : Core
    {
        public Requests(string yourZendeskUrl, string user, string password)
            : base(yourZendeskUrl, user, password)
        {
        }

#if SYNC
        public GroupRequestResponse GetAllRequests()
        {
            return GenericGet<GroupRequestResponse>("requests.json");
        }

        public GroupRequestResponse GetAllSolvedRequests()
        {
            return GenericGet<GroupRequestResponse>("requests/solved.json");
        }

        public GroupRequestResponse GetAllCcdRequests()
        {
            return GenericGet<GroupRequestResponse>("requests/ccd.json");
        }

        public GroupRequestResponse GetAllRequestsForUser(long id)
        {
            return GenericGet<GroupRequestResponse>(string.Format("users/{0}/requests.json", id));
        }

        public  IndividualRequestResponse GetRequestById(long id)
        {
            return GenericGet<IndividualRequestResponse>(string.Format("requests/{0}.json", id));
        }

        public GroupCommentResponse GetRequestCommentsById(long id)
        {
            return GenericGet<GroupCommentResponse>(string.Format("requests/{0}/comments.json", id));
        }

        public IndividualCommentResponse GetSpecificRequestComment(long requestId, long commentId)
        {
            return GenericGet<IndividualCommentResponse>(string.Format("requests/{0}/comments/{1}.json", requestId, commentId));
        }

        public IndividualRequestResponse CreateRequest(Request request)
        {
            var body = new {request};
            return GenericPost<IndividualRequestResponse>("requests.json", body);
        }

        public IndividualRequestResponse UpdateRequest(long id, Comment comment)
        {
            var body = new { request = new { comment} };
            return GenericPut<IndividualRequestResponse>(string.Format("requests/{0}.json", id), body);
        }

        public IndividualRequestResponse UpdateRequest(Request request, Comment comment=null)
        {
            if (comment != null)
                request.Comment = comment;
            var body = new { request };
            
            return GenericPut<IndividualRequestResponse>(string.Format("requests/{0}.json", request.Id.Value), body);
        }
#endif

#if ASYNC
        public async Task<GroupRequestResponse> GetAllRequestsAsync()
        {
            return await GenericGetAsync<GroupRequestResponse>("requests.json");
        }

        public async Task<GroupRequestResponse> GetAllSolvedRequestsAsync()
        {
            return await GenericGetAsync<GroupRequestResponse>("requests/solved.json");
        }

        public async Task<GroupRequestResponse> GetAllCcdRequestsAsync()
        {
            return await GenericGetAsync<GroupRequestResponse>("requests/ccd.json");
        }

        public async Task<GroupRequestResponse> GetAllRequestsForUserAsync(long id)
        {
            return await GenericGetAsync<GroupRequestResponse>(string.Format("users/{0}/requests.json", id));
        }

        public async Task<IndividualRequestResponse> GetRequestByIdAsync(long id)
        {
            return await GenericGetAsync<IndividualRequestResponse>(string.Format("requests/{0}.json", id));
        }

        public async Task<GroupCommentResponse> GetRequestCommentsByIdAsync(long id)
        {
            return await GenericGetAsync<GroupCommentResponse>(string.Format("requests/{0}/comments.json", id));
        }

        public async Task<IndividualCommentResponse> GetSpecificRequestCommentAsync(long requestId, long commentId)
        {
            return await GenericGetAsync<IndividualCommentResponse>(string.Format("requests/{0}/comments/{1}.json", requestId, commentId));
        }

        public async Task<IndividualRequestResponse> CreateRequestAsync(Request request)
        {
            var body = new {request};
            return await GenericPostAsync<IndividualRequestResponse>("requests.json", body);
        }

        public async Task<IndividualRequestResponse> UpdateRequestAsync(long id, Comment comment)
        {
            var body = new { request = new { comment} };
            return await GenericPutAsync<IndividualRequestResponse>(string.Format("requests/{0}.json", id), body);
        }
#endif
    }
}