using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using ZendeskApi_v2.Models.Constants;
using ZendeskApi_v2.Models.Groups;
using ZendeskApi_v2.Models.Users;
using ZendeskApi_v2.Tests.Base;

namespace ZendeskApi_v2.Tests;

[TestFixture]
public class GroupTests : TestBase
{
    [OneTimeSetUp]
    public async Task CleanUp()
    {
        var resp = await Api.Search.SearchForAsync<User>("test133@test.com");

        foreach (var user in resp.Results)
        {
            await Api.Users.DeleteUserAsync(user.Id.Value);
        }

        var resp2 = await Api.Search.SearchForAsync<Group>("Test Group");
        foreach (var group in resp2.Results)
        {
            await Api.Groups.DeleteGroupAsync(group.Id.Value);
        }
    }

    [Test]
    public void CanGetGroups()
    {
        var res = Api.Groups.GetGroups();
        Assert.That(res.Count, Is.GreaterThan(0));
    }

    [Test]
    public void CanGetAssignableGroups()
    {
        var res = Api.Groups.GetAssignableGroups();
        Assert.That(res.Count, Is.GreaterThan(0));
    }

    [Test]
    public void CanGetGroup()
    {
        var res = Api.Groups.GetGroups();
        var res1 = Api.Groups.GetGroupById(res.Groups[0].Id.Value);

        Assert.That(res.Groups[0].Id.Value, Is.EqualTo(res1.Group.Id.Value));
    }

    [Test]
    public void CanCreateUpdateAndDeleteGroup()
    {
        var res = Api.Groups.CreateGroup("Test Group");
        Assert.That(res.Group.Id, Is.GreaterThan(0));

        res.Group.Name = "Updated Test Group";
        var res1 = Api.Groups.UpdateGroup(res.Group);
        Assert.Multiple(() =>
        {
            Assert.That(res.Group.Name, Is.EqualTo(res1.Group.Name));

            Assert.That(Api.Groups.DeleteGroup(res.Group.Id.Value), Is.True);
        });
    }

    [Test]
    public void CanGetGroupMemberships()
    {
        var res = Api.Groups.GetGroupMemberships();
        Assert.That(res.Count, Is.GreaterThan(0));

        var res1 = Api.Groups.GetGroupMembershipsByUser(Admin.ID);
        Assert.That(res1.Count, Is.GreaterThan(0));

        var groups = Api.Groups.GetGroups();
        var res2 = Api.Groups.GetGroupMembershipsByGroup(groups.Groups[0].Id.Value);
        Assert.That(res2.Count, Is.GreaterThan(0));
    }

    [Test]
    public void CanGetAssignableGroupMemberships()
    {
        var res = Api.Groups.GetAssignableGroupMemberships();
        Assert.That(res.Count, Is.GreaterThan(0));

        var groups = Api.Groups.GetGroups();
        var res1 = Api.Groups.GetAssignableGroupMembershipsByGroup(groups.Groups[0].Id.Value);
        Assert.That(res1.Count, Is.GreaterThan(0));
    }

    [Test]
    public void CanGetIndividualGroupMemberships()
    {
        var res = Api.Groups.GetGroupMemberships();

        var res1 = Api.Groups.GetGroupMembershipsByMembershipId(res.GroupMemberships[0].Id.Value);
        Assert.That(res.GroupMemberships[0].Id, Is.EqualTo(res1.GroupMembership.Id));

        var res2 = Api.Groups.GetGroupMembershipsByUserAndMembershipId(res1.GroupMembership.UserId, res.GroupMemberships[0].Id.Value);
        Assert.That(res1.GroupMembership.UserId, Is.EqualTo(res2.GroupMembership.UserId));
    }

    [Test]
    public void CanCreateUpdateAndDeleteMembership()
    {
        var group = Api.Groups.CreateGroup("Test Group 2").Group;
        var user = Api.Users.CreateUser(new User()
        {
            Name = "test user133",
            Email = "test133@test.com",
            Role = UserRoles.Agent
        }).User;

        var res = Api.Groups.CreateGroupMembership(new GroupMembership()
        {
            UserId = user.Id.Value,
            GroupId = group.Id.Value
        });

        Assert.That(res.GroupMembership.Id, Is.GreaterThan(0));

        var res2 = Api.Groups.SetGroupMembershipAsDefault(user.Id.Value, res.GroupMembership.Id.Value);
        Assert.That(res2.GroupMemberships.First(x => x.Id == res.GroupMembership.Id).Default, Is.True);
        Assert.Multiple(() =>
        {
            Assert.That(Api.Groups.DeleteGroupMembership(res.GroupMembership.Id.Value), Is.True);
            Assert.That(Api.Users.DeleteUser(user.Id.Value), Is.True);
            Assert.That(Api.Groups.DeleteGroup(group.Id.Value), Is.True);
        });
    }

    [Test]
    public void CanGetGroupMembershipsAsync()
    {
        var res = Api.Groups.GetGroupMembershipsAsync().Result;
        Assert.That(res.Count, Is.GreaterThan(0));

        var res1 = Api.Groups.GetGroupMembershipsByUserAsync(Admin.ID).Result;
        Assert.That(res1.Count, Is.GreaterThan(0));

        var groups = Api.Groups.GetGroupsAsync().Result;
        var res2 = Api.Groups.GetGroupMembershipsByGroupAsync(groups.Groups[0].Id.Value).Result;
        Assert.That(res2.Count, Is.GreaterThan(0));
    }
}
