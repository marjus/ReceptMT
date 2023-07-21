using Microsoft.EntityFrameworkCore;
using ReceptMT.API.Models;

namespace ReceptMT.API.Data;

public class RecipeContext : DbContext
{
	public RecipeContext(DbContextOptions<RecipeContext> options) : base(options)
	{
	}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<ShoppingListItem>()
        //    .HasOne(p => p.Product)
        //    .HasForeignKey(p => p.Product_Id);
    }

    public DbSet<Recipe> Recipes { get; set; }

	public DbSet<Ingredient> Ingredients { get; set; }

    public DbSet<RecipeIngredient> RecipeIngredients { get; set; }

	public DbSet<Menu> Menus { get; set; }

	public DbSet<ShoppingList> ShoppingLists { get; set; }

    public DbSet<ShoppingListItem> ShoppingListItems { get; set; }

    public DbSet<Product> Products { get; set; }
}
