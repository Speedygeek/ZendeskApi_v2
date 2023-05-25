using System;

namespace ZendeskApi_v2.Tests;

public class Settings
{
    public const string Phone = "1234567890";
    public const string FormattedPhone = "(111) 222-3333";
    public const string BadPhone = "0987654321";

    public const string FieldKey = "test";
    public const string FieldValue = "0897c9c1f80646118a8194c942aa84cf";
    public const string BadFieldValue = "BAD";

    public const long SampleTicketId = 990;
    public const long SampleTicketId2 = 1366;
    public const long SampleTicketId3 = 1364;

    public const string ColloboratorEmail = "zendeskcsharpapi454@gmail.com";
    public const long CollaboratorId = 2110053086;

    public const long CustomFieldId = 22117646;
    public const long CustomBoolFieldId = 23812686;
    public const long CustomDropDownId = 360023145351;

    public const long GroupId = 20402842;

    public const string ViewName = "My unsolved tickets";
    public const int ViewId = 31559032;

    public const long TicketFormId = 26227;

    public const long CustomFieldTypeOfFeedbackId = 23999346;

    // Help Center
    public const long Category_ID = 200382245;
    public const long Topic_ID = 360000971672;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2211:Non-constant fields should not be visible", Justification = "<Pending>")]
    public static DateTimeOffset Epoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
}
