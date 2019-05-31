using System.Threading.Tasks;
using NUnit.Framework;
using ZendeskApi_v2;
using ZendeskApi_v2.Models.Articles;
using ZendeskApi_v2.Models.HelpCenter.Post;
using ZendeskApi_v2.Models.HelpCenter.Subscriptions;
using ZendeskApi_v2.Models.HelpCenter.Topics;
using ZendeskApi_v2.Models.Sections;

namespace Tests.HelpCenter
{
    [TestFixture]
    [Category("HelpCenter")]
    public class SubscriptionTests
    {
        private const string ARTICLE_TITLE = "Subscription Testing Please Delete";
        private const string SECTION_NAME = "Subscription Test Section";
        private const string TOPIC_NAME = "Subscription Testing Please Delete";
        private const string POST_TITLE = "Subscription Testing Please Delete";
        private const string LOCALE = "en-us";
        private readonly ZendeskApi api = new ZendeskApi(Settings.Site, Settings.AdminEmail, Settings.AdminPassword);
        private Section section;
        private Article article;
        private Topic topic;
        private Post post;

        [OneTimeSetUp]
        public async Task SetupAsync()
        {
            var articlesResp = await api.HelpCenter.Articles.SearchArticlesForAsync(ARTICLE_TITLE);
            do
            {
                foreach (var result in articlesResp.Results)
                {
                    await api.HelpCenter.Articles.DeleteArticleAsync(result.Id.Value);
                }

                if (!string.IsNullOrWhiteSpace(articlesResp.NextPage))
                {
                    articlesResp = await api.HelpCenter.Articles.GetByPageUrlAsync<ArticleSearchResults>(articlesResp.NextPage, 100);
                }
            } while (articlesResp.Results.Count > 0);

            var sectionsResp = await api.HelpCenter.Sections.GetSectionsAsync();
            do
            {
                foreach (var section in sectionsResp.Sections)
                {
                    if (section.Name == SECTION_NAME)
                    {
                        await api.HelpCenter.Sections.DeleteSectionAsync(section.Id.Value);
                    }
                }

                if (!string.IsNullOrWhiteSpace(sectionsResp.NextPage))
                {
                    sectionsResp = await api.HelpCenter.Articles.GetByPageUrlAsync<GroupSectionResponse>(sectionsResp.NextPage, 100);
                }
            } while (!string.IsNullOrWhiteSpace(sectionsResp.NextPage));

            var usersSubscriptions = await api.Users.GetSubscriptionsAsync(Settings.UserId, SubscriptionSideLoadOptions.None, 100, 1);
            do
            {
                foreach (var subscription in usersSubscriptions.Subscriptions)
                {
                    switch (subscription.ContentType)
                    {
                        case "Article":
                            await api.HelpCenter.Articles.DeleteSubscriptionAsync(subscription.ContentId, subscription.Id.Value);
                            break;
                        case "Section":
                            await api.HelpCenter.Sections.DeleteSubscriptionAsync(subscription.ContentId, subscription.Id.Value);
                            break;
                        case "Post":
                            await api.HelpCenter.Posts.DeleteSubscriptionAsync(subscription.ContentId, subscription.Id.Value);
                            break;
                        case "Topic":
                            await api.HelpCenter.Topics.DeleteSubscriptionAsync(subscription.ContentId, subscription.Id.Value);
                            break;
                    }
                }

                if (!string.IsNullOrWhiteSpace(usersSubscriptions.NextPage))
                {
                    usersSubscriptions = await api.Users.GetByPageUrlAsync<GroupSubscriptionsResponse>(usersSubscriptions.NextPage, 100);
                }
            } while (!string.IsNullOrWhiteSpace(usersSubscriptions.NextPage));

            var sectionResp = await api.HelpCenter.Sections.CreateSectionAsync(new Section { Name = SECTION_NAME, Locale = LOCALE, CategoryId = Settings.Category_ID });
            section = sectionResp.Section;

            var articleResp = await api.HelpCenter.Articles.CreateArticleAsync(section.Id.Value, new Article { Title = ARTICLE_TITLE });
            article = articleResp.Article;

            var topicResp = await api.HelpCenter.Topics.CreateTopicAsync(new Topic { Name = TOPIC_NAME });
            topic = topicResp.Topic;

            var postResp = await api.HelpCenter.Posts.CreatePostAsync(new Post { Title = POST_TITLE, TopicId = topic.Id.Value });
            post = postResp.Post;

            await Task.Delay(100);
        }

        [OneTimeTearDown]
        public async Task CleanUpAsync()
        {
            await api.HelpCenter.Articles.DeleteArticleAsync(article.Id.Value);
            await api.HelpCenter.Sections.DeleteSectionAsync(section.Id.Value);
            await api.HelpCenter.Posts.DeletePostAsync(post.Id.Value);
            await api.HelpCenter.Topics.DeleteTopicAsync(topic.Id.Value);
        }

        [Test]
        public async Task CanCreateArticleSubscriptionAsync()
        {
            var resp = await api.HelpCenter.Articles.CreateSubscriptionAsync(article.Id.Value, new ArticleSubscription(LOCALE));

            Assert.That(resp.Subscription, Is.Not.Null);
        }

        [Test]
        public async Task CanGetArticleSubscriptionAsync()
        {
            var resp = await api.HelpCenter.Articles.CreateSubscriptionAsync(article.Id.Value, new ArticleSubscription(LOCALE));

            var listResp = await api.HelpCenter.Articles.GetSubscriptionAsync(article.Id.Value, resp.Subscription.Id.Value, SubscriptionSideLoadOptions.Articles | SubscriptionSideLoadOptions.Sections);

            Assert.That(listResp.Subscription, Is.Not.Null);
            Assert.That(listResp.Articles.Count, Is.GreaterThan(0));
            Assert.That(listResp.Sections.Count, Is.GreaterThan(0));
        }

        [Test]
        public async Task CanGetArticlesSubscriptionAsync()
        {
            var resp = await api.HelpCenter.Articles.CreateSubscriptionAsync(article.Id.Value, new ArticleSubscription(LOCALE));

            var listResp = await api.HelpCenter.Articles.GetSubscriptionsAsync(article.Id.Value, SubscriptionSideLoadOptions.Articles | SubscriptionSideLoadOptions.Sections);

            Assert.That(listResp.Subscriptions, Is.Not.Null);
            Assert.That(listResp.Articles, Is.Not.Null);
        }

        [Test]
        public async Task CanDeleteArticleSubscriptionAsync()
        {
            var resp = await api.HelpCenter.Articles.CreateSubscriptionAsync(article.Id.Value, new ArticleSubscription(LOCALE));

            Assert.That(await api.HelpCenter.Articles.DeleteSubscriptionAsync(article.Id.Value, resp.Subscription.Id.Value), Is.True);
        }

        [Test]
        public async Task CanCreateSectionSubscriptionAsync()
        {
            var resp = await api.HelpCenter.Sections.CreateSubscriptionAsync(section.Id.Value, new SectionSubscription(LOCALE));

            Assert.That(resp.Subscription, Is.Not.Null);
        }

        [Test]
        public async Task CanGetSectionSubscriptionAsync()
        {
            var resp = await api.HelpCenter.Sections.CreateSubscriptionAsync(section.Id.Value, new SectionSubscription(LOCALE));

            var listResp = await api.HelpCenter.Sections.GetSubscriptionAsync(section.Id.Value, resp.Subscription.Id.Value, SubscriptionSideLoadOptions.Sections);

            Assert.That(resp.Subscription, Is.Not.Null);
            Assert.That(listResp.Sections.Count, Is.GreaterThan(0));
        }

        [Test]
        public async Task CanDeleteSectionsSubscriptionAsync()
        {
            var resp = await api.HelpCenter.Sections.CreateSubscriptionAsync(section.Id.Value, new SectionSubscription(LOCALE));

            Assert.That(await api.HelpCenter.Sections.DeleteSubscriptionAsync(section.Id.Value, resp.Subscription.Id.Value), Is.True);
        }

        [Test]
        public async Task CanListUserSubscriptionsAsync()
        {
            var resp = await api.HelpCenter.Articles.CreateSubscriptionAsync(article.Id.Value, new ArticleSubscription(LOCALE) { UserId = Settings.UserId });

            var listResp = await api.Users.GetSubscriptionsAsync(Settings.UserId);

            Assert.That(listResp.Subscriptions.Count, Is.GreaterThan(0));
        }

        [Test]
        public async Task CanCreatePostSubscriptionAsync()
        {
            var resp = await api.HelpCenter.Posts.CreateSubscriptionAsync(post.Id.Value, new Subscription { Locale = LOCALE });

            Assert.That(resp.Subscription, Is.Not.Null);
        }

        [Test]
        public async Task CanGetPostSubscriptionAsync()
        {
            var resp = await api.HelpCenter.Posts.CreateSubscriptionAsync(post.Id.Value, new Subscription { Locale = LOCALE });

            var listResp = await api.HelpCenter.Posts.GetSubscriptionAsync(post.Id.Value, resp.Subscription.Id.Value, SubscriptionSideLoadOptions.Users);

            Assert.That(resp.Subscription, Is.Not.Null);
            Assert.That(listResp.Users.Count, Is.GreaterThan(0));
        }

        [Test]
        public async Task CanGetPostSubscriptionsAsync()
        {
            var resp = await api.HelpCenter.Posts.CreateSubscriptionAsync(post.Id.Value, new Subscription { Locale = LOCALE });

            var listResp = await api.HelpCenter.Posts.GetSubscriptionsAsync(post.Id.Value, SubscriptionSideLoadOptions.Users);

            Assert.That(resp.Subscription, Is.Not.Null);
            Assert.That(listResp.Users.Count, Is.GreaterThan(0));
        }

        [Test]
        public async Task CanDeletePostSubscriptionAsync()
        {
            var resp = await api.HelpCenter.Posts.CreateSubscriptionAsync(post.Id.Value, new Subscription { Locale = LOCALE });

            Assert.That(await api.HelpCenter.Posts.DeleteSubscriptionAsync(post.Id.Value, resp.Subscription.Id.Value), Is.True);
        }

        [Test]
        public async Task CanCreateTopicSubscriptionAsync()
        {
            var resp = await api.HelpCenter.Topics.CreateSubscriptionAsync(topic.Id.Value, new Subscription { Locale = LOCALE, IncludeComments = true });

            Assert.That(resp.Subscription, Is.Not.Null);
            Assert.That(resp.Subscription.IncludeComments, Is.True);
        }

        [Test]
        public async Task CanGetTopicSubscriptionAsync()
        {
            var resp = await api.HelpCenter.Topics.CreateSubscriptionAsync(topic.Id.Value, new Subscription { Locale = LOCALE });

            var listResp = await api.HelpCenter.Topics.GetSubscriptionAsync(topic.Id.Value, resp.Subscription.Id.Value, SubscriptionSideLoadOptions.Users);

            Assert.That(resp.Subscription, Is.Not.Null);
            Assert.That(listResp.Users.Count, Is.GreaterThan(0));
        }

        [Test]
        public async Task CanGetTopicSubscriptionsAsync()
        {
            var resp = await api.HelpCenter.Topics.CreateSubscriptionAsync(topic.Id.Value, new Subscription { Locale = LOCALE });

            var listResp = await api.HelpCenter.Topics.GetSubscriptionsAsync(topic.Id.Value, SubscriptionSideLoadOptions.Users);

            Assert.That(resp.Subscription, Is.Not.Null);
            Assert.That(listResp.Users.Count, Is.GreaterThan(0));
        }

        [Test]
        public async Task CanUpdateTopicSubscriptionAsync()
        {
            var resp = await api.HelpCenter.Topics.CreateSubscriptionAsync(topic.Id.Value, new Subscription { Locale = LOCALE, IncludeComments = true });
            var sub = resp.Subscription;
            sub.IncludeComments = false;

            var updateResp = await api.HelpCenter.Topics.UpdateSubscriptionAsync(topic.Id.Value, sub);
            Assert.That(updateResp.Subscription, Is.Not.Null);
            Assert.That(updateResp.Subscription.IncludeComments, Is.False);
        }

        [Test]
        public async Task CanDeleteTopicSubscriptionAsync()
        {
            var resp = await api.HelpCenter.Topics.CreateSubscriptionAsync(topic.Id.Value, new Subscription { Locale = LOCALE });

            Assert.That(await api.HelpCenter.Topics.DeleteSubscriptionAsync(topic.Id.Value, resp.Subscription.Id.Value), Is.True);
        }
    }
}