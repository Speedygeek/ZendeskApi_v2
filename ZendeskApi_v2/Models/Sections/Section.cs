using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ZendeskApi_v2.Models.HelpCenter.Translations;

namespace ZendeskApi_v2.Models.Sections
{

	public class Section
	{
		[JsonProperty("id")]
		public long? Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("locale")]
		public string Locale { get; set; }

        [JsonProperty("source_locale")]
		public string SourceLocale { get; set; }

        [JsonProperty("url")]
		public string Url { get; set; }

        [JsonProperty("html_url")]
		public string HtmlUrl { get; set; }

		[JsonProperty("category_id")]
		public long? CategoryId { get; set; }

        [JsonProperty("outdated")]
		public bool Outdated { get; set; }

		[JsonProperty("visibility")]
		public string Visibility { get; set; }

		[JsonProperty("position")]
		public int Position { get; set; }

		[JsonProperty("created_at")]
		public string CreatedAt { get; set; }

		[JsonProperty("updated_at")]
		public string UpdatedAt { get; set; }

        [JsonProperty("translations")]
        public IList<Translation> Translations { get; set; }
	}
}
