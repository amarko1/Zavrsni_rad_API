using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public enum Priority
    {
        Low = 0,
        Medium = 1,
        High = 2
    }

    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public Priority Priority { get; set; }
        public int? AssignedUserId { get; set; }
        public User? AssignedUser { get; set; }
    }
}
