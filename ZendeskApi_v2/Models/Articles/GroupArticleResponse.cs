using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Articles
{

	public class GroupArticleResponse : GroupResponseBase
	{

		[JsonProperty("articles")]
		public IList<Article> Articles { get; set; }
	}
}
