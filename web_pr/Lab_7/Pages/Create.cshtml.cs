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
                Ingredients = new List<Ingredient> { new Ingredient() } // ������������� � ����� ������������
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // ������ �������� �� ���������� ������

            // ��������� ����� ����� ������������� � ��������
            foreach (var ingredient in Recipe.Ingredients)
            {
                ingredient.Recipe = Recipe; // ������������� ����� � ��������
            }

            // ������ ��������� �� ������
            double totalCost = 0;
            foreach (var ingredient in Recipe.Ingredients)
            {
                totalCost += ingredient.Cost; // ������������ ��������� ������������
            }

            if (Recipe.NumberOfServings > 0)
            {
                Recipe.CostPerServing = totalCost / Recipe.NumberOfServings; // ������ ��������� �� ������
            }
            else
            {
                Recipe.CostPerServing = 0; // ���� ��� ������, ��������� 0
            }

            _context.Recipes.Add(Recipe); // ��������� ������

            // ���������� ������������ � �������� ���� ������
            foreach (var ingredient in Recipe.Ingredients)
            {
                _context.Ingredients.Add(ingredient); // ������ ����� ��� �����������
            }

            try
            {
                await _context.SaveChangesAsync(); // ��������� ��������� � ���� ������
                return RedirectToPage("./Index"); // �������� �� �������� �������
            }
            catch (Exception ex)
            {
                Console.WriteLine($"������ ��� ����������: {ex.Message}");
                return Page(); // ������� �������� � ����������� �� ������
            }
        }
    }
}