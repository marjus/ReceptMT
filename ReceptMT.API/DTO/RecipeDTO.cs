using ReceptMT.API.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ReceptMT.API.DTO
{
    public class RecipeDTO
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }


        public string? Process { get; set; }

        public virtual IEnumerable<IngredientDTO>? Ingredients { get; set; }
    }

    public class IngredientDTO
    {
        public string Name { get; set; }

        public float? Amount { get; set; }
        public string Unit { get; set; }
    }
}
