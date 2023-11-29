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
        ArticleAttachment UploadAttachment(long? articleId, ZenFile file, bool inline = false);
        bool DeleteAttachment(long? attachmentId);
#endif
#if ASYNC
        Task<GroupAttachmentResponse> GetAttachmentsAsync(long? articleId);
        Task<ArticleAttachment> UploadAttachmentAsync(long? articleId, ZenFile file, bool inline = false);
        Task<bool> DeleteAttachmentAsync(long? attachmentId);
#endif
    }

    public class ArticleAttachments : Core, IArticleAttachments
    {
        private readonly string _locale;

        private string GeneralAttachmentsPath => string.IsNullOrWhiteSpace(_locale)
            ? "help_center/articles"
            : $"help_center/{_locale}/articles";

        public ArticleAttachments(
            string yourZendeskUrl,
            string user,
            string password,
            string apiToken,
            string locale,
            string p_OAuthToken,
            Dictionary<string, string> customHeaders)
            : base(yourZendeskUrl, user, password, apiToken, p_OAuthToken, customHeaders)
        {
            _locale = locale;
        }

#if SYNC
        public GroupAttachmentResponse GetAttachments(long? articleId)
        {
            if (!articleId.HasValue)
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            return GenericGet<GroupAttachmentResponse>($"{GeneralAttachmentsPath}/{articleId}/attachments.json");
        }

        public ArticleAttachment UploadAttachment(long? articleId, ZenFile file, bool inline = false)
        {
            if (!articleId.HasValue)
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            var form = new Dictionary<string, object> { { "inline", inline }, { "file", file } };

            return GenericPostForm<ArticleAttachment>($"help_center/articles/{articleId}/attachments.json", formParameters: form);

        }

        public bool DeleteAttachment(long? attachmentId)
        {
            return GenericDelete($"help_center/articles/attachments/{attachmentId}.json");
        }
#endif
#if ASYNC
        public Task<GroupAttachmentResponse> GetAttachmentsAsync(long? articleId)
        {
            if (!articleId.HasValue)
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            return GenericGetAsync<GroupAttachmentResponse>($"{GeneralAttachmentsPath}/{articleId}/attachments.json");
        }

        public Task<ArticleAttachment> UploadAttachmentAsync(long? articleId, ZenFile file, bool inline = false)
        {
            if (!articleId.HasValue)
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            var form = new Dictionary<string, object> { { "inline", inline }, { "file", file } };

            return GenericPostFormAsync<ArticleAttachment>($"help_center/articles/{articleId}/attachments.json", formParameters: form);

        }

        public Task<bool> DeleteAttachmentAsync(long? attachmentId)
        {
            return GenericDeleteAsync($"help_center/articles/attachments/{attachmentId}.json");
        }

#endif
    }
}
