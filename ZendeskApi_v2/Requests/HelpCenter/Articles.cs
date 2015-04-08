using System;
#if ASYNC
using System.Threading.Tasks;
#endif
using ZendeskApi_v2.Extensions;
using ZendeskApi_v2.Models.Articles;


namespace ZendeskApi_v2.Requests.HelpCenter
{
	public interface IArticles : ICore
	{
#if SYNC

		GroupArticleResponse GetArticles();
		GroupArticleResponse GetArticlesByCategoryId(long categoryId);
		GroupArticleResponse GetArticlesBySectionId(long sectionId);
		GroupArticleResponse GetArticlesByUserId(long userId);
		GroupArticleResponse GetArticlesSinceDateTime(DateTime startTime);
        ArticleSearchResults SearchArticlesFor(string query, string category = "", string section = "", string labels = "", string locale = "", DateTime? createdBefore = null, DateTime? createdAfter = null, DateTime? createdAt = null, DateTime? updatedBefore = null, DateTime? updatedAfter = null, DateTime? updatedAt = null);
		IndividualArticleResponse CreateArticle(long sectionId, Article article);
		IndividualArticleResponse UpdateArticle(Article article);
		bool DeleteArticle(long id);
#endif
#if ASYNC
		Task<GroupArticleResponse> GetArticlesAsync();
		Task<GroupArticleResponse> GetArticlesByCategoryIdAsync(long categoryId);
		Task<GroupArticleResponse> GetArticlesBySectionIdAsync(long sectionId);
		Task<GroupArticleResponse> GetArticlesByUserIdAsync(long userId);
		Task<GroupArticleResponse> GetArticlesSinceDateTimeAsync(DateTime startTime);
        Task<ArticleSearchResults> SearchArticlesForAsync(string query, string category = "", string section = "", string labels = "", string locale = "", DateTime? createdBefore = null, DateTime? createdAfter = null, DateTime? createdAt = null, DateTime? updatedBefore = null, DateTime? updatedAfter = null, DateTime? updatedAt = null);
		Task<IndividualArticleResponse> CreateArticleAsync(long sectionId, Article article);
		Task<IndividualArticleResponse> UpdateArticleAsync(Article article);
		Task<bool> DeleteArticleAsync(long id);
#endif

	}

	public class Articles : Core, IArticles
	{
		public Articles(string zendeskApiUrl, string user, string password, string apiToken)
			: base(zendeskApiUrl, user, password, apiToken)
		{
		}

#if SYNC

		public GroupArticleResponse GetArticles()
		{ 
			return GenericGet<GroupArticleResponse>("help_center/articles.json");
		}
		public GroupArticleResponse GetArticlesByCategoryId(long categoryId)
		{
			return GenericGet<GroupArticleResponse>(string.Format("help_center/categories/{0}/articles.json", categoryId));
		}
		public GroupArticleResponse GetArticlesBySectionId(long sectionId)
		{
			return GenericGet<GroupArticleResponse>(string.Format("help_center/sections/{0}/articles.json", sectionId));
		}
		public GroupArticleResponse GetArticlesByUserId(long userId)
		{
			return GenericGet<GroupArticleResponse>(string.Format("help_center/users/{0}/articles.json", userId));
		}
		public GroupArticleResponse GetArticlesSinceDateTime(DateTime startTime)
		{
			return GenericGet<GroupArticleResponse>(string.Format("help_center/incremental/articles.json?start_time={0}", startTime.GetEpoch()));
		}
        public ArticleSearchResults SearchArticlesFor(string query, string category = "", string section = "", string labels = "", string locale = "", DateTime? createdBefore = null, DateTime? createdAfter = null, DateTime? createdAt = null, DateTime? updatedBefore = null, DateTime? updatedAfter = null, DateTime? updatedAt = null)
		{
			return GenericGet<ArticleSearchResults>(string.Format("help_center/articles/search.json?query={0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}", 
                                                                            query,
                                                                            String.IsNullOrEmpty(category) ? "" : "&category=" + category,
                                                                            String.IsNullOrEmpty(section) ? "" : "&section=" + section,
                                                                            String.IsNullOrEmpty(labels) ? "" : "&label_names=" + labels,
                                                                            String.IsNullOrEmpty(locale) ? "" : "&locale=" + locale,
                                                                            !createdBefore.HasValue ? "" : "&created_before=" + createdBefore.Value.ToString("yyyy-MM-dd"),
                                                                            !createdAfter.HasValue ? "" : "&created_after=" + createdAfter.Value.ToString("yyyy-MM-dd"),
                                                                            !createdAt.HasValue ? "" : "&created_at=" + createdAt.Value.ToString("yyyy-MM-dd"),
                                                                            !updatedBefore.HasValue ? "" : "&updated_before=" + updatedBefore.Value.ToString("yyyy-MM-dd"),
                                                                            !updatedAfter.HasValue ? "" : "&updated_after=" + updatedAfter.Value.ToString("yyyy-MM-dd"),
                                                                            !updatedAt.HasValue ? "" : "&updated_at=" + updatedAt.Value.ToString("yyyy-MM-dd")));
		}
		public IndividualArticleResponse CreateArticle(long sectionId, Article article)
		{
			var body = new { article };
			return GenericPost<IndividualArticleResponse>(string.Format("help_center/sections/{0}/articles.json", sectionId), body);
		}

		public IndividualArticleResponse UpdateArticle(Article article)
		{
			var body = new { article };
			return GenericPut<IndividualArticleResponse>(string.Format("help_center/articles/{0}.json", article.Id), body);
		}

		public bool DeleteArticle(long id)
		{
			return GenericDelete(string.Format("help_center/articles/{0}.json", id));
		}
#endif
#if ASYNC

		public async Task<GroupArticleResponse> GetArticlesAsync()
		{
			return await GenericGetAsync<GroupArticleResponse>("help_center/articles.json");
		}

		public async Task<GroupArticleResponse> GetArticlesByCategoryIdAsync(long categoryId)
		{
			return await GenericGetAsync<GroupArticleResponse>(string.Format("help_center/categories/{0}/articles.json", categoryId));
		}

		public async Task<GroupArticleResponse> GetArticlesBySectionIdAsync(long sectionId)
		{
			return await GenericGetAsync<GroupArticleResponse>(string.Format("help_center/sections/{0}/articles.json", sectionId));
		}

		public async Task<GroupArticleResponse> GetArticlesByUserIdAsync(long userId)
		{
			return await GenericGetAsync<GroupArticleResponse>(string.Format("help_center/users/{0}/articles.json", userId));
		}

		public async Task<GroupArticleResponse> GetArticlesSinceDateTimeAsync(DateTime startTime)
		{
			return await GenericGetAsync<GroupArticleResponse>(string.Format("help_center/incremental/articles.json?start_time={0}", startTime.GetEpoch()));
		}
        
        public async Task<ArticleSearchResults> SearchArticlesForAsync(string query, string category = "", string section = "", string labels = "", string locale = "", DateTime? createdBefore = null, DateTime? createdAfter = null, DateTime? createdAt = null, DateTime? updatedBefore = null, DateTime? updatedAfter = null, DateTime? updatedAt = null)
		{
			return await GenericGetAsync<ArticleSearchResults>(string.Format("help_center/articles/search.json?query={0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}", 
                                                                            query,
                                                                            String.IsNullOrEmpty(category) ? "" : "&category=" + category,
                                                                            String.IsNullOrEmpty(section) ? "" : "&section=" + section,
                                                                            String.IsNullOrEmpty(labels) ? "" : "&label_names=" + labels,
                                                                            String.IsNullOrEmpty(locale) ? "" : "&locale=" + locale,
                                                                            !createdBefore.HasValue ? "" : "&created_before=" + createdBefore.Value.ToString("yyyy-MM-dd"),
                                                                            !createdAfter.HasValue ? "" : "&created_after=" + createdAfter.Value.ToString("yyyy-MM-dd"),
                                                                            !createdAt.HasValue ? "" : "&created_at=" + createdAt.Value.ToString("yyyy-MM-dd"),
                                                                            !updatedBefore.HasValue ? "" : "&updated_before=" + updatedBefore.Value.ToString("yyyy-MM-dd"),
                                                                            !updatedAfter.HasValue ? "" : "&updated_after=" + updatedAfter.Value.ToString("yyyy-MM-dd"),
                                                                            !updatedAt.HasValue ? "" : "&updated_at=" + updatedAt.Value.ToString("yyyy-MM-dd")));
		}

		public async Task<IndividualArticleResponse> CreateArticleAsync(long sectionId, Article article)
		{
			var body = new { article };
			return await GenericPostAsync<IndividualArticleResponse>(string.Format("help_center/sections/{0}/articles.json", sectionId), body);
		}

		public async Task<IndividualArticleResponse> UpdateArticleAsync(Article article)
		{
			var body = new { article };
			return await GenericPutAsync<IndividualArticleResponse>(string.Format("help_center/articles/{0}.json", article.Id), body);
		}

		public async Task<bool> DeleteArticleAsync(long id)
		{
			return await GenericDeleteAsync(string.Format("help_center/articles/{0}.json", id));
		}
#endif
	}
}
