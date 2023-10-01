using System;
using Microsoft.Extensions.DependencyInjection;
using PowerPipe.Extensions.MicrosoftDependencyInjection;
using PowerPipe.UnitTests.Steps;

namespace PowerPipe.UnitTests.Fixtures;

public sealed class AutoDIFixture : IDisposable
{
    public readonly IServiceCollection ServiceCollection;

    public AutoDIFixture()
    {
        var services = new ServiceCollection();
        services.AddPowerPipe(c =>
        {
            c.Lifetime = ServiceLifetime.Transient;
            c.RegisterServicesFromAssemblies(typeof(AutoDIFixture).Assembly);
        });

        ServiceCollection = services;
    }

    public void Dispose()
    {
    }
}