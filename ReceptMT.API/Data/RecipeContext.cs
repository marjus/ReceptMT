using Microsoft.EntityFrameworkCore;
using ReceptMT.API.Models;

namespace ReceptMT.API.Data;

public class RecipeContext : DbContext
{
	public RecipeContext(DbContextOptions<RecipeContext> options) : base(options)
	{
	}

	public DbSet<Recipe> Recipes { get; set; }

	public DbSet<Ingredient> Ingredients { get; set; }

	public DbSet<ReceptMT.API.Models.Menu> Menus { get; set; }

	public DbSet<ReceptMT.API.Models.ShoppingList> ShoppingLists { get; set; }

    public DbSet<ReceptMT.API.Models.ShoppingListItem> ShoppingListItems { get; set; }

    public DbSet<ReceptMT.API.Models.Product> Products { get; set; }
}
