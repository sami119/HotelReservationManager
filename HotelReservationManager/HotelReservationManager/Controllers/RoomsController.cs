using HotelReservationManager.Data;
using HotelReservationManager.Models;
using HotelReservationManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationManager.Controllers
{
    [Authorize]
    public class RoomsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private int lastPageSize;

        public RoomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rooms
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber, int pageSize)
        {
            ViewData["CapacitySortParm"] = String.IsNullOrEmpty(sortOrder) ? "capacity_desc" : "capacity";
            ViewData["_TypeSortParm"] = sortOrder == "_type" ? "_type_desc" : "_type";
            ViewData["IsAvailableSortParm"] = sortOrder == "IsAvailable" ? "isAvailable_desc" : "IsAvaliable";
            //ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["PageSize"] = pageSize;
            if (TempData["LastPageSize"] != null)
            {
                int lastPageSize = (int)TempData["LastPageSize"];
            }

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var rooms = from s in _context.Rooms select s;

            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    rooms = rooms.Where(s => s.RoomNumber.Contains(searchString));
            //}

            switch (sortOrder)
            {
                case "capacity":
                    rooms = rooms.OrderBy(s => s.Capacity);
                    break;
                case "capacity_desc":
                    rooms = rooms.OrderByDescending(s => s.Capacity);
                    break;
                case "_type":
                    rooms = rooms.OrderBy(s => s._Type);
                    break;
                case "_type_desc":
                    rooms = rooms.OrderByDescending(s => s._Type);
                    break;
                case "IsAvaliable":
                    rooms = rooms.OrderBy(s => s.IsAvailable);
                    break;
                case "isAvailable_desc":
                    rooms = rooms.OrderByDescending(s => s.IsAvailable);
                    break;
                default:
                    rooms = rooms.OrderBy(s => s.Capacity);
                    break;
            }

            if (pageSize != 0)
            {
                lastPageSize = pageSize;
            }
            else if (lastPageSize == 0 && pageSize == 0)
            {
                lastPageSize = 10;
            }

            return View(await PaginatedList<Room>.CreateAsync(rooms.AsNoTracking(), pageNumber ?? 1, lastPageSize));
        }
        

        // GET: Rooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .FirstOrDefaultAsync(m => m.ID == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // GET: Rooms/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("ID,Capacity,_Type,IsAvailable,PricePerBedForAdult,PricePerBedForChild,RoomNumber")] Room room)
        {
            if (ModelState.IsValid)
            {
                _context.Add(room);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        // GET: Rooms/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Capacity,_Type,IsAvailable,PricePerBedForAdult,PricePerBedForChild,RoomNumber")] Room room)
        {
            if (id != room.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(room);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomExists(room.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        // GET: Rooms/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .FirstOrDefaultAsync(m => m.ID == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.ID == id);
        }
    }
}
