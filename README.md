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

Remote Authentication
--------------
As an added bonus if you are using single sign on you can also generate a link to log a user in. As per the [docs](http://www.zendesk.com/support/api/remote-authentication) you should be able to use this without sso, but since the authentication token is under Settings -> Security -> Single Sign-On, I'm not sure how else you can get the token. Here is an example though.

	var loginUrl = api.GetLoginUrl("Name", "Email", "Your Auth Token", "optional forward to url");
	
A good example of how to use this would be, say you want a user to be able to see their tickets on your business's website. Using this you could link directly to the ticket from your site and with one click the user could be logged into Zendesk and seeing their ticket. Kind of nice :)