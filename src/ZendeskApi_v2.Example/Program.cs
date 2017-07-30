using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi_v2.Models.Tickets;
using ZendeskApi_v2.Models.Users;

namespace ZendeskApi_v2.Example
{
    class Program
    {

        static void Main(string[] args)
        {
            Task.Run(() => MainAsync(null)).Wait();

            System.Console.ReadKey();
        }

        static async Task MainAsync(string email)
        {

            string userEmailToSearchFor = "eneif123@yahoo.com";

            string userName = "csharpzendeskapi1234@gmail.com"; // the user that will be logging in the API aka the call center staff 
            string userPassword = "&H3n!0q^3OjDLdm";
            string companySubDomain = "csharpapi"; // sub-domain for the account with Zendesk
            int pageSize = 5;
            var api = new ZendeskApi(companySubDomain, userName, userPassword);

            var userResponse = api.Search.SearchFor<User>(userEmailToSearchFor);

            long userId = userResponse.Results[0].Id.Value;
            List<Ticket> tickets = new List<Ticket>();

            var ticketResponse = await api.Tickets.GetTicketsByUserIDAsync(userId, pageSize, sideLoadOptions: Requests.TicketSideLoadOptionsEnum.Users); // default per page is 100

            do
            {
                tickets.AddRange(ticketResponse.Tickets);

                if (!string.IsNullOrWhiteSpace(ticketResponse.NextPage))
                {
                    ticketResponse = await api.Tickets.GetByPageUrlAsync<GroupTicketResponse>(ticketResponse.NextPage, pageSize);
                }


            } while (tickets.Count != ticketResponse.Count);

            foreach (var ticket in tickets)
            {
                System.Console.WriteLine(string.Format("ticket id: {0 }, Assignee Id: {1}, Requester Id: {2}", ticket.Id, ticket.AssigneeId, ticket.RequesterId ));
            }

        }
    }
}
