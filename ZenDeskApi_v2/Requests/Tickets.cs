using System;
using System.Collections.Generic;
using System.Net;
using RestSharp;
using ZenDeskApi_v2.Extensions;
using ZenDeskApi_v2.Models.Shared;
using ZenDeskApi_v2.Models.Tickets;
using ZenDeskApi_v2.Models.Users;

namespace ZenDeskApi_v2.Requests
{
    public class Tickets : Core
    {
        private const string _tickets = "tickets";


        public Tickets(string yourZenDeskUrl, string user, string password)
            : base(yourZenDeskUrl, user, password)
        {
        }

        public GroupTicketResponse GetAllTickets()
        {            
            return GenericGet<GroupTicketResponse>(_tickets + ".json");
        }

        public IndividualTicketResponse GetTicket(int id)
        {            
            return GenericGet<IndividualTicketResponse>(string.Format("{0}/{1}.json", _tickets, id));
        }

        public GroupTicketResponse GetMultipleTickets(List<int> ids)
        {                        
            return GenericGet<GroupTicketResponse>(string.Format("{0}/show_many?ids={1}.json", _tickets, ids.ToCsv()));
        }

        public IndividualTicketResponse CreateTicket(Ticket ticket)
        {
            return GenericPost<IndividualTicketResponse, IndividualTicketResponse>(_tickets + ".json", new IndividualTicketResponse() { Ticket = ticket });
        }

        /// <summary>
        /// UpdateTicket a ticket or add comments to it. Keep in mind that somethings like the description can't be updated.
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        public IndividualTicketResponse UpdateTicket(Ticket ticket, Comment comment=null)
        {
            if (comment != null)
                ticket.Comment = comment;
            var body = new { ticket };

            return GenericPut<IndividualTicketResponse, object>(string.Format("{0}/{1}.json", _tickets, ticket.Id), body);    
        }

        public JobStatusResult BulkUpdate(List<int> ids, BulkUpdate info)
        {
            var body = new { ticket = info };
            return GenericPut<JobStatusResult, object>(string.Format("{0}/update_many.json?ids={1}", _tickets, ids.ToCsv()), body);            
        }

        public bool Delete(int id)
        {
            return GenericDelete(string.Format("{0}/{1}.json", _tickets, id));
        }

        public bool DeleteMultiple(List<int> ids)
        {
            return GenericDelete(string.Format("{0}/destroy_many.json?ids={1}", _tickets, ids.ToCsv()));
        }

        public GroupUserResponse GetCollaborators(int id)
        {
            return GenericGet<GroupUserResponse>(string.Format("{0}/{1}/collaborators.json", _tickets, id));
        }

        public GroupTicketResponse GetIncidents(int id)
        {
            return GenericGet<GroupTicketResponse>(string.Format("{0}/{1}/incidents.json", _tickets, id));
        }

        public GroupTicketResponse GetProblems()
        {
            return GenericGet<GroupTicketResponse>("problems.json");
        }

        public GroupTicketResponse AutoCompleteProblems(string text)
        {
            return GenericPost<GroupTicketResponse>("problems/autocomplete.json?text=" + text);
        }

        public  GroupAuditResponse GetAudits(int ticketId)
        {
            return GenericGet<GroupAuditResponse>(string.Format("tickets/{0}/audits.json", ticketId));
        }

        public IndividualAuditResponse GetAuditById(int ticketId, long auditId)
        {
            return GenericGet<IndividualAuditResponse>(string.Format("tickets/{0}/audits/{1}.json", ticketId, auditId));
        }

        public bool MarkAuditAsTrusted(int ticketId, long auditId)
        {                        
            var request = new RestRequest
            {
                Method = Method.PUT,
                Resource = string.Format("tickets/{0}/audits/{1}/trust.json", ticketId, auditId),
                RequestFormat = DataFormat.Json
            };

            var res = Execute(request);
            return res.StatusCode == HttpStatusCode.OK;
        }

        public TicketExportResponse GetInrementalTicketExport(DateTime startTime)
        {            
            return GenericGet<TicketExportResponse>("exports/tickets.json?start_time=" + startTime.GetEpoch());
        }

        /// <summary>
        /// Since the other method can only be called once every 5 minutes it is not sutable for Automated tests.
        /// </summary>
        /// <param name="startTime"></param>
        /// <returns></returns>
        public TicketExportResponse __TestOnly__GetInrementalTicketExport(DateTime startTime)
        {
            return GenericGet<TicketExportResponse>("exports/tickets/sample.json?start_time=" + startTime.GetEpoch());
        }

        public GroupTicketFieldResponse GetTicketFields()
        {
            return GenericGet<GroupTicketFieldResponse>("ticket_fields.json");
        }
        
        public IndividualTicketFieldResponse CreateTicketField(TicketField ticketField)
        {
            var request = new RestRequest
            {
                Method = Method.POST,
                Resource = "ticket_fields.json",
                RequestFormat = DataFormat.Json
            };

            var tmp = new
                          {
                              type = ticketField.Type,
                              title = ticketField.Title,                                                            
                          };
            request.AddAndSerializeParam(new { ticket_field = tmp }, ParameterType.RequestBody);

            var res = Execute<IndividualTicketFieldResponse>(request);
            return res;
            
            //return GenericPost<IndividualTicketFieldResponse, object>("ticket_fields.json", new { ticket_field = tmp });            
        }

        public IndividualTicketFieldResponse UpdateTicketField(TicketField ticketField)
        {
            return GenericPut<IndividualTicketFieldResponse, TicketField>(string.Format("ticket_fields/{0}.json", ticketField.Id), ticketField);            
        }

        public bool DeleteTicketField(int id)
        {
            return GenericDelete(string.Format("ticket_fields/{0}.json", id));            
        }
    }
}
