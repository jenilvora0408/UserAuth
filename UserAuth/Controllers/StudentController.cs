using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using UserAuth.Entities.Models;
using UserAuth.Entities.ViewModels;
using UserAuth.Repository.Interface;

namespace UserAuth.Controllers
{
    public class StudentController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IStudentRepository _studentRepository;
        public StudentController(ICourseRepository courseRepository, IStudentRepository studentRepository) 
        {
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UpsertStudent(int? Id)
        {
            List<SelectListItem> statusList = new()
                {
                    new SelectListItem { Value = "1", Text = "Active" },
                    new SelectListItem { Value = "2", Text = "InActive" }
                };
            ViewBag.Status = statusList;

            List<Course> courses = await _courseRepository.GetAllAsync();

            List<SelectListItem> courseList = courses.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.CourseTitle
            }).ToList();

            ViewBag.Course = courseList;

            if (Id == null)
            {
                UpsertStudentViewModel model = new()
                {
                    DateOfBirth = DateTime.UtcNow
                };
                return View(model);
            }
            else
            {
                Student? student = await _studentRepository.GetByIdAsync(Id);
                Course? course = await _courseRepository.GetByIdAsync(student.CourseId);

                UpsertStudentViewModel model = new()
                {
                    StudentId = student.Id,
                    Name = student.Name,
                    Enrollment = student.Enrollment,
                    Status = student.Status,
                    CourseId = student.CourseId,
                    CourseName = course.CourseTitle,
                    DateOfBirth = student.DateOfBirth
                };

                statusList.ForEach(s => s.Selected = (s.Value == student.Status.ToString()));
                courseList.ForEach(c => c.Selected = (c.Value == student.CourseId.ToString()));

                return View(model);
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> UpsertStudent(UpsertStudentViewModel model)
        {
            Student student = new()
            {
                Name = model.Name ?? string.Empty,
                Enrollment = model.Enrollment ?? string.Empty,
                CourseId = model.CourseId ?? 0,
                Status = model.Status ?? 0,
                DateOfBirth = model.DateOfBirth ?? DateTime.UtcNow
            };

            //Add
            if (model.StudentId == null)
            {
                await _studentRepository.AddAsync(student);
            }
            else
            {
                await _studentRepository.UpdateAsync(student);
            }

            return RedirectToAction("GetAllStudents");
        }

        public async Task<IActionResult> GetAllStudents(string? search, string? filter, string? sortColumn, string? sortDirection)
        {
            sortDirection = sortDirection?.ToLower() == "asc" ? "asc" : "desc";

            // Fetch students with Courses using filtering & sorting
            var students = await _studentRepository.GetAllAsync(
                filter: s =>
                    (string.IsNullOrEmpty(search) || s.Name.Contains(search) || s.Courses.CourseTitle.Contains(search)) &&
                    (string.IsNullOrEmpty(filter) || (filter == "Active" && s.Status == 1) || (filter == "Inactive" && s.Status == 2)),
                orderBy: q =>
                {
                    return sortColumn switch
                    {
                        "StudentId" => sortDirection == "asc" ? q.OrderBy(s => s.Id) : q.OrderByDescending(s => s.Id),
                        "Name" => sortDirection == "asc" ? q.OrderBy(s => s.Name) : q.OrderByDescending(s => s.Name),
                        "Enrollment" => sortDirection == "asc" ? q.OrderBy(s => s.Enrollment) : q.OrderByDescending(s => s.Enrollment),
                        _ => q.OrderBy(s => s.Id) // Default sorting
                    };
                },
                s => s.Courses // Include Courses
            );

            // Convert to ViewModel
            List<UpsertStudentViewModel> models = students.Select(s => new UpsertStudentViewModel
            {
                StudentId = s.Id,
                Name = s.Name,
                Enrollment = s.Enrollment,
                Status = s.Status,
                DateOfBirth = s.DateOfBirth,
                CourseName = s.Courses.CourseTitle
            }).ToList();

            return View(models);
        }


        public async Task<IActionResult> DeleteStudent(int Id)
        {
            Student? student = await _studentRepository.GetByIdAsync(Id);
            if(student != null)
            {
                await _studentRepository.DeleteAsync(student);
            }
            return RedirectToAction("GetAllStudents");
        }
    }
}
