using ZenDeskApi_v2.Models.Macros;

namespace ZenDeskApi_v2.Requests
{
    public class Macros : Core
    {
        public Macros(string yourZenDeskUrl, string user, string password)
            : base(yourZenDeskUrl, user, password)
        {
        }

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
    }
}