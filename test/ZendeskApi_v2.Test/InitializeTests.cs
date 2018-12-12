using System.Net;
using NUnit.Framework;

[SetUpFixture]
public class InitializeTests
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
    }
}
