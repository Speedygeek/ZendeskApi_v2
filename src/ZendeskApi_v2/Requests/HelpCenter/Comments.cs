#if ASYNC
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
	    GroupCommentResponse GetCommentsForPost(long? postId, int? perPage = null, int? page = null);
	    IndividualCommentsResponse CreateCommentForArticle(long articleId, Comment comment);
	    IndividualCommentsResponse CreateCommentForPost(long postId, Comment comment);
	    IndividualCommentsResponse UpdateCommentForArticle(long articleId, Comment comment);
	    IndividualCommentsResponse UpdateCommentForPost(long postId, Comment comment);
        bool DeleteCommentForArticle(long articleId, long commentId);
        bool DeleteCommentForPost(long postId, long commentId);
#endif
#if ASYNC
        Task<GroupCommentResponse> GetCommentsForArticleAsync(long? articleId);
        Task<GroupCommentResponse> GetCommentsForUserAsync(long? userId);
        Task<GroupCommentResponse> GetCommentsForCurrentUserAsync();
	    Task<GroupCommentResponse> GetCommentsForPostAsync(long? postId);
        Task<IndividualCommentsResponse> CreateCommentForArticleAsync(long articleId, Comment comment);
	    Task<IndividualCommentsResponse> CreateCommentForPostAsync(long postId, Comment comment);
	    Task<IndividualCommentsResponse> UpdateCommentForArticleAsync(long articleId, Comment comment);
	    Task<IndividualCommentsResponse> UpdateCommentForPostAsync(long postId, Comment comment);
	    Task<bool> DeleteCommentForArticleAsync(long articleId, long commentId);
	    Task<bool> DeleteCommentForPostAsync(long postId, long commentId);
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
			return GenericPagedGet<GroupCommentResponse>($"help_center/articles/{articleId}/comments.json", perPage, page);
		}

        public GroupCommentResponse GetCommentsForUser(long? userId)
		{
			return GenericGet<GroupCommentResponse>($"help_center/users/{userId}/comments.json");
		}

        public GroupCommentResponse GetCommentsForCurrentUser()
		{
			return GenericGet<GroupCommentResponse>("help_center/users/me/comments.json");
		}

	    public GroupCommentResponse GetCommentsForPost(long? postId, int? perPage = null, int? page = null)
	    {
	        return GenericPagedGet<GroupCommentResponse>($"community/posts/{postId}/comments.json", perPage, page);
        }

	    public IndividualCommentsResponse CreateCommentForArticle(long articleId, Comment comment)
	    {
	        var body = new { comment };
	        return GenericPost<IndividualCommentsResponse>($"help_center/articles/{articleId}/comments.json", body);
        }
        
	    public IndividualCommentsResponse CreateCommentForPost(long postId, Comment comment)
	    {
	        var body = new { comment };
	        return GenericPost<IndividualCommentsResponse>($"community/posts/{postId}/comments.json", body);
        }

	    public IndividualCommentsResponse UpdateCommentForArticle(long articleId, Comment comment)
	    {
	        var body = new { comment };
	        return GenericPut<IndividualCommentsResponse>($"help_center/articles/{articleId}/comments/{comment.Id}.json", body);
        }

	    public IndividualCommentsResponse UpdateCommentForPost(long postId, Comment comment)
	    {
	        var body = new { comment };
	        return GenericPut<IndividualCommentsResponse>($"community/posts/{postId}/comments/{comment.Id}.json", body);
        }

	    public bool DeleteCommentForArticle(long articleId, long commentId)
	    {
	        return GenericDelete($"help_center/articles/{articleId}/comments/{commentId}.json");
        }

	    public bool DeleteCommentForPost(long postId, long commentId)
	    {
	        return GenericDelete($"community/posts/{postId}/comments/{commentId}.json");
        }
#endif
#if ASYNC

		public async Task<GroupCommentResponse> GetCommentsForArticleAsync(long? articleId)
		{
			return await GenericGetAsync<GroupCommentResponse>($"help_center/articles/{articleId}/comments.json");
		}

        public async Task<GroupCommentResponse> GetCommentsForUserAsync(long? userId)
		{
			return await GenericGetAsync<GroupCommentResponse>($"help_center/users/{userId}/comments.json");
		}

        public async Task<GroupCommentResponse> GetCommentsForCurrentUserAsync()
		{
			return await GenericGetAsync<GroupCommentResponse>("help_center/users/me/comments.json");
		}

	    public async Task<GroupCommentResponse> GetCommentsForPostAsync(long? postId)
	    {
	        return await GenericGetAsync<GroupCommentResponse>($"community/posts/{postId}/comments.json");
        }

	    public async Task<IndividualCommentsResponse> CreateCommentForArticleAsync(long articleId, Comment comment)
	    {
	        var body = new { comment };
	        return await GenericPostAsync<IndividualCommentsResponse>($"help_center/articles/{articleId}/comments.json", body);
        }

	    public async Task<IndividualCommentsResponse> CreateCommentForPostAsync(long postId, Comment comment)
	    {
	        var body = new { comment };
	        return await GenericPostAsync<IndividualCommentsResponse>($"community/posts/{postId}/comments.json", body);
        }

	    public async Task<IndividualCommentsResponse> UpdateCommentForArticleAsync(long articleId, Comment comment)
	    {
	        var body = new { comment };
	        return await GenericPutAsync<IndividualCommentsResponse>($"help_center/articles/{articleId}/comments/{comment.Id}.json", body);
        }

	    public async Task<IndividualCommentsResponse> UpdateCommentForPostAsync(long postId, Comment comment)
	    {
	        var body = new { comment };
	        return await GenericPutAsync<IndividualCommentsResponse>($"community/posts/{postId}/comments/{comment.Id}.json", body);
        }

	    public async Task<bool> DeleteCommentForArticleAsync(long articleId, long commentId)
	    {
	        return await GenericDeleteAsync($"help_center/articles/{articleId}/comments/{commentId}.json");
        }

	    public async Task<bool> DeleteCommentForPostAsync(long postId, long commentId)
	    {
	        return await GenericDeleteAsync($"community/posts/{postId}/comments/{commentId}.json");
        }
#endif
	}
}
