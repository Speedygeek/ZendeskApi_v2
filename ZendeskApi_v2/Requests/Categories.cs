using ZendeskApi_v2.Models.Categories;
#if ASYNC
using System.Threading.Tasks;
#endif
using ZendeskApi_v2.Models.Groups;

namespace ZendeskApi_v2.Requests
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
        public Categories(string yourZendeskUrl, string user, string password, string apiToken)
            : base(yourZendeskUrl, user, password, apiToken)
        {
        }

#if SYNC
        public GroupCategoryResponse GetCategories()
        {
            return GenericGet<GroupCategoryResponse>("categories.json");
        }

        public IndividualCategoryResponse GetCategoryById(long id)
        {
            return GenericGet<IndividualCategoryResponse>(string.Format("categories/{0}.json", id));
        }

        public IndividualCategoryResponse CreateCategory(Category category)
        {
            var body = new {category};
            return GenericPost<IndividualCategoryResponse>(string.Format("categories.json"), body);
        }

        public IndividualCategoryResponse UpdateCategory(Category category)
        {
            var body = new { category };
            return GenericPut<IndividualCategoryResponse>(string.Format("categories/{0}.json", category.Id), body);
        }

        public bool DeleteCategory(long id)
        {            
            return GenericDelete(string.Format("categories/{0}.json", id));
        }
#endif

#if ASYNC                
        public async Task<GroupCategoryResponse> GetCategoriesAsync()
        {
            return await GenericGetAsync<GroupCategoryResponse>("categories.json");
        }

        public async Task<IndividualCategoryResponse> GetCategoryByIdAsync(long id)
        {
            return await GenericGetAsync<IndividualCategoryResponse>(string.Format("categories/{0}.json", id));
        }

        public async Task<IndividualCategoryResponse> CreateCategoryAsync(Category category)
        {
            var body = new { category };
            return await GenericPostAsync<IndividualCategoryResponse>(string.Format("categories.json"), body);
        }

        public async Task<IndividualCategoryResponse> UpdateCategoryAsync(Category category)
        {
            var body = new { category };
            return await GenericPutAsync<IndividualCategoryResponse>(string.Format("categories/{0}.json", category.Id), body);
        }

        public async Task<bool> DeleteCategoryAsync(long id)
        {
            return await GenericDeleteAsync(string.Format("categories/{0}.json", id));
        }
#endif
    }
}