using EcoClean.Models;
using EcoClean.Models.Client;
using EcoClean.Models.Enterprise;
using EcoClean.Models.SmartDevice;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Http;
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



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Enterprise>().HasData(
                new Enterprise
                {
                    EnterpriseId = 1,
                    Name = "WOG",
                    Kind = "Gas Station",
                    PhoneNumber = "+40097656789",
                    Product = "Fuel",
                    Address = "Riverside st, 33b",
                    Rate = 0,
                    ClientId = 1
                }
            );

            modelBuilder.Entity<Enterprise>().HasData(
                new Enterprise
                {
                    EnterpriseId = 2,
                    Name = "Johnny's",
                    Kind = "Restaurant",
                    PhoneNumber = "+40054776512",
                    Product = "Food",
                    Address = "DownCreek st, 16",
                    Rate = 0,
                    ClientId = 2
                }
            );

            modelBuilder.Entity<Enterprise>().HasData(
                new Enterprise
                {
                    EnterpriseId = 3,
                    Name = "Shoes On",
                    Kind = "Shoes Fabric",
                    PhoneNumber = "+380951332455",
                    Product = "Shoes",
                    Address = "Central st, 1",
                    Rate = 0,
                    ClientId = 1
                }
            );

            modelBuilder.Entity<SmartDeviceData>().HasData(
                new SmartDeviceData
                {
                    SmartDeviceDataId = 1,
                    EnterpriseId = 1,
                    AirPollution = 780,
                    WaterPollution = 2.5,
                    SmartDeviceDataDate = Convert.ToDateTime("2020-06-05"),
                }
            );

            modelBuilder.Entity<SmartDeviceData>().HasData(
                new SmartDeviceData
                {
                    SmartDeviceDataId = 2,
                    EnterpriseId = 1,
                    AirPollution = 830,
                    WaterPollution = 2.8,
                    SmartDeviceDataDate = Convert.ToDateTime("2020-09-08"),
                }
            );

            modelBuilder.Entity<SmartDeviceData>().HasData(
                new SmartDeviceData
                {
                    SmartDeviceDataId = 3,
                    EnterpriseId = 2,
                    AirPollution = 640,
                    WaterPollution = 1.7,
                    SmartDeviceDataDate = Convert.ToDateTime("2020-09-12"),
                }
            );

            modelBuilder.Entity<SmartDeviceData>().HasData(
                new SmartDeviceData
                {
                    SmartDeviceDataId = 4,
                    EnterpriseId = 1,
                    AirPollution = 797,
                    WaterPollution = 2.1,
                    SmartDeviceDataDate = Convert.ToDateTime("2020-12-01"),
                }
            );

            modelBuilder.Entity<SmartDeviceData>().HasData(
                new SmartDeviceData
                {
                    SmartDeviceDataId = 5,
                    EnterpriseId = 3,
                    AirPollution = 1246,
                    WaterPollution = 3.1,
                    SmartDeviceDataDate = Convert.ToDateTime("2020-12-02"),
                }
            );

            modelBuilder.Entity<SmartDeviceData>().HasData(
                new SmartDeviceData
                {
                    SmartDeviceDataId = 6,
                    EnterpriseId = 2,
                    AirPollution = 530,
                    WaterPollution = 1.1,
                    SmartDeviceDataDate = Convert.ToDateTime("2020-12-15"),
                }
            );

            modelBuilder.Entity<SmartDeviceData>().HasData(
                new SmartDeviceData
                {
                    SmartDeviceDataId = 7,
                    EnterpriseId = 2,
                    AirPollution = 499,
                    WaterPollution = 1.23,
                    SmartDeviceDataDate = Convert.ToDateTime("2021-02-03"),
                }
            );
            modelBuilder.Entity<SmartDeviceData>().HasData(
                new SmartDeviceData
                {
                    SmartDeviceDataId = 8,
                    EnterpriseId = 3,
                    AirPollution = 1189,
                    WaterPollution = 2.93,
                    SmartDeviceDataDate = Convert.ToDateTime("2021-02-05"),
                }
            );

            //modelBuilder.Entity<SmartDeviceData>().HasData(
            //    new SmartDeviceData
            //    {
            //        SmartDeviceDataId = 9,
            //        EnterpriseId = 3,
            //        AirPollution = 530,
            //        WaterPollution = 1.1,
            //        SmartDeviceDataDate = Convert.ToDateTime("2021-05-05"),
            //    }
            //);

        }
    }