using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ViewModelTableFormation.Data;
using ViewModelTableFormation.Models;

namespace ViewModelTableFormation.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TeacherController : Controller
    {
        private readonly SchoolIdentityDbContext _context;

        public TeacherController(SchoolIdentityDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Courses = _context.Courses.ToList();
            var models = await _context.Teachers.ToListAsync();
            if (models == null)
            {
                return View("Error");
            }
            return View(models);
        }

        public IActionResult Create()
        {
       
            ViewBag.Courses = new SelectList(_context.Courses.ToList(), "Id", "CourseName");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Teacher teacher)
        {
            ViewBag.Courses = new SelectList(_context.Courses.ToList(), "Id", "CourseName");
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(teacher);
                teacher.ImageUrl = uniqueFileName;
                _context.Teachers.Add(teacher);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(teacher);
        }

        public string UploadedFile(Teacher model)
        {
            string uniqueFileName = null;
            if (model.Image != null)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Image.CopyTo(fileStream);
                }

            }
            return uniqueFileName;
        }
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();

            }
            var teacher = await _context.Teachers.Include(s => s.Course).FirstOrDefaultAsync(x => x.Id == id);
            return View(teacher);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Courses = new SelectList(_context.Courses.ToList(), "Id", "CourseName");
            var model = _context.Teachers.Find(id);

            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }


        [HttpPost]

        public IActionResult Edit(Teacher teacher)
        {
            ViewBag.Courses = new SelectList(_context.Courses.ToList(), "Id", "CourseName");
          
            if (ModelState.IsValid)
            {
                string uniqueFile = UploadedFile(teacher);
                teacher.ImageUrl = uniqueFile;
                _context.Teachers.Update(teacher);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Teacher record updated successfully";

                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "There was an error updating the record";
            return View(teacher);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var model = _context.Teachers.Find(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(Teacher teacher)
        {
            var model = _context.Teachers.Remove(teacher);
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Teacher record deleted successfully";
            return RedirectToAction("Index");
        }


    }

}
