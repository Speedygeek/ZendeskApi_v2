Zendesk Api V2
==============

This is a full c# wrapper for Zendesk's api v2. http://developer.zendesk.com/documentation/rest_api/introduction.html

Here are some examples of things you might want to do, but for even more examples check out the "Tests" folder above. Everything is tested so there are plenty of examples! Also there is a live demo here: http://zendeskcdemo.apphb.com/



Creating a ticket:
--------------

```C#
	ZendeskApi api = new ZendeskApi(https://{yoursite}.zendesk.com/api/v2, "your@email.com", "password"); 
	var ticket = api.Tickets.CreateTicket(new Ticket()
                             {
                                 Subject = "my printer is on fire",
                                 Description = "HELP",
                                 Priority = TicketPriorities.Urgent
                             });
```							 
							 
Getting all the tickets in a view
--------------

```C#
	var myViewId = 1; //ex Id for the All unsolved tickets view
	var tickets = api.Views.ExecuteView(myViewId);
```
	

Uploading an attachment to a ticket
--------------
1st we need to upload the attachment.

```C#
	var attachment = api.Attachments.UploadAttachment(new ZenFile()
            {
                ContentType = "text/plain",
                FileName = "testupload.txt",
                FileData = File.ReadAllBytes("testupload.txt")
            });
```
			
Now we add the attachment token to a ticket. 
	
```C#	
	var ticket = new Ticket()
            {
                Subject = "testing attachments",
                Description = "test attachment",
                Priority = TicketPriorities.Normal,
                Comment = new Comment() { 
                    Body = "comments are required for attachments", 
                    Public = true, 
                    Uploads = new List<string>() { attachment.Token } \\Add the attachment token here
                },
            };

    var t1 = api.Tickets.CreateTicket(ticket);
```	
	
Updating a ticket works the same way, just create a new comment and set the Uploads token. Note you can add more than one attachment to a comment.

Paging
--------------
By default Zendesk will only return you 100 items at a time. But when there are more than that you can use the "NextPage" and "PreviousPage" urls to get more. For example lets say we call GetAllTickets and want to get the second page as well, here's how you would do it.
	
```C#	
	ZendeskApi api = new ZendeskApi(Settings.Site, Settings.Email, Settings.Password);
	var tickets = api.Tickets.GetAllTickets();
	if(!string.IsNullOrEmpty(tickets.NextPage))
    {
        var page2 = api.Tickets.GetByPageUrl<GroupTicketResponse>(tickets.NextPage);
    }
```	
	
*Note that the type in GetByPageUrl<T> must be the same type as returned by the original collection. So in this case api.Tickets.GetAllTickets() returns a GroupTicketResponse, so that is the type we want to use.
	
Remote Authentication
--------------
As an added bonus you can also generate a link to log a user in. Make sure you have "Token Access" enabled at Settings -> Channels -> Api. *Note if you are ussing Single Sign On the token would be in Admin -> Settings -> Security -> Single Sign-On.

```C#
	var loginUrl = api.GetLoginUrl("Name", "Email", "Your Auth Token", "optional forward to url");
```	
	
A good example of how to use this would be, say you want a user to be able to see their tickets on your business's website. Using this you could link directly to the ticket from your site and with one click the user could be logged into Zendesk and seeing their ticket. Kind of nice :)

Summary
--------------
All of the api calls are under the following properties. And these properties pretty much correspond to the [Zendesk docs](http://developer.zendesk.com/documentation/rest_api/tickets.html) except for things like "Ticket Audits" which are grouped with all of the other ticket methods.
- Tickets
- Attachments
- Views
- Users
- Requests
- Groups
- CustomAgentRoles
- Organizations
- Search
- Tags
- Forums
- Categories
- Topics
- AccountsAndActivity
- JobStatuses
- Locales
- Macros
- SatisfactionRatings
- SharingAgreements
- Triggers

ASYNC Support
--------------
As of 12/4/2012 the project now has complete async support for net4.0 + and for Portable Class Libraries. The PCL synchronous support was removed since windows phone is not meant to have synchronous tasks like this. Also note that you must have nuget 2.1+ to get the portable class library version!


For any API related questions please email api@zendesk.com, thanks!