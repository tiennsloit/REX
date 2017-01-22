﻿using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;
using Google.Apis.Services;
using REX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Hosting;
using System.Web.Http;

namespace REX.Web.Controllers
{
    public class DriveController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            var services = new DriveService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyAR-K-trsPvAJfkcjBTubxW-wKd0Rv4TYo",  // from https://console.developers.google.com (Public API access)
                ApplicationName = "Drive API Sample",
            });
            File file = services.Files.Get("1WWTx3lR_0MybYWQBp1ZQYPcNM0KYitrbTEafBXhHGiw").Execute();
            var filePath = "~/Drive//" + "excel" + DateTime.Now.Ticks.ToString() + ".xlsx";
            foreach (var link in file.ExportLinks)
            {
                if (link.Key == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    try
                    {
                        var x = services.HttpClient.GetByteArrayAsync(link.Value);
                        byte[] arrBytes = x.Result;
                        System.IO.File.WriteAllBytes(HostingEnvironment.MapPath(filePath), arrBytes);

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

            var excels = new ExcelService();
            excels.ReadExcel(HostingEnvironment.MapPath(filePath));

            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}