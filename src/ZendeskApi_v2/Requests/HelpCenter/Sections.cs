using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if ASYNC
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
            return GenericGet<GroupSectionResponse>($"help_center/categories/{categoryId}/sections.json?include=access_policies");
        }

        public IndividualSectionResponse GetSectionById(long id)
        {
            return GenericGet<IndividualSectionResponse>($"help_center/sections/{id}.json?include=access_policies");
        }

        public IndividualSectionResponse CreateSection(Section section)
        {
            var body = new { section };
            return GenericPost<IndividualSectionResponse>($"help_center/categories/{section.CategoryId}/sections.json?include=access_policies", body);
        }

        public IndividualSectionResponse UpdateSection(Section section)
        {
            var body = new { section };
            return GenericPut<IndividualSectionResponse>($"help_center/sections/{section.Id}.json?include=access_policies", body);
        }

        public bool DeleteSection(long id)
        {
            return GenericDelete($"help_center/sections/{id}.json?include=access_policies");
        }

#endif

#if ASYNC

        public async Task<GroupSectionResponse> GetSectionsAsync()
        {
            return await GenericGetAsync<GroupSectionResponse>("help_center/sections.json?include=access_policies");
        }

        public async Task<GroupSectionResponse> GetSectionsByCategoryIdAsync(long categoryId)
        {
            return await GenericGetAsync<GroupSectionResponse>($"help_center/categories/{categoryId}/sections.json?include=access_policies");
        }

        public async Task<IndividualSectionResponse> GetSectionByIdAsync(long id)
        {
            return await GenericGetAsync<IndividualSectionResponse>($"help_center/sections/{id}.json?include=access_policies");
        }

        public async Task<IndividualSectionResponse> CreateSectionAsync(Section section)
        {
            var body = new { section };
            return await GenericPostAsync<IndividualSectionResponse>($"help_center/categories/{section.CategoryId}/sections.json?include=access_policies", body);
        }

        public async Task<IndividualSectionResponse> UpdateSectionAsync(Section section)
        {
            var body = new { section };
            return await GenericPutAsync<IndividualSectionResponse>($"help_center/sections/{section.Id}.json?include=access_policies", body);
        }

        public async Task<bool> DeleteSectionAsync(long id)
        {
            return await GenericDeleteAsync($"help_center/sections/{id}.json?include=access_policies");
        }

#endif
    }
}
