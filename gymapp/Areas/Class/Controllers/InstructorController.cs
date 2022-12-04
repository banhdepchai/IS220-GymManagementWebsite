using App.Data;
using App.Models;
using App.Models.Classes;
using App.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace App.Areas.Class.Controllers
{
    [Area("Class")]
    [Authorize(Roles = RoleName.Administrator)]
    [Route("admin/{controller}/{action}/{id?}")]
    public class InstructorController : Controller
    {
        private readonly GymAppDbContext _context;

        public InstructorController(GymAppDbContext context)
        {
            _context = context;
        }

        [TempData] public string StatusMessage { set; get; }

        public IActionResult Index()
        {
            var instructors = _context.Instructors.ToList();

            return View(instructors);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,DateOfBirth,Gender,Email,Phone,Expertise,Salary")] Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                _context.Instructors.Add(instructor);
                await _context.SaveChangesAsync();

                StatusMessage = "Thêm mới huấn luyện viên thành công";
                return RedirectToAction("Index");
            }

            return View(instructor);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors.FindAsync(id);
            if (instructor == null)
            {
                return NotFound();
            }

            var instructorEdit = new Instructor()
            {
                Id = (int)id,
                Name = instructor.Name,
                DateOfBirth = instructor.DateOfBirth,
                Gender = instructor.Gender,
                Email = instructor.Email,
                Phone = instructor.Phone,
                Expertise = instructor.Expertise,
                Salary = instructor.Salary
            };

            return View(instructorEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id,Name,DateOfBirth,Gender,Email,Phone,Expertise,Salary")] Instructor instructor)
        {
            //if (id != instructor.Id)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    var instructorEdit = await _context.Instructors.FindAsync(instructor.Id);
                    if (instructorEdit == null)
                    {
                        return NotFound();
                    }

                    instructorEdit.Name = instructor.Name;
                    instructorEdit.DateOfBirth = instructor.DateOfBirth;
                    instructorEdit.Gender = instructor.Gender;
                    instructorEdit.Email = instructor.Email;
                    instructorEdit.Phone = instructor.Phone;
                    instructorEdit.Expertise = instructor.Expertise;
                    instructorEdit.Salary = instructor.Salary;

                    _context.Update(instructorEdit);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstructorExists(instructor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                StatusMessage = "Vừa cập nhật huấn luyện viên";
                return RedirectToAction(nameof(Index));
            }
            return View(instructor);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var instructor = _context.Instructors.Find(id);
            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        [HttpPost]
        public IActionResult Delete(Instructor instructor)
        {
            _context.Instructors.Remove(instructor);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var instructor = _context.Instructors.Find(id);
            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        private bool InstructorExists(int id)
        {
            return _context.Instructors.Any(e => e.Id == id);
        }
    }
}