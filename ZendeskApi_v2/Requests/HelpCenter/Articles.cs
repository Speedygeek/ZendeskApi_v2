using System;
using System.Threading.Tasks;
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
