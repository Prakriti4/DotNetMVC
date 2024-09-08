using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using ViewModelTableFormation.Models;
using ViewModelTableFormation.Repository;
using ViewModelTableFormation.ViewModel;
using ViewModelTableFormation.UnitOfWork;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class StudentController : Controller
{
    private readonly IStudentRepository _studentRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment; // Use IWebHostEnvironment for path handling

    public StudentController(IStudentRepository studentRepository, IMapper mapper, IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
    {
        _studentRepository = studentRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<IActionResult> Index()
    {
        var courses= await _unitOfWork.Courses.GetAllAsync();
        var students = await _studentRepository.GetAllAsync();
        var studentDetailsModels = _mapper.Map<IEnumerable<StudentDetailsModel>>(students);
        var courseViewModels = _mapper.Map<IEnumerable<CourseModel>>(courses);
        if (students == null)
        {
            return View("Error");
        }
        return View(studentDetailsModels);
    }

    public async Task<IActionResult> Create()
    {
        var courses = await _studentRepository.GetAllCoursesAsync();
        ViewBag.Course = new SelectList(courses, "Id", "CourseName");
        return View(new CreateStudentModel());
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateStudentModel model)
    {
        if (ModelState.IsValid)
        {
            var student = _mapper.Map<Student>(model);

            if (model.Image != null)
            {
                student.ImageUrl = UploadedFile(model.Image);
            }

            await _studentRepository.AddAsync(student);
            await _unitOfWork.CompleteAsync();

            return RedirectToAction("Index");
        }

        var courses = await _studentRepository.GetAllCoursesAsync();
        ViewBag.Course = new SelectList(courses, "Id", "CourseName");
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var student = await _studentRepository.GetByIdAsync(id);
        if (student == null)
        {
            return NotFound();
        }

        var studentViewModel = _mapper.Map<StudentDetailsModel>(student);
        return View(studentViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var student = await _studentRepository.GetByIdAsync(id);
        if (student == null)
        {
            return NotFound();
        }

        var model = _mapper.Map<EditStudentModel>(student);
        var courses = await _studentRepository.GetAllCoursesAsync();
        ViewBag.Courses = new SelectList(courses, "Id", "CourseName");
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditStudentModel model)
    {
        if (ModelState.IsValid)
        {
            var student = await _studentRepository.GetByIdAsync(model.Id);
            if (student == null)
            {
                return NotFound();
            }

            _mapper.Map(model, student);

            if (model.Image != null)
            {
                student.ImageUrl = UploadedFile(model.Image);
            }

            await _studentRepository.UpdateAsync(student);
            await _unitOfWork.CompleteAsync();

            return RedirectToAction("Index");
        }

        var courses = await _studentRepository.GetAllCoursesAsync();
        ViewBag.Courses = new SelectList(courses, "Id", "CourseName");
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var student = await _studentRepository.GetByIdAsync(id);
        if (student == null)
        {
            return NotFound();
        }

        var deleteStudentModel = _mapper.Map<DeleteStudentModel>(student);
        return View(deleteStudentModel);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(DeleteStudentModel model)
    {
        var student = await _studentRepository.GetByIdAsync(model.Id);
        if (student == null)
        {
            return NotFound();
        }

        await _studentRepository.DeleteAsync(model.Id);
        await _unitOfWork.CompleteAsync();

        return RedirectToAction("Index");
    }

    private string UploadedFile(IFormFile image)
    {
        if (image != null)
        {
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(image.FileName);
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(fileStream);
            }
            return "/images/" + uniqueFileName;
        }
        return null;
    }
}
