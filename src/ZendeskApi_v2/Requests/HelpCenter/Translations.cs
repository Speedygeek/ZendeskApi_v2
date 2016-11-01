﻿using System;
using System.Collections.Generic;

#if ASYNC

using System.Threading.Tasks;

#endif

using ZendeskApi_v2.Models.HelpCenter.Translations;

namespace ZendeskApi_v2.Requests.HelpCenter
{
    public interface ITranslations : ICore
    {
#if SYNC

        GroupTranslationResponse ListTranslationsForArticle(long articleId);

        GroupTranslationResponse ListTranslationsForSection(long articleId);

        GroupTranslationResponse ListTranslationsForCategory(long articleId);

        IList<string> ListMissingTranslationsForArticle(long articleId);

        IList<string> ListMissingTranslationsForSection(long articleId);

        IList<string> ListMissingTranslationsForCategory(long articleId);

        IndividualTranslationResponse ShowTranslationForArticle(long articleId, string locale);

        IndividualTranslationResponse CreateArticleTranslation(long articleId, Translation translation);

        IndividualTranslationResponse CreateSectionTranslation(long SectionId, Translation translation);

        IndividualTranslationResponse CreateCategoryTranslation(long CategoryId, Translation translation);

        IndividualTranslationResponse UpdateArticleTranslation(Translation translation);

        IndividualTranslationResponse UpdateSectionTranslation(Translation translation);

        IndividualTranslationResponse UpdateCategoryTranslation(Translation translation);

        bool DeleteTranslation(long translationId);

        IList<string> ListAllEnabledLocalesAndDefaultLocale(out string defaultLocale);

#endif

#if ASYNC

        Task<GroupTranslationResponse> ListTranslationsForArticleAsync(long articleId);

        Task<GroupTranslationResponse> ListTranslationsForSectionAsync(long articleId);

        Task<GroupTranslationResponse> ListTranslationsForCategoryAsync(long articleId);

        Task<IList<string>> ListMissingTranslationsForArticleAsync(long articleId);

        Task<IList<string>> ListMissingTranslationsForSectionAsync(long articleId);

        Task<IList<string>> ListMissingTranslationsForCategoryAsync(long articleId);

        Task<IndividualTranslationResponse> ShowTranslationForArticleAsync(long articleId, string locale);

        Task<IndividualTranslationResponse> CreateArticleTranslationAsync(long articleId, Translation translation);

        Task<IndividualTranslationResponse> CreateSectionTranslationAsync(long SectionId, Translation translation);

        Task<IndividualTranslationResponse> CreateCategoryTranslationAsync(long CategoryId, Translation translation);

        Task<IndividualTranslationResponse> UpdateArticleTranslationAsync(Translation translation);

        Task<IndividualTranslationResponse> UpdateSectionTranslationAsync(Translation translation);

        Task<IndividualTranslationResponse> UpdateCategoryTranslationAsync(Translation translation);

        Task<bool> DeleteTranslationAsync(long translationId);

        Task<Tuple<IList<string>, string>> ListAllEnabledLocalesAndDefaultLocaleAsync();

#endif
    }

    public class Translations : Core, ITranslations
    {
        public Translations(string yourZendeskUrl, string user, string password, string apiToken, string p_OAuthToken)
            : base(yourZendeskUrl, user, password, apiToken, p_OAuthToken)
        {
        }

#if SYNC

        public GroupTranslationResponse ListTranslationsForArticle(long articleId)
        {
            var resourceUrl = string.Format("/help_center/articles/{0}/translations.json", articleId);

            return GenericGet<GroupTranslationResponse>(resourceUrl);
        }

        public GroupTranslationResponse ListTranslationsForSection(long articleId)
        {
            var resourceUrl = string.Format("/help_center/sections/{0}/translations.json", articleId);

            return GenericGet<GroupTranslationResponse>(resourceUrl);
        }

        public GroupTranslationResponse ListTranslationsForCategory(long articleId)
        {
            var resourceUrl = string.Format("/help_center/categories/{0}/translations.json", articleId);

            return GenericGet<GroupTranslationResponse>(resourceUrl);
        }

        public IList<string> ListMissingTranslationsForArticle(long articleId)
        {
            var res = RunRequest(string.Format("help_center/articles/{0}/translations/missing.json", articleId), "GET");
            var anon_type = new { locales = new List<string>() };
            var locale_info = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(res.Content, anon_type);
            return locale_info.locales;
        }

        public IList<string> ListMissingTranslationsForSection(long articleId)
        {
            var res = RunRequest(string.Format("help_center/sections/{0}/translations/missing.json", articleId), "GET");
            var anon_type = new { locales = new List<string>() };
            var locale_info = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(res.Content, anon_type);
            return locale_info.locales;
        }

        public IList<string> ListMissingTranslationsForCategory(long articleId)
        {
            var res = RunRequest(string.Format("help_center/categories/{0}/translations/missing.json", articleId), "GET");
            var anon_type = new { locales = new List<string>() };
            var locale_info = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(res.Content, anon_type);
            return locale_info.locales;
        }

        public IndividualTranslationResponse ShowTranslationForArticle(long articleId, string locale)
        {
            var resourceUrl = string.Format("/help_center/articles/{0}/translations/{1}.json", articleId, locale);
            return GenericGet<IndividualTranslationResponse>(resourceUrl);
        }

        public IndividualTranslationResponse CreateArticleTranslation(long articleId, Translation translation)
        {
            var body = new { translation };
            return GenericPost<IndividualTranslationResponse>(string.Format("help_center/articles/{0}/translations.json", articleId), body);
        }

        public IndividualTranslationResponse CreateSectionTranslation(long SectionId, Translation translation)
        {
            var body = new { translation };
            return GenericPost<IndividualTranslationResponse>(string.Format("help_center/sections/{0}/translations.json", SectionId), body);
        }

        public IndividualTranslationResponse CreateCategoryTranslation(long CategoryId, Translation translation)
        {
            var body = new { translation };
            return GenericPost<IndividualTranslationResponse>(string.Format("help_center/categories/{0}/translations.json", CategoryId), body);
        }

        public IndividualTranslationResponse UpdateArticleTranslation(Translation translation)
        {
            var body = new { translation };
            return GenericPut<IndividualTranslationResponse>(string.Format("help_center/articles/{0}/translations/{1}.json", translation.SourceId, translation.Locale), body);
        }

        public IndividualTranslationResponse UpdateSectionTranslation(Translation translation)
        {
            var body = new { translation };
            return GenericPut<IndividualTranslationResponse>(string.Format("help_center/sections/{0}/translations/{1}.json", translation.SourceId, translation.Locale), body);
        }

        public IndividualTranslationResponse UpdateCategoryTranslation(Translation translation)
        {
            var body = new { translation };
            return GenericPut<IndividualTranslationResponse>(string.Format("help_center/categories/{0}/translations/{1}.json", translation.SourceId, translation.Locale), body);
        }

        public bool DeleteTranslation(long translationId)
        {
            return GenericDelete(string.Format("help_center/translations/{0}.json", translationId));
        }

        public IList<string> ListAllEnabledLocalesAndDefaultLocale(out string defaultLocale)
        {
            var res = RunRequest("help_center/locales.json", "GET");
            var anon_type = new { locales = new List<string>(), default_locale = string.Empty };
            var locale_info = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(res.Content, anon_type);
            defaultLocale = locale_info.default_locale;
            return locale_info.locales;
        }

#endif

#if ASYNC

        public async Task<GroupTranslationResponse> ListTranslationsForArticleAsync(long articleId)
        {
            var resourceUrl = string.Format("/help_center/articles/{0}/translations.json", articleId);

            return await GenericGetAsync<GroupTranslationResponse>(resourceUrl);
        }

        public async Task<GroupTranslationResponse> ListTranslationsForSectionAsync(long articleId)
        {
            var resourceUrl = string.Format("/help_center/sections/{0}/translations.json", articleId);

            return await GenericGetAsync<GroupTranslationResponse>(resourceUrl);
        }

        public async Task<GroupTranslationResponse> ListTranslationsForCategoryAsync(long articleId)
        {
            var resourceUrl = string.Format("/help_center/categories/{0}/translations.json", articleId);

            return await GenericGetAsync<GroupTranslationResponse>(resourceUrl);
        }

        public async Task<IList<string>> ListMissingTranslationsForArticleAsync(long articleId)
        {
            var res = await RunRequestAsync(string.Format("help_center/articles/{0}/translations/missing.json", articleId), "GET");
            var anon_type = new { locales = new List<string>() };
            var locale_info = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(res.Content, anon_type);
            return locale_info.locales;
        }

        public async Task<IList<string>> ListMissingTranslationsForSectionAsync(long articleId)
        {
            var res = await RunRequestAsync(string.Format("help_center/sections/{0}/translations/missing.json", articleId), "GET");
            var anon_type = new { locales = new List<string>() };
            var locale_info = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(res.Content, anon_type);
            return locale_info.locales;
        }

        public async Task<IList<string>> ListMissingTranslationsForCategoryAsync(long articleId)
        {
            var res = await RunRequestAsync(string.Format("help_center/categories/{0}/translations/missing.json", articleId), "GET");
            var anon_type = new { locales = new List<string>() };
            var locale_info = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(res.Content, anon_type);
            return locale_info.locales;
        }

        public async Task<IndividualTranslationResponse> ShowTranslationForArticleAsync(long articleId, string locale)
        {
            var resourceUrl = string.Format("/help_center/articles/{0}/translations/{1}.json", articleId, locale);
            return await GenericGetAsync<IndividualTranslationResponse>(resourceUrl);
        }

        public async Task<IndividualTranslationResponse> CreateArticleTranslationAsync(long articleId, Translation translation)
        {
            var body = new { translation };
            return await GenericPostAsync<IndividualTranslationResponse>(string.Format("help_center/articles/{0}/translations.json", articleId), body);
        }

        public async Task<IndividualTranslationResponse> CreateSectionTranslationAsync(long SectionId, Translation translation)
        {
            var body = new { translation };
            return await GenericPostAsync<IndividualTranslationResponse>(string.Format("help_center/sections/{0}/translations.json", SectionId), body);
        }

        public async Task<IndividualTranslationResponse> CreateCategoryTranslationAsync(long CategoryId, Translation translation)
        {
            var body = new { translation };
            return await GenericPostAsync<IndividualTranslationResponse>(string.Format("help_center/categories/{0}/translations.json", CategoryId), body);
        }

        public async Task<IndividualTranslationResponse> UpdateArticleTranslationAsync(Translation translation)
        {
            var body = new { translation };
            return await GenericPutAsync<IndividualTranslationResponse>(string.Format("help_center/articles/{0}/translations/{1}.json", translation.SourceId, translation.Locale), body);
        }

        public async Task<IndividualTranslationResponse> UpdateSectionTranslationAsync(Translation translation)
        {
            var body = new { translation };
            return await GenericPutAsync<IndividualTranslationResponse>(string.Format("help_center/sections/{0}/translations/{1}.json", translation.SourceId, translation.Locale), body);
        }

        public async Task<IndividualTranslationResponse> UpdateCategoryTranslationAsync(Translation translation)
        {
            var body = new { translation };
            return await GenericPutAsync<IndividualTranslationResponse>(string.Format("help_center/categories/{0}/translations/{1}.json", translation.SourceId, translation.Locale), body);
        }

        public async Task<bool> DeleteTranslationAsync(long translationId)
        {
            return await GenericDeleteAsync(string.Format("help_center/translations/{0}.json", translationId));
        }

        public async Task<Tuple<IList<string>, string>> ListAllEnabledLocalesAndDefaultLocaleAsync()
        {
            var res = await RunRequestAsync("help_center/locales.json", "GET");
            var anon_type = new { locales = new List<string>(), default_locale = string.Empty };
            var locale_info = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(res.Content, anon_type);
            return new Tuple<IList<string>, string>(locale_info.locales, locale_info.default_locale);
        }

#endif
    }
}