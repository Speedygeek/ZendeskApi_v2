using ZendeskApi_v2.Models.Categories;
#if ASYNC
using System.Threading.Tasks;
#endif
using ZendeskApi_v2.Models.Groups;

namespace ZendeskApi_v2.Requests
{
    public class Categories : Core
    {    
        internal Categories(IZendeskConnectionSettings connectionSettings)
            : base(connectionSettings)
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