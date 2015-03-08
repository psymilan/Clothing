using System.Configuration;
using System.Web;
using Clothing.Web.DataModels;
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

        public void DeleteImage(ProductImage image)
        {
            CloudBlobContainer blobContainer = GetCloudBlobContainer();
            CloudBlockBlob b = blobContainer.GetBlockBlobReference(image.ImageName);
            b.Delete();
        }

        public void SaveImage(ProductImage image, HttpPostedFileBase file)
        {
            CloudBlobContainer blobContainer = GetCloudBlobContainer();
            CloudBlockBlob b = blobContainer.GetBlockBlobReference(image.ImageName);
            b.UploadFromStream(file.InputStream);
        }
    }
}