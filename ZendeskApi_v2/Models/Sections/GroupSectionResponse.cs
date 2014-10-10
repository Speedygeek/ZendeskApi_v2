using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZendeskApi_v2.Models.Sections
{

	public class GroupSectionResponse : GroupResponseBase
	{

		[JsonProperty("sections")]
		public IList<Section> Sections { get; set; }
	}
}
