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
    [Flags]
    public enum TicketSideLoadOptionsEnum
    {
        None = 1,
        Users = 2,
        Groups = 4,
        Organizations = 8,
        Last_Audits = 16,
        Metric_Sets = 32,
        Sharing_Agreements = 64,
        Incident_Counts = 128,
        Ticket_Forms = 256
    }

    public interface ITickets : ICore
    {
        
#if SYNC
        GroupTicketFormResponse GetTicketForms();
        IndividualTicketFormResponse CreateTicketForm(TicketForm ticketForm);
        IndividualTicketFormResponse GetTicketFormById(long id);
        IndividualTicketFormResponse UpdateTicketForm(TicketForm ticketForm);
        bool ReorderTicketForms(long[] orderedTicketFormIds);
        IndividualTicketFormResponse CloneTicketForm(long ticketFormId);
        bool DeleteTicketForm(long id);
        GroupTicketResponse GetAllTickets(int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);
        GroupTicketResponse GetTicketsByViewID(long viewId, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);
        GroupTicketResponse GetTicketsByOrganizationID(long id, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);
        GroupTicketResponse GetTicketsByOrganizationID(long id, int pageNumber, int itemsPerPage, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);
        GroupTicketResponse GetRecentTickets(int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);
        GroupTicketResponse GetTicketsByUserID(long userId, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);
        GroupTicketResponse GetAssignedTicketsByUserID( long userId, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None );
        GroupTicketResponse GetTicketsWhereUserIsCopied(long userId, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);
        IndividualTicketResponse GetTicket(long id, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);
        GroupCommentResponse GetTicketComments(long ticketId, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);
        GroupTicketResponse GetMultipleTickets(IEnumerable<long> ids, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);
        IndividualTicketResponse CreateTicket(Ticket ticket);

        /// <summary>
        /// UpdateTicket a ticket or add comments to it. Keep in mind that somethings like the description can't be updated.
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        IndividualTicketResponse UpdateTicket(Ticket ticket, Comment comment = null);

        JobStatusResponse BulkUpdate(IEnumerable<long> ids, BulkUpdate info);
        bool Delete(long id);
        bool DeleteMultiple(IEnumerable<long> ids);
        GroupUserResponse GetCollaborators(long id);
        GroupTicketResponse GetIncidents(long id);
        GroupTicketResponse GetProblems();
        GroupTicketResponse AutoCompleteProblems(string text);
        GroupAuditResponse GetAudits(long ticketId);
        GroupAuditResponse GetAuditsNextPage(string NextPage);
        IndividualAuditResponse GetAuditById(long ticketId, long auditId);
        bool MarkAuditAsTrusted(long ticketId, long auditId);
        [Obsolete("This has been deprecated. Please use GetIncrementalTicketExport", true)]
        GroupTicketExportResponse GetInrementalTicketExport(DateTimeOffset startTime, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);
        GroupTicketExportResponse GetIncrementalTicketExport(DateTimeOffset startTime, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);
        GroupTicketExportResponse GetIncrementalTicketExportNextPage(string nextPage);

        /// <summary>
        /// Since the other method can only be called once every 5 minutes it is not sutable for Automated tests.
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="sideLoadOptions"></param>
        /// <returns></returns>
        [Obsolete("This has been deprecated. Please use __TestOnly__GetIncrementalTicketExport", true)]
        GroupTicketExportResponse __TestOnly__GetInrementalTicketExport(DateTimeOffset startTime, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);
        GroupTicketExportResponse __TestOnly__GetIncrementalTicketExport(DateTimeOffset startTime, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);

        GroupTicketFieldResponse GetTicketFields();
        IndividualTicketFieldResponse GetTicketFieldById(long id);
        IndividualTicketFieldResponse CreateTicketField(TicketField ticketField);
        IndividualTicketFieldResponse UpdateTicketField(TicketField ticketField);
        bool DeleteTicketField(long id);
        GroupSuspendedTicketResponse GetSuspendedTickets();
        IndividualSuspendedTicketResponse GetSuspendedTicketById(long id);
        bool RecoverSuspendedTicket(long id);
        bool RecoverManySuspendedTickets(IEnumerable<long> ids);
        bool DeleteSuspendedTickets(long id);
        bool DeleteManySuspendedTickets(IEnumerable<long> ids);
        GroupTicketMetricResponse GetAllTicketMetrics();
        IndividualTicketMetricResponse GetTicketMetricsForTicket(long ticket_id);
        IndividualTicketResponse ImportTicket(TicketImport ticket);
        JobStatusResponse BulkImportTickets(IEnumerable<TicketImport> tickets);
#endif

#if ASYNC
        Task<GroupTicketResponse> GetAllTicketsAsync(int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);
        Task<GroupTicketResponse> GetTicketsByViewIDAsync(long viewId, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);
        Task<GroupTicketResponse> GetTicketsByOrganizationIDAsync(long id, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);
        Task<GroupTicketResponse> GetRecentTicketsAsync(int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);
        Task<GroupTicketResponse> GetTicketsByUserIDAsync(long userId, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);
        Task<GroupTicketResponse> GetAssignedTicketsByUserIDAsync( long userId, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None );
        Task<GroupTicketResponse> GetTicketsWhereUserIsCopiedAsync(long userId, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);
        Task<IndividualTicketResponse> GetTicketAsync(long id, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);
        Task<GroupCommentResponse> GetTicketCommentsAsync(long ticketId, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);
        Task<GroupTicketResponse> GetMultipleTicketsAsync(IEnumerable<long> ids, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);
        Task<IndividualTicketResponse> CreateTicketAsync(Ticket ticket);

        /// <summary>
        /// UpdateTicket a ticket or add comments to it. Keep in mind that somethings like the description can't be updated.
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        Task<IndividualTicketResponse> UpdateTicketAsync(Ticket ticket, Comment comment = null);

        Task<JobStatusResponse> BulkUpdateAsync(IEnumerable<long> ids, BulkUpdate info);
        Task<bool> DeleteAsync(long id);
        Task<bool> DeleteMultipleAsync(IEnumerable<long> ids);
        Task<GroupUserResponse> GetCollaboratorsAsync(long id);
        Task<GroupTicketResponse> GetIncidentsAsync(long id);
        Task<GroupTicketResponse> GetProblemsAsync();
        Task<GroupTicketResponse> AutoCompleteProblemsAsync(string text);
        Task<GroupAuditResponse> GetAuditsAsync(long ticketId);
        Task<IndividualAuditResponse> GetAuditByIdAsync(long ticketId, long auditId);
        Task<bool> MarkAuditAsTrustedAsync(long ticketId, long auditId);
        Task<GroupTicketExportResponse> GetInrementalTicketExportAsync(DateTimeOffset startTime, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);

        /// <summary>
        /// Since the other method can only be called once every 5 minutes it is not sutable for Automated tests.
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="sideLoadOptions"></param>
        /// <returns></returns>
        Task<GroupTicketExportResponse> __TestOnly__GetInrementalTicketExportAsync(DateTimeOffset startTime, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);

        Task<GroupTicketFieldResponse> GetTicketFieldsAsync();
        Task<IndividualTicketFieldResponse> GetTicketFieldByIdAsync(long id);
        Task<IndividualTicketFieldResponse> CreateTicketFieldAsync(TicketField ticketField);
        Task<IndividualTicketFieldResponse> UpdateTicketFieldAsync(TicketField ticketField);
        Task<bool> DeleteTicketFieldAsync(long id);
        Task<GroupSuspendedTicketResponse> GetSuspendedTicketsAsync();
        Task<IndividualSuspendedTicketResponse> GetSuspendedTicketByIdAsync(long id);
        Task<bool> RecoverSuspendedTicketAsync(long id);
        Task<bool> RecoverManySuspendedTicketsAsync(IEnumerable<long> ids);
        Task<bool> DeleteSuspendedTicketsAsync(long id);
        Task<bool> DeleteManySuspendedTicketsAsync(IEnumerable<long> ids);
        Task<GroupTicketFormResponse> GetTicketFormsAsync();
        Task<IndividualTicketFormResponse> CreateTicketFormAsync(TicketForm ticketForm);
        Task<IndividualTicketFormResponse> GetTicketFormByIdAsync(long id);
        Task<IndividualTicketFormResponse> UpdateTicketFormAsync(TicketForm ticketForm);
        Task<bool> ReorderTicketFormsAsync(long[] orderedTicketFormIds);
        Task<IndividualTicketFormResponse> CloneTicketFormAsync(long ticketFormId);
        Task<bool> DeleteTicketFormAsync(long id);
        Task<GroupTicketMetricResponse> GetAllTicketMetricsAsync();
        Task<IndividualTicketMetricResponse> GetTicketMetricsForTicketAsync(long ticket_id);
        Task<IndividualTicketResponse> ImportTicketAsync(TicketImport ticket);
        Task<JobStatusResponse> BulkImportTicketsAsync(IEnumerable<TicketImport> tickets);
#endif
    }

    public class Tickets : Core, ITickets
    {
        private const string _tickets = "tickets";
        private const string _imports = "imports";
        private const string _create_many = "create_many";
        private const string _ticket_forms = "ticket_forms";
        private const string _views = "views";
        private const string _organizations = "organizations";
        private const string _ticket_metrics = "ticket_metrics";
        private const string _incremental_export = "incremental/tickets.json?start_time=";

        public Tickets(string yourZendeskUrl, string user, string password, string apiToken, string p_OAuthToken)
            : base(yourZendeskUrl, user, password, apiToken, p_OAuthToken)
        {
        }

#if SYNC

        public GroupTicketFormResponse GetTicketForms()
        {
            return GenericGet<GroupTicketFormResponse>(_ticket_forms + ".json");
        }

        public IndividualTicketFormResponse CreateTicketForm(TicketForm ticketForm)
        {
            var body = new { ticket_form = ticketForm };
            return GenericPost<IndividualTicketFormResponse>(_ticket_forms + ".json", body);
        }

        public IndividualTicketFormResponse GetTicketFormById(long id)
        {
            return GenericGet<IndividualTicketFormResponse>(string.Format("{0}/{1}.json", _ticket_forms, id));
        }

        public IndividualTicketFormResponse UpdateTicketForm(TicketForm ticketForm)
        {
            var body = new { ticket_form = ticketForm };
            return GenericPut<IndividualTicketFormResponse>(string.Format("{0}/{1}.json", _ticket_forms, ticketForm.Id), body);
        }


        public bool ReorderTicketForms(long[] orderedTicketFormIds)
        {
            var body = new { ticket_form_ids = orderedTicketFormIds };
            return GenericPut<bool>(string.Format("{0}/reorder.json", _ticket_forms), body);
        }

        public IndividualTicketFormResponse CloneTicketForm(long ticketFormId)
        {
            return GenericPost<IndividualTicketFormResponse>(string.Format("{0}/{1}/clone.json", _ticket_forms, ticketFormId));
        }

        public bool DeleteTicketForm(long id)
        {
            return GenericDelete(string.Format("{0}/{1}.json", _ticket_forms, id));
        }



        public GroupTicketResponse GetAllTickets(int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            string resource = GetResourceStringWithSideLoadOptionsParam(_tickets + ".json", sideLoadOptions);
            return GenericPagedGet<GroupTicketResponse>(resource, perPage, page);
        }

        public GroupTicketResponse GetTicketsByViewID(long viewId, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            string resource = GetResourceStringWithSideLoadOptionsParam(string.Format("{0}/{1}/{2}.json", _views, viewId, _tickets), sideLoadOptions);
            return GenericPagedGet<GroupTicketResponse>(resource, perPage, page);
        }

        public GroupTicketResponse GetTicketsByOrganizationID(long id, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            string resource = GetResourceStringWithSideLoadOptionsParam(string.Format("{0}/{1}/{2}.json", _organizations, id, _tickets), sideLoadOptions);
            return GenericGet<GroupTicketResponse>(resource);
        }

        public GroupTicketResponse GetTicketsByOrganizationID(long id, int pageNumber, int itemsPerPage, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            string resource = GetResourceStringWithSideLoadOptionsParam(string.Format("{0}/{1}/{2}.json", _organizations, id, _tickets), sideLoadOptions);
            return GenericPagedGet<GroupTicketResponse>(resource, itemsPerPage, pageNumber);
        }

        public GroupTicketResponse GetRecentTickets(int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            string resource = GetResourceStringWithSideLoadOptionsParam("tickets/recent.json", sideLoadOptions);
            return GenericPagedGet<GroupTicketResponse>(resource, perPage, page);
        }

        public GroupTicketResponse GetTicketsByUserID(long userId, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            string resource = GetResourceStringWithSideLoadOptionsParam(string.Format("users/{0}/tickets/requested.json", userId), sideLoadOptions);
            return GenericPagedGet<GroupTicketResponse>(resource, perPage, page);
        }
        public GroupTicketResponse GetAssignedTicketsByUserID( long userId, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None ) 
        {
            string resource = GetResourceStringWithSideLoadOptionsParam( string.Format( "users/{0}/tickets/assigned.json", userId ), sideLoadOptions );
            return GenericPagedGet<GroupTicketResponse>( resource, perPage, page );
        }

        public GroupTicketResponse GetTicketsWhereUserIsCopied(long userId, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            string resource = GetResourceStringWithSideLoadOptionsParam(string.Format("users/{0}/tickets/ccd.json", userId), sideLoadOptions);
            return GenericPagedGet<GroupTicketResponse>(resource, perPage, page);
        }

        public IndividualTicketResponse GetTicket(long id, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            string resource = GetResourceStringWithSideLoadOptionsParam(string.Format("{0}/{1}.json", _tickets, id), sideLoadOptions);
            return GenericGet<IndividualTicketResponse>(resource);
        }

        public GroupCommentResponse GetTicketComments(long ticketId, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            string resource = GetResourceStringWithSideLoadOptionsParam(string.Format("{0}/{1}/comments.json", _tickets, ticketId), sideLoadOptions);
            return GenericPagedGet<GroupCommentResponse>(resource, perPage, page);
        }

        public GroupTicketResponse GetMultipleTickets(IEnumerable<long> ids, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            string resource = GetResourceStringWithSideLoadOptionsParam(string.Format("{0}/show_many.json?ids={1}", _tickets, ids.ToCsv()), sideLoadOptions);
            return GenericGet<GroupTicketResponse>(resource);
        }


        public IndividualTicketResponse CreateTicket(Ticket ticket)
        {
            var body = new { ticket };
            return GenericPost<IndividualTicketResponse>(_tickets + ".json", body);
        }

        /// <summary>
        /// In addition to setting normal ticket properties, you can set the following time stamps on the tickets: solved_at, updated_at, and created_at.
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public IndividualTicketResponse ImportTicket(TicketImport ticket)
        {
            var body = new { ticket };
            return GenericPost<IndividualTicketResponse>(_imports + "/" + _tickets + ".json", body);
        }

        /// <summary>
        /// In addition to setting normal ticket properties, you can set the following time stamps on the tickets: solved_at, updated_at, and created_at.
        /// </summary>
        /// <param name="tickets"></param>
        /// <returns></returns>
        public JobStatusResponse BulkImportTickets(IEnumerable<TicketImport> tickets)
        {
            var body = new { tickets };
            return GenericPost<JobStatusResponse>(_imports + "/" + _tickets + "/" + _create_many + ".json", body);
        }

        /// <summary>
        /// UpdateTicket a ticket or add comments to it. Keep in mind that somethings like the description can't be updated.
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        public IndividualTicketResponse UpdateTicket(Ticket ticket, Comment comment = null)
        {
            if (comment != null)
                ticket.Comment = comment;
            var body = new { ticket };

            return GenericPut<IndividualTicketResponse>(string.Format("{0}/{1}.json", _tickets, ticket.Id), body);
        }

        public JobStatusResponse BulkUpdate(IEnumerable<long> ids, BulkUpdate info)
        {
            var body = new { ticket = info };
            return GenericPut<JobStatusResponse>(string.Format("{0}/update_many.json?ids={1}", _tickets, ids.ToCsv()), body);
        }

        public bool Delete(long id)
        {
            return GenericDelete(string.Format("{0}/{1}.json", _tickets, id));
        }

        public bool DeleteMultiple(IEnumerable<long> ids)
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

        public GroupAuditResponse GetAudits(long ticketId)
        {
            return GenericGet<GroupAuditResponse>(string.Format("tickets/{0}/audits.json", ticketId));
        }
        public GroupAuditResponse GetAuditsNextPage(string NextPage)
        {
            var resource = NextPage.Replace(ZendeskUrl, string.Empty);

            return GenericGet<GroupAuditResponse>(resource);
        }
        public IndividualAuditResponse GetAuditById(long ticketId, long auditId)
        {
            return GenericGet<IndividualAuditResponse>(string.Format("tickets/{0}/audits/{1}.json", ticketId, auditId));
        }

        public bool MarkAuditAsTrusted(long ticketId, long auditId)
        {
            var resource = string.Format("tickets/{0}/audits/{1}/trust.json", ticketId, auditId);
            var res = RunRequest(resource, RequestMethod.Put);
            return res.HttpStatusCode == HttpStatusCode.OK;
        }

        [Obsolete("This has been deprecated. Please use GetIncrementalTicketExport", true)]
        public GroupTicketExportResponse GetInrementalTicketExport(DateTimeOffset startTime, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            return GenericGet<GroupTicketExportResponse>(_incremental_export + startTime.UtcDateTime.GetEpoch());
        }

        /// <summary>
        /// Gets the tickets that have changed since a certain time.
        /// </summary>
        /// <param name="startTime">Return tickets that have changed since this time.</param>
        /// <param name="sideLoadOptions">Retrieve related records as part of this request.</param>
        /// <returns>Tickets that have changed since the given startTime.</returns>
        /// <remarks>
        /// The incremental api will return a maximum of 1000 items.  If the ticket count in the
        /// result is 1000, use the nextPage value of the result to request the next set of items.
        /// Keep repeating the request using the nextPage value until the number of tickets in the
        /// response is less than 1000.
        /// </remarks>
        public GroupTicketExportResponse GetIncrementalTicketExport(DateTimeOffset startTime, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            var resource =
                GetResourceStringWithSideLoadOptionsParam(
                    _incremental_export + startTime.UtcDateTime.GetEpoch(),
                    sideLoadOptions);

            return GenericGet<GroupTicketExportResponse>(resource);
        }

        /// <summary>
        /// Gets another page of changed tickets.
        /// </summary>
        /// <param name="nextPage">The URL of the next page of changed tickets.</param>
        /// <returns>
        /// The next page of tickets that have changed since a given startTime
        /// issued in a previous request.
        /// </returns>
        /// <remarks>
        /// This is the paging method for getting additional pages of changed tickets
        /// after an initial request is made with a given startTime.  Repeat the call to
        /// this method until the response ticket count is less than 1000.
        /// </remarks>
        public GroupTicketExportResponse GetIncrementalTicketExportNextPage(string nextPage)
        {
            var resource = nextPage.Replace(ZendeskUrl, string.Empty);

            return GenericGet<GroupTicketExportResponse>(resource);
        }

        /// <summary>
        /// Since the other method can only be called once every 5 minutes it is not sutable for Automated tests.
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="sideLoadOptions"></param>
        /// <returns></returns>
        [Obsolete("This has been deprecated. Please use __TestOnly__GetIncrementalTicketExport", true)]
        public GroupTicketExportResponse __TestOnly__GetInrementalTicketExport(DateTimeOffset startTime, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            return GenericGet<GroupTicketExportResponse>("incremental/tickets/sample.json?start_time=" + startTime.UtcDateTime.GetEpoch());
        }

        public GroupTicketExportResponse __TestOnly__GetIncrementalTicketExport(DateTimeOffset startTime, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            return GenericGet<GroupTicketExportResponse>("incremental/tickets/sample.json?start_time=" + startTime.UtcDateTime.GetEpoch());
        }

        public GroupTicketFieldResponse GetTicketFields()
        {
            return GenericGet<GroupTicketFieldResponse>("ticket_fields.json");
        }

        public IndividualTicketFieldResponse GetTicketFieldById(long id)
        {
            return GenericGet<IndividualTicketFieldResponse>(string.Format("ticket_fields/{0}.json", id));
        }

        public IndividualTicketFieldResponse CreateTicketField(TicketField ticketField)
        {
            if (ticketField.CustomFieldOptions != null)
            {
                foreach (var custom in ticketField.CustomFieldOptions)
                {
                    custom.Name = custom.Name.Replace(' ', '_');
                    custom.Value = custom.Value.Replace(' ', '_');
                }
            }

            var body = new
            {
                ticket_field = ticketField
            };

            var res = GenericPost<IndividualTicketFieldResponse>("ticket_fields.json", body);
            return res;
        }

        public IndividualTicketFieldResponse UpdateTicketField(TicketField ticketField)
        {
            var body = new
            {
                ticket_field = ticketField
            };
            return GenericPut<IndividualTicketFieldResponse>(string.Format("ticket_fields/{0}.json", ticketField.Id), body);
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
            var res = RunRequest(resource, RequestMethod.Put);
            return res.HttpStatusCode == HttpStatusCode.OK;
        }

        public bool RecoverManySuspendedTickets(IEnumerable<long> ids)
        {
            var resource = string.Format("suspended_tickets/recover_many.json?ids={0}", ids.ToCsv());
            var res = RunRequest(resource, RequestMethod.Put);
            return res.HttpStatusCode == HttpStatusCode.OK;
        }

        public bool DeleteSuspendedTickets(long id)
        {
            return GenericDelete(string.Format("suspended_tickets/{0}.json", id));
        }

        public bool DeleteManySuspendedTickets(IEnumerable<long> ids)
        {
            return GenericDelete(string.Format("suspended_tickets/destroy_many.json?ids={0}", ids.ToCsv()));
        }

        #region TicketMetrics
        public GroupTicketMetricResponse GetAllTicketMetrics()
        {
            return GenericGet<GroupTicketMetricResponse>(_ticket_metrics + ".json");
        }

        public IndividualTicketMetricResponse GetTicketMetricsForTicket(long ticket_id)
        {
            return GenericGet<IndividualTicketMetricResponse>(string.Format("{0}/{1}/metrics.json", _tickets, ticket_id));
        }

        #endregion
#endif


#if ASYNC
        public async Task<GroupTicketResponse> GetAllTicketsAsync(int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            string resource = GetResourceStringWithSideLoadOptionsParam(_tickets + ".json", sideLoadOptions);
            return await GenericPagedGetAsync<GroupTicketResponse>(resource, perPage, page);
        }

        public async Task<GroupTicketResponse> GetTicketsByViewIDAsync(long viewId, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            string resource = GetResourceStringWithSideLoadOptionsParam(string.Format("{0}/{1}/{2}.json", _views, viewId, _tickets), sideLoadOptions);
            return await GenericPagedGetAsync<GroupTicketResponse>(resource, perPage, page);
        }

        public async Task<GroupTicketResponse> GetTicketsByOrganizationIDAsync(long id, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            string resource = GetResourceStringWithSideLoadOptionsParam(string.Format("{0}/{1}/{2}.json", _organizations, id, _tickets), sideLoadOptions);
            return await GenericPagedGetAsync<GroupTicketResponse>(resource, perPage, page);
        }

        public async Task<GroupTicketResponse> GetRecentTicketsAsync(int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            string resource = GetResourceStringWithSideLoadOptionsParam("tickets/recent.json", sideLoadOptions);
            return await GenericPagedGetAsync<GroupTicketResponse>(resource, perPage, page);
        }

        public async Task<GroupTicketResponse> GetTicketsByUserIDAsync(long userId, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            string resource = GetResourceStringWithSideLoadOptionsParam(string.Format("users/{0}/tickets/requested.json", userId), sideLoadOptions);
            return await GenericPagedGetAsync<GroupTicketResponse>(resource, perPage, page);
        }

        public async Task<GroupTicketResponse> GetAssignedTicketsByUserIDAsync( long userId, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None ) 
        {
          string resource = GetResourceStringWithSideLoadOptionsParam( string.Format( "users/{0}/tickets/assigned.json", userId ), sideLoadOptions );
          return await GenericPagedGetAsync<GroupTicketResponse>( resource, perPage, page );
        }
        public async Task<GroupTicketResponse> GetTicketsWhereUserIsCopiedAsync(long userId, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            string resource = GetResourceStringWithSideLoadOptionsParam(string.Format("users/{0}/tickets/ccd.json", userId), sideLoadOptions);
            return await GenericPagedGetAsync<GroupTicketResponse>(resource, perPage, page);
        }

        public async Task<IndividualTicketResponse> GetTicketAsync(long id, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            string resource = GetResourceStringWithSideLoadOptionsParam(string.Format("{0}/{1}.json", _tickets, id), sideLoadOptions);
            return await GenericGetAsync<IndividualTicketResponse>(resource);
        }

        public async Task<GroupCommentResponse> GetTicketCommentsAsync(long ticketId, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            string resource = GetResourceStringWithSideLoadOptionsParam(string.Format("{0}/{1}/comments.json", _tickets, ticketId), sideLoadOptions);
            return await GenericPagedGetAsync<GroupCommentResponse>(resource, perPage, page);
        }

        public async Task<GroupTicketResponse> GetMultipleTicketsAsync(IEnumerable<long> ids, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            string resource = GetResourceStringWithSideLoadOptionsParam(string.Format("{0}/show_many.json?ids={1}", _tickets, ids.ToCsv()), sideLoadOptions);
            return await GenericGetAsync<GroupTicketResponse>(resource);
        }

        public async Task<IndividualTicketResponse> CreateTicketAsync(Ticket ticket)
        {
            var body = new { ticket };
            return await GenericPostAsync<IndividualTicketResponse>(_tickets + ".json", body);
        }

        /// <summary>
        /// In addition to setting normal ticket properties, you can set the following time stamps on the tickets: solved_at, updated_at, and created_at.
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public async Task<IndividualTicketResponse> ImportTicketAsync(TicketImport ticket)
        {
            var body = new { ticket };
            return await GenericPostAsync<IndividualTicketResponse>(_imports + "/" + _tickets + ".json", body);
        }

        /// <summary>
        /// In addition to setting normal ticket properties, you can set the following time stamps on the tickets: solved_at, updated_at, and created_at.
        /// </summary>
        /// <param name="tickets"></param>
        /// <returns></returns>
        public async Task<JobStatusResponse> BulkImportTicketsAsync(IEnumerable<TicketImport> tickets)
        {
            var body = new { tickets };
            return await GenericPostAsync<JobStatusResponse>(_imports + "/" + _tickets + "/" + _create_many + ".json", body);

        }

        /// <summary>
        /// UpdateTicket a ticket or add comments to it. Keep in mind that somethings like the description can't be updated.
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        public async Task<IndividualTicketResponse> UpdateTicketAsync(Ticket ticket, Comment comment = null)
        {
            if (comment != null)
                ticket.Comment = comment;
            var body = new { ticket };

            return await GenericPutAsync<IndividualTicketResponse>(string.Format("{0}/{1}.json", _tickets, ticket.Id), body);
        }

        public async Task<JobStatusResponse> BulkUpdateAsync(IEnumerable<long> ids, BulkUpdate info)
        {
            var body = new { ticket = info };
            return await GenericPutAsync<JobStatusResponse>(string.Format("{0}/update_many.json?ids={1}", _tickets, ids.ToCsv()), body);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await GenericDeleteAsync(string.Format("{0}/{1}.json", _tickets, id));
        }

        public async Task<bool> DeleteMultipleAsync(IEnumerable<long> ids)
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
            var res = RunRequestAsync(resource, RequestMethod.Put);
            return await res.ContinueWith(x => x.Result.HttpStatusCode == HttpStatusCode.OK);
        }

        public async Task<GroupTicketExportResponse> GetInrementalTicketExportAsync(DateTimeOffset startTime, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            return await GenericPagedGetAsync<GroupTicketExportResponse>(_incremental_export + startTime.UtcDateTime.GetEpoch());
        }

        /// <summary>
        /// Since the other method can only be called once every 5 minutes it is not sutable for Automated tests.
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="sideLoadOptions"></param>
        /// <returns></returns>
        public async Task<GroupTicketExportResponse> __TestOnly__GetInrementalTicketExportAsync(DateTimeOffset startTime, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            return await GenericPagedGetAsync<GroupTicketExportResponse>("incremental/tickets/sample.json?start_time=" + startTime.UtcDateTime.GetEpoch());
        }

        public async Task<GroupTicketFieldResponse> GetTicketFieldsAsync()
        {
            return await GenericGetAsync<GroupTicketFieldResponse>("ticket_fields.json");
        }

        public async Task<IndividualTicketFieldResponse> GetTicketFieldByIdAsync(long id)
        {
            return await GenericGetAsync<IndividualTicketFieldResponse>(string.Format("ticket_fields/{0}.json", id));
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
            var res = RunRequestAsync(resource, RequestMethod.Put);
            return await res.ContinueWith(x => x.Result.HttpStatusCode == HttpStatusCode.OK);
        }

        public async Task<bool> RecoverManySuspendedTicketsAsync(IEnumerable<long> ids)
        {
            var resource = string.Format("suspended_tickets/recover_many.json?ids={0}", ids.ToCsv());
            var res = RunRequestAsync(resource, RequestMethod.Put);
            return await res.ContinueWith(x => x.Result.HttpStatusCode == HttpStatusCode.OK);
        }

        public async Task<bool> DeleteSuspendedTicketsAsync(long id)
        {
            return await GenericDeleteAsync(string.Format("suspended_tickets/{0}.json", id));
        }

        public async Task<bool> DeleteManySuspendedTicketsAsync(IEnumerable<long> ids)
        {
            return await GenericDeleteAsync(string.Format("suspended_tickets/destroy_many.json?ids={0}", ids.ToCsv()));
        }

        public async Task<GroupTicketFormResponse> GetTicketFormsAsync()
        {
            return await GenericGetAsync<GroupTicketFormResponse>(_ticket_forms + ".json");
        }

        public async Task<IndividualTicketFormResponse> CreateTicketFormAsync(TicketForm ticketForm)
        {
            var body = new { ticket_form = ticketForm };
            return await GenericPostAsync<IndividualTicketFormResponse>(_ticket_forms + ".json", body);
        }

        public async Task<IndividualTicketFormResponse> GetTicketFormByIdAsync(long id)
        {
            return await GenericGetAsync<IndividualTicketFormResponse>(string.Format("{0}/{1}.json", _ticket_forms, id));
        }

        public async Task<IndividualTicketFormResponse> UpdateTicketFormAsync(TicketForm ticketForm)
        {
            return await GenericPutAsync<IndividualTicketFormResponse>(string.Format("{0}/{1}.json", _ticket_forms, ticketForm.Id), ticketForm);
        }


        public async Task<bool> ReorderTicketFormsAsync(long[] orderedTicketFormIds)
        {
            var body = new { ticket_form_ids = orderedTicketFormIds };
            return await GenericPutAsync<bool>(string.Format("{0}/reorder.json", _ticket_forms), body);
        }

        public async Task<IndividualTicketFormResponse> CloneTicketFormAsync(long ticketFormId)
        {
            return await GenericPostAsync<IndividualTicketFormResponse>(string.Format("{0}/{1}/clone.json", _ticket_forms, ticketFormId));
        }

        public async Task<bool> DeleteTicketFormAsync(long id)
        {
            return await GenericDeleteAsync(string.Format("{0}/{1}.json", _ticket_forms, id));
        }

        #region TicketMetrics
        public Task<GroupTicketMetricResponse> GetAllTicketMetricsAsync()
        {
            return GenericGetAsync<GroupTicketMetricResponse>(_ticket_metrics + ".json");
        }

        public Task<IndividualTicketMetricResponse> GetTicketMetricsForTicketAsync(long ticket_id)
        {
            return GenericGetAsync<IndividualTicketMetricResponse>(string.Format("{0}/{1}/metrics.json", _tickets, ticket_id));
        }


        #endregion
#endif

        private string GetResourceStringWithSideLoadOptionsParam(string resource, TicketSideLoadOptionsEnum sideLoadOptions)
        {
            if (sideLoadOptions != TicketSideLoadOptionsEnum.None)
            {
                if (sideLoadOptions.HasFlag(TicketSideLoadOptionsEnum.None))
                    sideLoadOptions &= ~TicketSideLoadOptionsEnum.None;

                string sideLoads = sideLoadOptions.ToString().ToLower().Replace(" ", "");
                resource += (resource.Contains("?") ? "&" : "?") + "include=" + sideLoads;
                return resource;
            }

            return resource;
        }
    }
}
