﻿/*
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
using PetHotel.Areas.Reservations.Models;
using PetHotel.Models;

namespace PetHotel.Areas.Reservations.Repositories
{
    public class PackageTypeRepository
    {
        private readonly PetHotelContext _context;

        public PackageTypeRepository(DbContextOptions<PetHotelContext> oPetHotelContext)
        {
            this._context = new PetHotelContext(oPetHotelContext);
        }

        public ICollection<PackageType> Select() => _context.PackageTypes.ToList();
        public PackageType GetByID(int packageTypeID) => _context.PackageTypes.Find(packageTypeID);
        public int GetPriceById(int packageTypeID) => _context.PackageTypes.Where(x => x.PackageTypeId == packageTypeID).Select(x => x.Price).SingleOrDefault();
    }
}