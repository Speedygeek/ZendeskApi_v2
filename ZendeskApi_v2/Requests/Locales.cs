#if ASYNC
using System.Threading.Tasks;
#endif
using ZendeskApi_v2.Models.Locales;

namespace ZendeskApi_v2.Requests
{
	public interface ILocales : ICore
	{
#if SYNC
		/// <summary>
		/// This lists the translation locales that are available for the account.
		/// </summary>
		/// <returns></returns>
		GroupLocaleResponse GetAllLocales();

		/// <summary>
		/// This lists the translation locales that have been localized for agents.
		/// </summary>
		/// <returns></returns>
		GroupLocaleResponse GetLocalesForAgents();

		/// <summary>
		/// This lists the translation locales that have been localized for agents.
		/// </summary>
		/// <returns></returns>
		IndividualLocaleResponse GetLocaleById(long id);

		/// <summary>
		/// This works exactly like show, but instead of taking an id as argument, it renders the locale of the user performing the request.
		/// </summary>
		/// <returns></returns>
		IndividualLocaleResponse GetCurrentLocale();
#endif

#if ASYNC
		/// <summary>
		/// This lists the translation locales that are available for the account.
		/// </summary>
		/// <returns></returns>
		Task<GroupLocaleResponse> GetAllLocalesAsync();

		/// <summary>
		/// This lists the translation locales that have been localized for agents.
		/// </summary>
		/// <returns></returns>
		Task<GroupLocaleResponse> GetLocalesForAgentsAsync();

		/// <summary>
		/// This lists the translation locales that have been localized for agents.
		/// </summary>
		/// <returns></returns>
		Task<IndividualLocaleResponse> GetLocaleByIdAsync(long id);

		/// <summary>
		/// This works exactly like show, but instead of taking an id as argument, it renders the locale of the user performing the request.
		/// </summary>
		/// <returns></returns>
		Task<IndividualLocaleResponse> GetCurrentLocaleAsync();
#endif
	}

	public class Locales : Core, ILocales
	{

        public Locales(string yourZendeskUrl, string user, string password, string apiToken)
            : base(yourZendeskUrl, user, password, apiToken)
        {
        }

#if SYNC
        /// <summary>
        /// This lists the translation locales that are available for the account.
        /// </summary>
        /// <returns></returns>
        public GroupLocaleResponse GetAllLocales()
        {
            return GenericGet<GroupLocaleResponse>(string.Format("locales.json"));
        }

        /// <summary>
        /// This lists the translation locales that have been localized for agents.
        /// </summary>
        /// <returns></returns>
        public GroupLocaleResponse GetLocalesForAgents()
        {
            return GenericGet<GroupLocaleResponse>(string.Format("locales/agent.json"));
        }

        /// <summary>
        /// This lists the translation locales that have been localized for agents.
        /// </summary>
        /// <returns></returns>
        public IndividualLocaleResponse GetLocaleById(long id)
        {
            return GenericGet<IndividualLocaleResponse>(string.Format("locales/{0}.json", id));
        }

        /// <summary>
        /// This works exactly like show, but instead of taking an id as argument, it renders the locale of the user performing the request.
        /// </summary>
        /// <returns></returns>
        public IndividualLocaleResponse GetCurrentLocale()
        {
            return GenericGet<IndividualLocaleResponse>(string.Format("locales/current.json"));
        }

#endif

#if ASYNC
        /// <summary>
        /// This lists the translation locales that are available for the account.
        /// </summary>
        /// <returns></returns>
        public async Task<GroupLocaleResponse> GetAllLocalesAsync()
        {
            return await GenericGetAsync<GroupLocaleResponse>(string.Format("locales.json"));
        }

        /// <summary>
        /// This lists the translation locales that have been localized for agents.
        /// </summary>
        /// <returns></returns>
        public async Task<GroupLocaleResponse> GetLocalesForAgentsAsync()
        {
            return await GenericGetAsync<GroupLocaleResponse>(string.Format("locales/agent.json"));
        }

        /// <summary>
        /// This lists the translation locales that have been localized for agents.
        /// </summary>
        /// <returns></returns>
        public async Task<IndividualLocaleResponse> GetLocaleByIdAsync(long id)
        {
            return await GenericGetAsync<IndividualLocaleResponse>(string.Format("locales/{0}.json", id));
        }

        /// <summary>
        /// This works exactly like show, but instead of taking an id as argument, it renders the locale of the user performing the request.
        /// </summary>
        /// <returns></returns>
        public async Task<IndividualLocaleResponse> GetCurrentLocaleAsync()
        {
            return await GenericGetAsync<IndividualLocaleResponse>(string.Format("locales/current.json"));
        }
#endif
    }
}