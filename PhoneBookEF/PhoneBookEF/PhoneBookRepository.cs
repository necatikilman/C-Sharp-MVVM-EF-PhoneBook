using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookEF
{
    internal class PhoneBookRepository
    {
        PhoneBookDB phoneBook = new PhoneBookDB();

        internal List<Contact> GetAllContacts()
        {
            return phoneBook.Contacts.ToList();
        }
        internal void SaveContact(Contact contact)
        {
            phoneBook.Contacts.Add(contact);
            phoneBook.SaveChanges();
        }
        internal void UpdateContact(Contact contact)
        {
            Contact contactToUpdate = phoneBook.Contacts.First(c => c.ID == contact.ID);
            contactToUpdate.Lastname = contact.Lastname;
            contactToUpdate.Firstname = contact.Firstname;
            contactToUpdate.Email = contact.Email;
            contactToUpdate.PhoneNumber = contact.PhoneNumber;
            phoneBook.SaveChanges();
        }
        internal void DeleteContact(Contact contact)
        {
            Contact contactToDelete = phoneBook.Contacts.First(c => c.ID == contact.ID);
            phoneBook.Contacts.Remove(contactToDelete);
            phoneBook.SaveChanges();
        }
    }
}
