Zendesk Api V2 
==============
[![Build status](https://ci.appveyor.com/api/projects/status/6ve1ey4x7ctd5029?svg=true)](https://ci.appveyor.com/project/mozts2005/zendeskapi-v2)

Hello 
--------------
Hello All, 

I have taken over the project from Eric. First I would like to thank Eric and all the others that have given so much of their personal time. I hope to work with all of you over time. 

In the short term I am planning on adding automated build to the project. This will allow test to be run when a pull request is submitted. 

I will be working over the next few days to get the issues filtered and tagged. Once this is done I will be able to give an accurate road map of the changes and updates I hope to make with the community. 

If you have any questions feel free to contact me here on github

Thanks, 
Beth  

About
--------------
This is a full c# wrapper for Zendesk's api v2. http://developer.zendesk.com/documentation/rest_api/introduction.html

I am sorry to say but I will no longer be able to support ths api but here are some examples of things you might want to do, for even more examples check out the "Tests" folder above. Everything is tested so there are plenty of examples! Also there is a live demo here: http://zendeskcdemo.apphb.com/

This code is released under the [Apache Version 2 License](http://www.apache.org/licenses/LICENSE-2.0.html).

Nov 2014 Possible Breaking Changes: Dates now use DateTimeOffset instead of string
--------------
Please look at [this pull request](https://github.com/eneifert/ZendeskApi_v2/pull/101) for more information.

Nov 2014 Help Center Api
--------------
Thanks to [PeteAllen](https://github.com/peteallen) there is now HelpCenter Support for the Api! You can find out more [here](https://github.com/eneifert/ZendeskApi_v2/pull/107)

FEB 2014 - Ticket Description Changes
--------------
As of Feb 2014 the Ticket.Description field goes from being mandatory to read only. So when you want to create a ticket you must now set the description by using the Comment.Body field instead. For example, from now on create tickets this way:

```C#
var ticket = api.Tickets.CreateTicket(new Ticket()
                             {
                                 Subject = "my printer is on fire",
                                 Comment = new Comment(){Body = "HELP"},
                                 Priority = TicketPriorities.Urgent
                             });
```

NOT this way:
```C#
var ticket = api.Tickets.CreateTicket(new Ticket()
                             {
                                 Subject = "my printer is on fire",
                                 Description = "HELP", //THIS WILL NOT WORK
                                 Priority = TicketPriorities.Urgent
                             });
```

See [here](http://developer.zendesk.com/documentation/rest_api/changes_roadmap.html#january-2,-2014) for more information. 

Setting Up:
--------------
You can instantiate the wrapper using the email and password or token. To set up using the password do it like this.
```C#
	ZendeskApi api = new ZendeskApi(https://{yoursite}.zendesk.com/api/v2, "your@email.com", "password"); 
```

To set up using the token, add the token as the fourth param. Note the third paramter (password) can be blank or not. Here is an example.
```C#
	ZendeskApi api = new ZendeskApi(https://{yoursite}.zendesk.com/api/v2, "your@email.com", "", "{your token here}"); 
```

Creating a ticket:
--------------

```C#
	ZendeskApi api = new ZendeskApi(https://{yoursite}.zendesk.com/api/v2, "your@email.com", "password"); 
	var ticket = api.Tickets.CreateTicket(new Ticket()
                             {
                                 Subject = "my printer is on fire",
                                 Comment = new Comment(){Body = "HELP"},
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
As an added bonus you can also generate a link to log a user in. Make sure you have "Token Access" enabled at Settings -> Channels -> Api. *Note if you are using Single Sign On the token would be in Admin -> Settings -> Security -> Single Sign-On.

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


Ticket Exporting
--------------
There is a Windows console application for keeping a local copy of your Zendesk tickets updated in an application-managed SQLite database, with optional export to csv availible [here](https://github.com/ritterim/zendesk-ticket-exporter). Thanks to Ken Dale!

For any API related questions please email api@zendesk.com, thanks!
