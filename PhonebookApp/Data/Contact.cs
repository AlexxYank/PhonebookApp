using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace PhonebookApp.Data
{
    [Table("Contacts")]
    public class Contact
    {
        [Column("ContactID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ID { get; set; }

        [Column("UserName")]
        public string UserName { get; set; }

        [Column("PhoneNumber")]
        [Required]
        public string PhoneNumber { get; set; }

        [Column("FirstName")]
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Column("LastName")]
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Column("Address")]
        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [Column("Email")]
        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Column("IsDeleted")]
        public bool IsDeleted { get; set; }

        public Contact()
        {
        }

        public Contact(int ID)
        {
            GetContact(ID);
        }

        Contact currContact;

        private void GetContact(int ID)
        {
            using (PhonebookDbContext db = new PhonebookDbContext())
            {
                currContact = db.Contacts.FirstOrDefault(c => c.ID == ID);
            }

            this.ID = currContact.ID;
            this.PhoneNumber = currContact.PhoneNumber;
            this.FirstName = currContact.PhoneNumber;
            this.LastName = currContact.PhoneNumber;
            this.Address = currContact.PhoneNumber;
            this.Email = currContact.PhoneNumber;
            this.IsDeleted = currContact.IsDeleted;
            this.UserName = currContact.UserName;

        }
    }
}
