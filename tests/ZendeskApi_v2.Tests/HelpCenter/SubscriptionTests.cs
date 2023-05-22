using NUnit.Framework;
using System.Threading.Tasks;
using ZendeskApi_v2.Models.Articles;
using ZendeskApi_v2.Models.HelpCenter.Post;
using ZendeskApi_v2.Models.HelpCenter.Subscriptions;
using ZendeskApi_v2.Models.HelpCenter.Topics;
using ZendeskApi_v2.Models.Sections;
using ZendeskApi_v2.Tests.Base;

namespace ZendeskApi_v2.Tests.HelpCenter;

[TestFixture]
[Category("HelpCenter")]
public class SubscriptionTests : TestBase
{
    private const string ARTICLE_TITLE = "Subscription Testing Please Delete";
    private const string SECTION_NAME = "Subscription Test Section";
    private const string TOPIC_NAME = "Subscription Testing Please Delete";
    private const string POST_TITLE = "Subscription Testing Please Delete";
    private const string LOCALE = "en-us";
    private Section section;
    private Article article;
    private Topic topic;
    private Post post;

    [OneTimeSetUp]
    public async Task SetupAsync()
    {
        var articlesResp = await Api.HelpCenter.Articles.SearchArticlesForAsync(ARTICLE_TITLE);
        do
        {
            foreach (var result in articlesResp.Results)
            {
                await Api.HelpCenter.Articles.DeleteArticleAsync(result.Id.Value);
            }

            if (!string.IsNullOrWhiteSpace(articlesResp.NextPage))
            {
                articlesResp = await Api.HelpCenter.Articles.GetByPageUrlAsync<ArticleSearchResults>(articlesResp.NextPage, 100);
            }
        } while (articlesResp.Results.Count > 0);

        var sectionsResp = await Api.HelpCenter.Sections.GetSectionsAsync();
        do
        {
            foreach (var section in sectionsResp.Sections)
            {
                if (section.Name == SECTION_NAME)
                {
                    await Api.HelpCenter.Sections.DeleteSectionAsync(section.Id.Value);
                }
            }

            if (!string.IsNullOrWhiteSpace(sectionsResp.NextPage))
            {
                sectionsResp = await Api.HelpCenter.Articles.GetByPageUrlAsync<GroupSectionResponse>(sectionsResp.NextPage, 100);
            }
        } while (!string.IsNullOrWhiteSpace(sectionsResp.NextPage));

        var usersSubscriptions = await Api.Users.GetSubscriptionsAsync(Admin.ID, SubscriptionSideLoadOptions.None, 100, 1);
        do
        {
            foreach (var subscription in usersSubscriptions.Subscriptions)
            {
                switch (subscription.ContentType)
                {
                    case "Article":
                        await Api.HelpCenter.Articles.DeleteSubscriptionAsync(subscription.ContentId, subscription.Id.Value);
                        break;
                    case "Section":
                        await Api.HelpCenter.Sections.DeleteSubscriptionAsync(subscription.ContentId, subscription.Id.Value);
                        break;
                    case "Post":
                        await Api.HelpCenter.Posts.DeleteSubscriptionAsync(subscription.ContentId, subscription.Id.Value);
                        break;
                    case "Topic":
                        await Api.HelpCenter.Topics.DeleteSubscriptionAsync(subscription.ContentId, subscription.Id.Value);
                        break;
                }
            }

            if (!string.IsNullOrWhiteSpace(usersSubscriptions.NextPage))
            {
                usersSubscriptions = await Api.Users.GetByPageUrlAsync<GroupSubscriptionsResponse>(usersSubscriptions.NextPage, 100);
            }
        } while (!string.IsNullOrWhiteSpace(usersSubscriptions.NextPage));

        var sectionResp = await Api.HelpCenter.Sections.CreateSectionAsync(new Section { Name = SECTION_NAME, Locale = LOCALE, CategoryId = Settings.Category_ID });
        section = sectionResp.Section;

        var articleResp = await Api.HelpCenter.Articles.CreateArticleAsync(section.Id.Value, new Article { Title = ARTICLE_TITLE });
        article = articleResp.Article;

        var topicResp = await Api.HelpCenter.Topics.CreateTopicAsync(new Topic { Name = TOPIC_NAME });
        topic = topicResp.Topic;

        var postResp = await Api.HelpCenter.Posts.CreatePostAsync(new Post { Title = POST_TITLE, TopicId = topic.Id.Value });
        post = postResp.Post;
        await Task.Delay(10);
    }

    [OneTimeTearDown]
    public async Task CleanUpAsync()
    {
        await Api.HelpCenter.Articles.DeleteArticleAsync(article.Id.Value);
        await Api.HelpCenter.Sections.DeleteSectionAsync(section.Id.Value);
        await Api.HelpCenter.Posts.DeletePostAsync(post.Id.Value);
        await Api.HelpCenter.Topics.DeleteTopicAsync(topic.Id.Value);
    }

    [Test]
    public async Task CanCreateArticleSubscriptionAsync()
    {
        var resp = await Api.HelpCenter.Articles.CreateSubscriptionAsync(article.Id.Value, new ArticleSubscription(LOCALE));

        Assert.That(resp.Subscription, Is.Not.Null);
    }

    [Test]
    public async Task CanGetArticleSubscriptionAsync()
    {
        var resp = await Api.HelpCenter.Articles.CreateSubscriptionAsync(article.Id.Value, new ArticleSubscription(LOCALE));

        var listResp = await Api.HelpCenter.Articles.GetSubscriptionAsync(article.Id.Value, resp.Subscription.Id.Value, SubscriptionSideLoadOptions.Articles | SubscriptionSideLoadOptions.Sections);
        Assert.Multiple(() =>
        {
            Assert.That(listResp.Subscription, Is.Not.Null);
            Assert.That(listResp.Articles, Is.Not.Empty);
            Assert.That(listResp.Sections, Is.Not.Empty);
        });
    }

    [Test]
    public async Task CanGetArticlesSubscriptionAsync()
    {
        await Api.HelpCenter.Articles.CreateSubscriptionAsync(article.Id.Value, new ArticleSubscription(LOCALE));

        var listResp = await Api.HelpCenter.Articles.GetSubscriptionsAsync(article.Id.Value, SubscriptionSideLoadOptions.Articles | SubscriptionSideLoadOptions.Sections);
        Assert.Multiple(() =>
        {
            Assert.That(listResp.Subscriptions, Is.Not.Null);
            Assert.That(listResp.Articles, Is.Not.Null);
        });
    }

    [Test]
    public async Task CanDeleteArticleSubscriptionAsync()
    {
        var resp = await Api.HelpCenter.Articles.CreateSubscriptionAsync(article.Id.Value, new ArticleSubscription(LOCALE));

        Assert.That(await Api.HelpCenter.Articles.DeleteSubscriptionAsync(article.Id.Value, resp.Subscription.Id.Value), Is.True);
    }

    [Test]
    public async Task CanCreateSectionSubscriptionAsync()
    {
        var resp = await Api.HelpCenter.Sections.CreateSubscriptionAsync(section.Id.Value, new SectionSubscription(LOCALE));

        Assert.That(resp.Subscription, Is.Not.Null);
    }

    [Test]
    public async Task CanGetSectionSubscriptionAsync()
    {
        var resp = await Api.HelpCenter.Sections.CreateSubscriptionAsync(section.Id.Value, new SectionSubscription(LOCALE));

        var listResp = await Api.HelpCenter.Sections.GetSubscriptionAsync(section.Id.Value, resp.Subscription.Id.Value, SubscriptionSideLoadOptions.Sections);
        Assert.Multiple(() =>
        {
            Assert.That(resp.Subscription, Is.Not.Null);
            Assert.That(listResp.Sections, Is.Not.Empty);
        });
    }

    [Test]
    public async Task CanDeleteSectionsSubscriptionAsync()
    {
        var resp = await Api.HelpCenter.Sections.CreateSubscriptionAsync(section.Id.Value, new SectionSubscription(LOCALE));

        Assert.That(await Api.HelpCenter.Sections.DeleteSubscriptionAsync(section.Id.Value, resp.Subscription.Id.Value), Is.True);
    }

    [Test]
    public async Task CanListUserSubscriptionsAsync()
    {
        await Api.HelpCenter.Articles.CreateSubscriptionAsync(article.Id.Value, new ArticleSubscription(LOCALE) { UserId = Admin.ID });

        var listResp = await Api.Users.GetSubscriptionsAsync(Admin.ID);

        Assert.That(listResp.Subscriptions, Is.Not.Empty);
    }

    [Test]
    public async Task CanCreatePostSubscriptionAsync()
    {
        var resp = await Api.HelpCenter.Posts.CreateSubscriptionAsync(post.Id.Value, new Subscription { Locale = LOCALE });

        Assert.That(resp.Subscription, Is.Not.Null);
    }

    [Test]
    public async Task CanGetPostSubscriptionAsync()
    {
        var resp = await Api.HelpCenter.Posts.CreateSubscriptionAsync(post.Id.Value, new Subscription { Locale = LOCALE });

        var listResp = await Api.HelpCenter.Posts.GetSubscriptionAsync(post.Id.Value, resp.Subscription.Id.Value, SubscriptionSideLoadOptions.Users);
        Assert.Multiple(() =>
        {
            Assert.That(resp.Subscription, Is.Not.Null);
            Assert.That(listResp.Users, Is.Not.Empty);
        });
    }

    [Test]
    public async Task CanGetPostSubscriptionsAsync()
    {
        var resp = await Api.HelpCenter.Posts.CreateSubscriptionAsync(post.Id.Value, new Subscription { Locale = LOCALE });

        var listResp = await Api.HelpCenter.Posts.GetSubscriptionsAsync(post.Id.Value, SubscriptionSideLoadOptions.Users);
        Assert.Multiple(() =>
        {
            Assert.That(resp.Subscription, Is.Not.Null);
            Assert.That(listResp.Users, Is.Not.Empty);
        });
    }

    [Test]
    public async Task CanDeletePostSubscriptionAsync()
    {
        var resp = await Api.HelpCenter.Posts.CreateSubscriptionAsync(post.Id.Value, new Subscription { Locale = LOCALE });

        Assert.That(await Api.HelpCenter.Posts.DeleteSubscriptionAsync(post.Id.Value, resp.Subscription.Id.Value), Is.True);
    }

    [Test]
    public async Task CanCreateTopicSubscriptionAsync()
    {
        var resp = await Api.HelpCenter.Topics.CreateSubscriptionAsync(topic.Id.Value, new Subscription { Locale = LOCALE, IncludeComments = true });

        Assert.That(resp.Subscription, Is.Not.Null);
        Assert.That(resp.Subscription.IncludeComments, Is.True);
    }

    [Test]
    public async Task CanGetTopicSubscriptionAsync()
    {
        var resp = await Api.HelpCenter.Topics.CreateSubscriptionAsync(topic.Id.Value, new Subscription { Locale = LOCALE });

        var listResp = await Api.HelpCenter.Topics.GetSubscriptionAsync(topic.Id.Value, resp.Subscription.Id.Value, SubscriptionSideLoadOptions.Users);
        Assert.Multiple(() =>
        {
            Assert.That(resp.Subscription, Is.Not.Null);
            Assert.That(listResp.Users, Is.Not.Empty);
        });
    }

    [Test]
    public async Task CanGetTopicSubscriptionsAsync()
    {
        var resp = await Api.HelpCenter.Topics.CreateSubscriptionAsync(topic.Id.Value, new Subscription { Locale = LOCALE });

        var listResp = await Api.HelpCenter.Topics.GetSubscriptionsAsync(topic.Id.Value, SubscriptionSideLoadOptions.Users);
        Assert.Multiple(() =>
        {
            Assert.That(resp.Subscription, Is.Not.Null);
            Assert.That(listResp.Users, Is.Not.Empty);
        });
    }

    [Test]
    public async Task CanUpdateTopicSubscriptionAsync()
    {
        var resp = await Api.HelpCenter.Topics.CreateSubscriptionAsync(topic.Id.Value, new Subscription { Locale = LOCALE, IncludeComments = true });
        var sub = resp.Subscription;
        sub.IncludeComments = false;

        var updateResp = await Api.HelpCenter.Topics.UpdateSubscriptionAsync(topic.Id.Value, sub);
        Assert.That(updateResp.Subscription, Is.Not.Null);
        Assert.That(updateResp.Subscription.IncludeComments, Is.False);
    }

    [Test]
    public async Task CanDeleteTopicSubscriptionAsync()
    {
        var resp = await Api.HelpCenter.Topics.CreateSubscriptionAsync(topic.Id.Value, new Subscription { Locale = LOCALE });

        Assert.That(await Api.HelpCenter.Topics.DeleteSubscriptionAsync(topic.Id.Value, resp.Subscription.Id.Value), Is.True);
    }
}