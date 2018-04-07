using REX.Core.Model;
using REX.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace REX.API.Controllers
{
    public class ListController : ApiController
    {
        private readonly IDistrictService _districtService;
        private readonly IProductTypeService _riceTypeService;
        private readonly ITimeADayService _timeADayService;
        public ListController(IDistrictService districtService, IProductTypeService riceTypeService, ITimeADayService timeADayService)
        {
            _districtService = districtService;
            _riceTypeService = riceTypeService;
            _timeADayService = timeADayService;
        }
        // GET api/<controller>
        public ListItemsModel Get()
        {
            return new ListItemsModel
            {
                Districts = _districtService.GetDistricts(),
                ProductTypes = _riceTypeService.GetProductTypes(),
                TimesInDay = _timeADayService.GetTimesADay()
            };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
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