using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Dto
{
    public class CakeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? ImageContent { get; set; }
        public List<string> Allergens { get; set; }
        public string Size { get; set; } = string.Empty;
        public string CustomMessage { get; set; } = string.Empty;
        public CategoryDto? Category { get; set; }
    }
}
