#if ASYNC
using System.Threading.Tasks;
using ZendeskApi_v2.Models.HelpCenter.Subscriptions;
#endif
using ZendeskApi_v2.Models.Sections;

namespace ZendeskApi_v2.Requests.HelpCenter
{
    public interface ISections : ICore
    {
#if SYNC
        GroupSectionResponse GetSections();
        GroupSectionResponse GetSections(int perPage, int page);
        GroupSectionResponse GetSectionsByCategoryId(long categoryId);
        IndividualSectionResponse GetSectionById(long id);
        IndividualSectionResponse CreateSection(Section section);
        IndividualSectionResponse UpdateSection(Section section);
        bool DeleteSection(long id);
        IndividualSubscriptionResponse CreateSubscription(long sectionId, SectionSubscription section);
        IndividualSubscriptionResponse GetSubscription(long sectionId, long subscriptionId, SubscriptionSideLoadOptions sideLoadOptions = SubscriptionSideLoadOptions.None);
        GroupSubscriptionsResponse GetSubscriptions(long sectionId, SubscriptionSideLoadOptions sideLoadOptions = SubscriptionSideLoadOptions.None);
        bool DeleteSubscription(long sectionId, long subscriptionId);
#endif

#if ASYNC
        Task<GroupSectionResponse> GetSectionsAsync();
        Task<GroupSectionResponse> GetSectionsAsync(int perPage, int page);
        Task<GroupSectionResponse> GetSectionsByCategoryIdAsync(long categoryId);
        Task<IndividualSectionResponse> GetSectionByIdAsync(long id);
        Task<IndividualSectionResponse> CreateSectionAsync(Section section);
        Task<IndividualSectionResponse> UpdateSectionAsync(Section section);
        Task<bool> DeleteSectionAsync(long id);
        Task<IndividualSubscriptionResponse> CreateSubscriptionAsync(long sectionId, SectionSubscription section);
        Task<IndividualSubscriptionResponse> GetSubscriptionAsync(long sectionId, long subscriptionId, SubscriptionSideLoadOptions sideLoadOptions = SubscriptionSideLoadOptions.None);
        Task<GroupSubscriptionsResponse> GetSubscriptionsAsync(long sectionId, SubscriptionSideLoadOptions sideLoadOptions = SubscriptionSideLoadOptions.None);
        Task<bool> DeleteSubscriptionAsync(long sectionId, long subscriptionId);
#endif
    }

    public class Sections : Core, ISections
    {
        private readonly string _locale;
        private readonly string _generalSectionsPath;

        public Sections(string yourZendeskUrl, string user, string password, string apiToken, string locale, string p_OAuthToken)
            : base(yourZendeskUrl, user, password, apiToken, p_OAuthToken)
        {
            _locale = locale;
            _generalSectionsPath = string.IsNullOrWhiteSpace(_locale) ? "help_center/sections" : $"help_center/{_locale}/sections";
        }

#if SYNC
        public GroupSectionResponse GetSections()
        {
            return GenericGet<GroupSectionResponse>($"{_generalSectionsPath}.json?include=access_policies");
        }

        public GroupSectionResponse GetSections(int perPage, int page)
        {
            return GenericPagedGet<GroupSectionResponse>($"{_generalSectionsPath}.json?include=access_policies", perPage, page);
        }

        public GroupSectionResponse GetSectionsByCategoryId(long categoryId)
        {
            return GenericGet<GroupSectionResponse>(GetSectionPathWithCategory(categoryId));
        }

        public IndividualSectionResponse GetSectionById(long id)
        {
            return GenericGet<IndividualSectionResponse>($"{_generalSectionsPath}/{id}.json?include=access_policies");
        }

        public IndividualSectionResponse CreateSection(Section section)
        {
            return GenericPost<IndividualSectionResponse>(GetSectionPathWithCategory(section.CategoryId), new { section });
        }

        public IndividualSectionResponse UpdateSection(Section section)
        {
            return GenericPut<IndividualSectionResponse>($"{_generalSectionsPath}/{section.Id}.json?include=access_policies", new { section });
        }

        public bool DeleteSection(long id)
        {
            return GenericDelete($"help_center/sections/{id}.json?include=access_policies");
        }

        public IndividualSubscriptionResponse CreateSubscription(long sectionId, SectionSubscription subscription)
        {
            return GenericPost<IndividualSubscriptionResponse>($"help_center/sections/{sectionId}/subscriptions.json", new { subscription });
        }

        public IndividualSubscriptionResponse GetSubscription(long sectionId, long subscriptionId, SubscriptionSideLoadOptions sideLoadOptions = SubscriptionSideLoadOptions.None)
        {
            return GenericGet<IndividualSubscriptionResponse>($"{_generalSectionsPath}/{sectionId}/subscriptions/{subscriptionId}.json".SubscriptionSideloadUri(sideLoadOptions));
        }

        public GroupSubscriptionsResponse GetSubscriptions(long sectionId, SubscriptionSideLoadOptions sideLoadOptions = SubscriptionSideLoadOptions.None)
        {
            return GenericGet<GroupSubscriptionsResponse>($"{_generalSectionsPath}/{sectionId}/subscriptions.json".SubscriptionSideloadUri(sideLoadOptions));
        }

        public bool DeleteSubscription(long sectionId, long subscriptionId)
        {
            return GenericDelete($"{_generalSectionsPath}/{sectionId}/subscriptions/{subscriptionId}.json");
        }
#endif

#if ASYNC
        public async Task<GroupSectionResponse> GetSectionsAsync()
        {
            return await GenericGetAsync<GroupSectionResponse>($"{_generalSectionsPath}.json?include=access_policies");
        }

        public async Task<GroupSectionResponse> GetSectionsAsync(int perPage, int page)
        {
            return await GenericPagedGetAsync<GroupSectionResponse>($"{_generalSectionsPath}.json?include=access_policies", perPage, page);
        }

        public async Task<GroupSectionResponse> GetSectionsByCategoryIdAsync(long categoryId)
        {
            return await GenericGetAsync<GroupSectionResponse>(GetSectionPathWithCategory(categoryId));
        }

        public async Task<IndividualSectionResponse> GetSectionByIdAsync(long id)
        {
            return await GenericGetAsync<IndividualSectionResponse>($"{_generalSectionsPath}/{id}.json?include=access_policies");
        }

        public async Task<IndividualSectionResponse> CreateSectionAsync(Section section)
        {
            return await GenericPostAsync<IndividualSectionResponse>(GetSectionPathWithCategory(section.CategoryId), new { section });
        }

        public async Task<IndividualSectionResponse> UpdateSectionAsync(Section section)
        {
            return await GenericPutAsync<IndividualSectionResponse>($"{_generalSectionsPath}/{section.Id}.json?include=access_policies", new { section });
        }

        public async Task<bool> DeleteSectionAsync(long id)
        {
            return await GenericDeleteAsync($"help_center/sections/{id}.json?include=access_policies");
        }

        public Task<IndividualSubscriptionResponse> CreateSubscriptionAsync(long sectionId, SectionSubscription subscription)
        {
            return GenericPostAsync<IndividualSubscriptionResponse>($"help_center/sections/{sectionId}/subscriptions.json", new { subscription });
        }

        public Task<IndividualSubscriptionResponse> GetSubscriptionAsync(long sectionId, long subscriptionId, SubscriptionSideLoadOptions sideLoadOptions = SubscriptionSideLoadOptions.None)
        {
            return GenericGetAsync<IndividualSubscriptionResponse>($"{_generalSectionsPath}/{sectionId}/subscriptions/{subscriptionId}.json".SubscriptionSideloadUri(sideLoadOptions));
        }

        public Task<GroupSubscriptionsResponse> GetSubscriptionsAsync(long sectionId, SubscriptionSideLoadOptions sideLoadOptions = SubscriptionSideLoadOptions.None)
        {
            return GenericGetAsync<GroupSubscriptionsResponse>($"{_generalSectionsPath}/{sectionId}/subscriptions.json".SubscriptionSideloadUri(sideLoadOptions));
        }

        public Task<bool> DeleteSubscriptionAsync(long sectionId, long subscriptionId)
        {
            return GenericDeleteAsync($"{_generalSectionsPath}/{sectionId}/subscriptions/{subscriptionId}.json");
        }
#endif

        private string GetSectionPathWithCategory(long? categoryId)
        {
            return !string.IsNullOrWhiteSpace(_locale)
                ? $"help_center/{_locale}/categories/{categoryId}/sections.json?include=access_policies"
                : $"help_center/categories/{categoryId}/sections.json?include=access_policies";
        }
    }
}
