using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samples.Middleware
{
    public class MyCustomMiddleware : IFunctionsWorkerMiddleware
    {
        public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {
            // This is added pre-function execution, function will have access to this information
            // in the context.Items dictionary
            context.Items.Add("middlewareitem", "Hello, from middleware");

            await next(context);

            // This happens after function execution. We can inspect the context after the function
            // was invoked
            if (context.Items.TryGetValue("functionitem", out object value) && value is string message)
            {
                ILogger logger = context.GetLogger<MyCustomMiddleware>();

                logger.LogInformation("From function: {message}", message);
            }
        }
    }
}
