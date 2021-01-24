using CMSTekrar.Entity.Entities.Concrete;
using CMSTekrar.Web.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSTekrar.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _singInManager;
        private IPasswordHasher<AppUser> _passwordHasher;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager, IPasswordHasher<AppUser> passwordHasher)
        {
            _userManager = userManager;
            _singInManager = signInManager;
            _passwordHasher = passwordHasher;
        }
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost,ValidateAntiForgeryToken,AllowAnonymous]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser
                {
                    UserName = user.UserName,
                    Email = user.Email
                };
                IdentityResult result = await _userManager.CreateAsync(appUser, user.Password);
                if (result.Succeeded) 
                {
                    return RedirectToAction("Login");               
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            return View(user);
        }
        [AllowAnonymous]
        public IActionResult LogIn(string returnUrl)
        {
            Login login = new Login { ReturnUrl = returnUrl };

            return View(login);
        }
        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(Login login)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = await _userManager.FindByNameAsync(login.UserName);

                if (appUser != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult signInResult = await _singInManager.PasswordSignInAsync(appUser.UserName, login.Password, false, false);

                    if (signInResult.Succeeded)
                    {
                        return Redirect("login.ReturnUrl" ?? "/");
                    }

                    ModelState.AddModelError("", "The login failed because of wrong credential information..!");
                }
            }
            return View(login);
        }
        public async Task<IActionResult> LogOut()
        {
            await _singInManager.SignOutAsync();

            return RedirectToAction("LogIn");
        }
        public async Task<IActionResult> Edit()
        {
            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);

            UserEdit userEdit = new UserEdit(appUser);

            return View(userEdit);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEdit userEdit)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);

                appUser.UserName = userEdit.UserName;
                if (userEdit.Password != null)
                {
                    appUser.PasswordHash = _passwordHasher.HashPassword(appUser, userEdit.Password);
                }

                IdentityResult ıdentityResult = await _userManager.UpdateAsync(appUser);
                if (ıdentityResult.Succeeded)
                {
                    TempData["Success"] = "Your information has been edited..!";
                }
                else
                {
                    TempData["Warning"] = "Your information has been wrong..!";
                }
            }

            return View(userEdit);


        }
    }
}
