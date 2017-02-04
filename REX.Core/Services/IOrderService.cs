using REX.Data;
using System.Collections.Generic;

namespace REX.Core.Services
{
    public interface IOrderService
    {
        Order CreateOrder(Order order);
        void RemoveOrder(int id);
        Order GetOrder(int id);
        ICollection<Order> GetOrders(int contactId);
        ICollection<Order> GetOrders();
        Order DefaultNewOrder(int userId, int contactId);
        void UpdateOrder(Order order);
    }
}