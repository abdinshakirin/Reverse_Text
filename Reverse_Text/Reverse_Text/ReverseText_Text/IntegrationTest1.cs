using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseText_Test
{
    [TestClass]
    public class IntegrationTest1
    {
        private HttpClient _client;

        [TestInitialize]
        public void Initialize()
        {
            // Create an instance of HttpClient and configure it for integration testing
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7025/") // Replace with your API base URL
            };
        }

        [TestCleanup]
        public void Cleanup()
        {
            _client.Dispose();
        }

        [TestMethod]
        public async Task TestReverseTextApiIntegration()
        {
            // Arrange
            var inputText = "12345";
            var expected = "54321";


            // Act
            var response = await _client.GetAsync($"api/ReverseText/ReverseText?inputText={inputText}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}

