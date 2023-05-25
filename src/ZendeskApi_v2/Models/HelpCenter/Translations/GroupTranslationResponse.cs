using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZendeskApi_v2.Models.HelpCenter.Translations
{
	public class GroupTranslationResponse : GroupResponseBase
	{

		[JsonProperty( "translations" )]
		public IList<Translations.Translation> Translations { get; set; }
	}
}
