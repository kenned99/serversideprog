using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ServerSide.Model
{
    class Drink
    {
        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 2)]
        [RegularExpression(@"^[a-zA-ZæøåÆØÅ]+$0-9\-")]
        public string Name { get; set; }

        public int StandardDrinks { get; set; }

        public int TimesDrank { get; set; }
    }
}
