using Microsoft.VisualStudio.TestTools.UnitTesting;
using REX.Core.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
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
        public void CreateOrder_With_Existing_Contact()
        {
            var users = userService.GetUsers();
            var contacts = contactService.GetContacts();
            var newOrder = orderService.CreateOrder(orderService.DefaultNewOrder(users.First().Id, contacts.First()));
            Assert.AreNotEqual(null, newOrder);
            orderService.RemoveOrder(newOrder.Id);
            var refetch = orderService.GetOrder(newOrder.Id);
            Assert.AreEqual(null, refetch);
        }

        [TestMethod]
        public void CreateOrder_With_New_Contact()
        {
            var users = userService.GetUsers();
            var contact = contactService.DefaultNewContact();
            contact.Name = "contact1";
            var order = orderService.DefaultNewOrder(users.First().Id, contact);
            var resultCreated = orderService.CreateOrder(order);

            //check success created?
            var refetchOrder = orderService.GetOrder(resultCreated.Id);
            Assert.AreNotEqual(null, refetchOrder);

            //check there is 1 order and 1 contact
            var refetchContact = contactService.GetContact("contact1");
            Assert.AreNotEqual(null, refetchContact);

            //remove contact and order
            //orderService.RemoveOrder(refetchOrder.Id); //not work
            contactService.RemoveContact("contact1");

            //check has remove test data: both contact and order
            var removedOrder = orderService.GetOrder(resultCreated.Id);
            Assert.AreEqual(null, removedOrder);
            var removedContact = contactService.GetContact("contact1");
            Assert.AreEqual(null, removedContact);

        }

        [TestMethod]
        public void CreateOrder_Using_Default()
        {
            string json = @"{
                'id': 0,
                'contactId': 0,
                'contact': {
                    'id': 0,
                    'name': '',
                    'faceBookName': '',
                    'phone1': '',
                    'phone2': '',
                    'address': '',
                    'districtId': 1,
                    'district': null,
                    'favourites': [
                        {
                            'id': 0,
                            'contactId': 0,
                            'productType': null,
                            'productTypeId': 1,
                            'price1': 0,
                            'price2': 0,
                            'isCurrently': true,
                            'weight': 0
                        }
                    ],
                    'timeCanReceived': null,
                    'timeCanReceivedId': 1,
                    'howManyDaysOfConsume': 30,
                    'howManyWeightOfConsume': 10,
                    'nextShipDate': '2018-03-11T18:04:57.339366+07:00',
                    'satisfied': '',
                    'unsatisfied': '',
                    'reasonNotSatisfied': ''
                },
                'weight': 10,
                'productType': null,
                'productTypeId': 1,
                'price': 0,
                'surcharge': 0,
                'amountToReceived': 0,
                'coverPrice': 0,
                'promoPrice': 0,
                'totalPrice': 0,
                'shipFee': 0,
                'otherFee': 0,
                'profit': 0,
                'paid': 0,
                'received': 0,
                'dateShipped': '2018-03-11T18:04:57.3403647+07:00',
                'isNew': true,
                'dateCreated': '2018-03-11T18:04:57.3403647+07:00',
                'dateModified': '2018-03-11T18:04:57.3403647+07:00',
                'userId': 1,
                'user': null,
                'isDeleted': false
            }";

            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                var order =  Newtonsoft.Json.JsonConvert.DeserializeObject<Data.Order>(json);
               
                orderService.CreateOrder(order);
            }

        }

        [TestMethod]
        public void UpdateOrder()
        {
            var users = userService.GetUsers();
            var contacts = contactService.GetContacts();
            var newOrder = orderService.CreateOrder(orderService.DefaultNewOrder(users.First().Id, contacts.First()));
            Assert.AreNotEqual(null, newOrder);

            //update the new order
            newOrder.Price = 500;
            newOrder.Paid = 1000;
            newOrder.ProductTypeId = 2;
            orderService.UpdateOrder(newOrder);

            var updatedOrder = orderService.GetOrder(newOrder.Id);
            Assert.AreEqual(500, updatedOrder.Price);
            Assert.AreEqual(1000, updatedOrder.Paid);

            orderService.RemoveOrder(newOrder.Id);
            var refetch = orderService.GetOrder(newOrder.Id);
            Assert.AreEqual(null, refetch);
        }

        [TestMethod]
        public void TestDeleteOrder()
        {
            var users = userService.GetUsers();
            var contact = contactService.DefaultNewContact();
            contact.Name = "contact1";
            var order = orderService.DefaultNewOrder(users.First().Id, contact);
            var resultCreated = orderService.CreateOrder(order);

            orderService.DeleteOrder(resultCreated.Id);

            var getOrderAgain = orderService.GetOrder(resultCreated.Id);
            Assert.AreEqual(true, getOrderAgain.IsDeleted);

            orderService.RemoveOrder(getOrderAgain.Id);
            contactService.RemoveContact("contact1");
            var getAgainOrder = orderService.GetOrder(resultCreated.Id);
            Assert.AreEqual(null, getAgainOrder);
        }

        [TestMethod]
        public void TestGetTopOrders()
        {
            var result = orderService.GetOrders().Take(10).OrderByDescending(x => x.DateModified).ToList();
        }
    }
}
