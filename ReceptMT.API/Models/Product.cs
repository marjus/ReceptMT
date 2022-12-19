using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReceptMT.API.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }
        public string?  Name { get; set; }
        
        public string? PrimaryShoppingUnit { get; set; }

        public bool HaveInStock { get; set; }
        public bool Stockable { get; set; }

        [NotMapped]
        public bool IsIngredientGroup { 
            get { return !Name.Any(char.IsLower); } 
        }
    }
}