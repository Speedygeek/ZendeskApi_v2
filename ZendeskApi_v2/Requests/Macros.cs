#if ASYNC
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
		GroupMacroResponse GetAllMacros();

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
		Task<GroupMacroResponse> GetAllMacrosAsync();

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
        public Macros(string yourZendeskUrl, string user, string password, string apiToken)
            : base(yourZendeskUrl, user, password, apiToken)
        {
        }

#if SYNC
        /// <summary>
        /// Lists all shared and personal macros available to the current user
        /// </summary>
        /// <returns></returns>
        public GroupMacroResponse GetAllMacros()
        {
            return GenericGet<GroupMacroResponse>(string.Format("macros.json"));
        }

        public IndividualMacroResponse GetMacroById(long id)
        {
            return GenericGet<IndividualMacroResponse>(string.Format("macros/{0}.json", id));
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
            return GenericPut<IndividualMacroResponse>(string.Format("macros/{0}.json", macro.Id), body);
        }

        public bool DeleteMacro(long id)
        {
            return GenericDelete(string.Format("macros/{0}.json", id));
        }

        /// <summary>
        /// Applies a macro to all applicable tickets.
        /// </summary>
        /// <param name="macroId"></param>
        /// <returns></returns>
        public ApplyMacroResponse ApplyMacro(long macroId)
        {
            return GenericGet<ApplyMacroResponse>(string.Format("macros/{0}/apply.json", macroId));
        }

        /// <summary>
        /// Applies a macro to a specific ticket
        /// </summary>
        /// <param name="ticketId"></param>
        /// <param name="macroId"></param>
        /// <returns></returns>
        public ApplyMacroResponse ApplyMacroToTicket(long ticketId, long macroId)
        {
            return GenericGet<ApplyMacroResponse>(string.Format("tickets/{0}/macros/{1}/apply.json", ticketId, macroId));
        }
#endif

#if ASYNC
        /// <summary>
        /// Lists all shared and personal macros available to the current user
        /// </summary>
        /// <returns></returns>
        public async Task<GroupMacroResponse> GetAllMacrosAsync()
        {
            return await GenericGetAsync<GroupMacroResponse>(string.Format("macros.json"));
        }

        public async Task<IndividualMacroResponse> GetMacroByIdAsync(long id)
        {
            return await GenericGetAsync<IndividualMacroResponse>(string.Format("macros/{0}.json", id));
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
            return await GenericPutAsync<IndividualMacroResponse>(string.Format("macros/{0}.json", macro.Id), body);
        }

        public async Task<bool> DeleteMacroAsync(long id)
        {
            return await GenericDeleteAsync(string.Format("macros/{0}.json", id));
        }

        /// <summary>
        /// Applies a macro to all applicable tickets.
        /// </summary>
        /// <param name="macroId"></param>
        /// <returns></returns>
        public async Task<ApplyMacroResponse> ApplyMacroAsync(long macroId)
        {
            return await GenericGetAsync<ApplyMacroResponse>(string.Format("macros/{0}/apply.json", macroId));
        }

        /// <summary>
        /// Applies a macro to a specific ticket
        /// </summary>
        /// <param name="ticketId"></param>
        /// <param name="macroId"></param>
        /// <returns></returns>
        public async Task<ApplyMacroResponse> ApplyMacroToTicketAsync(long ticketId, long macroId)
        {
            return await GenericGetAsync<ApplyMacroResponse>(string.Format("tickets/{0}/macros/{1}/apply.json", ticketId, macroId));
        }                        
#endif
    }
}