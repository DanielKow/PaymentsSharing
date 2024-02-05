Feature: Refunds
	Calculate refunds


Scenario: May 2022
	Given Mikołaj has paid 400 PLN
	And Mikołaj has paid 226 PLN
	And Mikołaj has paid 10 PLN for meat
	And Natalia has paid 14 PLN
	And Natalia has paid 100 PLN
	And Natalia has paid 103 PLN
	And Andrzej has paid 330 PLN
	And Andrzej has paid 4 PLN for meat
	When refund is recalculated
	Then Natalia should return 174 PLN to Mikołaj
	And Andrzej should return 64 PLN to Mikołaj
	
Scenario: January 2022
	Given Mikołaj has paid 308 PLN
	And Natalia has paid 357 PLN
	And Andrzej has paid 319 PLN and 43 PLN for meat
	When refund is recalculated
	Then Mikołaj should return 29 PLN to Natalia
	And Mikołaj should return 13 PLN to Andrzej
	
Scenario: March 2022
	Given Mikołaj has paid 273 PLN
	And Mikołaj has paid 44 PLN for meat
	And Natalia has paid 449 PLN
	And Andrzej has paid 86 PLN for meat
	When refund is recalculated
	Then Andrzej should return 12 PLN to Mikołaj
	And Andrzej should return 209 PLN to Natalia
	
Scenario: December 2021
	Given Mikołaj has paid 450 PLN and 47 PLN for meat
	And Natalia has paid 146 PLN
	And Andrzej has paid 435 PLN and 18 PLN for meat
	When refund is recalculated
	Then Natalia should return 121 PLN to Mikołaj
	And Natalia should return 77 PLN to Andrzej
	
Scenario: January 2024
	Given Mikołaj has paid 1025 PLN
	And Natalia has paid 47 PLN
	And Andrzej has paid 150 PLN
	And Natalia and Mikołaj have paid 915 PLN
	When refund is recalculated
	Then Andrzej should return 258 PLN to Mikołaj
	And Andrzej should return 305 PLN to Natalia+Mikołaj
	And Natalia should return 361 PLN to Mikołaj
	
Scenario: Mikołaj and Natalia paid from shared account
	Given Mikołaj has paid 1025 PLN
	And Natalia has paid 47 PLN
	And Andrzej has paid 150 PLN
	And Mikołaj and Natalia have paid 915 PLN
	When refund is recalculated
	Then Andrzej should return 258 PLN to Mikołaj
	And Andrzej should return 305 PLN to Natalia+Mikołaj
	And Natalia should return 361 PLN to Mikołaj
	
Scenario: Mikołaj paid for himself and Natalia
	Given Mikołaj has paid 300 PLN
	And Natalia has paid 90 PLN
	And Mikołaj has paid 200 PLN for Mikołaj and Natalia
	When refund is recalculated
	Then Andrzej should return 130 PLN to Mikołaj
	And Natalia should return 140 PLN to Mikołaj

@ignore
Scenario: Natalia paid for herself and Mikołaj - not optimized number of refunding persons
	Given Mikołaj has paid 300 PLN
	And Natalia has paid 90 PLN
	And Natalia has paid 200 PLN for Mikołaj and Natalia
	When refund is recalculated
	Then Andrzej should return 130 PLN to Mikołaj
	And Mikołaj should return 60 PLN to Natalia
	
Scenario: Natalia paid for herself and Mikołaj
	Given Mikołaj has paid 300 PLN
	And Natalia has paid 90 PLN
	And Natalia has paid 200 PLN for Mikołaj and Natalia
	When refund is recalculated
	Then Andrzej should return 70 PLN to Mikołaj
	And Andrzej should return 60 PLN to Natalia
	
Scenario: Couple payments from shared account of Mikołaj and Natalia
	Given Mikołaj has paid 900 PLN
	And Natalia has paid 300 PLN
	And Mikołaj and Natalia have paid 200 PLN
	And Natalia and Mikołaj have paid 100 PLN
	And Natalia and Mikołaj have paid 50 PLN
	And Mikołaj and Natalia have paid 300 PLN
	When refund is recalculated
	Then Natalia should return 100 PLN to Mikołaj
	And Andrzej should return 400 PLN to Mikołaj
	And Andrzej should return 217 PLN to Natalia+Mikołaj