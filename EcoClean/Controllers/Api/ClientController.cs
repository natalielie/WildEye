﻿using EcoClean.Data;
using EcoClean.Models;
using EcoClean.Models.Client;
using EcoClean.Models.Enterprise;
using EcoClean.Models.Request;
using EcoClean.Models.Response;
using EcoClean.Models.SmartDevice;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EcoClean.Controllers.Api
{
    [Route("api/client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _dbContext;

        // string[] airSubstance = new string[] { "NO", "CO2", "CH2O", "Hg", "H2S" };
        //string[] waterSubstance = new string[] { "NH4+", "", "", "", ""};

        public ClientController(ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        private string GetCurrentUser()
        {
            Claim userClaim =
           HttpContext?.User.Claims.FirstOrDefault(c => c.Type ==
           ClaimTypes.NameIdentifier);
            return userClaim?.Value;
        }


        public IActionResult Index()
        {
            return Content(User.Identity.Name);
        }

        // enterprises //

        [HttpPost]
        [Route("addEnterprise")]
        public void AddEnterprise(EnterpriseRequestModel request)
        {
            if (request != null)
            {
                string userid = GetCurrentUser();
                Client client = _dbContext.Clients.Single(x => x.UserId == userid);
                Enterprise enterprise = new Enterprise
                {
                    Name = request.Name,
                    Kind = request.Kind,
                    PhoneNumber = request.PhoneNumber,
                    Product = request.Product,
                    Address = request.Address,
                    ClientId = client.ClientId
                };
                try
                {
                    _dbContext.Enterprises.Add(enterprise);
                    _dbContext.SaveChanges();
                }
                catch (DbUpdateException)
                {
                }
            }
            else
            {
                throw new ArgumentException("Something went wrong, try again, please");
            }

        }


        [HttpGet]
        [Route("getAllEnterprises")]
        public IActionResult GetAllClientsEnterprises()
        {
            string userid = GetCurrentUser();
            Client client = _dbContext.Clients.Single(x => x.UserId == userid);
            List<Enterprise> enterprises = _dbContext.Enterprises.Where(x => x.ClientId == client.ClientId).ToList();

            List<EnterpriseResponseModel> responseModels = enterprises
                .Select(x => new EnterpriseResponseModel(x.EnterpriseId, x.Name,
                x.Kind, x.PhoneNumber, x.Product, x.Address, x.Rate))
                .ToList();
            return Ok(responseModels);
        }

        [HttpGet]
        [Route("getAllEnterprisesInSystem")]
        public IActionResult GetAllEnterprisesInSystem()
        {
            List<Enterprise> enterprises = _dbContext.Enterprises.ToList();

            List<EnterpriseResponseModel> responseModels = enterprises
                .Select(x => new EnterpriseResponseModel(x.EnterpriseId, x.Name,
                x.Kind, x.PhoneNumber, x.Product, x.Address, x.Rate))
                .ToList();
            return Ok(responseModels);
        }



        [HttpGet]
        [Route("GetEnterpriseById/{enterpriseId?}")]
        public Enterprise GetEnterpriseById([FromRoute] int enterpriseId)
        {
            Enterprise enterprise = _dbContext.Enterprises.SingleOrDefault(x => x.EnterpriseId == enterpriseId);

            return enterprise;
        }

        [HttpDelete]
        [Route("DeleteEnterpriseById/{enterpriseId?}")]
        public void DeleteEnterpriseById([FromRoute] int enterpriseId)
        {
            string userid = GetCurrentUser();
            Client client = _dbContext.Clients.Single(x => x.UserId == userid);


            Enterprise chosenEnterprise = _dbContext.Enterprises.SingleOrDefault(
                x => x.ClientId == client.ClientId && x.EnterpriseId == enterpriseId);

            if (chosenEnterprise == null)
            {
                throw new ArgumentException("Something wrong happened. Please, try again.");
            }

            try
            {
                _dbContext.Enterprises.Remove(chosenEnterprise);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException) { }
        }


        // statistics //

        [HttpGet]
        [Route("getStatistics")]
        public List<EnterpriseResponseModel> GetStatistics()
        {
            List<Enterprise> enterprises = _dbContext.Enterprises.ToList();

            List<EnterpriseResponseModel> responseModels = enterprises
                .Select(x => new EnterpriseResponseModel(x.EnterpriseId, x.Name,
                        x.Kind, x.PhoneNumber, x.Product, x.Address, x.Rate))
                .OrderByDescending(x => x.Rate)
                .ToList();
            return responseModels;
        }

        [HttpGet]
        [Route("getReversedStatistics")]
        public List<EnterpriseResponseModel> GetReversedStatistics()
        {
            List<Enterprise> enterprises = _dbContext.Enterprises.ToList();

            List<EnterpriseResponseModel> responseModels = enterprises
                .Select(x => new EnterpriseResponseModel(x.EnterpriseId, x.Name,
                        x.Kind, x.PhoneNumber, x.Product, x.Address, x.Rate))
                .OrderBy(x => x.Rate)
                .ToList();
            return responseModels;
        }

        [HttpGet]
        [Route("getStatisticsWthinOneKind")]
        public List<EnterpriseResponseModel> GetStatisticsWthinOneKind(string kind)
        {
            List<Enterprise> enterprises = _dbContext.Enterprises.Where(x => x.Kind == kind).ToList();

            List<EnterpriseResponseModel> responseModels = enterprises
                .Select(x => new EnterpriseResponseModel(x.EnterpriseId, x.Name,
                        x.Kind, x.PhoneNumber, x.Product, x.Address, x.Rate))
                .OrderByDescending(x => x.Rate)
                .ToList();
            return responseModels;
        }

        [HttpGet]
        [Route("getTop")]
        public List<EnterpriseResponseModel> GetTop(int topNumber)
        {
            List<Enterprise> enterprises = _dbContext.Enterprises.ToList();

            if (topNumber > enterprises.Count)
            {
                throw new ArgumentException("The number is too big. Please, try again.");
            }
            else
            {
                List<EnterpriseResponseModel> responseModels = GetStatistics().Take(topNumber).ToList();
                return responseModels;
            }

        }


        // certificates //

        [HttpPost]
        [Route("createCertificate")]
        public void CreateCertificate(CertificateRequestModel request)
        {
            Certificate certificate = new Certificate
            {
                EnterpriseId = request.EnterpriseId,
                CertificateDate = DateTime.Now
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

        [HttpGet]
        [Route("getAllUsersCertificates")]
        public List<Certificate> GetAllUsersCertificates()
        {
            string userid = GetCurrentUser();
            Client client = _dbContext.Clients.Single(x => x.UserId == userid);
            List<Enterprise> enterprises = _dbContext.Enterprises.Where(x => x.ClientId == client.ClientId).ToList();

            List<Certificate> certificates = new List<Certificate>();

            foreach (Enterprise enterprise in enterprises)
            {
                Certificate certificate = _dbContext.Certificates.SingleOrDefault(x => x.EnterpriseId == enterprise.EnterpriseId);
                if (certificate != null)
                    certificates.Add(certificate);
                else continue;
            }
            if (certificates.Count != 0)
                return certificates;
            else throw new NullReferenceException();
        }

        [HttpDelete]
        [Route("deleteCertificateById/{certificateId?}")]
        public void DeleteCertificateById([FromRoute] int certificateId)
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

        [HttpGet]
        [Route("getCertificate")]
        public List<CertificateResponseModel> GetCertificates(int enterpriseId)
        {
            Enterprise enterprise = GetEnterpriseById(enterpriseId);
            if (enterprise == null)
            {
                throw new ArgumentException("Something went wrong, try again, please");
            }
            else
            {
                List<Certificate> certificates = _dbContext.Certificates.Where(x => x.EnterpriseId == enterprise.EnterpriseId).ToList();

                List<CertificateResponseModel> responseModels = certificates
                    .Select(x => new CertificateResponseModel(x.EnterpriseId, x.CertificateId,
                    x.CertificateDate))
                    .ToList();
                return responseModels;
            }

        }

        [HttpGet]
        [Route("getCertificateById")]
        public Certificate GetCertificateById(int certificateId)
        {
            Certificate certificate = _dbContext.Certificates.SingleOrDefault(x => x.CertificateId == certificateId);

            if (certificate == null)
            {
                throw new ArgumentException("Something went wrong, try again, please");
            }
            else
            {
                return certificate;
            }
        }



        // tax //

        // formula: et = Σ(Mi * Npi)
        //Мi is the actual volume of emission of the i-th pollutant in tons (t);
        //Нпi - rates of eco-tax in the current year for a ton of the i-th pollutant
        [HttpPost]
        [Route("countTax")]
        public void CountTax(TaxInformationRequestModel request)
        {

            double tax = 0;

            SmartDeviceData latestPollutionData = _dbContext.SmartDeviceData.Where(x => x.EnterpriseId == request.EnterpriseId)
                .OrderByDescending(x => x.SmartDeviceDataDate).FirstOrDefault();

            if (latestPollutionData == null)
            {
                return;
            }
            else
            {
                if ((request.AirPollutionSubstance == 1 && request.AirEmissions > 500)
                    || request.AirPollutionSubstance != 1)
                {
                    double ecotaxRate = DefineAirEcoTaxRate(request.AirPollutionSubstance);
                    tax += request.AirEmissions * ecotaxRate;
                }

                if ((request.WaterPollutionSubstance == 0 && request.WaterEmissions >= 0.4)
                    || (request.WaterPollutionSubstance == 1 && request.WaterEmissions > 1.5)
                    || (request.WaterPollutionSubstance == 2 && request.WaterEmissions >= 1)
                    || request.WaterEmissions >= 6)
                {
                    double ecotaxRate = DefineWaterEcoTaxRate(request.WaterPollutionSubstance);
                    tax += request.WaterEmissions * ecotaxRate;
                }

                Tax taxInfo = new Tax
                (
                    request.EnterpriseId,
                    request.AirPollutionSubstance,
                    request.WaterPollutionSubstance,
                    request.AirEmissions,
                    request.WaterEmissions,
                    tax
                );

                try
                {
                    _dbContext.Taxes.Add(taxInfo);
                    _dbContext.SaveChanges();
                }
                catch (DbUpdateException) { }
            }

        }

        public double DefineAirEcoTaxRate(int airPollutionSubstance)
        {
            double[] substancesTax = new double[] { 71.11, 0.012, 176.06, 3014.33, 228.53 };
            return substancesTax[airPollutionSubstance];
        }

        public double DefineWaterEcoTaxRate(int waterPollutionSubstance)
        {
            double[] substancesTax = new double[] { 46.71, 18.70, 274.78, 4.02, 37.33 };
            return substancesTax[waterPollutionSubstance];
        }


        [HttpGet]
        [Route("getTaxOfEnterprise")]
        public Tax GetTaxOfEnterprise(int enterpriseId)
        {
            Tax tax = _dbContext.Taxes.Where(x => x.EnterpriseId == enterpriseId)
                .OrderByDescending(x => x.TaxId).FirstOrDefault();

            if (tax == null)
            {
                throw new ArgumentException("Something went wrong, try again, please");
            }
            else
            {
                return tax;
            }
        }

        [HttpGet]
        [Route("getAllTaxes")]
        public List<Tax> GetAllTaxes()
        {
            string userid = GetCurrentUser();
            Client client = _dbContext.Clients.Single(x => x.UserId == userid);
            List<Enterprise> enterprises = _dbContext.Enterprises.Where(x => x.ClientId == client.ClientId).ToList();

            List<Tax> taxes = new List<Tax>();

            foreach (Enterprise enterprise in enterprises)
            {
                Tax tax = _dbContext.Taxes.SingleOrDefault(x => x.EnterpriseId == enterprise.EnterpriseId);
                taxes.Add(tax);
            }
            return taxes;

        }


        // reports //

        [HttpPost]
        [Route("createReport")]
        public void CreateReport(ReportRequestModel request)
        {
            Tax tax = _dbContext.Taxes.Where(x => x.EnterpriseId == request.EnterpriseId)
                .OrderBy(x => x.TaxId).First();

            Report report = new Report
            {
                EnterpriseId = request.EnterpriseId,
                TaxId = tax.TaxId,
                Comment = request.Comment,
                ReportDate = DateTime.Now
            };

            try
            {
                _dbContext.Reports.Add(report);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException) { }


        }

        [HttpGet]
        [Route("getAllUsersReports")]
        public List<ReportResponseModel> getAllUsersReports()
        {
            string userid = GetCurrentUser();
            Client client = _dbContext.Clients.Single(x => x.UserId == userid);
            List<Enterprise> enterprises = _dbContext.Enterprises.Where(x => x.ClientId == client.ClientId).ToList();

            List<ReportResponseModel> responseReports = new List<ReportResponseModel>();

            foreach (Enterprise enterprise in enterprises)
            {
                List<ReportResponseModel> responseReportsOneEnterprise = GetAllReportsOfEnterprise(enterprise.EnterpriseId);

                responseReports.AddRange(responseReportsOneEnterprise);
            }
            if (responseReports.Count == 0)
            {
                return responseReports;
            }
            else
            {
                return responseReports;
            }


        }


        [HttpGet]
        [Route("getAllReportsOfEnterprise")]
        public List<ReportResponseModel> GetAllReportsOfEnterprise(int enterpriseId)
        {
            Enterprise enterprise = GetEnterpriseById(enterpriseId);
            if (enterprise == null)
            {
                throw new ArgumentException("There's no such eneterprise");
            }
            else
            {
                List<Report> reports = _dbContext.Reports.Where(x => x.EnterpriseId == enterprise.EnterpriseId).ToList();

                if (reports.Count == 0)
                {
                    //throw new ArgumentException("There's no reports");
                    return new List<ReportResponseModel>();
                }
                else
                {
                    // List<Tax> taxes = _dbContext.Taxes.Where(x => x.EnterpriseId == enterprise.EnterpriseId).ToList();
                    List<ReportResponseModel> responseModels = GetLisOfReports(reports);

                    return responseModels;
                }
            }

        }

        [HttpGet]
        [Route("getSingleReportOfEnterprise/{reportId?}")]
        public ReportResponseModel GetSingleReportOfEnterprise([FromRoute] int reportId)
        {
            List<Report> reports = _dbContext.Reports.Where(x => x.ReportId == reportId).ToList();

            List<Tax> taxes = _dbContext.Taxes.ToList();


            if (reports.Count == 0 || taxes.Count == 0)
            {
                throw new ArgumentException("There's no reports");
            }
            else
            {
                ReportResponseModel response = new ReportResponseModel();
                foreach (var report in reports)
                {
                    foreach (var tax in taxes)
                    {
                        if (report.TaxId == tax.TaxId)
                        {
                            response.ReportId = report.ReportId;
                            response.EnterpriseName = GetEnterpriseById(report.EnterpriseId).Name;
                            response.AirPollutionSubstance = tax.AirPollutionSubstance;
                            response.WaterPollutionSubstance = tax.WaterPollutionSubstance;
                            response.AirEmissions = tax.AirEmissions;
                            response.WaterEmissions = tax.WaterEmissions;
                            response.TaxCost = tax.TaxCost;
                            response.Comment = report.Comment;
                            response.ReportDate = report.ReportDate;

                        }
                    }
                }
                return response;
            }

        }

        [HttpGet]
        [Route("getReportOfEnterpriseByDate")]
        public List<ReportResponseModel> GetReportOfEnterpriseByDate(DateTime date)
        {
            List<Report> reports = _dbContext.Reports.Where(x => x.ReportDate == date).ToList();

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


        [HttpDelete]
        [Route("deleteReportById/{reportId?}")]
        public void deleteReportById([FromRoute] int reportId)
        {
            Report chosenReport = _dbContext.Reports.SingleOrDefault(x => x.ReportId == reportId);


            if (chosenReport == null)
            {
                throw new ArgumentException("Something wrong happened. Please, try again.");
            }
            try
            {
                _dbContext.Reports.Remove(chosenReport);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException) { }
        }


        // pollution data //


        [HttpPost]
        [Route("addSmartData")]
        public void addSmartDeviceData(SmartDeviceDataRequestModel request)
        {
            SmartDeviceData data = new SmartDeviceData
            {
                EnterpriseId = request.EnterpriseId,
                AirPollution = request.AirPollution,
                WaterPollution = request.WaterPollution,
                SmartDeviceDataDate = DateTime.Now
            };

            try
            {
                _dbContext.SmartDeviceData.Add(data);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException) { }


        }


        [HttpGet]
        [Route("getLatestPollutionData")]
        public SmartDeviceDataResponseModel GetLatestPollutionData(int enterpriseId)
        {
            SmartDeviceData latestPollutionData = _dbContext.SmartDeviceData
                .Where(x => x.EnterpriseId == enterpriseId)
                .OrderByDescending(x => x.SmartDeviceDataDate)
                .FirstOrDefault();
            Tax tax = _dbContext.Taxes.Where(x => x.EnterpriseId == enterpriseId)
            .OrderByDescending(x => x.TaxId).FirstOrDefault();

            if (tax == null || latestPollutionData == null)
            {
                throw new ArgumentException("There's no data");
            }
            else
            {
                SmartDeviceDataResponseModel response = new SmartDeviceDataResponseModel
            (
                enterpriseId,
                latestPollutionData.AirPollution,
                latestPollutionData.WaterPollution,
                tax.AirPollutionSubstance,
                tax.WaterPollutionSubstance,
                tax.AirEmissions,
                tax.WaterEmissions,
                latestPollutionData.SmartDeviceDataDate
            );

                return response;
            }
        }

        [HttpGet]
        [Route("getAllData")]
        public List<SmartDeviceData> getAllData()
        {
            string userid = GetCurrentUser();
            Client client = _dbContext.Clients.Single(x => x.UserId == userid);
            List<Enterprise> enterprises = _dbContext.Enterprises.Where(x => x.ClientId == client.ClientId).ToList();

            List<SmartDeviceData> responseModels = new List<SmartDeviceData>();

            foreach (Enterprise enterprise in enterprises)
            {
                List<SmartDeviceData> data = _dbContext.SmartDeviceData
                    .Where(x => x.EnterpriseId == enterprise.EnterpriseId).ToList();
                responseModels.AddRange(data);

            }
            return responseModels.OrderBy(x => x.EnterpriseId).ToList();
        }

        [HttpGet]
        [Route("getAllDataInSystem")]
        public List<SmartDeviceData> getAllDataInSystem()
        {
            List<SmartDeviceData> data = _dbContext.SmartDeviceData.ToList();

            return data;
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
            catch (ArgumentException)
            {
            }

            PollutionResponseModel allEnterprisesData = GetAveragePollutionData();

            double airRatio = enterpriseData.AirPollutionAverage / allEnterprisesData.AirPollutionAverage;
            double waterRatio = enterpriseData.WaterPollutionAverage / allEnterprisesData.WaterPollutionAverage;

            double averageRatio = (airRatio + waterRatio) / 2;

            double rate = 10 - Math.Round(10 - (averageRatio * 10));


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


        [HttpGet]
        [Route("getEnterpriseRateById")]
        public double getEnterpriseRateById(int enterpriseId)
        {
            Enterprise enterprise = GetEnterpriseById(enterpriseId);
            return Math.Round(enterprise.Rate, 2);
        }



        [HttpGet]
        [Route("getDataFromArduino")]
        public double[] getDataFromArduino()
        {
            double[] data = new double[2];
            SerialInformation.GetPorts();
            var serialInformation = new SerialInformation();

            serialInformation.ReadFromPort();

            for (int i = 0; i < 2; i++)
            {
                if(i % 2 != 0)
                {
                    data[i] = Convert.ToDouble(Console.ReadKey().Key);
                }
            }

            serialInformation.SerialPort.Close();

            return data;
        }
    }
}
