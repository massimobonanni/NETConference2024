using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace HttpDownload.AspNet
{
    public class HttpDownload
    {
        private readonly ILogger<HttpDownload> _logger;

        public HttpDownload(ILogger<HttpDownload> logger)
        {
            _logger = logger;
        }

        private const string BlobName = "func-cli-x64.msi";

        [Function("Download")]
        public IActionResult RunOld(
            [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req,
            [BlobInput($"data/{BlobName}")] Stream blobStream)
        {
            return new FileStreamResult(blobStream, "application/octet-stream")
            {
                FileDownloadName = BlobName
            };
        }
    }
}
