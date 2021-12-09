using System;
using Microsoft.EntityFrameworkCore;

namespace PhonebookApp.Data
{
    public class PhonebookDbContext : DbContext
    {
        public PhonebookDbContext(DbContextOptions<PhonebookDbContext> options)
            :base(options)
        {
        }

        public PhonebookDbContext()
        {

        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<DeletedContact> DeletedContacts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=app.db");
        }
    }
}
