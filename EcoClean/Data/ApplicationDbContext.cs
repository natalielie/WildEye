﻿using EcoClean.Models;
using EcoClean.Models.Client;
using EcoClean.Models.Enterprise;
using EcoClean.Models.SmartDevice;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoClean.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
   
        public DbSet<Enterprise> Enterprises { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Tax> Taxes { get; set; }
        public DbSet<SmartDeviceData> SmartDeviceData { get; set; }

        //public DbSet<PetDiaryNote> PetDiaryNotes { get; set; }
        //public DbSet<PetMedCardNote> PetMedCardNotes { get; set; }
        //public DbSet<SmartDeviceData> SmartDeviceData { get; set; }
        //public DbSet<Appointment> Appointments { get; set; }
        //public DbSet<ProfessionalRole> ProfessionalRoles { get; set; }
        //public DbSet<ProfessionalSchedule> ProfessionalSchedules { get; set; }

        //public DbSet<PetAssignment> PetAssignments { get; set; }
        //public DbSet<ProfessionalAppointment> ProfessionalAppointments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            //modelBuilder.ConfigurePersistedGrantContext(operationalStoreOptions.Value);

            //modelBuilder.Entity<PetAssignment>()
            //   .HasKey(c => new { c.PetId, c.ProfessionalId });

            //modelBuilder.Entity<ProfessionalAppointment>()
            //    .HasKey(c => new { c.ProfessionalId, c.PetId, c.AppointmentDateTime });

            //modelBuilder.Entity<ProfessionalSchedule>()
            //    .HasKey(c => new { c.ProfessionalId, c.Weekday, c.DateTimeBegin, c.DateTimeEnd });

            //modelBuilder.Entity<Client>().HasData(
            //    new Client
            //    {
            //        ClientId = 1,
            //        FirstName = "Tyler",
            //        LastName = "Joseph",
            //        PhoneNumber = "+40097656789",
            //        DateOfBirth = DateTime.Parse("1988-12-01"),
            //        Address = "Riverside st, 33b"
            //    }
            //);

            //modelBuilder.Entity<Client>().HasData(
            //    new Client
            //    {
            //        ClientId = 2,
            //        FirstName = "Joshua",
            //        LastName = "Dun",
            //        PhoneNumber = "+40054776512",
            //        DateOfBirth = DateTime.Parse("1988-06-12"),
            //        Address = "Riverside st, 33a"
            //    }
            //);

            //modelBuilder.Entity<Pet>().HasData(
            //    new Pet
            //    {
            //        PetId = 1,
            //        PetName = "Twinkie",
            //        AnimalKind = "Cat",
            //        PetSex = "Female",
            //        PetAge = 1,
            //        ClientId = 1
            //    }
            //);

            //modelBuilder.Entity<Pet>().HasData(
            //    new Pet
            //    {
            //        PetId = 2,
            //        PetName = "Jim",
            //        AnimalKind = "Dog",
            //        PetSex = "Male",
            //        PetAge = 3,
            //        ClientId = 2
            //    }
            //);

            //modelBuilder.Entity<Pet>().HasData(
            //    new Pet
            //    {
            //        PetId = 3,
            //        PetName = "Cinnabon",
            //        AnimalKind = "Cat",
            //        PetSex = "Male",
            //        PetAge = 1,
            //        ClientId = 2
            //    }
            //);
        }
    }
}
