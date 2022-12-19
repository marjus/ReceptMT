using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReceptMT.API.Models
{
    public class RecipeIngredient
    {
        [Key]
        public int Id { get; set; }
        public float? Amount { get; set; }
        public string? Unit { get; set; }

        public string? IngredientGroup { get; set; }

        [ForeignKey("Recipe")]
        public int RecipeId { get; set; }

        [ForeignKey("Ingredient")]
        public int IngredientId { get; set; }

        public virtual Ingredient Ingredient { get; set; }
       
    }
}