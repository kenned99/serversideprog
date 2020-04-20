using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ServerSide.Model
{
    class Person
    {
        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 2)]
        [RegularExpression(@"^[a-zA-ZæøåÆØÅ]+$\-", ErrorMessage = "Only letters are allowed in first name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 2)]
        [RegularExpression(@"^[a-zA-ZæøåÆØÅ]+$\-", ErrorMessage = "Only letters are allowed in last name")]
        public string LastName { get; set; }

        [Required]
        public bool Gender { get; set; }

        [Required]
        [Range(40, 200, ErrorMessage = "You weight somewhere between 40 and 200, type in your real weight.")]
        public int Weight { get; set; }

        public float CurPromille { get; set; }

        public float TopPromille { get; set; }

        public int RecommendedWater { get; set; }

        [Required]
        public DateTime DrinkingStart { get; set; }
    }
}
