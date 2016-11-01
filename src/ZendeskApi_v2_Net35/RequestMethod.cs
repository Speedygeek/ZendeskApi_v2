using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;

#if ASYNC

using System.Threading.Tasks;

#endif

using Newtonsoft.Json;
using ZendeskApi_v2.Extensions;
using ZendeskApi_v2.Models.Shared;

namespace ZendeskApi_v2
{
    public static class RequestMethod
    {
        public const string Get = "GET";
        public const string Put = "PUT";
        public const string Post = "POST";
        public const string Delete = "DELETE";
    }
}