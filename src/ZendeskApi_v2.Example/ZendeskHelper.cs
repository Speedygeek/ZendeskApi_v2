using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi_v2.Models.Requests;
using ZendeskApi_v2.Models.Tickets;
using ZendeskApi_v2.Models.Users;
using ZendeskApi_v2.Requests;

namespace ZendeskApi_v2.Example
{
    public class ZendeskHelper
    {
        private readonly IZendeskApi _api;
        private readonly int _pageSize;
        public ZendeskHelper(ZendeskApi_v2.IZendeskApi api, int pageSize = 100)
        {
            _api = api;
            _pageSize = pageSize;
        }

        public async Task<List<Ticket>> GetTickets(string userEmailToSearchFor)
        {
            var userResponse = await _api.Search.SearchForAsync<User>(userEmailToSearchFor);

            var userId = userResponse.Results[0].Id.Value;
            var tickets = new List<Ticket>();

            var ticketResponse = await _api.Tickets.GetTicketsByUserIDAsync(userId, _pageSize, sideLoadOptions: TicketSideLoadOptionsEnum.Users | TicketSideLoadOptionsEnum.Comment_Count); // default per page is 100

            do
            {
                tickets.AddRange(ticketResponse.Tickets);

                if (!string.IsNullOrWhiteSpace(ticketResponse.NextPage))
                {
                    ticketResponse = await _api.Tickets.GetByPageUrlAsync<GroupTicketResponse>(ticketResponse.NextPage, _pageSize);
                }


            } while (tickets.Count != ticketResponse.Count);
            return tickets;
        }

        public async Task<List<Comment>> GetComments(Ticket ticket)
        {
            var comments = new List<Comment>();

            if (ticket.CommentCount > 0)
            {
                var commnetResponse = await _api.Tickets.GetTicketCommentsAsync(ticket.Id.Value, _pageSize);

                do
                {
                    comments.AddRange(commnetResponse.Comments);

                    if (!string.IsNullOrWhiteSpace(commnetResponse.NextPage))
                    {
                        commnetResponse = await _api.Tickets.GetByPageUrlAsync<GroupCommentResponse>(commnetResponse.NextPage, _pageSize);
                    }
                } while (comments.Count != commnetResponse.Count);
            }

            return comments;
        }
    }
}