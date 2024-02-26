using Xunit;
using Cdek.Controllers;
using Cdek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Text.Json;

namespace Cdek.Controllers.Tests
{
    public class CdekControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public CdekControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/Cdek/Price")]
        public async Task Get_Prise(string url)
        {
            // Arrange
            var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
                BaseAddress = new Uri("https://localhost:7004")
            });
            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    id = 0,
                    weight = 5.9,
                    height = 1,
                    width = 2,
                    length = 3,
                    fromLocation = "17e498bd-5f9e-4221-8998-5fa24a35ed2e",
                    toLocation = "a9958004-d348-442e-ae0d-b09aca9fdf25"
                }),
                Encoding.UTF8,
                "application/json");
            
            // Act
            var response = await client.PostAsync(url, jsonContent);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/plain; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("/Cdek/GetPriceById/1")]
        public async Task Get_By_Id(string url)
        {
            // Arrange
            var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
                BaseAddress = new Uri("https://localhost:7004")
            });

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/plain; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("/Cdek/GetCities")]
        [InlineData("/Cdek/GetPackages")]
        public async Task Get_Cities_And_Packages(string url)
        {
            // Arrange
            var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
                BaseAddress = new Uri("https://localhost:7004")
            });

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
    }
}

