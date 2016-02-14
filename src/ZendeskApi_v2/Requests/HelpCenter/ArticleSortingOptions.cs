using System;

namespace ZendeskApi_v2.Requests.HelpCenter {
    public enum ArticleSortOrderEnum{
        Asc = 1,
        Desc = 2
    }

    public enum ArticleSortEnum {
        Default = 1,
        Position = 2,
        Title = 4,
        Created_At = 8,
        Updated_At = 16
    }

    public class ArticleSortingOptions {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleSortingOptions" /> class.
        /// </summary>
        /// <param name="locale">The locale, REQUIRED to sort</param>
        /// <param name="sortBy">The sort.</param>
        /// <param name="sortOrder">The order.</param>
        public ArticleSortingOptions(string locale = "en-us", ArticleSortEnum sortBy = ArticleSortEnum.Default, ArticleSortOrderEnum sortOrder = ArticleSortOrderEnum.Asc) {
            this.Locale = locale;
            this.SortBy = sortBy;
            this.SortOrder = sortOrder;
        }

        public string Locale { get; set; }
        public ArticleSortEnum SortBy { get; set; }
        public ArticleSortOrderEnum SortOrder { get; set; }

        public string GetSortingString(string resourceUrl){
            //If sorting is enabled, modify the Uri with the Locale
            resourceUrl = resourceUrl.Replace("help_center/", string.Format("help_center/{0}/", this.Locale));

            return string.Format("{0}?sort_by={1}&sort_order={2}", resourceUrl, SortBy.ToString().ToLower(), SortOrder.ToString().ToLower());
        }
    }
}