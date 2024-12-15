using Lab_7.Pages.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab_7.Pages.Models
{
    public class CookbookContext : DbContext
    {
        public CookbookContext(DbContextOptions<CookbookContext> options) : base(options) { }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
    }
}