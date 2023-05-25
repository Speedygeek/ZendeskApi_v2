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

		GroupTranslationResponse ListTranslationsForArticle( long articleId );
		GroupTranslationResponse ListTranslationsForSection( long articleId );
		GroupTranslationResponse ListTranslationsForCategory( long articleId );

		IList<string> ListMissingTranslationsForArticle( long articleId );
		IList<string> ListMissingTranslationsForSection( long articleId );
		IList<string> ListMissingTranslationsForCategory( long articleId );

		IndividualTranslationResponse ShowTranslationForArticle( long articleId, string locale );

		IndividualTranslationResponse CreateArticleTranslation( long articleId, Translation translation );
		IndividualTranslationResponse CreateSectionTranslation( long SectionId, Translation translation );
		IndividualTranslationResponse CreateCategoryTranslation( long CategoryId, Translation translation );

		IndividualTranslationResponse UpdateArticleTranslation( Translation translation );
		IndividualTranslationResponse UpdateSectionTranslation( Translation translation );
		IndividualTranslationResponse UpdateCategoryTranslation( Translation translation );

		bool DeleteTranslation( long translationId );

		IList<string> ListAllEnabledLocalesAndDefaultLocale( out string defaultLocale );
#endif

#if ASYNC

		Task<GroupTranslationResponse> ListTranslationsForArticleAsync( long articleId );
		Task<GroupTranslationResponse> ListTranslationsForSectionAsync( long articleId );
		Task<GroupTranslationResponse> ListTranslationsForCategoryAsync( long articleId );

		Task<IList<string>> ListMissingTranslationsForArticleAsync( long articleId );
		Task<IList<string>> ListMissingTranslationsForSectionAsync( long articleId );
		Task<IList<string>> ListMissingTranslationsForCategoryAsync( long articleId );

		Task<IndividualTranslationResponse> ShowTranslationForArticleAsync( long articleId, string locale );

		Task<IndividualTranslationResponse> CreateArticleTranslationAsync( long articleId, Translation translation );
		Task<IndividualTranslationResponse> CreateSectionTranslationAsync( long SectionId, Translation translation );
		Task<IndividualTranslationResponse> CreateCategoryTranslationAsync( long CategoryId, Translation translation );

		Task<IndividualTranslationResponse> UpdateArticleTranslationAsync( Translation translation );
		Task<IndividualTranslationResponse> UpdateSectionTranslationAsync( Translation translation );
		Task<IndividualTranslationResponse> UpdateCategoryTranslationAsync( Translation translation );

		Task<bool> DeleteTranslationAsync( long translationId );

		Task<Tuple<IList<string>, string>> ListAllEnabledLocalesAndDefaultLocaleAsync();

#endif
	}

	public class Translations : Core, ITranslations
	{
		public Translations( string yourZendeskUrl, string user, string password, string apiToken, string p_OAuthToken )
			: base( yourZendeskUrl, user, password, apiToken, p_OAuthToken )
		{
		}

#if SYNC
		public GroupTranslationResponse ListTranslationsForArticle( long articleId )
		{
			var resourceUrl = $"help_center/articles/{articleId}/translations";

			return GenericGet<GroupTranslationResponse>( resourceUrl );
		}

		public GroupTranslationResponse ListTranslationsForSection( long articleId )
		{
			var resourceUrl = $"help_center/sections/{articleId}/translations";

			return GenericGet<GroupTranslationResponse>( resourceUrl );
		}

		public GroupTranslationResponse ListTranslationsForCategory( long articleId )
		{
			var resourceUrl = $"help_center/categories/{articleId}/translations";

			return GenericGet<GroupTranslationResponse>( resourceUrl );
		}

		public IList<string> ListMissingTranslationsForArticle( long articleId )
		{
			var res = RunRequest( $"help_center/articles/{articleId}/translations/missing.json", "GET" );
			var anon_type = new { locales = new List<string>() };
			var locale_info = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType( res.Content, anon_type );
			return locale_info.locales;
		}
		public IList<string> ListMissingTranslationsForSection( long articleId )
		{
			var res = RunRequest( $"help_center/sections/{articleId}/translations/missing.json", "GET" );
			var anon_type = new { locales = new List<string>() };
			var locale_info = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType( res.Content, anon_type );
			return locale_info.locales;
		}
		public IList<string> ListMissingTranslationsForCategory( long articleId )
		{
			var res = RunRequest( $"help_center/categories/{articleId}/translations/missing.json", "GET" );
			var anon_type = new { locales = new List<string>() };
			var locale_info = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType( res.Content, anon_type );
			return locale_info.locales;
		}

		public IndividualTranslationResponse ShowTranslationForArticle( long articleId, string locale )
		{
			var resourceUrl = $"help_center/articles/{articleId}/translations/{locale}.json";
			return GenericGet<IndividualTranslationResponse>( resourceUrl );
		}

		public IndividualTranslationResponse CreateArticleTranslation( long articleId, Translation translation )
		{
			var body = new { translation };
			return GenericPost<IndividualTranslationResponse>( $"help_center/articles/{articleId}/translations.json", body );
		}
		public IndividualTranslationResponse CreateSectionTranslation( long SectionId, Translation translation )
		{
			var body = new { translation };
			return GenericPost<IndividualTranslationResponse>( $"help_center/sections/{SectionId}/translations.json", body );
		}
		public IndividualTranslationResponse CreateCategoryTranslation( long CategoryId, Translation translation )
		{
			var body = new { translation };
			return GenericPost<IndividualTranslationResponse>( $"help_center/categories/{CategoryId}/translations.json", body );
		}

		public IndividualTranslationResponse UpdateArticleTranslation( Translation translation )
		{
			var body = new { translation };
			return GenericPut<IndividualTranslationResponse>( $"help_center/articles/{translation.SourceId}/translations/{translation.Locale}.json", body );
		}

		public IndividualTranslationResponse UpdateSectionTranslation( Translation translation )
		{
			var body = new { translation };
			return GenericPut<IndividualTranslationResponse>( $"help_center/sections/{translation.SourceId}/translations/{translation.Locale}.json", body );
		}

		public IndividualTranslationResponse UpdateCategoryTranslation( Translation translation )
		{
			var body = new { translation };
			return GenericPut<IndividualTranslationResponse>( $"help_center/categories/{translation.SourceId}/translations/{translation.Locale}.json", body );
		}

		public bool DeleteTranslation( long translationId )
		{
			return GenericDelete( $"help_center/translations/{translationId}.json" );
		}

		public IList<string> ListAllEnabledLocalesAndDefaultLocale( out string defaultLocale )
		{
			var res = RunRequest( "help_center/locales.json", "GET" );
			var anon_type = new { locales = new List<string>(), default_locale = "" };
			var locale_info = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType( res.Content, anon_type );
			defaultLocale = locale_info.default_locale;
			return locale_info.locales;
		}

#endif

#if ASYNC

		public async Task<GroupTranslationResponse> ListTranslationsForArticleAsync( long articleId )
		{
			var resourceUrl = $"help_center/articles/{articleId}/translations.json";

			return await GenericGetAsync<GroupTranslationResponse>( resourceUrl );
		}

		public async Task<GroupTranslationResponse> ListTranslationsForSectionAsync( long articleId )
		{
			var resourceUrl = $"help_center/sections/{articleId}/translations.json";

			return await GenericGetAsync<GroupTranslationResponse>( resourceUrl );
		}

		public async Task<GroupTranslationResponse> ListTranslationsForCategoryAsync( long articleId )
		{
			var resourceUrl = $"help_center/categories/{articleId}/translations.json";

			return await GenericGetAsync<GroupTranslationResponse>( resourceUrl );
		}

		public async Task<IList<string>> ListMissingTranslationsForArticleAsync( long articleId )
		{
			var res = await RunRequestAsync( $"help_center/articles/{articleId}/translations/missing.json", "GET" );
			var anon_type = new { locales = new List<string>() };
			var locale_info = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType( res.Content, anon_type );
			return locale_info.locales;
		}
		public async Task<IList<string>> ListMissingTranslationsForSectionAsync( long articleId )
		{
			var res = await RunRequestAsync( $"help_center/sections/{articleId}/translations/missing.json", "GET" );
			var anon_type = new { locales = new List<string>() };
			var locale_info = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType( res.Content, anon_type );
			return locale_info.locales;
		}
		public async Task<IList<string>> ListMissingTranslationsForCategoryAsync( long articleId )
		{
			var res = await RunRequestAsync( $"help_center/categories/{articleId}/translations/missing.json", "GET" );
			var anon_type = new { locales = new List<string>() };
			var locale_info = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType( res.Content, anon_type );
			return locale_info.locales;
		}

		public async Task<IndividualTranslationResponse> ShowTranslationForArticleAsync( long articleId, string locale )
		{
			var resourceUrl = $"help_center/articles/{articleId}/translations/{locale}.json";
			return await GenericGetAsync<IndividualTranslationResponse>( resourceUrl );
		}

		public async Task<IndividualTranslationResponse> CreateArticleTranslationAsync( long articleId, Translation translation )
		{
			var body = new { translation };
			return await GenericPostAsync<IndividualTranslationResponse>( $"help_center/articles/{articleId}/translations.json", body );
		}
		public async Task<IndividualTranslationResponse> CreateSectionTranslationAsync( long SectionId, Translation translation )
		{
			var body = new { translation };
			return await GenericPostAsync<IndividualTranslationResponse>( $"help_center/sections/{SectionId}/translations.json", body );
		}
		public async Task<IndividualTranslationResponse> CreateCategoryTranslationAsync( long CategoryId, Translation translation )
		{
			var body = new { translation };
			return await GenericPostAsync<IndividualTranslationResponse>( $"help_center/categories/{CategoryId}/translations.json", body );
		}

		public async Task<IndividualTranslationResponse> UpdateArticleTranslationAsync( Translation translation )
		{
			var body = new { translation };
			return await GenericPutAsync<IndividualTranslationResponse>( $"help_center/articles/{translation.SourceId}/translations/{translation.Locale}.json", body );
		}

		public async Task<IndividualTranslationResponse> UpdateSectionTranslationAsync( Translation translation )
		{
			var body = new { translation };
			return await GenericPutAsync<IndividualTranslationResponse>( $"help_center/sections/{translation.SourceId}/translations/{translation.Locale}.json", body );
		}

		public async Task<IndividualTranslationResponse> UpdateCategoryTranslationAsync( Translation translation )
		{
			var body = new { translation };
			return await GenericPutAsync<IndividualTranslationResponse>( $"help_center/categories/{translation.SourceId}/translations/{translation.Locale}.json", body );
		}

		public async Task<bool> DeleteTranslationAsync( long translationId )
		{
			return await GenericDeleteAsync( $"help_center/translations/{translationId}.json" );
		}

		public async Task<Tuple<IList<string>, string>> ListAllEnabledLocalesAndDefaultLocaleAsync( )
		{
			var res = await RunRequestAsync( "help_center/locales.json", "GET" );
			var anon_type = new { locales = new List<string>(), default_locale = "" };
			var locale_info = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType( res.Content, anon_type );
			return new Tuple<IList<string>, string>( locale_info.locales, locale_info.default_locale );
		}
#endif

	}
}
