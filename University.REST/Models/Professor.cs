using System;
using System.Collections.Generic;
using System.Text;

namespace lab2.Models
{
    public class Professor : Person
    {
        public string? Department { get; set; }
        public DateTime HireDate { get; set; }

        public ICollection<Course>? Courses { get; set; }
    }
}
