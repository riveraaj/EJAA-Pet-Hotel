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

using PetHotel.Areas.Maintenances.Models;
using PetHotel.Areas.Reservations.Models;
using PetHotel.Models;
using System.ComponentModel.DataAnnotations;

namespace PetHotel.Areas.Employees.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    [Required(ErrorMessage = "*Campo requerido")]
    public int PersonId { get; set; }

    [Required(ErrorMessage = "*Campo requerido")]
    public int RoleId { get; set; }

    [Required(ErrorMessage = "*Campo requerido")]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "*Campo requerido")]
    public DateTime DateEntry { get; set; }

    public virtual ICollection<Maintenance> Maintenances { get; set; } = new List<Maintenance>();

    public virtual Person? Person { get; set; } = null!;

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual Role? Role { get; set; } = null!;
}