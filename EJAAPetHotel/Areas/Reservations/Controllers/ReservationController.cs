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
using PetHotel.Areas.Reservations.Models;
using PetHotel.Areas.Reservations.Services;

namespace PetHotel.Areas.Reservations.Controllers
{
    [Area("Reservations")]
    [Authorize]
    public class ReservationController : Controller
    {
        private readonly ReservationService _reservationService;

        public ReservationController(ReservationService oReservationService)
        {
            this._reservationService = oReservationService;
        }

        [Authorize(Roles = "1, 2")]
        public IActionResult Index() => View(_reservationService.GetReservationByStatusForTable('A')); //Accept Reservation

        [Authorize(Roles = "1, 2, 3")]
        public IActionResult Home()//Waiting for
        {
            ViewData["ReservationCount"] = _reservationService.GetReservationByStatusForTable('P').Count;
            ViewData["Reservation"] = _reservationService.GetReservationByStatusForTable('P');
            return View();
        }

        [Authorize(Roles = "1, 2")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int reservationId, char ReservationState)
        {
            Reservation oReservation = _reservationService.GetReservation(reservationId);

            oReservation.ReservationState = ReservationState;

            if (reservationId != oReservation.ReservationId) NotFound();

            if (ModelState.IsValid)
            {
                _reservationService.UpdateReservationState(oReservation, User.FindFirst("Email").Value);
                return RedirectToAction(nameof(Home));
            }

            return RedirectToAction(nameof(Home));
        }

        [Authorize(Roles = "1, 2")]
        public IActionResult Details(int? id)
        {
            var oReservation = _reservationService.GetAllReservationForDetails(id);

            if (oReservation == null) return NotFound();

            return View(oReservation);
        }

        [Authorize(Roles = "4")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("DateStart,DateEnd,DateUndefined,StayTypeId, PackageTypeId, DescriptionPetCare, Pet")] Reservation oReservation)
        {
            if (!ModelState.IsValid)
            {
                TempData["CreateError"] = "Hubo un error al hacer una reserva, inténtelo más tarde.";
                return RedirectToAction("Index", "PetHotel", new { area = "" });
            }

            int userID = int.Parse(User.FindFirst("UserId").Value);
            _reservationService.RegisterReservation(oReservation, userID);
            TempData["CreateReservation"] = "Done";
            return RedirectToAction("Index", "PetHotel", new { area = ""});
        }
    }
}