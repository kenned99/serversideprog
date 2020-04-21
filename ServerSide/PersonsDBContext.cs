using Microsoft.EntityFrameworkCore;
using ServerSide.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerSideData
{
    public class PersonsDBContext : DbContext
    {
        public PersonsDBContext(DbContextOptions<PersonsDBContext> options)
            : base(options)
        {

        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Drink> Drinks { get; set; }
    }
}
