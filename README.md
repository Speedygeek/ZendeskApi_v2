Zendesk Api V2
==============

This is a full c# wrapper for Zendesk's api v2. http://developer.zendesk.com/documentation/rest_api/introduction.html

Here are some examples of things you might want to do, but for even more examples check out the "Tests" folder above. Everything is tested so there are plenty of examples!

Creating a ticket:
--------------
	ZendeskApi api = new ZendeskApi(https://{yoursite}.zendesk.com/api/v2, "your@email.com", "password"); 
	var res = api.Tickets.CreateTicket(new Ticket()
                             {
                                 Subject = "my printer is on fire",
                                 Description = "HELP",
                                 Priority = TicketPriorities.Urgent
                             });
							 
Getting all the tickets in a view
--------------
	var myViewId = 1; //ex Id for the All unsolved tickets view
	var res = api.Views.ExecuteView(myViewId);
	

Uploading an attachment to a ticket
--------------
1st we need to upload the attachment.

	var attachment = api.Attachments.UploadAttachment(new ZenFile()
            {
                ContentType = "text/plain",
                FileName = "testupload.txt",
                FileData = File.ReadAllBytes("testupload.txt")
            });

Now we add the attachment token to a ticket. 
			
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
	
Updating a ticket works the same way, just create a new comment and set the Uploads token. Note you can add more than one attachment to a comment.

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