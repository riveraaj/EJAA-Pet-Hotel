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
using PetHotel.Areas.Accounts.Services;
using PetHotel.Areas.Employees.Models;
using PetHotel.Helpers;

namespace PetHotel.Areas.Accounts.Controllers
{
    [Area("Accounts")]
    public class AdminController : Controller
    {
        private readonly AccountService _accountService;

        public AdminController(AccountService oAccountService)
        {
            this._accountService = oAccountService;
        }

        [AllowAnonymous]
        public IActionResult Login() => (User.IsInRole("1") || User.IsInRole("2") || User.IsInRole("3")) ? RedirectToAction("Home", "Reservation", new { area = "Reservations" }) : View();

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Login([Bind("PersonId, Password")] Employee oEmployee)
        {
            //Validations
            if (User.IsInRole("1,2,3")) return RedirectToAction("Home", "Reservation", new { area = "Reservations" });

            if (!ValidatorHelper.ValidateId(oEmployee.PersonId.ToString()) || oEmployee.Password == null)
            {
                ViewData["LoginError"] = "*La cédula debe contener 9 dígitos numéricos y ningún capo debe estar vacío.";
                return View(oEmployee);
            }

            if (!ModelState.IsValid)
            {
                ViewData["LoginError"] = "*Hubo un error en el Inicio de sesión, intentelo más tarde.";
                return View(oEmployee);
            }

            //Login Process
            if (_accountService.LoginEmployee(oEmployee, HttpContext).Result) return RedirectToAction("Home", "Reservation", new { area = "Reservations" });
            else
            {
                ViewData["LoginError"] = "*Hubo un error en el Inicio de sesión, intentelo más tarde.";
                return View(oEmployee);
            }
        }

        [Authorize(Roles = "1,2,3")]
        public async Task<IActionResult> Close()
        {
            await CookiesHelper.RemoveAuthenticationCookie(HttpContext); //Logout
            return RedirectToAction(nameof(Login));
        }
    }
}