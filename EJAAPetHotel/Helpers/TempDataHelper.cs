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

using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PetHotel.Helpers
{
    public static class TempDataHelper
    {
        public static void Set<T>(this ITempDataDictionary tempData, string key, T value)
        {
            var serializedValue = JsonSerializer.Serialize(value, new JsonSerializerOptions
            {
                // Evitar referencias circulares en la serialización
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            });

            tempData[key] = serializedValue;
        }

        public static T? Get<T>(this ITempDataDictionary tempData, string key)
        {
            return tempData[key] is not string value ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}