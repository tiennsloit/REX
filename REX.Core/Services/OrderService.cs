﻿using REX.Data;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REX.Core.Services
{
    public class OrderService: IOrderService
    {
        private readonly IContactService _contactService;
        public OrderService(IContactService contactService)
        {
            _contactService = contactService;
        }
        public Order CreateOrder(Order order)
        {
            using (var dbContext = new RexDbContext())
            {
                if(order.Contact.Id > 0)
                {
                    //update contact separately because we haven't create a function to update all children of the order, but contact have been done.
                    _contactService.UpdateContact(order.Contact);
                    var updatedContact = _contactService.GetContact(order.Contact.Id);
                    order.Contact = null;//set to null so that the contact/favourite will not be create again in the database (should only the contactId is ok)
                }
                dbContext.Orders.Add(order);
                dbContext.SaveChanges();
            }

            return order;
        }

        /// <summary>
        /// Removes the order from database.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void RemoveOrder(int id)
        {
            using (var dbContext = new RexDbContext())
            {
                var o = dbContext.Orders.Attach(new Order { Id = id});
                dbContext.Orders.Remove(o);
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Set the delete property to true.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void DeleteOrder(int id)
        {
            using (var dbContext = new RexDbContext())
            {
                var o = dbContext.Orders.Where(x => x.Id == id).FirstOrDefault();
                o.IsDeleted = true;
                dbContext.Entry(o).CurrentValues.SetValues(o);
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Set the new attribute to true.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void FinishOrder(int id)
        {
            using (var dbContext = new RexDbContext())
            {
                var o = dbContext.Orders.Where(x => x.Id == id).FirstOrDefault();
                o.IsNew = false;
                dbContext.Entry(o).CurrentValues.SetValues(o);
                dbContext.SaveChanges();
            }
        }

        public Order GetOrder(int id)
        {
            var res = new Order();
            using (var dbContext = new RexDbContext())
            {
                res = dbContext.Orders.Where(x => x.Id == id)
                    .Include(y=>y.Contact.Favourites)
                    .FirstOrDefault();
            }

            return res;
        }

        public ICollection<Order> GetOrders(int contactId, bool include = false)
        {
            var res = new List<Order>();
            using (var dbContext = new RexDbContext())
            {
                if (include)
                {
                    res = dbContext.Orders
                    .Include(e => e.Contact.Favourites.Select(t => t.RiceType))
                    .Include(e => e.RiceType)
                    .Include(e => e.User)
                    .Where(x => x.ContactId == contactId && x.IsDeleted == false).ToList();
                }
                else {
                    res = dbContext.Orders
                    .Where(x => x.ContactId == contactId && x.IsDeleted == false).ToList();
                }
            }

            return res;
        }

        public ICollection<Order> GetOrders(string contactName, bool include = false)
        {
            var res = new List<Order>();
            using (var dbContext = new RexDbContext())
            {
                if (include)
                {
                    res = dbContext.Orders
                                        .Include(e => e.Contact.Favourites.Select(t => t.RiceType))
                                        .Include(e => e.RiceType)
                                        .Include(e => e.User)
                                        .Where(x => x.Contact.Name == contactName && x.IsDeleted == false).ToList();
                }
                else
                {
                    res = dbContext.Orders
                                        .Where(x => x.Contact.Name == contactName && x.IsDeleted == false).ToList();
                }
                
            }

            return res;
        }

        public ICollection<Order> GetOrders()
        {
            var res = new List<Order>();
            using (var dbContext = new RexDbContext())
            {
                res = dbContext.Orders.Where(x => x.IsNew == true).ToList();
            }

            return res;
        }


        public Order DefaultNewOrder(int userId, int contactId)
        {
            var order = DefaultNewOrderOriginal();
            order.UserId = userId;
            order.ContactId = contactId;
            return order;
        }

        private Order DefaultNewOrderOriginal()
        {
            return new Order
            {
                AmountToReceived = 0,
                CoverPrice = 0,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                DateShipped = DateTime.Now,
                IsNew = true,
                OtherFee = 0,
                Paid = 0,
                Price = 0,
                Profit = 0,
                PromoPrice = 0,
                Received = 0,
                ShipFee = 0,
                RiceType1Id = 1,
                Surcharge = 0,
                TotalPrice = 0,
                Weight = 10,
                IsDeleted = false,
            };
        }

        public Order DefaultNewOrder(int userId, Contact contact)
        {
            var order = DefaultNewOrderOriginal();
            order.UserId = userId;
            order.Contact = contact;
            return order;
        }

        public void UpdateOrder(Order order)
        {
            using (var dbContext = new RexDbContext())
            {
                //update contact separately because we haven't create a function to update all children of the order, but contact have been done.
                _contactService.UpdateContact(order.Contact);
                var updatedContact = _contactService.GetContact(order.Contact.Id);
               
                var orderUpdating = dbContext.Orders.Where(x => x.Id == order.Id).FirstOrDefault();
                orderUpdating.Contact = null;//set to null so that the contact/favourite will not be create again in the database (should only the contactId is ok)
                orderUpdating.DateModified = DateTime.Now;
                dbContext.Entry(orderUpdating).CurrentValues.SetValues(order);
                dbContext.SaveChanges();
            }
        }
    }
}
