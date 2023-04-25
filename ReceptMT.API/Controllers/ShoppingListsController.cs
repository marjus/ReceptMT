using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReceptMT.API.Data;
using ReceptMT.API.DTO;
using ReceptMT.API.Models;

namespace ReceptMT.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShoppingListsController : ControllerBase
    {
        private readonly RecipeContext _context;

        public ShoppingListsController(RecipeContext context)
        {
            _context = context;
        }

        // GET: api/ShoppingLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingListDTO>>> GetShoppingList()
        {
         //   var item = _context.ShoppingListItems.FirstOrDefault();
            var lists = _context.ShoppingLists.Include(l=> l.ShoppingListItems)
                .Where(l => l.IsOpen && l.CreatedDate > DateTime.Now.AddDays(-180));

            if (lists.Any())
            {
                return await lists.Select(s => new ShoppingListDTO
                {
                    Id = s.Id,
                    Title = s.Title,
                    IsClosed = !s.IsOpen,
                    CreatedTime = s.CreatedDate,
                    Items = s.ShoppingListItems.Select(i => ShoppingListItemToDTO(i, s))
                }).ToListAsync();
            }
            else
                return new List<ShoppingListDTO>();
        }

        private static ShoppingListItemDTO ShoppingListItemToDTO(ShoppingListItem item, ShoppingList list) =>
            new ShoppingListItemDTO
            {
                Amount = item.Amount.ToString(),
                Unit = item.Unit,
                Product = item.Product == null ? "" : item.Product.Name,
                FromMenuId = list.MenuId,
                FromMenuName = list.Menu == null ? "" : list.Menu.Name,
                FromRecipeId = item.FromRecipeId,
                FromRecipeName = item.FromRecipe == null ? "" : item.FromRecipe.Title,
                Id = item.Id,
                IsDone = item.Done
            };

        // GET: api/ShoppingLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingListDTO>> GetShoppingList(int id)
        {
            var shoppingList = await _context.ShoppingLists
                .Include(l => l.ShoppingListItems).ThenInclude(i=> i.Product)
                .SingleOrDefaultAsync(l => l.Id==id);

            if (shoppingList == null)
            {
                return NotFound();
            }

            return new ShoppingListDTO
            {
                Id = shoppingList.Id,
                Title = shoppingList.Title,
                IsClosed = !shoppingList.IsOpen,
                CreatedTime = shoppingList.CreatedDate,
                Items = shoppingList.ShoppingListItems.Select(i => new ShoppingListItemDTO
                {
                    Amount = i.Amount.ToString(),
                    Unit = i.Unit,
                    Product = i.Product?.Name,
                    FromMenuId = shoppingList.MenuId,
                    FromMenuName = shoppingList.Menu == null ? "" : shoppingList.Menu.Name,
                    FromRecipeId = i.FromRecipeId,
                    FromRecipeName = i.FromRecipe == null ? "" : i.FromRecipe.Title,
                    Id = i.Id,
                    IsDone = i.Done
                }),
            };
        }

        // PUT: api/ShoppingLists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoppingList(int id, ShoppingList shoppingList)
        {
            if (id != shoppingList.Id)
            {
                return BadRequest();
            }

            _context.Entry(shoppingList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingListExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ShoppingLists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShoppingList>> PostShoppingList(ShoppingList shoppingList)
        {
            _context.ShoppingLists.Add(shoppingList);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShoppingList", new { id = shoppingList.Id }, shoppingList);
        }

        // DELETE: api/ShoppingLists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoppingList(int id)
        {
            var shoppingList = await _context.ShoppingLists.FindAsync(id);
            if (shoppingList == null)
            {
                return NotFound();
            }

            _context.ShoppingLists.Remove(shoppingList);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShoppingListExists(int id)
        {
            return _context.ShoppingLists.Any(e => e.Id == id);
        }
    }
}
