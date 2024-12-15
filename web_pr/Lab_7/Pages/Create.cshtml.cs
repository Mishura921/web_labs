using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using Lab_7.Pages.Models;
using System.Collections.Generic;

namespace Lab_7.Pages
{
    public class CreateRecipesModel : PageModel
    {
        private readonly CookbookContext _context;

        public CreateRecipesModel(CookbookContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Recipe Recipe { get; set; }

        public void OnGet()
        {
            Recipe = new Recipe
            {
                DatePrepared = DateTime.Today,
                Ingredients = new List<Ingredient> { new Ingredient() } // Инициализация с одним ингредиентом
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Убрали проверку на валидность модели

            // Установка связи между ингредиентами и рецептом
            foreach (var ingredient in Recipe.Ingredients)
            {
                ingredient.Recipe = Recipe; // Устанавливаем связь с рецептом
            }

            // Расчет стоимости за порцию
            double totalCost = 0;
            foreach (var ingredient in Recipe.Ingredients)
            {
                totalCost += ingredient.Cost; // Суммирование стоимости ингредиентов
            }

            if (Recipe.NumberOfServings > 0)
            {
                Recipe.CostPerServing = totalCost / Recipe.NumberOfServings; // Расчет стоимости за порцию
            }
            else
            {
                Recipe.CostPerServing = 0; // Если нет порций, назначаем 0
            }

            _context.Recipes.Add(Recipe); // Добавляем рецепт

            // Добавление ингредиентов в контекст базы данных
            foreach (var ingredient in Recipe.Ingredients)
            {
                _context.Ingredients.Add(ingredient); // Теперь связь уже установлена
            }

            try
            {
                await _context.SaveChangesAsync(); // Сохраняем изменения в базе данных
                return RedirectToPage("./Index"); // Редирект на страницу индекса
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении: {ex.Message}");
                return Page(); // Вернуть страницу с информацией об ошибке
            }
        }
    }
}