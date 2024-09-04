using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ViewModelTableFormation.Data;
using ViewModelTableFormation.Models;

namespace ViewModelTableFormation.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CourseController : Controller
    {
        private readonly SchoolIdentityDbContext _context;

        public CourseController(SchoolIdentityDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public async Task<IActionResult> Index()
        {
            ViewBag.Faculties = _context.Faculties.ToList();
            var models = await _context.Courses.ToListAsync();
            if (models == null)
            {
                return View("Error");
            }
            return View(models);
        }

        public IActionResult Create()
        {
            //ViewData.Faculties = new SelectList(_context.Faculties.ToList(), "Id", "Name");
            ViewBag.Faculties = _context.Faculties.ToList();




            return View();
        }

        [HttpPost]
        public IActionResult Create(Course course)
        {
            ViewBag.Faculties = _context.Faculties.ToList();

            if (ModelState.IsValid)
            {
                _context.Courses.Add(course);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
         
            return View(course);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Faculties = new SelectList(_context.Faculties.ToList(), "Id", "Name");
            var model = _context.Courses.Find(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }


        [HttpPost]

        public IActionResult Edit(Course course)
        {
            ViewBag.Faculties = new SelectList(_context.Faculties.ToList(), "Id", "Name");
            if (ModelState.IsValid)
            {
                _context.Courses.Update(course);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Course record updated successfully";

                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "There was an error updating the record";
            return View(course);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var model = _context.Courses.Find(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(Course course)
        {
            var model = _context.Courses.Remove(course);
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Course record deleted successfully";
            return RedirectToAction("Index");
        }


    }

}
