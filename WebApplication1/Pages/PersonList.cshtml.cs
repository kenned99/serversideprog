using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServerSide;
using ServerSide.Model;

namespace WebApplication1.Pages
{
    public class PersonListModel : PageModel
    {
        private readonly IServersideAccess serverside;

        public PersonListModel(IServersideAccess serverside)
        {
            this.serverside = serverside;
        }
        public IEnumerable<Person> People => serverside.GetPersonByName("").OrderBy(x => x.Id);
        public void OnGet()
        {

        }

    }
}