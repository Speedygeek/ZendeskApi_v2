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
        Ticket_Forms = 256,
        Comment_Count = 512
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

        GroupTicketResponse GetTicketsByOrganizationID(long id, string sortBy, bool sortAscending, int pageNumber, int itemsPerPage, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);

        GroupTicketResponse GetRecentTickets(int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);

        GroupTicketResponse GetTicketsByUserID(long userId, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);

        GroupTicketResponse GetAssignedTicketsByUserID(long userId, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);

        GroupTicketResponse GetTicketsWhereUserIsCopied(long userId, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);

        IndividualTicketResponse GetTicket(long id, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);

        GroupCommentResponse GetTicketComments(long ticketId, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);

        GroupCommentResponse GetTicketComments(long ticketId, bool sortAscending, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);

        GroupTicketResponse GetMultipleTickets(IEnumerable<long> ids, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);

        IndividualTicketResponse CreateTicket(Ticket ticket);

        JobStatusResponse CreateManyTickets(IEnumerable<Ticket> tickets);

        /// <summary>
        /// UpdateTicket a ticket or add comments to it. Keep in mind that somethings like the description can't be updated.
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        IndividualTicketResponse UpdateTicket(Ticket ticket, Comment comment = null);

        JobStatusResponse BulkUpdate(IEnumerable<long> ids, BulkUpdate info);

        JobStatusResponse BatchUpdate(IEnumerable<Ticket> tickets);

        bool Delete(long id);

        bool DeleteMultiple(IEnumerable<long> ids);

        bool DeleteTicketPermanently(long id);

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

        GroupTicketFieldResponse GetTicketFields();

        IndividualTicketFieldResponse GetTicketFieldById(long id);

        IndividualTicketFieldResponse CreateTicketField(TicketField ticketField, bool replaceNameSpacesWithUnderscore = true);

        IndividualTicketFieldResponse UpdateTicketField(TicketField ticketField, bool replaceNameSpacesWithUnderscore = false);

        bool DeleteTicketField(long id);

        GroupSuspendedTicketResponse GetSuspendedTickets();

        IndividualSuspendedTicketResponse GetSuspendedTicketById(long id);

        bool RecoverSuspendedTicket(long id);

        bool RecoverManySuspendedTickets(IEnumerable<long> ids);

        bool DeleteSuspendedTickets(long id);

        bool DeleteManySuspendedTickets(IEnumerable<long> ids);

        GroupTicketMetricResponse GetAllTicketMetrics(int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);

        IndividualTicketMetricResponse GetTicketMetricsForTicket(long ticket_id);

        IndividualTicketResponse ImportTicket(TicketImport ticket);

        JobStatusResponse BulkImportTickets(IEnumerable<TicketImport> tickets);

        GroupTicketResponse GetTicketsByExternalId(string externalId, int pageNumber = 0, int itemsPerPage = 0, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);

        JobStatusResponse MergeTickets(long targetTicketId, IEnumerable<long> sourceTicketIds, string targetComment = "", string sourceComment = "", bool targetCommentPublic = false, bool sourceCommentPublic = false);

#endif

#if ASYNC

        Task<GroupTicketResponse> GetAllTicketsAsync(int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);

        Task<GroupTicketResponse> GetTicketsByViewIDAsync(long viewId, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);

        Task<GroupTicketResponse> GetTicketsByOrganizationIDAsync(long id, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);

        Task<GroupTicketResponse> GetTicketsByOrganizationIDAsync(long id, string sortBy, bool sortAscending, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);

        Task<GroupTicketResponse> GetTicketsByExternalIdAsync(string externalId, int pageNumber = 0, int itemsPerPage = 0, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);

        Task<GroupTicketResponse> GetRecentTicketsAsync(int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);

        Task<GroupTicketResponse> GetTicketsByUserIDAsync(long userId, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);

        Task<GroupTicketResponse> GetTicketsByUserIDAsync(long userId, string sortBy, bool sortAscending, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);

        Task<GroupTicketResponse> GetAssignedTicketsByUserIDAsync(long userId, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);

        Task<GroupTicketResponse> GetTicketsWhereUserIsCopiedAsync(long userId, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);

        Task<IndividualTicketResponse> GetTicketAsync(long id, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);

        Task<GroupCommentResponse> GetTicketCommentsAsync(long ticketId, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);

        Task<GroupCommentResponse> GetTicketCommentsAsync(long ticketId, bool sortAscending, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);

        Task<GroupTicketResponse> GetMultipleTicketsAsync(IEnumerable<long> ids, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);

        Task<IndividualTicketResponse> CreateTicketAsync(Ticket ticket);

        Task<JobStatusResponse> CreateManyTicketsAsync(IEnumerable<Ticket> tickets);

        /// <summary>
        /// UpdateTicket a ticket or add comments to it. Keep in mind that somethings like the description can't be updated.
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        Task<IndividualTicketResponse> UpdateTicketAsync(Ticket ticket, Comment comment = null);

        Task<JobStatusResponse> BulkUpdateAsync(IEnumerable<long> ids, BulkUpdate info);

        Task<JobStatusResponse> BatchUpdateAsync(IEnumerable<Ticket> tickets);

        Task<bool> DeleteAsync(long id);

        Task<bool> DeleteMultipleAsync(IEnumerable<long> ids);

        Task<bool> DeleteTicketPermanentlyAsync(long id);

        Task<GroupUserResponse> GetCollaboratorsAsync(long id);

        Task<GroupTicketResponse> GetIncidentsAsync(long id);

        Task<GroupTicketResponse> GetProblemsAsync();

        Task<GroupTicketResponse> AutoCompleteProblemsAsync(string text);

        Task<GroupAuditResponse> GetAuditsAsync(long ticketId);

        Task<IndividualAuditResponse> GetAuditByIdAsync(long ticketId, long auditId);

        Task<bool> MarkAuditAsTrustedAsync(long ticketId, long auditId);

        [Obsolete("This has been deprecated due to wrong spelling and sideLoadOptions was ignored. Please use GetIncrementalTicketExportAsync instead")]
        Task<GroupTicketExportResponse> GetInrementalTicketExportAsync(DateTimeOffset startTime, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);

        Task<GroupTicketExportResponse> GetIncrementalTicketExportAsync(DateTimeOffset startTime, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);

        Task<GroupTicketExportResponse> GetIncrementalTicketExportNextPageAsync(string nextPage);

        Task<GroupTicketFieldResponse> GetTicketFieldsAsync();

        Task<IndividualTicketFieldResponse> GetTicketFieldByIdAsync(long id);

        Task<IndividualTicketFieldResponse> CreateTicketFieldAsync(TicketField ticketField, bool replaceNameSpacesWithUnderscore = true);

        Task<IndividualTicketFieldResponse> UpdateTicketFieldAsync(TicketField ticketField, bool replaceNameSpacesWithUnderscore = false);

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

        Task<GroupTicketMetricResponse> GetAllTicketMetricsAsync(int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None);

        Task<IndividualTicketMetricResponse> GetTicketMetricsForTicketAsync(long ticket_id);

        Task<IndividualTicketResponse> ImportTicketAsync(TicketImport ticket);

        Task<JobStatusResponse> BulkImportTicketsAsync(IEnumerable<TicketImport> tickets);

        Task<JobStatusResponse> MergeTicketsAsync(long targetTicketId, IEnumerable<long> sourceTicketIds, string targetComment = "", string sourceComment = "", bool targetCommentPublic = false, bool sourceCommentPublic = false);

#endif
    }

    public class Tickets : Core, ITickets
    {
        private const string _tickets = "tickets";
        private const string _deletedTickets = "deleted_tickets";
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
            return GenericGet<GroupTicketFormResponse>($"{_ticket_forms}.json");
        }

        public IndividualTicketFormResponse CreateTicketForm(TicketForm ticketForm)
        {
            return GenericPost<IndividualTicketFormResponse>($"{_ticket_forms}.json", new { ticket_form = ticketForm });
        }

        public IndividualTicketFormResponse GetTicketFormById(long id)
        {
            return GenericGet<IndividualTicketFormResponse>($"{_ticket_forms}/{id}.json");
        }

        public IndividualTicketFormResponse UpdateTicketForm(TicketForm ticketForm)
        {
            return GenericPut<IndividualTicketFormResponse>($"{_ticket_forms}/{ticketForm.Id}.json", new { ticket_form = ticketForm });
        }

        public bool ReorderTicketForms(long[] orderedTicketFormIds)
        {
            return GenericPut<bool>($"{_ticket_forms}/reorder.json", new { ticket_form_ids = orderedTicketFormIds });
        }

        public IndividualTicketFormResponse CloneTicketForm(long ticketFormId)
        {
            return GenericPost<IndividualTicketFormResponse>($"{_ticket_forms}/{ticketFormId}/clone.json");
        }

        public bool DeleteTicketForm(long id)
        {
            return GenericDelete($"{_ticket_forms}/{id}.json");
        }

        public GroupTicketResponse GetAllTickets(int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            var resource = GetResourceStringWithSideLoadOptionsParam($"{_tickets}.json", sideLoadOptions);
            return GenericPagedGet<GroupTicketResponse>(resource, perPage, page);
        }

        public GroupTicketResponse GetTicketsByViewID(long viewId, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            var resource = GetResourceStringWithSideLoadOptionsParam($"{_views}/{viewId}/{_tickets}.json", sideLoadOptions);
            return GenericPagedGet<GroupTicketResponse>(resource, perPage, page);
        }

        public GroupTicketResponse GetTicketsByOrganizationID(long id, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            var resource = GetResourceStringWithSideLoadOptionsParam($"{_organizations}/{id}/{_tickets}.json", sideLoadOptions);
            return GenericGet<GroupTicketResponse>(resource);
        }

        public GroupTicketResponse GetTicketsByOrganizationID(long id, int pageNumber, int itemsPerPage, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            var resource = GetResourceStringWithSideLoadOptionsParam($"{_organizations}/{id}/{_tickets}.json", sideLoadOptions);
            return GenericPagedGet<GroupTicketResponse>(resource, itemsPerPage, pageNumber);
        }

        public GroupTicketResponse GetTicketsByOrganizationID(long id, string sortBy, bool sortAscending, int pageNumber, int itemsPerPage, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            var resource = GetResourceStringWithSideLoadOptionsParam($"{_organizations}/{id}/{_tickets}.json", sideLoadOptions);
            return GenericPagedSortedGet<GroupTicketResponse>(resource, itemsPerPage, pageNumber, sortBy, sortAscending);
        }

        public GroupTicketResponse GetTicketsByExternalId(string externalId, int pageNumber = 0, int itemsPerPage = 0, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            var resource = GetResourceStringWithSideLoadOptionsParam($"{_tickets}.json?external_id={Uri.EscapeDataString(externalId)}", sideLoadOptions);
            return GenericPagedGet<GroupTicketResponse>(resource, itemsPerPage, pageNumber);
        }

        public GroupTicketResponse GetRecentTickets(int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            var resource = GetResourceStringWithSideLoadOptionsParam("tickets/recent.json", sideLoadOptions);
            return GenericPagedGet<GroupTicketResponse>(resource, perPage, page);
        }

        public GroupTicketResponse GetTicketsByUserID(long userId, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            var resource = GetResourceStringWithSideLoadOptionsParam($"users/{userId}/tickets/requested.json", sideLoadOptions);
            return GenericPagedGet<GroupTicketResponse>(resource, perPage, page);
        }

        public GroupTicketResponse GetAssignedTicketsByUserID(long userId, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            var resource = GetResourceStringWithSideLoadOptionsParam($"users/{userId}/tickets/assigned.json", sideLoadOptions);
            return GenericPagedGet<GroupTicketResponse>(resource, perPage, page);
        }

        public GroupTicketResponse GetTicketsWhereUserIsCopied(long userId, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            var resource = GetResourceStringWithSideLoadOptionsParam($"users/{userId}/tickets/ccd.json", sideLoadOptions);
            return GenericPagedGet<GroupTicketResponse>(resource, perPage, page);
        }

        public IndividualTicketResponse GetTicket(long id, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            var resource = GetResourceStringWithSideLoadOptionsParam($"{_tickets}/{id}.json", sideLoadOptions);
            return GenericGet<IndividualTicketResponse>(resource);
        }

        public GroupCommentResponse GetTicketComments(long ticketId, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            var resource = GetResourceStringWithSideLoadOptionsParam($"{_tickets}/{ticketId}/comments.json", sideLoadOptions);
            return GenericPagedGet<GroupCommentResponse>(resource, perPage, page);
        }

        public GroupCommentResponse GetTicketComments(long ticketId, bool sortAscending, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            var resource = GetResourceStringWithSideLoadOptionsParam($"{_tickets}/{ticketId}/comments.json", sideLoadOptions);
            return GenericPagedSortedGet<GroupCommentResponse>(resource, perPage, page, sortAscending: sortAscending);
        }

        public GroupTicketResponse GetMultipleTickets(IEnumerable<long> ids, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            var resource = GetResourceStringWithSideLoadOptionsParam($"{_tickets}/show_many.json?ids={ids.ToCsv()}", sideLoadOptions);
            return GenericGet<GroupTicketResponse>(resource);
        }

        public IndividualTicketResponse CreateTicket(Ticket ticket)
        {
            return GenericPost<IndividualTicketResponse>($"{_tickets}.json", new { ticket });
        }

        public JobStatusResponse CreateManyTickets(IEnumerable<Ticket> tickets)
        {
            return GenericPost<JobStatusResponse>($"{_tickets}/{_create_many}.json", new { tickets });
        }

        /// <summary>
        /// In addition to setting normal ticket properties, you can set the following time stamps on the tickets: solved_at, updated_at, and created_at.
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public IndividualTicketResponse ImportTicket(TicketImport ticket)
        {
            return GenericPost<IndividualTicketResponse>($"{_imports}/{_tickets}.json", new { ticket });
        }

        /// <summary>
        /// In addition to setting normal ticket properties, you can set the following time stamps on the tickets: solved_at, updated_at, and created_at.
        /// </summary>
        /// <param name="tickets"></param>
        /// <returns></returns>
        public JobStatusResponse BulkImportTickets(IEnumerable<TicketImport> tickets)
        {
            return GenericPost<JobStatusResponse>($"{_imports}/{_tickets}/{_create_many}.json", new { tickets });
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
            {
                ticket.Comment = comment;
            }

            return GenericPut<IndividualTicketResponse>($"{_tickets}/{ticket.Id}.json", new { ticket });
        }

        public JobStatusResponse BulkUpdate(IEnumerable<long> ids, BulkUpdate info)
        {
            return GenericPut<JobStatusResponse>($"{_tickets}/update_many.json?ids={ids.ToCsv()}", new { ticket = info });
        }

        public JobStatusResponse BatchUpdate(IEnumerable<Ticket> tickets)
        {
            return GenericPut<JobStatusResponse>($"{_tickets}/update_many.json", new { tickets });
        }

        public bool Delete(long id)
        {
            return GenericDelete($"{_tickets}/{id}.json");
        }

        public bool DeleteMultiple(IEnumerable<long> ids)
        {
            return GenericDelete($"{_tickets}/destroy_many.json?ids={ids.ToCsv()}");
        }

        /// <summary>
        /// Permanently deletes a soft-deleted ticket
        /// </summary>
        /// <param name="id">Id of ticket to be permanently deleted.</param>
        /// <returns>Boolean response</returns>
        public bool DeleteTicketPermanently(long id)
        {
            return GenericDelete($"{_deletedTickets}/{id}.json");
        }

        /// <summary>
        /// Merges the source tickets in the "ids" list into the target ticket with comments as defined.
        /// </summary>
        /// <param name="targetTicketId">Id of ticket to be merged into.</param>
        /// <param name="sourceTicketIds">List of ids of source tickets to be merged from.</param>
        /// <param name="targetComment">Private comment to add to the target ticket (optional but recommended)</param>
        /// <param name="sourceComment">Private comment to add to the source ticket(s) (optional but recommended)</param>
        /// <param name="targetCommentPublic">Whether comment in target ticket is public or private (default = private)</param>
        /// <param name="sourceCommentPublic">Whether comment in source ticket is public or private (default = private)</param>
        /// <returns>JobStatusResponse</returns>
        public JobStatusResponse MergeTickets(long targetTicketId, IEnumerable<long> sourceTicketIds, string targetComment = "", string sourceComment = "", bool targetCommentPublic = false, bool sourceCommentPublic = false)
        {
            return GenericPost<JobStatusResponse>(
                $"{_tickets}/{targetTicketId}/merge.json",
                new
                {
                    ids = sourceTicketIds,
                    target_comment = targetComment,
                    source_comment = sourceComment,
                    target_comment_is_public = targetCommentPublic,
                    source_comment_is_public = sourceCommentPublic
                });
        }

        public GroupUserResponse GetCollaborators(long id)
        {
            return GenericGet<GroupUserResponse>($"{_tickets}/{id}/collaborators.json");
        }

        public GroupTicketResponse GetIncidents(long id)
        {
            return GenericGet<GroupTicketResponse>($"{_tickets}/{id}/incidents.json");
        }

        public GroupTicketResponse GetProblems()
        {
            return GenericGet<GroupTicketResponse>("problems.json");
        }

        public GroupTicketResponse AutoCompleteProblems(string text)
        {
            return GenericPost<GroupTicketResponse>($"problems/autocomplete.json?text={text}");
        }

        public GroupAuditResponse GetAudits(long ticketId)
        {
            return GenericGet<GroupAuditResponse>($"tickets/{ticketId}/audits.json");
        }

        public GroupAuditResponse GetAuditsNextPage(string NextPage)
        {
            var resource = NextPage.Replace(ZendeskUrl, string.Empty);

            return GenericGet<GroupAuditResponse>(resource);
        }

        public IndividualAuditResponse GetAuditById(long ticketId, long auditId)
        {
            return GenericGet<IndividualAuditResponse>($"tickets/{ticketId}/audits/{auditId}.json");
        }

        public bool MarkAuditAsTrusted(long ticketId, long auditId)
        {
            var resource = $"tickets/{ticketId}/audits/{auditId}/trust.json";
            var res = RunRequest(resource, RequestMethod.Put);
            return res.HttpStatusCode == HttpStatusCode.OK;
        }

        [Obsolete("This has been deprecated. Please use GetIncrementalTicketExport", true)]
        public GroupTicketExportResponse GetInrementalTicketExport(DateTimeOffset startTime, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            return GenericGet<GroupTicketExportResponse>($"{_incremental_export}{startTime.UtcDateTime.GetEpoch()}");
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
                GetResourceStringWithSideLoadOptionsParam($"{_incremental_export}{startTime.UtcDateTime.GetEpoch()}",
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

        public GroupTicketFieldResponse GetTicketFields()
        {
            return GenericGet<GroupTicketFieldResponse>("ticket_fields.json");
        }

        public IndividualTicketFieldResponse GetTicketFieldById(long id)
        {
            return GenericGet<IndividualTicketFieldResponse>($"ticket_fields/{id}.json");
        }

        public IndividualTicketFieldResponse CreateTicketField(TicketField ticketField, bool replaceNameSpacesWithUnderscore = true)
        {
            if (ticketField.CustomFieldOptions != null)
            {
                foreach (var custom in ticketField.CustomFieldOptions)
                {
                    if (replaceNameSpacesWithUnderscore)
                        custom.Name = custom.Name.Replace(' ', '_');
                    custom.Value = custom.Value.Replace(' ', '_');
                }
            }

            var res = GenericPost<IndividualTicketFieldResponse>("ticket_fields.json", new
            {
                ticket_field = ticketField
            });
            return res;
        }

        public IndividualTicketFieldResponse UpdateTicketField(TicketField ticketField, bool replaceNameSpacesWithUnderscore = false)
        {
            if (ticketField.CustomFieldOptions != null)
            {
                foreach (var custom in ticketField.CustomFieldOptions)
                {
                    if (replaceNameSpacesWithUnderscore)
                        custom.Name = custom.Name.Replace(' ', '_');
                    custom.Value = custom.Value.Replace(' ', '_');
                }
            }

            return GenericPut<IndividualTicketFieldResponse>($"ticket_fields/{ticketField.Id}.json", new
            {
                ticket_field = ticketField
            });
        }

        public bool DeleteTicketField(long id)
        {
            return GenericDelete($"ticket_fields/{id}.json");
        }

        public GroupSuspendedTicketResponse GetSuspendedTickets()
        {
            return GenericGet<GroupSuspendedTicketResponse>("suspended_tickets.json");
        }

        public IndividualSuspendedTicketResponse GetSuspendedTicketById(long id)
        {
            return GenericGet<IndividualSuspendedTicketResponse>($"suspended_tickets/{id}.json");
        }

        public bool RecoverSuspendedTicket(long id)
        {
            var resource = $"suspended_tickets/{id}/recover.json";
            var res = RunRequest(resource, RequestMethod.Put);
            return res.HttpStatusCode == HttpStatusCode.OK;
        }

        public bool RecoverManySuspendedTickets(IEnumerable<long> ids)
        {
            var resource = $"suspended_tickets/recover_many.json?ids={ids.ToCsv()}";
            var res = RunRequest(resource, RequestMethod.Put);
            return res.HttpStatusCode == HttpStatusCode.OK;
        }

        public bool DeleteSuspendedTickets(long id)
        {
            return GenericDelete($"suspended_tickets/{id}.json");
        }

        public bool DeleteManySuspendedTickets(IEnumerable<long> ids)
        {
            return GenericDelete($"suspended_tickets/destroy_many.json?ids={ids.ToCsv()}");
        }

        #region TicketMetrics

        public GroupTicketMetricResponse GetAllTicketMetrics(int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            var resource = GetResourceStringWithSideLoadOptionsParam($"{_ticket_metrics}.json", sideLoadOptions);
            return GenericPagedGet<GroupTicketMetricResponse>(resource, perPage, page);
        }

        public IndividualTicketMetricResponse GetTicketMetricsForTicket(long ticket_id)
        {
            return GenericGet<IndividualTicketMetricResponse>($"{_tickets}/{ticket_id}/metrics.json");
        }

        #endregion TicketMetrics

#endif

#if ASYNC

        public async Task<GroupTicketResponse> GetAllTicketsAsync(int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            var resource = GetResourceStringWithSideLoadOptionsParam($"{_tickets}.json", sideLoadOptions);
            return await GenericPagedGetAsync<GroupTicketResponse>(resource, perPage, page);
        }

        public async Task<GroupTicketResponse> GetTicketsByViewIDAsync(long viewId, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            var resource = GetResourceStringWithSideLoadOptionsParam($"{_views}/{viewId}/{_tickets}.json", sideLoadOptions);
            return await GenericPagedGetAsync<GroupTicketResponse>(resource, perPage, page);
        }

        public async Task<GroupTicketResponse> GetTicketsByOrganizationIDAsync(long id, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            var resource = GetResourceStringWithSideLoadOptionsParam($"{_organizations}/{id}/{_tickets}.json", sideLoadOptions);
            return await GenericPagedGetAsync<GroupTicketResponse>(resource, perPage, page);
        }

        public async Task<GroupTicketResponse> GetTicketsByOrganizationIDAsync(long id, string sortBy, bool sortAscending, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            var resource = GetResourceStringWithSideLoadOptionsParam($"{_organizations}/{id}/{_tickets}.json", sideLoadOptions);
            return await GenericPagedSortedGetAsync<GroupTicketResponse>(resource, perPage, page, sortBy, sortAscending);
        }

        public async Task<GroupTicketResponse> GetTicketsByExternalIdAsync(string externalId, int pageNumber = 0, int itemsPerPage = 0, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            var resource = GetResourceStringWithSideLoadOptionsParam($"{_tickets}.json?external_id={Uri.EscapeDataString(externalId)}", sideLoadOptions);
            return await GenericPagedGetAsync<GroupTicketResponse>(resource, itemsPerPage, pageNumber);
        }

        public async Task<GroupTicketResponse> GetRecentTicketsAsync(int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            var resource = GetResourceStringWithSideLoadOptionsParam("tickets/recent.json", sideLoadOptions);
            return await GenericPagedGetAsync<GroupTicketResponse>(resource, perPage, page);
        }

        public async Task<GroupTicketResponse> GetTicketsByUserIDAsync(long userId, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            var resource = GetResourceStringWithSideLoadOptionsParam($"users/{userId}/tickets/requested.json", sideLoadOptions);
            return await GenericPagedGetAsync<GroupTicketResponse>(resource, perPage, page);
        }

        public async Task<GroupTicketResponse> GetAssignedTicketsByUserIDAsync(long userId, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            var resource = GetResourceStringWithSideLoadOptionsParam($"users/{userId}/tickets/assigned.json", sideLoadOptions);
            return await GenericPagedGetAsync<GroupTicketResponse>(resource, perPage, page);
        }

        //overload for getting ticket by userId that takes sortBy and sortAscending
        public async Task<GroupTicketResponse> GetTicketsByUserIDAsync(long userId, string sortBy, bool sortAscending, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            var resource = GetResourceStringWithSideLoadOptionsParam($"users/{userId}/tickets/requested.json", sideLoadOptions);
            return await GenericPagedSortedGetAsync<GroupTicketResponse>(resource, perPage, page, sortBy, sortAscending);
        }

        public async Task<GroupTicketResponse> GetTicketsWhereUserIsCopiedAsync(long userId, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            var resource = GetResourceStringWithSideLoadOptionsParam($"users/{userId}/tickets/ccd.json", sideLoadOptions);
            return await GenericPagedGetAsync<GroupTicketResponse>(resource, perPage, page);
        }

        public async Task<IndividualTicketResponse> GetTicketAsync(long id, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            var resource = GetResourceStringWithSideLoadOptionsParam($"{_tickets}/{id}.json", sideLoadOptions);
            return await GenericGetAsync<IndividualTicketResponse>(resource);
        }

        public async Task<GroupCommentResponse> GetTicketCommentsAsync(long ticketId, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            var resource = GetResourceStringWithSideLoadOptionsParam($"{_tickets}/{ticketId}/comments.json", sideLoadOptions);
            return await GenericPagedGetAsync<GroupCommentResponse>(resource, perPage, page);
        }

        public async Task<GroupCommentResponse> GetTicketCommentsAsync(long ticketId, bool sortAscending, int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            var resource = GetResourceStringWithSideLoadOptionsParam($"{_tickets}/{ticketId}/comments.json", sideLoadOptions);
            return await GenericPagedSortedGetAsync<GroupCommentResponse>(resource, perPage, page, sortAscending: sortAscending);
        }

        public async Task<GroupTicketResponse> GetMultipleTicketsAsync(IEnumerable<long> ids, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            return await GenericGetAsync<GroupTicketResponse>(GetResourceStringWithSideLoadOptionsParam($"{_tickets}/show_many.json?ids={ids.ToCsv()}", sideLoadOptions));
        }

        public async Task<IndividualTicketResponse> CreateTicketAsync(Ticket ticket)
        {
            return await GenericPostAsync<IndividualTicketResponse>($"{_tickets}.json", new { ticket });
        }

        public async Task<JobStatusResponse> CreateManyTicketsAsync(IEnumerable<Ticket> tickets)
        {
            return await GenericPostAsync<JobStatusResponse>($"{_tickets}/{_create_many}.json", new { tickets });
        }

        /// <summary>
        /// In addition to setting normal ticket properties, you can set the following time stamps on the tickets: solved_at, updated_at, and created_at.
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public async Task<IndividualTicketResponse> ImportTicketAsync(TicketImport ticket)
        {
            return await GenericPostAsync<IndividualTicketResponse>($"{_imports}/{_tickets}.json", new { ticket });
        }

        /// <summary>
        /// In addition to setting normal ticket properties, you can set the following time stamps on the tickets: solved_at, updated_at, and created_at.
        /// </summary>
        /// <param name="tickets"></param>
        /// <returns></returns>
        public async Task<JobStatusResponse> BulkImportTicketsAsync(IEnumerable<TicketImport> tickets)
        {
            return await GenericPostAsync<JobStatusResponse>($"{_imports}/{_tickets}/{_create_many}.json", new { tickets });
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
            {
                ticket.Comment = comment;
            }

            return await GenericPutAsync<IndividualTicketResponse>($"{_tickets}/{ticket.Id}.json", new { ticket });
        }

        public async Task<JobStatusResponse> BulkUpdateAsync(IEnumerable<long> ids, BulkUpdate info)
        {
            return await GenericPutAsync<JobStatusResponse>($"{_tickets}/update_many.json?ids={ids.ToCsv()}", new { ticket = info });
        }

        public Task<JobStatusResponse> BatchUpdateAsync(IEnumerable<Ticket> tickets)
        {
            return GenericPutAsync<JobStatusResponse>($"{_tickets}/update_many.json", new { tickets });
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await GenericDeleteAsync($"{_tickets}/{id}.json");
        }

        public async Task<bool> DeleteMultipleAsync(IEnumerable<long> ids)
        {
            return await GenericDeleteAsync($"{_tickets}/destroy_many.json?ids={ids.ToCsv()}");
        }

        /// <summary>
        /// Permanently deletes a soft-deleted ticket
        /// </summary>
        /// <param name="id">Id of ticket to be permanently deleted.</param>
        /// <returns>Boolean response</returns>
        public async Task<bool> DeleteTicketPermanentlyAsync(long id)
        {
            return await GenericDeleteAsync($"{_deletedTickets}/{id}.json");
        }

        public async Task<GroupUserResponse> GetCollaboratorsAsync(long id)
        {
            return await GenericGetAsync<GroupUserResponse>($"{_tickets}/{id}/collaborators.json");
        }

        public async Task<GroupTicketResponse> GetIncidentsAsync(long id)
        {
            return await GenericGetAsync<GroupTicketResponse>($"{_tickets}/{id}/incidents.json");
        }

        public async Task<GroupTicketResponse> GetProblemsAsync()
        {
            return await GenericGetAsync<GroupTicketResponse>("problems.json");
        }

        public async Task<GroupTicketResponse> AutoCompleteProblemsAsync(string text)
        {
            return await GenericPostAsync<GroupTicketResponse>($"problems/autocomplete.json?text={text}");
        }

        public async Task<GroupAuditResponse> GetAuditsAsync(long ticketId)
        {
            return await GenericGetAsync<GroupAuditResponse>($"tickets/{ticketId}/audits.json");
        }

        public async Task<IndividualAuditResponse> GetAuditByIdAsync(long ticketId, long auditId)
        {
            return await GenericGetAsync<IndividualAuditResponse>($"tickets/{ticketId}/audits/{auditId}.json");
        }

        public async Task<bool> MarkAuditAsTrustedAsync(long ticketId, long auditId)
        {
            var resource = $"tickets/{ticketId}/audits/{auditId}/trust.json";
            var res = RunRequestAsync(resource, RequestMethod.Put);
            return await res.ContinueWith(x => x.Result.HttpStatusCode == HttpStatusCode.OK);
        }

        [Obsolete("This has been deprecated due to wrong spelling and sideLoadOptions was ignored. Please use GetIncrementalTicketExportAsync instead")]
        public async Task<GroupTicketExportResponse> GetInrementalTicketExportAsync(DateTimeOffset startTime, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            return await GenericPagedGetAsync<GroupTicketExportResponse>($"{_incremental_export}{startTime.UtcDateTime.GetEpoch()}");
        }

        public async Task<GroupTicketExportResponse> GetIncrementalTicketExportAsync(DateTimeOffset startTime, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            var resource = GetResourceStringWithSideLoadOptionsParam(
                $"{_incremental_export}{startTime.UtcDateTime.GetEpoch()}",
                sideLoadOptions
            );
            return await GenericPagedGetAsync<GroupTicketExportResponse>(resource);
        }

        public async Task<GroupTicketExportResponse> GetIncrementalTicketExportNextPageAsync(string nextPage)
        {
            var resource = nextPage.Replace(ZendeskUrl, string.Empty);

            return await GenericGetAsync<GroupTicketExportResponse>(resource);
        }

        public async Task<GroupTicketFieldResponse> GetTicketFieldsAsync()
        {
            return await GenericGetAsync<GroupTicketFieldResponse>("ticket_fields.json");
        }

        public async Task<IndividualTicketFieldResponse> GetTicketFieldByIdAsync(long id)
        {
            return await GenericGetAsync<IndividualTicketFieldResponse>($"ticket_fields/{id}.json");
        }

        public async Task<IndividualTicketFieldResponse> CreateTicketFieldAsync(TicketField ticketField, bool replaceNameSpacesWithUnderscore = true)
        {
            if (ticketField.CustomFieldOptions != null)
            {
                foreach (var custom in ticketField.CustomFieldOptions)
                {
                    if (replaceNameSpacesWithUnderscore)
                        custom.Name = custom.Name.Replace(' ', '_');
                    custom.Value = custom.Value.Replace(' ', '_');
                }
            }

            var res = GenericPostAsync<IndividualTicketFieldResponse>("ticket_fields.json", new
            {
                ticket_field = ticketField
            });
            return await res;
        }

        public async Task<IndividualTicketFieldResponse> UpdateTicketFieldAsync(TicketField ticketField, bool replaceNameSpacesWithUnderscore = false)
        {
            if (ticketField.CustomFieldOptions != null)
            {
                foreach (var custom in ticketField.CustomFieldOptions)
                {
                    if (replaceNameSpacesWithUnderscore)
                        custom.Name = custom.Name.Replace(' ', '_');
                    custom.Value = custom.Value.Replace(' ', '_');
                }
            }

            return await GenericPutAsync<IndividualTicketFieldResponse>($"ticket_fields/{ticketField.Id}.json", new
            {
                ticket_field = ticketField
            });
        }

        public async Task<bool> DeleteTicketFieldAsync(long id)
        {
            return await GenericDeleteAsync($"ticket_fields/{id}.json");
        }

        public async Task<GroupSuspendedTicketResponse> GetSuspendedTicketsAsync()
        {
            return await GenericGetAsync<GroupSuspendedTicketResponse>("suspended_tickets.json");
        }

        public async Task<IndividualSuspendedTicketResponse> GetSuspendedTicketByIdAsync(long id)
        {
            return await GenericGetAsync<IndividualSuspendedTicketResponse>($"suspended_tickets/{id}.json");
        }

        public async Task<bool> RecoverSuspendedTicketAsync(long id)
        {
            var resource = $"suspended_tickets/{id}/recover.json";
            var res = RunRequestAsync(resource, RequestMethod.Put);
            return await res.ContinueWith(x => x.Result.HttpStatusCode == HttpStatusCode.OK);
        }

        public async Task<bool> RecoverManySuspendedTicketsAsync(IEnumerable<long> ids)
        {
            var resource = $"suspended_tickets/recover_many.json?ids={ids.ToCsv()}";
            var res = RunRequestAsync(resource, RequestMethod.Put);
            return await res.ContinueWith(x => x.Result.HttpStatusCode == HttpStatusCode.OK);
        }

        public async Task<bool> DeleteSuspendedTicketsAsync(long id)
        {
            return await GenericDeleteAsync($"suspended_tickets/{id}.json");
        }

        public async Task<bool> DeleteManySuspendedTicketsAsync(IEnumerable<long> ids)
        {
            return await GenericDeleteAsync($"suspended_tickets/destroy_many.json?ids={ids.ToCsv()}");
        }

        public async Task<GroupTicketFormResponse> GetTicketFormsAsync()
        {
            return await GenericGetAsync<GroupTicketFormResponse>($"{_ticket_forms}.json");
        }

        public async Task<IndividualTicketFormResponse> CreateTicketFormAsync(TicketForm ticketForm)
        {
            return await GenericPostAsync<IndividualTicketFormResponse>($"{_ticket_forms}.json", new { ticket_form = ticketForm });
        }

        public async Task<IndividualTicketFormResponse> GetTicketFormByIdAsync(long id)
        {
            return await GenericGetAsync<IndividualTicketFormResponse>($"{_ticket_forms}/{id}.json");
        }

        public async Task<IndividualTicketFormResponse> UpdateTicketFormAsync(TicketForm ticketForm)
        {
            return await GenericPutAsync<IndividualTicketFormResponse>($"{_ticket_forms}/{ticketForm.Id}.json", ticketForm);
        }

        public async Task<bool> ReorderTicketFormsAsync(long[] orderedTicketFormIds)
        {
            return await GenericPutAsync<bool>($"{_ticket_forms}/reorder.json", new { ticket_form_ids = orderedTicketFormIds });
        }

        public async Task<IndividualTicketFormResponse> CloneTicketFormAsync(long ticketFormId)
        {
            return await GenericPostAsync<IndividualTicketFormResponse>($"{_ticket_forms}/{ticketFormId}/clone.json");
        }

        public async Task<bool> DeleteTicketFormAsync(long id)
        {
            return await GenericDeleteAsync($"{_ticket_forms}/{id}.json");
        }

        /// <summary>
        /// Merges the source tickets in the "ids" list into the target ticket with comments as defined.
        /// </summary>
        /// <param name="targetTicketId">Id of ticket to be merged into.</param>
        /// <param name="sourceTicketIds">List of ids of source tickets to be merged from.</param>
        /// <param name="targetComment">Private comment to add to the target ticket (optional but recommended)</param>
        /// <param name="sourceComment">Private comment to add to the source ticket(s) (optional but recommended)</param>
        /// <param name="targetCommentPublic">Whether comment in target ticket is public or private (default = private)</param>
        /// <param name="sourceCommentPublic">Whether comment in source ticket is public or private (default = private)</param>
        /// <returns>JobStatusResponse</returns>
        public async Task<JobStatusResponse> MergeTicketsAsync(long targetTicketId, IEnumerable<long> sourceTicketIds, string targetComment = "", string sourceComment = "", bool targetCommentPublic = false, bool sourceCommentPublic = false)
        {
            return await GenericPostAsync<JobStatusResponse>(
                $"{_tickets}/{targetTicketId}/merge.json",
                new
                {
                    ids = sourceTicketIds,
                    target_comment = targetComment,
                    source_comment = sourceComment,
                    target_comment_is_public = targetCommentPublic,
                    source_comment_is_public = sourceCommentPublic,
                });
        }

        #region TicketMetrics

        public async Task<GroupTicketMetricResponse> GetAllTicketMetricsAsync(int? perPage = null, int? page = null, TicketSideLoadOptionsEnum sideLoadOptions = TicketSideLoadOptionsEnum.None)
        {
            var resource = GetResourceStringWithSideLoadOptionsParam($"{_ticket_metrics}.json", sideLoadOptions);
            return await GenericPagedGetAsync<GroupTicketMetricResponse>(resource, perPage, page);
        }

        public Task<IndividualTicketMetricResponse> GetTicketMetricsForTicketAsync(long ticket_id)
        {
            return GenericGetAsync<IndividualTicketMetricResponse>($"{_tickets}/{ticket_id}/metrics.json");
        }

        #endregion TicketMetrics

#endif

        private string GetResourceStringWithSideLoadOptionsParam(string resource, TicketSideLoadOptionsEnum sideLoadOptions)
        {
            if (sideLoadOptions != TicketSideLoadOptionsEnum.None)
            {
                if (sideLoadOptions.HasFlag(TicketSideLoadOptionsEnum.None))
                {
                    sideLoadOptions &= ~TicketSideLoadOptionsEnum.None;
                }

                var sideLoads = sideLoadOptions.ToString().ToLower().Replace(" ", "");
                resource += $"{(resource.Contains("?") ? "&" : "?")}include={sideLoads}";
                return resource;
            }

            return resource;
        }
    }
}
