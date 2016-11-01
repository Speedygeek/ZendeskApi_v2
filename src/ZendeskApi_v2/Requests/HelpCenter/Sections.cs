﻿#if ASYNC

using System.Threading.Tasks;

#endif

using ZendeskApi_v2.Models.Sections;

namespace ZendeskApi_v2.Requests.HelpCenter
{
    public interface ISections : ICore
    {
#if SYNC

        GroupSectionResponse GetSections();

        GroupSectionResponse GetSectionsByCategoryId(long categoryId);

        IndividualSectionResponse GetSectionById(long id);

        IndividualSectionResponse CreateSection(Section section);

        IndividualSectionResponse UpdateSection(Section section);

        bool DeleteSection(long id);

#endif

#if ASYNC

        Task<GroupSectionResponse> GetSectionsAsync();

        Task<GroupSectionResponse> GetSectionsByCategoryIdAsync(long categoryId);

        Task<IndividualSectionResponse> GetSectionByIdAsync(long id);

        Task<IndividualSectionResponse> CreateSectionAsync(Section section);

        Task<IndividualSectionResponse> UpdateSectionAsync(Section section);

        Task<bool> DeleteSectionAsync(long id);

#endif
    }

    public class Sections : Core, ISections
    {
        public Sections(string yourZendeskUrl, string user, string password, string apiToken, string p_OAuthToken)
            : base(yourZendeskUrl, user, password, apiToken, p_OAuthToken)
        {
        }

#if SYNC

        public GroupSectionResponse GetSections()
        {
            return GenericGet<GroupSectionResponse>("help_center/sections.json?include=access_policies");
        }

        public GroupSectionResponse GetSectionsByCategoryId(long categoryId)
        {
            return GenericGet<GroupSectionResponse>(string.Format("help_center/categories/{0}/sections.json?include=access_policies", categoryId));
        }

        public IndividualSectionResponse GetSectionById(long id)
        {
            return GenericGet<IndividualSectionResponse>(string.Format("help_center/sections/{0}.json?include=access_policies", id));
        }

        public IndividualSectionResponse CreateSection(Section section)
        {
            var body = new { section };
            return GenericPost<IndividualSectionResponse>(string.Format("help_center/categories/{0}/sections.json?include=access_policies", section.CategoryId), body);
        }

        public IndividualSectionResponse UpdateSection(Section section)
        {
            var body = new { section };
            return GenericPut<IndividualSectionResponse>(string.Format("help_center/sections/{0}.json?include=access_policies", section.Id), body);
        }

        public bool DeleteSection(long id)
        {
            return GenericDelete(string.Format("help_center/sections/{0}.json?include=access_policies", id));
        }

#endif

#if ASYNC

        public async Task<GroupSectionResponse> GetSectionsAsync()
        {
            return await GenericGetAsync<GroupSectionResponse>("help_center/sections.json?include=access_policies");
        }

        public async Task<GroupSectionResponse> GetSectionsByCategoryIdAsync(long categoryId)
        {
            return await GenericGetAsync<GroupSectionResponse>(string.Format("help_center/categories/{0}/sections.json?include=access_policies", categoryId));
        }

        public async Task<IndividualSectionResponse> GetSectionByIdAsync(long id)
        {
            return await GenericGetAsync<IndividualSectionResponse>(string.Format("help_center/sections/{0}.json?include=access_policies", id));
        }

        public async Task<IndividualSectionResponse> CreateSectionAsync(Section section)
        {
            var body = new { section };
            return await GenericPostAsync<IndividualSectionResponse>(string.Format("help_center/categories/{0}/sections.json?include=access_policies", section.CategoryId), body);
        }

        public async Task<IndividualSectionResponse> UpdateSectionAsync(Section section)
        {
            var body = new { section };
            return await GenericPutAsync<IndividualSectionResponse>(string.Format("help_center/sections/{0}.json?include=access_policies", section.Id), body);
        }

        public async Task<bool> DeleteSectionAsync(long id)
        {
            return await GenericDeleteAsync(string.Format("help_center/sections/{0}.json?include=access_policies", id));
        }

#endif
    }
}