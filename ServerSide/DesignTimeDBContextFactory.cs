using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerSideData
{
    class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PersonsDBContext>
    {
        public PersonsDBContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<PersonsDBContext>();
            builder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CanYouDriveDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            return new PersonsDBContext(builder.Options);
        }
    }
}