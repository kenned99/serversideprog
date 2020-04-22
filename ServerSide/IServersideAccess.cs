using ServerSide.Model;
using ServerSide;
using System.Collections.Generic;
using ServerSideData;

namespace ServerSide
{
    public interface IServersideAccess
    {
        public Person GetPerson(int id);
        public Drink GetDrink(int id);
        public IEnumerable<Person> GetPersonByName(string name = null);
        public IEnumerable<Drink> GetDrinkByName(string name = null);
        public Person AddPerson(Person NewPerson);
        public Person UpdatePerson(Person UpdatedPerson);
        public Drink UpdateDrink(Drink UpdatedDrink);
        public int DeletePerson(int id);
        public int Commit();
    }
}