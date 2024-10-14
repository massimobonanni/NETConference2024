using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.OpenAI.TextCompletion;
using Microsoft.Extensions.Logging;

namespace Samples.OpenAI
{
    public class OpenAIFunction
    {
        private readonly ILogger<OpenAIFunction> _logger;

        public OpenAIFunction(ILogger<OpenAIFunction> logger)
        {
            _logger = logger;
        }

        [Function(nameof(WhoIs))]
        public static IActionResult WhoIs(
            [HttpTrigger(AuthorizationLevel.Function, Route = "whois/{name}")] HttpRequest req,
            [TextCompletionInput("Who is {name}?", Model = "%AZURE_OPENAI_MODELNAME%", Temperature ="0,75")] TextCompletionResponse response)
        {
            return new OkObjectResult(response.Content);
        }

        [Function(nameof(GenericCompletion))]
        public static IActionResult GenericCompletion(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req,
            [TextCompletionInput("{Prompt}", Model = "%AZURE_OPENAI_MODELNAME%", Temperature = "0,75")] TextCompletionResponse response,
        ILogger log)
        {
            string text = response.Content;
            return new OkObjectResult(text);
        }
    }
}
