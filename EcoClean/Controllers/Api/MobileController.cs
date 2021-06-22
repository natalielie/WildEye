using EcoClean.Areas.Identity.Pages.Account;
using EcoClean.Data;
using EcoClean.Models;
using EcoClean.Models.Client;
using EcoClean.Models.Enterprise;
using EcoClean.Models.Request;
using EcoClean.Models.Response;
using EcoClean.Models.SmartDevice;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EcoClean.Controllers.Api
{
    [Route("api/mobile")]
    [ApiController]
    public class MobileController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        string userId = "cf38fb1b-0b68-4cbb-a74c-f1983be40010";

        // string[] airSubstance = new string[] { "NO", "CO2", "CH2O", "Hg", "H2S" };
        //string[] waterSubstance = new string[] { "NH4+", "", "", "", ""};
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginRequestModel> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public MobileController(ApplicationDbContext dbContext,
            ILogger<LoginRequestModel> logger, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _dbContext = dbContext;
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        private string GetCurrentUser()
        {
            Claim userClaim =
           HttpContext?.User.Claims.FirstOrDefault(c => c.Type ==
           ClaimTypes.NameIdentifier);
            return userClaim?.Value;
        }

        [HttpPost]
        [Route("login")]
        public async Task<string> LogIn(LoginRequestModel request)
        {
            if(request != null)
            {
                //       var result = await _signInManager.PasswordSignInAsync(request.Email,
                //request.Password, false, false);
                //       _logger.LogInformation("User logged in.");
                //       await Authenticate(request.Email);
                //       string userid = GetCurrentUser();
                //       ApplicationUser user = _dbContext.Users.SingleOrDefault(
                //      x => x.Email == request.Email && x.PasswordHash == request.Password);
                //       if (user == null)
                //       {
                //           throw new NullReferenceException();
                //       }
                //       else
                //       {
                //           await Authenticate(request.Email);
                //           userId = user.Id;

                //           return userId;
                //       }
                var user = await _userManager.FindByEmailAsync(request.Email);
                var subjectId = Guid.NewGuid().ToString();
                if (user != null &&
                    await _userManager.CheckPasswordAsync(user, request.Password))
                {
                    var claims = new[] { 
                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(JwtClaimTypes.Subject, subjectId)
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        ExpiresUtc = DateTimeOffset.Now.AddDays(1),
                        IsPersistent = true,
                    };

                    var result = await _signInManager.PasswordSignInAsync(request.Email,
                    request.Password, false, false);

                    await HttpContext.SignInAsync(principal);
                    await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                     await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(principal), authProperties);

                }
                return HttpContext.User.Identity.Name;
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
    {
        new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
    };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        // enterprises //

        [HttpPost]
        [Route("addEnterprise")]
        public void AddEnterprise(EnterpriseRequestModel request)
        {
            if (request != null)
            {
                Client client = _dbContext.Clients.Single(x => x.UserId == userId);
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
            Client client = _dbContext.Clients.Single(x => x.UserId == userId);
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
            Client client = _dbContext.Clients.Single(x => x.UserId == userId);

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


        // smart device data //


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
            Client client = _dbContext.Clients.Single(x => x.UserId == userId);
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
    }
}
