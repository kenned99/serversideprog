using ServerSide.Model;
using System.Collections.Generic;

namespace ServerSide
{
    public interface IServersideAccess
    {
        public Person GetPerson(int id);
        public IEnumerable<Person> GetPersonByName(string navn = null);
        public Person AddPerson(Person NewPerson);
        public Person UpdatePerson(Person UpdatedPerson);
        public int DeletePerson(int id);
        public int Commit();
    }
}