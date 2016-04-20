using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if ASYNC
using System.Threading.Tasks;
#endif
using ZendeskApi_v2.Models.Sections;
using ZendeskApi_v2.Models.HelpCenter.Translations;

namespace ZendeskApi_v2.Requests.HelpCenter
{
	public interface ITranslations : ICore
	{
#if SYNC

		IList<string> ListMissingTranslationsForArticle( long articleId );
		IList<string> ListMissingTranslationsForSection( long articleId );
		IList<string> ListMissingTranslationsForCategory( long articleId );
		IList<string> ListAllEnabledLocalesAndDefaultLocale( out string defaultLocale );


		IndividualTranslationResponse CreateArticleTranslation( long articleId, Translation translation );
		IndividualTranslationResponse CreateSectionTranslation( long SectionId, Translation translation );
		IndividualTranslationResponse CreateCategoryTranslation( long CategoryId, Translation translation );

		IndividualTranslationResponse UpdateArticleTranslation( Translation translation );
		IndividualTranslationResponse UpdateSectionTranslation( Translation translation );
		IndividualTranslationResponse UpdateCategoryTranslation( Translation translation );

		bool DeleteTranslation( long translationId );
#endif

#if ASYNC
#endif
	}

	public class Translations : Core, ITranslations
	{
		public Translations( string yourZendeskUrl, string user, string password, string apiToken, string p_OAuthToken )
			: base( yourZendeskUrl, user, password, apiToken, p_OAuthToken )
		{
		}

#if SYNC
		public IList<string> ListMissingTranslationsForArticle( long articleId )
		{
			var res = RunRequest( String.Format( "help_center/articles/{0}/translations/missing.json", articleId ), "GET" );
			var anon_type = new { locales = new List<string>() };
			var locale_info = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType( res.Content, anon_type );
			return anon_type.locales;
		}
		public IList<string> ListMissingTranslationsForSection( long articleId )
		{
			var res = RunRequest( String.Format( "help_center/sections/{0}/translations/missing.json", articleId ), "GET" );
			var anon_type = new { locales = new List<string>() };
			var locale_info = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType( res.Content, anon_type );
			return anon_type.locales;
		}
		public IList<string> ListMissingTranslationsForCategory( long articleId )
		{
			var res = RunRequest( String.Format( "help_center/categories/{0}/translations/missing.json", articleId ), "GET" );
			var anon_type = new { locales = new List<string>() };
			var locale_info = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType( res.Content, anon_type );
			return anon_type.locales;
		}
		public IList<string> ListAllEnabledLocalesAndDefaultLocale( out string defaultLocale )
		{
			var res = RunRequest( "help_center/locales.json", "GET" );
			var anon_type = new { locales = new List<string>(), default_locale = "" };
			var locale_info = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType( res.Content, anon_type );
			defaultLocale = locale_info.default_locale;
			return locale_info.locales;
		}
		public IndividualTranslationResponse CreateArticleTranslation( long articleId, Translation translation )
		{
			var body = new { translation };
			return GenericPost<IndividualTranslationResponse>( string.Format( "help_center/articles/{0}/translations.json", translation.SourceId ), body );
		}
		public IndividualTranslationResponse CreateSectionTranslation( long SectionId, Translation translation )
		{
			var body = new { translation };
			return GenericPost<IndividualTranslationResponse>( string.Format( "help_center/sections/{0}/translations.json", translation.SourceId ), body );
		}
		public IndividualTranslationResponse CreateCategoryTranslation( long CategoryId, Translation translation )
		{
			var body = new { translation };
			return GenericPost<IndividualTranslationResponse>( string.Format( "help_center/categories/{0}/translations.json", translation.SourceId ), body );
		}

		public IndividualTranslationResponse UpdateArticleTranslation( Translation translation )
		{
			var body = new { translation };
			return GenericPut<IndividualTranslationResponse>( string.Format( "help_center/articles/{0}/translations/{1}.json", translation.SourceId, translation.Locale ), body );
		}

		public IndividualTranslationResponse UpdateSectionTranslation( Translation translation )
		{
			var body = new { translation };
			return GenericPut<IndividualTranslationResponse>( string.Format( "help_center/sections/{0}/translations/{1}.json", translation.SourceId, translation.Locale ), body );
		}

		public IndividualTranslationResponse UpdateCategoryTranslation( Translation translation )
		{
			var body = new { translation };
			return GenericPut<IndividualTranslationResponse>( string.Format( "help_center/categories/{0}/translations/{1}.json", translation.SourceId, translation.Locale ), body );
		}

		public bool DeleteTranslation( long translationId )
		{
			return GenericDelete( string.Format( "help_center/translations/{0}.json", translationId ) );
		}

#endif

#if ASYNC
#endif


	}
}
