using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if ASYNC
using System.Threading.Tasks;
#endif
using ZendeskApi_v2.Models.HelpCenter.Attachments;
using ZendeskApi_v2.Models.Shared;

namespace ZendeskApi_v2.Requests.HelpCenter
{
    public interface IArticleAttachments : ICore
    {
#if SYNC
        GroupAttachmentResponse GetAttachments(long? articleId);
        ArticleAttachment UploadAttchment(long? articleId, ZenFile file, bool inline = false);
        bool DeleteAttchment(long? attchmentId);
#endif
#if ASYNC
        Task<GroupAttachmentResponse> GetAttachmentsAsync(long? articleId);
        Task<ArticleAttachment> UploadAttchmentAsync(long? articleId, ZenFile file, bool inline = false);
        Task<bool> DeleteAttchmentAsync(long? attchmentId);
#endif
    }

    public class ArticleAttachments : Core, IArticleAttachments
    {
        public ArticleAttachments(string yourZendeskUrl, string user, string password, string apiToken, string p_OAuthToken)
            : base(yourZendeskUrl, user, password, apiToken, p_OAuthToken)
        {
        }

#if SYNC
        public GroupAttachmentResponse GetAttachments(long? articleId)
        {
            if (!articleId.HasValue)
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            return GenericGet<GroupAttachmentResponse>($"help_center/articles/{articleId}/attachments.json");
        }

        public ArticleAttachment UploadAttchment(long? articleId, ZenFile file, bool inline = false)
        {
            if (!articleId.HasValue)
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            var form = new Dictionary<string, object> { { "inline", inline }, { "file", file } };

            return GenericPostForm<ArticleAttachment>($"help_center/articles/{articleId}/attachments.json", formParameters: form);

        }

        public bool DeleteAttchment(long? attchmentId)
        {
            return GenericDelete($"help_center/articles/attachments/{attchmentId}.json");
        }
#endif
#if ASYNC
        public Task<GroupAttachmentResponse> GetAttachmentsAsync(long? articleId)
        {
            if (!articleId.HasValue)
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            return GenericGetAsync<GroupAttachmentResponse>($"help_center/articles/{articleId}/attachments.json");
        }

        public Task<ArticleAttachment> UploadAttchmentAsync(long? articleId, ZenFile file, bool inline = false)
        {
            if (!articleId.HasValue)
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            var form = new Dictionary<string, object> { { "inline", inline }, { "file", file } };

            return GenericPostFormAsync<ArticleAttachment>($"help_center/articles/{articleId}/attachments.json", formParameters: form);

        }

        public Task<bool> DeleteAttchmentAsync(long? attchmentId)
        {
            return GenericDeleteAsync($"help_center/articles/attachments/{attchmentId}.json");
        }

#endif
    }
}
