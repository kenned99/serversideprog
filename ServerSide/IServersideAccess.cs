using ServerSide.Model;
using ServerSide;
using System.Collections.Generic;
using ServerSideData;

namespace ServerSide
{
    public interface IServersideAccess
    {
        public Person GetPerson(int id);
        public IEnumerable<Person> GetPersonByName(string name = null);
        public Person AddPerson(Person NewPerson);
        public Person UpdatePerson(Person UpdatedPerson);
        public int DeletePerson(int id);
        public int Commit();
    }
}