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
using System.Linq;

namespace REX.UnitTest
{
    [TestClass]
    public class ContactTest:TestBase
    {
        public IContactService contactService;
        [TestInitialize()]
        public void Init()
        {
            contactService = this.GetService<IContactService>();
        }

        [TestMethod]
        public void CreateContact()
        {
            var newContact = contactService.CreateContact(TemplateNewContact("test new contact"));
            contactService.RemoveContact("test new contact");
            Assert.AreNotEqual(null, newContact);
            var contactRefetch = contactService.GetContact("test new contact");
            Assert.AreEqual(null, contactRefetch);
        }

        public Contact TemplateNewContact(string name)
        {
            var fav = new Favourite
            {
                IsCurrently = true,
                Price1 = 400,
                Price2 = 700,
                ProductTypeId = 1,
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
        public void UpdateContact_NewFavourite()
        {
            
            var ct = TemplateNewContact("Bich Tuyen");
            //create a contact first
            contactService.CreateContact(ct);
            ct.Phone1 = "abc1";
            //make a new favourite
            ct.Favourites = new List<Favourite> { new Favourite {
            IsCurrently = true,
                Price1 = 400,
                Price2 = 700,
                ProductTypeId = 1,
                Weight = 10} };
            contactService.UpdateContact(ct);

            var updatedContact = contactService.GetContact("Bich Tuyen");
            //clean test data
            contactService.RemoveContact("Bich Tuyen");
            Assert.AreEqual("abc1", updatedContact.Phone1);
            Assert.AreEqual(1, updatedContact.Favourites.Where(x=>x.Weight == 10).Count());

        }

        [TestMethod]
        public void UpdateContact_AddOneMoreFavourite()
        {
            
            var ct = TemplateNewContact("Bich Tuyen");
            //create a contact first
            contactService.CreateContact(ct);
            ct.Phone1 = "abc1";
            //make a new favourite
            ct.Favourites.Add( new Favourite {
            IsCurrently = true,
                Price1 = 400,
                Price2 = 700,
                ProductTypeId = 1,
                Weight = 999} );
            contactService.UpdateContact(ct);

            var updatedContact = contactService.GetContact("Bich Tuyen");
            //clean test data
            contactService.RemoveContact("Bich Tuyen");
            Assert.AreEqual("abc1", updatedContact.Phone1);
            Assert.AreEqual(1, updatedContact.Favourites.Where(x => x.Weight == 6).Count());
            Assert.AreEqual(1, updatedContact.Favourites.Where(x => x.Weight == 999).Count());
            Assert.AreEqual(2, updatedContact.Favourites.Count());
            //check if the test data has been removed
            var refetch = contactService.GetContact("Bich Tuyen");
            Assert.AreEqual(null, refetch);

        }

        [TestMethod]
        public void UpdateContact_UpdateExistingFavourite()
        {
           
            var ct = TemplateNewContact("Bich Tuyen");
            //create a contact first
            contactService.CreateContact(ct);
            ct.Phone1 = "abc1";
            //make a new favourite
            ct.Favourites.First().Price1 = 100000;
            contactService.UpdateContact(ct);

            var updatedContact = contactService.GetContact("Bich Tuyen");
            //clean test data
            contactService.RemoveContact("Bich Tuyen");
            Assert.AreEqual(100000, updatedContact.Favourites.First().Price1);
            //check if the test data has been removed
            var refetch = contactService.GetContact("Bich Tuyen");
            Assert.AreEqual(null, refetch);

        }

        [TestMethod]
        public void RemoveContact()
        {
            var ct = TemplateNewContact("Contact1");
            //create a contact first
            contactService.CreateContact(ct);

            var contactNew = contactService.GetContact("Contact1");
            Assert.AreNotEqual(null, contactNew);

            contactService.RemoveContact("Contact1");
            var contactRemoved = contactService.GetContact("Contact1");
            Assert.AreEqual(null, contactRemoved);
        }


        //[TestMethod]
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
