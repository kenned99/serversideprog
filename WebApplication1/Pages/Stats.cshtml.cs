using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServerSide;
using ServerSide.Model;

namespace WebApplication1.Pages
{
    public class StatsModel : PageModel
    {
        private readonly IServersideAccess ServersideAccess;
        private readonly IHtmlHelper htmlHelper;

        public IEnumerable<SelectListItem> GenderListItems => htmlHelper.GetEnumSelectList<GenderEnum>();

        public StatsModel(IServersideAccess SA, IHtmlHelper htmlHelper)
        {
            this.ServersideAccess = SA;
            this.htmlHelper = htmlHelper;
        }

        public Person Person { get; set; }

        [BindProperty]
        public int State { get; set; }

        public bool NewRecord = false;

        public void OnGet(int PersonId, int State)
        {
            if (PersonId == 0)
            {
                PersonId = 2;
            }

            if (State != 2)
            {
                Person = ServersideAccess.GetPerson(PersonId);
            }
            else
            {
                Person = new Person();
            }

            if (Person.CurPermille > Person.TopPermille)
            {
                NewRecord = true;
                Person.TopPermille = Person.CurPermille;
                ServersideAccess.UpdatePerson(Person);
                ServersideAccess.Commit();
            }

            this.State = State;
        }

        public IActionResult OnPostUpdate(Person Person)
        {
            if (ModelState.IsValid)
            {
                this.Person = ServersideAccess.UpdatePerson(Person);
                ServersideAccess.Commit();
                TempData.Clear();
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

                TempData.Clear();
                TempData.Add("lastAction", Person.FirstName + " was added successfully!");
                return RedirectToPage("/PersonList");
            }
            else
            {
                return Page();
            }
        }
    }
}