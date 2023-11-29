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
        bool DeleteSchedule(long id);
        IndividualScheduleWorkWeekResponse UpdateIntervals(long scheduleId, WorkWeek workweek);
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
        Task<bool> DeleteScheduleAsync(long id);
        Task<IndividualScheduleWorkWeekResponse> UpdateIntervalsAsync(long scheduleId, WorkWeek workweek);
        Task<GroupScheduleHolidayResponse> GetHolidaysByScheduleIdAsync(long scheduleId);
        Task<IndividualScheduleHolidayResponse> GetHolidayByIdAndScheduleIdAsync(long holidayId, long scheduleId);
        Task<IndividualScheduleHolidayResponse> CreateHolidayAsync(long scheduleId, Holiday holiday);
        Task<IndividualScheduleHolidayResponse> UpdateHolidayAsync(long scheduleId, Holiday holiday);
        Task<bool> DeleteHolidayAsync(long scheduleId, long holidayId);
#endif
    }
    public class Schedules : Core, ISchedules
    {
        public Schedules(string yourZendeskUrl, string user, string password, string apiToken, string p_OAuthToken, Dictionary<string,string> customHeaders)
            : base(yourZendeskUrl, user, password, apiToken, p_OAuthToken, customHeaders)
        {
        }

#if SYNC
        public GroupScheduleResponse GetAllSchedules()
        {
            return GenericGet<GroupScheduleResponse>("business_hours/schedules.json");
        }

        public IndividualScheduleResponse GetSchedule(long id)
        {
            return GenericGet<IndividualScheduleResponse>($"business_hours/schedules/{id}.json");
        }

        public IndividualScheduleResponse CreateSchedule(Schedule schedule)
        {
            var body = new { schedule };
            return GenericPost<IndividualScheduleResponse>("business_hours/schedules.json", body);
        }

        public IndividualScheduleResponse UpdateSchedule(Schedule schedule)
        {
            var body = new { schedule };
            return GenericPut<IndividualScheduleResponse>($"business_hours/schedules/{schedule.Id}.json", body);
        }

        public bool DeleteSchedule(long id)
        {
            return GenericDelete($"business_hours/schedules/{id}.json");
        }

        public IndividualScheduleWorkWeekResponse UpdateIntervals(long scheduleId, WorkWeek workweek)
        {
            var body = new { workweek };
            return GenericPut<IndividualScheduleWorkWeekResponse>($"business_hours/schedules/{scheduleId}/workweek.json", body);
        }

        public GroupScheduleHolidayResponse GetHolidaysByScheduleId(long scheduleId)
        {
            return GenericGet<GroupScheduleHolidayResponse>($"business_hours/schedules/{scheduleId}/holidays.json");
        }

        public IndividualScheduleHolidayResponse GetHolidayByIdAndScheduleId(long holidayId, long scheduleId)
        {
            return GenericGet<IndividualScheduleHolidayResponse>($"business_hours/schedules/{scheduleId}/holidays/{holidayId}.json");
        }

        public IndividualScheduleHolidayResponse CreateHoliday(long scheduleId, Holiday holiday)
        {
            return GenericPost<IndividualScheduleHolidayResponse>($"business_hours/schedules/{scheduleId}/holidays.json", new { holiday });
        }

        public IndividualScheduleHolidayResponse UpdateHoliday(long scheduleId, Holiday holiday)
        {
            return GenericPut<IndividualScheduleHolidayResponse>($"business_hours/schedules/{scheduleId}/holidays/{holiday.Id}.json", new { holiday });
        }

        public bool DeleteHoliday(long scheduleId, long holidayId)
        {
            return GenericDelete($"business_hours/schedules/{scheduleId}/holidays/{holidayId}.json");
        }
#endif

#if ASYNC
        public async Task<GroupScheduleResponse> GetAllSchedulesAsync()
        {
            return await GenericGetAsync<GroupScheduleResponse>("business_hours/schedules.json");
        }

        public async Task<IndividualScheduleResponse> GetScheduleAsync(long id)
        {
            return await GenericGetAsync<IndividualScheduleResponse>($"business_hours/schedules/{id}.json");
        }

        public async Task<IndividualScheduleResponse> CreateScheduleAsync(Schedule schedule)
        {
            var body = new { schedule };
            return await GenericPostAsync<IndividualScheduleResponse>("business_hours/schedules.json", body);
        }

        public async Task<IndividualScheduleResponse> UpdateScheduleAsync(Schedule schedule)
        {
            var body = new { schedule };
            return await GenericPutAsync<IndividualScheduleResponse>($"business_hours/schedules/{schedule.Id}.json", body);
        }

        public async Task<bool> DeleteScheduleAsync(long id)
        {
            return await GenericDeleteAsync($"business_hours/schedules/{id}.json");
        }

        public async Task<IndividualScheduleWorkWeekResponse> UpdateIntervalsAsync(long scheduleId, WorkWeek workweek)
        {
            var body = new { workweek };
            return await GenericPutAsync<IndividualScheduleWorkWeekResponse>($"business_hours/schedules/{scheduleId}/workweek.json", body);
        }

        public async Task<GroupScheduleHolidayResponse> GetHolidaysByScheduleIdAsync(long scheduleId)
        {
            return await GenericGetAsync<GroupScheduleHolidayResponse>($"business_hours/schedules/{scheduleId}/holidays.json");
        }

        public async Task<IndividualScheduleHolidayResponse> GetHolidayByIdAndScheduleIdAsync(long holidayId, long scheduleId)
        {
            return await GenericGetAsync<IndividualScheduleHolidayResponse>($"business_hours/schedules/{scheduleId}/holidays/{holidayId}.json");
        }

        public async Task<IndividualScheduleHolidayResponse> CreateHolidayAsync(long scheduleId, Holiday holiday)
        {
            var body = new { holiday };
            return await GenericPostAsync<IndividualScheduleHolidayResponse>($"business_hours/schedules/{scheduleId}/holidays.json", body);
        }

        public async Task<IndividualScheduleHolidayResponse> UpdateHolidayAsync(long scheduleId, Holiday holiday)
        {
            var body = new { holiday };
            return await GenericPutAsync<IndividualScheduleHolidayResponse>($"business_hours/schedules/{scheduleId}/holidays/{holiday.Id}.json", body);
        }

        public async Task<bool> DeleteHolidayAsync(long scheduleId, long holidayId)
        {
            return await GenericDeleteAsync($"business_hours/schedules/{scheduleId}/holidays/{holidayId}.json");
        }
#endif
    }
}
