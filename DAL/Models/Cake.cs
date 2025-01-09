using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Cake : Product
    {
        public List<string> Allergens { get; set; }
    }

    public class CakeFilterParams
    {
        public int? CategoryId { get; set; }
        public string? Name { get; set; }
        public List<string>? Allergens { get; set; }
    }
}
