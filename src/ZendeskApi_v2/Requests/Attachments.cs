using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
#if ASYNC
using System.Threading.Tasks;
#endif
using ZendeskApi_v2.Extensions;
using ZendeskApi_v2.Models.HelpCenter.Attachments;
using ZendeskApi_v2.Models.Requests;
using ZendeskApi_v2.Models.Shared;

namespace ZendeskApi_v2.Requests
{
    public interface IAttachments : ICore
    {
#if SYNC
        GroupAttachmentResponse GetAttachmentsFromArticle(long? articleId);
        Upload UploadAttachment(ZenFile file, int? timeout = null);
        Upload UploadAttachments(IEnumerable<ZenFile> files, int? timeout = null);
        bool DeleteUpload(Upload upload);
        IndividualAttachmentResponse RedactCommentAttachment(long attachmentId, long ticketId, long commentId);
#endif

#if ASYNC
        /// <summary>
        /// Uploads a file to zendesk and returns the corresponding token id.
        /// To upload another file to an existing token just pass in the existing token.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="token"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>  
        Task<Upload> UploadAttachmentAsync(ZenFile file, string token = "", int? timeout = null);
        Task<Upload> UploadAttachmentsAsync(IEnumerable<ZenFile> files, int? timeout = null);
        Task<bool> DeleteUploadAsync(Upload upload);
        Task<ZenFile> DownloadAttachmentAsync(Attachment attachment);
        Task<IndividualAttachmentResponse> RedactCommentAttachmentAsync(long attachmentId, long ticketId, long commentId);
#endif
    }

    public class Attachments : Core, IAttachments
    {
        public Attachments(string yourZendeskUrl, string user, string password, string apiToken, string p_OAuthToken)
            : base(yourZendeskUrl, user, password, apiToken, p_OAuthToken)
        { }
#if SYNC

        public GroupAttachmentResponse GetAttachmentsFromArticle(long? articleId)
        {
            return GenericGet<GroupAttachmentResponse>($"help_center/articles/{articleId}/attachments.json");
        }

        public Upload UploadAttachment(ZenFile file, int? timeout = null)
        {
            return UploadAttachment(file, "", timeout);
        }

        public Upload UploadAttachments(IEnumerable<ZenFile> files, int? timeout = null)
        {
            var zenFiles = files as IList<ZenFile> ?? files.ToList();
            if (!zenFiles.Any())
            {
                return null;
            }

            var res = UploadAttachment(zenFiles.First(), timeout);

            if (zenFiles.Count > 1)
            {
                var otherFiles = zenFiles.Skip(1);
                foreach (var curFile in otherFiles)
                {
                    res = UploadAttachment(curFile, res.Token, timeout);
                }
            }

            return res;
        }

        /// <summary>
        /// Uploads a file to zendesk and returns the corresponding token id.
        /// To upload another file to an existing token just pass in the existing token.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="token"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>       
        Upload UploadAttachment(ZenFile file, string token, int? timeout = null)
        {
            var resource = $"uploads.json?filename={file.FileName}";
            if (!token.IsNullOrWhiteSpace())
            {
                resource += $"&token={token}";
            }
            var requestResult = RunRequest<UploadResult>(resource, RequestMethod.Post, file, timeout);
            return requestResult.Upload;
        }

        public bool DeleteUpload(Upload upload)
        {
            return (upload?.Token) != null && GenericDelete($"/uploads/{upload.Token}.json");
        }

        public IndividualAttachmentResponse RedactCommentAttachment(long attachmentId, long ticketId, long commentId)
        {
            var resource = $"/tickets/{ticketId}/comments/{commentId}/attachments/{attachmentId}/redact";
            return RunRequest<IndividualAttachmentResponse>(resource, RequestMethod.Put);
        }
#endif

#if ASYNC
        public async Task<Upload> UploadAttachmentAsync(ZenFile file, int? timeout = null)
        {
            return await UploadAttachmentAsync(file, "", timeout);
        }

        public async Task<Upload> UploadAttachmentsAsync(IEnumerable<ZenFile> files, int? timeout = null)
        {
            var zenFiles = files as IList<ZenFile> ?? files.ToList();
            if (!zenFiles.Any())
            {
                return null;
            }

            var res = UploadAttachmentAsync(zenFiles.First(), timeout);

            if (zenFiles.Count > 1)
            {
                var otherFiles = zenFiles.Skip(1);
                foreach (var curFile in otherFiles)
                {
                    var file = curFile;
                    res = await res.ContinueWith(x => UploadAttachmentAsync(file, x.Result.Token, timeout));
                }
            }

            return await res;
        }

        /// <summary>
        /// Uploads a file to zendesk and returns the corresponding token id.
        /// To upload another file to an existing token just pass in the existing token.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="token"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>  
        public async Task<Upload> UploadAttachmentAsync(ZenFile file, string token = "", int? timeout = null)
        {
            var resource = $"uploads.json?filename={file.FileName}";

            if (!token.IsNullOrWhiteSpace())
            {
                resource += $"&token={token}";
            }

            var result = await RunRequestAsync<UploadResult>(resource, RequestMethod.Post, file, timeout);

            return result.Upload;
        }

        public async Task<bool> DeleteUploadAsync(Upload upload)
        {
            return (upload?.Token) != null && await GenericDeleteAsync($"/uploads/{upload.Token}.json");
        }

        public async Task<ZenFile> DownloadAttachmentAsync(Attachment attachment)
        {
            var file = new ZenFile { FileName = attachment.FileName, ContentType = attachment.ContentType };

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", GetPasswordOrTokenAuthHeader());
                file.FileData = await client.GetByteArrayAsync(attachment.ContentUrl);
            }

            return file;
        }

        public async Task<IndividualAttachmentResponse> RedactCommentAttachmentAsync(long attachmentId, long ticketId, long commentId)
        {
            var resource = $"/tickets/{ticketId}/comments/{commentId}/attachments/{attachmentId}/redact";
            return await RunRequestAsync<IndividualAttachmentResponse>(resource, RequestMethod.Put);
        }

#endif

    }
}
