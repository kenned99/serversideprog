using ServerSide.Model;
using System.Collections.Generic;
using System.Linq;
using System;
using ServerSideData;

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

        public IEnumerable<Person> GetPersonByName(string name = null)
        {
            var query = from d in db.Persons
                        where d.FirstName.StartsWith(name) || string.IsNullOrEmpty(name)
                        orderby d.FirstName
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

        public void CalculatePermille(Person Person, int Drinks)
        {
            double ratio = 0.7; //male ratio
            if (Person.Gender == GenderEnum.Female)
            {
                ratio = 0.6;
            }

            var time = (DateTime.Now - Person.DrinkingStart).TotalHours - 1;
            double Permille = (Drinks * 12) / (Person.Weight * ratio) - (0.15 * time);

            if (Permille < 0)
            {
                Permille = 0f;
                Person.DrinkingStart = DateTime.Now;
            }
            Person.CurPermille = (float)Permille;
            UpdatePerson(Person);
            Commit();
        }
    }
}