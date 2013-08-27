using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;

namespace ZendeskApi_v2
{
    public interface IZendeskCredentials
    {
        NetworkCredential CreateNetworkCredentials();
    }

    public class ZendeskApiTokenCredentials : IZendeskCredentials
    {

        public string Identity { get; set; }

#if !MOBILE && ASYNC
        public SecureString SecureApiToken { get; set; }
#endif
        public string ApiToken { get; set; }

        public NetworkCredential CreateNetworkCredentials()
        {
            var identityWithToken = String.Format("{0}/token", Identity);

#if MOBILE || !ASYNC
            return new NetworkCredential(identityWithToken, ApiToken);
#else
            return SecureApiToken == null ? new NetworkCredential(identityWithToken, ApiToken) : new NetworkCredential(identityWithToken, SecureApiToken);
#endif
        }
    }

    public class ZendeskUsernamePasswordCredentials : IZendeskCredentials
    {

        public string Identity { get; set; }

#if !MOBILE && ASYNC
        public SecureString SecurePassword { get; set; }
#endif
        public string Password { get; set; }

        public NetworkCredential CreateNetworkCredentials()
        {
#if MOBILE || !ASYNC
            return new NetworkCredential(Identity, Password);
#else
            return SecurePassword == null ? new NetworkCredential(Identity,  Password) : new NetworkCredential(Identity,  SecurePassword);
#endif
        }
    }
}
