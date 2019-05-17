using BLL.Services.Interface;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Implementation
{
    public class BlobStorageService : IBlobStorageService
    {
        private const string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=hbrstorage;AccountKey=qrUsM0vJ6+GXCDpqFc1+6sGP9up6hNy3admWGKDnsdUtjJyrxHUBOMluczT/DhqElOQh4Rm1KOYuZUIkEf3L2Q==;EndpointSuffix=core.windows.net";
        private const string containerName = "bookcontainer";
        public async Task<MemoryStream> GetFileFromStorageAsStream(int bookId, string extension)
        {
            if (CloudStorageAccount.TryParse(storageConnectionString, out var storageAccount))
            {
                try
                {
                    var cloudBlobClient = storageAccount.CreateCloudBlobClient();
                    var cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);

                    //var reference = await cloudBlobContainer.GetBlobReferenceFromServerAsync("Arifureta Shokugyou de Sekai Saikyou [WN]_01.pdf");

                    var bbReference = cloudBlobContainer.GetBlockBlobReference("Arifureta Shokugyou de Sekai Saikyou [WN]_01.pdf");
                    bbReference = cloudBlobContainer.GetBlockBlobReference($"{bookId}.{extension}");

                    var reference = await cloudBlobContainer.GetBlobReferenceFromServerAsync($"{bookId}.{extension}");
                    var stream = new MemoryStream();
                    try
                    {
                        await reference.DownloadToStreamAsync(stream);
                        stream.Seek(0, SeekOrigin.Begin);
                        return stream;
                    }
                    catch
                    {
                        stream.Dispose();
                        throw;
                    }
                }
                catch (Exception e)
                {
                }
            }
            throw new HbrException("Hiba a fájlszerver elérés közben");
        }
    }
}
