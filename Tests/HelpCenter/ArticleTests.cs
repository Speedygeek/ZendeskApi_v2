using NUnit.Framework;
using ZendeskApi_v2;
using ZendeskApi_v2.Models.Articles;
using ZendeskApi_v2.Requests.HelpCenter;

namespace Tests.HelpCenter
{
	[TestFixture]
	public class ArticleTests
	{
		private ZendeskApi api = new ZendeskApi(Settings.Site, Settings.Email, Settings.Password);
        private long _articleIdWithComments = 204838115; //https://csharpapi.zendesk.com/hc/en-us/articles/204838115-Thing-4?page=1#comment_200486479

        [Test]
		public void CanGetSingleArticle()
		{
			var res = api.HelpCenter.Articles.GetArticle(_articleIdWithComments);
            Assert.IsNotNull(res.Arcticle);
		}

		[Test]
		public void CanGetArticles()
		{
			var res = api.HelpCenter.Articles.GetArticles();
			Assert.Greater(res.Count, 0);

            var resSections = api.HelpCenter.Sections.GetSections();
            var res1 = api.HelpCenter.Articles.GetArticlesBySectionId(resSections.Sections[0].Id.Value);
			Assert.AreEqual(res1.Articles[0].SectionId, resSections.Sections[0].Id);
		}

        #region Sideloaded Content
        public void CanGetArticleSideloadedWith()
		{
			var res = api.HelpCenter.Articles.GetArticles(ArticleSideLoadOptionsEnum.Sections | ArticleSideLoadOptionsEnum.Categories | ArticleSideLoadOptionsEnum.Users);

            Assert.IsTrue(res.Articles.Count > 0);
            Assert.IsTrue(res.Categories.Count > 0);
            Assert.IsTrue(res.Sections.Count > 0);
            Assert.IsTrue(res.Users.Count > 0);
		}

        [Test]
		public void CanGetArticleSideloadedWithUsers()
		{
			var res = api.HelpCenter.Articles.GetArticles(ArticleSideLoadOptionsEnum.Users);

            Assert.IsTrue(res.Articles.Count > 0);
            Assert.IsTrue(res.Users.Count > 0);
		}

        [Test]
		public void CanGetArticleSideloadedWithSections()
		{
			var res = api.HelpCenter.Articles.GetArticles(ArticleSideLoadOptionsEnum.Sections);

            Assert.IsTrue(res.Articles.Count > 0);
            Assert.IsTrue(res.Sections.Count > 0);
		}

        [Test]
		public void CanGetArticleSideloadedWithCategories()
		{
			var res = api.HelpCenter.Articles.GetArticles(ArticleSideLoadOptionsEnum.Categories);

            Assert.IsTrue(res.Articles.Count > 0);
            Assert.IsTrue(res.Categories.Count > 0);
		}

        [Test]
		public void CanGetArticleSideloadedWithTranslations()
		{
			var res = api.HelpCenter.Articles.GetArticles(ArticleSideLoadOptionsEnum.Categories | ArticleSideLoadOptionsEnum.Sections | ArticleSideLoadOptionsEnum.Users | ArticleSideLoadOptionsEnum.Translations);

            Assert.IsTrue(res.Categories[0].Translations.Count > 0);
            Assert.IsTrue(res.Articles[0].Translations.Count > 0);
            Assert.IsTrue(res.Sections[0].Translations.Count > 0);
		}

        [Test]
        public void CanGetArticleByCategoryWithSideloadedSections()
		{
            var firstCategory = api.HelpCenter.Categories.GetCategories().Categories[0];
			var res = api.HelpCenter.Articles.GetArticlesByCategoryId(firstCategory.Id.Value, ArticleSideLoadOptionsEnum.Sections);

            Assert.IsTrue(res.Sections.Count > 0);
		}

        [Test]
        public void CanGetArticlesSorted()
		{
            var articlesAscending = api.HelpCenter.Articles.GetArticles(ArticleSideLoadOptionsEnum.None, new ArticleSortingOptions(){ SortBy = ArticleSortEnum.Title });
            var articlesDescending = api.HelpCenter.Articles.GetArticles(ArticleSideLoadOptionsEnum.None, new ArticleSortingOptions(){ SortBy = ArticleSortEnum.Title, SortOrder = ArticleSortOrderEnum.Desc });

            Assert.IsTrue(articlesAscending.Articles[0].Title != articlesDescending.Articles[0].Title);
		}

        [Test]
        public void CanGetArticlesSortedInASection()
		{
            var section = api.HelpCenter.Sections.GetSections().Sections[0];
            var articlesAscending = api.HelpCenter.Articles.GetArticlesBySectionId(section.Id.Value, ArticleSideLoadOptionsEnum.None, new ArticleSortingOptions(){ SortBy = ArticleSortEnum.Title });
            var articlesDescending = api.HelpCenter.Articles.GetArticlesBySectionId(section.Id.Value, ArticleSideLoadOptionsEnum.None, new ArticleSortingOptions(){ SortBy = ArticleSortEnum.Title, SortOrder = ArticleSortOrderEnum.Desc });

            Assert.IsTrue(articlesAscending.Articles[0].Title != articlesDescending.Articles[0].Title);
		}

        /// <summary>
        /// This throws a 500 error, no idea why, ticket into Zendesk
        /// </summary>
        [Test]
        public void CanGetArticlesSortedInACategory()
		{
            var category = api.HelpCenter.Categories.GetCategories().Categories[0];
            var articlesAscending = api.HelpCenter.Articles.GetArticlesByCategoryId(category.Id.Value, ArticleSideLoadOptionsEnum.None, new ArticleSortingOptions(){ SortBy = ArticleSortEnum.Title });
            var articlesDescending = api.HelpCenter.Articles.GetArticlesByCategoryId(category.Id.Value, ArticleSideLoadOptionsEnum.None, new ArticleSortingOptions(){ SortBy = ArticleSortEnum.Title, SortOrder = ArticleSortOrderEnum.Desc });

            Assert.IsTrue(articlesAscending.Articles[0].Title != articlesDescending.Articles[0].Title);
		}
        #endregion

        [Test]
        public void CanCreateUpdateAndDeleteArticles()
        {
            var resSections = api.HelpCenter.Sections.GetSections();
            var res = api.HelpCenter.Articles.CreateArticle(resSections.Sections[0].Id.Value, new Article()
            {
                Title = "My Test article",
                Body = "The body of my article",
                Locale = "en-us"
            });
            Assert.Greater(res.Arcticle.Id, 0);

            res.Arcticle.Body = "updated body";
            var update = api.HelpCenter.Articles.UpdateArticle(res.Arcticle);
            Assert.AreEqual(update.Arcticle.Body, res.Arcticle.Body);

            Assert.True(api.HelpCenter.Articles.DeleteArticle(res.Arcticle.Id.Value));
        }

        [Test]
        public void CanGetArticlesAsync()
        {
            var res = api.HelpCenter.Articles.GetArticlesAsync().Result;
            Assert.Greater(res.Count, 0);

            var resSections = api.HelpCenter.Sections.GetSectionsAsync().Result;
            var res1 = api.HelpCenter.Articles.GetArticlesBySectionIdAsync(resSections.Sections[0].Id.Value);
            Assert.AreEqual(res1.Result.Articles[0].SectionId, resSections.Sections[0].Id);
        }

        [Test]
        public void CanCreateUpdateAndDeleteArticlesAsync()
        {
            var resSections = api.HelpCenter.Sections.GetSectionsAsync().Result;
            var res = api.HelpCenter.Articles.CreateArticleAsync(resSections.Sections[0].Id.Value, new Article()
            {
                Title = "My Test article",
                Body = "The body of my article",
                Locale = "en-us"
            }).Result;
            Assert.Greater(res.Arcticle.Id, 0);

            res.Arcticle.Body = "updated body";
            var update = api.HelpCenter.Articles.UpdateArticleAsync(res.Arcticle).Result;
            Assert.AreEqual(update.Arcticle.Body, res.Arcticle.Body);

            Assert.True(api.HelpCenter.Articles.DeleteArticleAsync(res.Arcticle.Id.Value).Result);
        }
	}
}