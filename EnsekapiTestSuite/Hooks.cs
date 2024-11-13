using Reqnroll;
using System.Net.Http.Headers;
using System.Net.Http;
using EnsekBddTests.RequestModels;
using CucumberExpressions.Ast;
using System.Net.Http.Json;
using System.Text.Json;
using EnsekBddTests.ResponseModels;
using System.Net;

namespace EnsekapiTestSuite
{
    [Binding]
    public class Hooks
    {
        public HttpClient? _client;
        private string? _token;

        [BeforeScenario]
        public async Task BeforeScenario()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://qacandidatetest.ensek.io/ENSEK/");
            Authentication? authToken = await this.GetAuthToken();
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", authToken.AuthToken);
            HttpContent content = null;
            //Here the test data is reset before any test scenario is run
            HttpResponseMessage response = await _client.PostAsync("reset", content);
            response.EnsureSuccessStatusCode();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _client.Dispose();
        }

        private async Task<Authentication?> GetAuthToken()
        {
            Login loginDetails = new Login();
            loginDetails.Username = "test";
            loginDetails.Password = "testing";
            HttpResponseMessage response = await _client.PostAsJsonAsync("login", loginDetails);
            string content = await response.Content.ReadAsStringAsync();
            Authentication? authToken = JsonSerializer.Deserialize<Authentication>(content);
            return authToken;
        }
    }
}