using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
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
using Google.Apis.Auth.OAuth2;
using System.Threading;
using System.IO;
using Google.Apis.Util.Store;

namespace REX.API.Controllers
{
    public class DriveController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        static string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        static string ApplicationName = "Other client 1";
        //here is how to generate .json key https://developers.google.com/sheets/api/quickstart/dotnet
        public string GetValue()
        {
            //IList<Object> obj = new List<Object>();
            //obj.Add("A2");
            //obj.Add("B2");
            //IList<IList<Object>> values = new List<IList<Object>>();
            //values.Add(obj);

            //SpreadsheetsResource.ValuesResource.AppendRequest request =
            //        service.Spreadsheets.Values.Append(new ValueRange() { Values = values }, spreadsheetId, range);
            //request.InsertDataOption = SpreadsheetsResource.ValuesResource.AppendRequest.InsertDataOptionEnum.INSERTROWS;
            //request.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.RAW;
            //var response = request.Execute();
            var tss = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);

            UserCredential credential;

            using (var stream =
                new FileStream(HostingEnvironment.MapPath("/Key/client_secret.json"), FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/sheets.googleapis.com-dotnet-quickstart.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(HostingEnvironment.MapPath("/Key"), true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }


            var services = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            String range = "Sheet1!AD3:AD3";
            //var file = services.Spreadsheets.Values.Get(StringHelper.GetAppSettingValueOrDefault("DriveFileId", ""), range).Execute();
            //var filePath = "~/Drive//" + "excel" + DateTime.Now.Ticks.ToString() + ".xlsx";
           
            var t =
                    services.Spreadsheets.Values.Get(StringHelper.GetAppSettingValueOrDefault("DriveFileId", ""), range);

            // Prints the names and majors of students in a sample spreadsheet:
            // https://docs.google.com/spreadsheets/d/1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms/edit
            var response = t.Execute();
            IList<IList<Object>> values = response.Values;
            var vr = new Google.Apis.Sheets.v4.Data.ValueRange();
            vr.Values = values;
            values[0][0] = "y";

            var e = services.Spreadsheets.Values.Update(vr, StringHelper.GetAppSettingValueOrDefault("DriveFileId", ""), range);
            e.Execute();
            return values[0][0].ToString();
        }

        // GET api/<controller>/5
        public ICollection<ContactModel> Get(int id)
        {
            var services = new DriveService(new BaseClientService.Initializer()
            {
                ApiKey = StringHelper.GetAppSettingValueOrDefault("DriveAPIKey", ""),  // from https://console.developers.google.com (Public API access)
                ApplicationName = "Drive API Sample",
            });
            var file = services.Files.Get(StringHelper.GetAppSettingValueOrDefault("DriveFileId", "")).Execute();
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