using System;
using System.Collections.Generic;
using System.Net;
#if ASYNC
using System.Threading.Tasks;
#endif
using ZendeskApi_v2.Extensions;
using ZendeskApi_v2.Models.Requests;
using ZendeskApi_v2.Models.Shared;
using ZendeskApi_v2.Models.Tickets;
using ZendeskApi_v2.Models.Tickets.Suspended;
using ZendeskApi_v2.Models.Users;

namespace ZendeskApi_v2.Requests
{
    public class Tickets : Core
    {
        private const string _tickets = "tickets";
        private const string _views = "views";
        private const string _organizations = "organizations";


        public Tickets(string yourZendeskUrl, string user, string password)
            : base(yourZendeskUrl, user, password)
        {
        }

#if SYNC
        public GroupTicketResponse GetAllTickets()
        {            
            return GenericGet<GroupTicketResponse>(_tickets + ".json");
        }

        public GroupTicketResponse GetTicketsByViewID(int id)
        {
            return GenericGet<GroupTicketResponse>(string.Format("{0}/{1}/{2}.json", _views, id, _tickets));
        }

        public GroupTicketResponse GetTicketsByOrganizationID(long id)
        {
            return GenericGet<GroupTicketResponse>(string.Format("{0}/{1}/{2}.json", _organizations, id, _tickets));
        }

        public GroupTicketResponse GetRecentTickets()
        {
            return GenericGet<GroupTicketResponse>("tickets/recent.json");
        }

        public GroupTicketResponse GetTicketsByUserID(long userId)
        {
            return GenericGet<GroupTicketResponse>(string.Format("users/{0}/tickets/requested.json", userId));
        }

        public GroupTicketResponse GetTicketsWhereUserIsCopied(long userId)
        {
            return GenericGet<GroupTicketResponse>(string.Format("users/{0}/tickets/ccd.json", userId));
        }

        public IndividualTicketResponse GetTicket(long id)
        {            
            return GenericGet<IndividualTicketResponse>(string.Format("{0}/{1}.json", _tickets, id));
        }

        public GroupCommentResponse GetTicketComments(long ticketId)
        {
            return GenericGet<GroupCommentResponse>(string.Format("{0}/{1}/comments.json", _tickets, ticketId));
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
            var resource = string.Format("tickets/{0}/audits/{1}/trust.json", ticketId, auditId);
            var res = RunRequest(resource, "PUT");            
            return res.HttpStatusCode == HttpStatusCode.OK;
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
            var body = new
                           {
                               ticket_field = new
                                                  {
                                                      type = ticketField.Type,
                                                      title = ticketField.Title
                                                  }
                           };

            var res = GenericPost<IndividualTicketFieldResponse>("ticket_fields.json", body);            
            return res;                        
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
            var resource = string.Format("suspended_tickets/{0}/recover.json", id);
            var res = RunRequest(resource, "PUT");
            return res.HttpStatusCode == HttpStatusCode.OK;
        }

        public bool RecoverManySuspendedTickets(List<long> ids)
        {
            var resource = string.Format("suspended_tickets/recover_many.json?ids={0}", ids.ToCsv());
            var res = RunRequest(resource, "PUT");            
            return res.HttpStatusCode == HttpStatusCode.OK;            
        }

        public bool DeleteSuspendedTickets(long id)
        {
            return GenericDelete(string.Format("suspended_tickets/{0}.json", id));
        }

        public bool DeleteManySuspendedTickets(List<long> ids)
        {
            return GenericDelete(string.Format("suspended_tickets/destroy_many.json?ids={0}", ids.ToCsv()));
        }
#endif


#if ASYNC
        public async Task<GroupTicketResponse> GetAllTicketsAsync()
        {            
            return await GenericGetAsync<GroupTicketResponse>(_tickets + ".json");
        }

        public async Task<GroupTicketResponse> GetTicketsByViewIDAsync(int id)
        {
            return await GenericGetAsync<GroupTicketResponse>(string.Format("{0}/{1}/{2}.json", _views, id, _tickets));
        }

        public async Task<GroupTicketResponse> GetTicketsByOrganizationIDAsync(long id)
        {
            return await GenericGetAsync<GroupTicketResponse>(string.Format("{0}/{1}/{2}.json", _organizations, id, _tickets));
        }

        public async Task<GroupTicketResponse> GetRecentTicketsAsync()
        {
            return await GenericGetAsync<GroupTicketResponse>("tickets/recent.json");
        }

        public async Task<GroupTicketResponse> GetTicketsByUserIDAsync(long userId)
        {
            return await GenericGetAsync<GroupTicketResponse>(string.Format("users/{0}/tickets/requested.json", userId));
        }

        public async Task<GroupTicketResponse> GetTicketsWhereUserIsCopiedAsync(long userId)
        {
            return await GenericGetAsync<GroupTicketResponse>(string.Format("users/{0}/tickets/ccd.json", userId));
        }

        public async Task<IndividualTicketResponse> GetTicketAsync(long id)
        {            
            return await GenericGetAsync<IndividualTicketResponse>(string.Format("{0}/{1}.json", _tickets, id));
        }

        public async Task<GroupCommentResponse> GetTicketCommentsAsync(long ticketId)
        {
            return await GenericGetAsync<GroupCommentResponse>(string.Format("{0}/{1}/comments.json", _tickets, ticketId));
        }

        public async Task<GroupTicketResponse> GetMultipleTicketsAsync(List<long> ids)
        {                        
            return await GenericGetAsync<GroupTicketResponse>(string.Format("{0}/show_many?ids={1}.json", _tickets, ids.ToCsv()));
        }

        public async Task<IndividualTicketResponse> CreateTicketAsync(Ticket ticket)
        {
            var body = new {ticket};
            return await GenericPostAsync<IndividualTicketResponse>(_tickets + ".json", body);
        }

        /// <summary>
        /// UpdateTicket a ticket or add comments to it. Keep in mind that somethings like the description can't be updated.
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        public async Task<IndividualTicketResponse> UpdateTicketAsync(Ticket ticket, Comment comment=null)
        {
            if (comment != null)
                ticket.Comment = comment;
            var body = new { ticket };

            return await GenericPutAsync<IndividualTicketResponse>(string.Format("{0}/{1}.json", _tickets, ticket.Id), body);    
        }

        public async Task<JobStatusResponse> BulkUpdateAsync(List<long> ids, BulkUpdate info)
        {
            var body = new { ticket = info };
            return await GenericPutAsync<JobStatusResponse>(string.Format("{0}/update_many.json?ids={1}", _tickets, ids.ToCsv()), body);            
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await GenericDeleteAsync(string.Format("{0}/{1}.json", _tickets, id));
        }

        public async Task<bool> DeleteMultipleAsync(List<long> ids)
        {
            return await GenericDeleteAsync(string.Format("{0}/destroy_many.json?ids={1}", _tickets, ids.ToCsv()));
        }

        public async Task<GroupUserResponse> GetCollaboratorsAsync(long id)
        {
            return await GenericGetAsync<GroupUserResponse>(string.Format("{0}/{1}/collaborators.json", _tickets, id));
        }

        public async Task<GroupTicketResponse> GetIncidentsAsync(long id)
        {
            return await GenericGetAsync<GroupTicketResponse>(string.Format("{0}/{1}/incidents.json", _tickets, id));
        }

        public async Task<GroupTicketResponse> GetProblemsAsync()
        {
            return await GenericGetAsync<GroupTicketResponse>("problems.json");
        }

        public async Task<GroupTicketResponse> AutoCompleteProblemsAsync(string text)
        {
            return await GenericPostAsync<GroupTicketResponse>("problems/autocomplete.json?text=" + text);
        }

        public async Task<GroupAuditResponse> GetAuditsAsync(long ticketId)
        {
            return await GenericGetAsync<GroupAuditResponse>(string.Format("tickets/{0}/audits.json", ticketId));
        }

        public async Task<IndividualAuditResponse> GetAuditByIdAsync(long ticketId, long auditId)
        {
            return await GenericGetAsync<IndividualAuditResponse>(string.Format("tickets/{0}/audits/{1}.json", ticketId, auditId));
        }

        public async Task<bool> MarkAuditAsTrustedAsync(long ticketId, long auditId)
        {
            var resource = string.Format("tickets/{0}/audits/{1}/trust.json", ticketId, auditId);
            var res = RunRequestAsync(resource, "PUT");
            return await res.ContinueWith(x => x.Result.HttpStatusCode == HttpStatusCode.OK);            
        }

        public async Task<TicketExportResponse> GetInrementalTicketExportAsync(DateTime startTime)
        {            
            return await GenericGetAsync<TicketExportResponse>("exports/tickets.json?start_time=" + startTime.GetEpoch());
        }

        /// <summary>
        /// Since the other method can only be called once every 5 minutes it is not sutable for Automated tests.
        /// </summary>
        /// <param name="startTime"></param>
        /// <returns></returns>
        public async Task<TicketExportResponse> __TestOnly__GetInrementalTicketExportAsync(DateTime startTime)
        {
            return await GenericGetAsync<TicketExportResponse>("exports/tickets/sample.json?start_time=" + startTime.GetEpoch());
        }

        public async Task<GroupTicketFieldResponse> GetTicketFieldsAsync()
        {
            return await GenericGetAsync<GroupTicketFieldResponse>("ticket_fields.json");
        }
        
        public async Task<IndividualTicketFieldResponse> CreateTicketFieldAsync(TicketField ticketField)
        {                        
            var body = new
                           {
                               ticket_field = new
                                                  {
                                                      type = ticketField.Type,
                                                      title = ticketField.Title
                                                  }
                           };

            var res = GenericPostAsync<IndividualTicketFieldResponse>("ticket_fields.json", body);            
            return await res;                        
        }

        public async Task<IndividualTicketFieldResponse> UpdateTicketFieldAsync(TicketField ticketField)
        {
            return await GenericPutAsync<IndividualTicketFieldResponse>(string.Format("ticket_fields/{0}.json", ticketField.Id), ticketField);            
        }

        public async Task<bool> DeleteTicketFieldAsync(long id)
        {
            return await GenericDeleteAsync(string.Format("ticket_fields/{0}.json", id));            
        }

        public async Task<GroupSuspendedTicketResponse> GetSuspendedTicketsAsync()
        {
            return await GenericGetAsync<GroupSuspendedTicketResponse>(string.Format("suspended_tickets.json"));
        }

        public async Task<IndividualSuspendedTicketResponse> GetSuspendedTicketByIdAsync(long id)
        {
            return await GenericGetAsync<IndividualSuspendedTicketResponse>(string.Format("suspended_tickets/{0}.json", id));
        }

        public async Task<bool> RecoverSuspendedTicketAsync(long id)
        {
            var resource = string.Format("suspended_tickets/{0}/recover.json", id);
            var res = RunRequestAsync(resource, "PUT");
            return await res.ContinueWith(x => x.Result.HttpStatusCode == HttpStatusCode.OK);            
        }

        public async Task<bool> RecoverManySuspendedTicketsAsync(List<long> ids)
        {
            var resource = string.Format("suspended_tickets/recover_many.json?ids={0}", ids.ToCsv());
            var res = RunRequestAsync(resource, "PUT");
            return await res.ContinueWith(x => x.Result.HttpStatusCode == HttpStatusCode.OK);            
        }

        public async Task<bool> DeleteSuspendedTicketsAsync(long id)
        {
            return await GenericDeleteAsync(string.Format("suspended_tickets/{0}.json", id));
        }

        public async Task<bool> DeleteManySuspendedTicketsAsync(List<long> ids)
        {
            return await GenericDeleteAsync(string.Format("suspended_tickets/destroy_many.json?ids={0}", ids.ToCsv()));
        }
#endif
    }
}
