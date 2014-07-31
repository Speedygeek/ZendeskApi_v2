using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZendeskApi_v2.Models.Sections;


namespace ZendeskApi_v2.Requests
{
	public class Sections : Core
	{
		public Sections(string zendeskApiUrl, string user, string password, string apiToken) : base(zendeskApiUrl, user, password, apiToken)
		{
		}

#if SYNC

        public GroupSectionResponse GetSections()
        {
			return GenericGet<GroupSectionResponse>("help_center/sections.json");
        }
		public GroupSectionResponse GetSectionsByCategoryId(long categoryId)
		{
			return GenericGet<GroupSectionResponse>(string.Format("help_center/categories/{0}/sections.json", categoryId));
		}

        public IndividualSectionResponse GetSectionById(long id)
        {
			return GenericGet<IndividualSectionResponse>(string.Format("help_center/sections/{0}.json", id));
        }

        public IndividualSectionResponse CreateSection(Section section)
        {
			var body = new { section };
			return GenericPost<IndividualSectionResponse>(string.Format("help_center/sections.json"), body);
        }

        public IndividualSectionResponse UpdateSection(Section section)
        {
			var body = new { section };
			return GenericPut<IndividualSectionResponse>(string.Format("help_center/sections/{0}.json", section.Id), body);
        }

        public bool DeleteSection(long id)
        {
			return GenericDelete(string.Format("help_center/sections/{0}.json", id));
        }

#endif
	}
}
