using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZendeskApi_v2.Models.Constants;
using ZendeskApi_v2.Models.Tickets;
using ZendeskApi_v2.Tests.Base;

namespace ZendeskApi_v2.Tests;

[TestFixture]
[Category("Tickets")]
public class TicketTestsPart2 : TestBase
{
    private long customDropDownId;

    [OneTimeSetUp]
    public async Task TestSetUp()
    {
        var dropDown = new TicketField()
        {
            Type = TicketFieldTypes.Tagger,
            Title = "testing",
            Description = "test description",
            TitleInPortal = "Test Tagger",
            CustomFieldOptions = new List<CustomFieldOptions>(),
            Active = true
        };

        dropDown.CustomFieldOptions.Add(new CustomFieldOptions()
        {
            Name = "my work",
            Value = "mywork"
        });

        dropDown.CustomFieldOptions.Add(new CustomFieldOptions()
        {
            Name = "broken",
            Value = "broken"
        });

        var res = await Api.Tickets.CreateTicketFieldAsync(dropDown, true);
        customDropDownId = res.TicketField.Id.Value;
    }

    [OneTimeTearDown]
    public async Task TestCleanUp()
    {
        await Api.Tickets.DeleteTicketFieldAsync(customDropDownId);
    }

    [Test]
    public async Task CanGetTicketsByExternalIdAsync()
    {
        var ticket = new Ticket
        {
            Subject = "my printer is on fire",
            Comment = new Comment { Body = "HELP", Public = true },
            Priority = TicketPriorities.Urgent,
            ExternalId = TEST_EXTERNAL_ID
        };

        var resp1 = await Api.Tickets.CreateTicketAsync(ticket);
        var response = await Api.Tickets.GetTicketsByExternalIdAsync(ticket.ExternalId);
        Assert.Multiple(async () =>
        {
            Assert.That(response.Tickets, Is.Not.Empty);
            Assert.That(await Api.Tickets.DeleteAsync(resp1.Ticket.Id.Value), Is.True);
        });
    }

    [Test]
    public void CanGetTicketsByExternalId()
    {
        var ticket = new Ticket
        {
            Subject = "my printer is on fire",
            Comment = new Comment { Body = "HELP", Public = true },
            Priority = TicketPriorities.Urgent,
            ExternalId = TEST_EXTERNAL_ID
        };

        var resp1 = Api.Tickets.CreateTicket(ticket);
        var response = Api.Tickets.GetTicketsByExternalId(ticket.ExternalId);
        Assert.Multiple(() =>
        {
            Assert.That(response.Tickets, Is.Not.Empty);
            Assert.That(Api.Tickets.Delete(resp1.Ticket.Id.Value), Is.True);
        });
    }

    [Test]
    public async Task CanBatchUpdateTickets()
    {
        var pendingStatus = "pending";

        var tickets = new List<Ticket>{ new Ticket
        {
            Subject = "Batch Update Ticket 1",
            Comment = new Comment { Body = "HELP", Public = true },
            Priority = TicketPriorities.Urgent,
            ExternalId = TEST_EXTERNAL_ID
        },  new Ticket
        {
            Subject = "Batch Update Ticket 2",
            Comment = new Comment { Body = "HELP", Public = true },
            Priority = TicketPriorities.Urgent,
            ExternalId = TEST_EXTERNAL_ID
        }};

        var ticketsToUpdate = new List<Ticket>();

        foreach (var ticket in tickets)
        {
            var createResp = await Api.Tickets.CreateTicketAsync(ticket);
            ticketsToUpdate.Add(createResp.Ticket);
        }

        ticketsToUpdate.ForEach(t => t.Status = pendingStatus);

        var updateResp = await Api.Tickets.BatchUpdateAsync(ticketsToUpdate);

        var job = await Api.JobStatuses.GetJobStatusAsync(updateResp.JobStatus.Id);
        var count = 0;
        while (job.JobStatus.Status.ToLower() != "completed" && count < 10)
        {
            await Task.Delay(1000);
            job = await Api.JobStatuses.GetJobStatusAsync(updateResp.JobStatus.Id);
            count++;
        }

        Assert.That(job.JobStatus.Status.ToLower(), Is.EqualTo("completed"));

        foreach (var r in job.JobStatus.Results)
        {
            var ticket = (await Api.Tickets.GetTicketAsync(r.Id)).Ticket;
            Assert.That(ticket.Status, Is.EqualTo(pendingStatus));
            await Api.Tickets.DeleteAsync(r.Id);
        }
    }

    [Test]
    public async Task CustomDropDownFieldSaveAsync()
    {
        var ticket = new Ticket()
        {
            Subject = "my printer is on fire",
            Comment = new Comment { Body = "HELP" },
            Priority = TicketPriorities.Urgent,
            CustomFields = new List<CustomField> { new CustomField { Id = customDropDownId, Value = "mywork" } },
            ExternalId = TEST_EXTERNAL_ID
        };

        var resp = Api.Tickets.CreateTicket(ticket);
        var newTicket = resp.Ticket;
        Assert.That(ticket.CustomFields[0].Value, Is.EqualTo(newTicket.CustomFields.FirstOrDefault(x => x.Id == customDropDownId).Value));

        newTicket.CustomFields.FirstOrDefault(x => x.Id == customDropDownId).Value = "broken";

        var resp2 = await Api.Tickets.UpdateTicketAsync(newTicket, new Comment { Body = "Update ticket" });
        var updateTicket = resp2.Ticket;
        Assert.Multiple(() =>
        {
            Assert.That(newTicket.CustomFields.FirstOrDefault(x => x.Id == customDropDownId).Value,
                Is.EqualTo(updateTicket.CustomFields.FirstOrDefault(x => x.Id == customDropDownId).Value));

            Assert.That(Api.Tickets.Delete(newTicket.Id.Value), Is.True);
        });
    }

    [Test]
    public async Task CanGetFollowUpIds()
    {
        var ticket = new Ticket { Comment = new Comment { Body = "Original ticket", Public = false } };

        var resp1 = await Api.Tickets.CreateTicketAsync(ticket);

        var closedTicket = resp1.Ticket;

        closedTicket.Status = TicketStatus.Closed;

        await Api.Tickets.UpdateTicketAsync(closedTicket, new Comment { Body = "Closing Original Ticket" });

        var ticket_Followup = new Ticket()
        {
            Subject = "I am the follow up Ticket",
            Comment = new Comment { Body = "I will be linked to the closed ticket" },
            Priority = TicketPriorities.Low,
            ViaFollowupSourceId = closedTicket.Id.Value
        };

        var resp3 = await Api.Tickets.CreateTicketAsync(ticket_Followup);
        var resp4 = Api.Tickets.GetTicket(closedTicket.Id.Value);
        Assert.Multiple(async () =>
        {
            Assert.That(resp3.Ticket.Via.Source.Rel, Is.EqualTo("follow_up"));
            Assert.That(resp4.Ticket.FollowUpIds, Has.Count.EqualTo(1));
            Assert.That(resp3.Ticket.Id, Is.EqualTo(resp4.Ticket.FollowUpIds.ElementAt(0)));

            Assert.That(await Api.Tickets.DeleteAsync(resp3.Ticket.Id.Value), Is.True);
            Assert.That(await Api.Tickets.DeleteAsync(closedTicket.Id.Value), Is.True);
        });
    }
}
