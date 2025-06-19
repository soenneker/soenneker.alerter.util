using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Alerter.Util.Abstract;
using Soenneker.Email.Support.Registrars;
using Soenneker.MsTeams.Util.Registrars;

namespace Soenneker.Alerter.Util.Registrars;

/// <summary>
/// A utility library for alert related operations and abstraction over other notification services
/// </summary>
public static class AlerterRegistrar
{
    /// <summary>
    /// Adds <see cref="IAlerter"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddAlerterAsSingleton(this IServiceCollection services)
    {
        services.AddMsTeamsUtilAsSingleton().AddEmailSupportUtilAsSingleton().TryAddSingleton<IAlerter, Alerter>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="IAlerter"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddAlerterAsScoped(this IServiceCollection services)
    {
        services.AddMsTeamsUtilAsScoped().AddEmailSupportUtilAsScoped().TryAddScoped<IAlerter, Alerter>();

        return services;
    }
}