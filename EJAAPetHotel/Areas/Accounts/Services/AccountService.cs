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

using PetHotel.Areas.Accounts.Models;
using PetHotel.Areas.Accounts.Repositories;
using PetHotel.Areas.Employees.Models;
using PetHotel.Areas.Employees.Repositories;
using PetHotel.Helpers;
using PetHotel.Models;
using PetHotel.Repositories;
using PetHotel.Services;

namespace PetHotel.Areas.Accounts.Services
{
    public class AccountService
    {
        private readonly PersonService _personService;
        private readonly UserRepository _userRepository;
        private readonly EmployeeRepository _employeeRepository;
        private readonly IRepositoryDB<Person> _personRepository;

        public AccountService(PersonService oPersonService, UserRepository oUserRepository, IRepositoryDB<Person> oPersonRepository, EmployeeRepository oEmployeeRepository)
        {
            this._personService = oPersonService;
            this._userRepository = oUserRepository;      
            this._personRepository = oPersonRepository;
            this._employeeRepository = oEmployeeRepository;
        }

        public void RegisterUser(User oUser)
        {
            oUser.Password = PasswordEncryptorHelper.EncryptPassword(oUser.Password);
            Person oPerson = oUser.Person;
            oUser.PersonId = oUser.Person.PersonId;
            oUser.Person = null;
            oUser.RoleId = 4;

            if (_personService.IsPersonExists(oPerson.PersonId))
            {
                _userRepository.Create(oUser);
                _userRepository.Save();
            }
            else
            {
                _personService.CreatePerson(oPerson);
                _userRepository.Create(oUser);
                _userRepository.Save();
            }
        }

        public async Task<bool> LoginUser(User oUser, HttpContext oHttpContext)
        {
            User auxUser = _userRepository.GetByPersonID(oUser.PersonId);
            if (auxUser != null)
            {
                if (!PasswordEncryptorHelper.VerifyPassword(oUser.Password, auxUser.Password) || _personRepository.GetByID(oUser.PersonId) == null) return false;
                else
                {
                    await CookiesHelper.CreateAuthenticationCookie(oHttpContext, auxUser.RoleId, auxUser.PersonId, auxUser.UserId, auxUser.Person.Email);
                    return true;
                }
            }
            else return false;
        }

        public async Task<bool> LoginEmployee(Employee oEmployee, HttpContext oHttpContext)
        {
            Employee auxEmployee = _employeeRepository.GetByPersonID(oEmployee.PersonId);
            if (auxEmployee != null)
            {
                if (!PasswordEncryptorHelper.VerifyPassword(oEmployee.Password, auxEmployee.Password) || _personRepository.GetByID(oEmployee.PersonId) == null) return false;
                else
                {
                    await CookiesHelper.CreateAuthenticationCookie(oHttpContext, auxEmployee.RoleId, auxEmployee.PersonId, auxEmployee.EmployeeId, auxEmployee.Person.Email);
                    return true;
                }
            }
            else return false; 
        }

        public bool IsUserExists(int personId) => _userRepository.GetByPersonID(personId) != null;
    }
}