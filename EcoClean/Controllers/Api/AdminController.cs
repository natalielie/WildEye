using EcoClean.Data;
using EcoClean.Models.Enterprise;
using EcoClean.Models.Request;
using EcoClean.Models.Response;
using EcoClean.Models.SmartDevice;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EcoClean.Controllers.Api
{
    [Route("api/admin")]
    //[Authorize(Roles = "Administrator")]
    [ApiController]
    public class AdminController : Controller
    {

      //  RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _dbContext;

        public AdminController(ApplicationDbContext dbContext)
        {
           // _roleManager = manager;
            _dbContext = dbContext;
        }

        //public async Task<IActionResult> GetRoles()
        //{
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "admin", NormalizedName = "ADMIN" });
        //    return View(await _roleManager.Roles.ToListAsync());
        //}
        public IActionResult Index()
        {
            return View();
        }

        // enterprises //

        [HttpGet]
        [Route("getEnterpriseById")]
        public Enterprise GetEnterpriseById(int enterpriseId)
        {
            // Client client = _dbContext.Clients.Single(x => x.UserId == id);
            Enterprise enterprise = _dbContext.Enterprises.SingleOrDefault(x => x.EnterpriseId == enterpriseId);

            return enterprise;
        }

        // certificate //

        [HttpGet]
        [Route("getAllCertificates")]
        public List<Certificate> GetAllCertificates()
        {
            List<Certificate> certificates = _dbContext.Certificates.ToList();

            if (certificates.Count == 0)
            {
                throw new ArgumentException("There's no certificate");
            }
            else
            {
                return certificates;
            }
        }

        [HttpPost]
        [Route("createCertificate")]
        public void CreateCertificate(CertificateRequestModel request)
        {
            Certificate certificate = new Certificate
            {
                EnterpriseId = request.EnterpriseId,
                CertificateDate = request.CertificateDate
            };
            try
            {
                _dbContext.Certificates.Add(certificate);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException)
            {

            }

        }

        [HttpDelete]
        [Route("deleteCertificateById")]
        public void DeleteCertificateById(int certificateId)
        {
            Certificate chosenCertificate = _dbContext.Certificates.SingleOrDefault(x => x.CertificateId == certificateId);


            if (chosenCertificate == null)
            {
                throw new ArgumentException("Something wrong happened. Please, try again.");
            }
            try
            {
                _dbContext.Certificates.Remove(chosenCertificate);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException) { }
        }

        // reports //

        [HttpGet]
        [Route("getAllReports")]
        public List<ReportResponseModel> GetAllReports()
        {
            List<Report> reports = _dbContext.Reports.ToList();

            if (reports.Count == 0)
            {
                throw new ArgumentException("There's no reports");
            }
            else
            {
                List<ReportResponseModel> responseModels = GetLisOfReports(reports);

                return responseModels;
            }
        }

        public List<ReportResponseModel> GetLisOfReports(List<Report> reports)
        {
            List<ReportResponseModel> responseModels = new List<ReportResponseModel>();

            List<Tax> taxes = _dbContext.Taxes.ToList();

            if (taxes.Count == 0)
            {
                throw new ArgumentException("There's no data");
            }
            else
            {

                foreach (var report in reports)
                {
                    foreach (var tax in taxes)
                    {
                        if (report.TaxId == tax.TaxId)
                        {
                            string enterpriseName = GetEnterpriseById(report.EnterpriseId).Name;
                            ReportResponseModel response = new ReportResponseModel
                            (
                                report.ReportId,
                                enterpriseName,
                                tax.AirPollutionSubstance,
                                tax.WaterPollutionSubstance,
                                tax.AirEmissions,
                                tax.WaterEmissions,
                                tax.TaxCost,
                                report.Comment,
                                report.ReportDate
                            );
                            responseModels.Add(response);
                        }
                    }
                }
                return responseModels;
            }
        }

        // rating //

        [HttpGet]
        [Route("getAveragePollutionOfEnterprise")]
        public PollutionResponseModel GetAveragePollutionOfEnterprise(int enterpriseId)
        {
            Enterprise enterprise = GetEnterpriseById(enterpriseId);

            List<SmartDeviceData> latestPollutionData = _dbContext.SmartDeviceData
               .Where(x => x.EnterpriseId == enterpriseId)
               .OrderByDescending(x => x.SmartDeviceDataDate).ToList();

            if (latestPollutionData.Count >= 3)
            {
                latestPollutionData = latestPollutionData.Take(3).ToList();
            }
            else
            {
                throw new ArgumentException("There's not enough data to get an average pollution");
            }


            double airPollutionAverage = 0;
            double waterPollutionAverage = 0;

            foreach (SmartDeviceData data in latestPollutionData)
            {
                airPollutionAverage += data.AirPollution;
                waterPollutionAverage += data.WaterPollution;
            }

            airPollutionAverage /= 4;
            waterPollutionAverage /= 4;

            PollutionResponseModel response = new PollutionResponseModel(airPollutionAverage, waterPollutionAverage);

            return response;

        }

        [HttpGet]
        [Route("getAveragePollutionData")]
        public PollutionResponseModel GetAveragePollutionData()
        {
            List<Enterprise> enterprises = _dbContext.Enterprises.ToList();

            double airPollutionAverage = 0;
            double waterPollutionAverage = 0;


            foreach (Enterprise enterprise in enterprises)
            {
                try
                {
                    PollutionResponseModel response = GetAveragePollutionOfEnterprise(enterprise.EnterpriseId);
                    airPollutionAverage += response.AirPollutionAverage;
                    waterPollutionAverage += response.WaterPollutionAverage;
                }
                catch (ArgumentException)
                {
                }
            }

            airPollutionAverage /= enterprises.Count;
            waterPollutionAverage /= enterprises.Count;


            PollutionResponseModel responseModel = new PollutionResponseModel(
                airPollutionAverage, waterPollutionAverage);

            return responseModel;
        }

        [HttpPost]
        [Route("setEnterpriseRate")]
        public async void SetEnterpriseRate(int enterpriseId)
        {
            Enterprise enterprise = GetEnterpriseById(enterpriseId);

            PollutionResponseModel enterpriseData = new PollutionResponseModel();

            try
            {
                enterpriseData = GetAveragePollutionOfEnterprise(enterprise.EnterpriseId);
            }
            catch (ArgumentException ex)
            {
            }

            PollutionResponseModel allEnterprisesData = GetAveragePollutionData();

            //if(enterpriseData.AirPollutionAverage > 350)
            //{
            double airRatio = enterpriseData.AirPollutionAverage / allEnterprisesData.AirPollutionAverage;
            //}
            //if (enterpriseData.AirPollutionAverage >)
            //{
            double waterRatio = enterpriseData.WaterPollutionAverage / allEnterprisesData.WaterPollutionAverage;
            //}

            double averageRatio = (airRatio + waterRatio) / 2;

            // 10-point system
            double rate = 10 - Math.Abs(10 - (averageRatio * 10));


            var enterpriseToUpdate = await _dbContext.Enterprises.FindAsync(enterpriseId);


            await TryUpdateModelAsync<Enterprise>(
                enterpriseToUpdate,
                "",
                x => x.EnterpriseId,
                x => x.Name,
                x => x.Kind,
                x => x.PhoneNumber,
                x => x.Product,
                x => x.Address);

            enterprise.Rate = rate;

            try
            {
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException /* ex */)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, " +
                    "see your system administrator.");
            }

        }

        // database
        [HttpPost]
        [Route("backupDatabase")]
        public async Task<string> BackupDatabase()
        {
            // read connectionstring from config file
            var connectionString = "Server=(localdb)\\mssqllocaldb;Database=aspnet-EcoCleanDb;Trusted_Connection=True;MultipleActiveResultSets=true";

            // read backup folder from config file ("C:/temp/")
            var backupFolder = "C:/Program Files/Microsoft SQL Server/MSSQL12.SQLEXPRESS/MSSQL/Backup/";

            var sqlConStrBuilder = new SqlConnectionStringBuilder(connectionString);

            // set backupfilename (you will get something like: "C:/temp/MyDatabase-2013-12-07.bak")
            var backupFileName = String.Format("{0}{1}-{2}.bak",
                backupFolder, sqlConStrBuilder.InitialCatalog,
                DateTime.Now.ToString("yyyy-MM-dd"));

            using (var connection = new SqlConnection(sqlConStrBuilder.ConnectionString))
            {
                var query = String.Format("BACKUP DATABASE {0} TO DISK='{1}'",
                    sqlConStrBuilder.InitialCatalog, backupFileName);

                using (var command = new SqlCommand(query, connection))
                {
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    return "Backup done";
                }
            }
        }


        [HttpPost]
        [Route("restoreDatabase")]
        private void RestoreDatabase(string localDatabasePath, string fileListDataName, string fileListLogName)
        {
            string localDownloadFilePath = "C:/Program Files/Microsoft SQL Server/MSSQL12.SQLEXPRESS/MSSQL/Backup/";
            Console.WriteLine(string.Format("Restoring database {0}...", localDatabasePath));
            string fileListDataPath = Directory.GetParent(localDownloadFilePath).Parent.FullName + @"\DATA\" + fileListDataName + ".mdf";
            string fileListLogPath = Directory.GetParent(localDownloadFilePath).Parent.FullName + @"\DATA\" + fileListLogName + ".ldf";

            string sql = @"RESTORE DATABASE @dbName FROM DISK = @path WITH RECOVERY,
        MOVE @fileListDataName TO @fileListDataPath,
        MOVE @fileListLogName TO @fileListLogPath";

            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=aspnet-EcoCleanDb;Trusted_Connection=True;MultipleActiveResultSets=true";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.CommandTimeout = 7200;
                    command.Parameters.AddWithValue("@dbName", fileListDataName);
                    command.Parameters.AddWithValue("@path", localDatabasePath);
                    command.Parameters.AddWithValue("@fileListDataName", fileListDataName);
                    command.Parameters.AddWithValue("@fileListDataPath", fileListDataPath);
                    command.Parameters.AddWithValue("@fileListLogName", fileListLogName);
                    command.Parameters.AddWithValue("@fileListLogPath", fileListLogPath);

                    command.ExecuteNonQuery();
                }
            }
            Console.WriteLine(string.Format("Database restoration complete for {0}.", localDatabasePath));
        }
    }
}

