using Microsoft.Extensions.Logging;
using Soenneker.Alerter.Util.Abstract;
using Soenneker.Extensions.ValueTask;
using Soenneker.MsTeams.Util.Abstract;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Alerter.Util;

/// <inheritdoc cref="IAlerter"/>
public class Alerter : IAlerter
{
    private readonly ILogger<Alerter> _logger;
    private readonly IMsTeamsUtil _msTeamsUtil;

    public Alerter(ILogger<Alerter> logger, IMsTeamsUtil msTeamsUtil)
    {
        _logger = logger;
        _msTeamsUtil = msTeamsUtil;
    }

    public async ValueTask Error(string message, string? channel = null, CancellationToken cancellationToken = default)
    {
        await _msTeamsUtil.SendMessage(message, channel ?? "Errors", cancellationToken: cancellationToken).NoSync();

        _logger.LogError(message);
    }
}