using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Google.Apis.Drive.v2;
using Google.Apis.Auth.OAuth2;
using System.Threading;
using Google.Apis.Services;
using System.Collections;
using Google.Apis.Drive.v2.Data;
using System.Collections.Generic;

namespace REX.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
       
        [TestMethod]
        public void TestMethod1()
        {
            var services = new DriveService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyAR-K-trsPvAJfkcjBTubxW-wKd0Rv4TYo",  // from https://console.developers.google.com (Public API access)
                ApplicationName = "Drive API Sample",
            });
            File file = services.Files.Get("1WWTx3lR_0MybYWQBp1ZQYPcNM0KYitrbTEafBXhHGiw").Execute();

            foreach (var link in file.ExportLinks)
            {
                if (link.Key == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    try
                    {
                        var x = services.HttpClient.GetByteArrayAsync(link.Value);
                        byte[] arrBytes = x.Result;
                        System.IO.File.WriteAllBytes("C:\\Projects\\excelfile.xlsx", arrBytes);

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("An error occurred: " + e.Message);

                    }
                }
                else
                {
                    // The file doesn't have any content stored on Drive.

                }
            }
            

        }

        public static IList GetFiles(DriveService service, string search)
        {

            IList Files = new List<File>();

            try
            {
                //List all of the files and directories for the current user.  
                // Documentation: https://developers.google.com/drive/v2/reference/files/list
                FilesResource.ListRequest list = service.Files.List();
                list.MaxResults = 1000;
                if (search != null)
                {
                    list.Q = search;
                }
                FileList filesFeed = list.Execute();

                //// Loop through until we arrive at an empty page
                while (filesFeed.Items != null)
                {
                    // Adding each item  to the list.
                    foreach (File item in filesFeed.Items)
                    {
                        Files.Add(item);
                    }

                    // We will know we are on the last page when the next page token is
                    // null.
                    // If this is the case, break.
                    if (filesFeed.NextPageToken == null)
                    {
                        break;
                    }

                    // Prepare the next page of results
                    list.PageToken = filesFeed.NextPageToken;

                    // Execute and process the next page request
                    filesFeed = list.Execute();
                }
            }
            catch (Exception ex)
            {
                // In the event there is an error with the request.
                Console.WriteLine(ex.Message);
            }
            return Files;
        }
    }
}
