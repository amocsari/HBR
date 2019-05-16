﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;

namespace WebApplication1.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=hbrstorage;AccountKey=qrUsM0vJ6+GXCDpqFc1+6sGP9up6hNy3admWGKDnsdUtjJyrxHUBOMluczT/DhqElOQh4Rm1KOYuZUIkEf3L2Q==;EndpointSuffix=core.windows.net";
            BlobContinuationToken blobContinuationToken = null;

            if (CloudStorageAccount.TryParse(storageConnectionString, out var storageAccount))
            {
                try
                {
                    var cloudBlobClient = storageAccount.CreateCloudBlobClient();

                    var cloudBlobContainer = cloudBlobClient.GetContainerReference("bookcontainer");
                    var result = await cloudBlobContainer.ListBlobsSegmentedAsync(null, blobContinuationToken);
                    blobContinuationToken = result.ContinuationToken;
                    var book = result.Results.First();

                    var reference = await cloudBlobContainer.GetBlobReferenceFromServerAsync("Arifureta Shokugyou de Sekai Saikyou [WN]_01.pdf");

                    var stream = new MemoryStream();
                    try
                    {
                        await reference.DownloadToStreamAsync(stream);
                        stream.Seek(0, SeekOrigin.Begin);
                        return File(stream, "application/pdf");
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

            return null;
        }
    }
}
