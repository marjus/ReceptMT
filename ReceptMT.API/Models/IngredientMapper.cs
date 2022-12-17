using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReceptMT.API.Models
{
    public class IngredientMapper
    {
        public int Id { get; set; }
        public string? FromValue { get; set; }
        public string? ToValue { get; set; }
    }
}