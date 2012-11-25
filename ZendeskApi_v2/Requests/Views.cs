using System.Collections.Generic;
using ZenDeskApi_v2.Extensions;
using ZenDeskApi_v2.Models.Views;
using ZenDeskApi_v2.Models.Views.Executed;

namespace ZenDeskApi_v2.Requests
{
    public class Views : Core
    {
        
        public Views(string yourZenDeskUrl, string user, string password)
            : base(yourZenDeskUrl, user, password)
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
    }
}
