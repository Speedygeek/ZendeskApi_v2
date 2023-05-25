using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ZendeskApi_v2.Models.Constants;
using ZendeskApi_v2.Models.Shared;
using ZendeskApi_v2.Models.Users;
using ZendeskApi_v2.Requests;
using ZendeskApi_v2.Tests.Base;

namespace ZendeskApi_v2.Tests;

[TestFixture]
[Category("Users")]
public class UserTests : TestBase
{
    private const int MaxRetryAttempts = 10;
    private const string JobStatusCompleted = "completed";
    private const string JobStatusQueued = "queued";

    [Test]
    public void CanGetUsers()
    {
        var res = Api.Users.GetAllUsers();
        Assert.That(res.Count, Is.GreaterThan(0));
    }

    [Test]
    public void CanGetAgents()
    {
        var res = Api.Users.GetAllAgents();
        Assert.That(res.Count, Is.GreaterThan(0));
    }

    [Test]
    public void CanGetAdmins()
    {
        var res = Api.Users.GetAllAdmins();
        Assert.That(res.Count, Is.GreaterThan(0));
    }

    [Test]
    public void CanGetEndUsers()
    {
        var res = Api.Users.GetAllEndUsers();
        Assert.That(res.Count, Is.GreaterThan(0));
    }

    [Test]
    public void CanGetAllUsersInRoles()
    {
        var res = Api.Users.GetAllUsersInRoles(agents: true, admins: true);
        Assert.That(res.Count, Is.GreaterThan(0));
    }

    [Test]
    public void CanGetUserByCustomField()
    {
        var res = Api.Users.SearchByCustomUserField(Settings.FieldKey, Settings.FieldValue);
        var user = res.Users.FirstOrDefault();

        Assert.That(user, Is.Not.Null);
        Assert.That(user.Id, Is.EqualTo(1158278453));
    }

    [Test]
    public void CannotGetUserByCustomField()
    {
        var res = Api.Users.SearchByCustomUserField(Settings.FieldKey, Settings.BadFieldValue);

        Assert.That(res.Users, Is.Empty);
        Assert.That(res.Users.FirstOrDefault(), Is.Null);
    }

    [Test]
    public void CanGetUser()
    {
        var res = Api.Users.GetUser(Admin.ID);
        Assert.That(res.User.Id, Is.EqualTo(Admin.ID));
    }

    [Test]
    public void CanGetUsersInGroup()
    {
        var res = Api.Users.GetUsersInGroup(Settings.GroupId);
        Assert.That(res.Count, Is.GreaterThan(0));
    }

    [Test]
    public void CanGetUsersInOrg()
    {
        var res = Api.Users.GetUsersInOrganization(Organization.ID);
        Assert.That(res.Count, Is.GreaterThan(0));
    }

    [Test]
    public void CanGetUsersInOrgPaginated()
    {
        // create users
        var users = new List<User>();
        for (var i = 0; i < 5; i++)
        {
            var user = new User()
            {
                Name = Guid.NewGuid().ToString("N") + " " + Guid.NewGuid().ToString("N"),
                Email = Guid.NewGuid().ToString("N") + "@" + Guid.NewGuid().ToString("N") + ".com",
                OrganizationId = Organization.ID
            };

            var res1 = Api.Users.CreateUser(user);

            users.Add(res1.User);
        }

        var res = Api.Users.GetUsersInOrganization(Organization.ID, 3, 0);
        Assert.Multiple(() =>
        {
            Assert.That(res.Users, Has.Count.EqualTo(3));
            Assert.That(res.NextPage, Is.Not.Null);
        });
        users.ForEach(u => Api.Users.DeleteUser(u.Id.Value));
    }

    [Test]
    public void CanCreateUpdateSuspendAndDeleteUser()
    {
        var groupUserResponse = Api.Users.SearchByEmail("test772@tester.com");
        foreach (var u in groupUserResponse.Users)
        {
            Api.Users.DeleteUser(u.Id.Value);
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

        var res1 = Api.Users.CreateUser(user);
        var userId = res1.User.Id ?? 0;
        Assert.Multiple(() =>
        {
            Assert.That(res1.User.Id, Is.GreaterThan(0));

            Assert.That(Api.Users.SetUsersPassword(userId, "t34sssting"), Is.True);
            Assert.That(Api.Users.ChangeUsersPassword(userId, "t34sssting", "newpassw33rd"), Is.True);
        });
        res1.User.Phone = "555-555-5555";
        res1.User.RemotePhotoUrl = "http://i.imgur.com/b2gxj.jpg";

        var res2 = Api.Users.UpdateUser(res1.User);
        Api.Users.GetUser(res1.User.Id.Value);
        Assert.That(res2.User.Phone, Is.EqualTo(res1.User.Phone));

        var res3 = Api.Users.SuspendUser(res2.User.Id.Value);
        Assert.That(res3.User.Suspended, Is.True);

        var res4 = Api.Users.DeleteUser(res3.User.Id.Value);
        Assert.That(res4, Is.True);
    }

    [Test]
    public void CanCreateOrUpdateUser_NewUser()
    {
        var groupUserResponse = Api.Users.SearchByEmail("test772@tester.com");
        foreach (var u in groupUserResponse.Users)
        {
            Api.Users.DeleteUser(u.Id.Value);
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

        var res1 = Api.Users.CreateOrUpdateUser(user);
        Assert.That(res1.User.Id, Is.GreaterThan(0));
    }
    [Test]
    public void CanCreateOrUpdateUser_CreateSameUserTwice()
    {
        var groupUserResponse = Api.Users.SearchByEmail("test772@tester.com");
        foreach (var u in groupUserResponse.Users)
        {
            Api.Users.DeleteUser(u.Id.Value);
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
        _ = Api.Users.CreateOrUpdateUser(user);
        _ = Api.Users.CreateOrUpdateUser(user);

        var user72group = Api.Users.SearchByEmail("test772@tester.com");

        Assert.That(user72group.Count, Is.EqualTo(1));
    }

    [Test]
    public void CanCreateOrUpdateUser_UpdateUser()
    {
        var groupUserResponse = Api.Users.SearchByEmail("test772@tester.com");
        foreach (var u in groupUserResponse.Users)
        {
            Api.Users.DeleteUser(u.Id.Value);
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
        _ = Api.Users.CreateOrUpdateUser(user);

        user.Name = "tester user721";
        var res2 = Api.Users.CreateOrUpdateUser(user);

        var user72group = Api.Users.SearchByEmail("test772@tester.com");
        Assert.Multiple(() =>
        {
            Assert.That(user72group.Count, Is.EqualTo(1));
            Assert.That(res2.User.Name, Does.Contain("721"));
        });
    }

    [Test]
    public async Task CanCreateOrUpdateUserAsync_NewUser()
    {
        var groupUserResponse = Api.Users.SearchByEmail("test772@tester.com");
        foreach (var u in groupUserResponse.Users)
        {
            Api.Users.DeleteUser(u.Id.Value);
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

        var res1 = await Api.Users.CreateOrUpdateUserAsync(user);
        Assert.That(res1.User.Id, Is.GreaterThan(0));
    }
    [Test]
    public async Task CanCreateOrUpdateUserAsync_CreateSameUserTwice()
    {
        var groupUserResponse = Api.Users.SearchByEmail("test772@tester.com");
        foreach (var u in groupUserResponse.Users)
        {
            Api.Users.DeleteUser(u.Id.Value);
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
        _ = await Api.Users.CreateOrUpdateUserAsync(user);
        _ = await Api.Users.CreateOrUpdateUserAsync(user);

        var user72group = Api.Users.SearchByEmail("test772@tester.com");

        Assert.That(user72group.Count, Is.EqualTo(1));
    }

    [Test]
    public async Task CanCreateOrUpdateUserAsync_UpdateUser()
    {
        var groupUserResponse = Api.Users.SearchByEmail("test772@tester.com");
        foreach (var u in groupUserResponse.Users)
        {
            Api.Users.DeleteUser(u.Id.Value);
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
        _ = await Api.Users.CreateOrUpdateUserAsync(user);

        user.Name = "tester user721";
        var res2 = Api.Users.CreateOrUpdateUser(user);

        var user72group = Api.Users.SearchByEmail("test772@tester.com");
        Assert.Multiple(() =>
        {
            Assert.That(user72group.Count, Is.EqualTo(1));
            Assert.That(res2.User.Name, Does.Contain("721"));
        });
    }

    [Test]
    public void CanFindUser()
    {
        //var res1 = Api.Users.SearchByEmail(Settings.Email);
        var res1 = Api.Users.SearchByEmail(Settings.ColloboratorEmail);
        Assert.That(res1.Users, Is.Not.Empty);
    }

    [Test]
    public void CanFindUserByPhone()
    {
        var res1 = Api.Users.SearchByPhone(Settings.Phone);
        Assert.That(res1.Users, Is.Not.Empty);
        Assert.Multiple(() =>
        {
            Assert.That(res1.Users.First().Phone, Is.EqualTo(Settings.Phone));
            Assert.That(res1.Users.First().Name, Is.EqualTo("0897c9c1f80646118a8194c942aa84cf 162a3d865f194ef8b7a2ad3525ea6d7c"));
        });
    }

    [Test]
    public void CanFindUserByFormattedPhone()
    {
        var res1 = Api.Users.SearchByPhone(Settings.FormattedPhone);
        Assert.That(res1.Users, Is.Not.Empty);
        Assert.Multiple(() =>
        {
            Assert.That(res1.Users.First().Phone, Is.EqualTo(Settings.FormattedPhone));
            Assert.That(res1.Users.First().Name, Is.EqualTo("dc4d7cf57d0c435cbbb91b1d4be952fe 504b509b0b1e48dda2c8471a88f068a5"));
        });
    }

    [Test]
    public void CanFindUserByPhoneAsync()
    {
        var res1 = Api.Users.SearchByPhoneAsync(Settings.Phone).Result;
        Assert.That(res1.Users, Is.Not.Empty);
        Assert.Multiple(() =>
        {
            Assert.That(res1.Users.First().Phone, Is.EqualTo(Settings.Phone));
            Assert.That(res1.Users.First().Name, Is.EqualTo("0897c9c1f80646118a8194c942aa84cf 162a3d865f194ef8b7a2ad3525ea6d7c"));
        });
    }

    [Test]
    public void CannotFindUserByPhone()
    {
        var res1 = Api.Users.SearchByPhone(Settings.BadPhone);
        Assert.That(res1.Users, Is.Empty);
    }

    [Test]
    public void CannotFindUserByPhoneAsync()
    {
        var res1 = Api.Users.SearchByPhoneAsync(Settings.BadPhone).Result;
        Assert.That(res1.Users, Is.Empty);
    }

    [Test]
    public void CanGetCurrentUser()
    {
        var res1 = Api.Users.GetCurrentUser();
        Assert.That(res1.User.Id, Is.GreaterThan(0));
    }

    [Test]
    public void CanGetUserIdentities()
    {
        var res = Api.Users.GetCurrentUser();

        var res1 = Api.Users.GetUserIdentities(res.User.Id.Value);
        Assert.That(res1.Identities[0].Id, Is.GreaterThan(0));

        var res2 = Api.Users.GetSpecificUserIdentity(res.User.Id.Value, res1.Identities[0].Id.Value);
        Assert.That(res2.Identity.Id, Is.GreaterThan(0));
    }

    [Test]
    public void CanCreateUpdateAndDeleteIdentities()
    {
        var user = new User()
        {
            Name = "test user10",
            Email = "test10@test.com",
        };

        var existingUser = Api.Users.SearchByEmail(user.Email);
        if (existingUser.Count > 0)
        {
            Api.Users.DeleteUser(existingUser.Users[0].Id.Value);
        }

        var res1 = Api.Users.CreateUser(user);
        var userId = res1.User.Id.Value;

        var res2 = Api.Users.AddUserIdentity(userId, new UserIdentity()
        {
            Type = UserIdentityTypes.Email,
            Value = "moretest2@test.com"
        });
        var identityId = res2.Identity.Id.Value;
        Assert.That(identityId, Is.GreaterThan(0));

        var verified = Api.Users.SetUserIdentityAsVerified(userId, identityId);
        Assert.That(verified.Identity.Id, Is.EqualTo(identityId));

        var primaries = Api.Users.SetUserIdentityAsPrimary(userId, identityId);
        Assert.That(primaries.Identities.First(x => x.Primary).Id, Is.EqualTo(identityId));
        Assert.Multiple(() =>
        {
            Assert.That(Api.Users.DeleteUserIdentity(userId, identityId), Is.True);
            Assert.That(Api.Users.DeleteUser(userId), Is.True);
        });
    }

    [Test]
    public void CanGetMultipleUsers()
    {
        var userList = Api.Users.GetAllUsers(10, 1).Users.Select(u => u.Id.Value).ToList();
        var result = Api.Users.GetMultipleUsers(userList, UserSideLoadOptions.Organizations | UserSideLoadOptions.Identities | UserSideLoadOptions.Roles);
        Assert.Multiple(() =>
        {
            Assert.That(result.Count, Is.EqualTo(userList.Count));
            Assert.That((result.Organizations != null && result.Organizations.Any()) || (result.Identities != null && result.Identities.Any()), Is.True);
        });
    }

    [Test]
    public void CanGetMultipleUsersAsync()
    {
        var userList = Api.Users.GetAllUsersAsync(10, 1).Result.Users.Select(u => u.Id.Value).ToList();
        var result = Api.Users.GetMultipleUsers(userList, UserSideLoadOptions.Organizations | UserSideLoadOptions.Identities);
        Assert.Multiple(() =>
        {
            Assert.That(result.Count, Is.EqualTo(userList.Count));
            Assert.That((result.Organizations != null && result.Organizations.Any()) || (result.Identities != null && result.Identities.Any()), Is.True);
        });
    }

    [Test]
    public void CanSetUserPhoto()
    {
        var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "gracehoppertocat3.jpg");

        var file = new ZenFile()
        {
            ContentType = "image/jpeg",
            FileName = "gracehoppertocat3.jpg",
            FileData = File.ReadAllBytes(path)
        };

        var user = Api.Users.SetUserPhoto(Admin.ID, file);
        Assert.Multiple(() =>
        {
            Assert.That(user.User.Photo.ContentUrl, Is.Not.Null);
            Assert.That(user.User.Photo.Size, Is.Not.Zero);
        });
    }

    [Test]
    public async Task CanSetUserPhotoAsync()
    {
        var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "gracehoppertocat3.jpg");
        var file = new ZenFile()
        {
            ContentType = "image/jpeg",
            FileName = "gracehoppertocat3.jpg",
            FileData = File.ReadAllBytes(path)
        };

        var user = await Api.Users.SetUserPhotoAsync(Admin.ID, file);
        Assert.Multiple(() =>
        {
            Assert.That(user.User.Photo.ContentUrl, Is.Not.Null);
            Assert.That(user.User.Photo.Size, Is.Not.Zero);
        });
    }

    [Test]
    public void CanGetUserRelatedInformation()
    {
        //Arrange
        var userId = Admin.ID;

        //Act
        var result = Api.Users.GetUserRelatedInformation(userId);

        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf(typeof(IndividualUserRelatedInformationResponse)));
    }

    [Test]
    public async Task CanGetUserRelatedInformationAsync()
    {
        //Arrange
        var userId = Admin.ID;

        //Act
        var result = await Api.Users.GetUserRelatedInformationAsync(userId);

        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf(typeof(IndividualUserRelatedInformationResponse)));
    }

    [Test]
    public async Task CanCreateUpdateAndDeleteIdentitiesAsync()
    {
        var user = new User()
        {
            Name = "test user10",
            Email = "test10@test.com",
        };

        var existingUser = await Api.Users.SearchByEmailAsync(user.Email);
        if (existingUser.Count > 0)
        {
            await Api.Users.DeleteUserAsync(existingUser.Users[0].Id.Value);
        }

        var res1 = await Api.Users.CreateUserAsync(user);
        var userId = res1.User.Id.Value;

        var res2 = await Api.Users.AddUserIdentityAsync(userId, new UserIdentity
        {
            Type = UserIdentityTypes.Email,
            Value = "moretest2@test.com"
        });

        var identityId = res2.Identity.Id.Value;
        Assert.That(identityId, Is.GreaterThan(0));
        res2.Identity.Value = "moretest2@test.com";

        await Api.Users.UpdateUserIdentityAsync(userId, res2.Identity);

        var res3 = await Api.Users.GetSpecificUserIdentityAsync(userId, identityId);
        Assert.Multiple(() =>
        {
            Assert.That(res3.Identity.Id, Is.EqualTo(identityId));
            Assert.That(res3.Identity.Value, Is.EqualTo(res2.Identity.Value));

            Assert.That(Api.Users.DeleteUserIdentity(userId, identityId), Is.True);
            Assert.That(Api.Users.DeleteUser(userId), Is.True);
        });
    }

    [Test]
    public async Task CanBulkDeleteUsersAsync()
    {
        var users = new List<User>();

        for (var i = 0; i < 2; i++)
        {
            var user = new User()
            {
                Name = Guid.NewGuid().ToString("N") + " " + Guid.NewGuid().ToString("N"),
                Email = Guid.NewGuid().ToString("N") + "@" + Guid.NewGuid().ToString("N") + ".com",
                Verified = true
            };

            var response = await Api.Users.CreateUserAsync(user);

            users.Add(response.User);
        }

        var jobResponse = await Api.Users.BulkDeleteUsersAsync(users);

        Assert.That(jobResponse.JobStatus.Status.ToLower(), Is.EqualTo(JobStatusQueued));

        var count = 0;

        while (jobResponse.JobStatus.Status.ToLower() != JobStatusCompleted && count < MaxRetryAttempts)
        {
            await Task.Delay(1000);
            jobResponse = Api.JobStatuses.GetJobStatus(jobResponse.JobStatus.Id);
            count++;
        }

        Assert.Multiple(() =>
        {
            Assert.That(jobResponse.JobStatus.Status.ToLower(), Is.EqualTo(JobStatusCompleted));
            Assert.That(jobResponse.JobStatus.Total, Is.EqualTo(users.Count));
        });
    }

    [Test]
    public void CanBulkDeleteUsers()
    {
        var users = new List<User>();

        for (var i = 0; i < 2; i++)
        {
            var user = new User()
            {
                Name = Guid.NewGuid().ToString("N") + " " + Guid.NewGuid().ToString("N"),
                Email = Guid.NewGuid().ToString("N") + "@" + Guid.NewGuid().ToString("N") + ".com",
                Verified = true
            };

            var response = Api.Users.CreateUser(user);

            users.Add(response.User);
        }

        var jobResponse = Api.Users.BulkDeleteUsers(users);

        Assert.That(jobResponse.JobStatus.Status.ToLower(), Is.EqualTo(JobStatusQueued));

        var count = 0;

        while (jobResponse.JobStatus.Status.ToLower() != JobStatusCompleted && count < MaxRetryAttempts)
        {
            Thread.Sleep(1000);
            jobResponse = Api.JobStatuses.GetJobStatus(jobResponse.JobStatus.Id);
            count++;
        }

        Assert.Multiple(() =>
        {
            Assert.That(jobResponse.JobStatus.Status.ToLower(), Is.EqualTo(JobStatusCompleted));
            Assert.That(jobResponse.JobStatus.Total, Is.EqualTo(users.Count));
        });
    }

    [Test]
    public void CanGetIncrementalUserExport()
    {
        var incrementalUserExport = Api.Users.GetIncrementalUserExport(Settings.Epoch);
        Assert.Multiple(() =>
        {
            Assert.That(incrementalUserExport.Users, Is.Not.Empty);
            Assert.That(incrementalUserExport.Organizations, Is.Null);
            Assert.That(incrementalUserExport.Identities, Is.Null);
            Assert.That(incrementalUserExport.Groups, Is.Null);
        });
        var incrementalUserExportNextPage = Api.Users.GetIncrementalUserExportNextPage(incrementalUserExport.NextPage);
        Assert.Multiple(() =>
        {
            Assert.That(incrementalUserExportNextPage.Users, Is.Not.Empty);
            Assert.That(incrementalUserExportNextPage.Organizations, Is.Null);
            Assert.That(incrementalUserExportNextPage.Identities, Is.Null);
            Assert.That(incrementalUserExportNextPage.Groups, Is.Null);
        });
    }

    //[Test]
    //public void CanGetIncrementalUserExportWithSideLoadOptions()
    //{
    //    Thread.Sleep(60000);

    //    var incrementalUserExport = Api.Users.GetIncrementalUserExport(Settings.Epoch, UserSideLoadOptions.Organizations | UserSideLoadOptions.Groups | UserSideLoadOptions.Identities);
    //    Assert.That(incrementalUserExport.Users.Count, Is.GreaterThan(0));
    //    Assert.That(incrementalUserExport.Organizations, Is.Not.Null);
    //    Assert.That(incrementalUserExport.Identities, Is.Not.Null);
    //    Assert.That(incrementalUserExport.Groups, Is.Not.Null);

    //    var incrementalUserExportNextPage = Api.Users.GetIncrementalUserExportNextPage(incrementalUserExport.NextPage);
    //    Assert.That(incrementalUserExportNextPage.Users.Count, Is.GreaterThan(0));
    //    Assert.That(incrementalUserExportNextPage.Organizations, Is.Not.Null);
    //    Assert.That(incrementalUserExportNextPage.Identities, Is.Not.Null);
    //    Assert.That(incrementalUserExportNextPage.Groups, Is.Not.Null);
    //}

    [Test]
    public async Task CanGetIncrementalUserExportAsync()
    {
        var incrementalUserExport = await Api.Users.GetIncrementalUserExportAsync(Settings.Epoch);
        Assert.Multiple(() =>
        {
            Assert.That(incrementalUserExport.Users, Is.Not.Empty);
            Assert.That(incrementalUserExport.Organizations, Is.Null);
            Assert.That(incrementalUserExport.Identities, Is.Null);
            Assert.That(incrementalUserExport.Groups, Is.Null);
        });
        var incrementalUserExportNextPage = await Api.Users.GetIncrementalUserExportNextPageAsync(incrementalUserExport.NextPage);
        Assert.Multiple(() =>
        {
            Assert.That(incrementalUserExportNextPage.Users, Is.Not.Empty);
            Assert.That(incrementalUserExportNextPage.Organizations, Is.Null);
            Assert.That(incrementalUserExportNextPage.Identities, Is.Null);
            Assert.That(incrementalUserExportNextPage.Groups, Is.Null);
        });
    }

    [Test]
    public async Task CanGetIncrementalUserExportAsyncWithSideLoadOptions()
    {
        var incrementalUserExport = await Api.Users.GetIncrementalUserExportAsync(Settings.Epoch, UserSideLoadOptions.Organizations | UserSideLoadOptions.Groups | UserSideLoadOptions.Identities);
        Assert.Multiple(() =>
        {
            Assert.That(incrementalUserExport.Users, Is.Not.Empty);
            Assert.That(incrementalUserExport.Organizations, Is.Not.Null);
            Assert.That(incrementalUserExport.Identities, Is.Not.Null);
            Assert.That(incrementalUserExport.Groups, Is.Not.Null);
        });
        var incrementalUserExportNextPage = await Api.Users.GetIncrementalUserExportNextPageAsync(incrementalUserExport.NextPage);
        Assert.Multiple(() =>
        {
            Assert.That(incrementalUserExportNextPage.Users, Is.Not.Empty);
            Assert.That(incrementalUserExportNextPage.Organizations, Is.Not.Null);
            Assert.That(incrementalUserExportNextPage.Identities, Is.Not.Null);
            Assert.That(incrementalUserExportNextPage.Groups, Is.Not.Null);
        });
    }

    [Test]
    public void CanGetRolesAndAbilities()
    {
        var userList = Api.Users.GetAllUsers(10, 1).Users.Select(u => u.Id.Value).ToList();
        var result = Api.Users.GetMultipleUsers(userList, UserSideLoadOptions.Roles | UserSideLoadOptions.Abilities);

        Assert.That(result.Count, Is.EqualTo(userList.Count));
        var user = result.Users[0];
        Assert.That(result.Roles, Has.None.Null);
        Assert.That(user.Abilities, Is.Not.Null);
        Assert.That(user.Abilities["can_edit"], Is.Not.Null.Or.Empty);
    }

    [Test]
    public async Task CanBatchUpdateUsers()
    {
        var testDetails = "This is a Test";
        var users = new List<User>
        {
            new User
            {
                 Name = Guid.NewGuid().ToString("N") + " " + Guid.NewGuid().ToString("N"),
                 Email = Guid.NewGuid().ToString("N") + "@" + Guid.NewGuid().ToString("N") + ".com",
                 Verified = true
            },

            new User
            {
                Name = Guid.NewGuid().ToString("N") + " " + Guid.NewGuid().ToString("N"),
                Email = Guid.NewGuid().ToString("N") + "@" + Guid.NewGuid().ToString("N") + ".com",
                Verified = true
            },
        };

        var usersToUpdate = new List<User>();
        foreach (var user in users)
        {
            var createResp = await Api.Users.CreateUserAsync(user);
            usersToUpdate.Add(createResp.User);
        }

        usersToUpdate.ForEach(u => u.Details = testDetails);

        var updateResp = await Api.Users.BatchUpdateAsync(usersToUpdate);

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
            var user = (await Api.Users.GetUserAsync(r.Id)).User;
            Assert.That(user.Details, Is.EqualTo(testDetails));
            await Api.Users.DeleteUserAsync(r.Id);
        }
    }

    [Test]
    public async Task CanBulkUpdateUsers()
    {
        var testDetails = "This is a Test";
        var users = new List<User> {
            new User
            {
                 Name = Guid.NewGuid().ToString("N") + " " + Guid.NewGuid().ToString("N"),
                 Email = Guid.NewGuid().ToString("N") + "@" + Guid.NewGuid().ToString("N") + ".com",
                 Verified = true
            },

            new User
            {
                Name = Guid.NewGuid().ToString("N") + " " + Guid.NewGuid().ToString("N"),
                Email = Guid.NewGuid().ToString("N") + "@" + Guid.NewGuid().ToString("N") + ".com",
                Verified = true
            },
        };

        var usersToUpdate = new List<User>();
        foreach (var user in users)
        {
            var createResp = await Api.Users.CreateUserAsync(user);
            usersToUpdate.Add(createResp.User);
        }

        var updateResp = await Api.Users.BulkUpdateAsync(usersToUpdate.Select(u => u.Id.Value), new User { Details = testDetails });

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
            var user = (await Api.Users.GetUserAsync(r.Id)).User;
            Assert.That(user.Details, Is.EqualTo(testDetails));
            await Api.Users.DeleteUserAsync(r.Id);
        }
    }

    [Test]
    public async Task CanBulkCreateUpdateUsersAsync()
    {
        var users = new List<User> {
            new User
            {
                 Name = Guid.NewGuid().ToString("N") + " " + Guid.NewGuid().ToString("N"),
                 Email = Guid.NewGuid().ToString("N") + "@test.com",
                 Verified = true
            },
            new User
            {
                Name = Guid.NewGuid().ToString("N") + " " + Guid.NewGuid().ToString("N"),
                Email = Guid.NewGuid().ToString("N") + "@test.com",
                Verified = true
            },
        };
        var updateResp = await Api.Users.BulkCreateUpdateUsersAsync(users);
        var job = await Api.JobStatuses.GetJobStatusAsync(updateResp.JobStatus.Id);
        var count = 0;
        while (job.JobStatus.Status.ToLower() != "completed" && count < 10)
        {
            await Task.Delay(1000);
            job = await Api.JobStatuses.GetJobStatusAsync(updateResp.JobStatus.Id);
            count++;
        }
        Assert.That(job.JobStatus.Status.ToLower(), Is.EqualTo("completed"));

        foreach (var u in users)
        {
            var rsp = await Api.Users.SearchByEmailAsync(u.Email);
            if (rsp.Users.Any())
            {
                await Api.Users.DeleteUserAsync(rsp.Users[0].Id.Value);
            }
        }
    }
}




