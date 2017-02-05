using REX.Data;
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
        public Order CreateOrder(Order order)
        {
            using (var dbContext = new RexDbContext())
            {
                dbContext.Orders.Add(order);
                dbContext.SaveChanges();
            }

            return order;
        }

        public void RemoveOrder(int id)
        {
            using (var dbContext = new RexDbContext())
            {
                var o = dbContext.Orders.Attach(new Order { Id = id});
                dbContext.Orders.Remove(o);
                dbContext.SaveChanges();
            }
        }

        public Order GetOrder(int id)
        {
            var res = new Order();
            using (var dbContext = new RexDbContext())
            {
                res = dbContext.Orders.Where(x => x.Id == id).FirstOrDefault();
            }

            return res;
        }

        public ICollection<Order> GetOrders(int contactId)
        {
            var res = new List<Order>();
            using (var dbContext = new RexDbContext())
            {
                res = dbContext.Orders
                    .Include(e => e.Contact)
                    .Include(e=>e.RiceType)
                    .Include(e=>e.User)
                    .Where(x => x.ContactId == contactId).ToList();
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
                var orderUpdating = dbContext.Orders.Where(x => x.Id == order.Id).FirstOrDefault();
                dbContext.Entry(orderUpdating).CurrentValues.SetValues(order);
                dbContext.SaveChanges();
            }
        }
    }
}
