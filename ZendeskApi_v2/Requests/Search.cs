#if ASYNC
using System.Threading.Tasks;
#endif
using ZendeskApi_v2.Models.Search;

namespace ZendeskApi_v2.Requests
{
    /// <summary>
    /// The search API is a unified search API that returns tickets, users, organizations, and forum topics. 
    /// Define filters to narrow your search results according to result type, date attributes, and object attributes such as ticket requester or tag.
    /// </summary>
    public class Search : Core
    {
        public Search(string yourZendeskUrl, string user, string password)
            : base(yourZendeskUrl, user, password)
        {
        }

#if SYNC
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="sortBy">Possible values are 'updated_at', 'created_at', 'priority', 'status', and 'ticket_type</param>
        /// <param name="sortOrder">Possible values are 'relevance', 'asc', 'desc'. Defaults to 'relevance' when no 'order' criteria is requested.</param>
        /// <returns></returns>
        public SearchResults SearchFor(string searchTerm, string sortBy="", string sortOrder="")
        {
            var resource = string.Format("search.json?query={0}", searchTerm);
            
            if (!string.IsNullOrEmpty(sortBy))
                resource += "&sort_by=" + sortBy;

            if (!string.IsNullOrEmpty(sortOrder))
                resource += "&sort_order=" + sortOrder;

            return GenericGet<SearchResults>(resource);
        }        

        /// <summary>
        /// This resource behaves the same as SearchFor, but allows anonymous users to search public forums
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="sortBy">Possible values are 'updated_at', 'created_at', 'priority', 'status', and 'ticket_type</param>
        /// <param name="sortOrder">Possible values are 'relevance', 'asc', 'desc'. Defaults to 'relevance' when no 'order' criteria is requested.</param>
        /// <returns></returns>
        public SearchResults AnonymousSearchFor(string searchTerm, string sortBy = "", string sortOrder = "")
        {
            var resource = string.Format("portal/search.json?query={0}", searchTerm);

            if (!string.IsNullOrEmpty(sortBy))
                resource += "&sort_by=" + sortBy;

            if (!string.IsNullOrEmpty(sortOrder))
                resource += "&sort_order=" + sortOrder;

            return GenericGet<SearchResults>(resource);
        }
#endif

#if ASYNC
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="sortBy">Possible values are 'updated_at', 'created_at', 'priority', 'status', and 'ticket_type</param>
        /// <param name="sortOrder">Possible values are 'relevance', 'asc', 'desc'. Defaults to 'relevance' when no 'order' criteria is requested.</param>
        /// <returns></returns>
        public async Task<SearchResults> SearchForAsync(string searchTerm, string sortBy="", string sortOrder="")
        {
            var resource = string.Format("search.json?query={0}", searchTerm);
            
            if (!string.IsNullOrEmpty(sortBy))
                resource += "&sort_by=" + sortBy;

            if (!string.IsNullOrEmpty(sortOrder))
                resource += "&sort_order=" + sortOrder;

            return await GenericGetAsync<SearchResults>(resource);
        }

        /// <summary>
        /// This resource behaves the same as SearchFor, but allows anonymous users to search public forums
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="sortBy">Possible values are 'updated_at', 'created_at', 'priority', 'status', and 'ticket_type</param>
        /// <param name="sortOrder">Possible values are 'relevance', 'asc', 'desc'. Defaults to 'relevance' when no 'order' criteria is requested.</param>
        /// <returns></returns>
        public async Task<SearchResults> AnonymousSearchForAsync(string searchTerm, string sortBy = "", string sortOrder = "")
        {
            var resource = string.Format("portal/search.json?query={0}", searchTerm);

            if (!string.IsNullOrEmpty(sortBy))
                resource += "&sort_by=" + sortBy;

            if (!string.IsNullOrEmpty(sortOrder))
                resource += "&sort_order=" + sortOrder;

            return await GenericGetAsync<SearchResults>(resource);
        }
#endif
    }
}