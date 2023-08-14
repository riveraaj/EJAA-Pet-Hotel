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

using PetHotel.Areas.Reservations.Models;
using PetHotel.Models;
using PetHotel.Repositories;

namespace PetHotel.Services
{
    public class PetService
    {
        private readonly PetRepository _petRepository;

        public PetService(PetRepository oPetRepository) => this._petRepository = oPetRepository;

        public int RegisterPet(Reservation oReservation, int userID)
        {
            if (!IsPetExist(userID, oReservation.Pet.PetName))
            {
                Pet oPet = new()
                {
                    UserId = userID,
                    PetName = oReservation.Pet.PetName
                };

                _petRepository.Create(oPet);
                _petRepository.Save();
            }

            Pet auxPet = _petRepository.GetPetIdByUserIdAndPetName(userID, oReservation.Pet.PetName);
            return auxPet.PetId;
        }

        private bool IsPetExist(int userID, string petName) => _petRepository.GetPetIdByUserIdAndPetName(userID, petName) != null;
    }
}