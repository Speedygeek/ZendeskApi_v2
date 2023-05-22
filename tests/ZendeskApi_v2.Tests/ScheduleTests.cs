using NUnit.Framework;
using System;
using System.Linq;
using ZendeskApi_v2.Models.Schedules;
using ZendeskApi_v2.Tests.Base;

namespace ZendeskApi_v2.Tests;

[TestFixture]
public class ScheduleTests : TestBase
{
    [OneTimeSetUp]
    public void Setup()
    {
        var schedules = Api.Schedules.GetAllSchedules();
        if (schedules != null)
        {
            foreach (var schedule in schedules.Schedules.Where(o => o.Name.Contains("Test Schedule")))
            {
                Api.Schedules.DeleteSchedule(schedule.Id.Value);
            }
        }

        var res = Api.Schedules.CreateSchedule(new Schedule()
        {
            Name = "Root Test Schedule",
            TimeZone = "Pacific Time (US & Canada)"
        });
    }

    [OneTimeTearDown]
    public void Dispose()
    {
        var schedules = Api.Schedules.GetAllSchedules();
        if (schedules != null)
        {
            foreach (var schedule in schedules.Schedules.Where(o => o.Name.Contains("Root Test Schedule")))
            {
                Api.Schedules.DeleteSchedule(schedule.Id.Value);
            }
        }
    }

    [Test]
    public void CanGetSchedules()
    {
        var res = Api.Schedules.GetAllSchedules();
        Assert.That(res.Schedules, Is.Not.Empty);

        var org = Api.Schedules.GetSchedule(res.Schedules[0].Id.Value);
        Assert.That(res.Schedules[0].Id, Is.EqualTo(org.Schedule.Id));
    }

    [Test]
    public void CanCreateUpdateAndDeleteSchedule()
    {
        var res = Api.Schedules.CreateSchedule(new Schedule()
        {
            Name = "Test Schedule",
            TimeZone = "Pacific Time (US & Canada)"
        });

        Assert.That(res.Schedule.Id, Is.GreaterThan(0));

        res.Schedule.TimeZone = "Central Time (US & Canada)";
        var update = Api.Schedules.UpdateSchedule(res.Schedule);
        Assert.Multiple(() =>
        {
            Assert.That(res.Schedule.TimeZone, Is.EqualTo(update.Schedule.TimeZone));

            Assert.That(Api.Schedules.DeleteSchedule(res.Schedule.Id.Value), Is.True);
        });
    }

    [Test]
    public void CanUpdateIntervals()
    {
        var res = Api.Schedules.CreateSchedule(new Schedule()
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
        var update = Api.Schedules.UpdateIntervals(res.Schedule.Id.Value, work);

        Assert.That(update.WorkWeek.Intervals, Is.Not.Empty);
        Assert.Multiple(() =>
        {
            Assert.That(update.WorkWeek.Intervals[0].EndTime, Is.EqualTo(work.Intervals[0].EndTime));
            Assert.That(Api.Schedules.DeleteSchedule(res.Schedule.Id.Value), Is.True);
        });
    }

    [Test]
    public void CanCreateUpdateAndDeleteHoliday()
    {
        var res = Api.Schedules.CreateSchedule(new Schedule()
        {
            Name = "Test Schedule",
            TimeZone = "Pacific Time (US & Canada)"
        });

        var res2 = Api.Schedules.CreateHoliday(res.Schedule.Id.Value, new Holiday()
        {
            Name = "Test Holiday",
            StartDate = DateTimeOffset.UtcNow.AddDays(1).Date,
            EndDate = DateTimeOffset.UtcNow.AddDays(2).Date
        });

        Assert.That(res2.Holiday.Id, Is.GreaterThan(0));

        res2.Holiday.EndDate = DateTimeOffset.UtcNow.AddDays(3).Date;
        var update = Api.Schedules.UpdateHoliday(res.Schedule.Id.Value, res2.Holiday);
        Assert.Multiple(() =>
        {
            Assert.That(res2.Holiday.Name, Is.EqualTo(update.Holiday.Name));
            Assert.That(res2.Holiday.EndDate, Is.EqualTo(update.Holiday.EndDate));

            Assert.That(Api.Schedules.DeleteHoliday(res.Schedule.Id.Value, res2.Holiday.Id.Value), Is.True);
            Assert.That(Api.Schedules.DeleteSchedule(res.Schedule.Id.Value), Is.True);
        });
    }
}