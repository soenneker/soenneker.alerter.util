using Soenneker.Alerter.Util.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Alerter.Util.Tests;

[Collection("Collection")]
public class AlerterTests : FixturedUnitTest
{
    private readonly IAlerter _util;

    public AlerterTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _util = Resolve<IAlerter>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
