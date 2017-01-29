using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;
using Google.Apis.Services;
using Newtonsoft.Json;
using REX.Core;
using REX.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Mvc;

namespace REX.API.Controllers
{
    public class DriveController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        public string GetValue()
        {
            return "";
        }

        // GET api/<controller>/5
        public ICollection<ContactModel> Get(int id)
        {
            var services = new DriveService(new BaseClientService.Initializer()
            {
                ApiKey = StringHelper.GetAppSettingValueOrDefault("DriveAPIKey", ""),  // from https://console.developers.google.com (Public API access)
                ApplicationName = "Drive API Sample",
            });
            File file = services.Files.Get(StringHelper.GetAppSettingValueOrDefault("DriveFileId", "")).Execute();
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
            var contacts = excels.ReadExcel(HostingEnvironment.MapPath(filePath));

            return contacts;
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