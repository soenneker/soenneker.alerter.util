using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Soenneker.Alerter.Util.Abstract;

/// <summary>
/// Provides functionality to send alerts to Microsoft Teams channels and optionally via email.
/// </summary>
public interface IAlerter
{
    /// <summary>
    /// Sends an error-level alert with a subject and message to a Teams channel and optionally via email.
    /// </summary>
    /// <param name="subject">The subject or title of the alert.</param>
    /// <param name="message">The detailed message content of the alert.</param>
    /// <param name="channel">The Teams channel name or identifier. Defaults to "Errors".</param>
    /// <param name="includeEmail">Whether to also send the alert via email.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
    ValueTask Error(string subject, string message, string channel = "Errors", bool includeEmail = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends an informational notification with a subject and message to a Teams channel and optionally via email.
    /// </summary>
    /// <param name="subject">The subject or title of the notification.</param>
    /// <param name="message">The detailed message content of the notification.</param>
    /// <param name="channel">The Teams channel name or identifier. Defaults to "Notifications".</param>
    /// <param name="includeEmail">Whether to also send the notification via email.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
    ValueTask Notify(string subject, string message, string channel = "Notifications", bool includeEmail = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends an alert with a specified log level, subject, and message to a Teams channel and optionally via email.
    /// </summary>
    /// <param name="subject">The subject or title of the alert.</param>
    /// <param name="message">The detailed message content of the alert.</param>
    /// <param name="level">The severity level of the log. Defaults to <see cref="LogLevel.Error"/>.</param>
    /// <param name="channel">The Teams channel name or identifier. Defaults to "Errors".</param>
    /// <param name="includeEmail">Whether to also send the alert via email.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
    ValueTask Send(string subject, string message, LogLevel level = LogLevel.Error, string channel = "Errors", bool includeEmail = false, CancellationToken cancellationToken = default);
}