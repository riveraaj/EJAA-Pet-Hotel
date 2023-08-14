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
using System.Globalization;

namespace PetHotel.Helpers
{
    public class EmailHelper
    {
        public static string GenerateConfirmationEmailBody(Reservation oReservation)
        {
            return $@"
                            <html>
                            <body style='font-family: Arial, sans-serif; color: #333333;'>

                            <p>Estimado {oReservation.Pet.User.Person.PersonName + " " + oReservation.Pet.User.Person.PersonLastname},</p>

                            <p>¡Gracias por elegir Cuidados Los Patitos SA para su estadía! Estamos emocionados de darle la bienvenida y asegurarnos de que su mascota tenga una experiencia inolvidable.</p>

                            <p>Detalles de la Reserva:</p>
                            <ul>
                                <li>- Nombre de la Mascota: {oReservation.Pet.PetName}</li>
                                <li>- Fecha de Llegada: {oReservation.DateStart:dd/MM/yyyy}</li>
                                <li>- Fecha de Salida: {oReservation.DateEnd:dd/MM/yyyy}</li>
                                <li>- Tipo de Habitación: {oReservation.PackageType.PackageTypeDescription}</li>
                                <li>- Número de Habitación: {oReservation.RoomId}</li>
                            </ul>

                            <p>Confirmación de Pago:</p>
                            <ul>
                                <li>- Precio Total: {oReservation.FinalPrice.ToString("C2", new CultureInfo("es-CR"))}</li>
                                *Recuerde, el pago se realiza el mismo dia  de la llegada al hotel
                            </ul>

                            <p>Si tiene alguna pregunta o necesita asistencia adicional, no dude en ponerse en contacto con nuestro equipo de atención al cliente al 22291743 o por correo electrónico a pethotelejaa@gmail.com.</p>

                            <p>Esperamos darle la bienvenida pronto y hacer de su estadía una experiencia inolvidable. Siempre nos esforzamos por brindar el mejor servicio y comodidades para nuestros huéspedes.</p>

                            <p>¡Gracias por elegir Cuidados Los Patitos SA!</p>

                            <p>Atentamente,</p>
                            <p>El Equipo de Cuidados Los Patitos SA</p>
                            </body>
                            </html>
                            ";
        }

        public static string GenerateRejectionEmailBody(Reservation oReservation)
        {
            return $@"
                            <html>
                            <body style='font-family: Arial, sans-serif; color: #333333;'>

                            <p>Estimado {oReservation.Pet.User.Person.PersonName + " " + oReservation.Pet.User.Person.PersonLastname},</p>

                            <p>Esperamos que se encuentre bien. Le escribimos para informarle sobre el estado de su reservación en nuestro hotel, EJAA Pet Hotel</p>

                            <p>Lamentablemente, después de revisar detalladamente nuestros registros y disponibilidad, nos vemos en la necesidad de comunicarle que su reservación, programada para el {oReservation.DateStart:dd/MM/yyyy} al {oReservation.DateEnd:dd/MM/yyyy}, ha sido rechazada. Entendemos lo importante que es para usted su estancia en nuestro hotel y lamentamos sinceramente cualquier inconveniente que esto pueda causarle.</p>
                            

                            <p>Los motivos específicos del rechazo de su reservación pueden variar, y le pedimos disculpas si esto se debe a cualquier error o confusión que haya surgido durante el proceso de reserva. Nuestro equipo de atención al cliente estará encantado de brindarle más información sobre la situación y de ayudarle a explorar opciones alternativas para su estadía en caso de que aún esté interesado en alojarse con nosotros.</p>


                            <p>Si tiene alguna pregunta o necesita asistencia adicional, no dude en ponerse en contacto con nuestro equipo de atención al cliente al 22291743 o por correo electrónico a pethotelejaa@gmail.com.</p>

                            <p>Agradecemos su interés en nuestro hotel y lamentamos sinceramente cualquier decepción que este rechazo pueda haber causado. Esperamos tener la oportunidad de darle la bienvenida en el futuro y de brindarle una experiencia excepcional en su próxima visita.</p>

                            <p>Gracias por su comprensión.</p>

                            <p>Atentamente,</p>
                            <p>El Equipo de Cuidados Los Patitos SA</p>
                            </body>
                            </html>
                            ";
        }
    }
}