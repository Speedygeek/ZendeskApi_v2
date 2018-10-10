using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi_v2.Models.Tickets;
using ZendeskApi_v2.Models.Users;

namespace ZendeskApi_v2.Example
{
    class Program
    {
        async static Task Main(string[] args)
        {
            var userEmailToSearchFor = "eneif123@yahoo.com";

            var userName = "csharpzendeskapi1234@gmail.com"; // the user that will be logging in the API aka the call center staff 
            var userPassword = "&H3n!0q^3OjDLdm";
            var companySubDomain = "csharpapi"; // sub-domain for the account with Zendesk
            var api = new ZendeskApi(companySubDomain, userName, userPassword);
            var helper = new ZendeskHelper(api);

            var tickets = await helper.GetTickets(userEmailToSearchFor);

            var count = 0;
            foreach (var ticket in tickets)
            {
                Console.WriteLine($"{count}: ticket id: {ticket?.Id}, Assignee Id: {ticket?.AssigneeId}, Requester Id: {ticket?.RequesterId}");
                var comments = await helper.GetComments(ticket);
                foreach (var comment in comments)
                {
                    Console.WriteLine($"commnet ID: {comment.Id}");
                    foreach (var attachment in comment.Attachments)
                    {
                        Console.WriteLine($"attechemnt File Name: {attachment.FileName}");
                    }
                }
                count++;
            }

            System.Console.ReadKey();
        }
    }
}
