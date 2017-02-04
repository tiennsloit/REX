using Microsoft.VisualStudio.TestTools.UnitTesting;
using REX.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REX.UnitTest
{
    [TestClass]
    public class OrderTest:TestBase
    {
        public IContactService contactService;
        public IOrderService orderService;
        public IUserService userService;
        [TestInitialize()]
        public void Init()
        {
            contactService = this.GetService<IContactService>();
            orderService = this.GetService<IOrderService>();
            userService = this.GetService<IUserService>();
        }

        [TestMethod]
        public void CreateOrder()
        {
            var users = userService.GetUsers();
            var contacts = contactService.GetContacts();
            var newOrder = orderService.CreateOrder(orderService.DefaultNewOrder(users.First().Id, contacts.First().Id));
            Assert.AreNotEqual(null, newOrder);
            orderService.RemoveOrder(newOrder.Id);
            var refetch = orderService.GetOrder(newOrder.Id);
            Assert.AreEqual(null, refetch);
        }

        [TestMethod]
        public void UpdateOrder()
        {
            var users = userService.GetUsers();
            var contacts = contactService.GetContacts();
            var newOrder = orderService.CreateOrder(orderService.DefaultNewOrder(users.First().Id, contacts.First().Id));
            Assert.AreNotEqual(null, newOrder);

            //update the new order
            newOrder.Price = 500;
            newOrder.Paid = 1000;
            newOrder.RiceType1Id = 2;
            orderService.UpdateOrder(newOrder);

            var updatedOrder = orderService.GetOrder(newOrder.Id);
            Assert.AreEqual(500, updatedOrder.Price);
            Assert.AreEqual(1000, updatedOrder.Paid);

            orderService.RemoveOrder(newOrder.Id);
            var refetch = orderService.GetOrder(newOrder.Id);
            Assert.AreEqual(null, refetch);
        }
    }
}
