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
    public class AdministrationController : Controller
    {
        private readonly UserManager<HotelManagerUser> _userManager;
        private readonly SignInManager<HotelManagerUser> _signInManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public AdministrationController(
            UserManager<HotelManagerUser> userManager,
            SignInManager<HotelManagerUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
             _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Имейл")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Потребителско Име")]
            public string Username { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Парола")]
            public string Password { get; set; }

            [Required]
            [Display(Name = "Име")]
            public string Name { get; set; }

            [Required]
            [Display(Name = "Презиме")]
            public string SurName { get; set; }

            [Required]
            [Display(Name = "Фамилия")]
            public string FamilyName { get; set; }

            [Required]
            [Display(Name = "ЕГН")]
            public string EGN { get; set; }

            [Required]
            [Display(Name = "Телефонен номер")]
            public string PhoneNumber { get; set; }

            [Required]
            [Display(Name = "Дата на назначаване")]
            public DateTime DateOfApointment { get; set; }

            public string ReturnUrl { get; set; }
        }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

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

        // GET: Administration/Create
        public IActionResult Create()
        {
            return LocalRedirect("/Identity/Account/Register");
        }

        // POST: Administration/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,Email,Password,EGN,Name,SurName,FamilyName,PhoneNumber,DateOfApointment")] HotelManagerUser user)
        {
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            //if (ModelState.IsValid)
            //{
            //    var newuser = new HotelManagerUser
            //    {

            //        UserName = Input.Username,
            //        Email = Input.Email,
            //        Name = Input.Name,
            //        Surname = Input.SurName,
            //        Familyname = Input.FamilyName,
            //        EGN = Input.EGN,
            //        DateOfAppointment = Input.DateOfApointment,
            //        PhoneNumber = Input.PhoneNumber,
            //        IsActive = true
            //    };
            //    var result = await _userManager.CreateAsync(user, Input.Password);
            //    if (result.Succeeded)
            //    {
            //        _logger.LogInformation("User created a new account with password.");

            //        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            //        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            //        return RedirectToAction(nameof(Index));
            //    }
            //    foreach (var error in result.Errors)
            //    {
            //        ModelState.AddModelError(string.Empty, error.Description);
            //    }
            //}

            //// If we got this far, something failed, redisplay form
            //return View(user);
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserName,Email,EGN,Name,Surname,Familyname,PhoneNumber,DateOfAppointment")] HotelManagerUser user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var redactedUser = _userManager.Users.FirstOrDefault(u => u.Id == id);
                    _userManager.Users.FirstOrDefault(u => u.Id == id).Name = user.Name;
                    redactedUser.UserName = user.UserName;
                    redactedUser.Email = user.Email;

                    var hasPassword = await _userManager.HasPasswordAsync(user);
                    if (!hasPassword)
                    {
                        var addPasswordResult = await _userManager.AddPasswordAsync(user, Input.Password);
                        if (!addPasswordResult.Succeeded)
                        {
                            foreach (var error in addPasswordResult.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                        };
                    }

                    redactedUser.EGN = user.EGN;
                    redactedUser.Name = user.Name;
                    redactedUser.Surname = user.Surname;
                    redactedUser.Familyname = user.Familyname;
                    redactedUser.PhoneNumber = user.PhoneNumber;
                    redactedUser.DateOfAppointment = user.DateOfAppointment;
                }
                catch (Exception e)
                {
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            _userManager.Users.FirstOrDefault(u => u.Id == id).IsActive = false;
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(string id)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Id == id);
            if(user == null)
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