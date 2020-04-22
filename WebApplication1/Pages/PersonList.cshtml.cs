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
        private readonly IServersideAccess ServersideAccess;

        [BindProperty(SupportsGet = true)]
        public string Filter { get; set; }

        public PersonListModel(IServersideAccess serverside)
        {
            this.ServersideAccess = serverside;
        }

        protected void Application_Start (object sender, EventArgs e)
        {
            
        }

        public IEnumerable<Person> People => ServersideAccess.GetPersonByName(Filter).OrderBy(x => x.Id);
        public IActionResult OnGetDelete(int PersonId)
        {
            ServersideAccess.DeletePerson(PersonId);
            ServersideAccess.Commit();

            TempData.Clear();
            TempData.Add("lastAction", "Person with ID: \"" + PersonId + "\" was removed!");
            return RedirectToPage("./PersonList");
        }
    }
}