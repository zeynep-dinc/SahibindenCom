Feature: SahibindenCom


Scenario: Basic test case for Sahibinden.com
	Given go to url sahibinden.com 
	When search malikane
	When control the found list
	When write found good score
	Then finish the test