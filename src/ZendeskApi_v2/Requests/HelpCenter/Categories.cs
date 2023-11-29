
using ZendeskApi_v2.Models.HelpCenter.Categories;
using System.Collections.Generic;

#if ASYNC
using System.Threading.Tasks;
#endif
using System;

namespace ZendeskApi_v2.Requests.HelpCenter
{
    public interface ICategories : ICore
    {
#if SYNC
        GroupCategoryResponse GetCategories();
        GroupCategoryResponse GetCategories(int perPage, int page);
        IndividualCategoryResponse GetCategoryById(long id);
        IndividualCategoryResponse CreateCategory(Category category);
        IndividualCategoryResponse UpdateCategory(Category category);
        bool DeleteCategory(long id);
#endif

#if ASYNC
        Task<GroupCategoryResponse> GetCategoriesAsync();
        Task<GroupCategoryResponse> GetCategoriesAsync(int perPage, int page);
        Task<IndividualCategoryResponse> GetCategoryByIdAsync(long id);
        Task<IndividualCategoryResponse> CreateCategoryAsync(Category category);
        Task<IndividualCategoryResponse> UpdateCategoryAsync(Category category);
        Task<bool> DeleteCategoryAsync(long id);
#endif
    }

    public class Categories : Core, ICategories
    {
        private string Locale { get; set; }

        private string GeneralCategoriesPath => string.IsNullOrWhiteSpace(Locale)
            ? "help_center/categories"
            : $"help_center/{Locale}/categories";

        public Categories(string yourZendeskUrl, string user, string password, string apiToken, string locale, string p_OAuthToken, Dictionary<string,string> customHeaders)
            : base(yourZendeskUrl, user, password, apiToken, p_OAuthToken, customHeaders)
        {
            Locale = locale;
        }

#if SYNC
        public GroupCategoryResponse GetCategories()
        {
            return GenericGet<GroupCategoryResponse>($"{GeneralCategoriesPath}.json");
        }

        public GroupCategoryResponse GetCategories(int perPage, int page)
        {
            return GenericPagedGet<GroupCategoryResponse>($"{GeneralCategoriesPath}.json", perPage, page);
        }

        public IndividualCategoryResponse GetCategoryById(long id)
        {
            return GenericGet<IndividualCategoryResponse>($"{GeneralCategoriesPath}/{id}.json");
        }

        public IndividualCategoryResponse CreateCategory(Category category)
        {
            if (string.IsNullOrEmpty(category.Locale))
            {
                category.Locale = Locale;
            }

            var body = new { category };

            return GenericPost<IndividualCategoryResponse>($"{GeneralCategoriesPath}.json", body);
        }

        public IndividualCategoryResponse UpdateCategory(Category category)
        {
            var body = new { category };
            return GenericPut<IndividualCategoryResponse>($"{GeneralCategoriesPath}/{category.Id}.json", body);
        }

        public bool DeleteCategory(long id)
        {
            return GenericDelete($"help_center/categories/{id}.json");
        }
#endif

#if ASYNC
        public async Task<GroupCategoryResponse> GetCategoriesAsync()
        {
            return await GenericGetAsync<GroupCategoryResponse>($"{GeneralCategoriesPath}.json");
        }

        public async Task<GroupCategoryResponse> GetCategoriesAsync(int perPage, int page)
        {
            return await GenericPagedGetAsync<GroupCategoryResponse>($"{GeneralCategoriesPath}.json", perPage, page);
        }

        public async Task<IndividualCategoryResponse> GetCategoryByIdAsync(long id)
        {
            return await GenericGetAsync<IndividualCategoryResponse>($"{GeneralCategoriesPath}/{id}.json");
        }

        public async Task<IndividualCategoryResponse> CreateCategoryAsync(Category category)
        {
            var body = new { category };
            return await GenericPostAsync<IndividualCategoryResponse>($"{GeneralCategoriesPath}.json", body);
        }

        public async Task<IndividualCategoryResponse> UpdateCategoryAsync(Category category)
        {
            var body = new { category };
            return await GenericPutAsync<IndividualCategoryResponse>($"{GeneralCategoriesPath}/{category.Id}.json", body);
        }

        public async Task<bool> DeleteCategoryAsync(long id)
        {
            return await GenericDeleteAsync($"{GeneralCategoriesPath}/{id}.json");
        }
#endif
    }
}
