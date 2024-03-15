using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace HttpDownload.OldStyle
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
        public HttpResponseData RunOld(
            [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req,
            [BlobInput($"data/{BlobName}")] Stream blobStream)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "application/octet-stream");
            response.Body = blobStream;
            return response;
        }
    }
}
