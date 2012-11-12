using ZenDeskApi_v2.Models.Categories;

namespace ZenDeskApi_v2.Requests
{
    public class Categories : Core
    {
        public Categories(string yourZenDeskUrl, string user, string password)
            : base(yourZenDeskUrl, user, password)
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
    }
}