using REX.Data;
using System;
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

        public Order DefaultNewOrder(int userId, int contactId)
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
                UserId = userId,
                Weight = 10,
                ContactId = contactId,
                IsDeleted = false
            };
        }
    }
}
