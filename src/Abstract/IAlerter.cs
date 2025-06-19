using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Soenneker.Alerter.Util.Abstract;

/// <summary>
/// Sends structured alerts with logging, Microsoft Teams integration, and optional email support.
/// </summary>
public interface IAlerter
{
    /// <summary>
    /// Sends an error alert with <see cref="LogLevel.Error"/> logging, Teams notification, and optional email.
    /// </summary>
    /// <param name="message">The message to send and log.</param>
    /// <param name="channel">Optional override for the Teams channel. Defaults to "Errors".</param>
    /// <param name="includeEmail">Whether to also send the alert via email.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    ValueTask Error(string message, string? channel = null, bool includeEmail = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends an informational alert with <see cref="LogLevel.Information"/> logging, Teams notification, and optional email.
    /// </summary>
    /// <param name="message">The message to send and log.</param>
    /// <param name="channel">Optional override for the Teams channel. Defaults to "Notifications".</param>
    /// <param name="includeEmail">Whether to also send the alert via email.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    ValueTask Notify(string message, string? channel = null, bool includeEmail = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a custom alert with the specified <see cref="LogLevel"/>, Teams channel, subject, and optional email.
    /// </summary>
    /// <param name="level">The severity level for logging.</param>
    /// <param name="defaultChannel">The fallback Teams channel to use if <paramref name="channelOverride"/> is null.</param>
    /// <param name="emailSubject">The subject to use for email alerts.</param>
    /// <param name="message">The alert message to log and send.</param>
    /// <param name="channelOverride">An optional override for the Teams channel.</param>
    /// <param name="includeEmail">Whether to also send the alert via email.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    ValueTask Send(LogLevel level, string defaultChannel, string emailSubject, string message, string? channelOverride = null, bool includeEmail = false, CancellationToken cancellationToken = default);
}