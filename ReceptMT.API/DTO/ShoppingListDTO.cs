namespace ReceptMT.API.DTO
{
    public class ShoppingListDTO
    {
        public int Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public string? Title { get; set; }

        public bool IsClosed { get; set; }

        public IEnumerable<ShoppingListItemDTO> Items { get; set; }
    }

    public class ShoppingListItemDTO
    {
        public int Id { get; set; }
        public bool IsDone { get; set; }
        public string? Amount { get; set; }
        public string? Unit { get; set; }
        public string? Product { get; set; }

        public int? FromRecipeId { get; set; }
        public string? FromRecipeName { get; set; }

        public int? FromMenuId { get; set; }
        public string? FromMenuName { get; set; }

    }
}
