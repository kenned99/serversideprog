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

        [BindProperty(SupportsGet = true)]
        public string filter { get; set; }

        public PersonListModel(IServersideAccess serverside)
        {
            this.serverside = serverside;
        }
        public IEnumerable<Person> People => serverside.GetPersonByName(filter).OrderBy(x => x.Id);
        public IActionResult OnGetDelete(int PersonId)
        {
            serverside.DeletePerson(PersonId);
            serverside.Commit();

            TempData.Clear();
            TempData.Add("lastAction", "Member with ID: \"" + PersonId + "\" was removed!");
            return RedirectToPage("./PersonList");
        }
    }
}