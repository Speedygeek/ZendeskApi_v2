
using System;
using System.Linq;
using ZendeskApi_v2.Models;
using ZendeskApi_v2.Models.Groups;
using ZendeskApi_v2.Models.Organizations;
#if ASYNC
using System.Threading.Tasks;
#endif
using ZendeskApi_v2.Models.Search;
using ZendeskApi_v2.Models.Tickets;
using ZendeskApi_v2.Models.Users;
using System.Collections.Generic;

namespace ZendeskApi_v2.Requests
{
    public interface ISearch : ICore
    {
#if SYNC
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="page">Returns specified {page} - pagination</param>
        /// <param name="sortBy">Possible values are 'updated_at', 'created_at', 'priority', 'status', and 'ticket_type</param>
        /// <param name="sortOrder">Possible values are 'relevance', 'asc', 'desc'. Defaults to 'relevance' when no 'order' criteria is requested.</param>
        /// <returns></returns>
        SearchResults SearchFor(string searchTerm, string sortBy = "", string sortOrder = "", int page = 1, int? perPage = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="page">Returns specified {page} - pagination</param>
        /// <param name="sortBy">Possible values are 'updated_at', 'created_at', 'priority', 'status', and 'ticket_type</param>
        /// <param name="sortOrder">Possible values are 'relevance', 'asc', 'desc'. Defaults to 'relevance' when no 'order' criteria is requested.</param>
        /// <returns></returns>
        SearchResults<T> SearchFor<T>(string searchTerm, string sortBy = "", string sortOrder = "", int page = 1, int? perPage = null) where T : ISearchable;

        /// <summary>
        /// This resource behaves the same as SearchFor, but allows anonymous users to search public forums
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="page">Returns specified {page} - pagination</param>
        /// <param name="sortBy">Possible values are 'updated_at', 'created_at', 'priority', 'status', and 'ticket_type</param>
        /// <param name="sortOrder">Possible values are 'relevance', 'asc', 'desc'. Defaults to 'relevance' when no 'order' criteria is requested.</param>
        /// <returns></returns>
        SearchResults AnonymousSearchFor(string searchTerm, string sortBy = "", string sortOrder = "", int page = 1, int? perPage = null);

        /// <summary>
        /// This resource behaves the same as SearchFor, but allows anonymous users to search public forums
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="page">Returns specified {page} - pagination</param>
        /// <param name="sortBy">Possible values are 'updated_at', 'created_at', 'priority', 'status', and 'ticket_type</param>
        /// <param name="sortOrder">Possible values are 'relevance', 'asc', 'desc'. Defaults to 'relevance' when no 'order' criteria is requested.</param>
        /// <returns></returns>
        SearchResults<T> AnonymousSearchFor<T>(string searchTerm, string sortBy = "", string sortOrder = "", int page = 1, int? perPage = null) where T : ISearchable;
#endif

#if ASYNC
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="page">Returns specified {page} - pagination</param>
        /// <param name="sortBy">Possible values are 'updated_at', 'created_at', 'priority', 'status', and 'ticket_type</param>
        /// <param name="sortOrder">Possible values are 'relevance', 'asc', 'desc'. Defaults to 'relevance' when no 'order' criteria is requested.</param>
        /// <returns></returns>
        Task<SearchResults> SearchForAsync(string searchTerm, string sortBy = "", string sortOrder = "", int page = 1, int? perPage = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="page">Returns specified {page} - pagination</param>
        /// <param name="sortBy">Possible values are 'updated_at', 'created_at', 'priority', 'status', and 'ticket_type</param>
        /// <param name="sortOrder">Possible values are 'relevance', 'asc', 'desc'. Defaults to 'relevance' when no 'order' criteria is requested.</param>
        /// <returns></returns>
        Task<SearchResults<T>> SearchForAsync<T>(string searchTerm, string sortBy = "", string sortOrder = "", int page = 1, int? perPage = null) where T : ISearchable;

        /// <summary>
        /// This resource behaves the same as SearchFor, but allows anonymous users to search public forums
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="page">Returns specified {page} - pagination</param>
        /// <param name="sortBy">Possible values are 'updated_at', 'created_at', 'priority', 'status', and 'ticket_type</param>
        /// <param name="sortOrder">Possible values are 'relevance', 'asc', 'desc'. Defaults to 'relevance' when no 'order' criteria is requested.</param>
        /// <returns></returns>
        Task<SearchResults> AnonymousSearchForAsync(string searchTerm, string sortBy = "", string sortOrder = "", int page = 1, int? perPage = null);

        /// <summary>
        /// This resource behaves the same as SearchFor, but allows anonymous users to search public forums
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="page">Returns specified {page} - pagination</param>
        /// <param name="sortBy">Possible values are 'updated_at', 'created_at', 'priority', 'status', and 'ticket_type</param>
        /// <param name="sortOrder">Possible values are 'relevance', 'asc', 'desc'. Defaults to 'relevance' when no 'order' criteria is requested.</param>
        /// <returns></returns>
        Task<SearchResults<T>> AnonymousSearchForAsync<T>(string searchTerm, string sortBy = "", string sortOrder = "", int page = 1, int? perPage = null) where T : ISearchable;
#endif
    }

    /// <summary>
    /// The search API is a unified search API that returns tickets, users, organizations, and forum topics. 
    /// Define filters to narrow your search results according to result type, date attributes, and object attributes such as ticket requester or tag.
    /// </summary>
    public class Search : Core, ISearch
    {
        public Search(string yourZendeskUrl, string user, string password, string apiToken, string p_OAuthToken, Dictionary<string,string> customHeaders)
            : base(yourZendeskUrl, user, password, apiToken, p_OAuthToken, customHeaders)
        {
        }

#if SYNC
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="page">Returns specified {page} - pagination</param>
        /// <param name="sortBy">Possible values are 'updated_at', 'created_at', 'priority', 'status', and 'ticket_type</param>
        /// <param name="sortOrder">Possible values are 'relevance', 'asc', 'desc'. Defaults to 'relevance' when no 'order' criteria is requested.</param>
        /// <returns></returns>
        public SearchResults SearchFor(string searchTerm, string sortBy = "", string sortOrder = "", int page = 1, int? perPage = null)
        {
            var resource = $"search.json?query={searchTerm}";

            return GenericPagedSortedGet<SearchResults>(resource, perPage, page, sortBy, SortOrderAscending(sortOrder));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="page">Returns specified {page} - pagination</param>
        /// <param name="sortBy">Possible values are 'updated_at', 'created_at', 'priority', 'status', and 'ticket_type</param>
        /// <param name="sortOrder">Possible values are 'relevance', 'asc', 'desc'. Defaults to 'relevance' when no 'order' criteria is requested.</param>
        /// <returns></returns>
        public SearchResults<T> SearchFor<T>(string searchTerm, string sortBy = "", string sortOrder = "", int page = 1, int? perPage = null) where T : ISearchable
        {
            var resource = $"search.json?query={AddTypeToSearchTerm<T>(searchTerm)}";

            return GenericPagedSortedGet<SearchResults<T>>(resource, perPage, page, sortBy, SortOrderAscending(sortOrder));
        }

        /// <summary>
        /// This resource behaves the same as SearchFor, but allows anonymous users to search public forums
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="page">Returns specified {page} - pagination</param>
        /// <param name="sortBy">Possible values are 'updated_at', 'created_at', 'priority', 'status', and 'ticket_type</param>
        /// <param name="sortOrder">Possible values are 'relevance', 'asc', 'desc'. Defaults to 'relevance' when no 'order' criteria is requested.</param>
        /// <returns></returns>
        public SearchResults AnonymousSearchFor(string searchTerm, string sortBy = "", string sortOrder = "", int page = 1, int? perPage = null)
        {
            var resource = $"portal/search.json?query={searchTerm}";

            return GenericPagedSortedGet<SearchResults>(resource, perPage, page, sortBy, SortOrderAscending(sortOrder));
        }

        /// <summary>
        /// This resource behaves the same as SearchFor, but allows anonymous users to search public forums
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="page">Returns specified {page} - pagination</param>
        /// <param name="sortBy">Possible values are 'updated_at', 'created_at', 'priority', 'status', and 'ticket_type</param>
        /// <param name="sortOrder">Possible values are 'relevance', 'asc', 'desc'. Defaults to 'relevance' when no 'order' criteria is requested.</param>
        /// <returns></returns>
        public SearchResults<T> AnonymousSearchFor<T>(string searchTerm, string sortBy = "", string sortOrder = "", int page = 1, int? perPage = null) where T : ISearchable
        {
            var resource = $"portal/search.json?query={AddTypeToSearchTerm<T>(searchTerm)}";

            return GenericPagedSortedGet<SearchResults<T>>(resource, perPage, page, sortBy, SortOrderAscending(sortOrder));
        }
#endif

#if ASYNC
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="page">Returns specified {page} - pagination</param>
        /// <param name="sortBy">Possible values are 'updated_at', 'created_at', 'priority', 'status', and 'ticket_type</param>
        /// <param name="sortOrder">Possible values are 'relevance', 'asc', 'desc'. Defaults to 'relevance' when no 'order' criteria is requested.</param>
        /// <returns></returns>
        public async Task<SearchResults> SearchForAsync(string searchTerm, string sortBy = "", string sortOrder = "", int page = 1, int? perPage = null)
        {
            var resource = $"search.json?query={searchTerm}";

            return await GenericPagedSortedGetAsync<SearchResults>(resource, perPage, page, sortBy, SortOrderAscending(sortOrder));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="page">Returns specified {page} - pagination</param>
        /// <param name="sortBy">Possible values are 'updated_at', 'created_at', 'priority', 'status', and 'ticket_type</param>
        /// <param name="sortOrder">Possible values are 'relevance', 'asc', 'desc'. Defaults to 'relevance' when no 'order' criteria is requested.</param>
        /// <returns></returns>
        public async Task<SearchResults<T>> SearchForAsync<T>(string searchTerm, string sortBy = "", string sortOrder = "", int page = 1, int? perPage = null) where T : ISearchable
        {
            var resource = $"search.json?query={AddTypeToSearchTerm<T>(searchTerm)}";

            return await GenericPagedSortedGetAsync<SearchResults<T>>(resource, perPage, page, sortBy, SortOrderAscending(sortOrder));
        }

        /// <summary>
        /// This resource behaves the same as SearchFor, but allows anonymous users to search public forums
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="page">Returns specified {page} - pagination</param>
        /// <param name="sortBy">Possible values are 'updated_at', 'created_at', 'priority', 'status', and 'ticket_type</param>
        /// <param name="sortOrder">Possible values are 'relevance', 'asc', 'desc'. Defaults to 'relevance' when no 'order' criteria is requested.</param>
        /// <returns></returns>
        public async Task<SearchResults> AnonymousSearchForAsync(string searchTerm, string sortBy = "", string sortOrder = "", int page = 1, int? perPage = null)
        {
            var resource = $"portal/search.json?query={searchTerm}";

            return await GenericPagedSortedGetAsync<SearchResults>(resource, perPage, page, sortBy, SortOrderAscending(sortOrder));
        }

        /// <summary>
        /// This resource behaves the same as SearchFor, but allows anonymous users to search public forums
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="page">Returns specified {page} - pagination</param>
        /// <param name="sortBy">Possible values are 'updated_at', 'created_at', 'priority', 'status', and 'ticket_type</param>
        /// <param name="sortOrder">Possible values are 'relevance', 'asc', 'desc'. Defaults to 'relevance' when no 'order' criteria is requested.</param>
        /// <returns></returns>
        public async Task<SearchResults<T>> AnonymousSearchForAsync<T>(string searchTerm, string sortBy = "", string sortOrder = "", int page = 1, int? perPage = null) where T : ISearchable
        {
            var resource = $"portal/search.json?query={AddTypeToSearchTerm<T>(searchTerm)}";

            return await GenericPagedSortedGetAsync<SearchResults<T>>(resource, perPage, page, sortBy, SortOrderAscending(sortOrder));
        }
#endif

        public string AddTypeToSearchTerm<T>(string searchTerm = "")
        {
            var typeName = typeof(T).Name;

            return "type:" + typeName + (!(string.IsNullOrEmpty(searchTerm.Trim())) ? " " : "") + searchTerm.Trim();
        }

        public bool SortOrderAscending(string sortOrder)
        {
            return (sortOrder ?? "ascending").ToLower() == "ascending" || (sortOrder ?? "asc").ToLower() == "asc";
        }
    }
}
