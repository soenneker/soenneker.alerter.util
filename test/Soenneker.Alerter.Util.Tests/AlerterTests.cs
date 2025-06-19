using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Alerter.Util.Tests;

[Collection("Collection")]
public class AlerterTests : FixturedUnitTest
{

    public AlerterTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
    }

    [Fact]
    public void Default()
    {

    }
}
