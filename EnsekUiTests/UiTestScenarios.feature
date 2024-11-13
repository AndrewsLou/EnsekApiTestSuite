Feature: UiTestScenarios

The set of test scenarios below are for end to end testing of the buy functionality on the application provided.

#Green path scenario
Scenario Outline: User places an order on the web page with valid values
	Given the user navigates to the buy energy page
	 And the user enters a value of <NumberOfUnits> into the <UnitType>
	When the user clicks the buy button
	Then the user is redirected to the order confirmation page
	 And the amount of units remaining is reduced by <NumberOfUnits>
Examples: 
| UnitType    | NumberOfUnits |
| Gas         | 1             |
| Electricity | 1             |
| Oil         | 1             |

#Negative scenarios
Scenario Outline: User orders more than the quantity of units available
	Given the user navigates to the buy energy page
	 And the user enters a value of <NumberOfUnits> into the <UnitType>
	When the user clicks the buy button
	Then the user is shown an error message 'You are unable to order more than the quantity of available units'
	 And the order is not placed
Examples: 
| UnitType    | NumberOfUnits |
| Gas         | 999999        |
| Electricity | 999999        |
| Oil         | 999999        |

Scenario Outline: User orders using invalid values
	Given the user navigates to the buy energy page
	 And the user enters a value of <NumberOfUnits> into the <UnitType>
	When the user clicks the buy button
	Then the user is shown an error message 'The value must be a number.'
	 And the order is not placed
Examples: 
| UnitType    | NumberOfUnits |
| Gas         | test          |
| Electricity | test          |
| Oil         | test          |

Scenario: User orders using no value at all
	Given the user navigates to the buy energy page
	 And the user enters a value of 0 into the Gas
	When the user clicks the buy button
	Then the user is shown an error message 'You must enter a value greater than 0'
	 And the order is not placed

