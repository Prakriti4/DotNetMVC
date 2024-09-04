using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ViewModelTableFormation.Data;
using ViewModelTableFormation.Models;
using ViewModelTableFormation.ViewModel;

namespace ViewModelTableFormation.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StudentController : Controller
    {
        private readonly SchoolIdentityDbContext _context;

    

        public StudentController(SchoolIdentityDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IActionResult> Index()
        {

            ViewBag.Coursess = _context.Courses.ToList();
            var models = await _context.Students.ToListAsync();
            if (models == null)
            {
                return View("Error");
            }
            return View(models);
        }

        public IActionResult Create()
        {
     
            ViewBag.Course = new SelectList(_context.Courses, "Id", "CourseName");
            return View(new CreateStudentModel());
        }

        [HttpPost]
        public IActionResult Create(CreateStudentModel model)
        {
            ViewBag.Course = new SelectList(_context.Courses, "Id", "CourseName");
            if (ModelState.IsValid)
            {
                var student = new Student
                {
                    Name = model.Name,
                    Address = model.Address,
                    Gmail = model.Gmail,
                    DateofBirth = model.DateofBirth,
                    Image = model.Image,
                    ImageUrl = model.ImageUrl,
                    CourseId = model.CourseId,
                };
                string uniqueFile = UploadedFile(student);
                student.ImageUrl=uniqueFile;
                _context.Add(student);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }


		[HttpGet]
		public IActionResult Details(int id)
		{
			var student = _context.Students
				.Include(s => s.Course)
				.FirstOrDefault(s => s.Id == id);

			if (student == null)
			{
				return NotFound();
			}

			var model = new StudentDetailsModel
			{
				Id = student.Id,
				Name = student.Name,
				Address = student.Address,
				Gmail = student.Gmail,
				DateofBirth = student.DateofBirth,
				ImageUrl = student.ImageUrl,
                CourseName= student.Course.CourseName,
		
			};

			return View(model);
		}

		public string UploadedFile(Student model)
        {
            string uniqueFileName = null;
            if(model.Image!=null)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                uniqueFileName=Guid.NewGuid().ToString()+"_"+model.Image.FileName;
                string filePath=Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream=new FileStream(filePath,FileMode.Create))
                {
                    model.Image.CopyTo(fileStream);
                }
                
            }
            return uniqueFileName;
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Courses = new SelectList(_context.Courses.ToList(), "Id", "CourseName");
            var model = _context.Students.Find(id);
            if (model == null)
            {
                return NotFound();
            }
            var model_1 = new EditStudentModel
            {
                Id = model.Id,
                Name = model.Name,
                Address = model.Address,
                Gmail = model.Gmail,
                DateofBirth = model.DateofBirth,
                ImageUrl = model.ImageUrl,
                CourseId = model.CourseId,
            };
            return View(model_1);
        }
        

        [HttpPost]

        public IActionResult Edit(EditStudentModel model)
        {
            ViewBag.Courses = new SelectList(_context.Courses.ToList(), "Id", "CourseName");
			if (ModelState.IsValid)
			{
				var student = _context.Students.Find(model.Id);
				if (student == null)
				{
					return NotFound();
				}

				student.Name = model.Name;
				student.Address = model.Address;
				student.Gmail = model.Gmail;
				student.DateofBirth = model.DateofBirth;
				student.CourseId = model.CourseId;

				// Handle image upload if a new image is provided
				if (model.Image != null)
				{
					var fileName = Path.GetFileName(model.Image.FileName);
					var filePath = Path.Combine("wwwroot/images", fileName);
					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						model.Image.CopyTo(stream);
					}
					student.ImageUrl = "/images/" + fileName;
				}

				_context.SaveChanges();

				return RedirectToAction("Index");
			}
			return View(model);
		}

		[HttpGet]
		public IActionResult Delete(int id)
		{
			var student = _context.Students.Find(id);
			if (student == null)
			{
				return NotFound();
			}

			var model = new DeleteStudentModel
			{
				Id = student.Id,
				Name = student.Name,
				ImageUrl = student.ImageUrl
			};

			return View(model);
		}

		[HttpPost]
		public IActionResult Delete(DeleteStudentModel model)
		{
			var student = _context.Students.Find(model.Id);
			if (student == null)
			{
				return NotFound();
			}

			_context.Students.Remove(student);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}


	}

}
