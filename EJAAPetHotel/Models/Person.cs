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
using PetHotel.Areas.Employees.Models;
using System.ComponentModel.DataAnnotations;

namespace PetHotel.Models;

public partial class Person
{
    [Required(ErrorMessage = "*Campo requerido")]
    public int PersonId { get; set; }

    [Required(ErrorMessage = "*Campo requerido")]
    public string PersonName { get; set; } = null!;

    [Required(ErrorMessage = "*Campo requerido")]
    public string PersonLastname { get; set; } = null!;

    [Required(ErrorMessage = "*Campo requerido")]
    public string PersonSecondname { get; set; } = null!;

    [Required(ErrorMessage = "*Campo requerido")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "*Campo requerido")]
    public int Phone { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
