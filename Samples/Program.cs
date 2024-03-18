using Core.Interfaces;
using Core.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Samples.Middleware;
using System.Text.Json.Serialization;
using System.Text.Json;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication(worker =>
    {
        worker.UseMiddleware<ExceptionHandlingMiddleware>();
        worker.UseMiddleware<MyCustomMiddleware>();

        worker.UseWhen<StampHttpHeaderMiddleware>(context=>
        {
            if (context.FunctionDefinition.EntryPoint.StartsWith("Samples.Middleware"))
            {
                return true;
            }
            return false;
        });
    })
    .ConfigureServices(services =>
    {
        services.AddScoped<ICoreService, SampleCoreService>();

        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();


    })
    .Build();

host.Run();
