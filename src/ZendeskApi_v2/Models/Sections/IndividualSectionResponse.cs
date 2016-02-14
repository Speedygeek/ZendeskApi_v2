using Newtonsoft.Json;
using ZendeskApi_v2.Models.Categories;

namespace ZendeskApi_v2.Models.Sections
{
	public class IndividualSectionResponse
	{

		[JsonProperty("section")]
		public Section Section { get; set; }
	}
}