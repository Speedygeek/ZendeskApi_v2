using ZendeskApi_v2.Models.Categories;

namespace ZendeskApi_v2.Requests
{
    public class Categories : Core
    {
        public Categories(string yourZendeskUrl, string user, string password)
            : base(yourZendeskUrl, user, password)
        {
        }

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

#if NotNet35        
        public aysnc Task<GroupCategoryResponse> GetCategoriesAsync()
        {
            return await GenericGetAsync<GroupCategoryResponse>("categories.json");
        }

        public aysnc Task<IndividualCategoryResponse> GetCategoryByIdAsync(long id)
        {
            return await GenericGetAsync<IndividualCategoryResponse>(string.Format("categories/{0}.json", id));
        }

        public aysnc Task<IndividualCategoryResponse> CreateCategoryAsync(Category category)
        {
            var body = new { category };
            return await GenericPostAsync<IndividualCategoryResponse>(string.Format("categories.json"), body);
        }

        public aysnc Task<IndividualCategoryResponse> UpdateCategoryAsync(Category category)
        {
            var body = new { category };
            return await GenericPutAsync<IndividualCategoryResponse>(string.Format("categories/{0}.json", category.Id), body);
        }

        public aysnc Task<bool> DeleteCategoryAsync(long id)
        {
            return await GenericDeleteAsync(string.Format("categories/{0}.json", id));
        }
#endif
    }
}