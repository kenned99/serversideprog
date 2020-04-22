using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServerSide;
using ServerSide.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : Controller
    {
        private readonly IServersideAccess serversideAccess;

        public PeopleController(IServersideAccess serversideAccess)
        {
            this.serversideAccess = serversideAccess;
        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Person> Get([FromQuery]string Filter = null)
        {
            return serversideAccess.GetPersonByName(Filter);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public Person Get(int id)
        {
            return serversideAccess.GetPerson(id);
        }

        // POST api/<controller>
        [HttpPost]
        public Person Post ([FromBody]Person Person)
        {
            var result = serversideAccess.UpdatePerson(Person);
            serversideAccess.Commit();
            return result;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public Person Put([FromBody]Person Person)
        {
            var result = serversideAccess.UpdatePerson(Person);
            serversideAccess.Commit();
            return result;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            serversideAccess.DeletePerson(id);
            serversideAccess.Commit();
        }
    }
}
