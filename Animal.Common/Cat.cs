using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animal.Common
{
    public class Cat : Animal
    {
        public string? TypeOfPurring {  get; set; }
        public string? CoatLength { get; set; }

        public bool IsIndoor { get; set; }
    }
}
