using Microsoft.Extensions.Logging;
using Soenneker.Alerter.Util.Abstract;
using Soenneker.Extensions.ValueTask;
using Soenneker.MsTeams.Util.Abstract;
using Soenneker.Email.Support.Abstract;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Alerter.Util;

/// <inheritdoc cref="IAlerter"/>
public sealed class Alerter : IAlerter
{
    private readonly ILogger<Alerter> _logger;
    private readonly IMsTeamsUtil _msTeamsUtil;
    private readonly IEmailSupportUtil _emailSupportUtil;

    public Alerter(ILogger<Alerter> logger, IMsTeamsUtil msTeamsUtil, IEmailSupportUtil emailSupportUtil)
    {
        _logger = logger;
        _msTeamsUtil = msTeamsUtil;
        _emailSupportUtil = emailSupportUtil;
    }

    public ValueTask Error(string subject, string message, string channel = "Errors", bool includeEmail = false,
        CancellationToken cancellationToken = default) =>
        Send(subject, message, LogLevel.Error, channel, includeEmail, cancellationToken);

    public ValueTask Notify(string subject, string message, string channel = "Notifications", bool includeEmail = false,
        CancellationToken cancellationToken = default) =>
        Send(subject, message, LogLevel.Information, channel, includeEmail, cancellationToken);

    public async ValueTask Send(string subject, string message, LogLevel level = LogLevel.Error, string channel = "Errors", bool includeEmail = false,
        CancellationToken cancellationToken = default)
    {
        _logger.Log(level, "{Level} alert: {Message}", level, message);

        await _msTeamsUtil.SendMessage(subject, channel, message, cancellationToken: cancellationToken).NoSync();

        if (includeEmail)
        {
            await _emailSupportUtil.Send(subject, message, cancellationToken: cancellationToken).NoSync();
        }
    }
}