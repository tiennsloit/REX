using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Google.Apis.Drive.v2;
using Google.Apis.Auth.OAuth2;
using System.Threading;
using Google.Apis.Services;
using System.Collections;
using Google.Apis.Drive.v2.Data;
using System.Collections.Generic;
using REX.Core.Services;
using REX.Data;

namespace REX.UnitTest
{
    [TestClass]
    public class UnitTest1:TestBase
    {

        [TestMethod]
        public void CreateContact()
        {
            var contactService = this.GetService<IContactService>();
            var fav = new Favourite
            {
                IsCurrently = true,
                Price1 = 400,
                Price2 = 600,
                RiceTypeId = 1,
                Weight = 6
            };

            contactService.CreateContact(new Contact
            {
                Address = "12A Trieu Viet Vuong, P9, Da Lat, Lam Dong",
                DistrictId = 1,
                FaceBookName = "tiens facebook",
                HowManyDaysOfConsume = 20,
                HowManyWeightOfConsume = 4,
                Name = "Tien Nguyen",
                NextShipDate = DateTime.Now,
                Phone1 = "0902156066",
                Phone2 = "0903032892",
                ReasonNotSatisfied = "no reason",
                TimeCanReceivedId = 1,
                Satisfied = "",
                Unsatisfied = "",
                Favourites = new List<Favourite> { fav }
            });
        }

        public Contact TemplateNewContact(string name)
        {
            var fav = new Favourite
            {
                IsCurrently = true,
                Price1 = 400,
                Price2 = 700,
                RiceTypeId = 1,
                Weight = 6

            };
            return new Contact
            {
                Address = "12A Trieu Viet Vuong, P9, Da Lat, Lam Dong",
                DistrictId = 1,
                FaceBookName = "tiens facebook",
                HowManyDaysOfConsume = 20,
                HowManyWeightOfConsume = 4,
                Name = name,
                NextShipDate = DateTime.Now,
                Phone1 = "0902156066",
                Phone2 = "0903032892",
                ReasonNotSatisfied = "no reason 1",
                TimeCanReceivedId = 1,
                Satisfied = "",
                Unsatisfied = "",
                Favourites = new List<Favourite> { fav }
            };
        }

        [TestMethod]
        public void UpdateContact_NewFav()
        {
            var contactService = this.GetService<IContactService>();
            var ct = TemplateNewContact("Bich Tuyen");
            //create a contact first
            contactService.CreateContact(ct);
            ct.Phone1 = "abc";
            //make a new favourite
            contactService.UpdateContact(ct);

            var updatedContact = contactService.GetContact("Bich Tuyen");
            //clean test data
            contactService.RemoveContact("Bich Tuyen");
            Assert.AreEqual("abc", updatedContact.Phone1);

        }

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
