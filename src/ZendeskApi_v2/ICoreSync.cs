using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZendeskApi_v2
{
    public partial interface ICore
    {
        T GetByPageUrl<T>(string pageUrl, int perPage = 100);
        T RunRequest<T>(string resource, string requestMethod, object body = null);
        RequestResult RunRequest(string resource, string requestMethod, object body = null);
    }
}
