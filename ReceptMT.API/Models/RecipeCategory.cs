using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReceptMT.API.Models
{
    public class RecipeCategory
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="Typ")]
        public string Category { get; set; }

        public RecipeCategory(string category)
        {
            Category = category;
        }
    }
}