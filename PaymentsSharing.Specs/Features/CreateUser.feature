Feature: CreateUser
	Checking user creation

Scenario: Adding new user
	Given username is "Jan Kowalski"
	And password is "qwerty"
	And user does not eat meat
	When user is created
	Then user "Jan Kowalski" should exist