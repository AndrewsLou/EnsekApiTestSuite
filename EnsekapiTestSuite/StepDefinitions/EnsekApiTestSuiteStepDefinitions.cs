using EnsekapiTestSuite.ResponseModels;
using EnsekBddTests.ResponseModels;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Reqnroll;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace EnsekapiTestSuite.StepDefinitions
{
    [Binding]
    public sealed class EnsekApiTestSuiteStepDefinitions : Hooks
    {
        private ScenarioContext _scenarioContext;

        EnsekApiTestSuiteStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [StepDefinition("a Get request is sent to the Energy endpoint")]
        public async Task WhenAGetRequestIsSentToTheEnergyEndpoint()
        {
            HttpResponseMessage response = await _client.GetAsync("energy");
            _scenarioContext.Add("Response", response);
        }

        [StepDefinition("a 200 Ok Response is returned")]
        public void ThenAOkResponseIsReturned()
        {
            HttpResponseMessage response = _scenarioContext.Get<HttpResponseMessage>("Response");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [StepDefinition("the Energy Data is deserialised")]
        public async Task ThenTheEnergyDataIsDeserialised()
        {
            HttpResponseMessage response = _scenarioContext.Get<HttpResponseMessage>("Response");
            var content = await response.Content.ReadAsStringAsync();
            EnergyData energydata = JsonSerializer.Deserialize<EnergyData>(content);
            _scenarioContext.Add("EnergyData", energydata);
        }

        [StepDefinition("the following Energy Data is returned")]
        public void ThenTheFollowingEnergyTypesAreReturned(DataTable dataTable)
        {
            EnergyData actualData = _scenarioContext.Get<EnergyData>("EnergyData");
            string fileName = "Data//EnergyData.json";
            string expectedJson = File.ReadAllText(fileName);
            EnergyData expectedData = JsonSerializer.Deserialize<EnergyData>(expectedJson);
            actualData.Should().BeEquivalentTo(expectedData);
        }

        [StepDefinition("the following order is sent to the Buy endpoint")]
        public async Task GivenTheFollowingOrderIsSentToTheBuyEndpoint(DataTable dataTable)
        {
            List<string> messages = [];
            int numberOfEnergyTypes = Enum.GetValues(typeof(EnergyTypeEnum)).Length;
            for (int i = 0; i < numberOfEnergyTypes; i++)
            {
                HttpContent content = null;
                var response = await _client.PutAsync($"buy/{i+1}/1", content);
                string message = await response.Content.ReadAsStringAsync();
                messages.Add(message);
            }
                _scenarioContext.Add($"OrderMessages", messages);
        }

        [StepDefinition("the Orders endpoint is called")]
        public async Task WhenTheOrdersEndpointIsCalled()
        {
            HttpResponseMessage response = await _client.GetAsync("orders");
            _scenarioContext.Add("Response", response);
        }

        [StepDefinition("the orders are deserialised")]
        public async Task ThenTheOrdersAreDeserialised()
        {
            var response = _scenarioContext.Get<HttpResponseMessage>("Response");
            string json = await response.Content.ReadAsStringAsync();
            var orders = JsonSerializer.Deserialize<List<Orders>>(json);
            _scenarioContext.Add("Orders", orders);
        }


        [StepDefinition("the orders are added to the list of orders")]
        public void ThenTheOrdersAreAddedToTheListOfOrders()
        {
            var orders = _scenarioContext.Get<List<Orders>>("Orders");
            var placedOrders = _scenarioContext.Get<List<string>>("OrderMessages");
            List<Guid> checkedPlacedOrders = [];
            for (int i = 0; i < placedOrders.Count(); i++)
            {
                int ordersLength = orders.Count();
                for (int k = 0; k < ordersLength; k++)
                {
                    string message = placedOrders[i];
                    if (message.Contains(orders[k].Id.ToString()))
                    {
                        checkedPlacedOrders.Add(orders[k].Id);
                        orders[k].Quantity.Should().Be(1);
                    } 
                }
                
            }
            checkedPlacedOrders.Count().Should().Be(placedOrders.Count());
        }

        [StepDefinition("an order exists")]
        public async Task GivenTheOrderExists()
        {
            await this.WhenTheOrdersEndpointIsCalled();
            await this.ThenTheOrdersAreDeserialised();
            List<Orders> orders = _scenarioContext.Get<List<Orders>>("Orders");
            _scenarioContext.Remove("Response");
            _scenarioContext.Add("OrderDeletedId", orders[0].Id.ToString());
        }

        [StepDefinition("a Delete request is sent to the Delete Order endpoint")]
        public async Task WhenADeleteRequestIsSentToTheDeleteOrderEndpoint()
        {
            string orderId = _scenarioContext.Get<string>("OrderDeletedId");
            var response = await _client.DeleteAsync($"orders/{orderId}");
            _scenarioContext.Add("Response", response);
        }

        [StepDefinition("the order is removed")]
        public async Task ThenTheOrderIsRemoved()
        {
            string orderDeletedId = _scenarioContext.Get<string>("OrderDeletedId");
            Guid orderdeletedGuid =  Guid.Parse(orderDeletedId);
            _scenarioContext.Remove("Response");
            await this.WhenTheOrdersEndpointIsCalled();
            await this.ThenTheEnergyDataIsDeserialised();
            List<Orders> orders = _scenarioContext.Get<List<Orders>>("Orders");

            for (int i = 0; i < orders.Count(); i++)
            {
                orders[i].Id.Should().NotBe(orderdeletedGuid);
            }
        }

        [StepDefinition("the energy type {string} and quantity {string} is ordered")]
        public void GivenTheEnergyTypeAndQuantityIsOrdered(string energytype, string quantity)
        {
            _scenarioContext.Add("EnergyType", energytype);
            _scenarioContext.Add("Quantity", quantity);
        }

        [StepDefinition("an order is made to the Buy endpoint")]
        public async Task WhenAnOrderIsMadeToTheBuyEndpoint()
        {
            string energyType = _scenarioContext.Get<string>("EnergyType");
            string quantity = _scenarioContext.Get<string>("Quantity");

            HttpContent content = null;
            var response = await _client.PutAsync($"buy/{energyType}/{quantity}", content);
            _scenarioContext.Add("Response", response);
        }

        [StepDefinition("a 400 bad request is returned")]
        public void ThenABadRequestIsReturned()
        {
            var response = _scenarioContext.Get<HttpResponseMessage>("Response");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

    }
}
