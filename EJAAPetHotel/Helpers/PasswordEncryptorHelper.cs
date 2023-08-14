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

using System.Security.Cryptography;
using System.Text;

namespace PetHotel.Helpers
{
    public class PasswordEncryptorHelper
    {
        public static string EncryptPassword(string password)
        {
            using SHA256 sha256Hash = SHA256.Create();
            // Validate that the ID has 9 numeric digits
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            // Convert bytes to a hexadecimal string
            StringBuilder builder = new();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            return builder.ToString();
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            using SHA256 sha256Hash = SHA256.Create();
            // Encrypt the entered password
            string encryptedPassword = EncryptPassword(password);

            // Compare the encrypted password with the stored password
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            return comparer.Compare(encryptedPassword, hashedPassword) == 0;
        }
    }
}