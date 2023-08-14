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
using PetHotel.Models;

namespace PetHotel.Areas.Reservations.Models;

public partial class Reservation
{
    public int ReservationId { get; set; }

    public int PetId { get; set; }

    public int EmployeeId { get; set; }

    public int RoomId { get; set; }

    public DateTime DateStart { get; set; }

    public DateTime DateEnd { get; set; }

    public bool DateUndefined { get; set; }

    public int StayTypeId { get; set; }

    public int PackageTypeId { get; set; }

    public string DescriptionPetCare { get; set; } = null!;

    public int FinalPrice { get; set; }

    public char ReservationState { get; set; }

    public virtual Employee? Employee { get; set; } = null!;

    public virtual PackageType? PackageType { get; set; } = null!;

    public virtual Pet? Pet { get; set; } = null!;

    public virtual Room? Room { get; set; } = null!;

    public virtual StayType? StayType { get; set; } = null!;
}