using Azure.Storage.Blobs;
using BlazorBookApp.Data;
using BlazorBookApp.Services.Interfaces;

namespace BlazorBookApp.Services
{
    public class FileService : IFileService
    {
        private readonly string azureConnectionString;
        public FileService(IConfiguration configuration) 
        {
            azureConnectionString = configuration.GetConnectionString("AzureConnectionString");
        }
        public async Task<string> UploadFile(UploadedFile uploadedFile)
        {
            try
            {
                // Azure connection string and container name passed as an argument to get the Blob reference of the container.
                var container = new BlobContainerClient(azureConnectionString, "some");
                var createResponse = await container.CreateIfNotExistsAsync();

                // If container successfully created, then set public access type to Blob.
                if (createResponse != null && createResponse.GetRawResponse().Status == 201)
                    await container.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

                // Method to create a new Blob client.
                var blob = container.GetBlobClient(uploadedFile.FileName);

                // If a blob with the same name exists, then we delete the Blob and its snapshots.
                await blob.DeleteIfExistsAsync(Azure.Storage.Blobs.Models.DeleteSnapshotsOption.IncludeSnapshots);

                // Create a file stream and use the UploadSync method to upload the Blob.
                Stream stream = new MemoryStream(uploadedFile.FileContent);
                await blob.UploadAsync(stream);
                return uploadedFile.FileName;

            }
            catch (Exception e)
            {
                throw new Exception("Upload failed");
            }
        }

        public string GetBlob(string fileName)
        {
            var container = new BlobContainerClient(azureConnectionString, "some");
            var blob = container.GetBlobClient(fileName);

            return blob.Uri.AbsoluteUri;
        }
    }
}
