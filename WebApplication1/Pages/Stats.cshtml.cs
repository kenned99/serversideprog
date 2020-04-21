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

        public void OnGet(int PersonId)
        {
            Person = ServersideAccess.GetPerson(PersonId);
        }
    }
}