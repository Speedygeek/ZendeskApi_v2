using System.Collections.Generic;
using System.Linq;

#if ASYNC

using System.Threading.Tasks;

#endif

using ZendeskApi_v2.Extensions;
using ZendeskApi_v2.Models.HelpCenter.Attachments;
using ZendeskApi_v2.Models.Shared;

namespace ZendeskApi_v2.Requests
{
    public interface IAttachments : ICore
    {
#if SYNC

        GroupAttachmentResponse GetAttachmentsFromArticle(long? articleId);

        Upload UploadAttachment(ZenFile file, int? timeout = null);

        Upload UploadAttachments(IEnumerable<ZenFile> files, int? timeout = null);

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

#endif
    }

    public class Attachments : Core, IAttachments
    {
        public Attachments(string yourZendeskUrl, string user, string password, string apiToken, string p_OAuthToken)
            : base(yourZendeskUrl, user, password, apiToken, p_OAuthToken)
        {
        }

#if SYNC

        public GroupAttachmentResponse GetAttachmentsFromArticle(long? articleId)
        {
            return GenericGet<GroupAttachmentResponse>(string.Format("help_center/articles/{0}/attachments.json", articleId));
        }

        public Upload UploadAttachment(ZenFile file, int? timeout = null)
        {
            return UploadAttachment(file, string.Empty, timeout);
        }

        public Upload UploadAttachments(IEnumerable<ZenFile> files, int? timeout = null)
        {
            var zenFiles = files as IList<ZenFile> ?? files.ToList();
            if (!zenFiles.Any())
            {
                return null;
            }

            var res = UploadAttachment(zenFiles.First(), timeout);

            if (zenFiles.Count() > 1)
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
        private Upload UploadAttachment(ZenFile file, string token, int? timeout = null)
        {
            var resource = string.Format("uploads.json?filename={0}", file.FileName);
            if (!token.IsNullOrWhiteSpace())
            {
                resource += string.Format("&token={0}", token);
            }
            var requestResult = RunRequest<UploadResult>(resource, RequestMethod.Post, file, timeout);
            return requestResult.Upload;
        }

#endif

#if ASYNC

        public async Task<Upload> UploadAttachmentAsync(ZenFile file, int? timeout = null)
        {
            return await UploadAttachmentAsync(file, string.Empty, timeout);
        }

        public async Task<Upload> UploadAttachmentsAsync(IEnumerable<ZenFile> files, int? timeout = null)
        {
            var zenFiles = files as IList<ZenFile> ?? files.ToList();
            if (!zenFiles.Any())
            {
                return null;
            }

            var res = UploadAttachmentAsync(zenFiles.First(), timeout);

            if (zenFiles.Count() > 1)
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
            string resource = string.Format("uploads.json?filename={0}", file.FileName);

            if (!token.IsNullOrWhiteSpace())
            {
                resource += string.Format("&token={0}", token);
            }

            UploadResult result = await RunRequestAsync<UploadResult>(resource, RequestMethod.Post, file, timeout);

            return result.Upload;
        }

#endif
    }
}