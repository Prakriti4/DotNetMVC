using System.ComponentModel.DataAnnotations;
using ViewModelTableFormation.Models;

namespace ViewModelTableFormation.ViewModel
{
	public class CourseModel
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string CourseName { get; set; }     // Name of the course
		public string CourseCode { get; set; }     // Unique code for the course
		public int Credits { get; set; }           // Number of credits the course is worth
		public string Description { get; set; }    // Brief description of the course content
		public double TuitionFee { get; set; }    // Cost or fee required to take the course
		public DateTime StartDate { get; set; }    // Date when the course starts
		public DateTime EndDate { get; set; }      // Date when the course ends



		public int FacultyId { get; set; }
		public Faculty Faculty { get; set; }

		public ICollection<Teacher> Teachers { get; set; }
		public ICollection<Student> Students { get; set; }


	}
}
