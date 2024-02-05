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