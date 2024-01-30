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