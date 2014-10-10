using Newtonsoft.Json;

namespace ZendeskApi_v2.Models.Articles
{

	public class Article
	{
		[JsonProperty("id")]
		public long? Id { get; set; }

		[JsonProperty("title")]
		public string Title { get; set; }

		[JsonProperty("body")]
		public string Body { get; set; }

		[JsonProperty("locale")]
		public string Locale { get; set; }

		[JsonProperty("author_id")]
		public long? AuthorId { get; set; }

		[JsonProperty("comments_disabled")]
		public bool CommentsDisabled { get; set; }

		[JsonProperty("promoted")]
		public bool Promoted { get; set; }

		[JsonProperty("position")]
		public int Position { get; set; }

		[JsonProperty("vote_sum")]
		public int VoteSum { get; set; }

		[JsonProperty("vote_count")]
		public int VoteCount { get; set; }
	
		[JsonProperty("section_id")]
		public long? SectionId { get; set; }

		[JsonProperty("created_at")]
		public string CreatedAt { get; set; }

		[JsonProperty("updated_at")]
		public string UpdatedAt { get; set; }
	}
}
