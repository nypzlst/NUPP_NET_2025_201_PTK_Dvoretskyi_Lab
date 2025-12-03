using System;
using System.Collections.Generic;
using System.Text;

namespace lab2.Models
{
    public class Student : Person
    {
        public DateTime StartOfTraining { get; set; }

        public DateTime EndOfTraining { get; set; }

        public StudentCard? StudentCard { get; set; }
    }
}
