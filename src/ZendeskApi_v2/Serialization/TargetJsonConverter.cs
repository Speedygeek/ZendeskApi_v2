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
            return typeName switch
            {
                "basecamp_target" => new BasecampTarget(),
                "campfire_target" => new CampfireTarget(),
                "clickatell_target" => new ClickatellTarget(),
                "email_target" => new EmailTarget(),
                "flowdock_target" => new FlowdockTarget(),
                "get_satisfaction_target" => new GetSatisfactionTarget(),
                "jira_target" => new JiraTarget(),
                "pivotal_target" => new PivotalTarget(),
                "twitter_target" => new TwitterTarget(),
                "url_target" => new URLTarget(),
                "http_target" => new HTTPTarget(),
                "url_target_v2" => new HTTPTarget(),
                _ => null,
            };
        }
    }
}
