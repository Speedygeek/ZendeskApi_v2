using Newtonsoft.Json.Linq;
using System;
using ZendeskApi_v2.Models.Targets;

namespace ZendeskApi_v2.Serialization
{
    public class TargetJsonConverter : BaseJsonConverter<BaseTarget>
    {
        protected override BaseTarget Create(Type objectType, JObject jsonObject)
        {
            var typeName = jsonObject["type"].ToString();
            switch (typeName)
            {
                case "basecamp_target":
                    return new BasecampTarget();
                case "campfire_target":
                    return new CampfireTarget();
                case "clickatell_target":
                    return new ClickatellTarget();
                case "email_target":
                    return new EmailTarget();
                case "flowdock_target":
                    return new FlowdockTarget();
                case "get_satisfaction_target":
                    return new GetSatisfactionTarget();
                case "jira_target":
                    return new JiraTarget();
                case "pivotal_target":
                    return new PivotalTarget();
                case "twitter_target":
                    return new TwitterTarget();
                case "url_target":
                    return new URLTarget();
                case "http_target":
                    return new HTTPTarget();
                case "url_target_v2":
                    return new HTTPTarget();
                default:
                    return null;
            }
        }
    }
}
