using ServerSide.Model;
using System.Collections.Generic;
using System.Linq;
using System;
using ServerSideData;
using Microsoft.EntityFrameworkCore;

namespace ServerSide
{
    public class ServersideAccess : IServersideAccess
    {
        private readonly PersonsDBContext db;

        public ServersideAccess(PersonsDBContext db)
        {
            this.db = db;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public Person GetPerson(int id)
        {
            return db.Persons.Find(id);
        }

        public Drink GetDrink(int id)
        {
            return db.Drinks.Find(id);
        }

        public IEnumerable<Person> GetPersonByName(string name = null)
        {
            var query = from d in db.Persons
                        where d.FirstName.StartsWith(name) || string.IsNullOrEmpty(name)
                        orderby d.FirstName
                        select d;
            return query;
        }

        public IEnumerable<Drink> GetDrinkByName(string name = null)
        {
            var query = from d in db.Drinks
                        where d.Name.StartsWith(name) || string.IsNullOrEmpty(name)
                        orderby d.Name
                        select d;
            return query;
        }
        
        

        public Person AddPerson(Person NewPerson)
        {
            db.Add(NewPerson);
            return NewPerson;
        }

        public Person UpdatePerson(Person UpdatedPerson)
        {
            db.Update(UpdatedPerson);
            return UpdatedPerson;
        }
        public Drink UpdateDrink(Drink UpdatedDrink)
        {
            db.Update(UpdatedDrink);
            return UpdatedDrink;
        }

        public int DeletePerson(int id)
        {
            var person = GetPerson(id);
            if (person != null)
            {
                db.Persons.Remove(person);
                return 1;
            }
            return 0;
        }
    }
}