using System.Collections.Generic;
#if ASYNC
using System.Threading.Tasks;
#endif
using ZendeskApi_v2.Models.Tags;

namespace ZendeskApi_v2.Requests
{
	public interface ITags : ICore
	{
#if SYNC
		GroupTagResult GetTags();

		/// <summary>
		/// Returns an array of registered and recent tag names that start with the specified name. The name must be at least 2 characters in length.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		TagAutocompleteResponse AutocompleteTags(string name);
#endif

#if ASYNC
		Task<GroupTagResult> GetTagsAsync();

		/// <summary>
		/// Returns an array of registered and recent tag names that start with the specified name. The name must be at least 2 characters in length.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		Task<TagAutocompleteResponse> AutocompleteTagsAsync(string name);
#endif
	}

	public class Tags : Core, ITags
	{
        public Tags(string yourZendeskUrl, string user, string password, string apiToken)
            : base(yourZendeskUrl, user, password, apiToken)
        {
        }

#if SYNC
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
#endif

#if ASYNC
        public async Task<GroupTagResult> GetTagsAsync()
        {
            return await GenericGetAsync<GroupTagResult>("tags.json");
        }

        /// <summary>
        /// Returns an array of registered and recent tag names that start with the specified name. The name must be at least 2 characters in length.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<TagAutocompleteResponse> AutocompleteTagsAsync(string name)
        {
            return await GenericPostAsync<TagAutocompleteResponse>(string.Format("autocomplete/tags.json?name={0}", name));
        }
#endif
    }
}