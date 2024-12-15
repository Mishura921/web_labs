using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Lab_7.Pages.Models;

namespace Lab_7.Pages
{
	public class DetailsModel : PageModel
	{
		private readonly CookbookContext _context;

		public DetailsModel(CookbookContext context)
		{
			_context = context;
		}

		public Recipe Recipe { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Recipe = await _context.Recipes
                .Include(r => r.Ingredients) // Загружаем ингредиенты
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Recipe == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}