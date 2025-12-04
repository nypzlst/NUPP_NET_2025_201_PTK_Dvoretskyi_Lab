namespace University.REST.Models
{
    public class ProfessorModel : PersonModel
    {
        public string? Department { get; set; }
        public DateTime HireDate { get; set; }
    }
}
