﻿using System;
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
    public class RecipesController : ControllerBase
    {
        private readonly RecipeContext _context;

        public RecipesController(RecipeContext context)
        {
            _context = context;
        }

        // GET: api/Recipes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeListDTO>>> GetRecipes()
        {
            // todo get from cache

            return await _context.Recipes
                .Include(r=> r.Category)
                .Select(r => ToDTO(r))
                .ToListAsync();
        }

        //         [HttpGet]
        // public async Task<ActionResult<IEnumerable<RecipeListDTO>>> GetRecipes(string filter)
        // {
        //     // todo get from cache

        //     return await _context.Recipes
        //         .Include(r=> r.Category)
        //         .Where(r=> r.Title.ToLower().StartsWith(filter))
        //         .Select(r => ToDTO(r))
        //         .ToListAsync();
        // }

        private static RecipeListDTO ToDTO(Recipe recipe) =>
            new RecipeListDTO
            {
                ID = recipe.Id,
                Title = recipe.Title,
                Description = recipe.Description,
                Category = recipe.Category?.Category
            };

        private static RecipeDTO ToDetailsDTO(Recipe recipe) =>
           new RecipeDTO
           {
               ID = recipe.Id,
               Title = recipe.Title,
               Description = recipe.Description,
               Ingredients = recipe.Ingredients?.Select(i => new IngredientDTO { Amount = i.Amount, Name = i.Ingredient?.Name, Unit = i.Unit })
           };


        // GET: api/Recipes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeDTO>> GetRecipe(int id)
        {
            var recipe = await _context.Recipes
                .Include(r=>r.Ingredients)
                .ThenInclude(i=> i.Ingredient).SingleOrDefaultAsync(r => r.Id==id);

            if (recipe == null)
            {
                return NotFound();
            }

            var recipeDTO = ToDetailsDTO(recipe);
            recipeDTO.Process = recipe.Process;

            return recipeDTO;
        }

        // PUT: api/Recipes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipe(int id, Recipe recipe)
        {
            // todo: add to cache

            if (id != recipe.Id)
            {
                return BadRequest();
            }

            _context.Entry(recipe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(id))
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

        private Models.Ingredient GetIngredient(string ingredientText)
        {
            var ingredient =  _context.Ingredients.FirstOrDefault(i => i.Name.ToLower() == ingredientText.ToLower());

            if(ingredient == null)
            {
                ingredient = _context.Ingredients.Add(new Ingredient { Name = ingredientText }).Entity;
            }

            return ingredient;
        }

        // POST: api/Recipes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Recipe>> PostRecipe(Recipe recipe)
        {
            // todo add to cache

            var ingredients = Util.InputParser.ParseIngredientList(recipe.IngredientsText);
            recipe.Ingredients = new List<RecipeIngredient>();
            recipe.Ingredients.AddRange(ingredients.Select(i => new RecipeIngredient { Amount = i.amount, Unit = i.unit, Ingredient = GetIngredient(i.ingredient) }));
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRecipe), new { id = recipe.Id }, recipe);
        }

        // DELETE: api/Recipes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            // todo remove from cache 
            
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.Id == id);
        }
    }
}
