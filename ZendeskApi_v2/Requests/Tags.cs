using System.Collections.Generic;
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

        /// <summary>
        /// Returns an array of registered and recent tag names that start with the specified name. The name must be at least 2 characters in length.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public TagAutocompleteResponse AutocompleteTags(string name)
        {
            return GenericPost<TagAutocompleteResponse>(string.Format("autocomplete/tags.json?name={0}", name));
        }
    }
}