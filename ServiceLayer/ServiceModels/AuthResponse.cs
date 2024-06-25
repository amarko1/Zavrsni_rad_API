using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceModels
{
    public class AuthResponse
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
