using System.Collections.Generic;
using ZendeskApi_v2.Extensions;
using ZendeskApi_v2.Models.Views;
using ZendeskApi_v2.Models.Views.Executed;

namespace ZendeskApi_v2.Requests
{
    public class Views : Core
    {
        
        public Views(string yourZendeskUrl, string user, string password)
            : base(yourZendeskUrl, user, password)
        {
        }

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

#if NotNet35
        public async Task<GroupViewResponse> GetAllViews()
        {            
            return await GenericGet<GroupViewResponse>("views.json");
        }

        public async Task<GroupViewResponse> GetActiveViews()
        {
            return await GenericGet<GroupViewResponse>("views/active.json");
        }

        public async Task<GroupViewResponse> GetCompactViews()
        {
            return await GenericGet<GroupViewResponse>("views/compact.json");
        }

        public async Task<IndividualViewResponse> GetView(long id)
        {
            return await GenericGet<IndividualViewResponse>(string.Format("views/{0}.json", id));
        }

        public async Task<ExecutedViewResponse> ExecuteView(long id, string sortCol = "", bool ascending = true)
        {
            var resource = string.Format("views/{0}/execute.json", id);
            if (!string.IsNullOrEmpty(sortCol))
                resource += string.Format("?sort_by={0}&sort_order={1}", sortCol, ascending ? "" : "desc");

            return await GenericGet<ExecutedViewResponse>(resource);
        }

        public async Task<ExecutedViewResponse> PreviewView(PreviewViewRequest preview)
        {
            return await GenericPost<ExecutedViewResponse>("views/preview.json", preview);
        }

        public async Task<GroupViewCountResponse> GetViewCounts(List<long> viewIds)
        {
            return await GenericGet<GroupViewCountResponse>(string.Format("views/count_many.json?ids={0}", viewIds.ToCsv()));
        }

        public async Task<IndividualViewCountResponse> GetViewCount(long viewId)
        {
            return await GenericGet<IndividualViewCountResponse>(string.Format("views/{0}/count.json", viewId));
        }
#endif
    }
}
