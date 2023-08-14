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

using Microsoft.AspNetCore.Mvc.Rendering;
using PetHotel.Areas.Employees.Models;
using PetHotel.Areas.Employees.Repositories;
using PetHotel.Helpers;
using PetHotel.Models;
using PetHotel.Repositories;
using PetHotel.Services;

namespace PetHotel.Areas.Employees.Services
{
    public class EmployeeService
    {
        private readonly PersonService _personService;
        private readonly RoleRepository _roleRepository;
        private readonly EmployeeRepository _employeeRepository;

        public EmployeeService(PersonService oPersonService, RoleRepository oRoleRepository,
                                EmployeeRepository oEmployeeRepository)
        {
            this._personService = oPersonService;
            this._roleRepository = oRoleRepository;
            this._employeeRepository = oEmployeeRepository;
        }

        public IEnumerable<Employee> GetAllEmployeesForTable() => _employeeRepository.GetAtributtesForTable();

        public IEnumerable<SelectListItem> GetRolSelectListItemsByDescription()
        {
            var rolList = _roleRepository.Get();
            var filteredList = rolList.Where(x => x.RoleId != 4);
            return filteredList.Select(e => new SelectListItem
            {
                Value = e.RoleId.ToString(),
                Text = e.RoleDescription
            });
        }

        public void CreateEmployee(Employee oEmployee)
        {
            oEmployee.Password = PasswordEncryptorHelper.EncryptPassword(oEmployee.Person.PersonId.ToString());
            Person? oPerson = oEmployee.Person;
            oEmployee.PersonId = oPerson.PersonId;
            oEmployee.Person = null;

            if (_personService.IsPersonExists(oPerson.PersonId))
            {
                _employeeRepository.Create(oEmployee);
                _employeeRepository.Save();
            }
            else
            {
                _personService.CreatePerson(oPerson);
                _employeeRepository.Create(oEmployee);
                _employeeRepository.Save();
            }
        }

        public bool IsUserExists(int personID) => _employeeRepository.GetByPersonID(personID) != null;
    }
}