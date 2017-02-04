using REX.Core.Services;
using REX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace REX.API.Controllers
{
    public class OrderController : ApiController
    {
        private readonly IOrderService _orderService;
        private readonly IContactService _contactService;
        public OrderController(IOrderService orderService, IContactService contactService)
        {
            _orderService = orderService;
            _contactService = contactService;
        }
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        [Route("user/{userId}/order")]
        public Order GetOrderByDefault(int userId)
        {
            var defaultContact = _contactService.DefaultNewContact();
            var defaultOrder = _orderService.DefaultNewOrder(userId, defaultContact);
            return defaultOrder;
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