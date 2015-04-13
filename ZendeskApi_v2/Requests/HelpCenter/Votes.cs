using System;
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
		public Votes(string zendeskApiUrl, string user, string password, string apiToken)
			: base(zendeskApiUrl, user, password, apiToken)
		{
		}

#if SYNC

		public GroupVoteResponse GetVotesForArticle(long? articleId)
		{ 
			return GenericGet<GroupVoteResponse>(string.Format("help_center/articles/{0}/votes.json", articleId));
		}
		
#endif
#if ASYNC

		public async Task<GroupVoteResponse> GetVotesForArticleAsync(long? articleId)
		{
			return await GenericGetAsync<GroupVoteResponse>(string.Format("help_center/articles/{0}/votes.json", articleId));
		}

#endif
	}
}
