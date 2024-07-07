using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Cake : Product
    {
        public string Allergens { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public string CustomMessage { get; set; } = string.Empty;
    }
}
