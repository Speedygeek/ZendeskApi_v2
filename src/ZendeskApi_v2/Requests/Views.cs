using System.Collections.Generic;
using System.Globalization;
using System.Linq;
#if ASYNC
using System.Threading.Tasks;
#endif
using ZendeskApi_v2.Extensions;
using ZendeskApi_v2.Models.Views;
using ZendeskApi_v2.Models.Views.Executed;

namespace ZendeskApi_v2.Requests
{
	public interface IViews : ICore
	{
#if SYNC
		GroupViewResponse GetAllViews();
		GroupViewResponse GetActiveViews();
		GroupViewResponse GetCompactViews();
		IndividualViewResponse GetView(long id);
		ExecutedViewResponse ExecuteView(long id, string sortCol = "", bool ascending = true, int? perPage = null, int? page = null);
		ExecutedViewResponse PreviewView(PreviewViewRequest preview);
		GroupViewCountResponse GetViewCounts(IEnumerable<long> viewIds);
		IndividualViewCountResponse GetViewCount(long viewId);
#endif
		
#if ASYNC
		Task<GroupViewResponse> GetAllViewsAsync();
		Task<GroupViewResponse> GetActiveViewsAsync();
		Task<GroupViewResponse> GetCompactViewsAsync();
		Task<IndividualViewResponse> GetViewAsync(long id);
        Task<ExecutedViewResponse> ExecuteViewAsync(long id, string sortCol = "", bool ascending = true, int? perPage = null, int? page = null);
		Task<ExecutedViewResponse> PreviewViewAsync(PreviewViewRequest preview);
		Task<GroupViewCountResponse> GetViewCountsAsync(IEnumerable<long> viewIds);
		Task<IndividualViewCountResponse> GetViewCountAsync(long viewId);
#endif
	}

	public class Views : Core, IViews
	{
        
        public Views(string yourZendeskUrl, string user, string password, string apiToken, string p_OAuthToken, Dictionary<string,string> customHeaders)
            : base(yourZendeskUrl, user, password, apiToken, p_OAuthToken, customHeaders)
        {
        }

#if SYNC
        public GroupViewResponse GetAllViews()
        {            
            return GenericGet<GroupViewResponse>("views.json");
        }

        public GroupViewResponse GetActiveViews()
        {
            return GenericGet<GroupViewResponse>("views/active.json");
        }

        public GroupViewResponse GetCompactViews()
        {
            return GenericGet<GroupViewResponse>("views/compact.json");
        }

        public IndividualViewResponse GetView(long id)
        {
            return GenericGet<IndividualViewResponse>($"views/{id}.json");
        }

        public ExecutedViewResponse ExecuteView(long id, string sortCol = "", bool ascending = true, int? perPage = null, int? page = null)
        {
            var resource = $"views/{id}/execute.json";

            return GenericPagedSortedGet<ExecutedViewResponse>(resource, perPage, page, sortCol, ascending);
        }

        public ExecutedViewResponse PreviewView(PreviewViewRequest preview)
        {
            return GenericPost<ExecutedViewResponse>("views/preview.json", preview);
        }

        public GroupViewCountResponse GetViewCounts(IEnumerable<long> viewIds)
        {
            return GenericGet<GroupViewCountResponse>($"views/count_many.json?ids={viewIds.ToCsv()}");
        }

        public IndividualViewCountResponse GetViewCount(long viewId)
        {
            return GenericGet<IndividualViewCountResponse>($"views/{viewId}/count.json");
        }
#endif

#if ASYNC
        public async Task<GroupViewResponse> GetAllViewsAsync()
        {
            return await GenericGetAsync<GroupViewResponse>("views.json");
        }

        public async Task<GroupViewResponse> GetActiveViewsAsync()
        {
            return await GenericGetAsync<GroupViewResponse>("views/active.json");
        }

        public async Task<GroupViewResponse> GetCompactViewsAsync()
        {
            return await GenericGetAsync<GroupViewResponse>("views/compact.json");
        }

        public async Task<IndividualViewResponse> GetViewAsync(long id)
        {
            return await GenericGetAsync<IndividualViewResponse>($"views/{id}.json");
        }

        public async Task<ExecutedViewResponse> ExecuteViewAsync(long id, string sortCol = "", bool ascending = true, int? perPage = null, int? page = null)
        {
            var resource = $"views/{id}/execute.json";

            return await GenericPagedSortedGetAsync<ExecutedViewResponse>(resource, perPage, page, sortCol, ascending);
        }

        public async Task<ExecutedViewResponse> PreviewViewAsync(PreviewViewRequest preview)
        {
            return await GenericPostAsync<ExecutedViewResponse>("views/preview.json", preview);
        }

        public async Task<GroupViewCountResponse> GetViewCountsAsync(IEnumerable<long> viewIds)
        {
            return await GenericGetAsync<GroupViewCountResponse>($"views/count_many.json?ids={viewIds.ToCsv()}");
        }

        public async Task<IndividualViewCountResponse> GetViewCountAsync(long viewId)
        {
            return await GenericGetAsync<IndividualViewCountResponse>($"views/{viewId}/count.json");
        }
#endif
    }
}
