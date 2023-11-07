using System;
using Microsoft.Extensions.DependencyInjection;

namespace PowerPipe.UnitTests.Fixtures;

public sealed class AutoDIFixture : IDisposable
{
    public readonly IServiceCollection ServiceCollection;

    public AutoDIFixture()
    {
        var services = new ServiceCollection();
        services.AddPowerPipe(c =>
        {
            c.RegisterServicesFromAssemblies(typeof(AutoDIFixture).Assembly);
        });

        ServiceCollection = services;
    }

    public void Dispose()
    {
    }
}
