using REX.Data;
using System.Collections.Generic;

namespace REX.Core.Services
{
    public interface IOrderService
    {
        Order CreateOrder(Order order);
        void RemoveOrder(int id);
        Order GetOrder(int id);
        ICollection<Order> GetOrders(int contactId, bool include = false);
        ICollection<Order> GetOrders(string contactName, bool include = false);
        ICollection<Order> GetOrders();
        Order DefaultNewOrder(int userId, Contact contact);
        Order DefaultNewOrderByProductTypeId(int userId, Contact contact, int? productTypeId);
        void UpdateOrder(Order order);
        void DeleteOrder(int id);
        void FinishOrder(int id);
    }
}