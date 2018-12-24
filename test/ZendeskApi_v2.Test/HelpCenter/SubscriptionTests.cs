﻿using System.Threading.Tasks;
using NUnit.Framework;
using ZendeskApi_v2;
using ZendeskApi_v2.Models.Articles;
using ZendeskApi_v2.Models.HelpCenter.Subscriptions;
using ZendeskApi_v2.Models.Sections;

namespace Tests.HelpCenter
{
    [TestFixture]
    [Category("HelpCenter")]
    public class SubscriptionTests
    {
        private const string ARTICLE_TITLE = "Subscription Testing Please Delete";
        private const string SECTION_NAME = "Subscription Test Section";
        private const string LOCAL = "en-us";
        private readonly ZendeskApi api = new ZendeskApi(Settings.Site, Settings.AdminEmail, Settings.AdminPassword);
        private Section section;
        private Article article;

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

            var sectionResp = await api.HelpCenter.Sections.CreateSectionAsync(new Section { Name = SECTION_NAME, Locale = LOCAL, CategoryId = Settings.Category_ID });
            section = sectionResp.Section;

            var articleResp = await api.HelpCenter.Articles.CreateArticleAsync(section.Id.Value, new Article { Title = ARTICLE_TITLE });
            article = articleResp.Article;
        }

        [OneTimeTearDown]
        public async Task CleanUpAsync()
        {
            await api.HelpCenter.Articles.DeleteArticleAsync(article.Id.Value);
            await api.HelpCenter.Sections.DeleteSectionAsync(section.Id.Value);
        }

        [Test]
        public async Task CanCreateArticleSubscriptionAsync()
        {
            var resp = await api.HelpCenter.Articles.CreateSubscriptionAsync(article.Id.Value, new ArticleSubscription(LOCAL));

            Assert.That(resp.Subscription, Is.Not.Null);
        }

        [Test]
        public async Task CanGetArticleSubscriptionAsync()
        {
            var resp = await api.HelpCenter.Articles.CreateSubscriptionAsync(article.Id.Value, new ArticleSubscription(LOCAL));

            var listResp = await api.HelpCenter.Articles.GetSubscriptionAsync(article.Id.Value, resp.Subscription.Id.Value, SubscriptionSideLoadOptions.Articles | SubscriptionSideLoadOptions.Sections);

            Assert.That(listResp.Subscription, Is.Not.Null);
            Assert.That(listResp.Articles.Count, Is.GreaterThan(0));
            Assert.That(listResp.Sections.Count, Is.GreaterThan(0));
        }

        [Test]
        public async Task CanGetArticlesSubscriptionAsync()
        {
            var resp = await api.HelpCenter.Articles.CreateSubscriptionAsync(article.Id.Value, new ArticleSubscription(LOCAL));

            var listResp = await api.HelpCenter.Articles.GetSubscriptionsAsync(article.Id.Value, SubscriptionSideLoadOptions.Articles | SubscriptionSideLoadOptions.Sections);

            Assert.That(listResp.Subscriptions, Is.Not.Null);
            Assert.That(listResp.Articles, Is.Not.Null);
        }

        [Test]
        public async Task CanDeleteArticleSubscriptionAsync()
        {
            var resp = await api.HelpCenter.Articles.CreateSubscriptionAsync(article.Id.Value, new ArticleSubscription(LOCAL));

            Assert.That(await api.HelpCenter.Articles.DeleteSubscriptionAsync(article.Id.Value, resp.Subscription.Id.Value), Is.True);
        }

        [Test]
        public async Task CanCreateSectionSubscriptionAsync()
        {
            var resp = await api.HelpCenter.Sections.CreateSubscriptionAsync(section.Id.Value, new SectionSubscription(LOCAL));

            Assert.That(resp.Subscription, Is.Not.Null);
        }

        [Test]
        public async Task CanGetSectionSubscriptionAsync()
        {
            var resp = await api.HelpCenter.Sections.CreateSubscriptionAsync(section.Id.Value, new SectionSubscription(LOCAL));

            var listResp = await api.HelpCenter.Sections.GetSubscriptionAsync(section.Id.Value, resp.Subscription.Id.Value, SubscriptionSideLoadOptions.Sections);

            Assert.That(resp.Subscription, Is.Not.Null);
            Assert.That(listResp.Sections.Count, Is.GreaterThan(0));
        }

        [Test]
        public async Task CanDeleteSectionsSubscriptionAsync()
        {
            var resp = await api.HelpCenter.Sections.CreateSubscriptionAsync(section.Id.Value, new SectionSubscription(LOCAL));

            Assert.That(await api.HelpCenter.Sections.DeleteSubscriptionAsync(section.Id.Value, resp.Subscription.Id.Value), Is.True);
        }
    }
}