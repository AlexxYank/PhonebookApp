using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhonebookApp.Data;

namespace PhonebookApp.Models
{
    public class ContactModel : PageModel
    {
        public List<Contact> Contacts { get; set; }

        public ContactModel()
        {
            Contacts = new List<Contact>();
        }

        public void AddContacts(Contact contact)
        {
            using PhonebookDbContext db = new PhonebookDbContext();

            try
            {
                if (contact.ID == 0)
                {
                    db.Contacts.Add(contact);
                    db.SaveChanges();
                }
                else
                {
                    db.Contacts.Update(contact);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
        }

        //
        public void DeleteContact(Contact contact)
        {
            using PhonebookDbContext db = new PhonebookDbContext();
            try
            {
                contact.IsDeleted = true;
                db.Contacts.Update(contact);
                db.SaveChanges();

                db.DeletedContacts.Add(new DeletedContact { ContactID = contact.ID, DateDeleted = DateTime.Now });
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public void RecoverContact(int ID)
        {
            using PhonebookDbContext db = new PhonebookDbContext();
            try
            {
                Contact contact = db.Contacts.FirstOrDefault(c => c.ID == ID);

                contact.IsDeleted = false;
                db.Contacts.Update(contact);
                db.SaveChanges();

                DeletedContact dcontact = new DeletedContact(contact.ID);
                db.DeletedContacts.Remove(dcontact);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public void SearchContacts(string userName, string filter)
        {
            using PhonebookDbContext db = new PhonebookDbContext();

            IQueryable<Contact> FilteredContacts = from row in db.Contacts.Where(c => c.UserName == userName)
                                                   orderby row.FirstName
                                                   where row.FirstName.Contains(filter) || row.LastName.Contains(filter)
                                                   || row.PhoneNumber.Contains(filter)
                                                   select row;

            Contacts = FilteredContacts.ToList();
        }

        public void RemoveDeletedContacts()
        {
            using PhonebookDbContext db = new PhonebookDbContext();
            List<DeletedContact> forDelete = db.DeletedContacts.Where(c => c.DateDeleted.AddDays(30) > DateTime.Now).ToList();

            foreach (var item in forDelete)
            {
                Contact contact = new Contact(item.ContactID);
                db.Contacts.Remove(contact);
                db.SaveChanges();

                db.DeletedContacts.Remove(item);
                db.SaveChanges();
            }
        }

    }
}

