using NUnit.Framework;
using ZendeskApi_v2;
using ZendeskApi_v2.Models.HelpCenter.Translations;

namespace Tests.HelpCenter
{
	[TestFixture]
	[Category( "HelpCenter" )]
	public class TranslationTests
	{
		private ZendeskApi api = new ZendeskApi( Settings.Site, Settings.Email, Settings.Password );
		private long _articleId = 204838115; //https://csharpapi.zendesk.com/hc/en-us/articles/204838115-Thing-4?page=1#comment_200486479
		private long _sectionId = 201010935;
		private long _categoryId = 200382245;
		[ Test]
		public void CanListTranslations()
		{
			
			var res = api.HelpCenter.Translations.ListTranslationsForArticle( _articleId );
			Assert.AreEqual( 2, res.Count );

			res = api.HelpCenter.Translations.ListTranslationsForSection( _sectionId );
			Assert.AreEqual( 2, res.Count );

			res = api.HelpCenter.Translations.ListTranslationsForCategory( _categoryId );
			Assert.AreEqual( 2, res.Count );


		}

		[Test]
		public void CanShowTranslationForArticle()
		{
			var res = api.HelpCenter.Translations.ShowTranslationForArticle( _articleId, "en-us" );
			Assert.AreEqual( "en-us" , res.Translation.Locale );			
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
			var new_article_res = api.HelpCenter.Articles.CreateArticle( resSections.Sections[ 0 ].Id.Value, new ZendeskApi_v2.Models.Articles.Article()
			{
				Title = "My Test article for translations",
				Body = "The body of my article",
				Locale = "en-us"
			} );
			long article_id = new_article_res.Arcticle.Id.Value;

			var missing_res = api.HelpCenter.Translations.ListMissingTranslationsForArticle( article_id );
			Assert.AreEqual( 1, missing_res.Count );
			Assert.AreEqual( "fr", missing_res[ 0 ] );

			Translation fr_translation = new Translation()
			{
				Body = "Je ne parle pas français.",
				Title = "Mon article de test pour les traductions",
				Locale = "fr"
			};

			//create translation
			var add_res = api.HelpCenter.Translations.CreateArticleTranslation( article_id, fr_translation );
			Assert.Greater( add_res.Translation.Id, 0 );

			add_res.Translation.Body = "insérer plus français ici .";

			//update translation
			var update_res = api.HelpCenter.Translations.UpdateArticleTranslation( add_res.Translation );
			Assert.AreEqual( "insérer plus français ici .", update_res.Translation.Body );

			//delete translation
			Assert.IsTrue( api.HelpCenter.Translations.DeleteTranslation( update_res.Translation.Id.Value ) );

			//teardown.
			Assert.IsTrue( api.HelpCenter.Articles.DeleteArticle( article_id ) );

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
			var new_section_res = api.HelpCenter.Sections.CreateSection( new ZendeskApi_v2.Models.Sections.Section()
			{
				Name = "My Test section for translations",
				Description = "The body of my section (en-us)",
				Locale = "en-us",
				CategoryId = resCategoies.Categories[ 0 ].Id.Value
			} );
			long section_id = new_section_res.Section.Id.Value;

			var missing_res = api.HelpCenter.Translations.ListMissingTranslationsForSection( section_id );
			Assert.AreEqual( 1, missing_res.Count );
			Assert.AreEqual( "fr", missing_res[ 0 ] );

			Translation fr_translation = new Translation()
			{
				Body = "Je ne parle pas français.",
				Title = "french category here",
				Locale = "fr"
			};

			//create translation
			var add_res = api.HelpCenter.Translations.CreateSectionTranslation( section_id, fr_translation );
			Assert.Greater( add_res.Translation.Id, 0 );

			add_res.Translation.Body = "insérer plus français ici .";

			//update translation
			var update_res = api.HelpCenter.Translations.UpdateSectionTranslation( add_res.Translation );
			Assert.AreEqual( "insérer plus français ici .", update_res.Translation.Body );

			//delete translation
			Assert.IsTrue( api.HelpCenter.Translations.DeleteTranslation( update_res.Translation.Id.Value ) );

			//teardown.
			Assert.IsTrue( api.HelpCenter.Sections.DeleteSection( section_id ) );

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
			var new_category_res = api.HelpCenter.Categories.CreateCategory(  new ZendeskApi_v2.Models.HelpCenter.Categories.Category()
			{
				Name = "My Test category for translations",
				Description = "The body of my category (en-us)",
				Locale = "en-us"
			} );
			long category_id = new_category_res.Category.Id.Value;

			var missing_res = api.HelpCenter.Translations.ListMissingTranslationsForCategory( category_id );
			Assert.AreEqual( 1, missing_res.Count );
			Assert.AreEqual( "fr", missing_res[ 0 ] );

			Translation fr_translation = new Translation()
			{
				Body = "Je ne parle pas français.",
				Title = "french for 'this is a french category'",
				Locale = "fr"
			};

			//create translation
			var add_res = api.HelpCenter.Translations.CreateCategoryTranslation( category_id, fr_translation );
			Assert.Greater( add_res.Translation.Id, 0 );

			add_res.Translation.Body = "insérer plus français ici . (category)";

			//update translation
			var update_res = api.HelpCenter.Translations.UpdateCategoryTranslation( add_res.Translation );
			Assert.AreEqual( "insérer plus français ici . (category)", update_res.Translation.Body );

			//delete translation
			Assert.IsTrue( api.HelpCenter.Translations.DeleteTranslation( update_res.Translation.Id.Value ) );

			//teardown.
			Assert.IsTrue( api.HelpCenter.Categories.DeleteCategory( category_id ) );

		}

		[Test]
		public void CanListAllEnabledLocales()
		{
			//the only two locales enabled on the test site are us-en and fr. us-en is the default.
			//note: FR was already enabled in the Zendesk settings, however it had to be enabled again in the help center preferences.
			string default_locale;
			var res = api.HelpCenter.Translations.ListAllEnabledLocalesAndDefaultLocale( out default_locale );

			Assert.AreEqual( default_locale, "en-us" );
			Assert.IsTrue( res.Contains( "en-us" ) );
			Assert.IsTrue( res.Contains( "fr" ) );
			Assert.AreEqual( res.Count, 2 );

		}




	}
}
