﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Dto
{
    public class RecipeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Servings { get; set; }
        public decimal CostPrice { get; set; }
        public string RecipeDirections { get; set; }
        public string StorageInformation { get; set; }
        public List<string> Allergens { get; set; }
        public CategoryDto? Category { get; set; }
        public List<RecipeIngredientDto> RecipeIngredients { get; set; }
    }

    public class RecipeIngredientDto
    {
        public int IngredientId { get; set; }
        public decimal Quantity { get; set; }
    }
}
