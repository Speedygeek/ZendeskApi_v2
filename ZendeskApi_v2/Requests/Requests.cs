#if ASYNC
using System.Threading.Tasks;
#endif
using ZendeskApi_v2.Models.Requests;
using ZendeskApi_v2.Models.Tickets;

namespace ZendeskApi_v2.Requests
{
    public interface IRequests : ICore
    {
#if SYNC
        GroupRequestResponse GetAllRequests(int? perPage = null, int? page = null, string sortCol = null, bool? sortAscending = null);
        GroupRequestResponse GetAllOpenRequests(int? perPage = null, int? page = null, string sortCol = null, bool? sortAscending = null);
        GroupRequestResponse GetAllSolvedRequests(int? perPage = null, int? page = null, string sortCol = null, bool? sortAscending = null);
        GroupRequestResponse GetAllCcdRequests(int? perPage = null, int? page = null, string sortCol = null, bool? sortAscending = null);
        GroupRequestResponse GetAllRequestsForUser(long id);
        IndividualRequestResponse GetRequestById(long id);
        GroupCommentResponse GetRequestCommentsById(long id);
        IndividualCommentResponse GetSpecificRequestComment(long requestId, long commentId);
        IndividualRequestResponse CreateRequest(Request request);
        IndividualRequestResponse UpdateRequest(long id, Comment comment);
        IndividualRequestResponse UpdateRequest(Request request, Comment comment = null);
#endif

#if ASYNC
        Task<GroupRequestResponse> GetAllRequestsAsync(int? perPage = null, int? page = null, string sortCol = null, bool? sortAscending = null);
        Task<GroupRequestResponse> GetAllOpenRequestsAsync(int? perPage = null, int? page = null, string sortCol = null, bool? sortAscending = null);
        Task<GroupRequestResponse> GetAllSolvedRequestsAsync(int? perPage = null, int? page = null, string sortCol = null, bool? sortAscending = null);
        Task<GroupRequestResponse> GetAllCcdRequestsAsync(int? perPage = null, int? page = null, string sortCol = null, bool? sortAscending = null);
        Task<GroupRequestResponse> GetAllRequestsForUserAsync(long id);
        Task<IndividualRequestResponse> GetRequestByIdAsync(long id);
        Task<GroupCommentResponse> GetRequestCommentsByIdAsync(long id);
        Task<IndividualCommentResponse> GetSpecificRequestCommentAsync(long requestId, long commentId);
        Task<IndividualRequestResponse> CreateRequestAsync(Request request);
        Task<IndividualRequestResponse> UpdateRequestAsync(long id, Comment comment);
#endif
    }

    public class Requests : Core, IRequests
    {
        public Requests(string yourZendeskUrl, string user, string password, string apiToken, string p_OAuthToken)
            : base(yourZendeskUrl, user, password, apiToken, p_OAuthToken)
        {
        }

#if SYNC
        public GroupRequestResponse GetAllRequests(int? perPage = null, int? page = null, string sortCol = null, bool? sortAscending = null)
        {
            return GenericPagedSortedGet<GroupRequestResponse>("requests.json", perPage, page, sortCol, sortAscending);
        }

        public GroupRequestResponse GetAllOpenRequests(int? perPage = null, int? page = null, string sortCol = null, bool? sortAscending = null)
        {
            return GenericPagedSortedGet<GroupRequestResponse>("requests/open.json", perPage, page, sortCol, sortAscending);
        }

        public GroupRequestResponse GetAllSolvedRequests(int? perPage = null, int? page = null, string sortCol = null, bool? sortAscending = null)
        {
            return GenericPagedSortedGet<GroupRequestResponse>("requests/solved.json", perPage, page, sortCol, sortAscending);
        }

        public GroupRequestResponse GetAllCcdRequests(int? perPage = null, int? page = null, string sortCol = null, bool? sortAscending = null)
        {
            return GenericPagedSortedGet<GroupRequestResponse>("requests/ccd.json", perPage, page, sortCol, sortAscending);
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
        public async Task<GroupRequestResponse> GetAllRequestsAsync(int? perPage = null, int? page = null, string sortCol = null, bool? sortAscending = null)
        {
            return await GenericPagedSortedGetAsync<GroupRequestResponse>("requests.json", perPage, page, sortCol, sortAscending);
        }

        public async Task<GroupRequestResponse> GetAllOpenRequestsAsync(int? perPage = null, int? page = null, string sortCol = null, bool? sortAscending = null)
        {
            return await GenericPagedSortedGetAsync<GroupRequestResponse>("requests/open.json", perPage, page, sortCol, sortAscending);
        }

        public async Task<GroupRequestResponse> GetAllSolvedRequestsAsync(int? perPage = null, int? page = null, string sortCol = null, bool? sortAscending = null)
        {
            return await GenericPagedSortedGetAsync<GroupRequestResponse>("requests/solved.json", perPage, page, sortCol, sortAscending);
        }

        public async Task<GroupRequestResponse> GetAllCcdRequestsAsync(int? perPage = null, int? page = null, string sortCol = null, bool? sortAscending = null)
        {
            return await GenericPagedSortedGetAsync<GroupRequestResponse>("requests/ccd.json", perPage, page, sortCol, sortAscending);
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