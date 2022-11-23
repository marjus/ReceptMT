using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;

namespace ReceptMT.API.Models
{
    public class ShoppingList
    {
        [Key]
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:yy-MM-dd}")]
        public DateTime CreatedDate { get; set; }
        public virtual List<ShoppingListItem> ShoppingListItems { get; set; }
        public bool IsOpen { get; set; }
        public int? MenuId { get; set; }
        public virtual Menu? Menu { get; set; }

        public string? UserName { get; set; }

        [NotMapped]
        public string? Title { get; set; }

        public ShoppingList()
        {
            this.ShoppingListItems = new List<ShoppingListItem>();
        }
    }
}