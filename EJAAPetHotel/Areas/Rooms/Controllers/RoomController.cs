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

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHotel.Areas.Rooms.Services;

namespace PetHotel.Areas.Rooms.Controllers
{
    [Area("Rooms")]
    [Authorize(Roles = "1, 2, 3")]
    public class RoomController : Controller
    {
        private readonly RoomService _roomService;

        public RoomController(RoomService oRoomService)
        {
            this._roomService = oRoomService;
        }

        public IActionResult Index() => View(_roomService.GetAllRoomsForTable());
    }
}