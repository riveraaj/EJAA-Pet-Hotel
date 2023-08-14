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
    public class PersonRepository : IRepositoryDB<Person>
    {
        private readonly PetHotelContext _context;

        public PersonRepository(DbContextOptions<PetHotelContext> oPetHotelContext)
        {
            this._context = new PetHotelContext(oPetHotelContext);
        }

        public ICollection<Person> Get() => _context.People.ToList();
        public Person GetByID(int personID) => _context.People.Find(personID);
        public void Create(Person oPerson) => _context.People.Add(oPerson);
        public void Update(Person oPerson) => _context.Entry(oPerson).State = EntityState.Modified;

        public void Delete(int personID)
        {
            Person oPerson = _context.People.Find(personID);
            _context.People.Remove(oPerson);
        }

        public void Save() => _context.SaveChanges();
    }
}
