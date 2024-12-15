using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab_7.Pages.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите название блюда.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите описание блюда.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите рецепт.")]
        public string RecipeText { get; set; }

        public string ImageUrl { get; set; }

        [DataType(DataType.Date)]
        public DateTime DatePrepared { get; set; }

        [Range(1, 10, ErrorMessage = "Оценка должна быть от 1 до 10.")]
        public int? Rating { get; set; }

        public string Comments { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите количество порций.")]
        [Range(1, int.MaxValue, ErrorMessage = "Количество порций должно быть больше 0.")]
        public int NumberOfServings { get; set; }

        public double CostPerServing { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    }

    public class Ingredient
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите название ингредиента.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите количество ингредиента.")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Количество должно быть больше 0.")]
        public double Quantity { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите стоимость ингредиента.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Стоимость должна быть больше 0.")]
        public double Cost { get; set; }

        public int RecipeId { get; set; } // Этот идентификатор должен быть установлен
        public Recipe Recipe { get; set; } // Это поле обязательно!
    }
}