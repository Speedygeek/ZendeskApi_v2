using System;

namespace ZendeskApi_v2.Requests.HelpCenter
{
    public enum ArticleSortOrderEnum
    {
        Asc = 1,
        Desc = 2
    }

    [Flags]
    public enum ArticleSortEnum
    {
        Default = 1,
        Position = 2,
        Title = 4,
        Created_At = 8,
        Updated_At = 16
    }

    public class ArticleSortingOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleSortingOptions" /> class.
        /// </summary>
        /// <param name="locale">The locale, REQUIRED to sort</param>
        /// <param name="sortBy">The sort.</param>
        /// <param name="sortOrder">The order.</param>
        public ArticleSortingOptions(string locale = "en-us", ArticleSortEnum sortBy = ArticleSortEnum.Default, ArticleSortOrderEnum sortOrder = ArticleSortOrderEnum.Asc)
        {
            Locale = locale;
            SortBy = sortBy;
            SortOrder = sortOrder;
        }

        public string Locale { get; set; }
        public ArticleSortEnum SortBy { get; set; }
        public ArticleSortOrderEnum SortOrder { get; set; }

        public string GetSortingString(string resourceUrl, string urlPrefix)
        {
            //If sorting is enabled, modify the Uri with the Locale
            resourceUrl = resourceUrl.Replace(urlPrefix, $"help_center/{Locale}/");

            return $"{resourceUrl}?sort_by={SortBy.ToString().ToLower()}&sort_order={SortOrder.ToString().ToLower()}";
        }
    }
}