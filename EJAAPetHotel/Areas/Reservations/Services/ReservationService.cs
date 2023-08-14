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

using Microsoft.AspNetCore.Mvc.Rendering;
using PetHotel.Areas.Employees.Models;
using PetHotel.Areas.Employees.Repositories;
using PetHotel.Areas.Reservations.Models;
using PetHotel.Areas.Reservations.Repositories;
using PetHotel.Areas.Rooms.Models;
using PetHotel.Areas.Rooms.Repositories;
using PetHotel.Helpers;
using PetHotel.Services;

namespace PetHotel.Areas.Reservations.Services
{
    public class ReservationService
    {
        private readonly IMailService _mail;
        private readonly PetService _petService;
        private readonly RoomRepository _roomRepository;
        private readonly StayTypeRepository _stayTypeRepository;
        private readonly EmployeeRepository _employeeRepository;
        private readonly ReservationRepository _reservationRepository;
        private readonly PackageTypeRepository _packageTypeRepository;

        public ReservationService(IMailService oMailService, PetService oPetService, RoomRepository oRoomRepository,
            StayTypeRepository oStayTypeRepository, EmployeeRepository oEmployeeRepository, ReservationRepository oReservationRepository,
            PackageTypeRepository oPackageTypeRepository)
        {
            this._mail = oMailService;
            this._petService = oPetService;
            this._roomRepository = oRoomRepository;
            this._stayTypeRepository = oStayTypeRepository;
            this._employeeRepository = oEmployeeRepository;
            this._reservationRepository = oReservationRepository;
            this._packageTypeRepository = oPackageTypeRepository;
        }

        public Reservation GetReservation(int reservationId) => _reservationRepository.GetByID(reservationId);
        public List<Reservation> GetReservationByUserForTable(int userId) => (List<Reservation>)_reservationRepository.GetByUserId(userId);

        public List<Reservation> GetReservationByStatusForTable(char reservationState) => (List<Reservation>)_reservationRepository.GetAtributtesByStatusForTable(reservationState);

        public Reservation GetAllReservationForDetails(int? reservationID) => _reservationRepository.GetAtributtesById(reservationID);

        public List<Room> GetRooms(int status, int type) => (List<Room>)_roomRepository.GetByStatusAndType(status, type);

        public List<Room> GetAllRooms(int status) => (List<Room>)_roomRepository.GetByStatus(status);

        public IEnumerable<SelectListItem> GetStayTypeSelectListItems()
        {
            return _stayTypeRepository.Get().Select(stayType => new SelectListItem
            {
                Value = stayType.StayTypeId.ToString(),
                Text = stayType.StayTypeDescription
            });
        }

        public void RegisterReservation(Reservation oReservation, int userId)
        {
            int daysOfStay;
            int petId = _petService.RegisterPet(oReservation, userId);

            List<Employee> employeeList = (List<Employee>)_employeeRepository.GetByRole(2);
            List<Room> roomList;

            if (oReservation.PackageTypeId == 1) roomList = GetRooms(1, 1);
            else if (oReservation.PackageTypeId == 2) roomList = GetRooms(1, 2);
            else roomList = GetRooms(1, 3);

            oReservation.PetId = petId;
            oReservation.Pet = null;
            oReservation.EmployeeId = employeeList[GetRandomNumber(employeeList.Count)].EmployeeId;
            oReservation.RoomId = roomList[GetRandomNumber(roomList.Count)].RoomId;
            oReservation.ReservationState = 'P';

            if (oReservation.DateUndefined)
            {
                daysOfStay = 3;
                oReservation.DateEnd = oReservation.DateStart.AddDays(daysOfStay);
            }
            else daysOfStay = (oReservation.DateEnd - oReservation.DateStart).Days;

            int packagePrice = _packageTypeRepository.GetPriceById(oReservation.PackageTypeId) * daysOfStay;

            if (oReservation.StayTypeId == 1 || oReservation.StayTypeId == 2 || oReservation.StayTypeId == 3) oReservation.FinalPrice = packagePrice;
            else oReservation.FinalPrice = packagePrice + 10000;

            _reservationRepository.Insert(oReservation);
            _reservationRepository.Save();
            _roomRepository.UpdateRoomStatus(oReservation.RoomId, oReservation.ReservationState);
            _roomRepository.Save();
        }

        private static int GetRandomNumber(int maxNumber) => new Random().Next(0, maxNumber - 1);

        public void UpdateReservationState(Reservation oReservation, string email)
        {
            if (oReservation.ReservationState == 'A')
            {
                oReservation.ReservationState = 'A';
                _reservationRepository.Update(oReservation);
                _reservationRepository.Save();
                _roomRepository.UpdateRoomStatus(oReservation.RoomId, oReservation.ReservationState);
                _roomRepository.Save();
                SendEmailAsync('A', email, oReservation.ReservationId);
            }
            else if (oReservation.ReservationState == 'R')
            {
                oReservation.ReservationState = 'R';
                _reservationRepository.Update(oReservation);
                _reservationRepository.Save();
                _roomRepository.UpdateRoomStatus(oReservation.RoomId, oReservation.ReservationState);
                _roomRepository.Save();
                SendEmailAsync('R', email, oReservation.ReservationId);
            }
        }

        private async Task SendEmailAsync(char reservationState, string email, int reservationId)
        {
            Reservation oReservation = _reservationRepository.GetByIDForEmail(reservationId);

            string body;

            if (reservationState.Equals('A')) body = EmailHelper.GenerateConfirmationEmailBody(oReservation);
            else body = EmailHelper.GenerateRejectionEmailBody(oReservation);

            if (reservationState == 'A')
            {
                try
                {
                    await _mail.SendEmailAsync(email, "Confirmación de Reserva en Cuidados Los Patitos SA", body);
                }
                catch { }
            }
            else if (reservationState == 'R')
            {
                try
                {
                    await _mail.SendEmailAsync(email, " Información sobre su reservación en Cuidados Los Patitos SA", body);
                    _reservationRepository.Delete(oReservation.ReservationId);
                    _roomRepository.Save();
                }
                catch
                {
                    Console.WriteLine("An error occured. The Mail could not be sent.");
                }
            }
        }
    }
}