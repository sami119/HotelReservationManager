using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelReservationManager.Data;
using HotelReservationManager.Models;
using Microsoft.AspNetCore.Identity;
using HotelReservationManager.Areas.Identity.Data;
using Microsoft.AspNetCore.Http;
using HotelReservationManager.Services;
using Microsoft.AspNetCore.Authorization;

namespace HotelReservationManager.Controllers
{
    [Authorize]
    public class ReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<HotelManagerUser> _userManager;
        private readonly IHttpContextAccessor _httpContext;

        public ReservationsController(ApplicationDbContext context,
            UserManager<HotelManagerUser> userManager,
            IHttpContextAccessor httpContext
            )
        {
            _httpContext = httpContext;
            _userManager = userManager;
            _context = context;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reservations.ToListAsync());
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(m => m.ID == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            var roomNumbers = new List<string>();
            var availibleRooms = _context.Rooms.Where(i => i.IsAvailable == true).ToList();
            foreach (var room in availibleRooms)
            {
                roomNumbers.Add(room.RoomNumber);
            }
            ViewBag.RoomNumbers = roomNumbers;

            var clients = _context.Clients.ToList();
            var clientNames = new List<string>();
            foreach (var client in clients)
            {
                clientNames.Add(client.FirstName + " " + client.LastName);
            }
            ViewBag.ClientNames = clientNames;

            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ReservedRoomID,UserName,DateOfCheckIn,DateOfCheckOut,IncludeBreakfast,AllInclusive,Cost,ClientName")] Reservation reservation)
        {
            Room selectedRoom = _context.Rooms.Where(room => room.RoomNumber == reservation.ReservedRoomID).FirstOrDefault();
            selectedRoom.IsAvailable = false;

            UserIdentityProvider identityProvider = new UserIdentityProvider(_httpContext);
            var curUserId = identityProvider.GetCurrentUserId();
            var curUser = await _userManager.FindByIdAsync(curUserId);
            reservation.UserName = curUser.UserName;

            var selectedClient = _context.Clients.Where(client => client.FirstName + " " + client.LastName == reservation.ClientName).FirstOrDefault();
            if (selectedClient.IsAdult == true)
            {
                reservation.Cost = selectedRoom.PricePerBedForAdult;
            }
            else
            {
                reservation.Cost = selectedRoom.PricePerBedForChild;
            }

            if (reservation.AllInclusive == true)
            {
                reservation.Cost += 100;
            }
            if (reservation.IncludeBreakfast == true)
            {
                reservation.Cost += 15;
            }

            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                _context.Update(selectedRoom);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clients = _context.Clients.ToList();
            var clientNames = new List<string>();
            foreach (var client in clients)
            {
                clientNames.Add(client.FirstName + " " + client.LastName);
            }
            ViewBag.ClientNames = clientNames;

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ReservedRoomID,UserName,DateOfCheckIn,DateOfCheckOut,IncludeBreakfast,AllInclusive,Cost,ClientName")] Reservation reservation)
        {
            if (id != reservation.ID)
            {
                return NotFound();
            }

            var roomName = reservation.ReservedRoomID;
            var selectedRoom = _context.Rooms.Where(room => room.RoomNumber == roomName).FirstOrDefault();

            var selectedClient = _context.Clients.Where(client => client.FirstName + " " + client.LastName == reservation.ClientName).FirstOrDefault();
            if (selectedClient.IsAdult == true)
            {
                reservation.Cost = selectedRoom.PricePerBedForAdult;
            }
            else
            {
                reservation.Cost = selectedRoom.PricePerBedForChild;
            }

            if (reservation.AllInclusive == true)
            {
                reservation.Cost += 100;
            }
            if (reservation.IncludeBreakfast == true)
            {
                reservation.Cost += 15;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.ID))
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
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(m => m.ID == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.ID == id);
        }
    }
}
