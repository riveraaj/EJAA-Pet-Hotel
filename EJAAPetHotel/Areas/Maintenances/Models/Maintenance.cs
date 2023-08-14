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

using PetHotel.Areas.Employees.Models;
using PetHotel.Areas.Rooms.Models;
using System.ComponentModel.DataAnnotations;

namespace PetHotel.Areas.Maintenances.Models;

public partial class Maintenance
{
    public int MaintenanceId { get; set; }

    public int MaintenanceTypeId { get; set; }

    public int EmployeeId { get; set; }

    public int RoomId { get; set; }

    public bool MaintenanceState { get; set; }

    [Required(ErrorMessage = "*Campo requerido")]
    public DateTime DateEnd { get; set; }

    [Required(ErrorMessage = "*Campo requerido")]
    public string MaintenanceDescription { get; set; } = null!;

    public virtual Employee? Employee { get; set; } = null!;

    public virtual MaintenanceType? MaintenanceType { get; set; } = null!;

    public virtual Room? Room { get; set; } = null!;
}