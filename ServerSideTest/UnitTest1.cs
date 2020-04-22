using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerSide;
using ServerSide.Model;
using ServerSideData;
using System;
using System.Linq;


namespace ServerSideTest
{
    [TestClass]
    public class UnitTest1
    {
       //Skaber refferance
        ServersideAccess serversideAccess;

        //Tester forbindelsen til DB
        [TestInitialize]
        public void testInit()
        {
            var builder = new DbContextOptionsBuilder<PersonsDBContext>();
            builder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CanYouDriveDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            var db = new PersonsDBContext(builder.Options);
            serversideAccess = new ServersideAccess(db);
        }

        //Tester CreatePeople
        [TestMethod]
        public void CreatePeopleTest()
        {
         
            // Arrange
            var uniqueFirstName = Guid.NewGuid().ToString();

            Person newUser = new Person();
            newUser.FirstName = uniqueFirstName;
            newUser.LastName = "UnitTestEfternavn";
            newUser.Weight = 25;
            newUser.Gender = GenderEnum.Male;
            newUser.CurPermille = 1;
            newUser.TopPermille = 1;

            //Gem ny user i DB
            serversideAccess.AddPerson(newUser);
            
            var noOfRowsAffectedInDb = serversideAccess.Commit();
            //Hent yser fra DB med samme Id som lige er blevet gemt
            var retrivedPersons = serversideAccess.GetPerson(newUser.Id);

            // Assert
            Assert.AreEqual(1, noOfRowsAffectedInDb, "Det var forventet at der var indsat én række i databasen");
            Assert.AreEqual(uniqueFirstName, retrivedPersons.FirstName, "Det var forventet at hentetDeltagers fornavn var identiske med nyDeltagers");
        }

        //Test om man kan søge i DB efter navn
        [TestMethod]
        public void GetPersonByNameTest()
        {
            // Arrange
            var uniqueFirstName = Guid.NewGuid().ToString();

            Person newUser = new Person();
            newUser.FirstName = uniqueFirstName;
            newUser.LastName = "UnitTestEfternavn";
            newUser.Weight = 25;
            newUser.Gender = GenderEnum.Male;
            newUser.CurPermille = 1;
            newUser.TopPermille = 1;

            //Gem ny User i DB
            serversideAccess.AddPerson(newUser);
            var noOfRowsAffectedInDb = serversideAccess.Commit();

            //Hent user fra DB med samme Fornavn som lige er blevet gemt
            var retrivedPersons = serversideAccess.GetPersonByName(uniqueFirstName);

            // Assert
            Assert.AreEqual(1, retrivedPersons.Count(), "Det var forventet at der var fundet en deltager");
            Assert.AreEqual(uniqueFirstName, retrivedPersons.ToList<Person>()[0].FirstName, "Det var forventet at den hentede deltagers fornavn var identisk med det søgte fornavn");
        }




        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
