using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReceptMT.API.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        public string? Tagname { get; set; }

        public ICollection<Recipe> Recipes { get; set; }
    }
}