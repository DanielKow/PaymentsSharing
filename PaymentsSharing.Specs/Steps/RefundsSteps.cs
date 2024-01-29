using TechTalk.SpecFlow;

namespace PaymentsSharing.Specs.Steps;

[Binding]
public class RefundsSteps
{
    [Given("(.*) has paid (.*) PLN")]
    public void GivenPersonHasPaidPln(string person, int amount)
    {
        ScenarioContext.StepIsPending();
    }
}