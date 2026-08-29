[![](https://img.shields.io/nuget/v/soenneker.alerter.util.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.alerter.util/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.alerter.util/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.alerter.util/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.alerter.util.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.alerter.util/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.alerter.util/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.alerter.util/actions/workflows/codeql.yml)

# Soenneker.Alerter.Util

Provides functionality to send alerts to Microsoft Teams channels and optionally via email.

## Install

```bash
dotnet add package Soenneker.Alerter.Util
```

## Quick start

```csharp
using Soenneker.Alerter.Util.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddAlerterAsSingleton();
```

Adds `IAlerter` as a singleton service.

## What you get

- `IAlerter` — Provides functionality to send alerts to Microsoft Teams channels and optionally via email.
- `AlerterRegistrar` — A utility library for alert related operations and abstraction over other notification services.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `IAlerter.Error(subject, message, channel, includeEmail, cancellationToken)` | Sends an error-level alert with a subject and message to a Teams channel and optionally via email. | A `ValueTask` representing the asynchronous operation. |
| `IAlerter.Notify(subject, message, channel, includeEmail, cancellationToken)` | Sends an informational notification with a subject and message to a Teams channel and optionally via email. | A `ValueTask` representing the asynchronous operation. |
| `IAlerter.Send(subject, message, level, channel, includeEmail, cancellationToken)` | Sends an alert with a specified log level, subject, and message to a Teams channel and optionally via email. | A `ValueTask` representing the asynchronous operation. |
| `AlerterRegistrar.AddAlerterAsSingleton(services)` | Adds `IAlerter` as a singleton service. | The same service collection, so additional registrations can be chained. |
| `AlerterRegistrar.AddAlerterAsScoped(services)` | Adds `IAlerter` as a scoped service. | The same service collection, so additional registrations can be chained. |

## Practical notes

- Cancellation stops pending work; it does not undo work that has already completed.
