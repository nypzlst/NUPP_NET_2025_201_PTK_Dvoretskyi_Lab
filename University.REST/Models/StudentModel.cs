namespace University.REST.Models
{
    public class StudentModel : PersonModel
    { 
        public DateTime StartOfTraining { get; set; }

        public DateTime EndOfTraining { get; set; }
    }
}
