using System;
using System.Collections.Generic;
using System.Text;

namespace lab2.Models
{
    public class StudentCard
    {
        public Guid Id { get; set; }
        public string CardNumber { get; set; } = string.Empty;

        public Guid StudentId { get; set; }
        public Student? Student { get; set; }
    }
}
