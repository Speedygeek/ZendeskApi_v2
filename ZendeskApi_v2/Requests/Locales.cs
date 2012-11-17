using ZenDeskApi_v2.Models.Locales;

namespace ZenDeskApi_v2.Requests
{
    public class Locales : Core
    {

        public Locales(string yourZenDeskUrl, string user, string password)
            : base(yourZenDeskUrl, user, password)
        {
        }
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
    }
}