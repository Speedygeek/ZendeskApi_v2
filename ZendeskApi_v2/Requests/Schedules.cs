#if ASYNC
using System.Threading.Tasks;
#endif
using System.Collections.Generic;
using System.Linq;
using ZendeskApi_v2.Models.Schedules;
using ZendeskApi_v2.Models.Shared;

namespace ZendeskApi_v2.Requests
{
    public interface ISchedules : ICore
    {
#if SYNC
        GroupScheduleResponse GetAllSchedules();
        IndividualScheduleResponse GetSchedule(long id);
        IndividualScheduleResponse CreateSchedule(Schedule schedule);
        IndividualScheduleResponse UpdateSchedule(Schedule schedule);
        IndividualScheduleWorkWeekResponse UpdateIntervals(long scheduleId, Schedule workweek);
        bool DeleteSchedule(long id);
        GroupScheduleHolidayResponse GetHolidaysByScheduleId(long scheduleId);
        IndividualScheduleHolidayResponse GetHolidayByIdAndScheduleId(long scheduleId, long holidayId);
        IndividualScheduleHolidayResponse CreateHoliday(long scheduleId, Holiday holiday);
        IndividualScheduleHolidayResponse UpdateHoliday(long scheduleId, Holiday holiday);
        bool DeleteHoliday(long scheduleId, long holidayId);
#endif
#if ASYNC
        Task<GroupScheduleResponse> GetAllSchedulesAsync();
        Task<IndividualScheduleResponse> GetScheduleAsync(long id);
        Task<IndividualScheduleResponse> CreateScheduleAsync(Schedule schedule);
        Task<IndividualScheduleResponse> UpdateScheduleAsync(Schedule schedule);
        Task<IndividualScheduleWorkWeekResponse> UpdateIntervalsAsync(long scheduleId, IEnumerable<Interval> intervals);
        Task<bool> DeleteScheduleAsync(long id);
        Task<GroupScheduleHolidayResponse> GetHolidaysByScheduleIdAsync(long scheduleId);
        Task<IndividualScheduleHolidayResponse> GetHolidayByIdAndScheduleIdAsync(long holidayId, long scheduleId);
        Task<IndividualScheduleHolidayResponse> CreateHolidayAsync(long scheduleId, Holiday holiday);
        Task<IndividualScheduleHolidayResponse> UpdateHolidayAsync(long scheduleId, Holiday holiday);
        Task<bool> DeleteHolidayAsync(long scheduleId, long holidayId);
#endif
    }
    public class Schedules : Core, ISchedules
    {
        public Schedules(string yourZendeskUrl, string user, string password, string apiToken, string p_OAuthToken)
            : base(yourZendeskUrl, user, password, apiToken, p_OAuthToken)
        {
        }

#if SYNC
        public GroupScheduleResponse GetAllSchedules()
        {
            return GenericGet<GroupScheduleResponse>("business_hours/schedules.json");
        }

        public IndividualScheduleResponse GetSchedule(long id)
        {
            return GenericGet<IndividualScheduleResponse>(string.Format("business_hours/schedules/{0}.json", id));
        }

        public IndividualScheduleResponse CreateSchedule(Schedule schedule)
        {
            var body = new { schedule };
            return GenericPost<IndividualScheduleResponse>("business_hours/schedules.json", body);
        }

        public IndividualScheduleResponse UpdateSchedule(Schedule schedule)
        {
            var body = new { schedule };
            return GenericPut<IndividualScheduleResponse>(string.Format("business_hours/schedules/{0}.json", schedule.Id), body);
        }

        public bool DeleteSchedule(long id)
        {
            return GenericDelete(string.Format("business_hours/schedules/{0}.json", id));
        }

        public IndividualScheduleWorkWeekResponse UpdateIntervals(long scheduleId, Schedule workweek)
        {
            var body = new { workweek };
            return GenericPut<IndividualScheduleWorkWeekResponse>(string.Format("business_hours/schedules/{0}/workweek.json", scheduleId), body);
        }

        public GroupScheduleHolidayResponse GetHolidaysByScheduleId(long scheduleId)
        {
            return GenericGet<GroupScheduleHolidayResponse>(string.Format("business_hours/schedules/{0}/holidays.json", scheduleId));
        }

        public IndividualScheduleHolidayResponse GetHolidayByIdAndScheduleId(long holidayId, long scheduleId)
        {
            return GenericGet<IndividualScheduleHolidayResponse>(string.Format("business_hours/schedules/{0}/holidays/{1}.json", scheduleId, holidayId));
        }

        public IndividualScheduleHolidayResponse CreateHoliday(long scheduleId, Holiday holiday)
        {
            var body = new { holiday };
            return GenericPost<IndividualScheduleHolidayResponse>(string.Format("business_hours/schedules/{0}/holidays.json", scheduleId), body);
        }

        public IndividualScheduleHolidayResponse UpdateHoliday(long scheduleId, Holiday holiday)
        {
            var body = new { holiday };
            return GenericPut<IndividualScheduleHolidayResponse>(string.Format("business_hours/schedules/{0}/holidays/{1}.json", scheduleId, holiday.Id), body);
        }

        public bool DeleteHoliday(long scheduleId, long holidayId)
        {
            return GenericDelete(string.Format("business_hours/schedules/{0}/holidays/{1}.json", scheduleId, holidayId));
        }
#endif

#if ASYNC
        public async Task<GroupScheduleResponse> GetAllSchedulesAsync()
        {
            return await GenericGetAsync<GroupScheduleResponse>("business_hours/schedules.json");
        }

        public async Task<IndividualScheduleResponse> GetScheduleAsync(long id)
        {
            return await GenericGetAsync<IndividualScheduleResponse>(string.Format("business_hours/schedules/{0}.json", id));
        }

        public async Task<IndividualScheduleResponse> CreateScheduleAsync(Schedule schedule)
        {
            var body = new { schedule };
            return await GenericPostAsync<IndividualScheduleResponse>("business_hours/schedules.json", body);
        }

        public async Task<IndividualScheduleResponse> UpdateScheduleAsync(Schedule schedule)
        {
            var body = new { schedule };
            return await GenericPutAsync<IndividualScheduleResponse>(string.Format("business_hours/schedules/{0}.json", schedule.Id), body);
        }

        public async Task<bool> DeleteScheduleAsync(long id)
        {
            return await GenericDeleteAsync(string.Format("business_hours/schedules/{0}.json", id));
        }

        public async Task<IndividualScheduleWorkWeekResponse> UpdateIntervalsAsync(long scheduleId, IEnumerable<Interval> intervals)
        {
            var body = new { intervals };
            return await GenericPutAsync<IndividualScheduleWorkWeekResponse>(string.Format("business_hours/schedules/{0}/workweek.json", scheduleId), body);
        }

        public async Task<GroupScheduleHolidayResponse> GetHolidaysByScheduleIdAsync(long scheduleId)
        {
            return await GenericGetAsync<GroupScheduleHolidayResponse>(string.Format("business_hours/schedules/{0}/holidays.json", scheduleId));
        }

        public async Task<IndividualScheduleHolidayResponse> GetHolidayByIdAndScheduleIdAsync(long holidayId, long scheduleId)
        {
            return await GenericGetAsync<IndividualScheduleHolidayResponse>(string.Format("business_hours/schedules/{0}/holidays/{1}.json", scheduleId, holidayId));
        }

        public async Task<IndividualScheduleHolidayResponse> CreateHolidayAsync(long scheduleId, Holiday holiday)
        {
            var body = new { holiday };
            return await GenericPostAsync<IndividualScheduleHolidayResponse>(string.Format("business_hours/schedules/{0}/holidays.json", scheduleId), body);
        }

        public async Task<IndividualScheduleHolidayResponse> UpdateHolidayAsync(long scheduleId, Holiday holiday)
        {
            var body = new { holiday };
            return await GenericPutAsync<IndividualScheduleHolidayResponse>(string.Format("business_hours/schedules/{0}/holidays/{1}.json", scheduleId, holiday.Id), body);
        }

        public async Task<bool> DeleteHolidayAsync(long scheduleId, long holidayId)
        {
            return await GenericDeleteAsync(string.Format("business_hours/schedules/{0}/holidays/{1}.json", scheduleId, holidayId));
        }
#endif
    }
}