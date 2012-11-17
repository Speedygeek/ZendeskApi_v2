using System;
using System.Collections.Generic;
using System.Net;
using RestSharp;
using ZenDeskApi_v2.Extensions;
using ZenDeskApi_v2.Models.Shared;
using ZenDeskApi_v2.Models.Tickets;
using ZenDeskApi_v2.Models.Tickets.Suspended;
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

        public IndividualTicketResponse GetTicket(long id)
        {            
            return GenericGet<IndividualTicketResponse>(string.Format("{0}/{1}.json", _tickets, id));
        }

        public GroupTicketResponse GetMultipleTickets(List<long> ids)
        {                        
            return GenericGet<GroupTicketResponse>(string.Format("{0}/show_many?ids={1}.json", _tickets, ids.ToCsv()));
        }

        public IndividualTicketResponse CreateTicket(Ticket ticket)
        {
            var body = new {ticket};
            return GenericPost<IndividualTicketResponse>(_tickets + ".json", body);
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

            return GenericPut<IndividualTicketResponse>(string.Format("{0}/{1}.json", _tickets, ticket.Id), body);    
        }

        public JobStatusResponse BulkUpdate(List<long> ids, BulkUpdate info)
        {
            var body = new { ticket = info };
            return GenericPut<JobStatusResponse>(string.Format("{0}/update_many.json?ids={1}", _tickets, ids.ToCsv()), body);            
        }

        public bool Delete(long id)
        {
            return GenericDelete(string.Format("{0}/{1}.json", _tickets, id));
        }

        public bool DeleteMultiple(List<long> ids)
        {
            return GenericDelete(string.Format("{0}/destroy_many.json?ids={1}", _tickets, ids.ToCsv()));
        }

        public GroupUserResponse GetCollaborators(long id)
        {
            return GenericGet<GroupUserResponse>(string.Format("{0}/{1}/collaborators.json", _tickets, id));
        }

        public GroupTicketResponse GetIncidents(long id)
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

        public  GroupAuditResponse GetAudits(long ticketId)
        {
            return GenericGet<GroupAuditResponse>(string.Format("tickets/{0}/audits.json", ticketId));
        }

        public IndividualAuditResponse GetAuditById(long ticketId, long auditId)
        {
            return GenericGet<IndividualAuditResponse>(string.Format("tickets/{0}/audits/{1}.json", ticketId, auditId));
        }

        public bool MarkAuditAsTrusted(long ticketId, long auditId)
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
            return GenericPut<IndividualTicketFieldResponse>(string.Format("ticket_fields/{0}.json", ticketField.Id), ticketField);            
        }

        public bool DeleteTicketField(long id)
        {
            return GenericDelete(string.Format("ticket_fields/{0}.json", id));            
        }

        public GroupSuspendedTicketResponse GetSuspendedTickets()
        {
            return GenericGet<GroupSuspendedTicketResponse>(string.Format("suspended_tickets.json"));
        }

        public IndividualSuspendedTicketResponse GetSuspendedTicketById(long id)
        {
            return GenericGet<IndividualSuspendedTicketResponse>(string.Format("suspended_tickets/{0}.json", id));
        }

        public bool RecoverSuspendedTicket(long id)
        {
            var request = new RestRequest
            {
                Method = Method.PUT,
                Resource = string.Format("suspended_tickets/{0}/recover.json", id),
                RequestFormat = DataFormat.Json
            };            

            var res = Execute(request);
            return res.StatusCode == HttpStatusCode.OK;
        }

        public bool RecoverManySuspendedTickets(List<long> ids)
        {
            var request = new RestRequest
            {
                Method = Method.PUT,
                Resource = string.Format("suspended_tickets/recover_many.json?ids={0}", ids.ToCsv()),
                RequestFormat = DataFormat.Json
            };            

            var res = Execute(request);
            return res.StatusCode == HttpStatusCode.OK;            
        }

        public bool DeleteSuspendedTickets(long id)
        {
            return GenericDelete(string.Format("suspended_tickets/{0}.json", id));
        }

        public bool DeleteManySuspendedTickets(List<long> ids)
        {
            return GenericDelete(string.Format("suspended_tickets/destroy_many.json?ids={0}", ids.ToCsv()));
        }
    }
}
