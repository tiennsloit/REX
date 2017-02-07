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
        ICollection<Order> GetOrders(string contactName);
        ICollection<Order> GetOrders();
        Order DefaultNewOrder(int userId, int contactId);
        Order DefaultNewOrder(int userId, Contact contact);
        void UpdateOrder(Order order);
    }
}