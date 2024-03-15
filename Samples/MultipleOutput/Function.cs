using Azure.Storage.Queues;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Samples.MultipleOutput
{
    public class Function
    {
        private readonly ILogger<Function> _logger;

        public Function(ILogger<Function> logger)
        {
            _logger = logger;
        }

        [Function("MultipleOutput")]
        public async Task<MyCustomOutput> RunNew(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "multipleout")] HttpRequest req)
        {
            using StreamReader reader = new(req.Body);
            var requestBody = await reader.ReadToEndAsync();

            var output = new MyCustomOutput();
            output.QueueMessage = requestBody;
            output.BlobContent = requestBody;

            return output;
        }
    }

    public class MyCustomOutput
    {
        [QueueOutput("outputqueue")]
        public string QueueMessage { get; set; }

        [BlobOutput("outputcontainer/output.txt")]
        public string BlobContent { get; set; }

        public IActionResult HttpResponse { get; set; } = new OkResult();
    }
}
