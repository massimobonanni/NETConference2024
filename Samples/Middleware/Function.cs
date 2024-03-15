using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Samples.Middleware
{
    public class Function
    {
        private readonly ILogger<Function> _logger;

        public Function(ILogger<Function> logger)
        {
            _logger = logger;
        }

        [Function("Middleware")]
        public HttpResponseData Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "middleware")] HttpRequestData req,
            FunctionContext context)
        {
            if (req.Url.Query.Contains("throw-exception"))
            {
                throw new Exception("App code failed");
            }

            // Get the item set by the middleware
            if (context.Items.TryGetValue("middlewareitem", out object value) && value is string message)
            {
                _logger.LogInformation("From middleware: {message}", message);
            }

            var response = req.CreateResponse(HttpStatusCode.OK);

            // Set a context item the middleware can retrieve
            context.Items.Add("functionitem", "Hello from function!");

            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            response.Headers.Add("FunctionScope", "This is added by the function");
            response.WriteStringAsync("Welcome to .NET 8.0!!");

            return response;
        }
    }
}
