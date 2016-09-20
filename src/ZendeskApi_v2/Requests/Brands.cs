#if ASYNC
using System.Threading.Tasks;
#endif
using System.Collections.Generic;
using ZendeskApi_v2.Models.Brands;
using System;

namespace ZendeskApi_v2.Requests
{
    public interface IBrands : ICore
    {
#if SYNC
        GroupBrandResponse GetBrands();
        IndividualBrandResponse GetBrand(long id);
        IndividualBrandResponse CreateBrand(Brand brand);
        IndividualBrandResponse UpdateBrand(Brand brand);
        bool DeleteBrand(long id);
#endif

#if ASYNC
        Task<GroupBrandResponse> GetBrandsAsync();
        Task<IndividualBrandResponse> GetBrandAsync(long id);
        Task<IndividualBrandResponse> CreateBrandAsync(Brand brand);
        Task<IndividualBrandResponse> UpdateBrandAsync(Brand brand);
        Task<bool> DeleteBrandAsync(long id);
#endif
    }

    public class Brands : Core, IBrands
    {
        public Brands(string yourZendeskUrl, string user, string password, string apiToken, string p_OAuthToken)
            : base(yourZendeskUrl, user, password, apiToken, p_OAuthToken)
        {
        }

#if SYNC
        public GroupBrandResponse GetBrands()
        {
            return GenericGet<GroupBrandResponse>(string.Format("brands.json"));
        }

        public IndividualBrandResponse GetBrand(long id)
        {
            return GenericGet<IndividualBrandResponse>(string.Format("brands/{0}.json", id));
        }

        public IndividualBrandResponse CreateBrand(Brand brand)
        {
            var body = new { brand };
            return GenericPost<IndividualBrandResponse>("brands.json", body);
        }

        public IndividualBrandResponse UpdateBrand(Brand brand)
        {
            var body = new { brand };
            return GenericPut<IndividualBrandResponse>(string.Format("brands/{0}.json", brand.Id), body);
        }

        public bool DeleteBrand(long id)
        {
            return GenericDelete(string.Format("brands/{0}.json", id));
        }
#endif

#if ASYNC    
        public async Task<GroupBrandResponse> GetBrandsAsync()
        {
            return await GenericGetAsync<GroupBrandResponse>(string.Format("brands.json"));
        }

        public async Task<IndividualBrandResponse> GetBrandAsync(long id)
        {
            return await GenericGetAsync<IndividualBrandResponse>(string.Format("brands/{0}.json", id));
        }

        public async Task<IndividualBrandResponse> CreateBrandAsync(Brand brand)
        {
            var body = new { brand };
            return await GenericPostAsync<IndividualBrandResponse>("brands.json", body);
        }

        public async Task<IndividualBrandResponse> UpdateBrandAsync(Brand brand)
        {
            var body = new { brand };
            return await GenericPutAsync<IndividualBrandResponse>(string.Format("brands/{0}.json", brand.Id), body);
        }

        public async Task<bool> DeleteBrandAsync(long id)
        {
            return await GenericDeleteAsync(string.Format("brands/{0}.json", id));
        }
#endif
    }
}