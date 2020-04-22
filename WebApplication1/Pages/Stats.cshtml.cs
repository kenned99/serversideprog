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
        public IEnumerable<Drink> Drinks { get; set; }

        [BindProperty]
        public Drink Drink { get; set; }

        [BindProperty]
        public Person Person { get; set; }
        
        [BindProperty]
        public int State { get; set; }

        public bool NewRecord = false;

        public void OnGet(int PersonId, int State)
        {
            Drinks = ServersideAccess.GetDrinkByName();

            if (State != 2)
            {
                Person = ServersideAccess.GetPerson(PersonId);
                ServersideAccess.CalculatePermille(Person);
            }
            else
            {
                Person = new Person();
            }

            if (Person.CurPermille > Person.TopPermille)
            {
                NewRecord = true;
                TempData["IsNewRecord"] = NewRecord;
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
        public IActionResult OnPostDiscard()
        {
            TempData.Clear();
            TempData.Add("lastAction", "Person not updated!");
            return RedirectToPage("./PersonList");
        }
        [BindProperty]
        public int DrinkId { get; set; }

        public IActionResult OnPostAddDrink(int DrinkId, int PersonId)
        {
            Drink = ServersideAccess.GetDrink(DrinkId);
            Person Person = ServersideAccess.GetPerson(PersonId);
            Drink.TimesDrank += 1;
            Person.Drinks += Drink.StandardDrinks;

            ServersideAccess.UpdatePerson(Person);
            ServersideAccess.UpdateDrink(Drink);
            ServersideAccess.Commit();
            //Drinks = ServersideAccess.GetDrinkByName();
            OnGet(PersonId, 0);
            return Page();
        }

        public IActionResult OnPostMinusDrink(int DrinkId, int PersonId)
        {
            Drink = ServersideAccess.GetDrink(DrinkId);
            Person = ServersideAccess.GetPerson(PersonId);
            if (Drink.TimesDrank > 0 && Person.Drinks > 0)
            {
                Drink.TimesDrank -= 1;
                Person.Drinks -= Drink.StandardDrinks;
                ServersideAccess.CalculatePermille(Person);
                if (TempData["IsNewRecord"] as bool? == true)
                {
                    Person.TopPermille = Person.CurPermille;
                }
                ServersideAccess.UpdateDrink(Drink);
                ServersideAccess.UpdatePerson(Person);
                ServersideAccess.Commit();
            }
            OnGet(PersonId, 0);
            return Page();


        }
    }
}