using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServerSide;
using ServerSide.Model;

namespace WebApplication1.Pages
{
    public class DrinksListModel : PageModel
    {
        private readonly IServersideAccess ServersideAccess;

        public DrinksListModel(IServersideAccess SA)
        {
            this.ServersideAccess = SA;
        }
        public IEnumerable<Drink> Drinks { get; set; }
        
        public Drink Drink { get; set; }

        public void OnGet()
        {
            Drinks = ServersideAccess.GetDrinkByName();
        }

        [BindProperty]
        public int DrinkId { get; set; }

        public IActionResult OnPostAdd(int DrinkId)
        {
            Drink = ServersideAccess.GetDrink(DrinkId);
            Drink.TimesDrank = Drink.TimesDrank + 1;
            ServersideAccess.UpdateDrink(Drink);
            ServersideAccess.Commit();
            //Drinks = ServersideAccess.GetDrinkByName();
            return Redirect("./DrinksList");
        }

        public IActionResult OnPostMinus(int DrinkId)
        {
            Drink = ServersideAccess.GetDrink(DrinkId);
            if (Drink.TimesDrank <= 0)
            {
                //Drinks = ServersideAccess.GetDrinkByName();
                return Redirect("./DrinksList");

            } else {
                Drink.TimesDrank = Drink.TimesDrank - 1;
                ServersideAccess.UpdateDrink(Drink);
                ServersideAccess.Commit();
                //Drinks = ServersideAccess.GetDrinkByName();
                return Redirect("./DrinksList");
            }
            

        }
    }
}