using REX.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REX.Core.Services
{
    public class ContactService : IContactService
    {
        public Contact CreateContact(Contact contact)
        {
            Insert(contact);
            return contact;
        }

        public void UpdateContact(Contact contact)
        {
            Update(contact);
        }

        public Contact GetContact(int contactId)
        {
            var contact = new Contact();
            using (var dbContext = new RexDbContext())
            {
                //todo: filter by isActived property
                contact = dbContext.Contacts.Where(x => x.Id == contactId).Include(t=>t.Favourites).FirstOrDefault();
            }

            return contact;
        }

        public ICollection<Contact> GetContacts()
        {
            var contacts = new List<Contact>();
            using (var dbContext = new RexDbContext())
            {
                //todo: filter by isActived property
                contacts = dbContext.Contacts.Include(s=>s.Favourites).ToList();
            }

            return contacts;
        }

        public Contact GetContact(string name)
        {
            var contact = new Contact();
            using (var dbContext = new RexDbContext())
            {
                //todo: filter by isActived property
                contact = dbContext.Contacts.Where(x => x.Name == name).Include(t=>t.Favourites).FirstOrDefault();
            }

            return contact;
        }

        public Contact DefaultNewContact()
        {
            var fav = new Favourite
            {
                IsCurrently = true,
                Price1 = 0,
                Price2 = 0,
                ProductTypeId = 1,
                Weight = 0

            };
            return new Contact
            {
                
                Address = "",
                DistrictId = 1,
                FaceBookName = "",
                HowManyDaysOfConsume = 30,
                HowManyWeightOfConsume = 10,
                Name = "vãng lai",
                NextShipDate = DateTime.Now,
                Phone1 = "",
                Phone2 = "",
                ReasonNotSatisfied = "",
                TimeCanReceivedId = 1,
                Satisfied = "",
                Unsatisfied = "",
                Favourites = new List<Favourite> { fav }
            };
        }

        public void RemoveContact(int contactId)
        {
            using (var dbContext = new RexDbContext())
            {
                var contact = GetContact(contactId);
                dbContext.Contacts.Attach(contact);
                dbContext.Contacts.Remove(contact);
                dbContext.SaveChanges();
            }
        }

        public void RemoveContact(string contactName)
        {
            using (var dbContext = new RexDbContext())
            {
                var contact = GetContact(contactName);
                dbContext.Contacts.Attach(contact);
                dbContext.Contacts.Remove(contact);
                dbContext.SaveChanges();
            }
        }

        private void Update(Contact model)
        {
            using (var dbContext = new RexDbContext())
            {
                var existingParent = dbContext.Contacts
                .Where(p => p.Id == model.Id)
                .Include(p => p.Favourites)
                .SingleOrDefault();

                if (existingParent != null)
                {
                    // Update parent
                    dbContext.Entry(existingParent).CurrentValues.SetValues(model);

                    // Delete children
                    foreach (var existingChild in existingParent.Favourites.ToList())
                    {
                        if (!model.Favourites.Any(c => c.Id == existingChild.Id))
                            dbContext.Favourites.Remove(existingChild);
                    }

                    // Update and Insert children
                    foreach (var childModel in model.Favourites)
                    {
                        var existingChild = existingParent.Favourites
                            .Where(c => c.Id == childModel.Id)
                            .SingleOrDefault();

                        if (existingChild != null)
                            // Update child
                            dbContext.Entry(existingChild).CurrentValues.SetValues(childModel);
                        else
                        {
                            // Insert child
                            var newChild = new Favourite
                            {
                                ContactId = childModel.ContactId,
                                IsCurrently = childModel.IsCurrently,
                                Price1 = childModel.Price1,
                                Price2 = childModel.Price2,
                                ProductType = childModel.ProductType,
                                ProductTypeId = childModel.ProductTypeId,
                                Weight = childModel.Weight
                            };
                            existingParent.Favourites.Add(newChild);
                        }
                    }

                    dbContext.SaveChanges();
                }
            }
        }

        private void Insert(Contact model)
        {
            using (var dbContext = new RexDbContext())
            {
                dbContext.Contacts.Add(model);
                dbContext.SaveChanges();
            }
        }

    }
}
