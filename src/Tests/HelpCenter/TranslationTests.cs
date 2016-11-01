using System.Threading.Tasks;
using NUnit.Framework;
using ZendeskApi_v2;
using ZendeskApi_v2.Models.Articles;
using ZendeskApi_v2.Models.HelpCenter.Categories;
using ZendeskApi_v2.Models.HelpCenter.Translations;
using ZendeskApi_v2.Models.Sections;

namespace Tests.HelpCenter
{
    [TestFixture]
    [Category("HelpCenter")]
    public class TranslationTests
    {
        private ZendeskApi api = new ZendeskApi(Settings.Site, Settings.Email, Settings.Password);
        private long _articleId = 204838115; //https://csharpapi.zendesk.com/hc/en-us/articles/204838115-Thing-4?page=1#comment_200486479
        private long _sectionId = 201010935;
        private long _categoryId = 200382245;

        [Test]
        public void CanListTranslations()
        {
            var res = api.HelpCenter.Translations.ListTranslationsForArticle(_articleId);
            Assert.AreEqual(2, res.Count);

            res = api.HelpCenter.Translations.ListTranslationsForSection(_sectionId);
            Assert.AreEqual(2, res.Count);

            res = api.HelpCenter.Translations.ListTranslationsForCategory(_categoryId);
            Assert.AreEqual(2, res.Count);
        }

        [Test]
        public void CanShowTranslationForArticle()
        {
            var res = api.HelpCenter.Translations.ShowTranslationForArticle(_articleId, "en-us");
            Assert.AreEqual("en-us", res.Translation.Locale);
        }

        [Test]
        public void CanListMissingCreateUpdateAndDeleteTranslationsForArticle()
        {
            //create an article with en-us locale.
            //verify that fr is missing.
            //add a translation and verify.
            //update translation and verify.
            //delete translation and verify.
            //delete new article.

            //prep
            var resSections = api.HelpCenter.Sections.GetSections();
            var new_article_res = api.HelpCenter.Articles.CreateArticle(resSections.Sections[0].Id.Value, new ZendeskApi_v2.Models.Articles.Article()
            {
                Title = "My Test article for translations",
                Body = "The body of my article",
                Locale = "en-us"
            });
            long article_id = new_article_res.Article.Id.Value;

            var missing_res = api.HelpCenter.Translations.ListMissingTranslationsForArticle(article_id);
            Assert.AreEqual(1, missing_res.Count);
            Assert.AreEqual("fr", missing_res[0]);

            Translation fr_translation = new Translation()
            {
                Body = "Je ne parle pas français.",
                Title = "Mon article de test pour les traductions",
                Locale = "fr"
            };

            //create translation
            var add_res = api.HelpCenter.Translations.CreateArticleTranslation(article_id, fr_translation);
            Assert.Greater(add_res.Translation.Id, 0);

            add_res.Translation.Body = "insérer plus français ici .";

            //update translation
            var update_res = api.HelpCenter.Translations.UpdateArticleTranslation(add_res.Translation);
            Assert.AreEqual("insérer plus français ici .", update_res.Translation.Body);

            //delete translation
            Assert.IsTrue(api.HelpCenter.Translations.DeleteTranslation(update_res.Translation.Id.Value));

            //teardown.
            Assert.IsTrue(api.HelpCenter.Articles.DeleteArticle(article_id));
        }

        [Test]
        public void CanListMissingCreateUpdateAndDeleteTranslationsForSection()
        {
            //create a section with en-us locale.
            //verify that fr is missing.
            //add a translation and verify.
            //update translation and verify.
            //delete translation and verify.
            //delete new section.

            //prep
            var resCategoies = api.HelpCenter.Categories.GetCategories();
            var new_section_res = api.HelpCenter.Sections.CreateSection(new ZendeskApi_v2.Models.Sections.Section()
            {
                Name = "My Test section for translations",
                Description = "The body of my section (en-us)",
                Locale = "en-us",
                CategoryId = resCategoies.Categories[0].Id.Value
            });
            long section_id = new_section_res.Section.Id.Value;

            var missing_res = api.HelpCenter.Translations.ListMissingTranslationsForSection(section_id);
            Assert.AreEqual(1, missing_res.Count);
            Assert.AreEqual("fr", missing_res[0]);

            Translation fr_translation = new Translation()
            {
                Body = "Je ne parle pas français.",
                Title = "french category here",
                Locale = "fr"
            };

            //create translation
            var add_res = api.HelpCenter.Translations.CreateSectionTranslation(section_id, fr_translation);
            Assert.Greater(add_res.Translation.Id, 0);

            add_res.Translation.Body = "insérer plus français ici .";

            //update translation
            var update_res = api.HelpCenter.Translations.UpdateSectionTranslation(add_res.Translation);
            Assert.AreEqual("insérer plus français ici .", update_res.Translation.Body);

            //delete translation
            Assert.IsTrue(api.HelpCenter.Translations.DeleteTranslation(update_res.Translation.Id.Value));

            //teardown.
            Assert.IsTrue(api.HelpCenter.Sections.DeleteSection(section_id));
        }

        [Test]
        public void CanListMissingCreateUpdateAndDeleteTranslationsForCategory()
        {
            //create a category with en-us locale.
            //verify that fr is missing.
            //add a translation and verify.
            //update translation and verify.
            //delete translation and verify.
            //delete new category.

            //prep
            var new_category_res = api.HelpCenter.Categories.CreateCategory(new ZendeskApi_v2.Models.HelpCenter.Categories.Category()
            {
                Name = "My Test category for translations",
                Description = "The body of my category (en-us)",
                Locale = "en-us"
            });
            long category_id = new_category_res.Category.Id.Value;

            var missing_res = api.HelpCenter.Translations.ListMissingTranslationsForCategory(category_id);
            Assert.AreEqual(1, missing_res.Count);
            Assert.AreEqual("fr", missing_res[0]);

            Translation fr_translation = new Translation()
            {
                Body = "Je ne parle pas français.",
                Title = "french for 'this is a french category'",
                Locale = "fr"
            };

            //create translation
            var add_res = api.HelpCenter.Translations.CreateCategoryTranslation(category_id, fr_translation);
            Assert.Greater(add_res.Translation.Id, 0);

            add_res.Translation.Body = "insérer plus français ici . (category)";

            //update translation
            var update_res = api.HelpCenter.Translations.UpdateCategoryTranslation(add_res.Translation);
            Assert.AreEqual("insérer plus français ici . (category)", update_res.Translation.Body);

            //delete translation
            Assert.IsTrue(api.HelpCenter.Translations.DeleteTranslation(update_res.Translation.Id.Value));

            //teardown.
            Assert.IsTrue(api.HelpCenter.Categories.DeleteCategory(category_id));
        }

        [Test]
        public void CanListAllEnabledLocales()
        {
            //the only two locales enabled on the test site are us-en and fr. us-en is the default.
            //note: FR was already enabled in the Zendesk settings, however it had to be enabled again in the help center preferences.
            string default_locale;
            var res = api.HelpCenter.Translations.ListAllEnabledLocalesAndDefaultLocale(out default_locale);

            Assert.AreEqual(default_locale, "en-us");
            Assert.IsTrue(res.Contains("en-us"));
            Assert.IsTrue(res.Contains("fr"));
        }

        //Async tests:

        [Test]
        public async Task CanListTranslationsAsync()
        {
            var res = await api.HelpCenter.Translations.ListTranslationsForArticleAsync(_articleId);
            Assert.That(res.Count, Is.EqualTo(2));

            res = await api.HelpCenter.Translations.ListTranslationsForSectionAsync(_sectionId);
            Assert.That(res.Count, Is.EqualTo(2));

            res = await api.HelpCenter.Translations.ListTranslationsForCategoryAsync(_categoryId);
            Assert.That(res.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task CanShowTranslationForArticleAsync()
        {
            var res = await api.HelpCenter.Translations.ShowTranslationForArticleAsync(_articleId, "en-us");
            Assert.That(res.Translation.Locale, Is.EqualTo("en-us"));
        }

        [Test]
        public async Task CanListMissingCreateUpdateAndDeleteTranslationsForArticleAsync()
        {
            //create an article with en-us locale.
            //verify that fr is missing.
            //add a translation and verify.
            //update translation and verify.
            //delete translation and verify.
            //delete new article.

            //prep
            var resSections = await api.HelpCenter.Sections.GetSectionsAsync();
            var new_article_res = await api.HelpCenter.Articles.CreateArticleAsync(resSections.Sections[0].Id.Value, new Article
            {
                Title = "My Test article for translations",
                Body = "The body of my article",
                Locale = "en-us"
            });
            long article_id = new_article_res.Article.Id.Value;

            var missing_res = await api.HelpCenter.Translations.ListMissingTranslationsForArticleAsync(article_id);
            Assert.That(missing_res.Count, Is.EqualTo(1));
            Assert.That(missing_res[0], Is.EqualTo("fr"));

            Translation fr_translation = new Translation()
            {
                Body = "Je ne parle pas français.",
                Title = "Mon article de test pour les traductions",
                Locale = "fr"
            };

            //create translation
            var add_res = await api.HelpCenter.Translations.CreateArticleTranslationAsync(article_id, fr_translation);
            Assert.That(add_res.Translation.Id, Is.GreaterThan(0));

            add_res.Translation.Body = "insérer plus français ici .";

            //update translation
            var update_res = await api.HelpCenter.Translations.UpdateArticleTranslationAsync(add_res.Translation);
            Assert.That(update_res.Translation.Body, Is.EqualTo("insérer plus français ici ."));

            //delete translation
            Assert.That(await api.HelpCenter.Translations.DeleteTranslationAsync(update_res.Translation.Id.Value), Is.True);

            //tear-down.
            Assert.That(await api.HelpCenter.Articles.DeleteArticleAsync(article_id), Is.True);
        }

        [Test]
        public async Task CanListMissingCreateUpdateAndDeleteTranslationsForSectionAsync()
        {
            //create a section with en-us locale.
            //verify that fr is missing.
            //add a translation and verify.
            //update translation and verify.
            //delete translation and verify.
            //delete new section.

            //prep
            var resCategoies = await api.HelpCenter.Categories.GetCategoriesAsync();
            var new_section_res = await api.HelpCenter.Sections.CreateSectionAsync(new Section
            {
                Name = "My Test section for translations",
                Description = "The body of my section (en-us)",
                Locale = "en-us",
                CategoryId = resCategoies.Categories[0].Id.Value
            });

            long section_id = new_section_res.Section.Id.Value;

            var missing_res = await api.HelpCenter.Translations.ListMissingTranslationsForSectionAsync(section_id);
            Assert.That(missing_res.Count, Is.EqualTo(1));
            Assert.That(missing_res[0], Is.EqualTo("fr"));

            Translation fr_translation = new Translation
            {
                Body = "Je ne parle pas français.",
                Title = "french category here",
                Locale = "fr"
            };

            //create translation
            var add_res = await api.HelpCenter.Translations.CreateSectionTranslationAsync(section_id, fr_translation);
            Assert.That(add_res.Translation.Id, Is.GreaterThan(0));

            add_res.Translation.Body = "insérer plus français ici .";

            //update translation
            var update_res = await api.HelpCenter.Translations.UpdateSectionTranslationAsync(add_res.Translation);
            Assert.That(update_res.Translation.Body, Is.EqualTo("insérer plus français ici ."));

            //delete translation
            Assert.That(await api.HelpCenter.Translations.DeleteTranslationAsync(update_res.Translation.Id.Value), Is.True);

            //tear-down.
            Assert.That(await api.HelpCenter.Sections.DeleteSectionAsync(section_id), Is.True);
        }

        [Test]
        public async Task CanListMissingCreateUpdateAndDeleteTranslationsForCategoryAsync()
        {
            //create a category with en-us locale.
            //verify that fr is missing.
            //add a translation and verify.
            //update translation and verify.
            //delete translation and verify.
            //delete new category.

            //prep
            var new_category_res = await api.HelpCenter.Categories.CreateCategoryAsync(new Category()
            {
                Name = "My Test category for translations",
                Description = "The body of my category (en-us)",
                Locale = "en-us"
            });
            long category_id = new_category_res.Category.Id.Value;

            var missing_res = await api.HelpCenter.Translations.ListMissingTranslationsForCategoryAsync(category_id);
            Assert.That(missing_res.Count, Is.EqualTo(1));
            Assert.That(missing_res[0], Is.EqualTo("fr"));

            Translation fr_translation = new Translation()
            {
                Body = "Je ne parle pas français.",
                Title = "french for 'this is a french category'",
                Locale = "fr"
            };

            //create translation
            var add_res = await api.HelpCenter.Translations.CreateCategoryTranslationAsync(category_id, fr_translation);
            Assert.That(add_res.Translation.Id, Is.GreaterThan(0));

            add_res.Translation.Body = "insérer plus français ici . (category)";

            //update translation
            var update_res = await api.HelpCenter.Translations.UpdateCategoryTranslationAsync(add_res.Translation);
            Assert.That(update_res.Translation.Body, Is.EqualTo("insérer plus français ici . (category)"));

            //delete translation
            Assert.That(await api.HelpCenter.Translations.DeleteTranslationAsync(update_res.Translation.Id.Value), Is.True);

            //tear-down.
            Assert.That(await api.HelpCenter.Categories.DeleteCategoryAsync(category_id), Is.True);
        }

        [Test]
        public async Task CanListAllEnabledLocalesAsync()
        {
            //the only two locales enabled on the test site are us-en and fr. us-en is the default.
            //note: FR was already enabled in the Zendesk settings, however it had to be enabled again in the help center preferences.
            var res = await api.HelpCenter.Translations.ListAllEnabledLocalesAndDefaultLocaleAsync();

            Assert.That(res.Item2, Is.EqualTo("en-us"));
            Assert.That(res.Item1.Contains("en-us"), Is.True);
            Assert.That(res.Item1.Contains("fr"), Is.True);
        }
    }
}