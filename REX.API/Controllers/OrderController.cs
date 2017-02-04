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
        public string Get()
        {
            return "";
        }

        // GET api/<controller>/5
        public ICollection<Order> GetOrder(int contactId)
        {
            return _orderService.GetOrders(contactId);
        }

        [Route("orderByDefault/{userId}")]
        public Order GetOrderByDefault(int userId)
        {
            var defaultContact = _contactService.DefaultNewContact();
            var defaultOrder = _orderService.DefaultNewOrder(userId, defaultContact);
            return defaultOrder;
        }

        // POST api/<controller>
        public string PostOrder(Order order)
        {
            _orderService.CreateOrder(order);
            return "true";
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