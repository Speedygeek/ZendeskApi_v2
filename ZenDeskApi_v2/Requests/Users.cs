using System;
using System.Collections.Generic;
using System.Net;
using RestSharp;
using ZenDeskApi_v2.Extensions;
using ZenDeskApi_v2.Models.Shared;
using ZenDeskApi_v2.Models.Tickets;
using ZenDeskApi_v2.Models.Users;
using ZenDeskApi_v2.Models.Views;
using ZenDeskApi_v2.Models.Views.Executed;
using User = ZenDeskApi_v2.Models.Users.User;

namespace ZenDeskApi_v2.Requests
{
    public class Users : Core
    {        
        public Users(string yourZenDeskUrl, string user, string password)
            : base(yourZenDeskUrl, user, password)
        {
        }

        public IndividualUserResponse GetCurrentUser()
        {
            return GenericGet<IndividualUserResponse>("users/me.json");
        }

        public GroupUserResponse GetAllUsers()
        {
            return GenericGet<GroupUserResponse>("users.json");
        }

        public IndividualUserResponse GetUser(int id)
        {
            return GenericGet<IndividualUserResponse>(string.Format("users/{0}.json", id));
        }

        public GroupUserResponse SearchByEmail(string email)
        {
            return GenericGet<GroupUserResponse>(string.Format("users/search.json?query={0}", email));
        }

        public GroupUserResponse GetUsersInGroup(int id)
        {
            return GenericGet<GroupUserResponse>(string.Format("groups/{0}/users.json", id));
        }

        public GroupUserResponse GetUsersInOrganization(int id)
        {
            return GenericGet<GroupUserResponse>(string.Format("organizations/{0}/users.json", id));
        }

        public IndividualUserResponse CreateUser(User user)
        {
            var body = new { user = user };
            return GenericPost<IndividualUserResponse, object>("users.json", body);
        }

        public JobStatusResult BulkCreateUsers(List<User> users)
        {
            var body = new {users = users};
            return GenericPost<JobStatusResult, object>("users/create_many.json", body);
        }

        public IndividualUserResponse SuspendUser(int id)
        {
            var body = new {user = new {suspended = true}};
            return GenericPut<IndividualUserResponse, object>(string.Format("users/{0}.json", id), body);
        }

        public IndividualUserResponse UpdateUser(User user)
        {
            var body = new { user = user };
            return GenericPut<IndividualUserResponse, object>(string.Format("users/{0}.json", user.Id), body);
        }

        public bool DeleteUser(int id)
        {
            return GenericDelete(string.Format("users/{0}.json", id));
        }
    }
}
