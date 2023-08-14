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

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace PetHotel.Helpers
{
    public class CookiesHelper
    {
        public static async Task CreateAuthenticationCookie(HttpContext oHttpContext, int role, int personId, int id, string email)
        {
            var claims = new List<Claim> {
                            new Claim(ClaimTypes.Role, role.ToString()),
                            new Claim("PersonId", personId.ToString()),
                            new Claim("Email", email),
                            new Claim("UserId", id.ToString())};

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await oHttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }

        public static async Task RemoveAuthenticationCookie(HttpContext httpContext) => await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}
