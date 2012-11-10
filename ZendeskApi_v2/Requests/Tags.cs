using ZenDeskApi_v2.Models.Tags;

namespace ZenDeskApi_v2.Requests
{
    public class Tags : Core
    {
        public Tags(string yourZenDeskUrl, string user, string password)
            : base(yourZenDeskUrl, user, password)
        {
        }

        public GroupTagResult GetTags()
        {
            return GenericGet<GroupTagResult>("tags.json");
        }
    }
}