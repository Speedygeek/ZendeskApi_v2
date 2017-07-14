
using ZendeskApi_v2.Models.HelpCenter.Categories;
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
        IndividualCategoryResponse GetCategoryById(long id);
        IndividualCategoryResponse CreateCategory(Category category);
        IndividualCategoryResponse UpdateCategory(Category category);
        bool DeleteCategory(long id);
#endif

#if ASYNC
        Task<GroupCategoryResponse> GetCategoriesAsync();
        Task<IndividualCategoryResponse> GetCategoryByIdAsync(long id);
        Task<IndividualCategoryResponse> CreateCategoryAsync(Category category);
        Task<IndividualCategoryResponse> UpdateCategoryAsync(Category category);
        Task<bool> DeleteCategoryAsync(long id);
#endif
    }

    public class Categories : Core, ICategories
    {
        private string Locale { get; set; }

        public Categories(string yourZendeskUrl, string user, string password, string apiToken, string locale, string p_OAuthToken)
            : base(yourZendeskUrl, user, password, apiToken, p_OAuthToken)
        {
            Locale = locale;
        }

#if SYNC
        public GroupCategoryResponse GetCategories()
        {
            return GenericGet<GroupCategoryResponse>("help_center/categories.json");
        }

        public IndividualCategoryResponse GetCategoryById(long id) 
        {
            return GenericGet<IndividualCategoryResponse>($"help_center/categories/{id}.json");
        }

        public IndividualCategoryResponse CreateCategory(Category category)
        {
            if (string.IsNullOrEmpty(category.Locale))
                category.Locale = Locale;
            var body = new { category };

            return GenericPost<IndividualCategoryResponse>("help_center/categories.json", body);
        }

        public IndividualCategoryResponse UpdateCategory(Category category)
        {
            var body = new { category };
            return GenericPut<IndividualCategoryResponse>($"help_center/categories/{category.Id}.json", body);
        }

        public bool DeleteCategory(long id)
        {
            return GenericDelete($"help_center/categories/{id}.json");
        }
#endif

#if ASYNC
        public async Task<GroupCategoryResponse> GetCategoriesAsync()
        {
            return await GenericGetAsync<GroupCategoryResponse>("help_center/categories.json");
        }

        public async Task<IndividualCategoryResponse> GetCategoryByIdAsync(long id)
        {
            return await GenericGetAsync<IndividualCategoryResponse>($"help_center/categories/{id}.json");
        }

        public async Task<IndividualCategoryResponse> CreateCategoryAsync(Category category)
        {
            var body = new { category };
            return await GenericPostAsync<IndividualCategoryResponse>(string.Format("help_center/categories.json"), body);
        }

        public async Task<IndividualCategoryResponse> UpdateCategoryAsync(Category category)
        {
            var body = new { category };
            return await GenericPutAsync<IndividualCategoryResponse>($"help_center/categories/{category.Id}.json", body);
        }

        public async Task<bool> DeleteCategoryAsync(long id)
        {
            return await GenericDeleteAsync($"help_center/categories/{id}.json");
        }
#endif
    }
}
