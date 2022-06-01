using PhoneBookEF.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookEF
{
    internal class PhoneBookDB : DbContext
    {
        public virtual DbSet<Contact> Contacts { get; set; }
        public PhoneBookDB() : base("phonebook")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PhoneBookDB, Configuration>());
        }

    }
}
