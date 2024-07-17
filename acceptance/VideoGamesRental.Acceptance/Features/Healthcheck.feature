Feature: Healthcheck

@Acceptance
Scenario: My API should respond
    When I GET the Healthcheck 
    Then I should receive an OK result