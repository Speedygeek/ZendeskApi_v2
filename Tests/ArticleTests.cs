using NUnit.Framework;
using ZendeskApi_v2;
using ZendeskApi_v2.Models.Articles;

namespace Tests
{
	[TestFixture]
	public class ArticleTests
	{
		private ZendeskApi api = new ZendeskApi(Settings.Site, Settings.Email, Settings.Password);

		[Test]
		public void CanGetArticles()
		{
			var res = api.Articles.GetArticles();
			Assert.Greater(res.Count, 0);

			var resSections = api.Sections.GetSections();
			var res1 = api.Articles.GetArticlesBySectionId(resSections.Sections[0].Id.Value);
			Assert.AreEqual(res1.Articles[0].SectionId, resSections.Sections[0].Id);
		}

		[Test]
		public void CanCreateUpdateAndDeleteArticles()
		{
			var resSections = api.Sections.GetSections();
			var res = api.Articles.CreateArticle(resSections.Sections[0].Id.Value, new Article()
			{
				Title = "My Test article",
				Body = "The body of my article",
				Locale = "en-us"
			});
			Assert.Greater(res.Arcticle.Id, 0);

			res.Arcticle.Body = "updated body";
			var update = api.Articles.UpdateArticle(res.Arcticle);
			Assert.AreEqual(update.Arcticle.Body, res.Arcticle.Body);

			Assert.True(api.Articles.DeleteArticle(res.Arcticle.Id.Value));
		}

		[Test]
		public void CanGetArticlesAsync()
		{
			var res = api.Articles.GetArticlesAsync().Result;
			Assert.Greater(res.Count, 0);

			var resSections = api.Sections.GetSectionsAsync().Result;
			var res1 = api.Articles.GetArticlesBySectionIdAsync(resSections.Sections[0].Id.Value);
			Assert.AreEqual(res1.Result.Articles[0].SectionId, resSections.Sections[0].Id);
		}

		[Test]
		public void CanCreateUpdateAndDeleteArticlesAsync()
		{
			var resSections = api.Sections.GetSectionsAsync().Result;
			var res = api.Articles.CreateArticleAsync(resSections.Sections[0].Id.Value, new Article()
			{
				Title = "My Test article",
				Body = "The body of my article",
				Locale = "en-us"
			}).Result;
			Assert.Greater(res.Arcticle.Id, 0);

			res.Arcticle.Body = "updated body";
			var update = api.Articles.UpdateArticleAsync(res.Arcticle).Result;
			Assert.AreEqual(update.Arcticle.Body, res.Arcticle.Body);

			Assert.True(api.Articles.DeleteArticleAsync(res.Arcticle.Id.Value).Result);
		}
	}
}