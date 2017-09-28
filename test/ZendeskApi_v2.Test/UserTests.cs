using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using ZendeskApi_v2;
using ZendeskApi_v2.Models.Constants;
using ZendeskApi_v2.Models.Users;
using ZendeskApi_v2.Requests;
using ZendeskApi_v2.Models.Shared;
using System.IO;

namespace Tests
{
    [TestFixture]
    [Category("Users")]
    public class UserTests
    {
        ZendeskApi api = new ZendeskApi(Settings.Site, Settings.AdminEmail, Settings.AdminPassword);

        [Test]
        public void CanGetUsers()
        {
            var res = api.Users.GetAllUsers();
            Assert.True(res.Count > 0);
        }
        
        [Test]
        public void CanGetAgents()
        {
            var res = api.Users.GetAllAgents();
            Assert.True(res.Count > 0);
        }

        [Test]
        public void CanGetAdmins()
        {
            var res = api.Users.GetAllAdmins();
            Assert.True(res.Count > 0);
        }

        [Test]
        public void CanGetEndUsers()
        {
            var res = api.Users.GetAllEndUsers();
            Assert.True(res.Count > 0);
        }

        [Test]
        public void CanGetAllUsersInRoles()
        {
            var res = api.Users.GetAllUsersInRoles(agents: true, admins: true);
            Assert.True(res.Count > 0);
        }

        [Test]
        public void CanGetUserByCustomField()
        {
            var res = api.Users.SearchByCustomUserField(Settings.FieldKey, Settings.FieldValue);
            var user = res.Users.FirstOrDefault();

            Assert.NotNull(user);
            Assert.AreEqual(1158278453, user.Id);
        }

        [Test]
        public void CannotGetUserByCustomField()
        {
            var res = api.Users.SearchByCustomUserField(Settings.FieldKey, Settings.BadFieldValue);

            Assert.AreEqual(0, res.Users.Count);
            Assert.Null(res.Users.FirstOrDefault());
        }

        [Test]
        public void CanGetUser()
        {
            var res = api.Users.GetUser(Settings.UserId);
            Assert.True(res.User.Id == Settings.UserId);
        }

        [Test]
        public void CanGetUsersInGroup()
        {
            var res = api.Users.GetUsersInGroup(Settings.GroupId);
            Assert.True(res.Count > 0);
        }

        [Test]
        public void CanGetUsersInOrg()
        {
            var res = api.Users.GetUsersInOrganization(Settings.OrganizationId);
            Assert.True(res.Count > 0);
        }

        [Test]
        public void CanCreateUpdateSuspendAndDeleteUser()
        {
            var list = api.Users.GetAllUsers();
            var users = list.Users.Where(x => x.Email == "test772@tester.com");

            foreach(var u in users)
            {
                api.Users.DeleteUser(u.Id.Value);
            }

            var user = new User()
            {
                Name = "tester user72",
                Email = "test772@tester.com",
                Role = "end-user",
                Verified = true,
                CustomFields = new Dictionary<string, object>()
                                  {
                                      {"user_dropdown", "option_1"}
                                  }
            };

            var res1 = api.Users.CreateUser(user);
            var userId = res1.User.Id ?? 0;
            Assert.IsTrue(res1.User.Id > 0);

            Assert.True(api.Users.SetUsersPassword(userId, "t34sssting"));
            Assert.True(api.Users.ChangeUsersPassword(userId, "t34sssting", "newpassw33rd"));

            res1.User.Phone = "555-555-5555";
            res1.User.RemotePhotoUrl = "http://i.imgur.com/b2gxj.jpg";

            var res2 = api.Users.UpdateUser(res1.User);
            var blah = api.Users.GetUser(res1.User.Id.Value);
            Assert.AreEqual(res1.User.Phone, res2.User.Phone);


            var res3 = api.Users.SuspendUser(res2.User.Id.Value);
            Assert.IsTrue(res3.User.Suspended);

            var res4 = api.Users.DeleteUser(res3.User.Id.Value);
            Assert.True(res4);

            //check the remote photo url
            //Assert.AreEqual(res1.User.RemotePhotoUrl, res2.User.RemotePhotoUrl);
        }

        [Test]
        public void CanFindUser()
        {
            //var res1 = api.Users.SearchByEmail(Settings.Email);
            var res1 = api.Users.SearchByEmail(Settings.ColloboratorEmail);
            Assert.True(res1.Users.Count > 0);
        }

        [Test]
        public void CanFindUserByPhone()
        {
            var res1 = api.Users.SearchByPhone(Settings.Phone);
            Assert.True(res1.Users.Count > 0);
            Assert.AreEqual(Settings.Phone, res1.Users.First().Phone);
            Assert.AreEqual("0897c9c1f80646118a8194c942aa84cf 162a3d865f194ef8b7a2ad3525ea6d7c", res1.Users.First().Name);
        }

        [Test]
        public void CanFindUserByFormattedPhone()
        {
            var res1 = api.Users.SearchByPhone(Settings.FormattedPhone);
            Assert.True(res1.Users.Count > 0);
            Assert.AreEqual(Settings.FormattedPhone, res1.Users.First().Phone);
            Assert.AreEqual("dc4d7cf57d0c435cbbb91b1d4be952fe 504b509b0b1e48dda2c8471a88f068a5", res1.Users.First().Name);
        }

        [Test]
        public void CanFindUserByPhoneAsync()
        {
            var res1 = api.Users.SearchByPhoneAsync(Settings.Phone).Result;
            Assert.True(res1.Users.Count > 0);
            Assert.AreEqual(Settings.Phone, res1.Users.First().Phone);
            Assert.AreEqual("0897c9c1f80646118a8194c942aa84cf 162a3d865f194ef8b7a2ad3525ea6d7c", res1.Users.First().Name);
        }

        [Test]
        public void CannotFindUserByPhone()
        {
            var res1 = api.Users.SearchByPhone(Settings.BadPhone);
            Assert.True(res1.Users.Count == 0);
        }

        [Test]
        public void CannotFindUserByPhoneAsync()
        {
            var res1 = api.Users.SearchByPhoneAsync(Settings.BadPhone).Result;
            Assert.True(res1.Users.Count == 0);
        }

        [Test]
        public void CanGetCurrentUser()
        {
            var res1 = api.Users.GetCurrentUser();
            Assert.True(res1.User.Id > 0);
        }

        [Test]
        public void CanGetUserIdentities()
        {
            var res = api.Users.GetCurrentUser();

            var res1 = api.Users.GetUserIdentities(res.User.Id.Value);
            Assert.Greater(res1.Identities[0].Id, 0);

            var res2 = api.Users.GetSpecificUserIdentity(res.User.Id.Value, res1.Identities[0].Id.Value);
            Assert.Greater(res2.Identity.Id, 0);
        }

        [Test]
        public void CanCreateUpdateAndDeleteIdentities()
        {
            var user = new User()
            {
                Name = "test user10",
                Email = "test10@test.com",
            };

            var existingUser = api.Users.SearchByEmail(user.Email);
            if (existingUser.Count > 0)
                api.Users.DeleteUser(existingUser.Users[0].Id.Value);

            var res1 = api.Users.CreateUser(user);
            var userId = res1.User.Id.Value;

            var res2 = api.Users.AddUserIdentity(userId, new UserIdentity()
            {
                Type = UserIdentityTypes.Email,
                Value = "moretest@test.com"
            });
            var identityId = res2.Identity.Id.Value;
            Assert.Greater(identityId, 0);

            var verfified = api.Users.SetUserIdentityAsVerified(userId, identityId);
            Assert.AreEqual(identityId, verfified.Identity.Id);

            var primaries = api.Users.SetUserIdentityAsPrimary(userId, identityId);
            Assert.AreEqual(identityId, primaries.Identities.First(x => x.Primary).Id);

            Assert.True(api.Users.DeleteUserIdentity(userId, identityId));
            Assert.True(api.Users.DeleteUser(userId));
        }

        [Test]
        public async Task CanMergeUsersAsync()
        {
            var user1 = new User();
            user1.Name = Guid.NewGuid().ToString("N") + " " + Guid.NewGuid().ToString("N");
            user1.Email = Guid.NewGuid().ToString("N") + "@" + Guid.NewGuid().ToString("N") + ".com";

            var user2 = new User();
            user2.Name = Guid.NewGuid().ToString("N") + " " + Guid.NewGuid().ToString("N");
            user2.Email = Guid.NewGuid().ToString("N") + "@" + Guid.NewGuid().ToString("N") + ".com";

            var resultUser1 = api.Users.CreateUser(user1);
            var resultUser2 = api.Users.CreateUser(user2);

            var mergedUser = await api.Users.MergeUserAsync(resultUser1.User.Id.Value, resultUser2.User.Id.Value);

            await Task.Delay(1000);
            var mergedIdentities = await api.Users.GetUserIdentitiesAsync(mergedUser.User.Id.Value);

            Assert.That(resultUser2.User.Id, Is.EqualTo(mergedUser.User.Id));
            Assert.IsTrue(mergedIdentities.Identities.Any(i => i.Value.ToLower() == user1.Email.ToLower()));
            Assert.IsTrue(mergedIdentities.Identities.Any(i => i.Value.ToLower() == user2.Email.ToLower()));

            api.Users.DeleteUser(resultUser1.User.Id.Value);
            api.Users.DeleteUser(resultUser2.User.Id.Value);
        }

        [Test]
        public void CanGetMultipleUsers()
        {
            var userList = api.Users.GetAllUsers(10, 1).Users.Select(u => u.Id.Value).ToList();
            var result = api.Users.GetMultipleUsers(userList, UserSideLoadOptions.Organizations | UserSideLoadOptions.Identities | UserSideLoadOptions.Roles);

            Assert.AreEqual(userList.Count, result.Count);
            Assert.IsTrue((result.Organizations != null && result.Organizations.Any()) || (result.Identities != null && result.Identities.Any()));
        }

        [Test]
        public void CanGetMultipleUsersAsync()
        {
            var userList = api.Users.GetAllUsersAsync(10, 1).Result.Users.Select(u => u.Id.Value).ToList();
            var result = api.Users.GetMultipleUsers(userList, UserSideLoadOptions.Organizations | UserSideLoadOptions.Identities);
            Assert.AreEqual(userList.Count, result.Count);
            Assert.IsTrue((result.Organizations != null && result.Organizations.Any()) || (result.Identities != null && result.Identities.Any()));
        }

        [Test]
        public void CanSetUserPhoto()
        {
            var file = new ZenFile()
            {
                ContentType = "image/jpeg",
                FileName = "gracehoppertocat3.jpg",
                FileData = File.ReadAllBytes(TestContext.CurrentContext.TestDirectory + "\\gracehoppertocat3.jpg")
            };

            var user = api.Users.SetUserPhoto(Settings.UserId, file);
            Assert.That(user.User.Photo.ContentUrl, Is.Not.Null);
            Assert.That(user.User.Photo.Size, Is.EqualTo(6553));
        }

        [Test]
        public async Task CanSetUserPhotoAsync()
        {
            var file = new ZenFile()
            {
                ContentType = "image/jpeg",
                FileName = "gracehoppertocat3.jpg",
                FileData = File.ReadAllBytes(TestContext.CurrentContext.TestDirectory + "\\gracehoppertocat3.jpg")
            };

            var user = await api.Users.SetUserPhotoAsync(Settings.UserId, file);
            Assert.That(user.User.Photo.ContentUrl, Is.Not.Null);
            Assert.That(user.User.Photo.Size, Is.EqualTo(6553));
        }

        [Test]
        public void CanGetUserRelatedInformation()
        {
            //Arrange
            var userId = Settings.UserId;

            //Act
            var result = api.Users.GetUserRelatedInformation(userId);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(IndividualUserRelatedInformationResponse), result);
        }

        [Test]
        public async Task CanGetUserRelatedInformationAsync()
        {
            //Arrange
            var userId = Settings.UserId;

            //Act
            var result = await api.Users.GetUserRelatedInformationAsync(userId);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(IndividualUserRelatedInformationResponse), result);
        }
    }
}
