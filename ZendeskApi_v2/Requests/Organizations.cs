using ZendeskApi_v2.Models.Organizations;

namespace ZendeskApi_v2.Requests
{
    public class Organizations : Core
    {
        public Organizations(string yourZendeskUrl, string user, string password)
            : base(yourZendeskUrl, user, password)
        {
        }

        public GroupOrganizationResponse GetOrganizations()
        {
            return GenericGet<GroupOrganizationResponse>("organizations.json");
        }

        /// <summary>
        /// Returns an array of organizations whose name starts with the value specified in the name parameter. The name must be at least 2 characters in length.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public GroupOrganizationResponse GetOrganizationsStartingWith(string name)
        {
            return GenericPost<GroupOrganizationResponse>(string.Format("organizations/autocomplete.json?name={0}", name));
        }

        public GroupOrganizationResponse SearchForOrganizations(string searchTerm)
        {
            return GenericPost<GroupOrganizationResponse>(string.Format("organizations/autocomplete.json?external_id={0}", searchTerm));
        }

        public IndividualOrganizationResponse GetOrganization(long id)
        {
            return GenericGet<IndividualOrganizationResponse>(string.Format("organizations/{0}.json", id));
        }

        public IndividualOrganizationResponse CreateOrganization(Organization organization)
        {
            var body = new {organization};
            return GenericPost<IndividualOrganizationResponse>("organizations.json", body);
        }

        public IndividualOrganizationResponse UpdateOrganization(Organization organization)
        {
            var body = new { organization };
            return GenericPut<IndividualOrganizationResponse>(string.Format("organizations/{0}.json", organization.Id), body);
        }

        public bool DeleteOrganization(long id)
        {            
            return GenericDelete(string.Format("organizations/{0}.json", id));
        }
    }
}