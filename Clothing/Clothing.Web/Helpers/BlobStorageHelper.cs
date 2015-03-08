using System.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Clothing.Web.Helpers
{
    public class BlobStorageHelper
    {
        private const string Url = "https://shirinvahidi.blob.core.windows.net/images/";

        public CloudBlobContainer GetCloudBlobContainer()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer blobContainer = blobClient.GetContainerReference("images");

            return blobContainer;
        }

        public string GetUrl()
        {
            return Url;
        }
    }
}