Feature: EnsekApiTestSuite

A set of tests for testing the Ensek candidate API endpoints

@GetRequest
Scenario: Get Request Sent to Energy Endpoint Returns 200 And List Of EnergyTypes
When a Get request is sent to the Energy endpoint
Then a 200 Ok Response is returned
 And the Energy Data is deserialised
 And the following Energy Data is returned
  | EnergyData | EnergyId | PricePerUnit | QuantityOfUnits | UnitType |
  | Electric   | 3        | 0.47         | 4170            | kwh      |
  | Gas        | 1        | 0.34         | 2810            | m³       |
  | Nuclear    | 2        | 0.56         | 0               | MW       |
  | Oil        | 4        | 0.5          | -359            | Litres   |

 Scenario: Creating Order Of Each Fuel
 Given the following order is sent to the Buy endpoint
 | EnergyType | Quantity |
 | Gas        | 1        |
 | Oil        | 1        |
 | Electric   | 1        |
 | Nuclear    | 1        |
 When the Orders endpoint is called
 Then a 200 Ok Response is returned
  And the orders are deserialised
  And the orders are added to the list of orders

Scenario: Creating An Order For An EnergyType That Does Not Exist
Given the energy type '99' and quantity '1' is ordered
When an order is made to the Buy endpoint
Then a 400 bad request is returned

#This test is failing due to the delete order endpoint not working correctly
Scenario: Delete an existing order returns 200 and removes order
Given an order exists
When a Delete request is sent to the Delete Order endpoint
Then a 200 Ok Response is returned
 And the order is removed

