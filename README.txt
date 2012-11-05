This is a full c# wrapper for ZenDesk's api v2. http://developer.zendesk.com/documentation/rest_api/introduction.html

Here are some examples of things you might want to do:

Creating a ticket:
	ZenDeskApi api = new ZenDeskApi(Settings.Site, Settings.Email, Settings.Password); 
	var res = api.Tickets.CreateTicket(new Ticket()
                             {
                                 Subject = "my printer is on fire",
                                 Description = "HELP",
                                 Priority = TicketPriorities.Urgent
                             });
							 
Getting all the tickets in a view
	var myViewId = 1; //ex Id for the All unsolved tickets view
	var res = api.Views.ExecuteView(myViewId);
	

More to come!