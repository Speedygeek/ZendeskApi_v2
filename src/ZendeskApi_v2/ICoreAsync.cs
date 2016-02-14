using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZendeskApi_v2
{
    public partial interface ICore
    {
        Task<T> GetByPageUrlAsync<T>(string pageUrl, int perPage = 100);
        Task<T> RunRequestAsync<T>(string resource, string requestMethod, object body = null);
        Task<RequestResult> RunRequestAsync(string resource, string requestMethod, object body = null);
    }
}
