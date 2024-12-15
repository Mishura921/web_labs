using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lab_7.Pages.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab_7.Pages
{
    public class IndexModel : PageModel
    {
        private readonly CookbookContext _context;

        public IndexModel(CookbookContext context)
        {
            _context = context;
        }

        public List<Recipe> Recipes { get; set; }

        public async Task OnGetAsync()
        {
            Recipes = await _context.Recipes
                .Include(r => r.Ingredients) // Загрузите ингредиенты, если вы хотите их также отображать на странице
                .ToListAsync();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var recipe = await _context.Recipes.Include(r => r.Ingredients).FirstOrDefaultAsync(m => m.Id == id);

            if (recipe == null)
            {
                return NotFound();
            }

            // Удаление всех ингредиентов, относящихся к рецепту
            _context.Ingredients.RemoveRange(recipe.Ingredients);
            // Удаление рецепта
            _context.Recipes.Remove(recipe);

            await _context.SaveChangesAsync();
            return RedirectToPage("./Index"); // Редирект на главную страницу
        }
    }
}