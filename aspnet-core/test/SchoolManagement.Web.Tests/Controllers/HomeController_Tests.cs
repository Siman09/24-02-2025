﻿using System.Threading.Tasks;
using SchoolManagement.Models.TokenAuth;
using SchoolManagement.Web.Controllers;
using Shouldly;
using Xunit;

namespace SchoolManagement.Web.Tests.Controllers
{
    public class HomeController_Tests: SchoolManagementWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}