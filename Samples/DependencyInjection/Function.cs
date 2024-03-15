using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Samples.DependencyInjection
{
    public class Function
    {
        private readonly ILogger<Function> _logger;
        private readonly Core.Interfaces.ICoreService _coreService;
        public Function(ILogger<Function> logger, Core.Interfaces.ICoreService coreService)
        {
            _logger = logger;
            _coreService = coreService;
        }

        [Function("DependencyInjection")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get",Route ="di")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var result=this._coreService.DoSomething();
            return new OkObjectResult(result);
        }
    }
}
