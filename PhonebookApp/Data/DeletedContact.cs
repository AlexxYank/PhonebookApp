using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace PhonebookApp.Data
{
    [Table("DeletedContacts")]
    public class DeletedContact
    {
        DeletedContact currContact;

        public DeletedContact(int ContactID)
        {
            GetContact(ContactID);
        }

        public DeletedContact()
        {

        }

        [Column("ContactID")]
        [Key]
        [Required]
        public int ContactID { get; set; }

        [Column("DateDeleted")]
        [Required]
        public DateTime DateDeleted { get; set; }

        [Column("UserName")]
        public string UserName { get; set; }

        private void GetContact(int id)
        {
            using (PhonebookDbContext db = new PhonebookDbContext())
            {
                currContact = db.DeletedContacts.FirstOrDefault(c => c.ContactID == id);
            }

            this.ContactID = currContact.ContactID;
            this.DateDeleted = currContact.DateDeleted;
        }
    }
}
