using Newtonsoft.Json.Converters;

namespace ZendeskApi_v2.Serialization
{
    class HolidayDateConverter : IsoDateTimeConverter
    {
        public HolidayDateConverter()
        {
            base.DateTimeFormat = "yyyy-MM-dd";
        }
    }
}
