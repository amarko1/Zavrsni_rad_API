using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Dto
{
    public class SupplyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Supplier { get; set; }
        public int CategoryId { get; set; }
        public decimal CostPrice { get; set; }
    }

}
