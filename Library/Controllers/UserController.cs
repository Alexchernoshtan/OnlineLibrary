using Library.Models;
using Library.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Controllers
{
    public class UserController:Controller
    {

        UserManager<User> _userManager;
        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            User user = _userManager.FindByIdAsync(userId).Result;
            return View(user);
        }
        public async Task<IActionResult> ChangePassword()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            User user = _userManager.FindByIdAsync(userId).Result;
            if (user == null)
            {
                return NotFound();
            }
            ChangePasswordViewModel model = new ChangePasswordViewModel { Id = user.Id, Email = user.Email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(HttpContext.User);
                User user = _userManager.FindByIdAsync(userId).Result;
                if (user != null)
                {
                    IdentityResult result =
                        await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
                }
            }
            return View(model);
        }
        public async Task<IActionResult> Edit()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            User user = _userManager.FindByIdAsync(userId).Result;
            if (user == null)
            {
                return NotFound();
            }
            EditViewModel model = new EditViewModel { Id = user.Id, Email = user.Email, Year = user.Year };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(HttpContext.User);
                User user = _userManager.FindByIdAsync(userId).Result;
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    user.Year = model.Year;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }

    }
}
