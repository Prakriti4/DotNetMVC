using System.ComponentModel.DataAnnotations;

namespace ViewModelTableFormation.Models
{
    public class Faculty
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }     // Name of the faculty, e.g., "Science and Technology"
        public string Dean { get; set; }     // Name of the Dean or Head of Faculty
        public string Description { get; set; } // Optional: Description of the faculty


        public ICollection<Course> Courses { get; set; }    
     
    }

       
}

