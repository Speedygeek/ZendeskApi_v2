using System;
using System.Collections.Generic;
#if ASYNC
using System.Threading.Tasks;
#endif
using ZendeskApi_v2.Extensions;
using ZendeskApi_v2.Models.Articles;


namespace ZendeskApi_v2.Requests.HelpCenter
{
    [Flags]
    public enum ArticleSideLoadOptionsEnum{
        None = 1,
        Users = 2,
        Sections = 4,
        Categories = 8,
        Translations = 16
    }


	public interface IArticles : ICore
	{
#if SYNC
        IndividualArticleResponse GetArticle(long articleId);
		GroupArticleResponse GetArticles(ArticleSideLoadOptionsEnum sideloadOptions = ArticleSideLoadOptionsEnum.None, ArticleSortingOptions options = null);
		GroupArticleResponse GetArticlesByCategoryId(long categoryId, ArticleSideLoadOptionsEnum sideloadOptions = ArticleSideLoadOptionsEnum.None, ArticleSortingOptions options = null);
		GroupArticleResponse GetArticlesBySectionId(long sectionId, ArticleSideLoadOptionsEnum sideloadOptions = ArticleSideLoadOptionsEnum.None, ArticleSortingOptions options = null);
		GroupArticleResponse GetArticlesByUserId(long userId);
		GroupArticleResponse GetArticlesSinceDateTime(DateTime startTime);
        ArticleSearchResults SearchArticlesFor(string query, string category = "", string section = "", string labels = "", string locale = "", DateTime? createdBefore = null, DateTime? createdAfter = null, DateTime? createdAt = null, DateTime? updatedBefore = null, DateTime? updatedAfter = null, DateTime? updatedAt = null);
		IndividualArticleResponse CreateArticle(long sectionId, Article article);
		IndividualArticleResponse UpdateArticle(Article article);
		bool DeleteArticle(long id);
#endif
#if ASYNC
        Task<IndividualArticleResponse> GetArticleAsync(long articleId);
		Task<GroupArticleResponse> GetArticlesAsync(ArticleSideLoadOptionsEnum sideloadOptions = ArticleSideLoadOptionsEnum.None, ArticleSortingOptions options = null);
        Task<GroupArticleResponse> GetArticlesByCategoryIdAsync(long categoryId, ArticleSideLoadOptionsEnum sideloadOptions = ArticleSideLoadOptionsEnum.None, ArticleSortingOptions options = null);
		Task<GroupArticleResponse> GetArticlesBySectionIdAsync(long sectionId, ArticleSideLoadOptionsEnum sideloadOptions = ArticleSideLoadOptionsEnum.None, ArticleSortingOptions options = null);
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
        public IndividualArticleResponse GetArticle(long articleId)
		{
			return GenericGet<IndividualArticleResponse>(string.Format("help_center/articles/{0}.json", articleId));
		}

        public GroupArticleResponse GetArticles(ArticleSideLoadOptionsEnum sideloadOptions = ArticleSideLoadOptionsEnum.None, ArticleSortingOptions options = null) {
            var resourceUrl = this.GetFormattedArticlesUri("help_center/articles.json", options, sideloadOptions);

            return GenericGet<GroupArticleResponse>(resourceUrl);
        }


        public GroupArticleResponse GetArticlesByCategoryId(long categoryId, ArticleSideLoadOptionsEnum sideloadOptions = ArticleSideLoadOptionsEnum.None, ArticleSortingOptions options = null)
		{
            var uri = string.Format("help_center/categories/{0}/articles.json", categoryId);
            var resourceUrl = this.GetFormattedArticlesUri(uri, options, sideloadOptions);

			return GenericGet<GroupArticleResponse>(resourceUrl);
		}

		public GroupArticleResponse GetArticlesBySectionId(long sectionId, ArticleSideLoadOptionsEnum sideloadOptions = ArticleSideLoadOptionsEnum.None, ArticleSortingOptions options = null)
		{
            var uri = string.Format("help_center/sections/{0}/articles.json", sectionId);
            var resourceUrl = this.GetFormattedArticlesUri(uri, options, sideloadOptions);

			return GenericGet<GroupArticleResponse>(resourceUrl);
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
        public async Task<IndividualArticleResponse> GetArticleAsync(long articleId)
		{
            return await GenericPostAsync<IndividualArticleResponse>(string.Format("help_center/articles/{0}.json", articleId));
		}

		public async Task<GroupArticleResponse> GetArticlesAsync(ArticleSideLoadOptionsEnum sideloadOptions = ArticleSideLoadOptionsEnum.None, ArticleSortingOptions options = null)
		{
            var resourceUrl = this.GetFormattedArticlesUri("help_center/articles.json", options, sideloadOptions);

			return await GenericGetAsync<GroupArticleResponse>(resourceUrl);
		}

		public async Task<GroupArticleResponse> GetArticlesByCategoryIdAsync(long categoryId, ArticleSideLoadOptionsEnum sideloadOptions = ArticleSideLoadOptionsEnum.None, ArticleSortingOptions options = null)
		{
            var uri = string.Format("help_center/categories/{0}/articles.json", categoryId);
            var resourceUrl = this.GetFormattedArticlesUri(uri, options, sideloadOptions);

			return await GenericGetAsync<GroupArticleResponse>(resourceUrl);
		}

		public async Task<GroupArticleResponse> GetArticlesBySectionIdAsync(long sectionId, ArticleSideLoadOptionsEnum sideloadOptions = ArticleSideLoadOptionsEnum.None, ArticleSortingOptions options = null)
		{
            var uri = string.Format("help_center/sections/{0}/articles.json", sectionId);
            var resourceUrl = this.GetFormattedArticlesUri(uri, options, sideloadOptions);

			return await GenericGetAsync<GroupArticleResponse>(resourceUrl);
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

        private string GetFormattedArticlesUri(string resourceUrl, ArticleSortingOptions options, ArticleSideLoadOptionsEnum sideloadOptions) {
            if (options != null) {
                if (string.IsNullOrEmpty(options.Locale))
                    throw new ArgumentException("Locale is required to sort");

                resourceUrl = options.GetSortingString(resourceUrl);
            }

            string sideLoads = sideloadOptions.ToString().ToLower().Replace(" ", "");
            if (sideloadOptions != ArticleSideLoadOptionsEnum.None) {
                resourceUrl += resourceUrl.Contains("?") ? "&include=" : "?include=";

                //Categories flag REQUIRES sections to be added as well, or nothing will be returned
                if (sideloadOptions.HasFlag(ArticleSideLoadOptionsEnum.Categories) && !sideloadOptions.HasFlag(ArticleSideLoadOptionsEnum.Sections)) {
                    sideLoads += string.Format(",{0}", ArticleSideLoadOptionsEnum.Sections.ToString().ToLower());
                }

                resourceUrl += sideLoads;
            }
            return resourceUrl;
        }
	}
}