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
using PetHotel.Areas.Maintenances.Models;
using PetHotel.Models;

namespace PetHotel.Areas.Maintenances.Repositories
{
    public class MaintenanceRepository
    {
        private readonly PetHotelContext _context;

        public MaintenanceRepository(DbContextOptions<PetHotelContext> oPetHotelContext)
        {
            this._context = new PetHotelContext(oPetHotelContext);
        }

        public ICollection<Maintenance> Get() => _context.Maintenances.ToList();
        public Maintenance GetByID(int maintenanceID) => _context.Maintenances.Find(maintenanceID);
        public ICollection<Maintenance> GetAtributtesForTable() => _context.Maintenances.Include(m => m.Employee.Person)
                                                                                        .Include(m => m.MaintenanceType)
                                                                                        .Include(m => m.Room).ToList();
        public void Create(Maintenance oMaintenance) => _context.Maintenances.Add(oMaintenance);
        public void Update(Maintenance oMaintenance) => _context.Entry(oMaintenance).State = EntityState.Modified;

        public void Delete(int maintenanceID)
        {
            Maintenance oMaintenance = _context.Maintenances.Find(maintenanceID);
            _context.Maintenances.Remove(oMaintenance);
        }

        public void Save() => _context.SaveChanges();
    }
}