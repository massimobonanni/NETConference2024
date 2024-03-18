using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Azure;
using Azure.Core.Pipeline;
using Azure.Core;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices((hostContext, services) =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.AddAzureClients(clientBuilder =>
        {
            clientBuilder
                .AddBlobServiceClient(hostContext.Configuration.GetSection("AzureWebJobsStorage"))
                .ConfigureOptions(options =>
                {
                    var policy = new RetryPolicy(3,
                        DelayStrategy.CreateFixedDelayStrategy(TimeSpan.FromMilliseconds(500)));

                    options.RetryPolicy = policy;
                })
                .WithName("blobService");
        });
    })
    .Build();

host.Run();
