using NUnit.Framework;
using System.Net;


[SetUpFixture]
public class InitializeTests
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
    }
}