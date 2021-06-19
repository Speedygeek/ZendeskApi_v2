using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using ZendeskApi_v2;
using ZendeskApi_v2.Models.Schedules;

namespace Tests
{
    [TestFixture]
    public class ScheduleTests
    {
        private readonly ZendeskApi api = new ZendeskApi(Settings.Site, Settings.AdminEmail, Settings.AdminPassword);

        [OneTimeSetUp]
        public void Init()
        {
            var schedules = api.Schedules.GetAllSchedules();
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

        [OneTimeTearDown]
        public void Dispose()
        {
            var schedules = api.Schedules.GetAllSchedules();
            if (schedules != null)
            {
                foreach (var schedule in schedules.Schedules.Where(o => o.Name.Contains("Master Test Schedule")))
                {
                    api.Schedules.DeleteSchedule(schedule.Id.Value);
                }
            }
        }

        [Test]
        public void CanGetSchedules()
        {
            var res = api.Schedules.GetAllSchedules();
            Assert.That(res.Schedules.Count, Is.GreaterThan(0));

            var org = api.Schedules.GetSchedule(res.Schedules[0].Id.Value);
            Assert.That(res.Schedules[0].Id, Is.EqualTo(org.Schedule.Id));
        }

        [Test]
        public void CanCreateUpdateAndDeleteSchedule()
        {
            var res = api.Schedules.CreateSchedule(new Schedule()
            {
                Name = "Test Schedule",
                TimeZone = "Pacific Time (US & Canada)"
            });

            Assert.That(res.Schedule.Id, Is.GreaterThan(0));

            res.Schedule.TimeZone = "Central Time (US & Canada)";
            var update = api.Schedules.UpdateSchedule(res.Schedule);
            Assert.That(res.Schedule.TimeZone, Is.EqualTo(update.Schedule.TimeZone));

            Assert.That(api.Schedules.DeleteSchedule(res.Schedule.Id.Value), Is.True);
        }

        [Test]
        public void CanUpdateIntervals()
        {
            var res = api.Schedules.CreateSchedule(new Schedule()
            {
                Name = "Test Schedule",
                TimeZone = "Pacific Time (US & Canada)"
            });

            Assert.That(res.Schedule.Id, Is.GreaterThan(0));

            var work = new WorkWeek
            {
                Intervals = res.Schedule.Intervals
            };

            work.Intervals[0].StartTime = 1860;
            work.Intervals[0].EndTime = 2460;
            var update = api.Schedules.UpdateIntervals(res.Schedule.Id.Value, work);

            Assert.That(update.WorkWeek.Intervals.Count, Is.GreaterThan(0));

            Assert.That(update.WorkWeek.Intervals[0].EndTime, Is.EqualTo(work.Intervals[0].EndTime));
            Assert.That(api.Schedules.DeleteSchedule(res.Schedule.Id.Value), Is.True);
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
                StartDate = DateTimeOffset.UtcNow.AddDays(1).Date,
                EndDate = DateTimeOffset.UtcNow.AddDays(2).Date
            });

            Assert.That(res2.Holiday.Id, Is.GreaterThan(0));

            res2.Holiday.EndDate = DateTimeOffset.UtcNow.AddDays(3).Date;
            var update = api.Schedules.UpdateHoliday(res.Schedule.Id.Value, res2.Holiday);
            Assert.That(res2.Holiday.Name, Is.EqualTo(update.Holiday.Name));
            Assert.That(res2.Holiday.EndDate, Is.EqualTo(update.Holiday.EndDate));

            Assert.That(api.Schedules.DeleteHoliday(res.Schedule.Id.Value, res2.Holiday.Id.Value), Is.True);
            Assert.That(api.Schedules.DeleteSchedule(res.Schedule.Id.Value), Is.True);
        }
    }
}