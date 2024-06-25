using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceModels
{
    public class LoginResponse : AuthResponse
    {
        public Tokens Tokens { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}
