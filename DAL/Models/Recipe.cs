using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int Servings { get; set; }
        public decimal CostPrice { get; set; }
        public string RecipeDirections { get; set; }
        public string Allergens { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<RecipeIngredient> RecipeIngredients { get; set; }
        public Category Category { get; set; }
    }
}
