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
using Microsoft.AspNetCore.Mvc.Rendering;
using PetHotel.Areas.Accounts.Models;
using PetHotel.Areas.Reservations.Services;
using PetHotel.Helpers;

namespace PetHotel.Controllers
{
    public class PetHotelController : Controller
    {
        private readonly ReservationService _reservationService;

        public PetHotelController(ReservationService oReservationService)
        {
            this._reservationService = oReservationService;
        }

        public IActionResult NotFound() => PartialView("_NotFound");
        

        public IActionResult Index()
        {
            ViewBag.Rooms = _reservationService.GetAllRooms(1).Count;
            ViewData["StayTypeId"] = new SelectList(_reservationService.GetStayTypeSelectListItems(), "Value", "Text");
            if (TempData["CreateError"] is string message) ViewData["CreateError"] = message;
            if (TempData["CreateReservation"] is string alert) ViewData["CreateReservation"] = alert;
            return View();
        }

        public IActionResult Specialist() => View();

        public IActionResult Rooms() => View();

        public IActionResult About() => View();

        public IActionResult Contact() => View();

        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User.IsInRole("4")) RedirectToAction(nameof(Index));

            if (TempData["LoginError"] is string message) ViewData["LoginError"] = message;

            User? oUser = TempData.Get<User>("LoginUser");

            if (oUser != null) return View(oUser);

            return View();
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User.IsInRole("4")) RedirectToAction(nameof(Index));

            if (TempData["RegisterError"] is string message) ViewData["RegisterError"] = message;

            User? oUser = TempData.Get<User>("RegisterUser");

            if (oUser != null) return View(oUser);

            return View();
        }

        public async Task<IActionResult> Reservation()
        {
            if (User.IsInRole("4"))
            {
                int userID = int.Parse(User.FindFirst("UserId").Value);
                var reservationList = _reservationService.GetReservationByUserForTable(userID);
                if (reservationList == null) return NotFound();
                return View(reservationList);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}