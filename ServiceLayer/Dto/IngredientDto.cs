using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Dto
{
    public class IngredientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Supplier { get; set; }
        public string Measurement { get; set; }
        public decimal PurchaseSize { get; set; }
        public decimal CostPrice { get; set; }
    }
}
