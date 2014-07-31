using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZendeskApi_v2.Extensions;
using ZendeskApi_v2.Models.Articles;


namespace ZendeskApi_v2.Requests
{
	public class Articles : Core
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
	}
}
