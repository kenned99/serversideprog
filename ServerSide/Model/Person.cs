using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
          
namespace ServerSide.Model
{

    public enum GenderEnum
    {
        Female = 0,
        Male = 1
    }

    public class Person
    {
        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 2)]
        [RegularExpression(@"^[a-zA-ZæøåÆØÅ]+$", ErrorMessage = "Only letters are allowed in first name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 2)]
        [RegularExpression(@"^[a-zA-ZæøåÆØÅ]+$", ErrorMessage = "Only letters are allowed in last name")]
        public string LastName { get; set; }

        [Required]
        public GenderEnum Gender { get; set; }

        [Required]
        [Range(40, 200, ErrorMessage = "You weight somewhere between 40 and 200, type in your real weight.")]
        public int Weight { get; set; }

        public float CurPermille { get; set; }

        public float TopPermille { get; set; }

        public int Drinks { get; set; }

        [Required]
        public DateTime DrinkingStart { get; set; }
    }
}
