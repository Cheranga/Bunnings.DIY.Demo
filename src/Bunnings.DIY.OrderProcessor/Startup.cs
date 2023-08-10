using Bunnings.DIY.OrderProcessor;
using Bunnings.DIY.OrderProcessor.Features.ReadFile;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

[assembly: FunctionsStartup(typeof(Startup))]

namespace Bunnings.DIY.OrderProcessor;

public class Startup : FunctionsStartup
{
    protected virtual IConfiguration GetConfiguration(IFunctionsHostBuilder builder)
    {
        var executionContextOptions = builder.Services
            .BuildServiceProvider()
            .GetService<IOptions<ExecutionContextOptions>>()
            .Value;

        return new ConfigurationBuilder()
            .SetBasePath(executionContextOptions.AppDirectory)
            .AddJsonFile("local.settings.json", true)
            .AddEnvironmentVariables()
            .Build();
    }

    public override void Configure(IFunctionsHostBuilder builder)
    {
        var services = builder.Services;

        services.AddSingleton(_ =>
        {
            var config = GetConfiguration(builder)
                .GetSection(nameof(ReadFileConfig))
                .Get<ReadFileConfig>();
            return config;
        });
    }
}
