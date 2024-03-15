using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using FromBodyAttribute = Microsoft.Azure.Functions.Worker.Http.FromBodyAttribute;

namespace Samples.AspNet
{
    public class Function
    {
        private readonly ILogger<Function> _logger;

        public Function(ILogger<Function> logger)
        {
            _logger = logger;
        }

        [Function("AddContacts")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "contacts")] HttpRequest req,
            [FromBody] IEnumerable<Contact> contacts)
        {
            if (contacts == null)
                return new BadRequestResult();

            foreach (var item in contacts)
            {
                _logger.LogInformation("Contact: {item}", item);
            }

            return new OkResult();
        }
    }
}
