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
using PetHotel.Areas.Accounts.Models;
using PetHotel.Areas.Employees.Models;
using PetHotel.Areas.Maintenances.Models;
using PetHotel.Areas.Reservations.Models;
using PetHotel.Areas.Rooms.Models;

namespace PetHotel.Models;

public partial class PetHotelContext : DbContext
{
    public PetHotelContext(DbContextOptions<PetHotelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Maintenance> Maintenances { get; set; }

    public virtual DbSet<MaintenanceType> MaintenanceTypes { get; set; }

    public virtual DbSet<PackageType> PackageTypes { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Pet> Pets { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<RoomStatus> RoomStatuses { get; set; }

    public virtual DbSet<RoomType> RoomTypes { get; set; }

    public virtual DbSet<StayType> StayTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__C134C9A14CBC01F3");

            entity.ToTable("Employee");

            entity.Property(e => e.EmployeeId).HasColumnName("employeeID");
            entity.Property(e => e.DateEntry)
                .HasColumnType("date")
                .HasColumnName("dateEntry");
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.PersonId).HasColumnName("personID");
            entity.Property(e => e.RoleId).HasColumnName("roleID");

            entity.HasOne(d => d.Person).WithMany(p => p.Employees)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employee__person__3B75D760");

            entity.HasOne(d => d.Role).WithMany(p => p.Employees)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employee__roleID__3C69FB99");
        });

        modelBuilder.Entity<Maintenance>(entity =>
        {
            entity.HasKey(e => e.MaintenanceId).HasName("PK__Maintena__75249AEB0DA3ADE3");

            entity.ToTable("Maintenance");

            entity.Property(e => e.MaintenanceId).HasColumnName("maintenanceID");
            entity.Property(e => e.DateEnd)
                .HasColumnType("date")
                .HasColumnName("dateEnd");
            entity.Property(e => e.EmployeeId).HasColumnName("employeeID");
            entity.Property(e => e.MaintenanceDescription)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("maintenanceDescription");
            entity.Property(e => e.MaintenanceState).HasColumnName("maintenanceState");
            entity.Property(e => e.MaintenanceTypeId).HasColumnName("maintenanceTypeID");
            entity.Property(e => e.RoomId).HasColumnName("roomID");

            entity.HasOne(d => d.Employee).WithMany(p => p.Maintenances)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Maintenan__emplo__5070F446");

            entity.HasOne(d => d.MaintenanceType).WithMany(p => p.Maintenances)
                .HasForeignKey(d => d.MaintenanceTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Maintenan__maint__4F7CD00D");

            entity.HasOne(d => d.Room).WithMany(p => p.Maintenances)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Maintenan__roomI__5165187F");
        });

        modelBuilder.Entity<MaintenanceType>(entity =>
        {
            entity.HasKey(e => e.MaintenanceTypeId).HasName("PK__Maintena__B89CEE2CB1C95C18");

            entity.ToTable("Maintenance_Type");

            entity.Property(e => e.MaintenanceTypeId).HasColumnName("maintenanceTypeID");
            entity.Property(e => e.MaintenanceTypeDescription)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("maintenanceTypeDescription");
        });

        modelBuilder.Entity<PackageType>(entity =>
        {
            entity.HasKey(e => e.PackageTypeId).HasName("PK__Package___3B08091FE1F07F6F");

            entity.ToTable("Package_Type");

            entity.Property(e => e.PackageTypeId).HasColumnName("packageTypeID");
            entity.Property(e => e.PackageTypeDescription)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("packageTypeDescription");
            entity.Property(e => e.Price).HasColumnName("price");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK__Person__EC7D7D6DE1759228");

            entity.ToTable("Person");

            entity.Property(e => e.PersonId)
                .ValueGeneratedNever()
                .HasColumnName("personID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.PersonLastname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("personLastname");
            entity.Property(e => e.PersonName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("personName");
            entity.Property(e => e.PersonSecondname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("personSecondname");
            entity.Property(e => e.Phone).HasColumnName("phone");
        });

        modelBuilder.Entity<Pet>(entity =>
        {
            entity.HasKey(e => e.PetId).HasName("PK__Pet__DDF85059EED5EF44");

            entity.ToTable("Pet");

            entity.Property(e => e.PetId).HasColumnName("petID");
            entity.Property(e => e.PetName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("petName");
            entity.Property(e => e.UserId).HasColumnName("userID");

            entity.HasOne(d => d.User).WithMany(p => p.Pets)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pet__userID__4316F928");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.ReservationId).HasName("PK__Reservat__B14BF5A586FB3FE7");

            entity.ToTable("Reservation");

            entity.Property(e => e.ReservationId).HasColumnName("reservationID");
            entity.Property(e => e.DateEnd)
                .HasColumnType("date")
                .HasColumnName("dateEnd");
            entity.Property(e => e.DateStart)
                .HasColumnType("date")
                .HasColumnName("dateStart");
            entity.Property(e => e.DateUndefined).HasColumnName("dateUndefined");
            entity.Property(e => e.DescriptionPetCare)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("descriptionPetCare");
            entity.Property(e => e.EmployeeId).HasColumnName("employeeID");
            entity.Property(e => e.FinalPrice).HasColumnName("finalPrice");
            entity.Property(e => e.PackageTypeId).HasColumnName("packageTypeID");
            entity.Property(e => e.PetId).HasColumnName("petID");
            entity.Property(e => e.ReservationState)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("reservationState");
            entity.Property(e => e.RoomId).HasColumnName("roomID");
            entity.Property(e => e.StayTypeId).HasColumnName("stayTypeID");

            entity.HasOne(d => d.Employee).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservati__emplo__59FA5E80");

            entity.HasOne(d => d.PackageType).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.PackageTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservati__packa__5CD6CB2B");

            entity.HasOne(d => d.Pet).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.PetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservati__petID__59063A47");

            entity.HasOne(d => d.Room).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservati__roomI__5AEE82B9");

            entity.HasOne(d => d.StayType).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.StayTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservati__stayT__5BE2A6F2");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__CD98460A99AD5A69");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId)
                .ValueGeneratedNever()
                .HasColumnName("roleID");
            entity.Property(e => e.RoleDescription)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("roleDescription");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("PK__Room__6C3BF5DE68F25251");

            entity.ToTable("Room");

            entity.Property(e => e.RoomId).HasColumnName("roomID");
            entity.Property(e => e.RoomStatusId).HasColumnName("roomStatusID");
            entity.Property(e => e.RoomTypeId).HasColumnName("roomTypeID");

            entity.HasOne(d => d.RoomStatus).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.RoomStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Room__roomStatus__49C3F6B7");

            entity.HasOne(d => d.RoomType).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.RoomTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Room__roomTypeID__4AB81AF0");
        });

        modelBuilder.Entity<RoomStatus>(entity =>
        {
            entity.HasKey(e => e.RoomStatusId).HasName("PK__Room_Sta__AB16F43AE5222FA7");

            entity.ToTable("Room_Status");

            entity.Property(e => e.RoomStatusId).HasColumnName("roomStatusID");
            entity.Property(e => e.RoomStatusDescription)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("roomStatusDescription");
        });

        modelBuilder.Entity<RoomType>(entity =>
        {
            entity.HasKey(e => e.RoomTypeId).HasName("PK__Room_Typ__5E5E0CD386A24A0A");

            entity.ToTable("Room_Type");

            entity.Property(e => e.RoomTypeId).HasColumnName("roomTypeID");
            entity.Property(e => e.RoomTypeDescription)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("roomTypeDescription");
        });

        modelBuilder.Entity<StayType>(entity =>
        {
            entity.HasKey(e => e.StayTypeId).HasName("PK__Stay_Typ__38767773C142CAEF");

            entity.ToTable("Stay_Type");

            entity.Property(e => e.StayTypeId).HasColumnName("stayTypeID");
            entity.Property(e => e.StayTypeDescription)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("stayTypeDescription");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__CB9A1CDF7E0EB04E");

            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnName("userID");
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.PersonId).HasColumnName("personID");
            entity.Property(e => e.RoleId).HasColumnName("roleID");

            entity.HasOne(d => d.Person).WithMany(p => p.Users)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User__personID__3F466844");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User__roleID__403A8C7D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
