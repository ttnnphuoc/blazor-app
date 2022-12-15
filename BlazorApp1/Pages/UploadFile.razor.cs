using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using BlazorApp1;
using BlazorApp1.Shared;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Azure.Storage.Blobs;
using Azure;

namespace BlazorApp1.Pages
{
    public partial class UploadFile
    {
        private List<IBrowserFile> loadedFiles = new();
        private long maxFileSize = 1024 * 15;
        private int maxAllowedFiles = 3;
        private bool isLoading;
        private string path = "";
        private BlobContainerClient blobContainerClient;
        private List<string> listImage = new List<string>();
        private string msg = "";
        protected override async Task OnInitializedAsync()
        {

            blobContainerClient = new BlobContainerClient("DefaultEndpointsProtocol=https;AccountName=ttnnphuoc;AccountKey=5IdDhjf0foKEgXPArCLTo4Sx4qdZUYo5LLs+cczZy22cJVa8Rx7hNuwROiAC6rwuL0GDLM6oQiA4+AStcgNpYA==;EndpointSuffix=core.windows.net", "mysample");
        }

        private async Task LoadFiles(InputFileChangeEventArgs e)
        {
            isLoading = true;
            loadedFiles.Clear();
            msg = "";
            foreach (var ufile in e.GetMultipleFiles(maxAllowedFiles))
            {

                path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", ufile.Name);
                try
                {
                    await using (FileStream fs = new(path, FileMode.Create))
                    {
                        await ufile.OpenReadStream(2000000).CopyToAsync(fs);
                    }

                    BlobClient blobClient = blobContainerClient.GetBlobClient(ufile.Name);

                    FileStream fileStream = File.OpenRead(path);
                    await blobClient.UploadAsync(fileStream, true);
                    fileStream.Close();
                    File.Delete(path);
                }
                catch (Exception exp)
                {
                    msg = "Max size is 2MB";
                }
            }

            isLoading = false;
        }
        private async Task onRefesh()
        {
            listImage = new List<string>();
            await ListBlobsFlatListing(blobContainerClient, 10);
        }
        private async Task ListBlobsFlatListing(BlobContainerClient blobContainerClient,
                                               int? segmentSize)
        {
            try
            {
                isLoading = true;
                // Call the listing operation and return pages of the specified size.
                var resultSegment = blobContainerClient.GetBlobsAsync()
                    .AsPages(default, segmentSize);

                // Enumerate the blobs returned for each page.
                await foreach (var blobPage in resultSegment)
                {
                    foreach (var blobItem in blobPage.Values)
                    {
                        listImage.Add("https://ttnnphuoc.blob.core.windows.net/mysample/" + blobItem.Name);
                    }

                    Console.WriteLine();
                }
            }
            catch (RequestFailedException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                throw;
            }
            isLoading = false;
        }
    }
}