using REX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REX.Core.Services
{
    public interface IContactService
    {
        Contact CreateContact(Contact contact);
        void UpdateContact(Contact contact);
        Contact GetContact(string name);
        Contact GetContact(int id);
        void RemoveContact(int contactId);
        void RemoveContact(string contactName);
    }
}
