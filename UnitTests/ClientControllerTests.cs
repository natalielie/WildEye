using System;
using Xunit;
using EcoClean.Controllers.Api;
using EcoClean.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using EcoClean.Models.Response;
using Microsoft.EntityFrameworkCore;
using EcoClean;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using Shouldly;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using Moq;
using NUnit.Framework;

namespace UnitTests
{
    //public class ClientControllerTests
    //{
    //    [Fact]
    //    public void ValidEmail()
    //    {
    //        //Arrange
    //        var mailManager = new MailManager();
    //        const string mailAddress = "john.smith@company.com";

    //        //Act
    //        bool isValid = mailManager.IsValidAddress(mailAddress);

    //        //Assert
    //        Assert.True(isValid, $"The email {mailAddress} is not valid");
    //    }

    //    [Fact]
    //    public void NotValidEmail()
    //    {
    //        //Arrange
    //        var mailManager = new MailManager();
    //        const string mailAddress = "john.smith.company.com";

    //        //Act
    //        bool isValid = mailManager.IsValidAddress(mailAddress);

    //        //Assert
    //        Assert.False(isValid, $"The email {mailAddress} is valid, but it shouldn’t");
    //    }

    //    [Theory]
    //    [InlineData("john.smith@company.com", true)]
    //    [InlineData("johnsmith@company.com", true)]
    //    [InlineData("john.smith@company.comma", true)]
    //    [InlineData("john.smith@company.it", true)]
    //    [InlineData("john.smith.company.com", false)]
    //    [InlineData("john@smith@company.com", false)]
    //    [InlineData("john", false)]
    //    [InlineData("", false)]
    //    public void CheckEmail(string mailAddress, bool expectedTestResult)
    //    {
    //        var mailManager = new MailManager();

    //        Assert.Equal(expectedTestResult, mailManager.IsValidAddress(mailAddress));
    //    }
    //}
    public class ClientControllerTests
    {

    }
}




//ClientController _controller;
//private readonly ApplicationDbContext _dbContext;
//public ClientControllerTests()
//{

//    _controller = new ClientController(_dbContext);
//}
//[Fact]
//public void Get_WhenCalled_ReturnsOkResult()
//{
//// Act
//var okResult = _controller.GetEnterpriseById(3);
//// Assert
//Assert.IsType<OkObjectResult>(okResult);


//}
//[Fact]
//public void Get_WhenCalled_ReturnsAllItems()
//{
//    // Act
//    var okResult = _controller.GetAllEnterprisesInSystem().Result as OkObjectResult;
//    // Assert
//    var items = Assert.IsType<List<EnterpriseResponseModel>>(okResult.Value);
//    Assert.Equal(5, items.Count);
//}
