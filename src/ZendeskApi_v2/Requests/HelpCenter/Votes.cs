using System;
using System.Collections.Generic;

#if ASYNC
using System.Threading.Tasks;
#endif
using ZendeskApi_v2.Extensions;
using ZendeskApi_v2.Models.HelpCenter.Votes;

namespace ZendeskApi_v2.Requests.HelpCenter
{
	public interface IVotes : ICore
	{
#if SYNC

		GroupVoteResponse GetVotesForArticle(long? articleId);
#endif
#if ASYNC
		Task<GroupVoteResponse> GetVotesForArticleAsync(long? articleId);
#endif

	}

	public class Votes : Core, IVotes
	{
		public Votes(string yourZendeskUrl, string user, string password, string apiToken, string p_OAuthToken, Dictionary<string,string> customHeaders)
            : base(yourZendeskUrl, user, password, apiToken, p_OAuthToken, customHeaders)
		{
		}

#if SYNC

		public GroupVoteResponse GetVotesForArticle(long? articleId)
		{ 
			return GenericGet<GroupVoteResponse>($"help_center/articles/{articleId}/votes.json");
		}
		
#endif
#if ASYNC

		public async Task<GroupVoteResponse> GetVotesForArticleAsync(long? articleId)
		{
			return await GenericGetAsync<GroupVoteResponse>($"help_center/articles/{articleId}/votes.json");
		}

#endif
	}
}
