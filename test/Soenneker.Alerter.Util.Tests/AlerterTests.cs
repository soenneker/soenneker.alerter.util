using Soenneker.Tests.HostedUnit;

namespace Soenneker.Alerter.Util.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class AlerterTests : HostedUnitTest
{
    public AlerterTests(Host host) : base(host)
    {
    }

    [Test]
    public void Default()
    {
    }
}