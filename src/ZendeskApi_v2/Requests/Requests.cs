using System;
using System.Collections.Generic;

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
        /// <summary>
        /// Gets all requests available to user.
        /// </summary>
        /// <param name="perPage">Number of results per page. Must be between 1 and 100.</param>
        /// <param name="page">Page to get results for. Must be greater than 0.</param>
        /// <param name="sortCol">Column to sort by. Only "updated_at" and "created_at" are supported by API.</param>
        /// <param name="sortAscending">Whether or not to sort ascending. API defaults to true.</param>
        /// <returns>GroupRequestResponse</returns>
        GroupRequestResponse GetAllRequests(int? perPage = null, int? page = null, string sortCol = null, bool? sortAscending = null);

        /// <summary>
        /// Gets all open requests available to user.
        /// </summary>
        /// <param name="perPage">Number of results per page. Must be between 1 and 100.</param>
        /// <param name="page">Page to get results for. Must be greater than 0.</param>
        /// <param name="sortCol">Column to sort by. Only "updated_at" and "created_at" are supported by API.</param>
        /// <param name="sortAscending">Whether or not to sort ascending. API defaults to true.</param>
        /// <returns>GroupRequestResponse</returns>
        GroupRequestResponse GetAllOpenRequests(int? perPage = null, int? page = null, string sortCol = null, bool? sortAscending = null);

        /// <summary>
        /// Gets all solved (including closed) requests available to user.
        /// </summary>
        /// <param name="perPage">Number of results per page. Must be between 1 and 100.</param>
        /// <param name="page">Page to get results for. Must be greater than 0.</param>
        /// <param name="sortCol">Column to sort by. Only "updated_at" and "created_at" are supported by API.</param>
        /// <param name="sortAscending">Whether or not to sort ascending. API defaults to true.</param>
        /// <returns>GroupRequestResponse</returns>
        GroupRequestResponse GetAllSolvedRequests(int? perPage = null, int? page = null, string sortCol = null, bool? sortAscending = null);

        /// <summary>
        /// Gets all closed requests available to user.
        /// </summary>
        /// <param name="perPage">Number of results per page. Must be between 1 and 100.</param>
        /// <param name="page">Page to get results for. Must be greater than 0.</param>
        /// <param name="sortCol">Column to sort by. Only "updated_at" and "created_at" are supported by API.</param>
        /// <param name="sortAscending">Whether or not to sort ascending. API defaults to true.</param>
        /// <returns>GroupRequestResponse</returns>
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
        /// <summary>
        /// Gets all requests available to user.
        /// </summary>
        /// <param name="perPage">Number of results per page. Must be between 1 and 100.</param>
        /// <param name="page">Page to get results for. Must be greater than 0.</param>
        /// <param name="sortCol">Column to sort by. Only "updated_at" and "created_at" are supported by API.</param>
        /// <param name="sortAscending">Whether or not to sort ascending. API defaults to true.</param>
        /// <returns>GroupRequestResponse Task</returns>
        Task<GroupRequestResponse> GetAllRequestsAsync(int? perPage = null, int? page = null, string sortCol = null, bool? sortAscending = null);

        /// <summary>
        /// Gets all open requests available to user.
        /// </summary>
        /// <param name="perPage">Number of results per page. Must be between 1 and 100.</param>
        /// <param name="page">Page to get results for. Must be greater than 0.</param>
        /// <param name="sortCol">Column to sort by. Only "updated_at" and "created_at" are supported by API.</param>
        /// <param name="sortAscending">Whether or not to sort ascending. API defaults to true.</param>
        /// <returns>GroupRequestResponse Task</returns>
        Task<GroupRequestResponse> GetAllOpenRequestsAsync(int? perPage = null, int? page = null, string sortCol = null, bool? sortAscending = null);

        /// <summary>
        /// Gets all solved (including closed) requests available to user.
        /// </summary>
        /// <param name="perPage">Number of results per page. Must be between 1 and 100.</param>
        /// <param name="page">Page to get results for. Must be greater than 0.</param>
        /// <param name="sortCol">Column to sort by. Only "updated_at" and "created_at" are supported by API.</param>
        /// <param name="sortAscending">Whether or not to sort ascending. API defaults to true.</param>
        /// <returns>GroupRequestResponse Task</returns>
        Task<GroupRequestResponse> GetAllSolvedRequestsAsync(int? perPage = null, int? page = null, string sortCol = null, bool? sortAscending = null);

        /// <summary>
        /// Gets all closed requests available to user.
        /// </summary>
        /// <param name="perPage">Number of results per page. Must be between 1 and 100.</param>
        /// <param name="page">Page to get results for. Must be greater than 0.</param>
        /// <param name="sortCol">Column to sort by. Only "updated_at" and "created_at" are supported by API.</param>
        /// <param name="sortAscending">Whether or not to sort ascending. API defaults to true.</param>
        /// <returns>GroupRequestResponse Task</returns>
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
        public Requests(string yourZendeskUrl, string user, string password, string apiToken, string p_OAuthToken, Dictionary<string,string> customHeaders)
            : base(yourZendeskUrl, user, password, apiToken, p_OAuthToken, customHeaders)
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
            return GenericGet<GroupRequestResponse>($"users/{id}/requests.json");
        }

        public  IndividualRequestResponse GetRequestById(long id)
        {
            return GenericGet<IndividualRequestResponse>($"requests/{id}.json");
        }

        public GroupCommentResponse GetRequestCommentsById(long id)
        {
            return GenericGet<GroupCommentResponse>($"requests/{id}/comments.json");
        }

        public IndividualCommentResponse GetSpecificRequestComment(long requestId, long commentId)
        {
            return GenericGet<IndividualCommentResponse>($"requests/{requestId}/comments/{commentId}.json");
        }

        public IndividualRequestResponse CreateRequest(Request request)
        {
            var body = new {request};
            return GenericPost<IndividualRequestResponse>("requests.json", body);
        }

        public IndividualRequestResponse UpdateRequest(long id, Comment comment)
        {
            var request = new Request
            {
                Id = id,
                Comment = comment
            };

            return UpdateRequest(request);
        }

        public IndividualRequestResponse UpdateRequest(Request request, Comment comment=null)
        {
            if (!request.Id.HasValue) { throw new ArgumentException("request must have Id set."); }

            if (comment != null)
            {
                request.Comment = comment;
            }

            var body = new { request };
            
            return GenericPut<IndividualRequestResponse>($"requests/{request.Id.Value}.json", body);
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
            return await GenericGetAsync<GroupRequestResponse>($"users/{id}/requests.json");
        }

        public async Task<IndividualRequestResponse> GetRequestByIdAsync(long id)
        {
            return await GenericGetAsync<IndividualRequestResponse>($"requests/{id}.json");
        }

        public async Task<GroupCommentResponse> GetRequestCommentsByIdAsync(long id)
        {
            return await GenericGetAsync<GroupCommentResponse>($"requests/{id}/comments.json");
        }

        public async Task<IndividualCommentResponse> GetSpecificRequestCommentAsync(long requestId, long commentId)
        {
            return await GenericGetAsync<IndividualCommentResponse>($"requests/{requestId}/comments/{commentId}.json");
        }

        public async Task<IndividualRequestResponse> CreateRequestAsync(Request request)
        {
            var body = new {request};
            return await GenericPostAsync<IndividualRequestResponse>("requests.json", body);
        }

        public async Task<IndividualRequestResponse> UpdateRequestAsync(long id, Comment comment)
        {
            var body = new { request = new { comment} };
            return await GenericPutAsync<IndividualRequestResponse>($"requests/{id}.json", body);
        }
#endif
    }
}
