using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Alerter.Util.Abstract;

/// <summary>
/// A utility library for alert related operations and abstraction over other notification services
/// </summary>
public interface IAlerter
{
    ValueTask Error(string message, string? channel = null, CancellationToken cancellationToken = default);
}