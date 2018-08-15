using System.Threading.Tasks;
using NUnit.Framework;
using ZendeskApi_v2;
using ZendeskApi_v2.Models.Articles;
using ZendeskApi_v2.Models.HelpCenter.Comments;
using ZendeskApi_v2.Models.HelpCenter.Post;

namespace Tests
{
    [TestFixture]
    public class CommentTests
    {
        private ZendeskApi api = new ZendeskApi(Settings.Site, Settings.AdminEmail, Settings.AdminPassword);
        private readonly long _testSectionIdForCommentsTest = 360000205286; //https://csharpapi.zendesk.com/hc/en-us/sections/360000205286-Test-Section-For-Comment-Tests
        private long _testTopicIdForCommentsTest = 360000016546; //https://csharpapi.zendesk.com/hc/en-us/community/topics/360000016546-Test-Topic-For-Comment-Tests

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            DeleteAllArticlesFromTestSection(); //Get rid of all articles in the test-section (there should be none but there could be if previous tests have failed)
            DeleteAllPostsFromTestTopic(); //Get rid of all posts in the test-topic (there should be none but there could be if previous tests have failed)
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            DeleteAllArticlesFromTestSection(); //Clean-up after tests
            DeleteAllPostsFromTestTopic(); //Clean-up after tests
        }

        private void DeleteAllArticlesFromTestSection()
        {
            var articlesBySectionId = api.HelpCenter.Articles.GetArticlesBySectionId(_testSectionIdForCommentsTest);
            foreach (var article in articlesBySectionId.Articles)
            {
                api.HelpCenter.Articles.DeleteArticle(article.Id.Value);
            }
        }

        private void DeleteAllPostsFromTestTopic()
        {
            var articlesBySectionId = api.HelpCenter.Posts.GetPostsByTopicId(_testTopicIdForCommentsTest);
            foreach (var post in articlesBySectionId.Posts)
            {
                api.HelpCenter.Posts.DeletePost(post.Id.Value);
            }
        }

        [Test]
        public void CanCreateUpdateAndDeleteCommentsForArticle()
        {
            var articleId = CreateTestArticle("Test-Article-For-Comments");

            //Create 3 Comments
            var individualCommentsResponse1 = api.HelpCenter.Comments.CreateCommentForArticle(articleId, new Comment {Body = "Comment 1", Locale = "en-us"});
            Assert.NotNull(individualCommentsResponse1.Comment);
            Assert.Greater(individualCommentsResponse1.Comment.Id, 0);

            var individualCommentsResponse2 = api.HelpCenter.Comments.CreateCommentForArticle(articleId, new Comment {Body = "Comment 2", Locale = "en-us" });
            Assert.NotNull(individualCommentsResponse2.Comment);
            Assert.Greater(individualCommentsResponse2.Comment.Id, 0);

            var individualCommentsResponse3 = api.HelpCenter.Comments.CreateCommentForArticle(articleId, new Comment {Body = "Comment 3", Locale = "en-us" });
            Assert.NotNull(individualCommentsResponse3.Comment);
            Assert.Greater(individualCommentsResponse3.Comment.Id, 0);
            
            Assert.AreEqual(3, api.HelpCenter.Comments.GetCommentsForArticle(articleId).Count); //That article have 3 comments

            Assert.AreEqual("Comment 1", individualCommentsResponse1.Comment.Body);
            Assert.AreEqual("Comment 2", individualCommentsResponse2.Comment.Body);
            Assert.AreEqual("Comment 3", individualCommentsResponse3.Comment.Body);

            //Update Comment
            var updatedCommentBody = "Comment 2 Updated";
            individualCommentsResponse2.Comment.Body = updatedCommentBody;
            var updateCommentForArticle = api.HelpCenter.Comments.UpdateCommentForArticle(articleId, individualCommentsResponse2.Comment);
            Assert.AreEqual(updatedCommentBody, updateCommentForArticle.Comment.Body);

            //Delete Comment
            api.HelpCenter.Comments.DeleteCommentForArticle(articleId, individualCommentsResponse2.Comment.Id.Value);
            Assert.AreEqual(2, api.HelpCenter.Comments.GetCommentsForArticle(articleId).Count); //One less comments now
        }

        [Test]
        public void CanCreateUpdateAndDeleteCommentsForPost()
        {
            var postId = CreateTestPost("Test-Post-For-Comments");

            //Create 3 Comments
            var individualCommentsResponse1 = api.HelpCenter.Comments.CreateCommentForPost(postId, new Comment { Body = "Comment 1" });
            Assert.NotNull(individualCommentsResponse1.Comment);
            Assert.Greater(individualCommentsResponse1.Comment.Id, 0);

            var individualCommentsResponse2 = api.HelpCenter.Comments.CreateCommentForPost(postId, new Comment { Body = "Comment 2" });
            Assert.NotNull(individualCommentsResponse2.Comment);
            Assert.Greater(individualCommentsResponse2.Comment.Id, 0);

            var individualCommentsResponse3 = api.HelpCenter.Comments.CreateCommentForPost(postId, new Comment { Body = "Comment 3" });
            Assert.NotNull(individualCommentsResponse3.Comment);
            Assert.Greater(individualCommentsResponse3.Comment.Id, 0);

            Assert.AreEqual(3, api.HelpCenter.Comments.GetCommentsForPost(postId).Count); //That post have 3 comments

            Assert.AreEqual("Comment 1", individualCommentsResponse1.Comment.Body);
            Assert.AreEqual("Comment 2", individualCommentsResponse2.Comment.Body);
            Assert.AreEqual("Comment 3", individualCommentsResponse3.Comment.Body);

            //Update Comment
            var updatedCommentBody = "Comment 2 Updated";
            individualCommentsResponse2.Comment.Body = updatedCommentBody;
            var updateCommentForPost = api.HelpCenter.Comments.UpdateCommentForPost(postId, individualCommentsResponse2.Comment);
            Assert.AreEqual(updatedCommentBody, updateCommentForPost.Comment.Body);

            //Delete Comment
            api.HelpCenter.Comments.DeleteCommentForPost(postId, individualCommentsResponse2.Comment.Id.Value);
            Assert.AreEqual(2, api.HelpCenter.Comments.GetCommentsForPost(postId).Count); //One less comments now
        }

        [Test]
        public async Task CanCreateUpdateAndDeleteCommentsForArticleAsync()
        {
            var articleId = CreateTestArticle("Test-Article-For-Comments-Async");

            //Create 3 Comments
            var individualCommentsResponse1 = await api.HelpCenter.Comments.CreateCommentForArticleAsync(articleId, new Comment {Body = "Comment 1", Locale = "en-us"});
            Assert.NotNull(individualCommentsResponse1.Comment);
            Assert.Greater(individualCommentsResponse1.Comment.Id, 0);

            var individualCommentsResponse2 = await api.HelpCenter.Comments.CreateCommentForArticleAsync(articleId, new Comment {Body = "Comment 2", Locale = "en-us" });
            Assert.NotNull(individualCommentsResponse2.Comment);
            Assert.Greater(individualCommentsResponse2.Comment.Id, 0);

            var individualCommentsResponse3 = await api.HelpCenter.Comments.CreateCommentForArticleAsync(articleId, new Comment {Body = "Comment 3", Locale = "en-us" });
            Assert.NotNull(individualCommentsResponse3.Comment);
            Assert.Greater(individualCommentsResponse3.Comment.Id, 0);
            
            Assert.AreEqual(3, (await api.HelpCenter.Comments.GetCommentsForArticleAsync(articleId)).Count); //That article have 3 comments

            Assert.AreEqual("Comment 1", individualCommentsResponse1.Comment.Body);
            Assert.AreEqual("Comment 2", individualCommentsResponse2.Comment.Body);
            Assert.AreEqual("Comment 3", individualCommentsResponse3.Comment.Body);

            //Update Comment
            var updatedCommentBody = "Comment 2 Updated";
            individualCommentsResponse2.Comment.Body = updatedCommentBody;
            var updateCommentForArticle = await api.HelpCenter.Comments.UpdateCommentForArticleAsync(articleId, individualCommentsResponse2.Comment);
            Assert.AreEqual(updatedCommentBody, updateCommentForArticle.Comment.Body);

            //Delete Comment
            await api.HelpCenter.Comments.DeleteCommentForArticleAsync(articleId, individualCommentsResponse2.Comment.Id.Value);
            Assert.AreEqual(2, (await api.HelpCenter.Comments.GetCommentsForArticleAsync(articleId)).Count); //One less comments now
        }

        [Test]
        public async Task CanCreateUpdateAndDeleteCommentsForPostAsync()
        {
            var postId = CreateTestPost("Test-Post-For-Comments-Async");

            //Create 3 Comments
            var individualCommentsResponse1 = await api.HelpCenter.Comments.CreateCommentForPostAsync(postId, new Comment { Body = "Comment 1" });
            Assert.NotNull(individualCommentsResponse1.Comment);
            Assert.Greater(individualCommentsResponse1.Comment.Id, 0);

            var individualCommentsResponse2 = await api.HelpCenter.Comments.CreateCommentForPostAsync(postId, new Comment { Body = "Comment 2" });
            Assert.NotNull(individualCommentsResponse2.Comment);
            Assert.Greater(individualCommentsResponse2.Comment.Id, 0);

            var individualCommentsResponse3 = await api.HelpCenter.Comments.CreateCommentForPostAsync(postId, new Comment { Body = "Comment 3" });
            Assert.NotNull(individualCommentsResponse3.Comment);
            Assert.Greater(individualCommentsResponse3.Comment.Id, 0);

            Assert.AreEqual(3, (await api.HelpCenter.Comments.GetCommentsForPostAsync(postId)).Count); //That post have 3 comments

            Assert.AreEqual("Comment 1", individualCommentsResponse1.Comment.Body);
            Assert.AreEqual("Comment 2", individualCommentsResponse2.Comment.Body);
            Assert.AreEqual("Comment 3", individualCommentsResponse3.Comment.Body);

            //Update Comment
            var updatedCommentBody = "Comment 2 Updated";
            individualCommentsResponse2.Comment.Body = updatedCommentBody;
            var updateCommentForPost = await api.HelpCenter.Comments.UpdateCommentForPostAsync(postId, individualCommentsResponse2.Comment);
            Assert.AreEqual(updatedCommentBody, updateCommentForPost.Comment.Body);

            //Delete Comment
            await api.HelpCenter.Comments.DeleteCommentForPostAsync(postId, individualCommentsResponse2.Comment.Id.Value);
            Assert.AreEqual(2, (await api.HelpCenter.Comments.GetCommentsForPostAsync(postId)).Count); //One less comments now
        }

        private long CreateTestArticle(string testArticleForComments)
        {
            var article = new Article {Title = testArticleForComments, Body = "Test"};

            var individualArticleResponse = api.HelpCenter.Articles.CreateArticle(_testSectionIdForCommentsTest, article);
            return individualArticleResponse.Article.Id.Value;
        }

        private long CreateTestPost(string testPostForComments)
        {
            var post = new Post {Title = testPostForComments, TopicId = _testTopicIdForCommentsTest};

            var individualArticleResponse = api.HelpCenter.Posts.CreatePost(post);
            return individualArticleResponse.Post.Id.Value;
        }
    }
}