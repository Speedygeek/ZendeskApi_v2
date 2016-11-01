﻿#if ASYNC

using System.Threading.Tasks;

#endif

using ZendeskApi_v2.Models.HelpCenter.Comments;

namespace ZendeskApi_v2.Requests.HelpCenter
{
    public interface IComments : ICore
    {
#if SYNC

        GroupCommentResponse GetCommentsForArticle(long? articleId, int? perPage = null, int? page = null);

        GroupCommentResponse GetCommentsForUser(long? userId);

        GroupCommentResponse GetCommentsForCurrentUser();

#endif
#if ASYNC

        Task<GroupCommentResponse> GetCommentsForArticleAsync(long? articleId);

        Task<GroupCommentResponse> GetCommentsForUserAsync(long? userId);

        Task<GroupCommentResponse> GetCommentsForCurrentUserAsync();

#endif
    }

    /// <summary>
    /// https://developer.zendesk.com/rest_api/docs/help_center/comments
    /// </summary>
	public class Comments : Core, IComments
    {
        public Comments(string yourZendeskUrl, string user, string password, string apiToken, string p_OAuthToken)
            : base(yourZendeskUrl, user, password, apiToken, p_OAuthToken)
        {
        }

#if SYNC

        public GroupCommentResponse GetCommentsForArticle(long? articleId, int? perPage = null, int? page = null)
        {
            return GenericPagedGet<GroupCommentResponse>(string.Format("help_center/articles/{0}/comments.json", articleId), perPage, page);
        }

        public GroupCommentResponse GetCommentsForUser(long? userId)
        {
            return GenericGet<GroupCommentResponse>(string.Format("help_center/users/{0}/comments.json", userId));
        }

        public GroupCommentResponse GetCommentsForCurrentUser()
        {
            return GenericGet<GroupCommentResponse>("help_center/users/me/comments.json");
        }

#endif
#if ASYNC

        public async Task<GroupCommentResponse> GetCommentsForArticleAsync(long? articleId)
        {
            return await GenericGetAsync<GroupCommentResponse>(string.Format("help_center/articles/{0}/comments.json", articleId));
        }

        public async Task<GroupCommentResponse> GetCommentsForUserAsync(long? userId)
        {
            return await GenericGetAsync<GroupCommentResponse>(string.Format("help_center/users/{0}/comments.json", userId));
        }

        public async Task<GroupCommentResponse> GetCommentsForCurrentUserAsync()
        {
            return await GenericGetAsync<GroupCommentResponse>("help_center/users/me/comments.json");
        }

#endif
    }
}