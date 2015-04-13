using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Articles
{

	public class GroupArticleResponse : GroupResponseBase
	{

		[JsonProperty("articles")]
		public IList<Article> Articles { get; set; }

        [JsonProperty("sections")]
        public IList<Sections.Section> Sections { get; set; }

        [JsonProperty("categories")]
        public IList<HelpCenter.Categories.Category> Categories { get; set; }

        [JsonProperty("users")]
        public IList<Users.User> Users { get; set; }
	}
}
