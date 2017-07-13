#if ASYNC
using System.Threading.Tasks;
#endif
using ZendeskApi_v2.Models.HelpCenter.Post;

namespace ZendeskApi_v2.Requests.HelpCenter
{
    public interface IPosts : ICore
    {
#if SYNC
        GroupPostResponse GetPosts(int? perPage = null, int? page = null);
        GroupPostResponse GetPostsByTopicId(long topicId, int? perPage = null, int? page = null);
        GroupPostResponse GetPostsByUserId(long userId, int? perPage = null, int? page = null);
        IndividualPostResponse GetPost(long postId);
        IndividualPostResponse CreatePost(Post post);
        IndividualPostResponse UpdatePost(Post post);
        bool DeletePost(long PostId);
#endif
#if ASYNC
        Task<GroupPostResponse> GetPostsAsync(int? perPage = null, int? page = null);
        Task<GroupPostResponse> GetPostsByTopicIdAsync(long topicId, int? perPage = null, int? page = null);
        Task<GroupPostResponse> GetPostsByUserIdAsync(long userId, int? perPage = null, int? page = null);
        Task<IndividualPostResponse> GetPostAsync(long postId);
        Task<IndividualPostResponse> CreatePostAsync(Post post);
        Task<IndividualPostResponse> UpdatePostAsync(Post post);
        Task<bool> DeletePostAsync(long PostId);
#endif
    }

    public class Posts : Core, IPosts
    {
        public Posts(string yourZendeskUrl, string user, string password, string apiToken, string p_OAuthToken)
            : base(yourZendeskUrl, user, password, apiToken, p_OAuthToken)
        {
        }
#if SYNC
        public GroupPostResponse GetPosts(int? perPage = null, int? page = null)
        {
            return GenericPagedGet<GroupPostResponse>("community/posts.json", perPage, page);
        }

        public GroupPostResponse GetPostsByTopicId(long topicId, int? perPage = null, int? page = null)
        {
            return GenericPagedGet<GroupPostResponse>($"community/topics/{topicId}/posts.json", perPage, page);
        }

        public GroupPostResponse GetPostsByUserId(long userId, int? perPage = null, int? page = null)
        {
            return GenericPagedGet<GroupPostResponse>($"community/users/{userId}/posts.json", perPage, page);
        }

        public IndividualPostResponse GetPost(long postId)
        {
            return GenericGet<IndividualPostResponse>($"community/posts/{postId}.json");
        }

        public IndividualPostResponse CreatePost(Post post)
        {
            var body = new { post };
            return GenericPost<IndividualPostResponse>("community/posts.json", body);
        }

        public IndividualPostResponse UpdatePost(Post post)
        {
            var body = new { post };
            return GenericPut<IndividualPostResponse>($"community/posts/{post.Id.Value}.json", body);
        }

        public bool DeletePost(long postId)
        {
            return GenericDelete($"community/posts/{postId}.json");
        }
#endif
#if ASYNC
        public async Task<IndividualPostResponse> CreatePostAsync(Post post)
        {
            var body = new { post };
            return await GenericPostAsync<IndividualPostResponse>("community/posts.json", body);
        }

        public async Task<IndividualPostResponse> GetPostAsync(long postId)
        {
            return await GenericGetAsync<IndividualPostResponse>($"community/posts/{postId}.json");
        }

        public Task<GroupPostResponse> GetPostsByTopicIdAsync(long topicId, int? perPage = null, int? page = null)
        {
            return GenericPagedGetAsync<GroupPostResponse>($"community/topics/{topicId}/posts.json", perPage, page);
        }

        public async Task<GroupPostResponse> GetPostsByUserIdAsync(long userId, int? perPage = null, int? page = null)
        {
            return await GenericPagedGetAsync<GroupPostResponse>($"community/users/{userId}/posts.json", perPage, page);
        }

        public async Task<GroupPostResponse> GetPostsAsync(int? perPage = null, int? page = null)
        {
            return await GenericPagedGetAsync<GroupPostResponse>("community/posts.json", perPage, page);
        }

        public async Task<IndividualPostResponse> UpdatePostAsync(Post post)
        {
            var body = new { post };
            return await GenericPutAsync<IndividualPostResponse>($"community/posts/{post.Id.Value}.json", body);
        }

        public async Task<bool> DeletePostAsync(long postId)
        {
            return await GenericDeleteAsync($"community/posts/{postId}.json");
        }
#endif
    }
}
