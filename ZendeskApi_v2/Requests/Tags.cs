using System.Collections.Generic;
using ZendeskApi_v2.Models.Tags;

namespace ZendeskApi_v2.Requests
{
    public class Tags : Core
    {
        public Tags(string yourZendeskUrl, string user, string password)
            : base(yourZendeskUrl, user, password)
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