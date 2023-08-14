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
using PetHotel.Areas.Rooms.Models;
using PetHotel.Models;

namespace PetHotel.Areas.Rooms.Repositories
{
    public class RoomRepository
    {
        private readonly PetHotelContext _context;

        public RoomRepository(DbContextOptions<PetHotelContext> oPetHotelContext)
        {
            this._context = new PetHotelContext(oPetHotelContext);
        }

        public ICollection<Room> Get() => _context.Rooms.ToList();
        public Room GetByID(int roomID) => _context.Rooms.Find(roomID);
        public ICollection<Room> GetAtributtesForTable() => _context.Rooms.Include(r => r.RoomStatus).Include(r => r.RoomType).ToList();
        public ICollection<Room> GetByStatus(int roomStatusID) => _context.Rooms.Where(x => x.RoomStatusId == roomStatusID).ToList();
        public ICollection<Room> GetByStatusAndType(int roomStatusID, int roomTypeID) => _context.Rooms.Where(x => x.RoomStatusId == roomStatusID)
                                                                                                        .Where(x => x.RoomTypeId == roomTypeID).ToList();
        public void Create(Room oRoom) => _context.Rooms.Add(oRoom);
        public void Update(Room oRoom) => _context.Entry(oRoom).State = EntityState.Modified;

        public void Delete(int roomID)
        {
            Room? oRoom = _context.Rooms.Find(roomID);
            _context.Rooms.Remove(oRoom);
        }

        public void Save() => _context.SaveChanges();

        public void UpdateRoomStatus(int roomId, bool maintenanceState)
        {
            var room = _context.Rooms.Find(roomId);
            if (room != null)
            {
                if (maintenanceState)
                {
                    room.RoomStatusId = 3; // Update roomStatusID to 3 when creating a new Maintenance
                }
                else
                {
                    room.RoomStatusId = 1; // Change roomStatusID to 1 when Maintenance is marked as completed
                }
            }
        }

        public void UpdateRoomStatus(int roomId, char reservationState)
        {
            var room = _context.Rooms.Find(roomId);
            if (room != null)
            {
                if (reservationState == 'P') room.RoomStatusId = 2; // Update roomStatusID to 2 when creating the reservation
                else if (reservationState == 'R') room.RoomStatusId = 1; // Update roomStatusID to 1 when reservation is rejected
                else if (reservationState == 'A') room.RoomStatusId = 2; // Update roomStatusID to 2 when reservation is accepted
                else room.RoomStatusId = 1;
            }
        }
    }
}