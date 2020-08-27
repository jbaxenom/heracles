using FluentAssertions;
using heracles.webapp.client;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace heracles.webapp.integration.tests
{
    [TestClass]
    public class HeraclesBackendIntegrationTests
    {
        private string BaseUri;

        [TestInitialize]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("testsettings.json", optional: false)
                .Build();

            // To know which endpoint to reach we check if it's set as Env Variable (for pipelines). If not we check the config file
            BaseUri = GetSettingFromEnvironmentOrFile("BaseUri", configuration.GetSection("TestEndpointSettings:BaseUri").Value, "https://localhost:32782");
        }

        [TestMethod]
        public void HeraclesBackend_Should_GiveValidResponseWithValidInput()
        {
            // Arrange
            var input = "1600";
            var heraclesClient = new HeraclesClient(new Uri(BaseUri));

            // Act
            var result = HeraclesClientExtensions.FormatMoney(heraclesClient, input);

            // Assert
            result.Should().Be("'1 600.00'");
        }

        private string GetSettingFromEnvironmentOrFile(string testSetting, string value, string defaultValue)
        {
            string envVariable = Environment.GetEnvironmentVariable(testSetting.ToUpper());

            if(string.IsNullOrEmpty(envVariable))
            {
                return string.IsNullOrEmpty(value) ? defaultValue : value;
            }

            return envVariable;
        }
    }
}
