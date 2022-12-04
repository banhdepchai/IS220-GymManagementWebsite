using App.Data;
using App.Models;
using App.Models.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Areas.Class.Controllers
{
    [Area("Class")]
    [Authorize(Roles = RoleName.Administrator)]
    [Route("/admin/room/{action=Index}/{id?}")]
    public class RoomController : Controller
    {
        private readonly GymAppDbContext _context;

        public RoomController(GymAppDbContext context)
        {
            _context = context;
        }

        [TempData] public string StatusMessage { set; get; }

        public IActionResult Index()
        {
            var rooms = _context.Rooms.ToList();

            return View(rooms);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("RoomName,Capacity")] Room room)
        {
            if (ModelState.IsValid)
            {
                _context.Rooms.Add(room);
                await _context.SaveChangesAsync();

                StatusMessage = "Thêm phòng tập mới thành công";
                return RedirectToAction("Index");
            }

            return View(room);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var room = _context.Rooms.Find(id);
            if (room == null)
            {
                return NotFound();
            }

            var roomEdit = new Room
            {
                RoomId = room.RoomId,
                RoomName = room.RoomName,
                Capacity = room.Capacity
            };

            return View(roomEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, [Bind("RoomName,Capacity")] Room room)
        {
            if (id != room.RoomId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var roomEdit = await _context.Rooms.FindAsync(id);
                if (roomEdit == null)
                {
                    return NotFound();
                }

                roomEdit.RoomName = room.RoomName;
                roomEdit.Capacity = room.Capacity;

                await _context.SaveChangesAsync();

                StatusMessage = "Cập nhật phòng tập thành công";
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var room = _context.Rooms.Find(id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var room = _context.Rooms.Find(id);
            if (room == null)
            {
                return NotFound();
            }

            _context.Rooms.Remove(room);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var room = _context.Rooms.Find(id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }
    }
}