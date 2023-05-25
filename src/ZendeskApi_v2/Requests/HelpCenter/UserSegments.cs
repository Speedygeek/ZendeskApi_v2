using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if ASYNC
using System.Threading.Tasks;
#endif
using ZendeskApi_v2.Models.UserSegments;

namespace ZendeskApi_v2.Requests.HelpCenter
{
    public interface IUserSegments : ICore
    {
#if SYNC
       IndividualUserSegmentResponse GetUserSegment(long userSegmentId);

       GroupUserSegmentResponse GetUserSegments(int? perPage = null, int? page = null);

       GroupUserSegmentResponse GetUserSegmentsApplicable(int? perPage = null, int? page = null);

       GroupUserSegmentResponse GetUserSegmentsByUserId(long userId, int? perPage = null, int? page = null);

       GroupUserSegmentResponse GetSectionsByUserSegmentId(long userSegmentId, int? perPage = null, int? page = null);

       GroupUserSegmentResponse GetTopicsByUserSegmentId(long userSegmentId, int? perPage = null, int? page = null);

       IndividualUserSegmentResponse UpdateUserSegment(UserSegment userSegment);

       IndividualUserSegmentResponse CreateUserSegment(UserSegment userSegment);

       bool DeleteUserSegment(long id);

#endif

#if ASYNC
        Task<IndividualUserSegmentResponse> GetUserSegmentAsync(long userSegmentId);

        Task<GroupUserSegmentResponse> GetUserSegmentsAsync(int? perPage = null, int? page = null);

        Task<GroupUserSegmentResponse> GetUserSegmentsApplicableAsync(int? perPage = null, int? page = null);

        Task<GroupUserSegmentResponse> GetUserSegmentsByUserIdAsync(long userId, int? perPage = null, int? page = null);

        Task<GroupUserSegmentResponse> GetSectionsByUserSegmentIdAsync(long userSegmentId, int? perPage = null, int? page = null);

        Task<GroupUserSegmentResponse> GetTopicsByUserSegmentIdAsync(long userSegmentId, int? perPage = null, int? page = null);

        Task<IndividualUserSegmentResponse> UpdateUserSegmentAsync(UserSegment userSegment);

        Task<IndividualUserSegmentResponse> CreateUserSegmentAsync(UserSegment userSegment);

        Task<bool> DeleteUserSegmentAsync(long id);

#endif
    }

    public class UserSegments : Core, IUserSegments
    {
        public UserSegments(string yourZendeskUrl, string user, string password, string apiToken, string p_OAuthToken)
            : base(yourZendeskUrl, user, password, apiToken, p_OAuthToken)
        {
        }

#if SYNC
        public IndividualUserSegmentResponse GetUserSegment(long userSegmentId)
        {
            return GenericGet<IndividualUserSegmentResponse>($"help_center/user_segments/{userSegmentId}.json");
        }

        public GroupUserSegmentResponse GetUserSegments(int? perPage = null,
            int? page = null)
        {
            return GenericPagedGet<GroupUserSegmentResponse>($"help_center/user_segments.json", perPage, page);
        }

        public GroupUserSegmentResponse GetUserSegmentsApplicable(int? perPage = null,
            int? page = null)
        {
            return GenericPagedGet<GroupUserSegmentResponse>($"help_center/user_segments/applicable.json", perPage, page);
        }

        public GroupUserSegmentResponse GetUserSegmentsByUserId(long userId,
            int? perPage = null,
            int? page = null)
        {
            return GenericPagedGet<GroupUserSegmentResponse>($"help_center/users/{userId}/user_segments.json", perPage, page);
        }

        public GroupUserSegmentResponse GetSectionsByUserSegmentId(long userSegmentId,
            int? perPage = null,
            int? page = null)
        {
            return GenericPagedGet<GroupUserSegmentResponse>($"help_center/user_segments/{userSegmentId}/sections.json", perPage, page);
        }

        public GroupUserSegmentResponse GetTopicsByUserSegmentId(long userSegmentId,
            int? perPage = null,
            int? page = null)
        {
            return GenericPagedGet<GroupUserSegmentResponse>($"help_center/user_segments/{userSegmentId}/topics.json", perPage, page);
        }

        public IndividualUserSegmentResponse UpdateUserSegment(UserSegment UserSegment)
        {
            return GenericPut<IndividualUserSegmentResponse>($"help_center/user_segments/{UserSegment.Id}.json", new IndividualUserSegmentResponse{UserSegment=UserSegment});
        }

        public IndividualUserSegmentResponse CreateUserSegment(UserSegment UserSegment)
        {
            return GenericPost<IndividualUserSegmentResponse>($"help_center/user_segments.json", new IndividualUserSegmentResponse{UserSegment=UserSegment}); 
        }

        public bool DeleteUserSegment(long id)
        {
            return GenericDelete($"help_center/user_segments/{id}.json");
        }
#endif
#if ASYNC
        public async Task<IndividualUserSegmentResponse> GetUserSegmentAsync(long userSegmentId)
        {
            return await GenericGetAsync<IndividualUserSegmentResponse>($"help_center/user_segments/{userSegmentId}.json");
        }

        public async Task<GroupUserSegmentResponse> GetUserSegmentsAsync(int? perPage = null,
            int? page = null)
        {
            return await GenericPagedGetAsync<GroupUserSegmentResponse>($"help_center/user_segments.json", perPage, page);
        }

        public async Task<GroupUserSegmentResponse> GetUserSegmentsApplicableAsync(int? perPage = null,
            int? page = null)
        {
            return await GenericPagedGetAsync<GroupUserSegmentResponse>($"help_center/user_segments/applicable.json", perPage, page);
        }

        public async Task<GroupUserSegmentResponse> GetUserSegmentsByUserIdAsync(long userId,
            int? perPage = null,
            int? page = null)
        {
            return await GenericPagedGetAsync<GroupUserSegmentResponse>($"help_center/users/{userId}/user_segments.json", perPage, page);
        }

        public async Task<GroupUserSegmentResponse> GetSectionsByUserSegmentIdAsync(long userSegmentId,
            int? perPage = null,
            int? page = null)
        {
            return await GenericPagedGetAsync<GroupUserSegmentResponse>($"help_center/user_segments/{userSegmentId}/sections.json", perPage, page);
        }

        public async Task<GroupUserSegmentResponse> GetTopicsByUserSegmentIdAsync(long userSegmentId,
            int? perPage = null,
            int? page = null)
        {
            return await GenericPagedGetAsync<GroupUserSegmentResponse>($"help_center/user_segments/{userSegmentId}/topics.json", perPage, page);
        }

        public async Task<IndividualUserSegmentResponse> UpdateUserSegmentAsync(UserSegment UserSegment)
        {
            return await GenericPutAsync<IndividualUserSegmentResponse>($"help_center/user_segments/{UserSegment.Id}.json", new IndividualUserSegmentResponse{UserSegment=UserSegment});
        }

        public async Task<IndividualUserSegmentResponse> CreateUserSegmentAsync(UserSegment UserSegment)
        {
            return await GenericPostAsync<IndividualUserSegmentResponse>($"help_center/user_segments.json", new IndividualUserSegmentResponse{UserSegment=UserSegment}); 
        }

        public async Task<bool> DeleteUserSegmentAsync(long id)
        {
            return await GenericDeleteAsync($"help_center/user_segments/{id}.json");
        }
#endif

    }
}
