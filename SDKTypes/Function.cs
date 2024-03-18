using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace SDKTypes
{
    public class Function
    {
        private readonly ILogger<Function> _logger;
        private readonly BlobServiceClient _blobServiceClient;

        public Function(ILogger<Function> logger, BlobServiceClient client)
        {
            _logger = logger;
            _blobServiceClient = client;
        }

        [Function("WriteToBlob")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous,  "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var blobContainer=_blobServiceClient.GetBlobContainerClient("data");

            blobContainer.CreateIfNotExists();

            var blobName=$"{Guid.NewGuid()}.txt";
            var blobClient=blobContainer.GetBlobClient(blobName);
            await blobClient.UploadAsync(req.Body);
            
            return new OkObjectResult($"Blob {blobName} created!");
        }
    }
}
