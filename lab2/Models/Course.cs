using System;
using System.Collections.Generic;
using System.Text;

namespace lab2.Models
{
    public class Course
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public int Credits { get; set; }


        public Guid ProfessorId { get; set; }
        public Professor? Professor { get; set; }
    }
}
