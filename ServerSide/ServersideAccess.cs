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

        public void CalculatePermille(Person Person)
        {
            if (Person.Drinks > 0)
            {
                double ratio = 0.7; //Male ratio
                if (Person.Gender == GenderEnum.Female)
                {
                    ratio = 0.6;
                }

                var time = (DateTime.Now - Person.DrinkingStart).TotalHours;
                if (time >= 1)
                {
                    time = time - 1;
                }

                double Permille = (Person.Drinks * 12) / (Person.Weight * ratio) - (0.15 * time);
                Permille = Math.Round(Permille, 2);

                if (Permille < 0)
                {
                    Person.CurPermille = 0f;
                    Person.Drinks = 0;
                }
                Person.CurPermille = (float)Permille;
                UpdatePerson(Person);
                Commit();
            }
            else
            {
                Person.CurPermille = 0f;
                UpdatePerson(Person);
                Commit();
            }
        }
    }
}