using NUnit.Framework;
using ZendeskApi_v2;
using ZendeskApi_v2.Models.Sections;
using ZendeskApi_v2.Requests.HelpCenter;

namespace Tests.HelpCenter
{
	[TestFixture]
	[Category( "HelpCenter" )]
	class SectionTests
	{
		private ZendeskApi api = new ZendeskApi( Settings.Site, Settings.Email, Settings.Password );

		[Test]

		public void CanGetSections()
		{
			var res = api.HelpCenter.Sections.GetSections();
			Assert.Greater( res.Count, 0 );

			var res1 = api.HelpCenter.Sections.GetSectionById( res.Sections[ 0 ].Id.Value );
			Assert.AreEqual( res1.Section.Id, res.Sections[ 0 ].Id.Value );
		}

		[Test]
		public void CanCreateUpdateAndDeleteSections()
		{
			var res = api.HelpCenter.Sections.CreateSection( new Section()
			{
				Name = "My Test section"
			} );
			Assert.Greater( res.Section.Id, 0 );

			res.Section.Description = "updated description";
			var update = api.HelpCenter.Sections.UpdateSection( res.Section );
			Assert.AreEqual( update.Section.Description, res.Section.Description );

			Assert.True( api.HelpCenter.Sections.DeleteSection( res.Section.Id.Value ) );
		}
	}
}
