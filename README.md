[![](https://img.shields.io/nuget/v/soenneker.alerter.util.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.alerter.util/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.alerter.util/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.alerter.util/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.alerter.util.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.alerter.util/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.alerter.util/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.alerter.util/actions/workflows/codeql.yml)

# Soenneker.Alerter.Util

A single application-facing abstraction for logging an alert, sending it to a Microsoft Teams channel, and optionally copying it to a support email address.

`IAlerter` is useful when application code should describe the alert without depending directly on the Teams and email delivery services. It includes convenience methods for the common error and notification cases, plus a method that accepts any `LogLevel`.

## Installation

```bash
dotnet add package Soenneker.Alerter.Util
```

## Registration

```csharp
using Soenneker.Alerter.Util.Registrars;

builder.Services.AddAlerterAsSingleton();
```

For applications that keep notification dependencies scoped:

```csharp
builder.Services.AddAlerterAsScoped();
```

The registrar also adds the required `IMsTeamsUtil` and `IEmailSupportUtil` dependency graphs with the matching lifetime. `IAlerter` uses `TryAdd`, so an implementation registered earlier by the application is preserved.

## Send alerts

Inject `IAlerter` into the service that detects the condition:

```csharp
using Soenneker.Alerter.Util.Abstract;

public sealed class ImportMonitor
{
    private readonly IAlerter _alerter;

    public ImportMonitor(IAlerter alerter)
    {
        _alerter = alerter;
    }

    public ValueTask ReportFailure(
        string importId,
        CancellationToken cancellationToken)
    {
        return _alerter.Error(
            subject: $"Import {importId} failed",
            message: "The source file could not be processed. Review the worker logs.",
            channel: "DataImports",
            includeEmail: true,
            cancellationToken: cancellationToken);
    }
}
```

For an informational message, `Notify` uses `LogLevel.Information` and defaults to the `Notifications` Teams channel:

```csharp
await alerter.Notify(
    subject: "Catalog import complete",
    message: "2,418 products were updated.",
    cancellationToken: cancellationToken);
```

Use `Send` when the severity and channel are selected at runtime:

```csharp
using Microsoft.Extensions.Logging;

await alerter.Send(
    subject: "Queue depth warning",
    message: "The billing queue has exceeded 10,000 messages.",
    level: LogLevel.Warning,
    channel: "Operations",
    cancellationToken: cancellationToken);
```

## Delivery behavior

Every call performs these operations in order:

1. Logs the message through `ILogger<Alerter>` at the selected level.
2. Sends the subject and message to the selected Teams channel.
3. When `includeEmail` is `true`, sends the same subject and message through the support email service.

Email delivery occurs only after Teams delivery completes successfully. If the Teams operation throws or is cancelled, the email operation is not attempted. The methods do not swallow delivery exceptions, so callers can retry, record a failure, or allow the exception to propagate according to application policy.

The default values are:

| Method | Log level | Teams channel | Email |
| --- | --- | --- | --- |
| `Error` | `Error` | `Errors` | Disabled |
| `Notify` | `Information` | `Notifications` | Disabled |
| `Send` | `Error` | `Errors` | Disabled |

## Configuration

Teams delivery is provided by [`Soenneker.MsTeams.Util`](https://www.nuget.org/packages/Soenneker.MsTeams.Util), and email delivery is provided by [`Soenneker.Email.Support`](https://www.nuget.org/packages/Soenneker.Email.Support). Configure both underlying services before sending alerts.

At minimum, Teams channel enablement is read from the channel name passed to `IAlerter`:

```json
{
  "Environment": "Production",
  "MsTeams": {
    "DataImports": {
      "Enabled": true
    },
    "Notifications": {
      "Enabled": true
    },
    "Errors": {
      "Enabled": true
    }
  },
  "Email": {
    "SupportAddress": "support@example.com"
  }
}
```

`Email:SupportAddress` is required only when email delivery is used, but the email dependency must still be resolvable because it is injected into `Alerter`. See the underlying packages for their transport-specific configuration, including Teams immediate-versus-queued delivery and email dispatching.

## API

| Method | Purpose |
| --- | --- |
| `Error(subject, message, channel, includeEmail, cancellationToken)` | Sends an error-level alert; defaults to the `Errors` channel. |
| `Notify(subject, message, channel, includeEmail, cancellationToken)` | Sends an information-level alert; defaults to the `Notifications` channel. |
| `Send(subject, message, level, channel, includeEmail, cancellationToken)` | Sends an alert with explicit severity and routing. |
| `AddAlerterAsSingleton()` | Registers the alerter and its notification dependencies as singletons. |
| `AddAlerterAsScoped()` | Registers the alerter and its notification dependencies as scoped services. |
