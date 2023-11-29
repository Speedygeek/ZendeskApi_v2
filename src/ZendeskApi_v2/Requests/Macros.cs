#if ASYNC
using System.Collections.Generic;
using System.Threading.Tasks;
#endif
using ZendeskApi_v2.Models.Macros;

namespace ZendeskApi_v2.Requests
{
	public interface IMacros : ICore
	{
#if SYNC
		/// <summary>
		/// Lists all shared and personal macros available to the current user
		/// </summary>
		/// <returns></returns>
		GroupMacroResponse GetAllMacros(int? perPage = null, int? page = null);

		IndividualMacroResponse GetMacroById(long id);

		/// <summary>
		/// Lists all active shared and personal macros available to the current user
		/// </summary>
		/// <returns></returns>
		GroupMacroResponse GetActiveMacros();

		IndividualMacroResponse CreateMacro(Macro macro);
		IndividualMacroResponse UpdateMacro(Macro macro);
		bool DeleteMacro(long id);

		/// <summary>
		/// Applies a macro to all applicable tickets.
		/// </summary>
		/// <param name="macroId"></param>
		/// <returns></returns>
		ApplyMacroResponse ApplyMacro(long macroId);

		/// <summary>
		/// Applies a macro to a specific ticket
		/// </summary>
		/// <param name="ticketId"></param>
		/// <param name="macroId"></param>
		/// <returns></returns>
		ApplyMacroResponse ApplyMacroToTicket(long ticketId, long macroId);
#endif

#if ASYNC
		/// <summary>
		/// Lists all shared and personal macros available to the current user
		/// </summary>
		/// <returns></returns>
		Task<GroupMacroResponse> GetAllMacrosAsync(int? perPage = null, int? page = null);

		Task<IndividualMacroResponse> GetMacroByIdAsync(long id);

		/// <summary>
		/// Lists all active shared and personal macros available to the current user
		/// </summary>
		/// <returns></returns>
		Task<GroupMacroResponse> GetActiveMacrosAsync();

		Task<IndividualMacroResponse> CreateMacroAsync(Macro macro);
		Task<IndividualMacroResponse> UpdateMacroAsync(Macro macro);
		Task<bool> DeleteMacroAsync(long id);

		/// <summary>
		/// Applies a macro to all applicable tickets.
		/// </summary>
		/// <param name="macroId"></param>
		/// <returns></returns>
		Task<ApplyMacroResponse> ApplyMacroAsync(long macroId);

		/// <summary>
		/// Applies a macro to a specific ticket
		/// </summary>
		/// <param name="ticketId"></param>
		/// <param name="macroId"></param>
		/// <returns></returns>
		Task<ApplyMacroResponse> ApplyMacroToTicketAsync(long ticketId, long macroId);
#endif
	}

	public class Macros : Core, IMacros
	{
        public Macros(string yourZendeskUrl, string user, string password, string apiToken, string p_OAuthToken, Dictionary<string,string> customHeaders)
            : base(yourZendeskUrl, user, password, apiToken, p_OAuthToken, customHeaders)
        {
        }

#if SYNC
        /// <summary>
        /// Lists all shared and personal macros available to the current user
        /// </summary>
        /// <returns></returns>
        public GroupMacroResponse GetAllMacros(int? perPage = null, int? page = null)
        {
            return GenericPagedGet<GroupMacroResponse>(string.Format("macros.json"), perPage, page);
        }

        public IndividualMacroResponse GetMacroById(long id)
        {
            return GenericGet<IndividualMacroResponse>($"macros/{id}.json");
        }

        /// <summary>
        /// Lists all active shared and personal macros available to the current user
        /// </summary>
        /// <returns></returns>
        public GroupMacroResponse GetActiveMacros()
        {
            return GenericGet<GroupMacroResponse>(string.Format("macros/active.json"));
        }

        public IndividualMacroResponse CreateMacro(Macro macro)
        {
            var body = new {macro};
            return GenericPost<IndividualMacroResponse>("macros.json", body);
        }

        public IndividualMacroResponse UpdateMacro(Macro macro)
        {
            var body = new { macro };
            return GenericPut<IndividualMacroResponse>($"macros/{macro.Id}.json", body);
        }

        public bool DeleteMacro(long id)
        {
            return GenericDelete($"macros/{id}.json");
        }

        /// <summary>
        /// Applies a macro to all applicable tickets.
        /// </summary>
        /// <param name="macroId"></param>
        /// <returns></returns>
        public ApplyMacroResponse ApplyMacro(long macroId)
        {
            return GenericGet<ApplyMacroResponse>($"macros/{macroId}/apply.json");
        }

        /// <summary>
        /// Applies a macro to a specific ticket
        /// </summary>
        /// <param name="ticketId"></param>
        /// <param name="macroId"></param>
        /// <returns></returns>
        public ApplyMacroResponse ApplyMacroToTicket(long ticketId, long macroId)
        {
            return GenericGet<ApplyMacroResponse>($"tickets/{ticketId}/macros/{macroId}/apply.json");
        }
#endif

#if ASYNC
        /// <summary>
        /// Lists all shared and personal macros available to the current user
        /// </summary>
        /// <returns></returns>
        public async Task<GroupMacroResponse> GetAllMacrosAsync(int? perPage = null, int? page = null)
        {
            return await GenericPagedGetAsync<GroupMacroResponse>(string.Format("macros.json"), perPage, page);
        }

        public async Task<IndividualMacroResponse> GetMacroByIdAsync(long id)
        {
            return await GenericGetAsync<IndividualMacroResponse>($"macros/{id}.json");
        }

        /// <summary>
        /// Lists all active shared and personal macros available to the current user
        /// </summary>
        /// <returns></returns>
        public async Task<GroupMacroResponse> GetActiveMacrosAsync()
        {
            return await GenericGetAsync<GroupMacroResponse>(string.Format("macros/active.json"));
        }

        public async Task<IndividualMacroResponse> CreateMacroAsync(Macro macro)
        {
            var body = new { macro };
            return await GenericPostAsync<IndividualMacroResponse>("macros.json", body);
        }

        public async Task<IndividualMacroResponse> UpdateMacroAsync(Macro macro)
        {
            var body = new { macro };
            return await GenericPutAsync<IndividualMacroResponse>($"macros/{macro.Id}.json", body);
        }

        public async Task<bool> DeleteMacroAsync(long id)
        {
            return await GenericDeleteAsync($"macros/{id}.json");
        }

        /// <summary>
        /// Applies a macro to all applicable tickets.
        /// </summary>
        /// <param name="macroId"></param>
        /// <returns></returns>
        public async Task<ApplyMacroResponse> ApplyMacroAsync(long macroId)
        {
            return await GenericGetAsync<ApplyMacroResponse>($"macros/{macroId}/apply.json");
        }

        /// <summary>
        /// Applies a macro to a specific ticket
        /// </summary>
        /// <param name="ticketId"></param>
        /// <param name="macroId"></param>
        /// <returns></returns>
        public async Task<ApplyMacroResponse> ApplyMacroToTicketAsync(long ticketId, long macroId)
        {
            return await GenericGetAsync<ApplyMacroResponse>($"tickets/{ticketId}/macros/{macroId}/apply.json");
        }                        
#endif
    }
}
