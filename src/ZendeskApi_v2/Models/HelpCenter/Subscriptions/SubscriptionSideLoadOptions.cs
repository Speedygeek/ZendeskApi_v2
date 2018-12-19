using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZendeskApi_v2.Models.HelpCenter.Subscriptions
{
    [Flags]
    public enum SubscriptionSideLoadOptions
    {
        None = 0,
        Users = 2,
        Sections = 4,
        Articles = 8,
        Translations = 16,
        Posts = 32,
        Topics = 64
    }
}
