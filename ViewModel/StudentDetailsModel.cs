using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ViewModelTableFormation.Models;

namespace ViewModelTableFormation.ViewModel
{
	public class StudentDetailsModel
	{
		[Key]
		public int Id { get; set; }


		public string Name { get; set; }

		public string Address { get; set; }

		public string Gmail { get; set; }

		public DateTime DateofBirth { get; set; }

		[NotMapped]
		public IFormFile Image { get; set; }

		public string ImageUrl { get; set; }

		public int CourseId { get; set; }

		public string CourseName { get; set; }

		public Course Course { get; set; }

	}
}
