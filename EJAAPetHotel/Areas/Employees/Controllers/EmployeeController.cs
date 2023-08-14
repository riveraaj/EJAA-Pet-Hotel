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
using PetHotel.Areas.Employees.Models;
using PetHotel.Areas.Employees.Services;
using PetHotel.Helpers;
using PetHotel.Services;

namespace PetHotel.Areas.Employees.Controllers
{
    [Area("Employees")]
    [Authorize(Roles = "1")]
    public class EmployeeController : Controller
    {
        private readonly PersonService _personService;
        private readonly EmployeeService _employeeService;

        public EmployeeController(PersonService oPersonService, EmployeeService oEmployeeService)
        {
            this._personService = oPersonService;
            this._employeeService = oEmployeeService;
        }

        public IActionResult Index()
        {
            var rolList = _employeeService.GetRolSelectListItemsByDescription();
            var employeeList = _employeeService.GetAllEmployeesForTable();
            ViewData["Role"] = new SelectList(rolList, "Value", "Text");
            ViewData["CreateError"] = TempData["CreateError"];
            ViewData["Employee"] = employeeList;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("EmployeeId, RoleId, Password, DateEntry, Person")] Employee oEmployee)
        {
            if (!ValidatorHelper.ValidateId(oEmployee.Person.PersonId.ToString()))
            {
                TempData["CreateError"] = "*La cédula debe contener 9 dígitos numéricos.";
            }else if (!ValidatorHelper.ValidateEmail(oEmployee.Person.Email))
            {
                TempData["CreateError"] = "*Ingrese una dirección de correo electrónico válida.";
            }else if (!ModelState.IsValid)
            {
                TempData["CreateError"] = "*Hubo un error al hacer el registro, intentelo más tarde.";
            }else if (_employeeService.IsUserExists(oEmployee.Person.PersonId))
            {
                TempData["CreateError"] = "*Este usuario ya existe.";
            }
            else
            {
                _employeeService.CreateEmployee(oEmployee); //Register Process
            }

            return RedirectToAction(nameof(Index));
        }
    }
}