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
using PetHotel.Areas.Employees.Repositories;
using PetHotel.Areas.Maintenances.Models;
using PetHotel.Areas.Maintenances.Repositories;
using PetHotel.Areas.Rooms.Repositories;

namespace PetHotel.Areas.Maintenances.Services
{
    public class MaintenanceService
    {
        private readonly RoomRepository _roomRepository;
        private readonly EmployeeRepository _employeeRepository;
        private readonly MaintenanceRepository _maintenanceRepository;
        private readonly MaintenanceTypeRepository _maintenanceTypeRepository;

        public MaintenanceService(RoomRepository oRoomRepository, EmployeeRepository oEmployeeRepository,
                                    MaintenanceRepository oMaintenanceRepository, MaintenanceTypeRepository oMaintenanceTypeRepository)
        {
            _maintenanceTypeRepository = oMaintenanceTypeRepository;
            _maintenanceRepository = oMaintenanceRepository;
            _employeeRepository = oEmployeeRepository;
            _roomRepository = oRoomRepository;
        }

        public IEnumerable<Maintenance> GetAllMaintenancesForTable() => _maintenanceRepository.GetAtributtesForTable();

        public Maintenance GetMaintenanceById(int maintenanceID) => _maintenanceRepository.GetByID(maintenanceID);

        public void CreateMaintenance(Maintenance oMaintenance)
        {
            oMaintenance.MaintenanceState = true;
            _roomRepository.UpdateRoomStatus(oMaintenance.RoomId, oMaintenance.MaintenanceState);
            _roomRepository.Save();
            _maintenanceRepository.Create(oMaintenance);
            _maintenanceRepository.Save();
        }

        public void UpdateMaintenance(Maintenance oMaintenance)
        {
            _roomRepository.UpdateRoomStatus(oMaintenance.RoomId, oMaintenance.MaintenanceState);
            _roomRepository.Save();
            _maintenanceRepository.Update(oMaintenance);
            _maintenanceRepository.Save();
        }

        public void DeleteMaintenance(int maintenanceId)
        {
            var maintenance = _maintenanceRepository.GetByID(maintenanceId);
            if (maintenance != null)
            {
                maintenance.MaintenanceState = false;
                _roomRepository.UpdateRoomStatus(maintenance.RoomId, maintenance.MaintenanceState);
                _maintenanceRepository.Delete(maintenanceId);
            }
            _roomRepository.Save();
            _maintenanceRepository.Save();

        }

        public IEnumerable<SelectListItem> GetEmployeeSelectListItemsByRole(int roleID)
        {
            var employeeList = _employeeRepository.GetByRole(roleID);
            return employeeList.Select(e => new SelectListItem
            {
                Value = e.EmployeeId.ToString(),
                Text = $"{e.Person?.PersonName} {e.Person?.PersonLastname}"
            });
        }

        public IEnumerable<SelectListItem> GetMaintenanceTypeSelectListItems()
        {
            var maintenanceTypeList = _maintenanceTypeRepository.Get();
            return maintenanceTypeList.Select(e => new SelectListItem
            {
                Value = e.MaintenanceTypeId.ToString(),
                Text = e.MaintenanceTypeDescription
            });
        }

        public IEnumerable<SelectListItem> GetRoomSelectListItemsByStatus(int status)
        {
            return _roomRepository.GetByStatus(status).Select(room => new SelectListItem
            {
                Value = room.RoomId.ToString(),
                Text = room.RoomId.ToString()
            });
        }
    }
}