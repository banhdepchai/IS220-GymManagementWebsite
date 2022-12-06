using App.Data;
using App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using ClassModel = App.Models.Classes.Class;

namespace App.Areas.Class.Controllers
{
    [Area("Class")]
    [Route("admin/{controller}/{action}/{id?}")]
    [Authorize(Roles = RoleName.Administrator + "," + RoleName.Editor)]
    public class ClassController : Controller
    {
        private readonly GymAppDbContext _context;

        public ClassController(GymAppDbContext context)
        {
            _context = context;
        }

        [TempData] public string? StatusMessage { get; set; }

        // GET: Product/Product
        public async Task<IActionResult> Index([FromQuery(Name = "p")] int currentPage, int pagesize)
        {
            var classes = _context.Classes
                .Include(i => i.Instructor)
                .Include(r => r.Room);

            int totalClasses = await classes.CountAsync();
            if (pagesize <= 0) pagesize = 5;
            int countPages = (int)Math.Ceiling((double)totalClasses / pagesize);

            if (currentPage > countPages) currentPage = countPages;
            if (currentPage < 1) currentPage = 1;

            var pagingModel = new PagingModel()
            {
                countpages = countPages,
                currentpage = currentPage,
                generateUrl = (pageNumber) => Url.Action("Index", new
                {
                    p = pageNumber,
                    pagesize = pagesize
                }) ?? string.Empty
            };

            ViewBag.pagingModel = pagingModel;
            ViewBag.totalClasses = totalClasses;

            ViewBag.classIndex = (currentPage - 1) * pagesize;

            var classesInPage = await classes.Skip((currentPage - 1) * pagesize)
                .Take(pagesize)
                .ToListAsync();

            return View(classesInPage);
        }

        public IActionResult Details(int id)
        {
            var classModel = _context.Classes.Include(i => i.Instructor).Include(r => r.Room).FirstOrDefault(c => c.ClassId == id);
            if (classModel == null)
            {
                return NotFound();
            }

            return View(classModel);
        }

        public async Task<IActionResult> Create()
        {
            var rooms = new SelectList(await _context.Rooms.ToListAsync(), "RoomId", "RoomName");
            ViewData["rooms"] = rooms;
            var instructors = new SelectList(await _context.Instructors.ToListAsync(), "Id", "Name");
            ViewData["instructors"] = instructors;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClassTitle,ClassDate,ClassPeriod,ClassCost,RoomId,InstructorId")] ClassModel classModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classModel);

                await _context.SaveChangesAsync();
                StatusMessage = "Vừa tạo khóa tập mới: " + classModel.ClassTitle;
                return RedirectToAction(nameof(Index));
            }

            return View(classModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classModel = await _context.Classes.FindAsync(id);
            if (classModel == null)
            {
                return NotFound();
            }

            var classEdit = new ClassModel()
            {
                ClassId = classModel.ClassId,
                ClassTitle = classModel.ClassTitle,
                ClassDate = classModel.ClassDate,
                ClassPeriod = classModel.ClassPeriod,
                ClassCost = classModel.ClassCost,
                RoomId = classModel.RoomId,
                InstructorId = classModel.InstructorId
            };
            ViewData["rooms"] = new SelectList(await _context.Rooms.ToListAsync(), "RoomId", "RoomName");
            ViewData["instructors"] = new SelectList(await _context.Instructors.ToListAsync(), "Id", "Name");

            return View(classEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ClassId,ClassTitle,ClassDate,ClassPeriod,ClassCost,RoomId,InstructorId")] ClassModel classModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var classUpdate = await _context.Classes.FindAsync(classModel.ClassId);
                    if (classUpdate == null)
                    {
                        return NotFound();
                    }

                    classUpdate.ClassTitle = classModel.ClassTitle;
                    classUpdate.ClassDate = classModel.ClassDate;
                    classUpdate.ClassPeriod = classModel.ClassPeriod;
                    classUpdate.ClassCost = classModel.ClassCost;
                    classUpdate.RoomId = classModel.RoomId;
                    classUpdate.InstructorId = classModel.InstructorId;

                    _context.Update(classUpdate);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassExists(classModel.ClassId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                StatusMessage = "Đã cập nhật khóa tập: " + classModel.ClassTitle;
                return RedirectToAction(nameof(Index));
            }
            return View(classModel);
        }

        // GET: Blog/Post/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classModel = await _context.Classes.FirstOrDefaultAsync(m => m.ClassId == id);
            if (classModel == null)
            {
                return NotFound();
            }

            return View(classModel);
        }

        // POST: Blog/Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var classModel = await _context.Classes.FindAsync(id);

            if (classModel == null)
            {
                return NotFound();
            }

            _context.Classes.Remove(classModel);
            await _context.SaveChangesAsync();

            StatusMessage = "Bạn vừa xóa khóa tập: " + classModel.ClassTitle;

            return RedirectToAction(nameof(Index));
        }

        private bool ClassExists(int id)
        {
            return _context.Classes.Any(e => e.ClassId == id);
        }
    }
}