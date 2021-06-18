using System;

namespace Tests
{
    public class Settings
    {
        public const string Site = "https://csharpapi.zendesk.com/api/v2/";
        public const string DefaultOrg = "csharpapi";
        public const string DefaultExternalId = "1234abc";

        public const string AdminPassword = "wHth9.pEvn2MLE^=}Y*j.v_x";
        public const string AdminEmail = "csharpzendeskapi1234@gmail.com";

        public const string ApiToken = "sclVCOJERiSCWmTLpkexGXiYeawG2noGxPxnaBuc";
        public const string Phone = "1234567890";
        public const string FormattedPhone = "(111) 222-3333";
        public const string BadPhone = "0987654321";

        public const string AgentEmail = "zendeskcsharpapi454@gmail.com";
        public const string AgentPassword = "Pa55word,";

        public const string FieldKey = "test";
        public const string FieldValue = "0897c9c1f80646118a8194c942aa84cf";
        public const string BadFieldValue = "BAD";

        public const long UserId = 281513402;
        public const long SampleTicketId = 990;
        public const long SampleTicketId2 = 1366;
        public const long SampleTicketId3 = 1364;

        public const long EndUserId = 287004928;

        public const string ColloboratorEmail = "zendeskcsharpapi454@gmail.com";//"eneif123@yahoo.com";
        public const long CollaboratorId = 2110053086; // 282136547;

        public const long CustomFieldId = 22117646;
        public const long CustomBoolFieldId = 23812686;
        public const long CustomDropDownId = 360023145351;

        public const long GroupId = 20402842;
        public const long OrganizationId = 361477412411;

        public const string ViewName = "My unsolved tickets";
        public const int ViewId = 31559032;

        public const long TicketFormId = 26227;

        public const long CustomFieldTypeOfFeedbackId = 23999346;

        // Help Center
        public const long Category_ID = 200382245;
        public const long Topic_ID = 360000971672;

        public static DateTimeOffset Epoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
    }
}
