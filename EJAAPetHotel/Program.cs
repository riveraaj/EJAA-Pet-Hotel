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
using Microsoft.EntityFrameworkCore;
using PetHotel.Areas.Accounts.Repositories;
using PetHotel.Areas.Accounts.Services;
using PetHotel.Areas.Employees.Repositories;
using PetHotel.Areas.Employees.Services;
using PetHotel.Areas.Maintenances.Repositories;
using PetHotel.Areas.Maintenances.Services;
using PetHotel.Areas.Reservations.Repositories;
using PetHotel.Areas.Reservations.Services;
using PetHotel.Areas.Rooms.Repositories;
using PetHotel.Areas.Rooms.Services;
using PetHotel.Models;
using PetHotel.Repositories;
using PetHotel.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//Add cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Accounts/Admin/Login";
        options.ExpireTimeSpan = TimeSpan.FromDays(15);
        options.AccessDeniedPath = "/Accounts/Admin/Login";
    });

// Add services to the container.
builder.Services.AddControllersWithViews();

//System.Text.Json to avoid circular references in serialization
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

//DB Connection
builder.Services.AddDbContext<PetHotelContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("connection")));

//Email Service
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection(nameof(MailSettings)));
builder.Services.AddTransient<IMailService, MailService>();

//Routine
builder.Services.AddHostedService<RoomStatusService>();

//Services Scoped
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<MaintenanceService>();
builder.Services.AddScoped<PersonService>();
builder.Services.AddScoped<PetService>();
builder.Services.AddScoped<ReservationService>();
builder.Services.AddScoped<RoomService>();
builder.Services.AddScoped<AccountService>();

//Repositories Scoped
builder.Services.AddScoped<EmployeeRepository>();
builder.Services.AddScoped<MaintenanceRepository>();
builder.Services.AddScoped<MaintenanceTypeRepository>();
builder.Services.AddScoped<PackageTypeRepository>();
builder.Services.AddScoped<IRepositoryDB<Person>, PersonRepository>();
builder.Services.AddScoped<PetRepository>();
builder.Services.AddScoped<ReservationRepository>();
builder.Services.AddScoped<RoleRepository>();
builder.Services.AddScoped<RoomRepository>();
builder.Services.AddScoped<RoomStatusRepository>();
builder.Services.AddScoped<RoomTypeRepository>();
builder.Services.AddScoped<StayTypeRepository>();
builder.Services.AddScoped<UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error/Http");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithRedirects("/Error/Http?statusCode={0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapAreaControllerRoute(
    name: "MyAreaAccounts",
    areaName: "Accounts",
    pattern: "Accounts/{controller=Admin}/{action=Login}/{id?}");

app.MapAreaControllerRoute(
    name: "MyAreaEmployees",
    areaName: "Employees",
    pattern: "Employees/{controller=Employee}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "MyAreaMaintenances",
    areaName: "Maintenances",
    pattern: "Maintenances/{controller=Maintenance}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "MyAreaRooms",
    areaName: "Rooms",
    pattern: "Rooms/{controller=Room}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "MyAreaReservations",
    areaName: "Reservations",
    pattern: "Reservations/{controller=Reservation}/{action=Home}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=PetHotel}/{action=Index}/{id?}");

app.Run();