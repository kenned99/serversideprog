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
    public class StatsModel : PageModel
    {
        private readonly IServersideAccess ServersideAccess;

        public StatsModel(IServersideAccess SA)
        {
            this.ServersideAccess = SA;
        }

        public Person Person { get; set; }

        [BindProperty]
        public int State { get; set; }

        public bool NewRecord = false;

        public void OnGet(int PersonId, int State)
        {
            if (PersonId == 0)
            {
                PersonId = 1;
            }
            Person = ServersideAccess.GetPerson(PersonId);

            if (Person.CurPermille > Person.TopPermille)
            {
                NewRecord = true;
                Person.TopPermille = Person.CurPermille;
                ServersideAccess.UpdatePerson(Person);
            }

            this.State = State;
        }

        public IActionResult OnPostUpdate()
        {
            if (ModelState.IsValid)
            {
                this.Person = ServersideAccess.UpdatePerson(Person);
                ServersideAccess.Commit();

                TempData.Add("lastAction", "Person with ID: \"" + Person.Id + "\" was updated!");
                return RedirectToPage("./PersonList");
            }
            else
            {
                return Page();
            }
        }

        public IActionResult OnPostUndo()
        {
            TempData.Add("lastAction", "Person not updated!");
            return RedirectToPage("./PersonList");
        }

        public IActionResult OnPostAdd(Person Person)
        {
            if (ModelState.IsValid)
            {
                ServersideAccess.AddPerson(Person);
                ServersideAccess.Commit();

                TempData.Add("lastAction", Person.FirstName + " was added successfully!");
                return RedirectToPage("./PersonList");
            }
            else
            {
                return Page();
            }
        }
    }
}