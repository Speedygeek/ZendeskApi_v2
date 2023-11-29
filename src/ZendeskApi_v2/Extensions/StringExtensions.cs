using ZendeskApi_v2.Models.Shared;

namespace ZendeskApi_v2.Extensions
{
    internal static class StringExtensions
    {
        internal static bool IsNullOrWhiteSpace(this string value)
        {

            // this is needed because .net 3.5 and older don't have 
            // string.IsNullOrWhiteSpace
            if (value == null)
            {
                return true;
            }

            for (var i = 0; i < value.Length; i++)
            {
                if (!char.IsWhiteSpace(value[i]))
                {
                    return false;
                }
            }

            return true;
        }

        internal static bool IsNotNullOrWhiteSpace(this string value) => !IsNullOrWhiteSpace(value);
    }
}
