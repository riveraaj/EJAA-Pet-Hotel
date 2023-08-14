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

using Microsoft.EntityFrameworkCore;
using PetHotel.Areas.Employees.Models;
using PetHotel.Models;

namespace PetHotel.Areas.Employees.Repositories
{
    public class EmployeeRepository
    {
        private readonly PetHotelContext _context;

        public EmployeeRepository(DbContextOptions<PetHotelContext> oPetHotelContext)
        {
            this._context = new PetHotelContext(oPetHotelContext);
        }

        public ICollection<Employee> Get() => _context.Employees.ToList();
        public Employee GetByID(int employeeID) => _context.Employees.Find(employeeID);
        public Employee GetByPersonID(int personID) => _context.Employees.Include(x => x.Person).FirstOrDefault(x => x.PersonId == personID);
        public ICollection<Employee> GetByRole(int roleID) => _context.Employees.Include(x => x.Person).Where(x => x.RoleId == roleID).ToList();
        public ICollection<Employee> GetAtributtesForTable() => _context.Employees.Include(x => x.Person).Include(x => x.Role).ToList();
        public void Create(Employee oEmployee) => _context.Employees.Add(oEmployee);
        public void Update(Employee oEmployee) => _context.Entry(oEmployee).State = EntityState.Modified;

        public void Delete(int employeeID)
        {
            Employee oEmployee = _context.Employees.Find(employeeID);
            _context.Employees.Remove(oEmployee);
        }
        public void Save() => _context.SaveChanges();
    }
}