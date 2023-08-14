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
using PetHotel.Areas.Accounts.Models;
using PetHotel.Models;

namespace PetHotel.Areas.Accounts.Repositories
{
    public class UserRepository
    {
        private readonly PetHotelContext _context;

        public UserRepository(DbContextOptions<PetHotelContext> oPetHotelContext)
        {
            this._context = new PetHotelContext(oPetHotelContext);
        }

        public ICollection<User> Get() => _context.Users.ToList();
        public User GetByID(int userID) => _context.Users.Find(userID);
        public User GetByPersonID(int personId) => _context.Users.Include(x => x.Person).FirstOrDefault(x => x.PersonId == personId);
        public void Create(User oUser) => _context.Users.Add(oUser);
        public void Update(User oUser) => _context.Entry(oUser).State = EntityState.Modified;

        public void Delete(int userID)
        {
            User oUser = _context.Users.Find(userID);
            _context.Users.Remove(oUser);
        }

        public void Save() => _context.SaveChanges();
    }
}