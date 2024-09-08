using System.ComponentModel.DataAnnotations;
using ViewModelTableFormation.Models;

namespace ViewModelTableFormation.ViewModel
{
	public class CourseModel
	{
		public IEnumerable<StudentDetailsModel> Students { get; set; }
		public IEnumerable<Course> Courses { get; set; }

	}
}
