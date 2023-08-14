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
using PetHotel.Models;

namespace PetHotel.Repositories
{
    public class PetRepository
    {
        private readonly PetHotelContext _context;

        public PetRepository(DbContextOptions<PetHotelContext> oPetHotelContext)
        {
            this._context = new PetHotelContext(oPetHotelContext);
        }

        public ICollection<Pet> Get() => _context.Pets.ToList();
        public Pet GetByID(int petID) => _context.Pets.Find(petID);
        public Pet GetByIDForPerson(int petID) => _context.Pets.Include(x => x.User).Include(x => x.User.Person).Where(x => x.PetId == petID).SingleOrDefault();
        public Pet GetPetIdByUserIdAndPetName(int userId, string petName) => _context.Pets.Where(x => x.UserId == userId).Where(x => x.PetName == petName).SingleOrDefault();
        public ICollection<Pet> GetByUserID(int userID) => _context.Pets.Where(x => x.UserId == userID).ToList();
        public void Create(Pet oPet) => _context.Pets.Add(oPet);
        public void Save() => _context.SaveChanges();
    }
}