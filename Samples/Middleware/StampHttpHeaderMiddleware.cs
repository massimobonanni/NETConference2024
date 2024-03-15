using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Azure.Functions.Worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samples.Middleware
{
    /// <summary>
    /// Middleware to stamp a response header on the result of http trigger invocation.
    /// </summary>
    internal sealed class StampHttpHeaderMiddleware : IFunctionsWorkerMiddleware
    {
        public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {
            var requestData = await context.GetHttpRequestDataAsync();

            string correlationId;
            if (requestData!.Headers.TryGetValues("x-correlationId", out var values))
            {
                correlationId = values.First();
            }
            else
            {
                correlationId = Guid.NewGuid().ToString();
            }

            var httpContext = context.GetHttpContext();
            httpContext.Response.Headers.Add("x-correlationId", correlationId);

            await next(context);

            
        }
    }
}
