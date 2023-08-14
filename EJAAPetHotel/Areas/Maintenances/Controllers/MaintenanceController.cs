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
using PetHotel.Areas.Maintenances.Models;
using PetHotel.Areas.Maintenances.Services;

namespace PetHotel.Areas.Maintenances.Controllers
{
    [Area("Maintenances")]
    [Authorize(Roles = "1, 3")]
    public class MaintenanceController : Controller
    {
        private readonly MaintenanceService _maintenanceService;

        public MaintenanceController(MaintenanceService oMaintenanceService)
        {
            this._maintenanceService = oMaintenanceService;
        }
        public IActionResult Index()
        {
            var employeeList = _maintenanceService.GetEmployeeSelectListItemsByRole(3);
            var maintenanceTypeList = _maintenanceService.GetMaintenanceTypeSelectListItems();
            var roomList = _maintenanceService.GetRoomSelectListItemsByStatus(1);

            ViewData["Maintenance"] = _maintenanceService.GetAllMaintenancesForTable();
            ViewData["Employee"] = new SelectList(employeeList, "Value", "Text");
            ViewData["MaintenanceType"] = new SelectList(maintenanceTypeList, "Value", "Text");
            ViewData["Room"] = new SelectList(roomList, "Value", "Text");
            ViewData["CreateError"] = TempData["CreateError"];

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("MaintenanceTypeId,EmployeeId,RoomId,DateEnd,MaintenanceDescription")] Maintenance oMaintenance)
        {
            if (ModelState.IsValid)
            {
                _maintenanceService.CreateMaintenance(oMaintenance);
                return RedirectToAction(nameof(Index));
            }

            TempData["CreateError"] = "*Hubo un error al crear el mantenimiento, inténtelo de nuevo más tarde.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int maintenanceID, [Bind("MaintenanceId,MaintenanceTypeId,EmployeeId,RoomId,MaintenanceState,DateEnd,MaintenanceDescription")] Maintenance oMaintenance)
        {
            if (maintenanceID != oMaintenance.MaintenanceId) RedirectToAction(nameof(Index));

            if (ModelState.IsValid)
            {
                _maintenanceService.UpdateMaintenance(oMaintenance);
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int maintenanceID)
        {
            _maintenanceService.DeleteMaintenance(maintenanceID);
            return RedirectToAction(nameof(Index));
        }
    }
}