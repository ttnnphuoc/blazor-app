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
        private List<BlobItem> listImage = new List<BlobItem>();
        private string msg = "";
        protected override async Task OnInitializedAsync()
        {

            blobContainerClient = new BlobContainerClient("", "");

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
            await ListBlobsFlatListing(blobContainerClient, 10);
        }

        private async Task onDelete(BlobItem item)
        {
            BlobClient blobClient = blobContainerClient.GetBlobClient(item.FileName);
            await blobClient.DeleteIfExistsAsync();
            listImage.Remove(item);
            StateHasChanged();
        }
        private async Task ListBlobsFlatListing(BlobContainerClient blobContainerClient,
                                               int? segmentSize)
        {
            listImage.Clear();
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
                        listImage.Add(new BlobItem(blobItem.Name, "https://ttnnphuoc.blob.core.windows.net/mysample/" + blobItem.Name));
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

        public class BlobItem
        {
            public string FileName { set; get; }
            public string Url { set; get; }
            public BlobItem(string fileName, string url)
            {
                this.FileName = fileName;
                this.Url = url;
            }
        }
    }
}