using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZendeskApi_v2
{
    public interface IZendeskConnectionSettings
    {

        /// <summary>
        /// This is the Zendesk subdomain name used for connection
        /// </summary>
        string Domain { get; }

        /// <summary>
        /// Credentials used for connection
        /// </summary>
        IZendeskCredentials Credentials { get;  }
    }
   
}
