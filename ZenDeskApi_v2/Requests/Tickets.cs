using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using RestSharp;
using ZenDeskApi_v2.Extensions;
using ZenDeskApi_v2.Models;
using ZenDeskApi_v2.Models.Tickets;

namespace ZenDeskApi_v2.Requests
{
    public class Tickets : Core
    {
        private const string _tickets = "tickets";


        public Tickets(string yourZenDeskUrl, string user, string password)
            : base(yourZenDeskUrl, user, password)
        {
        }

        public TicketResponse GetAll()
        {
            var request = new RestRequest
            {
                Method = Method.GET,
                Resource = _tickets + ".json"
            };

            return Execute<TicketResponse>(request);
        }

        public Ticket Get(int id)
        {
            var request = new RestRequest
            {
                Method = Method.GET,
                Resource = string.Format("{0}/{1}.json", _tickets, id)
            };

            return Execute<IndividualTicketResponse>(request).Ticket;
        }

        public TicketResponse GetMultiple(List<int> ids)
        {            
            var request = new RestRequest
            {
                Method = Method.GET,
                Resource = string.Format("{0}/show_many?ids={{{1}}}.json", _tickets, string.Join(",", ids.Select(x => x.ToString(CultureInfo.InvariantCulture)).ToArray()))
            };

            return Execute<TicketResponse>(request);
        }

        public Ticket CreateTicket(Ticket ticket)
        {
            var request = new RestRequest
            {
                Method = Method.POST,
                Resource = _tickets + ".json",
                RequestFormat = DataFormat.Json
                
            };

            var body = new IndividualTicketResponse()
                           {
                               Ticket = ticket
                           };
            request.AddAndSerializeParam(body, ParameterType.RequestBody);            

            var res = Execute<IndividualTicketResponse>(request).Ticket;
            return res;
        }



        ///// <summary>
        ///// Creates a new ticket AND creates a new user as the tickets requester, IF the user does not already exist (based on the requester email). 
        ///// If the requester exists, no user is created and the ticket is created with the existing user as requester
        ///// </summary>
        ///// <param name="description"></param>
        ///// <param name="priority"></param>
        ///// <param name="requesterName"></param>
        ///// <param name="requesterEmail"></param>
        ///// <returns></returns>
        //public int CreateTicketWithRequester(string description, TicketPriorities priority, string requesterName, string requesterEmail)
        //{
        //    return
        //        CreateTicketWithRequester(new Ticket
        //        {
        //            Description = description,
        //            PriorityId = (int)priority,
        //            RequesterName = requesterName,
        //            RequesterEmail = requesterEmail
        //        });
        //}

        ///// <summary>
        ///// Creates a new ticket AND creates a new user as the tickets requester, IF the user does not already exist (based on the requester email). 
        ///// If the requester exists, no user is created and the ticket is created with the existing user as requester
        ///// </summary>
        ///// <param name="ticket"></param>
        ///// <returns></returns>
        //public int CreateTicketWithRequester(Ticket ticket)
        //{
        //    var request = new RestRequest
        //    {
        //        Method = Method.POST,
        //        Resource = Tickets + ".xml"
        //    };

        //    request.AddBody(ticket);

        //    var res = Execute(request);
        //    return GetIdFromLocationHeader(res);
        //}









        //public List<Ticket> GetAllTicketsForUser(string email, int maxPages = 25)
        //{
        //    var tickets = new List<Ticket>();

        //    try
        //    {
        //        int page = 1;
        //        var ticks = new List<Ticket>();

        //        //Try getting the tickets for all of the pages
        //        while ((page == 1 || ticks.Count > 0) && page < maxPages)
        //        {
        //            ticks = GetTicketsForUserByPage(email, page);
        //            tickets.AddRange(ticks);

        //            page++;
        //        }
        //    }
        //    //There were no more pages so just go on
        //    catch (ArgumentNullException ex)
        //    { }

        //    return tickets;
        //}

        //public List<Ticket> GetTicketsForUserByPage(string email, int page = 1)
        //{
        //    var request = new RestRequest
        //    {
        //        Method = Method.GET,
        //        Resource = Requests + ".xml",
        //    };

        //    //Assume the user
        //    request.AddHeader(XOnBehalfOfEmail, email);
        //    request.AddParameter("page", page.ToString());

        //    //Get the open ones
        //    var ticktes = Execute<List<Ticket>>(request);

        //    //Now get the closed ones
        //    request.AddParameter("filter", "solved");
        //    var closedOrSolved = Execute<List<Ticket>>(request);

        //    ticktes.AddRange(closedOrSolved);
        //    return ticktes;
        //}

        //public List<Ticket> GetTicketsInViewByPage(int viewId, int page = 1)
        //{
        //    var request = new RestRequest
        //    {
        //        Method = Method.GET,
        //        Resource = string.Format("rules/{0}.xml", viewId)
        //    };

        //    request.AddParameter("page", page.ToString());

        //    return Execute<List<Ticket>>(request);
        //}

        //public List<Ticket> GetAllTicketsInView(int viewId)
        //{
        //    var tickets = new List<Ticket>();

        //    try
        //    {
        //        int page = 1;
        //        var ticks = new List<Ticket>();

        //        //Try getting the tickets for all of the pages
        //        while (page == 1 || ticks.Count > 0)
        //        {
        //            ticks = GetTicketsInViewByPage(viewId, page);
        //            tickets.AddRange(ticks);

        //            page++;
        //        }
        //    }
        //    //There were no more pages so just go on
        //    catch (ArgumentNullException ex)
        //    { }

        //    return tickets;
        //}

        //public int CreateTicket(string description, int requesterId, TicketPriorities priority, string setTags, List<TicketFieldEntry> ticketFields = null)
        //{
        //    return
        //        CreateTicket(new Ticket
        //        {
        //            Description = description,
        //            RequesterId = requesterId,
                    
        //            Priority = priority,
        //            SetTags = setTags,
        //            TicketFieldEntries = ticketFields
        //        });
        //}

        //public int CreateTicket(Ticket ticket)
        //{
        //    var request = new RestRequest
        //    {
        //        Method = Method.POST,
        //        Resource = Tickets + ".xml"
        //    };

        //    request.AddBody(ticket);

        //    var res = Execute(request);
        //    return GetIdFromLocationHeader(res);
        //}

        ///// <summary>
        ///// Creates a new ticket AND creates a new user as the tickets requester, IF the user does not already exist (based on the requester email). 
        ///// If the requester exists, no user is created and the ticket is created with the existing user as requester
        ///// </summary>
        ///// <param name="description"></param>
        ///// <param name="priority"></param>
        ///// <param name="requesterName"></param>
        ///// <param name="requesterEmail"></param>
        ///// <returns></returns>
        //public int CreateTicketWithRequester(string description, TicketPriorities priority, string requesterName, string requesterEmail)
        //{
        //    return
        //        CreateTicketWithRequester(new Ticket
        //        {
        //            Description = description,
        //            PriorityId = (int)priority,
        //            RequesterName = requesterName,
        //            RequesterEmail = requesterEmail
        //        });
        //}

        ///// <summary>
        ///// Creates a new ticket AND creates a new user as the tickets requester, IF the user does not already exist (based on the requester email). 
        ///// If the requester exists, no user is created and the ticket is created with the existing user as requester
        ///// </summary>
        ///// <param name="ticket"></param>
        ///// <returns></returns>
        //public int CreateTicketWithRequester(Ticket ticket)
        //{
        //    var request = new RestRequest
        //    {
        //        Method = Method.POST,
        //        Resource = Tickets + ".xml"
        //    };

        //    request.AddBody(ticket);

        //    var res = Execute(request);
        //    return GetIdFromLocationHeader(res);
        //}

        //public bool AddComment(int ticketId, Comment comment)
        //{
        //    var request = new RestRequest
        //    {
        //        Method = Method.PUT,
        //        Resource = string.Format("{0}/{1}.xml", Tickets, ticketId)
        //    };
        //    request.AddBody(comment);

        //    var res = Execute(request);

        //    return res.StatusCode == System.Net.HttpStatusCode.OK;
        //}

        //public bool DestroyTicket(int ticketId)
        //{
        //    var request = new RestRequest
        //    {
        //        Method = Method.DELETE,
        //        Resource = string.Format("{0}/{1}.xml", Tickets, ticketId.ToString())
        //    };

        //    var res = Execute(request);

        //    return res.StatusCode == System.Net.HttpStatusCode.OK;
        //}

        //public int CreateTicketAsEndUser(string endUserEmail, string subject, string description)
        //{
        //    return CreateTicketAsEndUser(endUserEmail, new Ticket { Subject = subject, Description = description });
        //}

        //public int CreateTicketAsEndUser(string endUserEmail, Ticket ticket)
        //{
        //    var request = new RestRequest
        //    {
        //        Method = Method.POST,
        //        Resource = Requests + ".xml"
        //    };

        //    request.AddHeader(XOnBehalfOfEmail, endUserEmail);
        //    request.AddBody(ticket);

        //    //request.AddParameter("text/xml", string.Format("<ticket><subject>{0}</subject><description>{1}</description></ticket>", ticket.Subject, ticket.Description), ParameterType.RequestBody);

        //    var res = Execute(request);
        //    return GetIdFromLocationHeader(res);
        //}

        ///// <summary>
        ///// To update custom ticket fields just add them to the ticket's TicketFieldEntries.
        ///// Use GetTicketFields and then manually search to find the one you want to update.
        ///// Note you can't add comments this way. Call AddComments to do that.
        ///// </summary>
        ///// <param name="ticket"></param>
        ///// <returns></returns>
        //public bool UpdateTicket(Ticket ticket)
        //{
        //    ticket.Comments.Clear();
        //    var request = new RestRequest
        //    {
        //        Method = Method.PUT,
        //        Resource = string.Format("{0}/{1}.xml", Tickets, ticket.NiceId.ToString())
        //    };

        //    request.AddBody(ticket);

        //    var res = Execute(request);

        //    return res.StatusCode == System.Net.HttpStatusCode.OK;
        //}

        //public bool UpdateTicketAsEndUser(int ticketId, string description)
        //{
        //    return UpdateTicketAsEndUser(ticketId, new Comment { Value = description });
        //}

        //public bool UpdateTicketAsEndUser(int ticketId, Comment comment)
        //{
        //    string email = GetUserById(GetTicketById(ticketId).RequesterId).Email;

        //    var request = new RestRequest
        //    {
        //        Method = Method.PUT,
        //        Resource = string.Format("{0}/{1}.xml", Requests, ticketId.ToString())
        //    };

        //    request.AddHeader(XOnBehalfOfEmail, email);
        //    request.AddBody(comment);

        //    //request.AddParameter("text/xml", string.Format("<ticket><subject>{0}</subject><description>{1}</description></ticket>", ticket.Subject, ticket.Description), ParameterType.RequestBody);

        //    var res = Execute(request);

        //    return res.StatusCode == System.Net.HttpStatusCode.OK;
        //}

        //public bool DestroyRequest(int requestId)
        //{
        //    var request = new RestRequest
        //    {
        //        Method = Method.DELETE,
        //        Resource = string.Format("{0}/{1}.xml", Requests, requestId.ToString())
        //    };

        //    var res = Execute(request);

        //    return res.StatusCode == System.Net.HttpStatusCode.OK;
        //}
    }
}
