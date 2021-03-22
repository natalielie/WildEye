using EcoClean.Data;
using EcoClean.Models;
using EcoClean.Models.Client;
using EcoClean.Models.Enterprise;
using EcoClean.Models.Request;
using EcoClean.Models.Response;
using EcoClean.Models.SmartDevice;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoClean.Controllers.Api
{
    [Route("api/client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _dbContext;
        private string id = "a96c31ed-8efc-4239-9b6b-10ddc2eb3970";

       // string[] airSubstance = new string[] { "NO", "CO2", "CH2O", "Hg", "H2S" };
        //string[] waterSubstance = new string[] { "NH4+", "", "", "", ""};

        public ClientController(ApplicationDbContext dbContext,
            UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return Content(User.Identity.Name);
        }


        // enterprises //

        [HttpPost]
        [Route("addEnterprise")]
        public void addEnterprise(EnterpriseRequestModel request)
        {
            Client client = _dbContext.Clients.Single(x => x.UserId == id);
            Enterprise enterprise = new Enterprise
            {
                Name = request.Name,
                Kind = request.Kind,
                PhoneNumber = request.PhoneNumber,
                Product = request.Product,
                Address = request.Address,
                ClientId = client.ClientId
            };
            _dbContext.Enterprises.Add(enterprise);
            _dbContext.SaveChanges();

        }

        [HttpGet]
        [Route("getAllEnterprises")]
        public List<EnterpriseResponseModel> getAllClientsEnterprises()
        {
            Client client = _dbContext.Clients.Single(x => x.UserId == id);
            List<Enterprise> enterprises = _dbContext.Enterprises.Where(x => x.ClientId == client.ClientId).ToList();

            List<EnterpriseResponseModel> responseModels = enterprises
                .Select(x => new EnterpriseResponseModel(x.EnterpriseId, x.Name,
                x.Kind, x.PhoneNumber, x.Product, x.Address, x.Rate))
                .ToList();
            return responseModels;
        }



        [HttpGet]
        [Route("getEnterpriseById")]
        public Enterprise getEnterpriseById(int enterpriseId)
        {
            Client client = _dbContext.Clients.Single(x => x.UserId == id);
            Enterprise enterprise = _dbContext.Enterprises.SingleOrDefault(x => x.ClientId == client.ClientId && x.EnterpriseId == enterpriseId);

            return enterprise;
        }

        [HttpDelete]
        [Route("DeleteEnterpriseById")]
        public void DeleteEnterpriseById(int enterpriseId)
        {
            //string userIdStringified = _userManager.GetUserId(User);
            Client client = _dbContext.Clients.Single(x => x.UserId == id);


            Enterprise chosenEnterprise = _dbContext.Enterprises.SingleOrDefault(
                x => x.ClientId == client.ClientId && x.EnterpriseId == enterpriseId);

            if (chosenEnterprise == null)
            {
                throw new ArgumentException("Something wrong happened. Please, try again.");
            }

            _dbContext.Enterprises.Remove(chosenEnterprise);
            _dbContext.SaveChanges();
        }

        // statistics //

        [HttpGet]
        [Route("getStatistics")]
        public List<EnterpriseResponseModel> getStatistics()
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
        public List<EnterpriseResponseModel> getReversedStatistics()
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
        public List<EnterpriseResponseModel> getStatisticsWthinOneKind(string kind)
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
        public List<EnterpriseResponseModel> getTop(int topNumber)
        {
            List<Enterprise> enterprises = _dbContext.Enterprises.ToList();

            if (topNumber > enterprises.Count)
            {
                throw new ArgumentException("The number is too big. Please, try again.");
            }
            else
            {
                List<EnterpriseResponseModel> responseModels = getStatistics().Take(topNumber).ToList();
                return responseModels;
            }

        }


        // certificates //


        [HttpGet]
        [Route("getCertificate")]
        public List<CertificateResponseModel> getCertificates(int enterpriseId)
        {
            Enterprise enterprise = getEnterpriseById(enterpriseId);
            List<Certificate> certificates = _dbContext.Certificates.Where(x => x.EnterpriseId == enterprise.EnterpriseId).ToList();

            List<CertificateResponseModel> responseModels = certificates
                .Select(x => new CertificateResponseModel(x.EnterpriseId, x.CertificateId,
                x.CertificateDate))
                .ToList();
            return responseModels;
        }

        [HttpGet]
        [Route("getCertificateById")]
        public Certificate getCertificateById(int certificateId)
        {
            Certificate certificate = _dbContext.Certificates.SingleOrDefault(x => x.CertificateId == certificateId);

            return certificate;
        }

        [HttpPost]
        [Route("createCertificate")]
        public void createCertificate(CertificateRequestModel request)
        {
            Certificate certificate = new Certificate
            {
                EnterpriseId = request.EnterpriseId,
                CertificateDate = request.CertificateDate
            };
            _dbContext.Certificates.Add(certificate);
            _dbContext.SaveChanges();

        }

        [HttpDelete]
        [Route("DeleteCertificateById")]
        public void DeleteCertificateById(int certificateId)
        {
            Certificate chosenCertificate = _dbContext.Certificates.SingleOrDefault(x => x.CertificateId == certificateId);


            if (chosenCertificate == null)
            {
                throw new ArgumentException("Something wrong happened. Please, try again.");
            }

            _dbContext.Certificates.Remove(chosenCertificate);
            _dbContext.SaveChanges();
        }


        // rating //

        [HttpGet]
        [Route("getEnterpriseRateById")]
        public double getEnterpriseRateById(int enterpriseId)
        {
            Enterprise enterprise = getEnterpriseById(enterpriseId);
            return enterprise.Rate;
        }


        // tax //

        // formula: et = Σ(Mi * Npi)
        //Мi is the actual volume of emission of the i-th pollutant in tons (t);
        //Нпi - rates of eco-tax in the current year for a ton of the i-th pollutant
        [HttpPost]
        [Route("countTax")]
        public void countTax(TaxInformationRequestModel request)
        {

            double tax = 0;

            SmartDeviceData latestPollutionData = _dbContext.SmartDeviceData.Where(x => x.EnterpriseId == request.EnterpriseId)
                .OrderByDescending(x => x.SmartDeviceDataDate).FirstOrDefault();


            if(latestPollutionData.AirPollution > 1000)
            {
                if((request.AirPollutionSubstance == 1 && request.AirEmissions > 500)
                    || request.AirPollutionSubstance != 1)
                {
                    double ecotaxRate = DefineAirEcoTaxRate(request.AirPollutionSubstance);
                    tax += request.AirEmissions * ecotaxRate;
                }
            }

            if((request.WaterPollutionSubstance == 0 && request.WaterEmissions >= 0.4)
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

            _dbContext.Taxes.Add(taxInfo);
            _dbContext.SaveChanges();

        }


        public double DefineAirEcoTaxRate(int airPollutionSubstance)
        {
            double[] substancesTax = new double[] { 71.11,  0.012, 176.06, 3014.33, 228.53};
            return substancesTax[airPollutionSubstance];
        }

        public double DefineWaterEcoTaxRate(int waterPollutionSubstance)
        {
            double[] substancesTax = new double[] { 46.71, 18.70, 274.78, 4.02, 37.33 };
            return substancesTax[waterPollutionSubstance];
        }


        [HttpGet]
        [Route("getTaxOfEnterprise")]
        public Tax getTaxOfEnterprise(int enterpriseId)
        {
            Tax tax = _dbContext.Taxes.Where(x => x.EnterpriseId == enterpriseId)
                .OrderByDescending(x => x.TaxId).FirstOrDefault();
            return tax;
        }


        // reports //

        [HttpPost]
        [Route("createReport")]
        public void createReport(ReportRequestModel request)
        {

            Report report  = new Report
            {
                EnterpriseId = request.EnterpriseId,
                TaxId = request.TaxId,
                Comment = request.Comment,
                ReportDate = DateTime.Now
            };
            _dbContext.Reports.Add(report);
            _dbContext.SaveChanges();

        }

        [HttpGet]
        [Route("getAllReportsOfEnterprise")]
        public List<ReportResponseModel> getAllReportsOfEnterprise(int enterpriseId)
        {
            Enterprise enterprise = getEnterpriseById(enterpriseId);
            List<Report> reports = _dbContext.Reports.Where(x => x.EnterpriseId == enterprise.EnterpriseId).ToList();
            List<Tax> taxes = _dbContext.Taxes.Where(x => x.EnterpriseId == enterprise.EnterpriseId).ToList();
            List<ReportResponseModel> responseModels = GetLisOfReports(reports, taxes);

            return responseModels;
        }

        [HttpGet]
        [Route("getSingleReportOfEnterprise")]
        public ReportResponseModel getSingleReportOfEnterprise(int reportId)
        {
            List<Report> reports = _dbContext.Reports.Where(x => x.ReportId == reportId).ToList();
            List<Tax> taxes = _dbContext.Taxes.ToList();


            foreach (var report in reports)
            {
                foreach (var tax in taxes)
                {
                    if (report.TaxId == tax.TaxId)
                    {
                        ReportResponseModel response = new ReportResponseModel
                        (
                            report.ReportId,
                            tax.AirPollutionSubstance,
                            tax.WaterPollutionSubstance,
                            tax.AirEmissions,
                            tax.WaterEmissions,
                            tax.TaxCost,
                            report.Comment,
                            report.ReportDate
                        );

                        return response;
                    }
                }
            }
            throw new ArgumentException("There's no report. Try again later, please.");
        }

        [HttpGet]
        [Route("getReportOfEnterpriseByDate")]
        public List<ReportResponseModel> getReportOfEnterpriseByDate(DateTime date)
        {
            List<Report> reports = _dbContext.Reports.Where(x => x.ReportDate == date).ToList();
            List<Tax> taxes = _dbContext.Taxes.ToList();
            List<ReportResponseModel> responseModels = GetLisOfReports(reports, taxes);

            return responseModels;
        }

        [HttpGet]
        [Route("getAllReports")]
        public List<ReportResponseModel> getAllReports()
        {
            List<Report> reports = _dbContext.Reports.ToList();
            List<Tax> taxes = _dbContext.Taxes.ToList();
            List<ReportResponseModel> responseModels = GetLisOfReports(reports, taxes);

            return responseModels;
        }

        public List<ReportResponseModel> GetLisOfReports(List<Report> reports, List<Tax> taxes)
        {
            List<ReportResponseModel> responseModels = new List<ReportResponseModel>();

            foreach (var report in reports)
            {
                foreach (var tax in taxes)
                {
                    if (report.TaxId == tax.TaxId)
                    {
                        ReportResponseModel response = new ReportResponseModel
                        (
                            report.ReportId,
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

        // pollution data //

        [HttpGet]
        [Route("getLatestPollutionData")]
        public SmartDeviceDataResponseModel getLatestPollutionData(int enterpriseId)
        {
            SmartDeviceData latestPollutionData = _dbContext.SmartDeviceData
                .Where(x => x.EnterpriseId == enterpriseId)
                .OrderByDescending(x => x.SmartDeviceDataDate)
                .FirstOrDefault();

            Tax tax = _dbContext.Taxes.Where(x => x.EnterpriseId == enterpriseId)
                .OrderByDescending(x => x.TaxId).FirstOrDefault();

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
}
