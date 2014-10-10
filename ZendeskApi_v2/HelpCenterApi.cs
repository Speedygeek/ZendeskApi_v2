namespace ZendeskApi_v2.HelpCenter
{
   public interface IHelpCenterApi
   {
      ZendeskApi_v2.Requests.HelpCenter.ICategories Categories { get; }

      string Locale { get; }
   }

   public class HelpCenterApi : IHelpCenterApi
   {
      public ZendeskApi_v2.Requests.HelpCenter.ICategories Categories { get; set; }

      public string Locale { get; set; }

      public HelpCenterApi(string yourZendeskUrl, string user, string password, string apiToken, string locale)
      {
         Categories = new Requests.HelpCenter.Categories(yourZendeskUrl, user, password, apiToken, locale);

         Locale = locale;
      }
   }
}
