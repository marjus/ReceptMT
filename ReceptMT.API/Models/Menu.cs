
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ReceptMT.API.Models
{
    public class Menu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? MenuUserKey { get; set; }

        public string? Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:yy-MM-dd}")]
        [Display(Name = "Skapad")]
        public DateTime CreatedDate { get; set; }

        public bool IsOpen { get; set; }

        public string? UserName { get; set; }

        public virtual ICollection<MenuRecipe> MenuRecipes { get; set; }

        public Menu()
        {
            this.MenuRecipes = new List<MenuRecipe>();
        }
    }
}