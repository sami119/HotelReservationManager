using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelReservationManager.Areas.Identity.Data;
using HotelReservationManager.Areas.Identity.Pages.Account;
using HotelReservationManager.Data;
using HotelReservationManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;

namespace HotelReservationManager.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdministrationController : Controller
    {

        private readonly UserManager<HotelManagerUser> _userManager;

        /// <summary>
        /// Initializes _userManager
        /// </summary>
        /// <param name="userManager"></param>
        public AdministrationController(
            UserManager<HotelManagerUser> userManager)
        {
            _userManager = userManager;
        }

        // GET: Administration
        public async Task<IActionResult> Index()
        {
            return View(await _userManager.Users.ToListAsync());
        }

        // GET: Administration/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _userManager.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        /// <summary>
        /// Redirects to Areas/Identity/Account/Register
        /// </summary>
        /// <returns></returns>
        // GET: Administration/Create
        public IActionResult Create()
        {
            return LocalRedirect("/Identity/Account/Register");
        }

        // POST: Administration/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Username,Email,Password,EGN,Name,SurName,FamilyName,PhoneNumber,DateOfApointment")] HotelManagerUser user)
        {
            return LocalRedirect("/Identity/Account/Register");
        }

        // GET: Administration/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _userManager.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Administration/Edit/5
        /// <summary>
        /// Editing method. Uses userManager to change the selected columns of the user databse
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserName,Email,EGN,Name,Surname,Familyname,PhoneNumber,DateOfAppointment,DateOfDismissal,IsActive")] HotelManagerUser user)
        {
            var redactedUser = _userManager.Users.FirstOrDefault(u => u.Id == id);

            if (id != user.Id)
            {
                return NotFound();
            }

            //for debug
            var errors = ModelState
                            .Where(x => x.Value.Errors.Count > 0)
                            .Select(x => new { x.Key, x.Value.Errors })
                            .ToArray();

            //if (ModelState.IsValid)
            //{
            try
            {
                redactedUser.Name = user.Name;
                redactedUser.UserName = user.UserName;
                redactedUser.Email = user.Email;
                redactedUser.EGN = user.EGN;
                redactedUser.Name = user.Name;
                redactedUser.Surname = user.Surname;
                redactedUser.Familyname = user.Familyname;
                redactedUser.PhoneNumber = user.PhoneNumber;
                redactedUser.DateOfAppointment = user.DateOfAppointment;
                redactedUser.DateOfDismissal = user.DateOfDismissal;
                redactedUser.IsActive = user.IsActive;

                //var hasPassword = await _userManager.HasPasswordAsync(user);
                //if (!hasPassword)
                //{
                //    var addPasswordResult = await _userManager.AddPasswordAsync(user, ViewBag["Password"]);
                //    if (!addPasswordResult.Succeeded)
                //    {
                //        foreach (var error in addPasswordResult.Errors)
                //        {
                //            ModelState.AddModelError(string.Empty, error.Description);
                //        }
                //    };
                //}
            }
            catch (Exception)
            {
            }
            await _userManager.UpdateAsync(redactedUser);
            return RedirectToAction(nameof(Index));
            //}
            //return View(user);
        }

        // GET: Administration/Delete/5
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _userManager.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Administration/Delete/5
        /// <summary>
        /// Delete Method. Uses userManager to override the IsActive state of the User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedAsync(string id)
        {
            var deactivatedUser = _userManager.Users.FirstOrDefault(u => u.Id == id);
            deactivatedUser.IsActive = false;
            await _userManager.UpdateAsync(deactivatedUser);
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(string id)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}