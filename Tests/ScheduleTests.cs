using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using ZendeskApi_v2;
using ZendeskApi_v2.Models.Schedules;

namespace Tests
{
    [TestFixture]
    public class ScheduleTests
    {
        private ZendeskApi api = new ZendeskApi(Settings.Site, Settings.Email, Settings.Password);
        [TestFixtureSetUp]
        public void Init()
        {
            var schedules = api.Schedules.GetSchedules();
            if (schedules != null)
            {
                foreach (var schedule in schedules.Schedules.Where(o => o.Name.Contains("Test Schedule")))
                {
                    api.Schedules.DeleteSchedule(schedule.Id.Value);
                }
            }

            var res = api.Schedules.CreateSchedule(new Schedule()
            {
                Name = "Master Test Schedule",
                TimeZone = "Pacific Time (US & Canada)"
            });
        }

        [TestFixtureTearDown]
        public void Dispose()
        {
            var schedules = api.Schedules.GetSchedules();
            if (schedules != null)
            {
                foreach (var schedule in schedules.Schedules.Where(o => o.Name.Contains("Master Test Schedule")))
                {
                    api.Schedules.DeleteSchedule(schedule.Id.Value);
                }
            }
        }

        //[Test]
        //public void CanGetSchedules()
        //{
        //    var res = api.Schedules.GetSchedules();
        //    Assert.Greater(res.Count, 0);

        //    var org = api.Schedules.GetSchedule(res.Schedules[0].Id.Value);
        //    Assert.AreEqual(org.Schedule.Id, res.Schedules[0].Id);
        //}

        [Test]
        public void CanCreateUpdateAndDeleteSchedule()
        {
            var res = api.Schedules.CreateSchedule(new Schedule()
            {
                Name = "Test Schedule",
                TimeZone = "Pacific Time (US & Canada)"
            });

            Assert.Greater(res.Schedule.Id, 0);

            res.Schedule.TimeZone = "Central Time (US & Canada)";
            var update = api.Schedules.UpdateSchedule(res.Schedule);
            Assert.AreEqual(update.Schedule.Name, res.Schedule.Name);

            Assert.True(api.Schedules.DeleteSchedule(res.Schedule.Id.Value));
        }

        [Test]
        public void CanUpdateIntervals()
        {
            var res = api.Schedules.CreateSchedule(new Schedule()
            {
                Name = "Test Schedule",
                TimeZone = "Pacific Time (US & Canada)"
            });

            Assert.Greater(res.Schedule.Id, 0);

            res.Schedule.Intervals[0].StartTime = 1860;
            res.Schedule.Intervals[0].EndTime = 2460;
            var update = api.Schedules.UpdateIntervals(res.Schedule.Id.Value, res.Schedule);

            Assert.Greater(update.WorkWeek.Intervals.Count, 0);

            Assert.AreEqual(update.WorkWeek.Intervals[0].EndTime, res.Schedule.Intervals[0].EndTime);
            Assert.True(api.Schedules.DeleteSchedule(res.Schedule.Id.Value));
        }

[Test]
        public void CanCreateUpdateAndDeleteHoliday()
        {
            var res = api.Schedules.CreateSchedule(new Schedule()
            {
                Name = "Test Schedule",
                TimeZone = "Pacific Time (US & Canada)"
            });

            var res2 = api.Schedules.CreateHoliday(res.Schedule.Id.Value, new Holiday()
            {
                Name = "Test Holiday",
                StartDate = 35,
                EndDate = 35
            });

            Assert.Greater(res2.Holiday.Id, 0);

            res2.Holiday.EndDate = 36;
            var update = api.Schedules.UpdateHoliday(res.Schedule.Id.Value, res2.Holiday);
            Assert.AreEqual(update.Holiday.Name, res2.Holiday.Name);

            Assert.True(api.Schedules.DeleteHoliday(res.Schedule.Id.Value, res2.Holiday.Id.Value));
            Assert.True(api.Schedules.DeleteSchedule(res.Schedule.Id.Value));
        }
    }
}