using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReceptMT.API.Models
{
    public class Recipe
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Titel")]
        public string Title { get; set; }
        [Display(Name ="Beskrivning")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [NotMapped]
        public int ContentSize { get; set; }

        [Display(Name = "Så här gör du")]
        [DataType(DataType.MultilineText)]
        public string Process { get; set; }

        public int CategoryId { get; set; }

        public virtual RecipeCategory Category { get; set; }

        public string Image { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public virtual ICollection<MenuRecipe> MenuRecipes { get; set; }

        public string CreatedByUserName { get; set; }

        public DateTime CreatedDate { get; set; }
        public bool Public { get; set; }

        public virtual List<RecipeIngredient> Ingredients { get; set; }

        [Display(Name = "Ingredienser")]
        [DataType(DataType.MultilineText)]

        public string IngredientsText { get; set; }

    }
}