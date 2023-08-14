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

using PetHotel.Areas.Reservations.Repositories;
using PetHotel.Areas.Rooms.Repositories;

namespace PetHotel.Areas.Rooms.Services
{
    public class RoomStatusService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RoomStatusService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var roomRepository = scope.ServiceProvider.GetRequiredService<RoomRepository>();
                    var reservationRepository = scope.ServiceProvider.GetRequiredService<ReservationRepository>();

                    // Your logic inside the DoWork method...
                    // Get bookings that have ended (dateEnd <= DateTime.Now).
                    var reservationsToCheck = reservationRepository.GetByEndDate(DateTime.Now);

                    // Updates the status of rooms associated with completed reservations.
                    foreach (var reservation in reservationsToCheck)
                    {
                        var room = roomRepository.GetByID(reservation.RoomId);
                        if (room != null)
                        {
                            room.RoomStatusId = 1; // 1 is the ID of the "Available" status.
                            roomRepository.Update(room);
                        }
                    }

                    // Saves the changes in the database.
                    roomRepository.Save();
                }

                await Task.Delay(TimeSpan.FromMinutes(2), stoppingToken); // Wait 3 minutes before running again.
            }
        }
    }
}