/*
 * Copyright [2023] [Jonathan Rivera Vasquez]
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHotel.Areas.Accounts.Models;
using PetHotel.Areas.Accounts.Services;
using PetHotel.Helpers;

namespace PetHotel.Areas.Accounts.Controllers
{
    [Area("Accounts")]
    public class PublicController : Controller
    {
        private readonly AccountService _accountService;

        public PublicController(AccountService oAccountService)
        {
            this._accountService = oAccountService;
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Login([Bind("PersonId, Password")] User oUser)
        {
            //Validations
            if (User.IsInRole("4")) return RedirectToAction("Index", "Home");

            if (!ValidatorHelper.ValidateId(oUser.PersonId.ToString()) || oUser.Password == null)
            {
                TempData["LoginError"] = "*La cédula debe contener 9 dígitos numéricos y ningún capo debe estar vacío.";
                return RedirectToAction("Login", "PetHotel", new { area = "" });
            }

            if (!ModelState.IsValid)
            {
                TempData.Set("LoginUser", oUser);
                TempData["LoginError"] = "*Hubo un error en el Inicio de sesión, intentelo más tarde.";
                return RedirectToAction("Login", "PetHotel", new { area = "" });
            }

            //Login Process
            if (_accountService.LoginUser(oUser, HttpContext).Result) return RedirectToAction("Index", "PetHotel", new { area = "" });
            else
            {
                TempData.Set("LoginUser", oUser);
                TempData["LoginError"] = "*Hubo un error al iniciar sesión, intentelo más tarde.";
                return RedirectToAction("Login", "PetHotel", new { area = "" });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Register(string PasswordConfirm, [Bind("UserId, RoleId, Password, Person")] User oUser)
        {
            //Validations
            if (User.IsInRole("4")) return RedirectToAction("Index", "Home");

            if (!ValidatorHelper.ValidateId(oUser.Person.PersonId.ToString()))
            {
                TempData.Set("RegisterUser", oUser);
                TempData["RegisterError"] = "*La cédula debe contener 9 dígitos numéricos.";
                return RedirectToAction("Register", "PetHotel", new { area = "" });
            }

            if (!PasswordConfirm.Equals(oUser.Password))
            {
                TempData.Set("RegisterUser", oUser);
                TempData["RegisterError"] = "*Las contraseñas deben de coincidir.";
                return RedirectToAction("Register", "PetHotel", new { area = "" });
            }

            if (!ValidatorHelper.ValidateEmail(oUser.Person.Email))
            {
                TempData.Set("RegisterUser", oUser);
                TempData["RegisterError"] = "*Ingrese una dirección de correo electrónico válida.";
                return RedirectToAction("Register", "PetHotel", new { area = "" });
            }

            if (!ValidatorHelper.ValidatePassword(oUser.Password.ToString()))
            {
                TempData.Set("RegisterUser", oUser);
                TempData["RegisterError"] = "*La contraseña debe contener al menos una letra mayúscula, una letra minúscula y un número.";
                return RedirectToAction("Register", "PetHotel", new { area = "" });
            }
            if (!ModelState.IsValid)
            {
                TempData.Set("RegisterUser", oUser);
                TempData["RegisterError"] = "*Hubo un error al hacer el registro, intentelo más tarde.";
                return RedirectToAction("Register", "PetHotel", new { area = "" });
            }

            if (_accountService.IsUserExists(oUser.Person.PersonId))
            {
                TempData.Set("RegisterUser", oUser);
                TempData["RegisterError"] = "*Este usuario ya existe.";
                return View();
            }

            //Register Process
            _accountService.RegisterUser(oUser);
            return RedirectToAction("Login", "PetHotel", new { area = "" });
        }

        [Authorize(Roles = "4")]
        public async Task<IActionResult> Close()
        {
            await CookiesHelper.RemoveAuthenticationCookie(HttpContext); //Logout
            return RedirectToAction("Index", "PetHotel", new { area = "" });
        }
    }
}