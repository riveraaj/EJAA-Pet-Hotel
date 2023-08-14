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
using PetHotel.Areas.Reservations.Models;
using PetHotel.Models;

namespace PetHotel.Areas.Reservations.Repositories
{
    public class ReservationRepository
    {
        private readonly PetHotelContext _context;

        public ReservationRepository(DbContextOptions<PetHotelContext> oPetHotelContext)
        {
            this._context = new PetHotelContext(oPetHotelContext);
        }

        public ICollection<Reservation> Get() => _context.Reservations.ToList();
        public Reservation GetByID(int reservationID) => _context.Reservations.Find(reservationID);
        public ICollection<Reservation> GetAtributtesByStatusForTable(char reservationState) => _context.Reservations.Include(r => r.Employee)
                                                                                                                        .Include(r => r.PackageType)
                                                                                                                        .Include(r => r.Pet)
                                                                                                                        .Include(r => r.Room)
                                                                                                                        .Include(r => r.StayType)
                                                                                                                        .Include(x => x.Employee.Person)
                                                                                                                        .Where(x => x.ReservationState == reservationState).ToList();
        public Reservation GetAtributtesById(int? reservationID) => _context.Reservations.Include(x => x.Employee)
                                                                                         .Include(x => x.PackageType)
                                                                                         .Include(x => x.Pet)
                                                                                         .Include(x => x.Room)
                                                                                         .Include(x => x.StayType)
                                                                                         .Include(x => x.Employee.Person)
                                                                                         .Include(x => x.Pet.User.Person)
                                                                                         .SingleOrDefault(x => x.ReservationId == reservationID);
        public IEnumerable<Reservation> GetByEndDate(DateTime oDateTime) => _context.Reservations.Where(x => x.DateEnd <= oDateTime).Where(x => x.ReservationState == 'A').ToList();
        public Reservation GetByIDForEmail(int reservationID) => _context.Reservations.Include(x => x.Pet.User.Person)
                                                                                        .Include(x => x.PackageType)
                                                                                        .SingleOrDefault(x => x.ReservationId == reservationID);

        public ICollection<Reservation> GetByUserId(int userID) => _context.Reservations.Include(x => x.Employee)
                                                                                                    .Include(x => x.PackageType)
                                                                                                    .Include(x => x.Pet)
                                                                                                    .Include(x => x.Room)
                                                                                                    .Include(x => x.StayType)
                                                                                                    .Include(x => x.Employee.Person)
                                                                                                    .Include(x => x.Pet.User.Person)
                                                                                                    .Where(x => x.Pet.UserId == userID).ToList();
        public void Insert(Reservation oReservation) => _context.Reservations.Add(oReservation);
        public void Update(Reservation oReservation) => _context.Reservations.Entry(oReservation).State = EntityState.Modified;

        public void Delete(int reservationID)
        {
            Reservation oReservation = _context.Reservations.Find(reservationID);
            _context.Reservations.Remove(oReservation);
        }

        public void Save() => _context.SaveChanges();
    }
}