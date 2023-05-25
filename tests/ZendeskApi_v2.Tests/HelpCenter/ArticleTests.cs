using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using ZendeskApi_v2.Models.Articles;
using ZendeskApi_v2.Requests.HelpCenter;
using ZendeskApi_v2.Tests.Base;

namespace ZendeskApi_v2.Tests.HelpCenter;

[TestFixture]
[Category("HelpCenter")]
[Parallelizable(ParallelScope.None)]
public class ArticleTests : TestBase
{
    private readonly long _articleIdWithComments = 360021096471; //https://csharpapi.zendesk.com/hc/en-us/articles/204838115-Thing-4?page=1#comment_200486479
    private readonly long sectionId = 360002891952;

    [Test]
    public void CanGetSingleArticle()
    {
        var res = Api.HelpCenter.Articles.GetArticle(_articleIdWithComments);
        Assert.That(res.Article, Is.Not.Null);
    }

    [Test]
    public void CanGetSingleArticleWithTranslations()
    {
        var res = Api.HelpCenter.Articles.GetArticle(_articleIdWithComments, ArticleSideLoadOptionsEnum.Translations);
        Assert.That(res.Article, Is.Not.Null);
        Assert.That(res.Article.Translations, Is.Not.Empty);
    }

    [Test]
    public void CanGetArticles()
    {
        var res = Api.HelpCenter.Articles.GetArticles();
        Assert.That(res.Count, Is.GreaterThan(0));

        Api.HelpCenter.Sections.GetSections();
        var res1 = Api.HelpCenter.Articles.GetArticlesBySectionId(sectionId);
        Assert.That(res1.Articles[0].SectionId, Is.EqualTo(sectionId));
    }

    [Test]
    public void CanGetArticleSideloadedWith()
    {
        var res = Api.HelpCenter.Articles.GetArticles(ArticleSideLoadOptionsEnum.Sections | ArticleSideLoadOptionsEnum.Categories | ArticleSideLoadOptionsEnum.Users);
        Assert.Multiple(() =>
        {
            Assert.That(res.Articles, Is.Not.Empty);
            Assert.That(res.Categories, Is.Not.Empty);
            Assert.That(res.Sections, Is.Not.Empty);
            Assert.That(res.Users, Is.Not.Empty);
        });
    }

    [Test]
    public void CanGetArticleSideloadedWithUsers()
    {
        var res = Api.HelpCenter.Articles.GetArticles(ArticleSideLoadOptionsEnum.Users);
        Assert.Multiple(() =>
        {
            Assert.That(res.Articles, Is.Not.Empty);
            Assert.That(res.Users, Is.Not.Empty);
        });
    }

    [Test]
    public void CanGetArticleSideloadedWithSections()
    {
        var res = Api.HelpCenter.Articles.GetArticles(ArticleSideLoadOptionsEnum.Sections);
        Assert.Multiple(() =>
        {
            Assert.That(res.Articles, Is.Not.Empty);
            Assert.That(res.Sections, Is.Not.Empty);
        });
    }

    [Test]
    public void CanGetArticleSideloadedWithCategories()
    {
        var res = Api.HelpCenter.Articles.GetArticles(ArticleSideLoadOptionsEnum.Categories);
        Assert.Multiple(() =>
        {
            Assert.That(res.Articles, Is.Not.Empty);
            Assert.That(res.Categories, Is.Not.Empty);
        });
    }

    [Test]
    public void CanGetArticleSideloadedWithTranslations()
    {
        var res = Api.HelpCenter.Articles.GetArticles(ArticleSideLoadOptionsEnum.Categories | ArticleSideLoadOptionsEnum.Sections | ArticleSideLoadOptionsEnum.Users | ArticleSideLoadOptionsEnum.Translations);
        Assert.Multiple(() =>
        {
            Assert.That(res.Categories[0].Translations, Is.Not.Empty);
            Assert.That(res.Articles[0].Translations, Is.Not.Empty);
            Assert.That(res.Sections[0].Translations, Is.Not.Empty);
        });
    }

    [Test]
    public void CanGetArticleByCategoryWithSideloadedSections()
    {
        var firstCategory = Api.HelpCenter.Categories.GetCategoryById(200382245).Category;
        var res = Api.HelpCenter.Articles.GetArticlesByCategoryId(firstCategory.Id.Value, ArticleSideLoadOptionsEnum.Sections);

        Assert.That(res.Sections, Is.Not.Empty);
    }

    [Test]
    public void CanGetArticlesSorted()
    {
        var articlesAscending = Api.HelpCenter.Articles.GetArticles(ArticleSideLoadOptionsEnum.None, new ArticleSortingOptions() { SortBy = ArticleSortEnum.Title });
        var articlesDescending = Api.HelpCenter.Articles.GetArticles(ArticleSideLoadOptionsEnum.None, new ArticleSortingOptions() { SortBy = ArticleSortEnum.Title, SortOrder = ArticleSortOrderEnum.Desc });

        Assert.That(articlesAscending.Articles[0].Title, Is.Not.EqualTo(articlesDescending.Articles[0].Title));
    }

    [Test]
    public void CanGetArticlesSortedInASection()
    {
        var section = Api.HelpCenter.Sections.GetSectionById(sectionId).Section;

        var articlesAscending = Api.HelpCenter.Articles.GetArticlesBySectionId(section.Id.Value, ArticleSideLoadOptionsEnum.None,
            new ArticleSortingOptions() { SortBy = ArticleSortEnum.Title });
        var articlesDescending = Api.HelpCenter.Articles.GetArticlesBySectionId(section.Id.Value, ArticleSideLoadOptionsEnum.None,
            new ArticleSortingOptions() { SortBy = ArticleSortEnum.Title, SortOrder = ArticleSortOrderEnum.Desc });

        Assert.That(articlesAscending.Articles[0].Title, Is.Not.EqualTo(articlesDescending.Articles[0].Title));
    }

    /// <summary>
    /// This throws a 500 error, no idea why, ticket into Zendesk
    /// </summary>
    [Test]
    public void CanGetArticlesSortedInACategory()
    {
        var category = Api.HelpCenter.Categories.GetCategoryById(200382245).Category;
        var articlesAscending = Api.HelpCenter.Articles.GetArticlesByCategoryId(category.Id.Value, ArticleSideLoadOptionsEnum.None, new ArticleSortingOptions() { SortBy = ArticleSortEnum.Title });
        var articlesDescending = Api.HelpCenter.Articles.GetArticlesByCategoryId(category.Id.Value, ArticleSideLoadOptionsEnum.None, new ArticleSortingOptions() { SortBy = ArticleSortEnum.Title, SortOrder = ArticleSortOrderEnum.Desc });

        Assert.That(articlesAscending.Articles[0].Title, Is.Not.EqualTo(articlesDescending.Articles[0].Title));
    }

    [Test]
    public void CanCreateUpdateAndDeleteArticles()
    {
        var resSections = Api.HelpCenter.Sections.GetSections();
        var res = Api.HelpCenter.Articles.CreateArticle(resSections.Sections[0].Id.Value, new Article()
        {
            Title = "My Test article",
            Body = "The body of my article",
            Locale = "en-us",
            PermissionGroupId = 221866,
        });
        Assert.That(res.Article.Id, Is.GreaterThan(0));

        res.Article.LabelNames = new string[] { "updated" };
        var update = Api.HelpCenter.Articles.UpdateArticleAsync(res.Article).Result;
        Assert.Multiple(() =>
        {
            Assert.That(update.Article.LabelNames, Is.EqualTo(res.Article.LabelNames));

            Assert.That(Api.HelpCenter.Articles.DeleteArticle(res.Article.Id.Value), Is.True);
        });
    }

    [Test]
    public void CanGetSingleArticleWithTranslationsAsync()
    {
        var res = Api.HelpCenter.Articles.GetArticleAsync(_articleIdWithComments, ArticleSideLoadOptionsEnum.Translations).Result;
        Assert.That(res.Article, Is.Not.Null);
        Assert.That(res.Article.Translations, Is.Not.Empty);
    }

    [Test]
    public async Task CanGetArticlesAsync()
    {
        var res = await Api.HelpCenter.Articles.GetArticlesAsync();
        Assert.That(res.Count, Is.GreaterThan(0));

        await Api.HelpCenter.Sections.GetSectionsAsync();
        var res1 = await Api.HelpCenter.Articles.GetArticlesBySectionIdAsync(sectionId);
        Assert.That(res1.Articles[0].SectionId, Is.EqualTo(sectionId));
    }

    [Test]
    public async Task CanCreateUpdateAndDeleteArticlesAsync()
    {
        var resSections = await Api.HelpCenter.Sections.GetSectionsAsync();
        var res = await Api.HelpCenter.Articles.CreateArticleAsync(resSections.Sections[0].Id.Value, new Article
        {
            Title = "My Test article",
            Body = "The body of my article",
            Locale = "en-us",
            PermissionGroupId = 221866
        });

        Assert.That(res.Article.Id, Is.GreaterThan(0));

        res.Article.LabelNames = new string[] { "photo", "tripod" };
        var update = await Api.HelpCenter.Articles.UpdateArticleAsync(res.Article);
        Assert.Multiple(async () =>
        {
            Assert.That(res.Article.LabelNames, Is.EqualTo(update.Article.LabelNames));

            Assert.That(await Api.HelpCenter.Articles.DeleteArticleAsync(res.Article.Id.Value), Is.True);
        });
    }

    [Test]
    public async Task CanGetSecondPageUisngGetByPageUrl()
    {
        var pageSize = 3;

        var res = await Api.HelpCenter.Articles.GetArticlesAsync(perPage: pageSize);
        Assert.That(res.PageSize, Is.EqualTo(pageSize));

        var resp = await Api.HelpCenter.Articles.GetByPageUrlAsync<GroupArticleResponse>(res.NextPage, pageSize);
        Assert.That(resp.Page, Is.EqualTo(2));
    }

    [Test]
    public async Task CanSearchForArticlesAsync()
    {
        var resp = await Api.HelpCenter.Articles.SearchArticlesForAsync("Test", createdBefore: DateTime.Now);

        Assert.That(resp.Count, Is.GreaterThan(0));
    }

    [Test]
    public void CanSearchForArticles()
    {
        var resp = Api.HelpCenter.Articles.SearchArticlesFor("Test", createdBefore: DateTime.Now);

        Assert.That(resp.Count, Is.GreaterThan(0));
    }

    [Test]
    public void CanGetDateStringWhenSearchingArticle()
    {
        var response = Api.HelpCenter.Articles.GetArticle(_articleIdWithComments);
        var expectedArticle = response.Article;
        var searchRes = Api.HelpCenter.Articles.SearchArticlesFor("Test", createdBefore: DateTime.Now);
        var resultArticle = searchRes.Results.First(res => res.Id == _articleIdWithComments);
        Assert.Multiple(() =>
        {
            Assert.That(expectedArticle.CreatedAt.ToString("yyyy-MM-ddTHH:mm:ssZ"), Is.EqualTo(resultArticle.CreatedAt));
            Assert.That(expectedArticle.EditedAt.ToString("yyyy-MM-ddTHH:mm:ssZ"), Is.EqualTo(resultArticle.EditedAt));
            Assert.That(expectedArticle.UpdatedAt.ToString("yyyy-MM-ddTHH:mm:ssZ"), Is.EqualTo(resultArticle.UpdatedAt));
        });
    }

    [Test]
    public void CanDeserializeDatesCorrectly()
    {
        var defaultDate = new DateTimeOffset();

        var res = Api.HelpCenter.Articles.GetArticle(_articleIdWithComments);
        Assert.Multiple(() =>
        {
            Assert.That(res.Article.CreatedAt, Is.Not.EqualTo(defaultDate));
            Assert.That(res.Article.EditedAt, Is.Not.EqualTo(defaultDate));
            Assert.That(res.Article.UpdatedAt, Is.Not.EqualTo(defaultDate));
        });
    }
}
