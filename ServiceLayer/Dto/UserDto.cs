using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public bool IsDisabled { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderDTO> Orders { get; set; }
    }
}
