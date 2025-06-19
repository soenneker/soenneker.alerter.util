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

    public ValueTask Error(string message, string? channel = null, bool includeEmail = false, CancellationToken cancellationToken = default) =>
        Send(LogLevel.Error, "Errors", "Alerter: Error", message, channel, includeEmail, cancellationToken);

    public ValueTask Notify(string message, string? channel = null, bool includeEmail = false, CancellationToken cancellationToken = default) =>
        Send(LogLevel.Information, "Notifications", "Alerter: Notification", message, channel, includeEmail, cancellationToken);

    public async ValueTask Send(LogLevel level, string defaultChannel, string emailSubject, string message, string? channelOverride, bool includeEmail,
        CancellationToken cancellationToken)
    {
        _logger.Log(level, "{Level} alert: {Message}", level, message);

        string channel = channelOverride ?? defaultChannel;

        await _msTeamsUtil.SendMessage(message, channel, cancellationToken: cancellationToken).NoSync();

        if (includeEmail)
        {
            await _emailSupportUtil.Send(emailSubject, message, cancellationToken: cancellationToken).NoSync();
        }
    }
}