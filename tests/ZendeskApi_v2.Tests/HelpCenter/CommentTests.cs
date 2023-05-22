using NUnit.Framework;
using System.Threading.Tasks;
using ZendeskApi_v2.Models.Articles;
using ZendeskApi_v2.Models.HelpCenter.Comments;
using ZendeskApi_v2.Models.HelpCenter.Post;
using ZendeskApi_v2.Tests.Base;

namespace ZendeskApi_v2.Tests.HelpCenter;

[TestFixture]
[Category("HelpCenter_Comment")]
public class CommentTests : TestBase
{
    private readonly long _testSectionIdForCommentsTest = 360000205286; //https://csharpapi.zendesk.com/hc/en-us/sections/360000205286-Test-Section-For-Comment-Tests
    private readonly long _testTopicIdForCommentsTest = Settings.Topic_ID; //https://csharpapi.zendesk.com/hc/en-us/community/topics/360000016546-Test-Topic-For-Comment-Tests

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
        var articlesBySectionId = Api.HelpCenter.Articles.GetArticlesBySectionId(_testSectionIdForCommentsTest);
        foreach (var article in articlesBySectionId.Articles)
        {
            Api.HelpCenter.Articles.DeleteArticle(article.Id.Value);
        }
    }

    private void DeleteAllPostsFromTestTopic()
    {
        var articlesBySectionId = Api.HelpCenter.Posts.GetPostsByTopicId(_testTopicIdForCommentsTest);
        foreach (var post in articlesBySectionId.Posts)
        {
            if (post.Title == "Help me!")
            {
                Api.HelpCenter.Posts.DeletePost(post.Id.Value);
            }
        }
    }

    [Test]
    public void CanCreateUpdateAndDeleteCommentsForArticle()
    {
        var articleId = CreateTestArticle("Test-Article-For-Comments");

        //Create 3 Comments
        var individualCommentsResponse1 = Api.HelpCenter.Comments.CreateCommentForArticle(articleId, new Comment { Body = "Comment 1", Locale = "en-us" });
        Assert.That(individualCommentsResponse1.Comment, Is.Not.Null);
        Assert.That(individualCommentsResponse1.Comment.Id, Is.GreaterThan(0));

        var individualCommentsResponse2 = Api.HelpCenter.Comments.CreateCommentForArticle(articleId, new Comment { Body = "Comment 2", Locale = "en-us" });
        Assert.That(individualCommentsResponse2.Comment, Is.Not.Null);
        Assert.That(individualCommentsResponse2.Comment.Id, Is.GreaterThan(0));

        var individualCommentsResponse3 = Api.HelpCenter.Comments.CreateCommentForArticle(articleId, new Comment { Body = "Comment 3", Locale = "en-us" });
        Assert.That(individualCommentsResponse3.Comment, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(individualCommentsResponse3.Comment.Id, Is.GreaterThan(0));

            Assert.That(Api.HelpCenter.Comments.GetCommentsForArticle(articleId).Count, Is.EqualTo(3)); //That article have 3 comments

            Assert.That(individualCommentsResponse1.Comment.Body, Is.EqualTo("Comment 1"));
            Assert.That(individualCommentsResponse2.Comment.Body, Is.EqualTo("Comment 2"));
            Assert.That(individualCommentsResponse3.Comment.Body, Is.EqualTo("Comment 3"));
        });

        //Update Comment
        var updatedCommentBody = "Comment 2 Updated";
        individualCommentsResponse2.Comment.Body = updatedCommentBody;
        var updateCommentForArticle = Api.HelpCenter.Comments.UpdateCommentForArticle(articleId, individualCommentsResponse2.Comment);
        Assert.That(updateCommentForArticle.Comment.Body, Is.EqualTo($"<p>{updatedCommentBody}</p>"));

        //Delete Comment
        Api.HelpCenter.Comments.DeleteCommentForArticle(articleId, individualCommentsResponse2.Comment.Id.Value);
        Assert.That(Api.HelpCenter.Comments.GetCommentsForArticle(articleId).Count, Is.EqualTo(2)); //One less comments now
    }

    [Test]
    public void CanCreateUpdateAndDeleteCommentsForPost()
    {
        var postId = CreateTestPost("Test-Post-For-Comments");

        //Create 3 Comments
        var individualCommentsResponse1 = Api.HelpCenter.Comments.CreateCommentForPost(postId, new Comment { Body = "Comment 1" });
        Assert.That(individualCommentsResponse1.Comment, Is.Not.Null);
        Assert.That(individualCommentsResponse1.Comment.Id, Is.GreaterThan(0));

        var individualCommentsResponse2 = Api.HelpCenter.Comments.CreateCommentForPost(postId, new Comment { Body = "Comment 2" });
        Assert.That(individualCommentsResponse2.Comment, Is.Not.Null);
        Assert.That(individualCommentsResponse2.Comment.Id, Is.GreaterThan(0));

        var individualCommentsResponse3 = Api.HelpCenter.Comments.CreateCommentForPost(postId, new Comment { Body = "Comment 3" });
        Assert.That(individualCommentsResponse3.Comment, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(individualCommentsResponse3.Comment.Id, Is.GreaterThan(0));

            Assert.That(Api.HelpCenter.Comments.GetCommentsForPost(postId).Count, Is.EqualTo(3)); //That post have 3 comments

            Assert.That(individualCommentsResponse1.Comment.Body, Is.EqualTo("Comment 1"));
            Assert.That(individualCommentsResponse2.Comment.Body, Is.EqualTo("Comment 2"));
            Assert.That(individualCommentsResponse3.Comment.Body, Is.EqualTo("Comment 3"));
        });

        //Update Comment
        var updatedCommentBody = "Comment 2 Updated";
        individualCommentsResponse2.Comment.Body = updatedCommentBody;
        var updateCommentForPost = Api.HelpCenter.Comments.UpdateCommentForPost(postId, individualCommentsResponse2.Comment);
        Assert.That(updateCommentForPost.Comment.Body, Is.EqualTo(updatedCommentBody));

        //Delete Comment
        Api.HelpCenter.Comments.DeleteCommentForPost(postId, individualCommentsResponse2.Comment.Id.Value);
        Assert.That(Api.HelpCenter.Comments.GetCommentsForPost(postId).Count, Is.EqualTo(2)); //One less comments now
    }

    [Test]
    public async Task CanCreateUpdateAndDeleteCommentsForArticleAsync()
    {
        var articleId = CreateTestArticle("Test-Article-For-Comments-Async");

        //Create 3 Comments
        var individualCommentsResponse1 = await Api.HelpCenter.Comments.CreateCommentForArticleAsync(articleId, new Comment { Body = "Comment 1", Locale = "en-us" });
        Assert.That(individualCommentsResponse1.Comment, Is.Not.Null);
        Assert.That(individualCommentsResponse1.Comment.Id, Is.GreaterThan(0));

        var individualCommentsResponse2 = await Api.HelpCenter.Comments.CreateCommentForArticleAsync(articleId, new Comment { Body = "Comment 2", Locale = "en-us" });
        Assert.That(individualCommentsResponse2.Comment, Is.Not.Null);
        Assert.That(individualCommentsResponse2.Comment.Id, Is.GreaterThan(0));

        var individualCommentsResponse3 = await Api.HelpCenter.Comments.CreateCommentForArticleAsync(articleId, new Comment { Body = "Comment 3", Locale = "en-us" });
        Assert.That(individualCommentsResponse3.Comment, Is.Not.Null);
        Assert.Multiple(async () =>
        {
            Assert.That(individualCommentsResponse3.Comment.Id, Is.GreaterThan(0));

            Assert.That((await Api.HelpCenter.Comments.GetCommentsForArticleAsync(articleId)).Count, Is.EqualTo(3)); //That article have 3 comments

            Assert.That(individualCommentsResponse1.Comment.Body, Is.EqualTo("Comment 1"));
            Assert.That(individualCommentsResponse2.Comment.Body, Is.EqualTo("Comment 2"));
            Assert.That(individualCommentsResponse3.Comment.Body, Is.EqualTo("Comment 3"));
        });

        //Update Comment
        var updatedCommentBody = "Comment 2 Updated";
        individualCommentsResponse2.Comment.Body = updatedCommentBody;
        var updateCommentForArticle = await Api.HelpCenter.Comments.UpdateCommentForArticleAsync(articleId, individualCommentsResponse2.Comment);
        Assert.That(updateCommentForArticle.Comment.Body, Is.EqualTo($"<p>{updatedCommentBody}</p>"));

        //Delete Comment
        await Api.HelpCenter.Comments.DeleteCommentForArticleAsync(articleId, individualCommentsResponse2.Comment.Id.Value);
        Assert.That((await Api.HelpCenter.Comments.GetCommentsForArticleAsync(articleId)).Count, Is.EqualTo(2)); //One less comments now
    }

    [Test]
    public async Task CanCreateUpdateAndDeleteCommentsForPostAsync()
    {
        var postId = CreateTestPost("Test-Post-For-Comments-Async");

        //Create 3 Comments
        var individualCommentsResponse1 = await Api.HelpCenter.Comments.CreateCommentForPostAsync(postId, new Comment { Body = "Comment 1" });
        Assert.That(individualCommentsResponse1.Comment, Is.Not.Null);
        Assert.That(individualCommentsResponse1.Comment.Id, Is.GreaterThan(0));

        var individualCommentsResponse2 = await Api.HelpCenter.Comments.CreateCommentForPostAsync(postId, new Comment { Body = "Comment 2" });
        Assert.That(individualCommentsResponse2.Comment, Is.Not.Null);
        Assert.That(individualCommentsResponse2.Comment.Id, Is.GreaterThan(0));

        var individualCommentsResponse3 = await Api.HelpCenter.Comments.CreateCommentForPostAsync(postId, new Comment { Body = "Comment 3" });
        Assert.That(individualCommentsResponse3.Comment, Is.Not.Null);
        Assert.Multiple(async () =>
        {
            Assert.That(individualCommentsResponse3.Comment.Id, Is.GreaterThan(0));

            Assert.That((await Api.HelpCenter.Comments.GetCommentsForPostAsync(postId)).Count, Is.EqualTo(3)); //That post have 3 comments

            Assert.That(individualCommentsResponse1.Comment.Body, Is.EqualTo("Comment 1"));
            Assert.That(individualCommentsResponse2.Comment.Body, Is.EqualTo("Comment 2"));
            Assert.That(individualCommentsResponse3.Comment.Body, Is.EqualTo("Comment 3"));
        });

        //Update Comment
        var updatedCommentBody = "Comment 2 Updated";
        individualCommentsResponse2.Comment.Body = updatedCommentBody;
        var updateCommentForPost = await Api.HelpCenter.Comments.UpdateCommentForPostAsync(postId, individualCommentsResponse2.Comment);
        Assert.That(updateCommentForPost.Comment.Body, Is.EqualTo(updatedCommentBody));

        //Delete Comment
        await Api.HelpCenter.Comments.DeleteCommentForPostAsync(postId, individualCommentsResponse2.Comment.Id.Value);
        Assert.That((await Api.HelpCenter.Comments.GetCommentsForPostAsync(postId)).Count, Is.EqualTo(2)); //One less comments now
    }

    private long CreateTestArticle(string testArticleForComments)
    {
        var article = new Article { Title = testArticleForComments, Body = "Test" };

        var individualArticleResponse = Api.HelpCenter.Articles.CreateArticle(_testSectionIdForCommentsTest, article);
        return individualArticleResponse.Article.Id.Value;
    }

    private long CreateTestPost(string testPostForComments)
    {
        var post = new Post { Title = testPostForComments, TopicId = _testTopicIdForCommentsTest };

        var individualArticleResponse = Api.HelpCenter.Posts.CreatePost(post);
        return individualArticleResponse.Post.Id.Value;
    }
}