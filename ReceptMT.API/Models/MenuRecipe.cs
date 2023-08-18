
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ReceptMT.API.Models
{
    public class MenuRecipe
    {
        [Key]
        public int Id { get; set; }
        
        public int MenuId { get; set; }
        public int RecipeID { get; set; }
        public int Servings { get; set; }

        public float Multiplier { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }

        [JsonIgnore]
        public virtual Recipe? Recipe { get; set; }

    }
}