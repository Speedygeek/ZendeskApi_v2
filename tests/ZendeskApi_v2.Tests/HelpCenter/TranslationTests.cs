using NUnit.Framework;
using System.Threading.Tasks;
using ZendeskApi_v2.Models.Articles;
using ZendeskApi_v2.Models.HelpCenter.Categories;
using ZendeskApi_v2.Models.HelpCenter.Translations;
using ZendeskApi_v2.Models.Sections;
using ZendeskApi_v2.Tests.Base;

namespace ZendeskApi_v2.Tests.HelpCenter;

[TestFixture]
[Category("HelpCenter")]
public class TranslationTests : TestBase
{
    private readonly long _articleId = 360021096471; //https://csharpapi.zendesk.com/hc/en-us/articles/204838115-Thing-4?page=1#comment_200486479
    private readonly long _sectionId = 201010935;
    private readonly long _categoryId = 200382245;

    [Test]
    public void CanListTranslations()
    {
        var res = Api.HelpCenter.Translations.ListTranslationsForArticle(_articleId);
        Assert.That(res.Count, Is.EqualTo(2));

        res = Api.HelpCenter.Translations.ListTranslationsForSection(_sectionId);
        Assert.That(res.Count, Is.EqualTo(2));

        res = Api.HelpCenter.Translations.ListTranslationsForCategory(_categoryId);
        Assert.That(res.Count, Is.EqualTo(2));
    }

    [Test]
    public void CanShowTranslationForArticle()
    {
        var res = Api.HelpCenter.Translations.ShowTranslationForArticle(_articleId, "en-us");
        Assert.That(res.Translation.Locale, Is.EqualTo("en-us"));
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
        var new_article_res = Api.HelpCenter.Articles.CreateArticle(_sectionId, new Article()
        {
            Title = "My Test article for translations",
            Body = "The body of my article",
            Locale = "en-us"
        });
        var article_id = new_article_res.Article.Id.Value;

        var missing_res = Api.HelpCenter.Translations.ListMissingTranslationsForArticle(article_id);
        Assert.That(missing_res, Has.Count.EqualTo(1));
        Assert.That(missing_res[0], Is.EqualTo("fr"));

        var fr_translation = new Translation()
        {
            Body = "Je ne parle pas français.",
            Title = "Mon article de test pour les traductions",
            Locale = "fr"
        };

        //create translation
        var add_res = Api.HelpCenter.Translations.CreateArticleTranslation(article_id, fr_translation);
        Assert.That(add_res.Translation.Id, Is.GreaterThan(0));

        add_res.Translation.Body = "insérer plus français ici .";

        //update translation
        var update_res = Api.HelpCenter.Translations.UpdateArticleTranslation(add_res.Translation);
        Assert.Multiple(() =>
        {
            Assert.That(update_res.Translation.Body, Is.EqualTo("insérer plus français ici ."));

            // delete translation
            Assert.That(Api.HelpCenter.Translations.DeleteTranslation(update_res.Translation.Id.Value), Is.True);

            // teardown.
            Assert.That(Api.HelpCenter.Articles.DeleteArticle(article_id), Is.True);
        });
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
        var resCategoies = Api.HelpCenter.Categories.GetCategories();
        var new_section_res = Api.HelpCenter.Sections.CreateSection(new Section()
        {
            Name = "My Test section for translations",
            Description = "The body of my section (en-us)",
            Locale = "en-us",
            CategoryId = resCategoies.Categories[0].Id.Value
        });
        var section_id = new_section_res.Section.Id.Value;

        var missing_res = Api.HelpCenter.Translations.ListMissingTranslationsForSection(section_id);
        Assert.That(missing_res, Has.Count.EqualTo(1));
        Assert.That(missing_res[0], Is.EqualTo("fr"));

        var fr_translation = new Translation()
        {
            Body = "Je ne parle pas français.",
            Title = "french category here",
            Locale = "fr"
        };

        //create translation
        var add_res = Api.HelpCenter.Translations.CreateSectionTranslation(section_id, fr_translation);
        Assert.That(add_res.Translation.Id, Is.GreaterThan(0));

        add_res.Translation.Body = "insérer plus français ici .";

        //update translation
        var update_res = Api.HelpCenter.Translations.UpdateSectionTranslation(add_res.Translation);
        Assert.Multiple(() =>
        {
            Assert.That(update_res.Translation.Body, Is.EqualTo("insérer plus français ici ."));

            //delete translation
            Assert.That(Api.HelpCenter.Translations.DeleteTranslation(update_res.Translation.Id.Value), Is.True);

            //teardown.
            Assert.That(Api.HelpCenter.Sections.DeleteSection(section_id), Is.True);
        });
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
        var new_category_res = Api.HelpCenter.Categories.CreateCategory(new Category()
        {
            Name = "My Test category for translations",
            Description = "The body of my category (en-us)",
            Locale = "en-us"
        });
        var category_id = new_category_res.Category.Id.Value;

        var missing_res = Api.HelpCenter.Translations.ListMissingTranslationsForCategory(category_id);
        Assert.That(missing_res, Has.Count.EqualTo(1));
        Assert.That(missing_res[0], Is.EqualTo("fr"));

        var fr_translation = new Translation()
        {
            Body = "Je ne parle pas français.",
            Title = "french for 'this is a french category'",
            Locale = "fr"
        };

        //create translation
        var add_res = Api.HelpCenter.Translations.CreateCategoryTranslation(category_id, fr_translation);
        Assert.That(add_res.Translation.Id, Is.GreaterThan(0));

        add_res.Translation.Body = "insérer plus français ici . (category)";

        //update translation
        var update_res = Api.HelpCenter.Translations.UpdateCategoryTranslation(add_res.Translation);
        Assert.Multiple(() =>
        {
            Assert.That(update_res.Translation.Body, Is.EqualTo("insérer plus français ici . (category)"));

            //delete translation
            Assert.That(Api.HelpCenter.Translations.DeleteTranslation(update_res.Translation.Id.Value), Is.True);

            //teardown.
            Assert.That(Api.HelpCenter.Categories.DeleteCategory(category_id), Is.True);
        });
    }

    [Test]
    public void CanListAllEnabledLocales()
    {
        // the only two locales enabled on the test site are us-en and fr. us-en is the default.
        // note: FR was already enabled in the Zendesk settings, however it had to be enabled again in the help center preferences.
        var res = Api.HelpCenter.Translations.ListAllEnabledLocalesAndDefaultLocale(out var default_locale);
        Assert.Multiple(() =>
        {
            Assert.That(default_locale, Is.EqualTo("en-us"));
            Assert.That(res.Contains("en-us"), Is.True);
            Assert.That(res.Contains("fr"), Is.True);
        });
    }

    [Test]
    public async Task CanListTranslationsAsync()
    {
        var res = await Api.HelpCenter.Translations.ListTranslationsForArticleAsync(_articleId);
        Assert.That(res.Count, Is.EqualTo(2));

        res = await Api.HelpCenter.Translations.ListTranslationsForSectionAsync(_sectionId);
        Assert.That(res.Count, Is.EqualTo(2));

        res = await Api.HelpCenter.Translations.ListTranslationsForCategoryAsync(_categoryId);
        Assert.That(res.Count, Is.EqualTo(2));
    }

    [Test]
    public async Task CanShowTranslationForArticleAsync()
    {
        var res = await Api.HelpCenter.Translations.ShowTranslationForArticleAsync(_articleId, "en-us");
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
        var new_article_res = await Api.HelpCenter.Articles.CreateArticleAsync(_sectionId, new Article
        {
            Title = "My Test article for translations",
            Body = "The body of my article",
            Locale = "en-us"
        });
        var article_id = new_article_res.Article.Id.Value;

        var missing_res = await Api.HelpCenter.Translations.ListMissingTranslationsForArticleAsync(article_id);
        Assert.That(missing_res, Has.Count.EqualTo(1));
        Assert.That(missing_res[0], Is.EqualTo("fr"));

        var fr_translation = new Translation()
        {
            Body = "Je ne parle pas français.",
            Title = "Mon article de test pour les traductions",
            Locale = "fr"
        };

        //create translation
        var add_res = await Api.HelpCenter.Translations.CreateArticleTranslationAsync(article_id, fr_translation);
        Assert.That(add_res.Translation.Id, Is.GreaterThan(0));

        add_res.Translation.Body = "insérer plus français ici .";

        //update translation
        var update_res = await Api.HelpCenter.Translations.UpdateArticleTranslationAsync(add_res.Translation);
        Assert.Multiple(async () =>
        {
            Assert.That(update_res.Translation.Body, Is.EqualTo("insérer plus français ici ."));

            //delete translation
            Assert.That(await Api.HelpCenter.Translations.DeleteTranslationAsync(update_res.Translation.Id.Value), Is.True);

            //tear-down.
            Assert.That(await Api.HelpCenter.Articles.DeleteArticleAsync(article_id), Is.True);
        });
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
        var resCategoies = await Api.HelpCenter.Categories.GetCategoriesAsync();
        var new_section_res = await Api.HelpCenter.Sections.CreateSectionAsync(new Section
        {
            Name = "My Test section for translations",
            Description = "The body of my section (en-us)",
            Locale = "en-us",
            CategoryId = resCategoies.Categories[0].Id.Value
        });

        var section_id = new_section_res.Section.Id.Value;

        var missing_res = await Api.HelpCenter.Translations.ListMissingTranslationsForSectionAsync(section_id);
        Assert.That(missing_res, Has.Count.EqualTo(1));
        Assert.That(missing_res[0], Is.EqualTo("fr"));

        var fr_translation = new Translation
        {
            Body = "Je ne parle pas français.",
            Title = "french category here",
            Locale = "fr"
        };

        //create translation
        var add_res = await Api.HelpCenter.Translations.CreateSectionTranslationAsync(section_id, fr_translation);
        Assert.That(add_res.Translation.Id, Is.GreaterThan(0));

        add_res.Translation.Body = "insérer plus français ici .";

        //update translation
        var update_res = await Api.HelpCenter.Translations.UpdateSectionTranslationAsync(add_res.Translation);
        Assert.Multiple(async () =>
        {
            Assert.That(update_res.Translation.Body, Is.EqualTo("insérer plus français ici ."));

            //delete translation
            Assert.That(await Api.HelpCenter.Translations.DeleteTranslationAsync(update_res.Translation.Id.Value), Is.True);

            //tear-down.
            Assert.That(await Api.HelpCenter.Sections.DeleteSectionAsync(section_id), Is.True);
        });
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
        var new_category_res = await Api.HelpCenter.Categories.CreateCategoryAsync(new Category()
        {
            Name = "My Test category for translations",
            Description = "The body of my category (en-us)",
            Locale = "en-us"
        });
        var category_id = new_category_res.Category.Id.Value;

        var missing_res = await Api.HelpCenter.Translations.ListMissingTranslationsForCategoryAsync(category_id);
        Assert.That(missing_res, Has.Count.EqualTo(1));
        Assert.That(missing_res[0], Is.EqualTo("fr"));

        var fr_translation = new Translation()
        {
            Body = "Je ne parle pas français.",
            Title = "french for 'this is a french category'",
            Locale = "fr"
        };

        //create translation
        var add_res = await Api.HelpCenter.Translations.CreateCategoryTranslationAsync(category_id, fr_translation);
        Assert.That(add_res.Translation.Id, Is.GreaterThan(0));

        add_res.Translation.Body = "insérer plus français ici . (category)";

        //update translation
        var update_res = await Api.HelpCenter.Translations.UpdateCategoryTranslationAsync(add_res.Translation);
        Assert.Multiple(async () =>
        {
            Assert.That(update_res.Translation.Body, Is.EqualTo("insérer plus français ici . (category)"));

            //delete translation
            Assert.That(await Api.HelpCenter.Translations.DeleteTranslationAsync(update_res.Translation.Id.Value), Is.True);

            //tear-down.
            Assert.That(await Api.HelpCenter.Categories.DeleteCategoryAsync(category_id), Is.True);
        });
    }

    [Test]
    public async Task CanListAllEnabledLocalesAsync()
    {
        //the only two locales enabled on the test site are us-en and fr. us-en is the default.
        //note: FR was already enabled in the Zendesk settings, however it had to be enabled again in the help center preferences.
        var res = await Api.HelpCenter.Translations.ListAllEnabledLocalesAndDefaultLocaleAsync();
        Assert.Multiple(() =>
        {
            Assert.That(res.Item2, Is.EqualTo("en-us"));
            Assert.That(res.Item1.Contains("en-us"), Is.True);
            Assert.That(res.Item1.Contains("fr"), Is.True);
        });
    }
}
