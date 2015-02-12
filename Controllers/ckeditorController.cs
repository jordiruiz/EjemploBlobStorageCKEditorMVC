using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using ejemploCKEditorAzureStorage.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Configuration;

namespace ejemploCKEditorAzureStorage.Controllers
{
    public class ckeditorController : Controller
    {
        // GET: ckeditor
        public ActionResult Index()
        {
            List<CKImageViewModel> images = new List<CKImageViewModel>();

            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.ConnectionStrings["AzureStorageConnection"].ConnectionString
            );

            // Create the blob client. 
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("nombrecontenedor");

            if (container.CreateIfNotExists())
            {
                // configuramos el contenedor con acceso publico
                var permissions = container.GetPermissions();
                permissions.PublicAccess = Microsoft.WindowsAzure.Storage.Blob.BlobContainerPublicAccessType.Container;
                container.SetPermissions(permissions);
            }

            // Loop over items within the container and output the length and URI.            
            string folderPath = "imagenes/2015/";

            foreach (IListBlobItem item in container.ListBlobs(folderPath, false))
            {
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;

                    images.Add(new CKImageViewModel
                    {
                        Url = item.Uri.ToString()
                    });
                }
            }

            return PartialView(images);
        }

        [HttpPost]
        public ActionResult uploadnow(HttpPostedFileWrapper upload)
        {
            ViewBag.Resultado = "";

            if (upload != null)
            {
                // Retrieve storage account from connection string.
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                    ConfigurationManager.ConnectionStrings["AzureStorageConnection"].ConnectionString
                );

                // Create the blob client.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                // Retrieve reference to a previously created container.
                CloudBlobContainer container = blobClient.GetContainerReference("nombrecontenedor");

                if (container.CreateIfNotExists())
                {
                    // configuramos el contenedor con acceso publico
                    var permissions = container.GetPermissions();
                    permissions.PublicAccess = Microsoft.WindowsAzure.Storage.Blob.BlobContainerPublicAccessType.Container;
                    container.SetPermissions(permissions);
                }

                string imageBlobName = string.Format("imagenes/2015/{0}", upload.FileName);

                // Retrieve reference to the imageBlobName
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(imageBlobName);

                // Create or overwrite the image blob with contents from uploaded file.
                blockBlob.Properties.ContentType = upload.ContentType;
                blockBlob.UploadFromStream(upload.InputStream);

                ViewBag.Resultado = "Imágen subida correctamente";
            }
            
            return PartialView();
        }
    }
}