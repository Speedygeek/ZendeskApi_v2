﻿using System.Collections.Generic;
#if ASYNC
using System.Threading.Tasks;
#endif
using ZendeskApi_v2.Extensions;
using ZendeskApi_v2.Models.Views;
using ZendeskApi_v2.Models.Views.Executed;

namespace ZendeskApi_v2.Requests
{
    public class Views : Core
    {
        internal Views(IZendeskConnectionSettings connectionSettings)
            : base(connectionSettings)
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
            return GenericGet<IndividualViewResponse>(string.Format("views/{0}.json", id));
        }

        public ExecutedViewResponse ExecuteView(long id, string sortCol = "", bool ascending = true)
        {
            var resource = string.Format("views/{0}/execute.json", id);
            if (!string.IsNullOrEmpty(sortCol))
                resource += string.Format("?sort_by={0}&sort_order={1}", sortCol, ascending ? "" : "desc");

            return GenericGet<ExecutedViewResponse>(resource);
        }

        public ExecutedViewResponse PreviewView(PreviewViewRequest preview)
        {
            return GenericPost<ExecutedViewResponse>("views/preview.json", preview);
        }

        public GroupViewCountResponse GetViewCounts(List<long> viewIds)
        {
            return GenericGet<GroupViewCountResponse>(string.Format("views/count_many.json?ids={0}", viewIds.ToCsv()));
        }

        public IndividualViewCountResponse GetViewCount(long viewId)
        {
            return GenericGet<IndividualViewCountResponse>(string.Format("views/{0}/count.json", viewId));
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
            return await GenericGetAsync<IndividualViewResponse>(string.Format("views/{0}.json", id));
        }

        public async Task<ExecutedViewResponse> ExecuteViewAsync(long id, string sortCol = "", bool ascending = true)
        {
            var resource = string.Format("views/{0}/execute.json", id);
            if (!string.IsNullOrEmpty(sortCol))
                resource += string.Format("?sort_by={0}&sort_order={1}", sortCol, ascending ? "" : "desc");

            return await GenericGetAsync<ExecutedViewResponse>(resource);
        }

        public async Task<ExecutedViewResponse> PreviewViewAsync(PreviewViewRequest preview)
        {
            return await GenericPostAsync<ExecutedViewResponse>("views/preview.json", preview);
        }

        public async Task<GroupViewCountResponse> GetViewCountsAsync(List<long> viewIds)
        {
            return await GenericGetAsync<GroupViewCountResponse>(string.Format("views/count_many.json?ids={0}", viewIds.ToCsv()));
        }

        public async Task<IndividualViewCountResponse> GetViewCountAsync(long viewId)
        {
            return await GenericGetAsync<IndividualViewCountResponse>(string.Format("views/{0}/count.json", viewId));
        }
#endif
    }
}
