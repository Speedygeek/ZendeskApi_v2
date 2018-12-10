﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using ZendeskApi_v2;
using ZendeskApi_v2.Models.Articles;
using ZendeskApi_v2.Models.Sections;
using ZendeskApi_v2.Models.Users;
using ZendeskApi_v2.Requests.HelpCenter;

namespace Tests.HelpCenter
{
    [TestFixture]
    [Category("HelpCenter")]
    [Parallelizable(ParallelScope.None)]
    public class ArticleTests
    {
        private readonly ZendeskApi api = new ZendeskApi(Settings.Site, Settings.AdminEmail, Settings.AdminPassword);
        private readonly long _articleIdWithComments = 360020742371; //https://csharpapi.zendesk.com/hc/en-us/articles/204838115-Thing-4?page=1#comment_200486479
        private readonly long sectionId = 360002891952;

        [Test]
        public void CanGetSingleArticle()
        {
            var res = api.HelpCenter.Articles.GetArticle(_articleIdWithComments);
            Assert.IsNotNull(res.Article);
        }

        [Test]
        public void CanGetSingleArticleWithTranslations()
        {
            var res = api.HelpCenter.Articles.GetArticle(_articleIdWithComments, ArticleSideLoadOptionsEnum.Translations);
            Assert.IsNotNull(res.Article);
            Assert.Greater(res.Article.Translations.Count, 0);
        }

        [Test]
        public void CanGetArticles()
        {
            var res = api.HelpCenter.Articles.GetArticles();
            Assert.Greater(res.Count, 0);

            var resSections = api.HelpCenter.Sections.GetSections();
            var res1 = api.HelpCenter.Articles.GetArticlesBySectionId(sectionId);
            Assert.That(res1.Articles[0].SectionId, Is.EqualTo(sectionId));
        }

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
            var firstCategory = api.HelpCenter.Categories.GetCategoryById(200382245).Category;
            var res = api.HelpCenter.Articles.GetArticlesByCategoryId(firstCategory.Id.Value, ArticleSideLoadOptionsEnum.Sections);

            Assert.IsTrue(res.Sections.Count > 0);
        }

        [Test]
        public void CanGetArticlesSorted()
        {
            var articlesAscending = api.HelpCenter.Articles.GetArticles(ArticleSideLoadOptionsEnum.None, new ArticleSortingOptions() { SortBy = ArticleSortEnum.Title });
            var articlesDescending = api.HelpCenter.Articles.GetArticles(ArticleSideLoadOptionsEnum.None, new ArticleSortingOptions() { SortBy = ArticleSortEnum.Title, SortOrder = ArticleSortOrderEnum.Desc });

            Assert.IsTrue(articlesAscending.Articles[0].Title != articlesDescending.Articles[0].Title);
        }

        [Test]
        public void CanGetArticlesSortedInASection()
        {
            var section = api.HelpCenter.Sections.GetSectionById(sectionId).Section;

            var articlesAscending = api.HelpCenter.Articles.GetArticlesBySectionId(section.Id.Value, ArticleSideLoadOptionsEnum.None,
                new ArticleSortingOptions() { SortBy = ArticleSortEnum.Title });
            var articlesDescending = api.HelpCenter.Articles.GetArticlesBySectionId(section.Id.Value, ArticleSideLoadOptionsEnum.None,
                new ArticleSortingOptions() { SortBy = ArticleSortEnum.Title, SortOrder = ArticleSortOrderEnum.Desc });

            Assert.IsTrue(articlesAscending.Articles[0].Title != articlesDescending.Articles[0].Title);
        }

        /// <summary>
        /// This throws a 500 error, no idea why, ticket into Zendesk
        /// </summary>
        [Test]
        public void CanGetArticlesSortedInACategory()
        {
            var category = api.HelpCenter.Categories.GetCategoryById(200382245).Category;
            var articlesAscending = api.HelpCenter.Articles.GetArticlesByCategoryId(category.Id.Value, ArticleSideLoadOptionsEnum.None, new ArticleSortingOptions() { SortBy = ArticleSortEnum.Title });
            var articlesDescending = api.HelpCenter.Articles.GetArticlesByCategoryId(category.Id.Value, ArticleSideLoadOptionsEnum.None, new ArticleSortingOptions() { SortBy = ArticleSortEnum.Title, SortOrder = ArticleSortOrderEnum.Desc });

            Assert.IsTrue(articlesAscending.Articles[0].Title != articlesDescending.Articles[0].Title);
        }

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
            Assert.Greater(res.Article.Id, 0);

            res.Article.LabelNames = new string[] { "updated" };
            var update = api.HelpCenter.Articles.UpdateArticleAsync(res.Article).Result;
            Assert.That(update.Article.LabelNames, Is.EqualTo(res.Article.LabelNames));

            Assert.True(api.HelpCenter.Articles.DeleteArticle(res.Article.Id.Value));
        }

        [Test]
        public void CanGetSingleArticleWithTranslationsAsync()
        {
            var res = api.HelpCenter.Articles.GetArticleAsync(_articleIdWithComments, ArticleSideLoadOptionsEnum.Translations).Result;
            Assert.IsNotNull(res.Article);
            Assert.Greater(res.Article.Translations.Count, 0);
        }

        [Test]
        public async Task CanGetArticlesAsync()
        {
            var res = await api.HelpCenter.Articles.GetArticlesAsync();
            Assert.Greater(res.Count, 0);

            var resSections = await api.HelpCenter.Sections.GetSectionsAsync();
            var res1 = await api.HelpCenter.Articles.GetArticlesBySectionIdAsync(sectionId);
            Assert.That(res1.Articles[0].SectionId, Is.EqualTo(sectionId));
        }

        [Test]
        public async Task CanCreateUpdateAndDeleteArticlesAsync()
        {
            var resSections = await api.HelpCenter.Sections.GetSectionsAsync();
            var res = await api.HelpCenter.Articles.CreateArticleAsync(resSections.Sections[0].Id.Value, new Article
            {
                Title = "My Test article",
                Body = "The body of my article",
                Locale = "en-us"
            });

            Assert.Greater(res.Article.Id, 0);

            res.Article.LabelNames = new string[] { "photo", "tripod" };
            var update = await api.HelpCenter.Articles.UpdateArticleAsync(res.Article);
            Assert.AreEqual(update.Article.LabelNames, res.Article.LabelNames);

            Assert.True(await api.HelpCenter.Articles.DeleteArticleAsync(res.Article.Id.Value));
        }

        [Test]
        public async Task CanGetSecondPageUisngGetByPageUrl()
        {
            var pageSize = 3;

            var res = await api.HelpCenter.Articles.GetArticlesAsync(perPage: pageSize);
            Assert.That(res.PageSize, Is.EqualTo(pageSize));

            var resp = await api.HelpCenter.Articles.GetByPageUrlAsync<GroupArticleResponse>(res.NextPage, pageSize);
            Assert.That(resp.Page, Is.EqualTo(2));
        }

        [Test]
        public async Task CanSearchForArticlesAsync()
        {
            var resp = await api.HelpCenter.Articles.SearchArticlesForAsync("Test", createdBefore: DateTime.Now);

            Assert.That(resp.Count, Is.GreaterThan(0));
        }

        [Test]
        public void CanSearchForArticles()
        {
            var resp = api.HelpCenter.Articles.SearchArticlesFor("Test", createdBefore: DateTime.Now);

            Assert.That(resp.Count, Is.GreaterThan(0));
        }
    }
}
