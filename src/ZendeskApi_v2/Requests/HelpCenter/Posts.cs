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
            return GenericPagedGet<GroupPostResponse>(string.Format("community/topics/{0}/posts.json", topicId), perPage, page);
        }

        public GroupPostResponse GetPostsByUserId(long userId, int? perPage = null, int? page = null)
        {
            return GenericPagedGet<GroupPostResponse>(string.Format("community/users/{0}/posts.json", userId), perPage, page);
        }

        public IndividualPostResponse GetPost(long postId)
        {
            return GenericGet<IndividualPostResponse>(string.Format("community/posts/{0}.json", postId));
        }

        public IndividualPostResponse CreatePost(Post post)
        {
            var body = new { post };
            return GenericPost<IndividualPostResponse>("community/posts.json", body);
        }

        public IndividualPostResponse UpdatePost(Post post)
        {
            var body = new { post };
            return GenericPut<IndividualPostResponse>(string.Format("community/posts/{0}.json", post.Id.Value), body);
        }

        public bool DeletePost(long postId)
        {
            return GenericDelete(string.Format("community/posts/{0}.json", postId));
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
            return await GenericGetAsync<IndividualPostResponse>(string.Format("community/posts/{0}.json", postId));
        }

        public Task<GroupPostResponse> GetPostsByTopicIdAsync(long topicId, int? perPage = null, int? page = null)
        {
            return GenericPagedGetAsync<GroupPostResponse>(string.Format("community/topics/{0}/posts.json", topicId), perPage, page);
        }

        public async Task<GroupPostResponse> GetPostsByUserIdAsync(long userId, int? perPage = null, int? page = null)
        {
            return await GenericPagedGetAsync<GroupPostResponse>(string.Format("community/users/{0}/posts.json", userId), perPage, page);
        }

        public async Task<GroupPostResponse> GetPostsAsync(int? perPage = null, int? page = null)
        {
            return await GenericPagedGetAsync<GroupPostResponse>("community/posts.json", perPage, page);
        }

        public async Task<IndividualPostResponse> UpdatePostAsync(Post post)
        {
            var body = new { post };
            return await GenericPutAsync<IndividualPostResponse>(string.Format("community/posts/{0}.json", post.Id.Value), body);
        }

        public async Task<bool> DeletePostAsync(long postId)
        {
            return await GenericDeleteAsync(string.Format("community/posts/{0}.json", postId));
        }
#endif
    }
}
