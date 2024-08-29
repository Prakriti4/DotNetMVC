using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ViewModelTableFormation.Models;

namespace ViewModelTableFormation.Controllers
{
    public class StudentController : Controller
    {
        private readonly SchoolDbContext _context;

    

        public StudentController(SchoolDbContext context)
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
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            ViewBag.Course = new SelectList(_context.Courses, "Id", "CourseName");
            if (ModelState.IsValid)
            {
                string uniqueFile = UploadedFile(student);
                student.ImageUrl=uniqueFile;
                _context.Add(student);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }


        public async Task<IActionResult> Details(int id)
        {
            if(id == 0)
            {
                return NotFound();

            }
            var student = await _context.Students.Include(s=>s.Course).FirstOrDefaultAsync(x => x.Id == id);
            return View(student);
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
            return View(model);
        }


        [HttpPost]

        public IActionResult Edit(Student student)
        {
            ViewBag.Courses = new SelectList(_context.Courses.ToList(), "Id", "CourseName");
            if (ModelState.IsValid)
            {
                _context.Students.Update(student);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Student record updated successfully";

                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "There was an error updating the record";
            return View(student);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var model = _context.Students.Find(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(Student student)
        {
            var model = _context.Students.Remove(student);
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Student record deleted successfully";
            return RedirectToAction("Index");
        }


    }

}
