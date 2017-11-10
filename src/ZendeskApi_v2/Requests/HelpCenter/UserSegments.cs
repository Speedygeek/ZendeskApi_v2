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
       IndividualUserSegmentResponse GetUserSegment(int userSegmentId);

       GroupUserSegmentResponse GetUserSegments(int? perPage = null, int? page = null);

       GroupUserSegmentResponse GetUserSegmentsApplicable(int? perPage = null, int? page = null);

       GroupUserSegmentResponse GetUserSegmentsByUserId(long userId, int? perPage = null, int? page = null);

       GroupUserSegmentResponse GetSectionsByUserSegmentId(int userSegmentId, int? perPage = null, int? page = null);

       GroupUserSegmentResponse GetTopicsByUserSegmentId(int userSegmentId, int? perPage = null, int? page = null);

       IndividualUserSegmentResponse UpdateUserSegment(UserSegment userSegment);

       IndividualUserSegmentResponse CreateUserSegment(UserSegment userSegment);

       bool DeleteUserSegment(int id);

#endif

#if ASYNC
        Task<IndividualUserSegmentResponse> GetUserSegmentAsync(int userSegmentId);

        Task<GroupUserSegmentResponse> GetUserSegmentsAsync(int? perPage = null, int? page = null);

        Task<GroupUserSegmentResponse> GetUserSegmentsApplicableAsync(int? perPage = null, int? page = null);

        Task<GroupUserSegmentResponse> GetUserSegmentsByUserIdAsync(long userId, int? perPage = null, int? page = null);

        Task<GroupUserSegmentResponse> GetSectionsByUserSegmentIdAsync(int userSegmentId, int? perPage = null, int? page = null);

        Task<GroupUserSegmentResponse> GetTopicsByUserSegmentIdAsync(int userSegmentId, int? perPage = null, int? page = null);

        Task<IndividualUserSegmentResponse> UpdateUserSegmentAsync(UserSegment userSegment);

        Task<IndividualUserSegmentResponse> CreateUserSegmentAsync(UserSegment userSegment);

        Task<bool> DeleteUserSegmentAsync(int id);

#endif
    }

    public class UserSegments : Core, IUserSegments
    {
        public UserSegments(string yourZendeskUrl, string user, string password, string apiToken, string p_OAuthToken)
            : base(yourZendeskUrl, user, password, apiToken, p_OAuthToken)
        {
        }

#if SYNC
        public IndividualUserSegmentResponse GetUserSegment(int userSegmentId)
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

        public GroupUserSegmentResponse GetSectionsByUserSegmentId(int userSegmentId,
            int? perPage = null,
            int? page = null)
        {
            return GenericPagedGet<GroupUserSegmentResponse>($"help_center/user_segments/{userSegmentId}/sections.json", perPage, page);
        }

        public GroupUserSegmentResponse GetTopicsByUserSegmentId(int userSegmentId,
            int? perPage = null,
            int? page = null)
        {
            return GenericPagedGet<GroupUserSegmentResponse>($"help_center/user_segments/{userSegmentId}/topics.json", perPage, page);
        }


        public IndividualUserSegmentResponse UpdateUserSegment(UserSegment user_segment)
        {
            return GenericPut<IndividualUserSegmentResponse>($"help_center/user_segments/{user_segment.Id}.json", user_segment);
        }

        public IndividualUserSegmentResponse CreateUserSegment(UserSegment user_segment)
        {
            return GenericPost<IndividualUserSegmentResponse>($"help_center/user_segments.json", new { user_segment }); 
        }

        public bool DeleteUserSegment(int id)
        {
            return GenericDelete($"help_center/user_segments/{id}.json");
        }
#endif
#if ASYNC
        public async Task<IndividualUserSegmentResponse> GetUserSegmentAsync(int userSegmentId)
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

        public async Task<GroupUserSegmentResponse> GetSectionsByUserSegmentIdAsync(int userSegmentId,
            int? perPage = null,
            int? page = null)
        {
            return await GenericPagedGetAsync<GroupUserSegmentResponse>($"help_center/user_segments/{userSegmentId}/sections.json", perPage, page);
        }

        public async Task<GroupUserSegmentResponse> GetTopicsByUserSegmentIdAsync(int userSegmentId,
            int? perPage = null,
            int? page = null)
        {
            return await GenericPagedGetAsync<GroupUserSegmentResponse>($"help_center/user_segments/{userSegmentId}/topics.json", perPage, page);
        }


        public async Task<IndividualUserSegmentResponse> UpdateUserSegmentAsync(UserSegment user_segment)
        {
            return await GenericPutAsync<IndividualUserSegmentResponse>($"help_center/user_segments/{user_segment.Id}.json", user_segment);
        }

        public async Task<IndividualUserSegmentResponse> CreateUserSegmentAsync(UserSegment user_segment)
        {
            return await GenericPostAsync<IndividualUserSegmentResponse>($"help_center/user_segments.json", new { user_segment }); 
        }

        public async Task<bool> DeleteUserSegmentAsync(int id)
        {
            return await GenericDeleteAsync($"help_center/user_segments/{id}.json");
        }
#endif

    }
}
