using Microsoft.EntityFrameworkCore;
using ServerSideData.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerSideData
{
    public class PersonsDBContext : DbContext
    {
        public PersonsDBContext(DbContextOptions<DeltagereDbContext> options)
            : base(options)
        {

        }
        public DbSet<Person> Persons { get; set; }
    }
}
